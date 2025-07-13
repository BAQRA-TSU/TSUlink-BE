using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Repositories
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly ApplicationDbContext _context;

        public LecturerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Lecturer?> GetLecturerWithDetailsAsync(int id)
        {
            return await _context.Lecturers
                .Include(l => l.LecturerSubjects)
                    .ThenInclude(ls => ls.Subject)
                .Include(l => l.LecturerReviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> LecturerExistsAsync(int id)
        {
            return await _context.Lecturers.AnyAsync(l => l.Id == id);
        }

        public async Task AddReviewAsync(LecturerReview review)
        {
            _context.LecturerReviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<LecturerReview?> GetReviewByIdAsync(int reviewId)
        {
            return await _context.LecturerReviews.FindAsync(reviewId);
        }

        public async Task UpdateReviewAsync(LecturerReview review)
        {
            _context.LecturerReviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.LecturerReviews.FindAsync(reviewId);
            if (review == null) return false;

            _context.LecturerReviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
