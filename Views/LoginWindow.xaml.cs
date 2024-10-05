using RegionSyd.RepositoriesSQL;
using System.Windows;

namespace RegionSyd.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DataManagerSQL dataManager;
        private List<Models.Dispatcher> Dispatchers { get; set; }

        public LoginWindow()
        {
            InitializeComponent();

            dataManager = new DataManagerSQL();
            Dispatchers = dataManager.GetAllDispatchers();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            Models.Dispatcher loggedInDispatcher = AuthenticateUser(username, password);

            if (loggedInDispatcher != null)
            {
                // Instantier og vis Main Window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Luk Login Window
                this.Close();
            }
        }

        public Models.Dispatcher AuthenticateUser(string username, string password)
        {
            // Tjek om brugernavn og kodeord matcher en dispatchers
            Models.Dispatcher dispatcher = Dispatchers.FirstOrDefault(d => d.UserName == username && d.Password == password);

            if (dispatcher != null)
            {
                MessageBox.Show($"Velkommen, {dispatcher.FirstName}!");
                return dispatcher;
            }
            else
            {
                MessageBox.Show("Ugyldigt brugernavn eller kodeord.");
                return null;
            }
        }
    }
}