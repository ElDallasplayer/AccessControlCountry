
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace PrincipalObjects.Objects
{
    public class User
    {
        #region Properties
        public int userId {  get; set; }
        public string userName {  get; set; }
        public byte[] userPassword {  get; set; }
        public string _passwordAsString { get; set; }
        public int userRol {  get; set; }
        public int userPermissions {  get; set; }
        #endregion

        public User() { }

        public async Task<List<User>> GetUsersFromDatabase()
        {
            try
            {
                var query = "SELECT userId, userName, userPassword, userRol, userPermissions FROM Users";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.OpenAsync();
                    var users = await connection.QueryAsync<User>(query);
                    return users.ToList();
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<User> ValidateUser(string user,string password)
        {
            try
            {
                byte[] encryptedPassword = Security.Encrypt(password);

                var query = $"SELECT userId, userName, userPassword, userRol, userPermissions FROM Users WHERE userName like '{user}' and userPassword = {encryptedPassword}";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var userToReturn = await connection.QueryFirstAsync<User>(query);
                    return userToReturn;
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
                        UserPassword = userToInsert.userPassword,
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
