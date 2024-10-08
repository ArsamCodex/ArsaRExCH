using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class LiveChat
    {
        /*Here for live chat you can use SignalR for real time but i like less librries in my app
         * thats why i implement manually you can use if you like */
        public int LiveChatId { get; set; }
        public string  Message { get; set; }
        public DateTime TimeAndDate { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser user { get; set; }

    }
}
