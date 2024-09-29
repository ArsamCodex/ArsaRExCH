using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface PostNadReplyInterface
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int postId);
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int postId);

        // For replies
        Task<IEnumerable<Reply>> GetRepliesForPostAsync(int postId);
        Task CreateReplyAsync(Reply reply);
        Task DeleteReplyAsync(int replyId);
    }
}
