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
    public class Family
    {
        #region Properties
        public int famId { get; set; }
        public int famEmpId { get; set; }
        public int famState { get; set; }
        public string famName { get; set; }
        public string famSurname { get; set; }
        public bool farEnabledPortal { get; set; }
        public string famUser { get; set; }
        public string famPassword { get; set; }
        #endregion

        public Family() { }

        public async Task<List<Family>> GetFamilyByEmpId(int id)
        {
            string query = $"SELECT * FROM Families WHERE famEmpId = {id}";

            using (var connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    IEnumerable<Family> family = await connection.QueryAsync<Family>(query);
                    return family.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
