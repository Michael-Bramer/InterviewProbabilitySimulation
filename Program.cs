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
        public static int NumberOfQuestionsAsked = 20;
        public static int NumberOfAnswersPerQuestion = 5;

        static void Main(string[] args)
        {
            //Variables
            bool exit_program = false;
          
            //Program Control Flow 
            while (!exit_program)
            {
                switch (DisplayMainMenu())
                {
                    case 1: //Display Prompt
                        DisplayPrompt();
                        break;
                    case 2: //Run Simulation
                        RunSimulation();
                        break;
                    case 3: //Exit Simulation
                        exit_program = true;
                        break;
                    default:
                        PressEnterToReturn("ERROR: Please Provide a Valid Numerical Input and Try Again!");
                        break;
                }

                Console.Clear();


            }
        }

        public static int DisplayMainMenu()
        {
            int selection_code = 0;
            Console.WriteLine("|=====================================================|");
            Console.WriteLine("| Welcome to the HR Interview Probability Simulation! |");
            Console.WriteLine("|=====================================================|\n\n");
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
            Console.WriteLine("\n\nIn an interview, a human resource (HR) specialist asks candidates 20 questions with 5 possible multiple choice answers to each question. " +
                              "This simulation will compute the average probability of " + NumberOfPeopleInterviewed.ToString() + " candidates providing successful answers " +
                              "for each of the following scenarios:");
            Console.WriteLine("\n\tHR has the last answer always correct, candidates select answers at random.");
            Console.WriteLine("\tHR’s solution is in a random location and student selects an answer at random.");

            PressEnterToReturn("Please press ENTER to return to the main menu...");
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

            if (top == bottom)
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }

        public static void RunSimulation()
        {
            //Variable Declaration
            float FixedCorrect = 0;
            float RandomCorrect = 0;
            float TotalFixedCorrect = 0;
            float TotalRandomCorrect = 0;
            float AverageFixedCorrect = 0;
            float AverageRandomCorrect = 0;
            int progress = 0;
            Console.WriteLine("\n");

           //Create a random number generator.
           Random rand = new Random();

          //Generate Simulation Record Array
          SimulationRecord[] Results = new SimulationRecord[NumberOfPeopleInterviewed];

          //Generate Fixed Answer Key
          int[] FixedAnswerKey = new int[NumberOfQuestionsAsked];
          int FixedAnswer = rand.Next(0, NumberOfAnswersPerQuestion);
          for (int i = 0; i < NumberOfQuestionsAsked; i++)
          {
              FixedAnswerKey[i] = FixedAnswer;
          }
          //DisplayDistributions(FixedAnswerKey, "Fixed Answer Key", true);

          //Generate Random Answer Key
          int[] RandomAnswerKey = new int[NumberOfQuestionsAsked];
          for (int i = 0; i < NumberOfQuestionsAsked; i++)
          {
              int RandomAnswer = rand.Next(0, NumberOfAnswersPerQuestion);
              RandomAnswerKey[i] = RandomAnswer;
          }
          //DisplayDistributions(RandomAnswerKey, "Random Answer Key", true);

            //Calculate the Running Average 
            for (int i = 0; i < NumberOfPeopleInterviewed; i++)
            {
                FixedCorrect = 0;
                RandomCorrect = 0;
                
                for(int x = 0; x < NumberOfQuestionsAsked; x++)
                {
                    int Answer = rand.Next(0, NumberOfAnswersPerQuestion);
                    if(Answer == RandomAnswerKey[x])
                    {
                        RandomCorrect += 1;
                    }
                    if (Answer == FixedAnswerKey[x])
                    {
                        FixedCorrect += 1;
                    }
                }

                TotalFixedCorrect  += (FixedCorrect / NumberOfQuestionsAsked);
                TotalRandomCorrect += (RandomCorrect / NumberOfQuestionsAsked);

                AverageFixedCorrect = TotalFixedCorrect / (i+1);
                AverageRandomCorrect = TotalRandomCorrect / (i+1);

                Results[i].PeopleInterviewed = i + 1;
                Results[i].ProbabilityOfCorrectFixedAnswer = AverageFixedCorrect;
                Results[i].ProbabilityOfCorrectRandomAnswer = AverageRandomCorrect;
                Results[i].DeltaProbability = Math.Abs(AverageFixedCorrect - AverageRandomCorrect);

                progress = 100 - ( ((NumberOfPeopleInterviewed - i) * 100) / NumberOfPeopleInterviewed);
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("The Simulation has Started. Current Progress: %" + progress);

            }

            //Generate the Output File

            //Press Enter to Returnt to the Menu
            PressEnterToReturn("Please press ENTER to return to the main menu...");


        }

        public static void DisplayDistributions(int[] Entries, string Title, bool PauseAfterDisplay)
        {
            int[] EntryCount = new int[NumberOfAnswersPerQuestion];

            for (int i = 0; i < EntryCount.Length; i++)
            {
                EntryCount[i] = 0;
            }

            for(int i = 0; i < Entries.Length; i++)
            {
                EntryCount[Entries[i]] += 1;                
            }

            Console.WriteLine("\n\nDisplaying Distributions for " + Title + " :");

            for (int i = 0; i < EntryCount.Length; i++)
            {
                Console.WriteLine("\tPercent Distribution for value " + (i.ToString()) + " : %" + (float)(((float)EntryCount[i] / (float)Entries.Length)*100)); 
                    
            }

            if (PauseAfterDisplay)
            {
                PressEnterToReturn("Please press ENTER to resume the simulation...");
            }

        }

        public static void PressEnterToReturn(string DisplayText)
        {
            Console.WriteLine("\n" + DisplayText + "\n");
            string DummyInput = Console.ReadLine();
        }

    }

    
}
