namespace ArsaRExCH.Model
{
    public class Order
    {  /*Here about Class ID i chose for INT . its range is -2,147,483,648 to 2,147,483,647
         in fact we should use GUID but for now its ok */
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderType OrderType { get; set; }
        public string PairName { get; set; }
        public double Price { get; set; }

       // public int UserId { get; set; }
       // public UserClient User { get; set; }

  
    }
}
