using Microsoft.Data.SqlClient;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Repositories.Interfaces;
using MiniAccountManagementSystem.ViewModels;
using System.Data;
using System.Text.Json;

namespace MiniAccountManagementSystem.Repositories.Implementations
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public VoucherRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<bool> SaveVoucherAsync(VoucherEntryViewModel model)
        {
            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            try
            {
                using var cmd = new SqlCommand("sp_SaveVoucher_JSON", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Serialize details list to JSON
                var voucherDetailsJson = JsonSerializer.Serialize(model.VoucherDetails);

                cmd.Parameters.AddWithValue("@VoucherNo", model.VoucherMaster.VoucherNo);
                cmd.Parameters.AddWithValue("@VoucherType", model.VoucherMaster.VoucherType);
                cmd.Parameters.AddWithValue("@VoucherDate", model.VoucherMaster.VoucherDate);
                cmd.Parameters.AddWithValue("@Narration", model.VoucherMaster.Narration ?? "");
                cmd.Parameters.AddWithValue("@Remarks", model.VoucherMaster.Remarks ?? "");
                cmd.Parameters.AddWithValue("@AddedBy", model.VoucherMaster.AddedBy ?? "System");
                cmd.Parameters.AddWithValue("@AddedPc", model.VoucherMaster.AddedPc ?? Environment.MachineName);
                cmd.Parameters.AddWithValue("@VoucherDetailsJson", voucherDetailsJson);

                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<VoucherMaster>> GetAllVouchersAsync()
        {
            // Load from database
            var vouchers = new List<VoucherMaster>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM VoucherMaster", conn);
                conn.Open();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        vouchers.Add(new VoucherMaster
                        {
                            VoucherMasterId = reader.GetInt32(reader.GetOrdinal("VoucherMasterId")),
                            VoucherNo = reader["VoucherNo"].ToString(),
                            VoucherDate = reader.GetDateTime(reader.GetOrdinal("VoucherDate")),
                            VoucherType = reader["VoucherType"].ToString(),
                            Narration = reader["Narration"].ToString(),
                            Remarks = reader["Remarks"].ToString()
                        });
                    }
                }
            }

            return vouchers;
        }

    }
}
