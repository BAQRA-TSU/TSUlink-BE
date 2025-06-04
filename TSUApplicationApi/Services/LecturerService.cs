using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public class LecturerService : ILecturerService
    {
        private readonly ApplicationDbContext _context;

        public LecturerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LecturerDetailDto> GetByIdAsync(int id)
        {
            var lecturer = await _context.Lecturers
                .Include(l => l.LecturerSubjects)
                    .ThenInclude(ls => ls.Subject)
                .Include(l => l.LecturerReviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lecturer == null)
                return null;

            return new LecturerDetailDto
            {
                Name = lecturer.FullName,
                Subjects = lecturer.LecturerSubjects
                    .Select(ls => new SubjectItemDto
                    {
                        Name = ls.Subject.Name,
                        Id = ls.LecturerId
                        //ShortName = ls.Subject.ShortName
                    }).ToList(),
                Information = new ContactInfoDto
                {
                    Email = lecturer.Email,
                    PhoneNumber = lecturer.PhoneNumber,
                    Office = lecturer.Office
                },
                Reviews = lecturer.LecturerReviews
                    .Select(r => new ReviewDto { Name = /*r.User.Username*/$"{r.User.FirstName} {r.User.LastName}",Review = r.Text })
                    .ToList()
            };
        }

        public async Task<bool> LecturerExistsAsync(int lecturerId)
        {
            return await _context.Lecturers.AnyAsync(l => l.Id == lecturerId);
        }

        public async Task AddReviewAsync(LecturerReview review)
        {
            _context.LecturerReviews.Add(review);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
