using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;


namespace InterviewSimulation
{
    struct InterviewMetrics
    {
        public float NumberOfCorrectAnswers;
        public float NumberOfPassingExams;
        public float CummulativeProbabilityOfAnsweringCorrectly;
        public float AverageProbabilityOfAnsweringCorrectly;
        public float CummulativeProbabilityOfPassing;
        public float AverageProbabilityOfPassing;
    };

    struct SolutionTypeRecord
    {
        public float NumberPassed;
        public float AverageProbabilityOfAnsweringCorrectly;
        public float AverageProbabilityOfPassing;
    };

    struct SimulationRecord
    {
        public int PeopleInterviewed;
        public SolutionTypeRecord FixedSolution;
        public SolutionTypeRecord RandomSolution;              
        public float DeltaCorrectAnswerProbability;
        public float DeltaPassingIntervewProbability;
    };

    class Program
    {
        public static int NumberOfPeopleInterviewed = 100000;
        public static int NumberOfQuestionsAsked = 20;
        public static int NumberOfAnswersPerQuestion = 5;
        public static int NumberOfCorrectAnswersToPass = 10;

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
            InterviewMetrics FixedSolutions;
            InterviewMetrics RandomSolutions;
            int progress = 0;

            //Structure Initialization
            FixedSolutions.NumberOfCorrectAnswers = 0;
            FixedSolutions.NumberOfPassingExams = 0; 
            FixedSolutions.AverageProbabilityOfAnsweringCorrectly = 0;
            FixedSolutions.CummulativeProbabilityOfAnsweringCorrectly = 0;
            FixedSolutions.CummulativeProbabilityOfPassing = 0;
            FixedSolutions.AverageProbabilityOfPassing = 0;

            RandomSolutions.NumberOfCorrectAnswers = 0;
            RandomSolutions.NumberOfPassingExams = 0;
            RandomSolutions.AverageProbabilityOfAnsweringCorrectly = 0;
            RandomSolutions.CummulativeProbabilityOfAnsweringCorrectly = 0;
            RandomSolutions.CummulativeProbabilityOfPassing = 0;
            RandomSolutions.AverageProbabilityOfPassing = 0;

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
                //Reset The Number of Correct Answers for Each Candidate
                FixedSolutions.NumberOfCorrectAnswers = 0;
                RandomSolutions.NumberOfCorrectAnswers = 0;

                //Record How Each Candidate Performs
                for (int x = 0; x < NumberOfQuestionsAsked; x++)
                {
                    int Answer = rand.Next(0, NumberOfAnswersPerQuestion);
                    if(Answer == RandomAnswerKey[x])
                    {
                        RandomSolutions.NumberOfCorrectAnswers += 1;
                    }
                    if (Answer == FixedAnswerKey[x])
                    {
                        FixedSolutions.NumberOfCorrectAnswers += 1;
                    }
                }

                //Calulate the Cummulative Probability of Answering Correctly From the Current Candidate Pool
                FixedSolutions.CummulativeProbabilityOfAnsweringCorrectly += (FixedSolutions.NumberOfCorrectAnswers / NumberOfQuestionsAsked);
                RandomSolutions.CummulativeProbabilityOfAnsweringCorrectly += (RandomSolutions.NumberOfCorrectAnswers / NumberOfQuestionsAsked);

                //Calulate the Average Probability of Answering Correctly From the Current Candidate Pool
                FixedSolutions.AverageProbabilityOfAnsweringCorrectly = FixedSolutions.CummulativeProbabilityOfAnsweringCorrectly / (i + 1);
                RandomSolutions.AverageProbabilityOfAnsweringCorrectly = RandomSolutions.CummulativeProbabilityOfAnsweringCorrectly / (i + 1);
                
                //Record the Number of Candidates that Passed
                if(RandomSolutions.NumberOfCorrectAnswers >= NumberOfCorrectAnswersToPass)
                {
                    RandomSolutions.NumberOfPassingExams += 1;
                }

                if (FixedSolutions.NumberOfCorrectAnswers >= NumberOfCorrectAnswersToPass)
                {
                    FixedSolutions.NumberOfPassingExams += 1;
                }

                //Record the Probability of Candidates that Passed
                FixedSolutions.CummulativeProbabilityOfPassing  += FixedSolutions.NumberOfPassingExams  / (i + 1);
                RandomSolutions.CummulativeProbabilityOfPassing += RandomSolutions.NumberOfPassingExams / (i + 1);

                FixedSolutions.AverageProbabilityOfPassing  = FixedSolutions.CummulativeProbabilityOfPassing / (i + 1);
                RandomSolutions.AverageProbabilityOfPassing = RandomSolutions.CummulativeProbabilityOfPassing / (i + 1);

