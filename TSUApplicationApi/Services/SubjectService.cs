using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public SubjectService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<SubjectDetailDto?> GetByIdAsync(int id)
        {
            var subject = await _context.Subjects
                .Include(s => s.LecturerSubjects)
                    .ThenInclude(ls => ls.Lecturer)
                //.Include(s => s.Files)
                .Include(s => s.SubjectReviews)
                .ThenInclude(r => r.User)
                .Include(s => s.Files)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (subject == null)
                return null;


            var dto = new SubjectDetailDto
            {
                Name = subject.Name,
                Description = subject.Description,
                //Files = _mapper.Map<List<FileDto>>(subject.Files),
                //Reviews = _mapper.Map<List<ReviewDto>>(subject.Reviews),

                Files = subject.Files.Select(f => new FileDto
                {
                    FileName = f.FileName,
                    FileUrl = $"/api/subjects/{subject.Id}/files/{f.Id}"
                }).ToList(),

                Reviews = subject.SubjectReviews.Select(r => new ReviewDto
                {
                    Name = /*r.User.Username,*/ $"{r.User.FirstName} {r.User.LastName}", // სრული სახელი
                    Review = r.Text          // მიმოხილვის ტექსტი
                }).ToList(),
                Lecturers = new LecturerGroupedDto
                {
                    Lecture = subject.LecturerSubjects
                        .Where(ls => ls.Type == "lecturer")
                        .Select(ls => new LecturerDto
                        {
                            Id = ls.Lecturer.Id,
                            Name = $"{ls.Lecturer.FullName} {ls.Lecturer.Id}"
                        })
                        .ToList(),

                    Practical = subject.LecturerSubjects
                        .Where(ls => ls.Type == "practical")
                        .Select(ls => new LecturerDto
                        {
                            Id = ls.Lecturer.Id,
                            Name = $"{ls.Lecturer.FullName} {ls.Lecturer.Id}"
                        })
                        .ToList(),

                    Lab = subject.LecturerSubjects
                        .Where(ls => ls.Type == "lab")
                        .Select(ls => new LecturerDto
                        {
                            Id = ls.Lecturer.Id,
                            Name = $"{ls.Lecturer.FullName}   {ls.Lecturer.Id}"
                        })
                        .ToList(),
                }
            };

            return dto;
        }

        public async Task<bool> SubjectExistsAsync(int subjectId)
        {
            return await _context.Subjects.AnyAsync(l => l.Id == subjectId);
        }

        public async Task AddReviewAsync(SubjectReview review)
        {
            _context.SubjectReviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        //public async Task<(bool Success, string Message, string FileName)> UploadFileAsync(int subjectId, IFormFile file)
        //{
        //    Console.WriteLine($"WebRootPath: {_environment.WebRootPath}");
        //    if (file == null || file.Length == 0)
        //        return (false, "No file uploaded.", null);

        //    var subject = await _context.Subjects.FindAsync(subjectId);
        //    if (subject == null)
        //        return (false, "Subject not found.", null);

        //    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "subjects");
        //    Directory.CreateDirectory(uploadsFolder);

        //    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    var subjectFile = new SubjectFile
        //    {
        //        FileName = uniqueFileName,
        //        SubjectId = subjectId
        //    };

        //    _context.SubjectFiles.Add(subjectFile);
        //    await _context.SaveChangesAsync();

        //    return (true, "File uploaded successfully.", uniqueFileName);
        //}

        public async Task<(bool Success, string Message, FileDto? File)> UploadFileAsync(int subjectId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, "No file uploaded.", null);

            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject == null)
                return (false, "Subject not found.", null);

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var subjectFile = new SubjectFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileContent = fileBytes,
                SubjectId = subjectId
            };

            _context.SubjectFiles.Add(subjectFile);
            await _context.SaveChangesAsync();

            var fileDto = new FileDto
            {
                FileName = file.FileName,
                FileUrl = $"/api/subjects/{subjectId}/files/{subjectFile.Id}" 
            };

            return (true, "File uploaded successfully.", fileDto);
        }

        //public async Task<(byte[] FileData, string FileName, string ContentType)?> DownloadFileAsync(int fileId)
        //{
        //    var file = await _context.SubjectFiles.FindAsync(fileId);
        //    if (file == null)
        //        return null;

        //    return (file.FileContent, file.FileName, file.ContentType);
        //}

        //public async Task AddFileAsync(SubjectFile file)
        //{
        //    _context.SubjectFiles.Add(file);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<SubjectFile?> GetFileByIdAsync(int fileId, int subjectId)
        //{
        //    return await _context.SubjectFiles
        //        .FirstOrDefaultAsync(f => f.Id == fileId && f.SubjectId == subjectId);
        //}

        public async Task<SubjectFile?> DownloadFileAsync(int fileId, int subjectId)
        {
            return await _context.SubjectFiles
                .FirstOrDefaultAsync(f => f.Id == fileId && f.SubjectId == subjectId);
        }




    }
}
