using RegionSyd.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RegionSyd.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    OnPropertyChanged(nameof(SelectedTabIndex));
                    
                }
            }
        }

        public MainViewModel()
        {
            SelectedTabIndex = 0;
            // Register to listen for "ChangeTab" messages
            Messenger.Register("ChangeTab", (param) =>
            {
               
                if (param is int tabIndex && tabIndex != SelectedTabIndex)
                {
                    SelectedTabIndex = tabIndex;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
