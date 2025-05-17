namespace MiniAccountManagementSystem.Models
{
    public class VoucherModel
    {
        public string VoucherType { get; set; }
        public DateTime VoucherDate { get; set; }
        public string? ReferenceNo { get; set; }
        public int AccountID { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string? remarks { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? AddedPc { get; set; }
        public string? AddedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedPc { get; set; }
        public string? UpdatedBy { get; set; }


    }
}
