﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RegionSyd.Repositories
{
    public class TaskRepository
    {
        public List<Models.Task> Tasks { get; set; }

        public TaskRepository()
        {
            Tasks = new List<Models.Task>();
        }

        public List<Models.Task> GetAllTasks()
        {
            string filePath = @"../../../Data/opgaver.csv";

            try
            {
                // Read all lines from the CSV file
                var lines = File.ReadAllLines(filePath);

                // Ensure the CSV has at least one row of data
                for (int i = 1; i < lines.Length; i++)
                {
                    var fields = lines[i].Split(';');

                    // Ensure each row has enough fields (4 fields: Id, Til, Fra, Tid)
                    if (fields.Length >= 4)
                    {
                        Tasks.Add(new Models.Task
                        {
                            TaskId = int.Parse(fields[0]),
                            Origin = fields[1],
                            Destination = fields[2],
                            PickupTime = DateTime.Parse(fields[3])
                        });
                    }
                }
                return Tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}");
                return Tasks;
            }

            MessageBox.Show($"Total tasks: {Tasks.Count}");
        }

        public void RemoveTask(int taskId)
        {
            var task = Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task != null)
            {
                Tasks.Remove(task);
                MessageBox.Show($"Task with Id: {taskId} removed");
                SaveTasks();
            }
            else
            {
                MessageBox.Show($"Task with Id: {taskId} not found");
            }
        }

        public void SaveTasks()
        {
            string filePath = @"../../../Data/opgaver.csv";

            try
            {
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // Write the header with property names
                    writer.WriteLine("TaskId;Origin;Destination;PickupTime");

                    // Write each task to the CSV
                    foreach (var task in Tasks)
                    {
                        writer.WriteLine($"{task.TaskId};{task.Origin};{task.Destination};{task.PickupTime:yyyy-MM-dd HH:mm:ss}");
                    }
                }
                MessageBox.Show($"Successfully saved {Tasks.Count} tasks to {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
            }
        }

    }
}
