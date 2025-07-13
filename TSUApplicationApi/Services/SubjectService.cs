using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;
using TSUApplicationApi.Repositories;

namespace TSUApplicationApi.Services
{
    public class SubjectService : ISubjectService
    {
        //    private readonly ApplicationDbContext _context;
        //    private readonly IWebHostEnvironment _environment;
        //    public SubjectService(ApplicationDbContext context, IWebHostEnvironment environment)
        //    {
        //        _context = context;
        //        _environment = environment;
        //    }

        //    public async Task<SubjectDetailDto?> GetByIdAsync(int id, string? role = null, Guid? currentUserId = null)
        //    {
        //        var subject = await _context.Subjects
        //            .Include(s => s.LecturerSubjects)
        //                .ThenInclude(ls => ls.Lecturer)
        //            //.Include(s => s.Files)
        //            .Include(s => s.SubjectReviews)
        //            .ThenInclude(r => r.User)
        //            //.Include(s => s.Files)
        //            .FirstOrDefaultAsync(l => l.Id == id);

        //        if (subject == null)
        //            return null;

        //        var files = await _context.SubjectFiles
        //    .Where(f => f.SubjectId == subject.Id)
        //    .Select(f => new FileDto
        //    {
        //        FileName = f.FileName,
        //        FileUrl = $"/api/subjects/{subject.Id}/files/{f.Id}"
        //    }).ToListAsync();

        //        var dto = new SubjectDetailDto
        //        {
        //            Name = subject.Name,
        //            Description = subject.Description,
        //            Files = files,
        //            //Files = _mapper.Map<List<FileDto>>(subject.Files),
        //            //Reviews = _mapper.Map<List<ReviewDto>>(subject.Reviews),

        //            //Files = subject.Files.Select(f => new FileDto
        //            //{
        //            //    FileName = f.FileName,
        //            //    FileUrl = $"/api/subjects/{subject.Id}/files/{f.Id}"
        //            //}).ToList(),

        //            Reviews = subject.SubjectReviews
        //            .Where(r => role == "Admin" || r.IsApproved || (currentUserId != null && r.UserId == currentUserId))
        //            .Select(r => new ReviewDto
        //            {
        //                Id = r.Id,
        //                Name = /*r.User.Username,*/ $"{r.User.FirstName} {r.User.LastName}", // სრული სახელი
        //                Review = r.Text,          // მიმოხილვის ტექსტი
        //                IsApproved = role == "Admin" ? r.IsApproved : null,
        //                CanDelete = role == "Admin" || (currentUserId != null && r.UserId == currentUserId),
        //                Status = r.IsApproved ? "approved" : (r.UserId == currentUserId ? "pending" : null)
        //            }).ToList(),
        //            Lecturers = new LecturerGroupedDto
        //            {
        //                Lecture = subject.LecturerSubjects
        //                    .Where(ls => ls.Type == "lecturer")
        //                    .Select(ls => new LecturerDto
        //                    {
        //                        Id = ls.Lecturer.Id,
        //                        Name = ls.Lecturer.FullName
        //                    })
        //                    .ToList(),

        //                Practical = subject.LecturerSubjects
        //                    .Where(ls => ls.Type == "practical")
        //                    .Select(ls => new LecturerDto
        //                    {
        //                        Id = ls.Lecturer.Id,
        //                        Name = ls.Lecturer.FullName
        //                    })
        //                    .ToList(),

        //                Lab = subject.LecturerSubjects
        //                    .Where(ls => ls.Type == "lab")
        //                    .Select(ls => new LecturerDto
        //                    {
        //                        Id = ls.Lecturer.Id,
        //                        Name = ls.Lecturer.FullName
        //                    })
        //                    .ToList(),
        //            }
        //        };

        //        return dto;
        //    }

        //    public async Task<bool> SubjectExistsAsync(int subjectId)
        //    {
        //        return await _context.Subjects.AnyAsync(l => l.Id == subjectId);
        //    }

