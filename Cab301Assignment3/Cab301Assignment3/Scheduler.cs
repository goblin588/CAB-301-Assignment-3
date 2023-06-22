using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Assignment_3
{
    internal class Scheduler
    {
        //Scheduler contains all the sorting algorithms. Uses the graph structure provided in project and task nodes. Has methods to FindTaskSequence and 
        //CalculateEarliestTimes and methods to run these + ToString them for use in the console
        private Project project;
        public Scheduler(Project prjct)
        {
            this.project = prjct;
        }

        //Function returns a List of task IDs in oredr that no dependencies are violated. Implements Topolographical sort
        public List<string> FindTaskSequence()
        {
            Stack<string> order = new Stack<string>();
            HashSet<string> visited = new HashSet<string>();

            //Find tasks with no dependencies
            var roots = project.tasks.Where(task => task.Value.Dependencies.Count == 0).Select(task => task.Value.Id).ToList();

            //Depth first seach from each root
            foreach (var root in roots)
            {
                if (!visited.Contains(root))
                {
                    FindTaskSequence_DFS(root, visited, order);
                }
            }

            //Check for any components that havent been visited via root method
            foreach (var task in project.tasks)
            {
                if (!visited.Contains(task.Value.Id))
                {
                    FindTaskSequence_DFS(task.Value.Id, visited, order);
                }
            }

            return order.ToList();
        }


        //Depth first searching method for FindTaskSequence
        private void FindTaskSequence_DFS(string task, HashSet<string> visited, Stack<string> order)
        {
            //Add task to visited list 
            visited.Add(task);

            //Visit all of this tasks depenencies
            foreach (string dependency in project.tasks[task].Dependencies)
            {
                if (!visited.Contains(dependency))
                {
                    //Recurse algorithm 
                    FindTaskSequence_DFS(dependency, visited, order);
                }
            }

            //Add task to order list 
            order.Push(task);
        }

        //CalculateEarliestTimes sums time for completion of all dependancies (and dependanices dependacies etc) for a task (each task) to show
        // the earliest time it could be started. Uses depth first search. 
        public Dictionary<string, int> CalculateEarliestTimes()
        {
            //Dictionary for EST where task IDs are keys 
            Dictionary<string, int> earliestTimes = new Dictionary<string, int>();

            //Run depth first search from each task and calculate earliest start times
            foreach (var task in project.tasks.Keys)
            {
                HashSet<string> visitedTasks = new HashSet<string>();
                earliestTimes[task] = CalculateEarliestTimes_DFS(task, visitedTasks);
            }

            return earliestTimes;
        }

        //Depth first search for CalculateEarliestTimes method 
        private int CalculateEarliestTimes_DFS(string task, HashSet<string> visitedTasks)
        {
            int totalDependencyTime = 0;

            //Visit all of this task's dependencies and calculate their earliest start times recursively
            foreach (string dependency in project.tasks[task].Dependencies)
            {
                //Add task time for completion if it hasn't already been visited
                if (!visitedTasks.Contains(dependency))
                {
                    //Mark task as visited
                    visitedTasks.Add(dependency); 
                    //Sum and recurse
                    totalDependencyTime += project.tasks[dependency].TimeForCompletion + CalculateEarliestTimes_DFS(dependency, visitedTasks);
                }
            }

            return totalDependencyTime;
        }

        //Method to run FindTaskSequence and then display its output in terminal. Call this in program.
        public void FTSToString()
        {
            List<string> taskSequence = FindTaskSequence();
            if (taskSequence.Count == 0)
            {
                Console.WriteLine("Task Sequence is empty");
            }
            else
            {
                //Write best sequence in console 
                Console.WriteLine("Task Sequence: ");
                foreach (string task in taskSequence)
                {
                    Console.WriteLine(task);
                }
                //Write to file Sequence.txt which will be saved in project folder (relative path to bin folder)
                File.WriteAllLines("../../../../Sequence.txt", taskSequence);
            }
        }

        //Method to run CalculateEarliestTimes and then display its output in terminal. Call this in program.
        public void CETToString()
        {
            Dictionary<string, int> earliestTimes = CalculateEarliestTimes();
            StringBuilder sb = new StringBuilder();
            
            //Write each dictionary line to console and to string builder
            sb.AppendLine("Earliest Times: ");
            foreach (var task in earliestTimes)
            {
                sb.AppendLine($"{task.Key}: {task.Value}");
                Console.WriteLine($"{task.Key}: {task.Value}");
            }

            //Write to file EarliestTimes.txt which will be saved in project folder (relative path to bin folder)
            File.WriteAllText("../../../../EarliestTimes.txt", sb.ToString());

            Console.WriteLine("The earliest task commencement list has been saved in EarliestTimes.txt");
        }

    }
}