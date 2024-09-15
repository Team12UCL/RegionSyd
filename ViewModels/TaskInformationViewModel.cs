using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModels
{
    public class TaskInformationViewModel : INotifyPropertyChanged
    {
        private string _taskId;
        private string _destinationLocation;
        private string _originLocation;
        private string _status;
        private DateTime _pickupTime;
        private TimeSpan _estimatedDriveTime;
        private double _distance;

        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to notify property changes
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // TaskId property
        public string TaskId
        {
            get => _taskId;
            set
            {
                if (_taskId != value)
                {
                    _taskId = value;
                    OnPropertyChanged(nameof(TaskId));
                }
            }
        }

        // DestinationLocation property
        public string DestinationLocation
        {
            get => _destinationLocation;
            set
            {
                if (_destinationLocation != value)
                {
                    _destinationLocation = value;
                    OnPropertyChanged(nameof(DestinationLocation));
                }
            }
        }

        // OriginLocation property
        public string OriginLocation
        {
            get => _originLocation;
            set
            {
                if (_originLocation != value)
                {
                    _originLocation = value;
                    OnPropertyChanged(nameof(OriginLocation));
                }
            }
        }

        // Status property
        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        // PickupTime property
        public DateTime PickupTime
        {
            get => _pickupTime;
            set
            {
                if (_pickupTime != value)
                {
                    _pickupTime = value;
                    OnPropertyChanged(nameof(PickupTime));
                }
            }
        }

        // EstimatedDriveTime property
        public TimeSpan EstimatedDriveTime
        {
            get => _estimatedDriveTime;
            set
            {
                if (_estimatedDriveTime != value)
                {
                    _estimatedDriveTime = value;
                    OnPropertyChanged(nameof(EstimatedDriveTime));
                }
            }
        }

        // Distance property
        public double Distance
        {
            get => _distance;
            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }

        // Constructor to initialize the ViewModel with task data (optional)
        public TaskInformationViewModel(string taskId, string destinationLocation, string originLocation, string status, DateTime pickupTime, TimeSpan estimatedDriveTime, double distance)
        {
            TaskId = taskId;
            DestinationLocation = destinationLocation;
            OriginLocation = originLocation;
            Status = status;
            PickupTime = pickupTime;
            EstimatedDriveTime = estimatedDriveTime;
            Distance = distance;
        }
    }
}
