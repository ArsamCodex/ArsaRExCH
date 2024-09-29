using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class Post
    {
        public int PostId { get; set; }
          public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign key for ApplicationUser (Admin)
        public string ApplicationUserId { get; set; }
        public ApplicationUser Admin { get; set; } // Admin user who created the post

        // Navigation property for replies
        public ICollection<Reply> Replies { get; set; }
    }
}
