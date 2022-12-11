using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze2
{
    class Program
    {

        //CTRL K D
        public static void Main(string[] args)
        {
            ////Given maze solver
            //Maze maze = new Maze(readMatrixFromTxt("Maze.txt"));
            //bool result = maze.tryToSolveMaze();
            //maze.printResult(maze.correctPath);
            //Console.WriteLine(result);

            ////Maze generator and printer
            //bool[][] asd = Maze.generateMaze();
            //Maze.exportAsTxt(asd);
            //Maze.printMatrix(asd);

            //Randomly generated maze solver
            //Maze maze = new Maze(Maze.generateMaze(), false);
            //bool result = maze.tryToSolveMaze();
            //maze.printResult(maze.correctPath);
            //Console.WriteLine(result);

            Console.Title = "Muhammed Bedii Yürek - 1030521129";
            printCommandList();
            bool isExit = false;
            while (!isExit)
            {
                Console.Write("Enter your command: ");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "h":
                    case "help":
                        printCommandList();
                        break;
                    case "1":
                        //Maze generator and printer
                        bool[][] asd = Maze.generateMaze();
                        Maze.printMatrix(asd);
                        Console.WriteLine(Maze.exportAsTxt(asd));
                        break;
                    case "2":
                        //Given maze solver
                        Console.WriteLine("Your file has to be same location  with program");
                        Console.Write("Type your char for path: ");
                        string pathChar = Console.ReadLine();
                        Console.Write("Type your file name: ");
                        string path = Console.ReadLine();
                        string checkSuffix = "";
                        try
                        {
                            checkSuffix = path.Substring(path.Length - 4);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                        }
                        
                        //Add .txt if path dont contains suffix of .txt
                        if (checkSuffix != ".txt")
                        {
                            path += ".txt";
                        }
                        bool isUsual = pathChar == "0" ? true : false;
                        
                        Maze maze = new Maze(readMatrixFromTxt(path), isUsual);
                        bool result = maze.tryToSolveMaze();
                        maze.printResultAsCoordinate();
                        maze.printResult(maze.correctPath);
                        printResut(result);
                        break;

                    case "3":
                        //Randomly generated maze solver
                        Maze maze2 = new Maze(Maze.generateMaze(), false);
                        bool result2 = maze2.tryToSolveMaze();
                        maze2.printResult(maze2.correctPath);
                        printResut(result2);
                        break;
                    case "cls":
                    case "clear":
                        Console.Clear();
                        break;
                    case "e":
                    case "exit":
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }

        static void printResut(bool result)
        {
            Console.Write($"Solution Result: ");
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void printCommandList()
        {
            Console.WriteLine("Type 1 for generate maze");
            Console.WriteLine("Type 2 for solve maze");
            Console.WriteLine("Type 3 for generate and solve generated maze");
            Console.WriteLine("Type cls or clear for clear console");
            Console.WriteLine("Type e or exit for end program");
            Console.WriteLine("Type help or h for see command list");
        }

        public static string readMatrixFromTxt(string path = @"Lab1Yol1.txt")
        {
            StreamReader sr = new StreamReader(path);
            string allLines = sr.ReadToEnd();
            sr.Close();
            return allLines;
        }
    }
}