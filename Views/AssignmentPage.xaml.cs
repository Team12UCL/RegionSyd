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
using RegionSyd.ViewModels;
using System.IO;
using RegionSyd.Utility;


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
            string filePath = @"../../../Data/opgaver.csv";
            //mvm.LoadDataFromFile(filePath);
        }        
    }
}
