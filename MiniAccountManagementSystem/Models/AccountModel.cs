namespace MiniAccountManagementSystem.Models
{
    public class AccountModel
    {

        public int AccountId { get; set; }
        public string? AccountName { get; set; }
        public string? AccountCode { get; set; }
        public int? ParentAccountId { get; set; }
        public string? AccountType { get; set; }
        public string? Remarks { get; set; }
        public int Revision { get; set; }
        public int IsActive { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedPc { get; set; }
        public string AddedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedPc { get; set; }
        public string? UpdatedBy { get; set; }
    }

}
