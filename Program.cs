using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InterviewSimulation
{
    struct SimulationRecord
    {
        public int PeopleInterviewed;
        public float ProbabilityOfCorrectFixedAnswer;
        public float ProbabilityOfCorrectRandomAnswer;
        public float DeltaProbability;
    };

    class Program
    {
        public static int NumberOfPeopleInterviewed = 100000;

        static void Main(string[] args)
        {
            //Variables
            bool exit_program = false;
            string DummyInput = "";
                        
            //Display Header Banner
            Console.WriteLine("|=====================================================|");
            Console.WriteLine("| Welcome to the HR Interview Probability Simulation! |");
            Console.WriteLine("|=====================================================|\n\n");

            //Program Control Flow 
            while (!exit_program)
            {
                switch (DisplayMainMenu())
                {
                    case 1: //Display Prompt
                        DisplayPrompt();
                        break;
                    case 2: //Run Simulation
                        Console.WriteLine("Case 2");
                        break;
                    case 3: //Exit Simulation
                        exit_program = true;
                        break;
                    default:
                        Console.WriteLine("\nERROR: Please Provide a Valid Numerical Input and Try Again!\n");
                        DummyInput = Console.ReadLine();
                        break;
                }

                ClearRangeOfConsoleLines(4, Console.CursorTop);


            }
        }

        public static int DisplayMainMenu()
        {
            int selection_code = 0;
            Console.WriteLine("Please Select an Action to Perform:");
            Console.WriteLine("\n\t1.] Display Prompt");
            Console.WriteLine("\t2.] Run Simulation");
            Console.WriteLine("\t3.] Exit Simulation");

            Console.Write("\nInput: ");
            string Input = Console.ReadLine();

            try
            {
                selection_code = Int32.Parse(Input);
            }
            catch (FormatException)
            {
                //Console.WriteLine("Please Provide a Valid Numerical Input and Try Again!\n");
                selection_code = -1;
            }


            return selection_code;
        }

        public static void DisplayPrompt()
        {
            string DummyInput = "";

            Console.WriteLine("\n\nIn an interview, a human resource (HR) specialist asks candidates 20 questions with 5 possible multiple choice answers to each question. " +
                              "This simulation will compute the average probability of " + NumberOfPeopleInterviewed.ToString() + " candidates providing successful answers " +
                              "for each of the following scenarios:");
            Console.WriteLine("\n\tHR has the last answer always correct, candidates select answers at random.");
            Console.WriteLine("\tHR’s solution is in a random location and student selects an answer at random.");

            Console.WriteLine("\nPlease press ENTER to return to the main menu...!\n");
            DummyInput = Console.ReadLine();
        }


        public static void ClearRangeOfConsoleLines(int top, int bottom)
        {
            for(int i = bottom; i > top; i--)
            {
                //Set The Cursor Position for the Interval
                Console.SetCursorPosition(0, Console.CursorTop - 1);

                //Clear Current Console Line
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }

        public static void RunSimulation()
        {

        }

    }
}
