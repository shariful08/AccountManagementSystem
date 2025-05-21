namespace MiniAccountManagementSystem.ViewModels
{
    public class VoucherListItemViewModel
    {
        public int VoucherMasterId { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string VoucherType { get; set; }
        public string Narration { get; set; }
        public string Remarks { get; set; }
    }
}
