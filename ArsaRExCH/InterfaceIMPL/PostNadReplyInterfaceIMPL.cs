using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class PostNadReplyInterfaceIMPL : PostNadReplyInterface
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public PostNadReplyInterfaceIMPL(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // For posts
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Post.Include(p => p.Replies).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Post.Include(p => p.Replies)
                                      .FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task CreatePostAsync(Post post)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Post.Add(post);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Post.Update(post);
            await context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var post = await context.Post.FindAsync(postId);
            if (post != null)
            {
                context.Post.Remove(post);
                await context.SaveChangesAsync();
            }
        }

        // For replies
        public async Task<IEnumerable<Reply>> GetRepliesForPostAsync(int postId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Reply.Where(r => r.PostId == postId).ToListAsync();
        }

        public async Task CreateReplyAsync(Reply reply)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Reply.Add(reply);
            await context.SaveChangesAsync();
        }

        public async Task DeleteReplyAsync(int replyId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var reply = await context.Reply.FindAsync(replyId);
            if (reply != null)
            {
                context.Reply.Remove(reply);
                await context.SaveChangesAsync();
            }
        }
    }
}

