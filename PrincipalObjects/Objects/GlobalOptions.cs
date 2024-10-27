#region Dependencies
#endregion

using Dapper;
using System;
using System.Threading.Tasks;

namespace PrincipalObjects.Objects
{
    public class GlobalOptions
    {
        public static GlobalOptions Configurations;
        #region Properties
        public string goAppName { get; set; }
        public string goFirstOption { get; set; }
        public string goSecondOption { get; set; }
        public string goThirdOption { get; set; }
        public string goFourthOption { get; set; }
        public string goCompanyName { get; set; }
        #endregion

        public GlobalOptions() { }

        public async Task<GlobalOptions> GetGlobalOptions()
        {
            try
            {
                var query = "SELECT goAppName,goFirstOption,goSecondOption,goThirdOption,goFourthOption,goCompanyName FROM GlobalOptions";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    await connection.OpenAsync();
                    var GlobalsOptions = await connection.QuerySingleAsync<GlobalOptions>(query);
                    Configurations = GlobalsOptions;
                    return GlobalsOptions;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
