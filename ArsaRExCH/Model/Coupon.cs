using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Model
{
    public class Coupon
    {
        public string CouponId { get; set; }
        public string Details { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DateReedemt { get; set; }
        public string Receiver { get; set; }
        [Precision(18,8)]
        public decimal Amount { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string IssuedByAdmin { get; set; }



    }
}
