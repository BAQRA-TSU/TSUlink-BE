using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public interface IFeedService
    {

        Task<FeedPost?> GetPostByIdAsync(int postId);
        Task UpdatePostAsync(FeedPost post);
        Task<bool> DeletePostAsync(int postId);

        Task<FeedComment?> GetCommentByIdAsync(int commentId);
        Task UpdateCommentAsync(FeedComment comment);
        Task<bool> DeleteCommentAsync(int commentId);


        Task AddPostAsync(FeedPost post);
        //Task<List<FeedPostDto>> GetPostsAsync(int offset, int limit);
        Task<List<FeedPostWithCommentsDto>> GetPostsWithCommentsAsync(int offset, int limit, string? role);
        //Task AddCommentAsync(int postId, Guid userId, string text);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<FeedComment> AddCommentAsync(int postId, Guid userId, string text);
    }
}
