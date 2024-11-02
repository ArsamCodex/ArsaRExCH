using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ArsaRExCH.InterfaceIMPL
{
    public class PostNadReplyInterfaceIMPL(IDbContextFactory<ApplicationDbContext> dbContextFactory) : PostNadReplyInterface
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory = dbContextFactory;


        // For posts
        public async Task<List<Post>> GetAllPostsAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Post.Include(p => p.Replies).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Post
                .Include(p => p.Replies) // Include replies
                .FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task CreatePostAsync(Post post)
        {
            using var context = _dbContextFactory.CreateDbContext();
           await context.Post.AddAsync(post);
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
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                await context.Reply.AddAsync(reply);
                await context.SaveChangesAsync();
                Console.WriteLine($"Content: added");
            }
            catch (Exception ex)
            {
                throw new Exception("daasdasdasd",ex);
            }

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

        public async Task<bool> AddReplyAsync(Reply reply, string userId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            try
            {
                var user = await context.Users.FindAsync(userId);
                if (user != null)
                {
                  //  reply.ApplicationUserId = userId; // Set the user ID of the reply
                    await context.Reply.AddAsync(reply);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

