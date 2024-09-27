using RegionSyd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegionSyd.Repositories
{
    public class AmbulanceRepository
    {
        public List<Ambulance> Ambulances { get; set; }
        private string _path = "../../../Data/ambulancer.csv";

        public AmbulanceRepository()
        {
            Ambulances = new List<Ambulance>();
            LoadAmbulances();
        }

        // Load ambulances from the CSV file
        public void LoadAmbulances()
        {
            Ambulances.Clear(); // Clear the list before loading
            if (!File.Exists(_path))
            {
                File.Create(_path).Close(); // Create the file if it doesn't exist
            }

            using (var reader = new StreamReader(_path))
            {
                string line;
                reader.ReadLine(); // Skip header line
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(';');
                    if (values.Length == 3)
                    {
                        int ambulanceId = int.Parse(values[0]);
                        int regionId = int.Parse(values[1]);
                        string status = values[2];
                        Ambulance ambulance = new Ambulance(ambulanceId, regionId, status);
                        Ambulances.Add(ambulance);
                    }
                }
            }
        }

        // Save a new ambulance to the list and append it to the CSV
        public void SaveAmbulance(Ambulance ambulance)
        {
            // Assign new AmbulanceId based on max existing
            ambulance.AmbulanceId = Ambulances.Count > 0 ? Ambulances.Max(a => a.AmbulanceId) + 1 : 1;
            Ambulances.Add(ambulance);

            // Append the new ambulance to the file
            using (var writer = new StreamWriter(_path, append: true))
            {
                writer.WriteLine($"{ambulance.AmbulanceId};{ambulance.RegionId}");
            }
        }

        // Remove an ambulance from the list and rewrite the CSV file
        public void RemoveAmbulance(Ambulance ambulance)
        {
            var ambulanceToRemove = Ambulances.FirstOrDefault(a => a.AmbulanceId == ambulance.AmbulanceId);
            if (ambulanceToRemove != null)
            {
                Ambulances.Remove(ambulanceToRemove);

                // Rewrite the entire CSV file
                using (var writer = new StreamWriter(_path, append: false))
                {
                    writer.WriteLine("AmbulanceId;RegionId"); // Write header
                    foreach (var a in Ambulances)
                    {
                        writer.WriteLine($"{a.AmbulanceId};{a.RegionId}");
                    }
                }
            }
        }

        // Get All Ambulances
        public List<Ambulance> GetAllAmbulances()
        {
            if (Ambulances.Count == 0)
            {
                LoadAmbulances();
            }
            return Ambulances;
        }

        // get available ambulances
        public List<Ambulance> GetAvailableAmbulances()
        {
            return Ambulances.Where(a => a.Status == "Available").ToList();
        }


        // Update Ambulace status
        public void UpdateAmbulanceStatus(int ambulanceId, string status)
        {
            var ambulance = Ambulances.FirstOrDefault(a => a.AmbulanceId == ambulanceId);
            if (ambulance != null)
            {
                ambulance.Status = status;

                // Rewrite the entire CSV file
                using (var writer = new StreamWriter(_path, append: false))
                {
                    writer.WriteLine("AmbulanceId;RegionId;Status"); // Write header
                    foreach (var a in Ambulances)
                    {
                        writer.WriteLine($"{a.AmbulanceId};{a.RegionId};{a.Status}");
                    }
                }
            }
        }

        // get ambulance by id
        public Ambulance? GetAmbulanceById(int ambulanceId)
        {
            return Ambulances.FirstOrDefault(a => a.AmbulanceId == ambulanceId);
        }
    }
}