using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegionSyd.ViewModels;

namespace RegionSyd
{
    /// <summary>
    /// Interaction logic for MatchTasks.xaml
    /// </summary>
    public partial class MatchTasks : Page
    {
        public MatchTasks()
        {
            InitializeComponent();
            this.DataContext = new MatchTasksViewModel();
        }
    }
}
