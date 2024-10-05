using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RegionSyd
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfigurationRoot Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // App-klasen (nuværende klasse) nedarver fra Application-klassen
            // Vi kalder her på Applicacation-klassens OnStartup metode (via 'base' keyword), så normal opstart også sker, inden vores egen OnStartUp metode her
            base.OnStartup(e);

            // Byg konfigurationen, som vi bruger i repositories til at hente connection string
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
