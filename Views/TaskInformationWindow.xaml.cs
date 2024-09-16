using RegionSyd.ViewModels;
using System.Windows;

namespace RegionSyd.Views
{
    public partial class TaskInformationWindow : Window
    {
        public TaskInformationWindow(string taskId, string destinationLocation, string originLocation, string status, DateTime pickupTime, TimeSpan estimatedDriveTime, double distance)
        {
            InitializeComponent();

            // Create ViewModel and bind it to the window's DataContext
            var viewModel = new TaskInformationViewModel(
                taskId,
                destinationLocation,
                originLocation,
                status,
                pickupTime,
                estimatedDriveTime,
                distance);

            this.DataContext = viewModel;
        }

        // Close the window, Make into command.
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
