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
            LoadDataFromFile(@"C:\Users\ivark\source\repos\Team12UCL\RegionSyd\opgaver.csv");

            // Sætter indholdet (pagen AutomaticMatchesPage) for de automatiske foreslag (Grid Row 3)
            AutomaticSuggestions.NavigationService.Navigate(new AutomaticMatchesPage());
        }

        private void DataDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        // Method to load data from the CSV file
        private void LoadDataFromFile(string filePath)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);

                // Read all lines from the CSV file
                var lines = File.ReadAllLines(filePath);

                // Assuming the file has headers, start from the second line
                if (lines.Length > 1)
                {
                    // Populate Row 1 (second line in the file)
                    var row1Fields = lines[1].Split(',');
                    PopulateTextBlock(OpgaveId1, OpgaveFra1, OpgaveTil1, OpgaveTid1, row1Fields);

                    // If there's more data, populate Row 2 (third line in the file)
                    if (lines.Length > 2)
                    {
                        var row2Fields = lines[2].Split(',');
                        PopulateTextBlock(OpgaveId2, OpgaveFra2, OpgaveTil2, OpgaveTid2, row2Fields);
                    }
                    // If there's more data, populate Row 3 (third line in the file)
                    if (lines.Length > 3)
                    {
                        var row3Fields = lines[3].Split(',');
                        PopulateTextBlock(OpgaveId3, OpgaveFra3, OpgaveTil3, OpgaveTid3, row3Fields);
                    }
                    // If there's more data, populate Row 4 (third line in the file)
                    if (lines.Length > 4)
                    {
                        var row4Fields = lines[4].Split(',');
                        PopulateTextBlock(OpgaveId4, OpgaveFra4, OpgaveTil4, OpgaveTid4, row4Fields);
                    }
                    // If there's more data, populate Row 5 (third line in the file)
                    if (lines.Length > 5)
                    {
                        var row5Fields = lines[5].Split(',');
                        PopulateTextBlock(OpgaveId5, OpgaveFra5, OpgaveTil5, OpgaveTid5, row5Fields);
                    }
                    // If there's more data, populate Row 6 (third line in the file)
                    if (lines.Length > 6)
                    {
                        var row6Fields = lines[6].Split(',');
                        PopulateTextBlock(OpgaveId6, OpgaveFra6, OpgaveTil6, OpgaveTid6, row6Fields);
                    }
                    // If there's more data, populate Row 7 (third line in the file)
                    if (lines.Length > 7)
                    {
                        var row7Fields = lines[7].Split(',');
                        PopulateTextBlock(OpgaveId7, OpgaveFra7, OpgaveTil7, OpgaveTid7, row7Fields);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}");
            }
        }
        private void PopulateTextBlock(TextBlock id, TextBlock fra, TextBlock til, TextBlock tid, string[] fields)
        {
            if (fields.Length >= 4)
            {
                id.Text = fields[0];   // Id
                fra.Text = fields[1];  // Fra
                til.Text = fields[2];  // Til
                tid.Text = fields[3];  // Tid
            }
        }
    }
}