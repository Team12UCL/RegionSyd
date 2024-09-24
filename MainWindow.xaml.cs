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

            // Sætter indholdet (pagen AutomaticMatchesPage) for de automatiske foreslag (Grid Row 3)
            AutomaticSuggestions.NavigationService.Navigate(new AutomaticMatchesPage());
            AssignmentPanel.NavigationService.Navigate(new AssignmentPage());
        }

        private void DataDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void PopulateTextBlock(TextBlock id, TextBlock fra, TextBlock til, TextBlock tid, string[] fields)
        //{
        //    if (fields.Length >= 4)
        //    {
        //        id.Text = fields[0];   // Id
        //        fra.Text = fields[1];  // Fra
        //        til.Text = fields[2];  // Til
        //        tid.Text = fields[3];  // Tid
        //    }
        //}
    }
}