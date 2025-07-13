using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Repositories
{
    public interface IFeedRepository
    {
        Task AddPostAsync(FeedPost post);
        Task<List<FeedPost>> GetPostsWithCommentsAsync(int offset, int limit, string? role, Guid? currentUserId);
        Task<FeedPost?> GetPostByIdAsync(int postId);
        Task UpdatePostAsync(FeedPost post);
        Task<bool> DeletePostAsync(int postId);

        Task<FeedComment?> AddCommentAsync(int postId, Guid userId, string text);
        Task<FeedComment?> GetCommentByIdAsync(int commentId);
        Task UpdateCommentAsync(FeedComment comment);
        Task<bool> DeleteCommentAsync(int commentId);

        Task<User?> GetUserByIdAsync(Guid userId);
    }
}
