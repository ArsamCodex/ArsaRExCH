namespace ArsaRExCH.Model
{
    public class UserClient
    {  /*Here about Class ID i chose for INT . its range is -2,147,483,648 to 2,147,483,647
         in fact we should use GUID but for now its ok */
        public Guid UserClientId { get; set; }
        public string UserInDbId { get; set; }
        public ICollection<Order> Orders { get; set; }
      //  public ICollection<Wallet> Wallets { get; set; }

    }
}
