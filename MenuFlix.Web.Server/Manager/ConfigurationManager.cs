
namespace MenuFlix.Web.Server.Manager
{
    public static class ConfigurationManager
    {
        /// <summary>
        /// appsettings.json should be present in project
        /// </summary>
        public static IConfiguration AppSetting
        {
            get
            {
                var config = new ConfigurationBuilder();

                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (string.IsNullOrWhiteSpace(environment))//Production
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true);
                }
                else
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile($"appsettings.{environment}.json", false, true);
                }

                return config.Build();
            }
        }


    }
}
