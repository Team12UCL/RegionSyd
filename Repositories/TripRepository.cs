using System;
using System.Collections.Generic;
using System.IO;

namespace RegionSyd.Repositories
{
    public class TripRepository
    {
        public List<Trip> Trips { get; set; }
        private string _path = "../../../Data/Trips.csv";

        public TripRepository()
        {
            Trips = new List<Trip>();
            LoadTrips();
        }

        // Save a new trip to the list and append it to the CSV
        public void SaveTrip(Trip trip)
        {
            // Add the new trip to the list
            trip.TripId = Trips.Count > 0 ? Trips.Max(t => t.TripId) + 1 : 1; // Assign new TripId based on max existing
            Trips.Add(trip);

            // Append the new trip to the file
            using (var writer = new StreamWriter(_path, append: true)) // Use append mode
            {
                writer.WriteLine($"{trip.TripId};{trip.AmbulanceId};{trip.TaskId}");
            }
        }

        // Remove a trip from the list and rewrite the CSV file
        public void RemoveTrip(Trip trip)
        {
            // Find the trip by TripId
            var tripToRemove = Trips.FirstOrDefault(t => t.TripId == trip.TripId);
            if (tripToRemove != null)
            {
                Trips.Remove(tripToRemove);

                // Rewrite the entire CSV file
                using (var writer = new StreamWriter(_path, append: false)) // Overwrite file
                {
                    foreach (var t in Trips)
                    {
                        writer.WriteLine($"{t.TripId};{t.AmbulanceId};{t.TaskId}");
                    }
                }
            }
        }

        // Load trips from CSV file
        public List<Trip> LoadTrips()
        {
            Trips.Clear(); // Clear the list before loading
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
                        int tripId = int.Parse(values[0]);
                        int ambulanceId = int.Parse(values[1]);
                        int taskId = int.Parse(values[2]);
                        Trip trip = new Trip(tripId, ambulanceId, taskId);
                        Trips.Add(trip);
                    }
                }
            }

            return Trips;
        }
    }
}
