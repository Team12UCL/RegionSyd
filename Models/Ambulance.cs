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
            public int RegionId { get; set; }
        public string Status { get; set; }

        public Ambulance(int ambulanceId, int regionId, string status)
            {
                AmbulanceId = ambulanceId;
                RegionId = regionId;
            Status = status;
        }
        
    }
}
