using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;
using TSUApplicationApi.Repositories;

namespace TSUApplicationApi.Services
{
    public class LecturerService : ILecturerService
    {
        //    private readonly ApplicationDbContext _context;

        //    public LecturerService(ApplicationDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<LecturerDetailDto> GetByIdAsync(int id, string? role = null, Guid? currentUserId = null)
        //    {
        //        var lecturer = await _context.Lecturers
        //            .Include(l => l.LecturerSubjects)
        //                .ThenInclude(ls => ls.Subject)
        //            .Include(l => l.LecturerReviews)
        //            .ThenInclude(r => r.User)
        //            .FirstOrDefaultAsync(l => l.Id == id);

        //        if (lecturer == null)
        //            return null;

        //        return new LecturerDetailDto
        //        {
        //            Name = lecturer.FullName,
        //            Subjects = lecturer.LecturerSubjects
        //                .GroupBy(ls => ls.Subject.Id)
        //                .Select(g => g.First().Subject)
        //                .Select(subject => new SubjectItemDto
        //                {
        //                    Id = subject.Id,
        //                    Name = subject.Name
        //                }).ToList(),
        //            Information = new ContactInfoDto
        //            {
        //                Email = lecturer.Email,
        //                PhoneNumber = lecturer.PhoneNumber,
        //                Office = lecturer.Office
        //            },
        //            Reviews = lecturer.LecturerReviews
        //                .Where(r => role == "Admin" || r.IsApproved || (currentUserId != null && r.UserId == currentUserId))
        //                .Select(r => new ReviewDto { Id = r.Id, Name = /*r.User.Username*/$"{r.User.FirstName} {r.User.LastName}",Review = r.Text,
        //                    IsApproved = role == "Admin" ? r.IsApproved : null,
        //                    CanDelete = role == "Admin" || (currentUserId != null && r.UserId == currentUserId),
        //                    Status = r.IsApproved ? "approved" : (r.UserId == currentUserId ? "pending" : null)
        //                })
        //                .ToList()
        //        };
        //    }

        //    public async Task<bool> LecturerExistsAsync(int lecturerId)
        //    {
        //        return await _context.Lecturers.AnyAsync(l => l.Id == lecturerId);
        //    }

        //    public async Task AddReviewAsync(LecturerReview review)
        //    {
        //        _context.LecturerReviews.Add(review);
        //        await _context.SaveChangesAsync();
        //    }
        //    public async Task<User?> GetUserByIdAsync(Guid userId)
        //    {
        //        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        //    }
        //    //-----------------------------------------

        //    public async Task<LecturerReview?> GetLecturerReviewByIdAsync(int reviewId)
        //=> await _context.LecturerReviews.FindAsync(reviewId);

        //    public async Task UpdateLecturerReviewAsync(LecturerReview review)
        //    {
        //        _context.LecturerReviews.Update(review);
        //        await _context.SaveChangesAsync();
        //    }

        //    public async Task<bool> DeleteLecturerReviewAsync(int reviewId)
        //    {
        //        var review = await _context.LecturerReviews.FindAsync(reviewId);
        //        if (review == null) return false;

        //        _context.LecturerReviews.Remove(review);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }


        private readonly ILecturerRepository _repository;

        public LecturerService(ILecturerRepository repository)
        {
            _repository = repository;
        }

        public async Task<LecturerDetailDto> GetByIdAsync(int id, string? role = null, Guid? currentUserId = null)
        {
            var lecturer = await _repository.GetLecturerWithDetailsAsync(id);
            if (lecturer == null) return null;

            return new LecturerDetailDto
            {
                Name = lecturer.FullName,
                Subjects = lecturer.LecturerSubjects
                    .GroupBy(ls => ls.Subject.Id)
                    .Select(g => g.First().Subject)
                    .Select(subject => new SubjectItemDto
                    {
                        Id = subject.Id,
                        Name = subject.Name
                    }).ToList(),
                Information = new ContactInfoDto
                {
                    Email = lecturer.Email,
                    PhoneNumber = lecturer.PhoneNumber,
                    Office = lecturer.Office
                },
                Reviews = lecturer.LecturerReviews
                    .Where(r => role == "Admin" || r.IsApproved || (currentUserId != null && r.UserId == currentUserId))
                    .Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Name = $"{r.User.FirstName} {r.User.LastName}",
                        Review = r.Text,
                        IsApproved = role == "Admin" ? r.IsApproved : null,
                        CanDelete = role == "Admin" || (currentUserId != null && r.UserId == currentUserId),
                        Status = r.IsApproved ? "approved" : (r.UserId == currentUserId ? "pending" : null)
                    })
                    .ToList()
            };
        }

        public async Task<bool> LecturerExistsAsync(int lecturerId)
            => await _repository.LecturerExistsAsync(lecturerId);

        public async Task AddReviewAsync(LecturerReview review)
            => await _repository.AddReviewAsync(review);

        public async Task<User?> GetUserByIdAsync(Guid userId)
            => await _repository.GetUserByIdAsync(userId);

        public async Task<LecturerReview?> GetLecturerReviewByIdAsync(int reviewId)
            => await _repository.GetReviewByIdAsync(reviewId);

        public async Task UpdateLecturerReviewAsync(LecturerReview review)
            => await _repository.UpdateReviewAsync(review);

        public async Task<bool> DeleteLecturerReviewAsync(int reviewId)
            => await _repository.DeleteReviewAsync(reviewId);


    }
}
