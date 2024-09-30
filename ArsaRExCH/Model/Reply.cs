using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class Reply
    {
        public int ReplyID { get; set; }
        public string Content { get; set; }
        public DateTime RepliedAt { get; set; }
        public string UserIdRelied { get; set; }

        // Foreign key for the Post being replied to
        public int PostId { get; set; }
        public Post Post { get; set; } // The post to which this reply belongs
    }
}