        //    public async Task AddReviewAsync(SubjectReview review)
        //    {
        //        _context.SubjectReviews.Add(review);
        //        await _context.SaveChangesAsync();
        //    }
        //    public async Task<User?> GetUserByIdAsync(Guid userId)
        //    {
        //        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        //    }

        //    //public async Task<(bool Success, string Message, string FileName)> UploadFileAsync(int subjectId, IFormFile file)
        //    //{
        //    //    Console.WriteLine($"WebRootPath: {_environment.WebRootPath}");
        //    //    if (file == null || file.Length == 0)
        //    //        return (false, "No file uploaded.", null);

        //    //    var subject = await _context.Subjects.FindAsync(subjectId);
        //    //    if (subject == null)
        //    //        return (false, "Subject not found.", null);

        //    //    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "subjects");
        //    //    Directory.CreateDirectory(uploadsFolder);

        //    //    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //    //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    //    {
        //    //        await file.CopyToAsync(stream);
        //    //    }

        //    //    var subjectFile = new SubjectFile
        //    //    {
        //    //        FileName = uniqueFileName,
        //    //        SubjectId = subjectId
        //    //    };

        //    //    _context.SubjectFiles.Add(subjectFile);
        //    //    await _context.SaveChangesAsync();

        //    //    return (true, "File uploaded successfully.", uniqueFileName);
        //    //}

        //    public async Task<(bool Success, string Message, FileDto? File)> UploadFileAsync(int subjectId, IFormFile file)
        //    {
        //        if (file == null || file.Length == 0)
        //            return (false, "No file uploaded.", null);

        //        var subject = await _context.Subjects.FindAsync(subjectId);
        //        if (subject == null)
        //            return (false, "Subject not found.", null);

        //        using var memoryStream = new MemoryStream();
        //        await file.CopyToAsync(memoryStream);
        //        var fileBytes = memoryStream.ToArray();

        //        var subjectFile = new SubjectFile
        //        {
        //            FileName = file.FileName,
        //            ContentType = file.ContentType,
        //            FileContent = fileBytes,
        //            SubjectId = subjectId
        //        };

        //        _context.SubjectFiles.Add(subjectFile);
        //        await _context.SaveChangesAsync();

        //        var fileDto = new FileDto
        //        {
        //            FileName = file.FileName,
        //            FileUrl = $"/api/subjects/{subjectId}/files/{subjectFile.Id}" 
        //        };

        //        return (true, "File uploaded successfully.", fileDto);
        //    }

        //    //public async Task<(byte[] FileData, string FileName, string ContentType)?> DownloadFileAsync(int fileId)
        //    //{
        //    //    var file = await _context.SubjectFiles.FindAsync(fileId);
        //    //    if (file == null)
        //    //        return null;

        //    //    return (file.FileContent, file.FileName, file.ContentType);
        //    //}

        //    //public async Task AddFileAsync(SubjectFile file)
        //    //{
        //    //    _context.SubjectFiles.Add(file);
        //    //    await _context.SaveChangesAsync();
        //    //}

        //    //public async Task<SubjectFile?> GetFileByIdAsync(int fileId, int subjectId)
        //    //{
        //    //    return await _context.SubjectFiles
        //    //        .FirstOrDefaultAsync(f => f.Id == fileId && f.SubjectId == subjectId);
        //    //}

        //    public async Task<SubjectFile?> DownloadFileAsync(int fileId, int subjectId)
        //    {
        //        return await _context.SubjectFiles
        //            .FirstOrDefaultAsync(f => f.Id == fileId && f.SubjectId == subjectId);
        //    }

        //    //-----------------------------------------//
        //    public async Task<SubjectReview?> GetSubjectReviewByIdAsync(int reviewId)
        //=> await _context.SubjectReviews.FindAsync(reviewId);

