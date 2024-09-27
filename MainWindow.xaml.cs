using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegionSyd.Repositories;
using RegionSyd.ViewModels;
using RegionSyd.Views;


namespace RegionSyd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = new MainViewModel();

            // Sætter indholdet (pagen AutomaticMatchesPage) for de automatiske foreslag (Grid Row 3)
            AutomaticSuggestions.NavigationService.Navigate(new AutomaticMatchesPage());
            AssignmentPanel.NavigationService.Navigate(new AssignmentPage());
            TaskInformationTab.NavigationService.Navigate(new TaskInformationPage());
            TripTab.NavigationService.Navigate(new TripPage());
        }
    }
}