                //Record the Results
                Results[i].PeopleInterviewed                                     = i + 1;

                Results[i].FixedSolution.NumberPassed                            = FixedSolutions.NumberOfPassingExams;
                Results[i].FixedSolution.AverageProbabilityOfAnsweringCorrectly  = FixedSolutions.AverageProbabilityOfAnsweringCorrectly;
                Results[i].FixedSolution.AverageProbabilityOfPassing             = FixedSolutions.AverageProbabilityOfPassing;
                
                Results[i].RandomSolution.NumberPassed                           = RandomSolutions.NumberOfPassingExams;
                Results[i].RandomSolution.AverageProbabilityOfAnsweringCorrectly = RandomSolutions.AverageProbabilityOfAnsweringCorrectly;
                Results[i].RandomSolution.AverageProbabilityOfPassing            = RandomSolutions.AverageProbabilityOfPassing;

                Results[i].DeltaCorrectAnswerProbability   = Math.Abs(FixedSolutions.AverageProbabilityOfAnsweringCorrectly - RandomSolutions.AverageProbabilityOfAnsweringCorrectly);
                Results[i].DeltaPassingIntervewProbability = Math.Abs(FixedSolutions.AverageProbabilityOfPassing - RandomSolutions.AverageProbabilityOfPassing);

                if (false){
                    Console.WriteLine(Results[i].PeopleInterviewed + "," +
                                  Results[i].FixedSolution.AverageProbabilityOfAnsweringCorrectly + "," +
                                  Results[i].RandomSolution.AverageProbabilityOfAnsweringCorrectly + "," +
                                  Results[i].DeltaCorrectAnswerProbability + "," +
                                  Results[i].FixedSolution.NumberPassed + "," +
                                  Results[i].RandomSolution.NumberPassed + "," +
                                  Results[i].FixedSolution.AverageProbabilityOfPassing + "," +
                                  Results[i].RandomSolution.AverageProbabilityOfPassing + "," +
                                  Results[i].DeltaPassingIntervewProbability);
                }
                
                progress = 100 - ( ((NumberOfPeopleInterviewed - i) * 100) / NumberOfPeopleInterviewed);
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("The Simulation has Started. Current Progress: %" + progress);
            }

            //Generate the Output File
            GenerateFile(Results);

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

        public static void GenerateFile(SimulationRecord[] Record)
        {
            int progress = 0;
            string path = @"C:\Users\Michael\Documents\Interview Simulation for " + DateTime.Now.ToString("yyyymmddhhmmss") + @".txt";
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] infoh = new UTF8Encoding(true).GetBytes("Number Interviewed" + "," +
                                                                    "Average Probability of Answering Correctly for a Fixed Solution Key" + "," +
                                                                    "Average Probability of Answering Correctly for a Random Solution Key" + "," +
                                                                    "Delta Probability of Answering Correctly" + "," +
                                                                    "Number of Passing Candidates for a Fixed Solution Key" + "," +
                                                                    "Number of Passing Candidates for a Random Solution Key" + "," +
                                                                    "Average Probability of Passing for Interview for a Fixed Solution Key" + "," +
                                                                    "Average Probability of Passing the Interview for a Random Solution Key" + "," +
                                                                    "Delta Probability of Passing" + "," +
                                                                    "Qb Stock" + "\n");

                    // Add some information to the file.
                    fs.Write(infoh, 0, infoh.Length);

                    for (int i = 0; i < Record.Length; i++)
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(Record[i].PeopleInterviewed.ToString() + "," +
                                                                      Record[i].FixedSolution.AverageProbabilityOfAnsweringCorrectly.ToString() + "," +
                                                                      Record[i].RandomSolution.AverageProbabilityOfAnsweringCorrectly.ToString() + "," +
                                                                      Record[i].DeltaCorrectAnswerProbability.ToString() + "," +
                                                                      Record[i].FixedSolution.NumberPassed.ToString() + "," +
                                                                      Record[i].RandomSolution.NumberPassed.ToString() + "," +
                                                                      Record[i].FixedSolution.AverageProbabilityOfPassing.ToString() + "," +
                                                                      Record[i].RandomSolution.AverageProbabilityOfPassing.ToString() + "," +
                                                                      Record[i].DeltaPassingIntervewProbability.ToString() + "\n");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);


                        progress = 100 - (((NumberOfPeopleInterviewed - i) * 100) / NumberOfPeopleInterviewed);
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write("Outputing the Data to the File Has Started. Current Progress: %" + progress);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }

    
}