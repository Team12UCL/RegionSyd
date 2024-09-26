using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RegionSyd.Models;
using RegionSyd.Repositories;

namespace RegionSyd.ViewModels
{
    internal class AssignmentViewModel
    {
        TaskRepository taskRepository { get; set; }
        public List<Models.Task> Tasks { get; set; }

        public AssignmentViewModel()
        {
            taskRepository = new TaskRepository();
            Tasks = taskRepository.GetAllTasks();
            //LoadDataFromFile("Tasks.csv");
        }


        //public void LoadDataFromFile(string filePath)
        //{
        //    try
        //    {
        //        // Read all lines from the CSV file
        //        var lines = File.ReadAllLines(filePath);

        //        // Ensure the CSV has at least one row of data
        //        for (int i = 1; i < lines.Length; i++)
        //        {
        //            var fields = lines[i].Split(';');

        //            // Ensure each row has enough fields (4 fields: Id, Til, Fra, Tid)
        //            if (fields.Length >= 4)
        //            {
        //                Tasks.Add(new Models.Task
        //                {
        //                    TaskId = int.Parse(fields[0]),
        //                    Origin = fields[1],
        //                    Destination = fields[2],
        //                    PickupTime = DateTime.Parse(fields[3])
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error reading file: {ex.Message}");
        //    }
        //}
    }
}
