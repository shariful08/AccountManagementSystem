using MiniAccountManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class AccountRepository : IAccountRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public AccountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<AccountModel>> GetAllAccountsAsync()
    {
        var accounts = new List<AccountModel>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GET");

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows) 
                    {
                        while (await reader.ReadAsync())
                        {
                            Console.WriteLine(reader["AccountName"]);
                            var account = new AccountModel
                            {
                                AccountId = Convert.ToInt32(reader["AccountId"]),
                                AccountCode = reader["AccountCode"].ToString(),
                                AccountName = reader["AccountName"].ToString(),
                                ParentAccountId = reader["ParentAccountId"] as int?,
                                AccountType = reader["AccountType"].ToString(),
                                Remarks = reader["Remarks"].ToString()
                            };
                            accounts.Add(account);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }
            }
        }
        return accounts;
    }

    public async Task AddAccountAsync(AccountModel model)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@QueryType", "CREATE");

            cmd.Parameters.AddWithValue("@AccountName", model.AccountName);
            cmd.Parameters.AddWithValue("@AccountCode", model.AccountCode);
            cmd.Parameters.AddWithValue("@ParentAccountId", (object)model.ParentAccountId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AccountType", model.AccountType);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Revision", model.Revision);
            cmd.Parameters.AddWithValue("@AddedDate", model.AddedDate);
            cmd.Parameters.AddWithValue("@AddedPc", model.AddedPc);
            cmd.Parameters.AddWithValue("@AddedBy", model.AddedBy);
            cmd.Parameters.AddWithValue("@UpdatedDate", (object)model.UpdatedDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedPc", (object)model.UpdatedPc ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object)model.UpdatedBy ?? DBNull.Value);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public async Task UpdateAccountAsync(AccountModel model)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@QueryType", "UPDATE");

            cmd.Parameters.AddWithValue("@AccountId", model.AccountId);
            cmd.Parameters.AddWithValue("@AccountName", model.AccountName);
            cmd.Parameters.AddWithValue("@AccountCode", model.AccountCode);
            cmd.Parameters.AddWithValue("@ParentAccountId", (object)model.ParentAccountId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AccountType", model.AccountType);
            cmd.Parameters.AddWithValue("@Remarks", (object)model.Remarks ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Revision", model.Revision);
            cmd.Parameters.AddWithValue("@AddedDate", (object)model.AddedDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AddedPc", (object)model.AddedPc ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AddedBy", (object)model.AddedBy ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedDate", (object)model.UpdatedDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedPc", (object)model.UpdatedPc ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object)model.UpdatedBy ?? DBNull.Value);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteAccountAsync(AccountModel model)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@QueryType", "DELETE");

            cmd.Parameters.AddWithValue("@AccountId", model.AccountId);

            cmd.Parameters.AddWithValue("@UpdatedDate", (object)model.UpdatedDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedPc", (object)model.UpdatedPc ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object)model.UpdatedBy ?? DBNull.Value);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
