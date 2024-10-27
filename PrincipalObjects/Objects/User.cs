#region Dependencies
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace PrincipalObjects.Objects
{
    public class User
    {
        #region Properties
        public int userId { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; } //BASE 64
        public int userRol { get; set; }
        public int userPermissions { get; set; }

        public string passwordAsString { get; set; }
        #endregion

        public User() { }

        public async Task<List<User>> GetUsersFromDatabase()
        {
            try
            {
                var query = "SELECT userId, userName, userPassword, userRol, userPermissions FROM Users";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var users = await connection.QueryAsync<User>(query);
                    return users.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> ValidateUser(string user, string password)
        {
            try
            {
                var query = "SELECT userId, userName, userPassword, userRol, userPermissions FROM Users WHERE LOWER(userName) = LOWER(@UserName)";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();

                    var userRecord = await connection.QueryFirstOrDefaultAsync<User>(query, new
                    {
                        UserName = user
                    });

                    if (userRecord != null)
                    {
                        string decryptedPassword = Security.DecryptFromBase64(userRecord.userPassword);

                        if (decryptedPassword == password)
                        {
                            return userRecord;
                        }
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> InsertUserInDatabase(User userToInsert)
        {
            try
            {
                var query =
                    "INSERT INTO Users (userName, userPassword, userRol, userPermissions)" +
                    " VALUES (@UserName, @UserPassword, @UserRol, @UserPermissions); SELECT CAST(SCOPE_IDENTITY() as int)";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var userId = await connection.QuerySingleAsync<int>(query, new
                    {
                        UserName = userToInsert.userName,
                        UserPassword = Security.EncryptToBase64(userToInsert.passwordAsString), // Guardar en Base64
                        UserRol = userToInsert.userRol,
                        UserPermissions = userToInsert.userPermissions
                    });

                    userToInsert.userId = userId;

                    return userToInsert;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
