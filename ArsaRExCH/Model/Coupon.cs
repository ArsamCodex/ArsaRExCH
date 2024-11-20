using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArsaRExCH.Model
{
    public class Coupon
    {
        public string CouponId { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public DateTime DateIssued { get; set; }
        public DateTime DateReedemt { get; set; }
        [Required]
        public string Receiver { get; set; }
        [Required]
        [Precision(18,8)]
        public decimal Amount { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? ExpirationDate { get; set; }
        [Required]
        public string IssuedByAdmin { get; set; }


    }
}
