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

        public SubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SubjectDetailDto?> GetByShortNameAsync(string shortName)
        {
            var subject = await _context.Subjects
                .Include(s => s.LecturerSubjects)
                    .ThenInclude(ls => ls.Lecturer)
                //.Include(s => s.Files)
                .Include(s => s.SubjectReviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(s => s.ShortName == shortName);

            if (subject == null)
                return null;

            var dto = new SubjectDetailDto
            {
                Name = subject.Name,
                Description = subject.Description,
                //Files = _mapper.Map<List<FileDto>>(subject.Files),
                //Reviews = _mapper.Map<List<ReviewDto>>(subject.Reviews),
                Reviews = subject.SubjectReviews.Select(r => new ReviewDto
                {
                    Name = r.User.Username, // მიმოხილვის ავტორის სრული სახელი
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

    }
}
