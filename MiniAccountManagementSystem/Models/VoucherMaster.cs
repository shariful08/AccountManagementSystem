namespace MiniAccountManagementSystem.Models
{
    public class VoucherMaster
    {
        public int? VoucherMasterId { get; set; }
        public string? VoucherNo { get; set; }
        public string? VoucherType { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? Narration { get; set; }
        public string? Remarks { get; set; }
        public string? AddedBy { get; set; }
        public string? AddedPc { get; set; }

        public List<VoucherDetail>? VoucherDetails { get; set; } = new();
    }
}
