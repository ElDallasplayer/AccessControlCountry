#region Dependencies
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace PrincipalObjects.Objects
{
    public class Employee : Entity
    {
        #region Properties
        //public string empId {  get; set; } => CHANGE THIS TO GUID
        public string empName {  get; set; }
        public string empSurname {  get; set; }
        public string empDocument {  get; set; }
        public string empPhone {  get; set; }
        public string empDetails {  get; set; }
        public int empAdress {  get; set; }
        public int empState {  get; set; }
        public bool empEnabledAccess {  get; set; }
        public bool empUseVehicle {  get; set; }
        public DateTime empBirthDay {  get; set; }
        public DateTime empStartDate {  get; set; }
        #endregion

        public Employee() { }

        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                var query = "SELECT empId, empName, empSurname, empDocument, empPhone, empDetails, empAddress, empState, empEnabledAccess, empUseVehicle, empBirthDay, empStartDate FROM Employees";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var employees = await connection.QueryAsync<Employee>(query);
                    return employees.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                var query = $"SELECT empId, empName, empSurname, empDocument, empPhone, empDetails, empAddress, empState, empEnabledAccess, empUseVehicle, empBirthDay, empStartDate FROM Employees WHERE empId = {id}";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var employees = await connection.QuerySingleAsync<Employee>(query);
                    return employees;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
