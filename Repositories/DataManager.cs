using RegionSyd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RegionSyd.Repositories
{
    public class DataManager
    {        
        public List<Region> Regions { get; set; }
        public List<Dispatcher> Dispatchers { get; set; } 
        public DataManager()
        {
            Regions = new List<Region>();
            Dispatchers = new List<Dispatcher>();
        }

        public void GetAllRegions()
        {
            string filePath = @"../../../Data/regioner.csv";            

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                // Skip the header line
                reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line into fields based on the semicolon separator
                    string[] fields = line.Split(';');

                    // Create a new Region object and add it to the list
                    Region region = new Region
                    {
                        Id = int.Parse(fields[0]),
                        Name = fields[1],
                        System = fields[2]
                    };

                    Regions.Add(region);
                }
            }

            MessageBox.Show($"Total regions: {Regions.Count}");
        }
        public void GetAllDispatchers()
        {
            string filePath = @"../../../Data/disponenter.csv";

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                // Skip the header line
                reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line into fields based on the semicolon separator
                    string[] fields = line.Split(';');

                    // Create a new dispatcher object and add it to the list
                    Dispatcher dispatcher = new Dispatcher
                    {
                        Id = int.Parse(fields[0]),
                        FirstName = fields[1],
                        LastName = fields[2],
                        UserName = fields[3],
                        Password = fields[4]
                    };

                    Dispatchers.Add(dispatcher);
                }
            }
            MessageBox.Show($"Total dispatchers: {Dispatchers.Count}");
        }
    }
}
