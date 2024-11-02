using PrincipalObjects;
using PrincipalObjects.Objects;

namespace TestingMethods
{
    [TestClass]
    public class Test_employee_class
    {
        public static string _databaseConnection = "Server=DESKTOP-3J6KST2\\SQLEXPRESS16;Database=CountryAPP;User Id=eldallas;Password=Delunoalocho;";

        [TestMethod]
        public async Task Test_getEmployees()
        {
            try
            {
                await DatabaseConnection.SetConnectionString(_databaseConnection);

                List<Employee> employees = await new Employee().GetEmployees();
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void Test_updateEmployees()
        {

        }

        [TestMethod]
        public void Test_insertEmployees()
        {

        }

        [TestMethod]
        public void Test_deleteEmployees()
        {

        }
    }
}