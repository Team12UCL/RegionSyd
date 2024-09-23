using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using RegionSyd.Models;

namespace RegionSyd.ViewModels
{
    public class MatchTasksViewModel : INotifyPropertyChanged
    {
        private Models.Task _selectedTask;
        private Models.Task _selectedMatch;
        private ObservableCollection<Models.Task> _potentialMatches;

        // Property for the selected task, with change notification
        public Models.Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                }
            }
        }

        // Collection of potential matches
        public ObservableCollection<Models.Task> PotentialMatches
        {
            get => _potentialMatches;
            set
            {
                if (_potentialMatches != value)
                {
                    _potentialMatches = value;
                    OnPropertyChanged(nameof(PotentialMatches));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
