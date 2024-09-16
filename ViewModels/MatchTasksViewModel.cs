using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using RegionSyd.Models;

namespace RegionSyd.ViewModels
{
    public class MatchTasksViewModel : INotifyPropertyChanged
    {
        private Task _selectedTask;
        private Task _selectedMatch;
        private ObservableCollection<Task> _potentialMatches;

        // Property for the selected task, with change notification
        public Task SelectedTask
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
        public ObservableCollection<Task> PotentialMatches
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
    }
}
