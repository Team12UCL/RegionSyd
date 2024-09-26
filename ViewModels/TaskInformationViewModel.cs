using RegionSyd.Utility;
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
        private Models.Task? _selectedTask;

        public Models.Task? SelectedTask
        {
            get => _selectedTask;
            set { 
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to notify property changes
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Constructor to initialize the ViewModel with task data (optional)
        public TaskInformationViewModel()
        {
            SelectedTask = null;

            Messenger.Register("SelectTask", (param) =>
            {

                if (param is Models.Task task && task != SelectedTask)
                {
                    SelectedTask = task;
                }
            });
        }
    }
}


//// TaskId property
//public string TaskId
//{
//    get => _taskId;
//    set
//    {
//        if (_taskId != value)
//        {
//            _taskId = value;
//            OnPropertyChanged(nameof(TaskId));
//        }
//    }
//}

//// DestinationLocation property
//public string DestinationLocation
//{
//    get => _destinationLocation;
//    set
//    {
//        if (_destinationLocation != value)
//        {
//            _destinationLocation = value;
//            OnPropertyChanged(nameof(DestinationLocation));
//        }
//    }
//}

//// OriginLocation property
//public string OriginLocation
//{
//    get => _originLocation;
//    set
//    {
//        if (_originLocation != value)
//        {
//            _originLocation = value;
//            OnPropertyChanged(nameof(OriginLocation));
//        }
//    }
//}

//// Status property
//public string Status
//{
//    get => _status;
//    set
//    {
//        if (_status != value)
//        {
//            _status = value;
//            OnPropertyChanged(nameof(Status));
//        }
//    }
//}

//// PickupTime property
//public DateTime PickupTime
//{
//    get => _pickupTime;
//    set
//    {
//        if (_pickupTime != value)
//        {
//            _pickupTime = value;
//            OnPropertyChanged(nameof(PickupTime));
//        }
//    }
//}

//// EstimatedDriveTime property
//public TimeSpan EstimatedDriveTime
//{
//    get => _estimatedDriveTime;
//    set
//    {
//        if (_estimatedDriveTime != value)
//        {
//            _estimatedDriveTime = value;
//            OnPropertyChanged(nameof(EstimatedDriveTime));
//        }
//    }
//}

//// Distance property
//public double Distance
//{
//    get => _distance;
//    set
//    {
//        if (_distance != value)
//        {
//            _distance = value;
//            OnPropertyChanged(nameof(Distance));
//        }
//    }
//}