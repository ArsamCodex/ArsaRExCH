namespace ArsaRExCH.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderType { get; set; }
        public string PairName { get; set; }
        public double Price { get; set; }

        public int UserId { get; set; }
        public UserClient User { get; set; }

  
    }
}
