using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    internal class UserInterface
    {
        //Interfacing class to simplify Program and reduce rewriting code. Has methods for printing titles and handling question response for strings and ints
        public UserInterface()
        {

        }

        //main options menu text 
        public void displayMainMenu()
        {
            Console.WriteLine("------Main Menu-----");
            Console.WriteLine("Please select an option:");

            Console.WriteLine("1. Add new task");
            Console.WriteLine("2. Remove a task");
            Console.WriteLine("3. Change time of completion for a task");
            Console.WriteLine("4. Get earliest possible commencement time for each task");
            Console.WriteLine("5. Get best task Sequence and save to new file");
            Console.WriteLine("6. Show tasks in current project");
            Console.WriteLine("7. Save tasks");
            Console.WriteLine("8. Exit\n");
        }

        //display ----- X Menu ----- at top of each 'page'
        public void printTitle(string title)
        {
            Console.Clear();
            Console.WriteLine("---- " + title + " ----\n");
        }

        //handling string responses
        public string getStringResponse(string prompt)
        {
            Console.WriteLine(prompt);

            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Input cannot be null. Please try again.");
                return getStringResponse(prompt); 
            }

            checkExit(input);
            return input;
        }

        //handling int responses
        public int getIntResponse(string prompt)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();

            

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input cannot be null or empty. Please try again.");
                return getIntResponse(prompt); 
            }

            checkExit(input);

            if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Please enter a valid integer.");
                return getIntResponse(prompt); 
            }
        }

        //wait for key then continue with program 
        public void pressToContinue()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public void Clear()
        {
            Console.Clear();
        }

        //method to close environment
        public void Exit()
        {
            Environment.Exit(0);
        }

        //Allow users to type 'exit' at any time to exit
        public void checkExit(string input)
        {
            if (input.ToLower() == "exit")
                Exit();
        }

    }
}
