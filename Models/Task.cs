using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime DropOffTime { get; set; }
        public int Distance { get; set; }


        public string Description { get; set; }
        public int PickUpRegionId { get; set; }
        public int DropOffRegionId { get; set; }


        // Properties to be added later?

        //public DateTime CreatedTime { get; private set; }
        //public DateTime? CompletedTime { get; private set; }
        // public TaskStatus Status { get; private set; }
        //public Priority Priority { get; set; }
        // public Guid? AssignedAmbulanceId { get; private set; }
        // public Guid? AssignedRegionId { get; private set; }
    }
}
