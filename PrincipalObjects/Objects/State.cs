#region Dependencies
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace PrincipalObjects.Objects
{
    public class State
    {
        #region Properties
        public int stateId { get; set; }
        public string stateDescription { get; set; }
        public bool stateActive { get; set; }
        public bool stateEnabledToPass { get; set; }
        public DateTime stateInitialDate { get; set; }
        public DateTime stateFinalDate { get; set; }
        #endregion

        public State() { }

        public async Task<List<State>> GetStates()
        {
            try
            {
                string query = "SELECT stateId, stateDescription, stateActive,stateEnabledToPass,stateInitialDate,stateFinalDate FROM States";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<State>(query);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
