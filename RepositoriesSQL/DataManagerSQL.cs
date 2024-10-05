using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RegionSyd.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RegionSyd.RepositoriesSQL
{
    public class DataManagerSQL
    {
        private readonly string _connectionString;

        public List<Region> Regions { get; set; }
        public List<Dispatcher> Dispatchers { get; set; }

        public DataManagerSQL()
        {
            _connectionString = App.Configuration.GetConnectionString("DefaultConnection");
            Regions = new List<Region>();
            Dispatchers = new List<Dispatcher>();
        }

        // Get all Regions from the database
        public List<Region> GetAllRegions()
        {
            Regions.Clear(); // Clear the list before loading

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, System FROM Region"; // SQL query to fetch regions
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var region = new Region
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                System = reader.GetString(2)
                            };
                            Regions.Add(region);
                        }
                    }
                }
            }

            MessageBox.Show($"Total regions: {Regions.Count}");
            return Regions;
        }

        // Get all Dispatchers from the database
        public List<Dispatcher> GetAllDispatchers()
        {
            Dispatchers.Clear(); // Clear the list before loading

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, FirstName, LastName, UserName, Password FROM Dispatcher"; // SQL query to fetch dispatchers
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dispatcher = new Dispatcher
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                UserName = reader.GetString(3),
                                Password = reader.GetString(4)
                            };
                            Dispatchers.Add(dispatcher);
                        }
                    }
                }
            }

            MessageBox.Show($"Total dispatchers: {Dispatchers.Count}");
            return Dispatchers;
        }
    }
}
