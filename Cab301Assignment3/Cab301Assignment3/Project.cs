using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assignment_3
{
    internal class Project
    {
        //Project is like a graph strucure using a dictionary where Nodes are the Task class with dependencies lists acting as edges

        public Dictionary<string, Task> tasks = new Dictionary<string, Task>();

        //Adds a task object to the tasks dictionary by checking if key is aready stored 
        public void AddTaskFromObject(Task task)
        {
            if (!tasks.ContainsKey(task.Id))
            {
                tasks.Add(task.Id, task);
            }
            else
            {
                Console.WriteLine("Task is already in list. Press to Return to Main Menu");
                Console.ReadKey();
            }
        }

        //Iteratively passes each task in taskList to be added to tasks dictionary using AddTaskFromObj method
        public void AddTasksFromList(List<Task> taskList)
        {
            foreach (var task in taskList)
            {
                AddTaskFromObject(task);
            }
        }

        //Instantiates a task from parameters then adds to tasks dictionary 
        public void AddTask(string id, int timeForCompletion, List<string> dependencies)
        {
            if (!tasks.ContainsKey(id))
            {
                var task = new Task(id, timeForCompletion, dependencies);
                tasks.Add(id, task);
            }
        }

        //Checks that task is in tasks dictionary and removes it then removes task from dependancies of all other tasks
        public void RemoveTask(string id)
        {
            if (tasks.ContainsKey(id))
            {
                tasks.Remove(id);

                // Remove the task from dependencies of other tasks
                foreach (var task in tasks.Values)
                {
                    task.Dependencies.Remove(id);
                }
            }
        }

        //Find task then change its time for completion property
        public void UpdateTimeForCompletion(string id, int newTimeForCompletion)
        {
            if (tasks.ContainsKey(id))
            {
                tasks[id].TimeForCompletion = newTimeForCompletion;
            }
            else
            {
                Console.WriteLine("Task with the given ID does not exist.");
            }
        }

        //Display all tasks currently stored in this project with their Id, TFC and dependencies
        public void displayProject()
        {
            foreach (var task in tasks.Values)
            {
                Console.WriteLine($"Task: {task.Id}");
                Console.WriteLine($"Time for completion: {task.TimeForCompletion}");
                Console.WriteLine("Dependencies:");
                foreach (var dependency in task.Dependencies)
                {
                    Console.WriteLine(dependency);
                }
                Console.WriteLine();
            }
        }
    }
}

