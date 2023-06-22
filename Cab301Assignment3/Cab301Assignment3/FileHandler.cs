using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    internal class FileHandler
    {
        //Filehandler reads information from text file and stores Id TFC and Dependencies. Can also write back to this file to save information

        private string filePath;

        public FileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Task> ReadTasksFromFile()
        {
            //Instantiate list to store tasks
            List<Task> tasks = new List<Task>();
            try
            {
                //Get lines from text file
                string[] lines = File.ReadAllLines(filePath);

                //Split up data on each line 
                foreach (string line in lines)
                {
                    //Split up Id TFC
                    string[] parts = line.Split(',');

                    if (parts.Length >= 2)
                    {
                        string taskId = parts[0].Trim();
                        int timeForCompletion = int.Parse(parts[1]);

                        List<string> dependencies = new List<string>();

                        //All values after 2nd index will be dependencies
                        if (parts.Length > 2)
                        {
                            for (int i = 2; i < parts.Length; i++)
                            {
                                string trimmedDep = parts[i].Trim();
                                if (!string.IsNullOrEmpty(trimmedDep))
                                {
                                    dependencies.Add(trimmedDep);
                                }
                            }
                        }

                        //Instantiate each task and add to task list
                        Task task = new Task(taskId, timeForCompletion, dependencies);
                        tasks.Add(task);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file at {filePath} was not found. Press key to continue");
                Console.ReadKey();
                //return empty list 
                return new List<Task>();
            }
            return tasks;
        }

        public void WriteTasksToFile(List<Task> tasks)
        {
                //Instantiate list for lines of text
                List<string> lines = new List<string>();

                foreach (Task task in tasks)
                {
                string line = $"{task.Id},{task.TimeForCompletion},{string.Join(",", task.Dependencies)}";
                lines.Add(line);
                }

                // Write all lins to a new file or overwrite existing
                File.WriteAllLines(filePath, lines);
        }
    }
}
