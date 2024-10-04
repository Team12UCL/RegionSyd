using RegionSyd.Models;

using RegionSyd.RepositoriesSQL;
using RegionSyd.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RegionSyd.ViewModels
{
    public class TripViewModel : ViewModelBase
    {
        private readonly TripRepositorySQL _tripRepository;
        private readonly TaskRepositorySQL _taskRepository;
        private readonly AmbulanceRepositorySQL _ambulanceRepository;

        private ObservableCollection<Trip> _trips;
        private ObservableCollection<Models.Task> _returnTasks;
        private Trip? _selectedTrip;
        private Models.Task? _selectedTask;
        private Models.Task? _selectedReturnTask;

        public ObservableCollection<Models.Task> ReturnTasks
        {
            get => _returnTasks;
            set
            {
                _returnTasks = value;
                OnPropertyChanged(nameof(ReturnTasks));
            }
        }
        public ObservableCollection<Models.Task> Tasks { get; set; }
        public ObservableCollection<Ambulance> Ambulances { get; set; }
        public ObservableCollection<Trip> Trips
        {
            get => _trips;
            set
            {
                _trips = value;
                OnPropertyChanged(nameof(Trips));
            }
        }

        public Trip? SelectedTrip
        {
            get => _selectedTrip;
            set
            {
                _selectedTrip = value;
                OnPropertyChanged(nameof(SelectedTrip));

                GetReturnTasks();
            }
        }

        public Models.Task? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public Models.Task? SelectedReturnTask
        {
            get => _selectedReturnTask;
            set
            {
                _selectedReturnTask = value;
                OnPropertyChanged(nameof(SelectedReturnTask));
            }
        }
        public Ambulance SelectedAmbulance { get; set; }

        public ICommand CreateTripCommand { get; set; }
        public ICommand RemoveTripCommand { get; set; }

        public TripViewModel()
        {
            _tripRepository = new TripRepositorySQL();
            _taskRepository = new TaskRepositorySQL();
            _ambulanceRepository = new AmbulanceRepositorySQL();

            // Initialize properties
            Tasks = new ObservableCollection<Models.Task>(_taskRepository.GetAllTasks());
            Trips = new ObservableCollection<Trip>(_tripRepository.LoadTrips());

            // Command to create a new trip
            CreateTripCommand = new RelayCommand(CreateReturnTrip, CanCreateReturnTrip);
            RemoveTripCommand = new RelayCommand<Trip>(RemoveTrip, CanRemoveTrip);

            Messenger.Register("UpdateTrips", (param) =>
            {
                Trips = new ObservableCollection<Trip>(_tripRepository.LoadTrips());
            });

        }



        // Get tasks for region by destination regionId and datetime
        public void GetReturnTasks()
        {
            if (SelectedTrip == null)
            {
                return;
            }
            int pickUpRegionId = SelectedTrip.DropOffRegionId;
            int dropOffRegionId = SelectedTrip.PickUpRegionId;
            SelectedTask = _taskRepository.GetTaskById(SelectedTrip.TaskId);
            DateTime dropOffTime = SelectedTask.DropOffTime;
            ReturnTasks = new ObservableCollection<Models.Task>(_taskRepository.GetReturnTasksByRegionIdAndDateTime(pickUpRegionId, dropOffRegionId, dropOffTime));
        }

        // Method to create a new trip
        private void CreateReturnTrip()
        {
            
            if (SelectedReturnTask != null && SelectedTrip != null)
            {
                SelectedAmbulance = _ambulanceRepository.GetAmbulanceById(SelectedTrip.AmbulanceId);
                if (SelectedAmbulance == null)
                {
                    MessageBox.Show("Ambulance not found");
                    return;
                }
                Trip newTrip = new Trip(SelectedReturnTask.TaskId,
                    SelectedAmbulance.AmbulanceId, SelectedReturnTask.PickUpRegionId, SelectedReturnTask.DropOffRegionId);


                _tripRepository.SaveTrip(newTrip);
                Trips = new ObservableCollection<Trip>(_tripRepository.LoadTrips());
                MessageBox.Show($"Trip with Id: {newTrip.TripId} created");
            }
        }

        // Check if a trip can be created (only if both task and ambulance are selected)
        private bool CanCreateReturnTrip()
        {
            return SelectedReturnTask != null && SelectedTrip != null;
        }

        private bool CanRemoveTrip(Trip trip)
        {
            return trip != null;
        }

        private void RemoveTrip(Trip trip)
        {
            if (trip != null && Trips.Contains(trip))
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to remove trip with Id: {trip.TripId}?", "Remove trip", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                _tripRepository.RemoveTrip(trip.TripId);
                _ambulanceRepository.UpdateAmbulanceStatus(trip.AmbulanceId, "Available");
                Trips = new ObservableCollection<Trip>(_tripRepository.LoadTrips());

                Messenger.Send("UpdateAmbulances");
            }
        }
    }
}
