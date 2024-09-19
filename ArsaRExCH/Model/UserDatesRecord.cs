using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class UserDatesRecord
    {
        public int UserDatesRecordId { get; set; }
        public DateTime UserLoggedInDates { get; set; }
        //return local ipadress
        public string? UserIpAdressX { get; set; }
        //        public string? UserIpAdressPublic { get; set; }

        // Foreign key property for ApplicationUser
        public string ApplicationUserId { get; set; }

        // Navigation property
        public ApplicationUser ApplicationUser { get; set; }
    }
}
