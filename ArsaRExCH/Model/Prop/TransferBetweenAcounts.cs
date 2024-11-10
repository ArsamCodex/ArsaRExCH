using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Model.Prop
{
    public class TransferBetweenAcounts
    {
        //Track Found Transfer Intern
        public int TransferBetweenAcountsId { get; set; }
        [Precision(18,2)]
        public string From { get; set; }
        [Precision(18, 2)]
        public string To { get; set; }
        public DateTime DateTimeC { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
