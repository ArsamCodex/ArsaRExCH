using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class AirDrop
    {
        public int AirDropID { get; set; }
        public int AirDropBalance { get; set; }
        public DateTime TimeClick { get; set; }
        // Foreign key property for ApplicationUser
        public string ApplicationUserId { get; set; }

        // Navigation property
        public ApplicationUser ApplicationUser { get; set; }
    }
}
