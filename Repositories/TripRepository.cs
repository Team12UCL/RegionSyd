using RegionSyd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegionSyd.Repositories
{
    public class TripRepository
    {
        public List<Trip> Trips { get; set; }
        private string _path = "../../../Data/trips.csv";

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
            using (var writer = new StreamWriter(_path, append: true))
            {
                writer.WriteLine($"{trip.TripId};{trip.AmbulanceId};{trip.TaskId};{trip.PickUpRegionId};{trip.DropOffRegionId}");
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

                // Rewrite the entire CSV file (not in append mode)
                using (var writer = new StreamWriter(_path)) // Overwrite file
                {
                    writer.WriteLine("TripId;AmbulanceId;TaskId;PickUpRegionId;DropOffRegionId"); // Include the header
                    foreach (var t in Trips)
                    {
                        writer.WriteLine($"{t.TripId};{t.AmbulanceId};{t.TaskId};{t.PickUpRegionId};{t.DropOffRegionId}");
                    }
                }
            }
        }

        // Load trips from CSV file
        public List<Trip> LoadTrips()
        {
            Trips.Clear(); // Clear the list before loading

            // Ensure the file exists, but don't create it immediately
            if (!File.Exists(_path))
            {
                return Trips; // Return an empty list if no file exists
            }

            using (var reader = new StreamReader(_path))
            {
                string line;
                reader.ReadLine(); // Skip the header line
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(';');
                    if (values.Length == 5) // Ensure correct number of fields
                    {
                        int tripId = int.Parse(values[0]);
                        int ambulanceId = int.Parse(values[1]);
                        int taskId = int.Parse(values[2]);
                        int pickUpRegionId = int.Parse(values[3]);
                        int dropOffRegionId = int.Parse(values[4]);

                        // Create a new Trip object
                        Trip trip = new Trip(tripId, ambulanceId, taskId, pickUpRegionId, dropOffRegionId);
                        Trips.Add(trip);
                    }
                }
            }

            return Trips;
        }
    }
}
