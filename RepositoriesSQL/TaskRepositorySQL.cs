
using Microsoft.Data.SqlClient;
using System.Data;

using System.Windows;

namespace RegionSyd.RepositoriesSQL
{
    public class TaskRepositorySQL
    {
        private string _connectionString;

        public List<Models.Task> Tasks { get; set; }

        public TaskRepositorySQL(string connectionString = 
            "Data Source=(localdb)\\GustavDB;Initial Catalog=RegionSyd;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
            )
        {
            _connectionString = connectionString;
            Tasks = new List<Models.Task>();
        }

        public List<Models.Task> GetAllTasks()
        {
            Tasks = [];
            string query = "SELECT * FROM Task"; // Replace with your actual table name

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Tasks.Add(new Models.Task
                        {
                            TaskId = reader.GetInt32(0), // TaskId
                            Origin = reader.GetString(1), // Origin
                            Destination = reader.GetString(2), // Destination
                            PickupTime = reader.GetDateTime(3), // PickupTime
                            DropOffTime = reader.GetDateTime(4), // DropOffTime
                            Distance = reader.GetDecimal(5), // Distance_km (decimal)
                            PickUpRegionId = reader.GetInt32(6), // OriginRegionId
                            DropOffRegionId = reader.GetInt32(7),
                            IsAvailable = reader.GetBoolean(8) // IsTaken
                        });
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading from database: {ex.Message}");
                }
            }

            
            return Tasks;
        }

        public List<Models.Task> GetTasksByRegionId(int regionId, bool isPickup)
        {
            if (Tasks == null || Tasks.Count == 0)
            {
                GetAllTasks();
            }

            return isPickup
                ? Tasks.Where(t => t.PickUpRegionId == regionId).ToList()
                : Tasks.Where(t => t.DropOffRegionId == regionId).ToList();
        }

        public void RemoveTask(int taskId)
        {
            string query = "DELETE FROM Task WHERE TaskId = @TaskId"; // Replace with your actual table name

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TaskId", taskId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Task with Id: {taskId} removed");
                    }
                    else
                    {
                        MessageBox.Show($"Task with Id: {taskId} not found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing task: {ex.Message}");
                }
            }
        }

        public void SaveTasks()
        {
            string query = "INSERT INTO Task (TaskId, Origin, Destination, PickupTime, DropOffTime, Distance, PickUpRegionId, DropOffRegionId, IsAvailable) VALUES (@TaskId, @Origin, @Destination, @PickupTime, @DropOffTime, @Distance, @PickUpRegionId, @DropOffRegionId, @IsAvailable)"; // Replace with your actual table name

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (var task in Tasks)
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@TaskId", task.TaskId);
                        command.Parameters.AddWithValue("@Origin", task.Origin);
                        command.Parameters.AddWithValue("@Destination", task.Destination);
                        command.Parameters.AddWithValue("@PickupTime", task.PickupTime);
                        command.Parameters.AddWithValue("@DropOffTime", task.DropOffTime);
                        command.Parameters.AddWithValue("@Distance", task.Distance);
                        command.Parameters.AddWithValue("@PickUpRegionId", task.PickUpRegionId);
                        command.Parameters.AddWithValue("@DropOffRegionId", task.DropOffRegionId);
                        command.Parameters.AddWithValue("@IsAvailable", task.IsAvailable);


                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Successfully saved {Tasks.Count} tasks to the database.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving tasks to the database: {ex.Message}");
                }
            }
        }

        public Models.Task? GetTaskById(int taskId)
        {
            string query = "SELECT * FROM Task WHERE TaskId = @TaskId"; // Replace with your actual table name
            Models.Task? task = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TaskId", taskId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        task = new Models.Task
                        {
                            TaskId = reader.GetInt32(0),
                            Origin = reader.GetString(1),
                            Destination = reader.GetString(2),
                            PickupTime = reader.GetDateTime(3),
                            DropOffTime = reader.GetDateTime(4),
                            Distance = reader.GetDecimal(5),
                            PickUpRegionId = reader.GetInt32(6),
                            DropOffRegionId = reader.GetInt32(7),
                            IsAvailable = reader.GetBoolean(8)
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving task: {ex.Message}");
                }
            }

            return task;
        }

        public List<Models.Task> GetReturnTasksByRegionIdAndDateTime(int pickUpRegionId, int dropOffRegionId, DateTime dropOffTime)
        {
            if (Tasks == null || Tasks.Count == 0)
            {
                GetAllTasks();
            }

            return Tasks.Where(t => t.PickUpRegionId == pickUpRegionId
                                 && t.DropOffRegionId == dropOffRegionId
                                 && t.PickupTime > dropOffTime
                                 && t.IsAvailable).ToList();
        }


        // Get tasks that is not taken
        public List<Models.Task> GetAvailableTasks()
        {
            
            
                Tasks = GetAllTasks();
            

            return Tasks.Where(t => t.IsAvailable).ToList();
        }

        // Update task availability
        public void UpdateTaskAvailability(int taskId, bool isAvailable)
        {
            string query = "UPDATE Task SET IsAvailable = @IsAvailable WHERE TaskId = @TaskId"; // Replace with your actual table name

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IsAvailable", isAvailable);
                    command.Parameters.AddWithValue("@TaskId", taskId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    { 
                        MessageBox.Show($"Task with Id: {taskId} not found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating task: {ex.Message}");
                }
            }
        }
    }
}
