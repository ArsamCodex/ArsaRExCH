namespace ArsaRExCH.Model
{
    public class UserClient
    {
        public Guid UserClientId { get; set; }
        public string UserInDbId { get; set; }
        public ICollection<Order> Orders { get; set; }
      //  public ICollection<Wallet> Wallets { get; set; }

    }
}
