using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public interface IFeedService
    {
        Task AddPostAsync(FeedPost post);
        //Task<List<FeedPostDto>> GetPostsAsync(int offset, int limit);
        Task<List<FeedPostWithCommentsDto>> GetPostsWithCommentsAsync(int offset, int limit);
        //Task AddCommentAsync(int postId, Guid userId, string text);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<FeedComment> AddCommentAsync(int postId, Guid userId, string text);
    }
}
