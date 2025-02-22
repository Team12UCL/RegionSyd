﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private DataManager dataManager { get; set; }
        public ICommand MatchTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; }
        private ObservableCollection<Models.Task> _tasks;
        private ObservableCollection<Models.Region> _regions;
        private Models.Region _selectedRegion;

        public ObservableCollection<Models.Region> Regions
        {
            get { return _regions; }
            set
            {
                _regions = value;
                OnPropertyChanged(nameof(Regions));
            }
        }

        public ObservableCollection<Models.Task> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

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

        public Models.Region SelectedRegion
        {
            get => _selectedRegion;
            set
            {
                if (_selectedRegion != value)
                {
                    _selectedRegion = value;
                    OnPropertyChanged(nameof(SelectedRegion));
                    SetTasksOnRegionSelect();

                    // Send a message to change the tab, only if SelectedTask is not null.
                    //if (_selectedTask != null)
                    //{
                    //    Messenger.Send("ChangeTab", 1); // Change to tab 2 when a task is selected.
                    //    Messenger.Send("SelectTask", SelectedTask);
                    //}
                }
            }
        }

        

        public AssignmentViewModel()
        {
            taskRepository = new TaskRepository();
            Tasks = new ObservableCollection<Models.Task>(taskRepository.GetAllTasks());
            dataManager = new DataManager();
            Regions = new ObservableCollection<Region>(dataManager.GetAllRegions());

            RemoveTaskCommand = new RelayCommand(RemoveTask, CanRemoveTask);
            //MatchTaskCommand = new RelayCommand(MatchTask, CanMatchTask);
        }

        //private void MatchTask()
        //{
        //    throw new NotImplementedException();
        //}

        //private bool CanMatchTask()
        //{
        //    throw new NotImplementedException();
        //}

        private void RemoveTask()
        {
            taskRepository.RemoveTask(SelectedTask.TaskId);
            Tasks.Remove(SelectedTask);
        }

        private bool CanRemoveTask()
        {
            return SelectedTask != null;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SetTasksOnRegionSelect()
        {
            Tasks = new ObservableCollection<Models.Task>(taskRepository.GetTasksByRegionId(SelectedRegion.Id, true));
        }
    }
}
