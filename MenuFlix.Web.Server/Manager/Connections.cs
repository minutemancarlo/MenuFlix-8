

namespace MenuFlix.Web.Server.Manager
{
    public  class Connections
    {
       

        public int CommandTimeoutParam
        {
            get
            {
                _ = int.TryParse(ConfigurationManager.AppSetting["AppSettings:CommandTimeoutParam"], out int result);
                return result == 0 ? 3000 : result;
            }

        }

        /// <summary>
        /// EIS Connection
        /// Should have entry in appsettings.json like below:
        /// <code>
        ///     "AppSettings": 
        ///     {
        ///         "SQLServerName": "<server_name>",
        ///         "SQLDatabaseName": "<database_name>",
        ///         "SQLUsername": "<database_userid>",
        ///         "SQLPassword": "<database_password>"
        ///     }
        /// </code> 
        /// </summary>
        /// <returns>SQL Connection string</returns>
        public string DefaultConnectionString
        {
            get
            {
                string sqlServerName = ConfigurationManager.AppSetting["AppSettings:SQLServerName"];
                string sqlDatabaseName = ConfigurationManager.AppSetting["AppSettings:SQLDatabaseName"];
                string sqlUsername = ConfigurationManager.AppSetting["AppSettings:SQLUsername"];
                string sqlPassword = ConfigurationManager.AppSetting["AppSettings:SQLPassword"];
                var sqlConnectionString = $"Server={sqlServerName}; Database={sqlDatabaseName}; User ID={sqlUsername}; PWD={sqlPassword};Trusted_Connection=False;Connection Timeout=30";
                return sqlConnectionString;
            }
        }
    }
}