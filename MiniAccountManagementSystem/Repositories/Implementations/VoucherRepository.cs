using Microsoft.Data.SqlClient;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Repositories.Interfaces;
using System.Data;

namespace MiniAccountManagementSystem.Repositories.Implementations
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly string _connectionString;

        public VoucherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task SaveVoucherAsync(List<VoucherModel> voucher)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_SaveVoucher", conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@VoucherType", voucher.VoucherType);
                //cmd.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                //cmd.Parameters.AddWithValue("@ReferenceNo", voucher.ReferenceNo);

                //var table = new DataTable();
                //table.Columns.Add("AccountID", typeof(int));
                //table.Columns.Add("DebitAmount", typeof(decimal));
                //table.Columns.Add("CreditAmount", typeof(decimal));
                //table.Columns.Add("Narration", typeof(string));

                ////foreach (var line in voucher.VoucherDetails)
                ////{
                ////    table.Rows.Add(line.AccountID, line.DebitAmount, line.CreditAmount, line.Narration);
                ////}

                //var tvp = cmd.Parameters.AddWithValue("@VoucherDetails", table);
                //tvp.SqlDbType = SqlDbType.Structured;
                //tvp.TypeName = "VoucherDetailType";

                //await conn.OpenAsync();
                //await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
