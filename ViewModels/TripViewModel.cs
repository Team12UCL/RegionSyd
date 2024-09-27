using RegionSyd.Models;
using RegionSyd.Repositories;
using RegionSyd.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RegionSyd.ViewModels
{
    public class TripViewModel : ViewModelBase
    {
        private readonly TripRepository _tripRepository;
        private readonly TaskRepository _taskRepository;
        private readonly AmbulanceRepository _ambulanceRepository;

        private Models.Task? _selectedTask;

        public ObservableCollection<Models.Task> Tasks { get; set; }
        public ObservableCollection<Ambulance> Ambulances { get; set; }
        public Models.Task? SelectedTask 
        {
            get => _selectedTask; 
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        public Ambulance SelectedAmbulance { get; set; }

        public ICommand CreateTripCommand { get; set; }

        public TripViewModel()
        {
            _tripRepository = new TripRepository();
            _taskRepository = new TaskRepository();
            _ambulanceRepository = new AmbulanceRepository();

            // Load data from repositories
            Tasks = new ObservableCollection<Models.Task>(_taskRepository.GetAllTasks());
            Ambulances = new ObservableCollection<Ambulance>(_ambulanceRepository.GetAllAmbulances());

            // Command to create a new trip
            CreateTripCommand = new RelayCommand(CreateTrip, CanCreateTrip);

            SelectedTask = null;

            Messenger.Register("SelectTask", (param) =>
            {

                if (param is Models.Task task && task != SelectedTask)
                {
                    SelectedTask = task;
                }
            });
        }

        // Method to create a new trip
        private void CreateTrip()
        {
            if (SelectedTask != null && SelectedAmbulance != null)
            {
                Trip newTrip = new Trip(SelectedTask.TaskId,
                    SelectedAmbulance.AmbulanceId);
                

                _tripRepository.SaveTrip(newTrip);
            }
        }

        // Check if a trip can be created (only if both task and ambulance are selected)
        private bool CanCreateTrip()
        {
            return SelectedTask != null && SelectedAmbulance != null;
        }
    }
}
