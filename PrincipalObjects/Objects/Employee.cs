#region Dependencies
using Dapper;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace PrincipalObjects.Objects
{
    public class Employee : Entity
    {
        #region EmployeeAge
        public class EmployeeAge
        {
            public int Age { get; private set; }

            public EmployeeAge(int age)
            {
                if (age < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(age), "The age must be a positive number.");
                }
                else if (age > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(age), "The age cannot exceed 100.");
                }

                Age = age;
            }
        }
        #endregion

        #region Properties
        public string empName { get; set; }
        public string empSurname { get; set; }
        public string empDocument { get; set; }
        public string empPhone { get; set; }
        public string empDetails { get; set; }
        public int empAdress { get; set; }
        public int empState { get; set; }
        public bool empEnabledAccess { get; set; }
        public bool empUseVehicle { get; set; }
        public DateTime empBirthDay { get; set; }
        public DateTime empStartDate { get; set; }

        private EmployeeAge _age;

        public EmployeeAge Age
        {
            get => _age;
            private set => _age = value;
        }

        public int empAge
        {
            get => Age?.Age ?? 0;
            set => Age = new EmployeeAge(value);
        }

        #endregion

        public Employee() { }

        public void SetAge(int age)
        {
            Age = new EmployeeAge(age);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                var query = "SELECT id, empName, empSurname, empDocument, empPhone, empDetails, empAddress, empState, empEnabledAccess, empUseVehicle, empBirthDay, empStartDate, empAge FROM Employees";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var employees = await connection.QueryAsync<Employee, int, Employee>(
                        query,
                        (employee, age) =>
                        {
                            employee.SetAge(age);
                            return employee;
                        },
                        splitOn: "empAge"
                    );

                    return employees.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Employee> GetEmployeeById(Guid id)
        {
            try
            {
                var query = "SELECT id, empName, empSurname, empDocument, empPhone, empDetails, empAddress, empState, empEnabledAccess, empUseVehicle, empBirthDay, empStartDate, empAge FROM Employees WHERE Id = @Id";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var employees = await connection.QueryAsync<Employee, int, Employee>(
                        query,
                        (emp, age) =>
                        {
                            emp.SetAge(age);
                            return emp;
                        },
                        new { Id = id },
                        splitOn: "empAge"
                    );

                    return employees.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}