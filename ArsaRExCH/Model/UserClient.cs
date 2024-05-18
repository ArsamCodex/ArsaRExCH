namespace ArsaRExCH.Model
{
    public class UserClient
    {
        public int UserClientId { get; set; }
        public string UserInDbId { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
