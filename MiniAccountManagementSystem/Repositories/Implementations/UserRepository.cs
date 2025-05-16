using MiniAccountManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MiniAccountManagementSystem.Repositories.Interfaces;

namespace MiniAccountManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", username);

            var count = (int)await cmd.ExecuteScalarAsync();
            return count > 0;
        }

        public async Task CreateUserAsync(UserModel user)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            SqlTransaction tx = conn.BeginTransaction();

            try
            {
                var insertUserCmd = new SqlCommand(
                    "INSERT INTO Users (Username, PasswordHash, FullName, IsActive) OUTPUT INSERTED.Id VALUES (@Username, @PasswordHash, @FullName, 1)", conn, tx);
                insertUserCmd.Parameters.AddWithValue("@Username", user.Username);
                insertUserCmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                insertUserCmd.Parameters.AddWithValue("@FullName", (object?)user.FullName ?? DBNull.Value);

                var userId = (int)await insertUserCmd.ExecuteScalarAsync();

                if (user.SelectedRoleId.HasValue)
                {
                    var insertRoleCmd = new SqlCommand(
                        "INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)", conn, tx);
                    insertRoleCmd.Parameters.AddWithValue("@UserId", userId);
                    insertRoleCmd.Parameters.AddWithValue("@RoleId", user.SelectedRoleId.Value);
                    await insertRoleCmd.ExecuteNonQueryAsync();
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public async Task<UserModel?> GetUserByUsernameAsync(string username)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT Id, Username, PasswordHash, FullName FROM Users WHERE Username = @Username AND IsActive = 1", conn);
            cmd.Parameters.AddWithValue("@Username", username);
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows) return null;
            await reader.ReadAsync();

            var user = new UserModel
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                PasswordHash = reader.GetString(2),
                FullName = reader.IsDBNull(3) ? "" : reader.GetString(3)
            };

            reader.Close();

            // Load roles
            var roleCmd = new SqlCommand("SELECT r.RoleName FROM UserRoles ur JOIN Roles r ON ur.RoleId = r.Id WHERE ur.UserId = @UserId", conn);
            roleCmd.Parameters.AddWithValue("@UserId", user.Id);
            using var roleReader = await roleCmd.ExecuteReaderAsync();

            while (await roleReader.ReadAsync())
            {
                user.Roles.Add(roleReader.GetString(0));
            }

            return user;
        }

        public async Task<List<UserWithRolesModel>> GetAllUsersWithRolesAsync()
        {
            var users = new List<UserWithRolesModel>();

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand(@"
        SELECT 
            u.Id, u.Username, u.FullName, r.RoleName
        FROM Users u
        LEFT JOIN UserRoles ur ON u.Id = ur.UserId
        LEFT JOIN Roles r ON ur.RoleId = r.Id
        ORDER BY u.Id", conn);

            var reader = await cmd.ExecuteReaderAsync();

            var userDict = new Dictionary<int, UserWithRolesModel>();

            while (await reader.ReadAsync())
            {
                var userId = reader.GetInt32(0);

                if (!userDict.ContainsKey(userId))
                {
                    userDict[userId] = new UserWithRolesModel
                    {
                        UserId = userId,
                        Username = reader.GetString(1),
                        FullName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Roles = new List<string>()
                    };
                }

                if (!reader.IsDBNull(3))
                {
                    userDict[userId].Roles.Add(reader.GetString(3));
                }
            }

            users = userDict.Values.ToList();
            return users;
        }

        public class RoleModel
        {
            public int Id { get; set; }
            public string RoleName { get; set; }
        }

        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            var roles = new List<RoleModel>();
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT Id, RoleName FROM Roles", conn);
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                roles.Add(new RoleModel
                {
                    Id = reader.GetInt32(0),
                    RoleName = reader.GetString(1)
                });
            }

            return roles;
        }

    }
}
