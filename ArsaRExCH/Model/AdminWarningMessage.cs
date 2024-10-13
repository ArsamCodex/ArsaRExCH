namespace ArsaRExCH.Model
{
    public class AdminWarningMessage
    {
        public int AdminWarningMessageId { get; set; }
        public string AdminUserId { get; set; }
        public DateTime time { get; set; }
        public string? Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}
