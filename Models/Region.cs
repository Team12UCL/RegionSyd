using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Models
{
    internal class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string System {  get; set; }

        public string GetRegionInfo()
        {
            return $"Region Name: {Name}";
        }
        public string GetTasks()
        {
            return List<Task>;
        }
    }
}
