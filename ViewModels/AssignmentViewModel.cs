using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RegionSyd.Models;
using RegionSyd.Repositories;
using RegionSyd.Utility;

namespace RegionSyd.ViewModels
{
    public class AssignmentViewModel : INotifyPropertyChanged
    {
        private Models.Task _selectedTask;
        private TaskRepository taskRepository { get; set; }

        public List<Models.Task> Tasks { get; set; }
        ICommand MatchTaskCommand { get; set; }
        ICommand RemoveTaskCommand { get; set; }

        public Models.Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));

                    // Send a message to change the tab, only if SelectedTask is not null.
                    if (_selectedTask != null)
                    {
                        Messenger.Send("ChangeTab", 1); // Change to tab 2 when a task is selected.
                        Messenger.Send("SelectTask", SelectedTask);
                    }
                }
            }
        }

        public AssignmentViewModel()
        {
            taskRepository = new TaskRepository();
            Tasks = taskRepository.GetAllTasks();

            RemoveTaskCommand = new RelayCommand(RemoveTask, CanRemoveTask);
            MatchTaskCommand = new RelayCommand(MatchTask, CanMatchTask);
        }

        private void MatchTask()
        {
            throw new NotImplementedException();
        }

        private bool CanMatchTask()
        {
            throw new NotImplementedException();
        }

        private void RemoveTask()
        {
            throw new NotImplementedException();
        }
        private bool CanRemoveTask()
        {
            throw new NotImplementedException();
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
