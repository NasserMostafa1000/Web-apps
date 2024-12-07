using Microsoft.Extensions.Configuration;

namespace SalamaTravelDTA
{
    public class Settings
    {
       // public static string ConnectionString { get; private set; }

        //public static void LoadConfiguration(IConfiguration configuration)
        //{
        //    ConnectionString = configuration.GetConnectionString("DefaultConnection");
        //}

          public static string ConnectionString { get; set; } = "Server=localhost; Database=SalamaTravelDB; User ID=SA; Password=Naser0120#; TrustServerCertificate=True;";
    }
}
