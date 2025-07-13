using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subject?> GetByIdWithDetailsAsync(int id) =>
            await _context.Subjects
                .Include(s => s.LecturerSubjects).ThenInclude(ls => ls.Lecturer)
                .Include(s => s.SubjectReviews).ThenInclude(r => r.User)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<List<SubjectFile>> GetFilesAsync(int subjectId) =>
            await _context.SubjectFiles.Where(f => f.SubjectId == subjectId).ToListAsync();

        public async Task<Subject?> FindByIdAsync(int subjectId) =>
            await _context.Subjects.FindAsync(subjectId);

        public async Task AddReviewAsync(SubjectReview review)
        {
            _context.SubjectReviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        public async Task AddFileAsync(SubjectFile file)
        {
            _context.SubjectFiles.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task<SubjectFile?> GetFileAsync(int fileId, int subjectId) =>
            await _context.SubjectFiles.FirstOrDefaultAsync(f => f.Id == fileId && f.SubjectId == subjectId);

        public async Task<SubjectReview?> GetReviewByIdAsync(int reviewId) =>
            await _context.SubjectReviews.FindAsync(reviewId);

        public async Task UpdateReviewAsync(SubjectReview review)
        {
            _context.SubjectReviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _context.SubjectReviews.FindAsync(reviewId);
            if (review == null) return false;

            _context.SubjectReviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
