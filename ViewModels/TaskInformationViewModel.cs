using RegionSyd.Models;
using RegionSyd.RepositoriesSQL;
using RegionSyd.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RegionSyd.ViewModels
{
    public class TaskInformationViewModel : ViewModelBase
    {
        private readonly AmbulanceRepositorySQL _ambulanceRepository;
        private readonly TripRepositorySQL _tripRepository;

        private Models.Task? _selectedTask;
        private ObservableCollection<Ambulance> _ambulances;
        private Ambulance _selectedAmbulance;

        public Models.Task? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                
            }
        }

        public ObservableCollection<Models.Task> Tasks { get; set; }

        public ObservableCollection<Ambulance> Ambulances
        {
            get => _ambulances;
            set
            {
                _ambulances = value;
                OnPropertyChanged(nameof(Ambulances));
            }
        }

        public Ambulance SelectedAmbulance
        {
            get => _selectedAmbulance;
            set
            {
                _selectedAmbulance = value;
                OnPropertyChanged(nameof(SelectedAmbulance));
               
            }
        }

        public ICommand CreateTripCommand { get; set; }

        public TaskInformationViewModel()
        {
            _tripRepository = new TripRepositorySQL();
            _ambulanceRepository = new AmbulanceRepositorySQL();

            CreateTripCommand = new RelayCommand(CreateTrip, CanCreateTrip);

            Ambulances = new ObservableCollection<Ambulance>(_ambulanceRepository.GetAvailableAmbulances());

            Messenger.Register("SelectTask", (param) =>
            {
                if (param is Models.Task task && task != SelectedTask)
                {
                    SelectedTask = task;
                }
            });

            Messenger.Register("UpdateAmbulances", (param) =>
            {
                Ambulances = new ObservableCollection<Ambulance>(_ambulanceRepository.GetAvailableAmbulances());
            });
        }

        private void CreateTrip()
        {
            if (SelectedTask != null && SelectedAmbulance != null)
            {
                Trip newTrip = new Trip( SelectedAmbulance.AmbulanceId, SelectedTask.TaskId,
                                        SelectedTask.PickUpRegionId, SelectedTask.DropOffRegionId);

                _ambulanceRepository.UpdateAmbulanceStatus(SelectedAmbulance.AmbulanceId, "On call");
                _tripRepository.SaveTrip(newTrip);
                Ambulances = new ObservableCollection<Ambulance>(_ambulanceRepository.GetAvailableAmbulances());

                Messenger.Send("UpdateTrips");
            }
        }

        private bool CanCreateTrip()
        {
            return SelectedTask != null && SelectedAmbulance != null;
        }
    }

}