        //    public async Task UpdateSubjectReviewAsync(SubjectReview review)
        //    {
        //        _context.SubjectReviews.Update(review);
        //        await _context.SaveChangesAsync();
        //    }

        //    public async Task<bool> DeleteSubjectReviewAsync(int reviewId)
        //    {
        //        var review = await _context.SubjectReviews.FindAsync(reviewId);
        //        if (review == null) return false;

        //        _context.SubjectReviews.Remove(review);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }


        private readonly ISubjectRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public SubjectService(ISubjectRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public async Task<SubjectDetailDto?> GetByIdAsync(int id, string? role = null, Guid? currentUserId = null)
        {
            var subject = await _repository.GetByIdWithDetailsAsync(id);
            if (subject == null) return null;

            var files = (await _repository.GetFilesAsync(subject.Id)).Select(f => new FileDto
            {
                FileName = f.FileName,
                FileUrl = $"/api/subjects/{subject.Id}/files/{f.Id}"
            }).ToList();

            var dto = new SubjectDetailDto
            {
                Name = subject.Name,
                Description = subject.Description,
                Files = files,
                Reviews = subject.SubjectReviews
                    .Where(r => role == "Admin" || r.IsApproved || (currentUserId != null && r.UserId == currentUserId))
                    .Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Name = $"{r.User.FirstName} {r.User.LastName}",
                        Review = r.Text,
                        IsApproved = role == "Admin" ? r.IsApproved : null,
                        CanDelete = role == "Admin" || (currentUserId != null && r.UserId == currentUserId),
                        Status = r.IsApproved ? "approved" : (r.UserId == currentUserId ? "pending" : null)
                    }).ToList(),
                Lecturers = new LecturerGroupedDto
                {
                    Lecture = subject.LecturerSubjects
                        .Where(ls => ls.Type == "lecturer")
                        .Select(ls => new LecturerDto { Id = ls.Lecturer.Id, Name = ls.Lecturer.FullName }).ToList(),
                    Practical = subject.LecturerSubjects
                        .Where(ls => ls.Type == "practical")
                        .Select(ls => new LecturerDto { Id = ls.Lecturer.Id, Name = ls.Lecturer.FullName }).ToList(),
                    Lab = subject.LecturerSubjects
                        .Where(ls => ls.Type == "lab")
                        .Select(ls => new LecturerDto { Id = ls.Lecturer.Id, Name = ls.Lecturer.FullName }).ToList()
                }
            };

            return dto;
        }

        public async Task<bool> SubjectExistsAsync(int subjectId) =>
            await _repository.FindByIdAsync(subjectId) is not null;

        public Task AddReviewAsync(SubjectReview review) =>
            _repository.AddReviewAsync(review);

        public Task<User?> GetUserByIdAsync(Guid userId) =>
            _repository.GetUserByIdAsync(userId);

        public async Task<(bool Success, string Message, FileDto? File)> UploadFileAsync(int subjectId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, "No file uploaded.", null);

            var subject = await _repository.FindByIdAsync(subjectId);
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

            await _repository.AddFileAsync(subjectFile);

            var fileDto = new FileDto
            {
                FileName = file.FileName,
                FileUrl = $"/api/subjects/{subjectId}/files/{subjectFile.Id}"
            };

            return (true, "File uploaded successfully.", fileDto);
        }

        public Task<SubjectFile?> DownloadFileAsync(int fileId, int subjectId) =>
            _repository.GetFileAsync(fileId, subjectId);

        public Task<SubjectReview?> GetSubjectReviewByIdAsync(int reviewId) =>
            _repository.GetReviewByIdAsync(reviewId);

        public Task UpdateSubjectReviewAsync(SubjectReview review) =>
            _repository.UpdateReviewAsync(review);

        public Task<bool> DeleteSubjectReviewAsync(int reviewId) =>
            _repository.DeleteReviewAsync(reviewId);



    }
}
