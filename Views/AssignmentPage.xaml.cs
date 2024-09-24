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
    /// Interaction logic for AssignmentPage.xaml
    /// </summary>
    public partial class AssignmentPage : Page
    {
        AssignmentViewModel mvm = new AssignmentViewModel();
        public AssignmentPage()
        {
            InitializeComponent();
            DataContext = mvm;
            mvm.LoadDataFromFile(@"C:\Users\ivark\Desktop\Datamatiker Online\Semester 2\Projects\RegionSydFromScratch\Assignments.csv");
        }
    }
}
