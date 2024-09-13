using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Models
{
    public class Ambulance
    {
        public int AmbulanceId { get; set; }
        public bool ActiveTrip { get; set; }
        public string Status {  get; set; }
        public ObservableCollection<Task> AssignedTasks { get; set; }


        public Ambulance(int ambulanceId, string status) 
        {
            AmbulanceId = ambulanceId;
            Status = status;
            AssignedTasks = new ObservableCollection<Task>();
        }
    }
}
