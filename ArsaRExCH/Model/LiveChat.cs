using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class LiveChat
    {
        public int LiveChatId { get; set; }
        public string  Message { get; set; }
        public DateTime TimeAndDate { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser user { get; set; }

    }
}
