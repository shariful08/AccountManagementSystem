namespace MiniAccountManagementSystem.Models
{
    public class VoucherDetail
    {
        public int? VoucherDetailId { get; set; }
        public int? VoucherMasterId { get; set; }
        public int? AccountId { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public string? Remarks { get; set; }
    }
}
