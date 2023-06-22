using System;

namespace Assignment_3 {
    class Program
    {
        //Program contains all the menus and requires user inputs for the console application

        private static FileHandler? Handler;
        private static Project ThisProject = new Project();
        private static Scheduler? ThisScheduler;
        private static UserInterface Interface = new UserInterface();

        //get new Task details then add it to project
        static void addNewTaskMenu()
        {
            Interface.printTitle("Add a Task to Project");

            string newId = Interface.getStringResponse("New Task Id");

            int newTFC = Interface.getIntResponse("New Task Time for Completion");

            string dependenciesInput = Interface.getStringResponse("New Task Dependencies separated by semicolon");
            List<string> newDependencies = dependenciesInput.Split(';').Select(d => d.Trim()).ToList();

            Task newTask = new Task(newId, newTFC, newDependencies);

            if (ThisProject != null)
            {
                ThisProject.AddTaskFromObject(newTask);
            }
            else
            {
                Console.WriteLine("Project is not initialized. Please load a task list first.");
            }
        }

        //get user inputs to locate a task then remove it from the project
        static void removeTaskMenu()
        {
            Interface.printTitle("Remove Task from Project");

            string removeId = Interface.getStringResponse("What ID the name of the task you would like to remove");

            if(ThisProject != null)
            {
                ThisProject.RemoveTask(removeId);
            }
        }

        //Get user inputs to locate a task then update its time for completion
        static void changeTaskTimeMenu()
        {
            Interface.printTitle("Change Time of Completion for a Task");

            string change_TFC_id = Interface.getStringResponse("What task would you like to update time of completion for");
            
            int change_TFC = Interface.getIntResponse("What is the new time of completion?");

            if (ThisProject != null)
            {
                ThisProject.UpdateTimeForCompletion(change_TFC_id, change_TFC);
            }
        }

        //Finds best task sequence and saves to text file
        static void bestTaskSequenceMenu()
        {
            Interface.printTitle("Best Task Sequence");
            
            try
            {
                if (ThisProject != null)
                {
                    ThisScheduler?.FTSToString();
                }
                Console.WriteLine("The best task sequence has been saved in Sequence.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Interface.pressToContinue();
        }

        //Finds the earliest task commencement for each task then saves to a text file
        static void earliestTaskCommencementMenu()
        {
            Interface.printTitle("Earliest Task Commencement");
            
            try
            {
                if (ThisScheduler != null)
                {
                    ThisScheduler.CETToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Interface.pressToContinue();
        }

        //Write the current task data back to file 
        static void saveTasks()
        {
            Interface.Clear();

            if(Handler != null)
            {
                //Write back to the file
                Handler.WriteTasksToFile(ThisProject.tasks.Values.ToList());
                Console.WriteLine("Tasks have been saved to file");
                Interface.pressToContinue();
            }


        }

        //Displays each task and each tasks property stored in current project
        static void showCurrentProject()
        {
            Interface.printTitle("Current Project");
            
            if (ThisProject != null)
            {
                ThisProject.displayProject();
            }

            Interface.pressToContinue();
        }

        //Menu for getting user to enter text file name and instantiate project and shceduler and read tasks from file 
        static void loadListMenu()
        {
            Interface.printTitle("Welcome to the project managment software!");
            string fileName = Interface.getStringResponse("To load your task list, ensure the text file is stored within the project folder then enter the file name (leave off the .txt)");

            if (fileName.Length != 0)
            {
                //Assign relative path to file
                string filePath = "../../../../" + fileName + ".txt";

                FileHandler filehandler = new FileHandler(filePath);
                Handler = filehandler;

                //Instantiate new tasks list 
                List<Task> tasks = filehandler.ReadTasksFromFile();

                //Check if tasks is empty (file not found)
                if (!tasks.Any())
                {
                    loadListMenu();
                }
                else
                {
                    //Add tasks to this project
                    ThisProject.AddTasksFromList(tasks);

                    //Instantiate Scheduler class with this project
                    Scheduler scheduler = new Scheduler(ThisProject);
                    ThisScheduler = scheduler;

                    Interface.pressToContinue();
                }

            }

            MainMenu();
        }

        //Main hub can select any of 8 options. Program always defaults back to this menu while running = true
        static void MainMenu()
        {
            //Ask user for filename with tasks stored

            //give menu options
            // 1. Add new task
            // 2. Remove a task
            // 3. Change time of completion for a task
            // 4. Save tasks
            // 5. Get best task Sequence and save to new file
            // 6. Get earliest possible commencment time for each task and save to new file
           
            bool running = true;

            while (running)
            {
                Interface.Clear();
                Interface.displayMainMenu();
                
                int option = Interface.getIntResponse("Selection:");

                switch (option)
                {
                    case 1:
                        addNewTaskMenu();
                        break;
                    case 2:
                        removeTaskMenu();
                        break;
                    case 3:
                        changeTaskTimeMenu();
                        break;
                    case 4:
                        earliestTaskCommencementMenu();
                        break;
                    case 5:
                        bestTaskSequenceMenu();
                        break;
                    case 6:
                        showCurrentProject();
                        break;
                    case 7:
                        saveTasks();
                        break;
                    case 8:
                        running = false;
                        break;
                    default: break;
                }
            }
            //Close environment gracefully
            Interface.Exit();
        }

        //Entry point, prompt user to load text file 
        static void Main(string[] args)
        {
            loadListMenu();
        }
    }
}