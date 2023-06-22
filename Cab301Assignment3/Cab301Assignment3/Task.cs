using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    internal class Task
    {
        //Task ADT acting as Node for Project Graph where neighbours are stored in dependencies list 

        //Task ID
        public string Id { get; set; }

        //Time for task to be completed [mins]
        public int TimeForCompletion { get; set; }

        //Store dependencies as list of dependencies
        public List<string> Dependencies { get; set; } 

        public Task(string id, int timeForCompletion, List<string> dependencies)
        {
            Id = id;
            TimeForCompletion = timeForCompletion;
            Dependencies = dependencies ?? new List<string>(); 
        }
    }
}
