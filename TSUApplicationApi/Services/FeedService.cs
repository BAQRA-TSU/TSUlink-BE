using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public class FeedService : IFeedService
    {
        private readonly ApplicationDbContext _context;

        public FeedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPostAsync(FeedPost post)
        {
            _context.FeedPosts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FeedPostWithCommentsDto>> GetPostsWithCommentsAsync(int offset, int limit)
        {
            return await _context.FeedPosts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .OrderByDescending(p => p.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .Select(p => new FeedPostWithCommentsDto
                {
                    Id = p.Id,
                    Name = p.User.Username,
                    Text = p.Content,
                    Comments = p.Comments
                        .OrderBy(c => c.CreatedAt)
                        .Select(c => new FeedCommentDto
                        {
                            Name = c.User.Username,
                            Text = c.Text
                        }).ToList()
                })
                .ToListAsync();
        }

        public async Task AddCommentAsync(int postId, Guid userId, string text)
        {
            var comment = new FeedComment
            {
                FeedPostId = postId,
                UserId = userId,
                Text = text,
                CreatedAt = DateTime.UtcNow
            };

            _context.FeedComments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
