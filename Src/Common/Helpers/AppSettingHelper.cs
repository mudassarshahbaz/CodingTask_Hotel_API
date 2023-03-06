using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Numerics;

namespace Common.Helpers
{
    public static class AppSettingHelper
    {

        private static string GetSettingValue(string parentKey, string childKey)
        {
            try
            {
                IConfigurationRoot configuration = GetSettingConfiguration();

                if (!configuration.GetSection(parentKey).Exists())
                {
                    throw new Exception();
                }

                if (!configuration.GetSection(parentKey).GetSection(childKey).Exists())
                {
                    throw new Exception();
                }

                return configuration.GetSection(parentKey).GetSection(childKey).Value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private static IConfigurationRoot GetSettingConfiguration()
        {
            try
            {
                return new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
                            .Build();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
       
        public static string GetDefaultConnection()
        {
            return GetSettingValue("ConnectionStrings", "DefaultConnection");
        }


        
      
    }
}