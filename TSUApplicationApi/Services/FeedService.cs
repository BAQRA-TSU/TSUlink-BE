using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;
using TSUApplicationApi.Repositories;

namespace TSUApplicationApi.Services
{
    public class FeedService : IFeedService
    {
        //private readonly ApplicationDbContext _context;

        //public FeedService(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task AddPostAsync(FeedPost post)
        //{
        //    _context.FeedPosts.Add(post);
        //    await _context.SaveChangesAsync();
        //}

        ////public async Task<List<FeedPostWithCommentsDto>> GetPostsWithCommentsAsync(int offset, int limit)
        ////{
        ////    return await _context.FeedPosts
        ////        .Where(p => p.IsApproved)
        ////        .Include(p => p.User)
        ////        .Include(p => p.Comments.Where(c => c.IsApproved))
        ////            .ThenInclude(c => c.User)
        ////        .OrderByDescending(p => p.CreatedAt)
        ////        .Skip(offset)
        ////        .Take(limit)
        ////        .Select(p => new FeedPostWithCommentsDto
        ////        {
        ////            Id = p.Id,
        ////            Name = p.User.FirstName + " " + p.User.LastName, 
        ////            Text = p.Content,
        ////            Comments = p.Comments
        ////                .OrderBy(c => c.CreatedAt)
        ////                .Select(c => new FeedCommentDto
        ////                {
        ////                    Name = c.User.FirstName + " " + c.User.LastName,
        ////                    Text = c.Text
        ////                }).ToList()
        ////        })
        ////        .ToListAsync();
        ////}


        //public async Task<List<FeedPostWithCommentsDto>> GetPostsWithCommentsAsync(int offset, int limit, string? role, Guid? currentUserId = null)
        //{
        //    var query = _context.FeedPosts
        //        .Include(p => p.User)
        //        .Include(p => p.Comments)
        //            .ThenInclude(c => c.User)
        //        .AsQueryable();


        //    if (role != "Admin")
        //    {
        //        query = query.Where(p => p.IsApproved || p.UserId == currentUserId);
        //    }

        //    var posts = await query
        //        .OrderByDescending(p => p.CreatedAt)
        //        .Skip(offset)
        //        .Take(limit)
        //        .ToListAsync();

        //    return posts.Select(p => new FeedPostWithCommentsDto
        //    {
        //        Id = p.Id,
        //        Name = p.User.FirstName + " " + p.User.LastName,
        //        Text = p.Content,
        //        //IsApproved = p.IsApproved,
        //        IsApproved = role == "Admin" ? p.IsApproved : null,
        //        Status = p.IsApproved ? "approved" : (p.UserId == currentUserId ? "pending" : null),
        //        CanDelete = role == "Admin" || (currentUserId != null && p.UserId == currentUserId),
        //        Comments = p.Comments

        //    .OrderBy(c => c.CreatedAt)
        //    .Select(c => new FeedCommentDto
        //    {
        //        Id = c.Id,
        //        Name = c.User.FirstName + " " + c.User.LastName,
        //        Text = c.Text,
        //        CanDelete = role == "Admin" || (currentUserId != null && c.UserId == currentUserId)
        //        //IsApproved = role == "Admin" ? c.IsApproved : null // 
        //    }).ToList()
        //    }).ToList();
        //}

        //public async Task<FeedComment> AddCommentAsync(int postId, Guid userId, string text)
        //{
        //    var comment = new FeedComment
        //    {
        //        FeedPostId = postId,
        //        UserId = userId,
        //        Text = text,
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    _context.FeedComments.Add(comment);
        //    await _context.SaveChangesAsync();

        //    return await _context.FeedComments
        //.Include(c => c.User)
        //.FirstOrDefaultAsync(c => c.Id == comment.Id);
        //}
        //public async Task<User?> GetUserByIdAsync(Guid userId)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        //}

        ////--------------------------

        //public async Task<FeedPost?> GetPostByIdAsync(int postId)
        //{
        //    return await _context.FeedPosts.FindAsync(postId);
        //}

        //public async Task UpdatePostAsync(FeedPost post)
        //{
        //    _context.FeedPosts.Update(post);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeletePostAsync(int postId)
        //{
        //    var post = await _context.FeedPosts.FindAsync(postId);
        //    if (post == null)
        //        return false;

        //    _context.FeedPosts.Remove(post);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<FeedComment?> GetCommentByIdAsync(int commentId)
        //{
        //    return await _context.FeedComments.FindAsync(commentId);
        //}

        //public async Task UpdateCommentAsync(FeedComment comment)
        //{
        //    _context.FeedComments.Update(comment);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteCommentAsync(int commentId)
        //{
        //    var comment = await _context.FeedComments.FindAsync(commentId);
        //    if (comment == null)
        //        return false;

        //    _context.FeedComments.Remove(comment);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}


        private readonly IFeedRepository _repository;

        public FeedService(IFeedRepository repository)
        {
            _repository = repository;
        }

        public async Task AddPostAsync(FeedPost post)
        {
            await _repository.AddPostAsync(post);
        }

        public async Task<List<FeedPostWithCommentsDto>> GetPostsWithCommentsAsync(int offset, int limit, string? role, Guid? currentUserId = null)
        {
            var posts = await _repository.GetPostsWithCommentsAsync(offset, limit, role, currentUserId);

            return posts.Select(p => new FeedPostWithCommentsDto
            {
                Id = p.Id,
                Name = p.User.FirstName + " " + p.User.LastName,
                Text = p.Content,
                IsApproved = role == "Admin" ? p.IsApproved : null,
                Status = p.IsApproved ? "approved" : (p.UserId == currentUserId ? "pending" : null),
                CanDelete = role == "Admin" || (currentUserId != null && p.UserId == currentUserId),
                Comments = p.Comments
                    .OrderBy(c => c.CreatedAt)
                    .Select(c => new FeedCommentDto
                    {
                        Id = c.Id,
                        Name = c.User.FirstName + " " + c.User.LastName,
                        Text = c.Text,
                        CanDelete = role == "Admin" || (currentUserId != null && c.UserId == currentUserId)
                    }).ToList()
            }).ToList();
        }

        public async Task<FeedComment> AddCommentAsync(int postId, Guid userId, string text)
        {
            return await _repository.AddCommentAsync(postId, userId, text);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _repository.GetUserByIdAsync(userId);
        }

        public async Task<FeedPost?> GetPostByIdAsync(int postId)
        {
            return await _repository.GetPostByIdAsync(postId);
        }

        public async Task UpdatePostAsync(FeedPost post)
        {
            await _repository.UpdatePostAsync(post);
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            return await _repository.DeletePostAsync(postId);
        }

        public async Task<FeedComment?> GetCommentByIdAsync(int commentId)
        {
            return await _repository.GetCommentByIdAsync(commentId);
        }

        public async Task UpdateCommentAsync(FeedComment comment)
        {
            await _repository.UpdateCommentAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            return await _repository.DeleteCommentAsync(commentId);
        }

    }
}
