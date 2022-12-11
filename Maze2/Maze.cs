using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze2
{
    internal class Maze
    {
        public static int width = 30;
        public static int height = 30;

        public bool isUsual;//Usual means 0 is paths and 1 is walls

        public bool[][] maze = new bool[height][];// The maze
        public bool[][] wasHere = new bool[height][];
        public bool[][] correctPath = new bool[height][]; // The solution to the maze
        int startColumn = 0, startRow = 0;
        int endColumn = 29, endRow = 0;


        //True if Walls are 1 and paths are 0
        //Constructor for string maze
        public Maze(string maze, bool isUsual = true)
        {
            init();
            this.isUsual = isUsual;
            this.maze = transformStringIntoJaggedArray(maze);
            if (!isUsual)
            {
                reverseMaze();
            }
        }

        //Constructor for bool[][] maze
        public Maze(bool[][] maze, bool isUsual = true)
        {
            init();
            this.isUsual = isUsual;
            this.maze = maze;
            if (!isUsual)
            {
                reverseMaze();
            }
        }

        public bool tryToSolveMaze()
        {
            for (int i = 0; i < height; i++)
            {
                startRow = i;
                for (int j = 0; j < height; j++)
                {
                    endRow = j; //Row
                    bool result = solveMaze();
                    if (result)
                    {
                        return result;
                    }
                }
            }
            return false;
        }

        bool solveMaze()
        {
            //maze = generateMaze(); // Create Maze (false = path, true = wall)


            for (int row = 0; row < maze.Length; row++)
                // Sets boolean Arrays to default values
                for (int col = 0; col < maze[row].Length; col++)
                {
                    wasHere[row][col] = false;
                    correctPath[row][col] = false;
                }
            bool b = recursiveSolve(startRow, startColumn);
            // Will leave you with a boolean array (correctPath) 
            // with the path indicated by true values.
            // If b is false, there is no solution to the maze
            return b;
        }

        bool recursiveSolve(int x, int y)
        {
            //Wall is 1 and path is 0
            //if (x == endColumn && y == endRow) return true; // If you reached the end
            if (y == endColumn) return true; // If you reached the end
            if (maze[x][y] || wasHere[x][y]) return false;
            // If you are on a wall or already were here
            wasHere[x][y] = true;
            if (x != 0) // Checks if not on top edge
                if (recursiveSolve(x - 1, y))
                { //Top
                    correctPath[x][y] = true; // Sets that path value to true;
                    return true;
                }
            if (x != width - 1) // Checks if not on bottom edge
                if (recursiveSolve(x + 1, y))
                { // bottom
                    correctPath[x][y] = true;
                    return true;
                }
            if (y != 0)  // Checks if not on left edge
                if (recursiveSolve(x, y - 1))
                { // Left
                    correctPath[x][y] = true;
                    return true;
                }
            if (y != height - 1) // Checks if not on right edge
                if (recursiveSolve(x, y + 1))
                { // Right
                    correctPath[x][y] = true;
                    return true;
                }
            return false;
        }

        //jagged array init
        void init()
        {
            for (int i = 0; i < width; i++)
            {
                maze[i] = new bool[height];
                wasHere[i] = new bool[height];
                correctPath[i] = new bool[height];
            }
        }

        bool[][] transformStringIntoJaggedArray(string allLines)
        {
            string[] lines = allLines.Split('\n');

            for (int i = 0; i < height; i++)
            {
                string[] currentLine = lines[i].Trim().Split(',');
                for (int j = 0; j < width; j++)
                {
                    currentLine[j] = currentLine[j].Trim().Replace("{", String.Empty);//{{0  
                    currentLine[j] = currentLine[j].Trim().Replace("}", String.Empty);
                    maze[i][j] = convertToBool(currentLine[j]); //"0"
                }
            }

            return this.maze;
        }

        bool convertToBool(string c)
        {
            return c == "1";
        }

        //It prints the raw maze with correct path coloring
        //Use it for showing result
        public void printResult(bool[][] matrix)
        {
            int k = 0;
            int l = 0;
            if (!this.isUsual)
            {
                reverseMaze();
            }
            //Print it
            for (k = 0; k < width; k++)
            {
                for (l = 0; l < height; l++)
                {
                    if (matrix[k][l])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(Convert.ToInt16(this.maze[k][l]));
                    if (l == 29)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        public void printResultAsCoordinate()
        {
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (this.correctPath[i][j])
                    {
                        Console.WriteLine($"({j},{i})");
                    }
                }
            }
        }

        //Its prints the given matrix
        public static void printMatrix(bool[][] matrix)
        {
            int k = 0;
            int l = 0;
            for (k = 0; k < 30; k++)
            {
                for (l = 0; l < 30; l++)
                {
                    Console.Write(Convert.ToInt16(matrix[k][l]));
                    if (l == 29)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        //Reverse the maze if paths are 1 and walls are 0
        void reverseMaze()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    this.maze[i][j] = !this.maze[i][j];
                }
            }
        }

        public static bool[][] generateMaze()
        {   //1 is path
            Random rnd = new Random();
            bool[][] randomMaze = new bool[30][];
            for (int i = 0; i < 30; i++)
            {
                randomMaze[i] = new bool[30];
            }
            int firstEnterence = rnd.Next(0, 16);
            int secondEnterence = rnd.Next(16, 30);
            randomMaze = createMaze(randomMaze, firstEnterence);
            randomMaze = createMaze(randomMaze, secondEnterence);
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (randomMaze[i][j] == false)
                    {
                        randomMaze[i][j] = Convert.ToBoolean(rnd.Next(0,2));
                    }
                }
            }
            return randomMaze;
        }

        static bool[][] createMaze(bool[][] randomMaze, int enterence)
        {
            int row = enterence;
            int column = 0;
            bool isArrivedExit = false;
            randomMaze[row][0] = true;
            while (!isArrivedExit)
            {
                Random randomDirection = new Random();
                int direction = randomDirection.Next(0, 3);
                if (row == 29)
                {
                    direction = randomDirection.Next(0, 2);
                } else if (row == 0)
                {
                    direction = randomDirection.Next(0, 3);
                    if (direction == 1)
                    {
                        direction = 2;
                    }
                }
                switch (direction)
                {
                    case 0://Right
                        column++;
                        break;
                    case 1://Up
                        row--;
                        break;
                    case 2://Down
                        row++;
                        break;
                    default:
                        break;
                }
                if (column == 30)
                {
                    isArrivedExit = true;
                }
                else
                {
                    randomMaze[row][column] = true;
                }
            }
            return randomMaze;
        }

        public static string exportAsTxt(bool[][] maze)
        {
            string fileName = "Generated.txt";
            string mazeString;
            StreamWriter sw = new StreamWriter(fileName);
            sw.Write("{");
            for (int i = 0; i < 30; i++)
            {
                sw.Write("{");
                for (int j = 0; j < 30; j++)
                {
                    mazeString = Convert.ToString(Convert.ToInt16(maze[i][j]));
                    if (j < 29)
                    {
                        sw.Write("{0},", mazeString);
                    }
                    else
                    {
                        sw.Write(mazeString);
                    }
                }
                if (i < 29)
                {
                    sw.Write("},");
                    sw.WriteLine();
                }
                else
                {
                    sw.Write("}");
                }
            }
            sw.Write("}");
            sw.Close();
            string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return $"{currentPath}\\{fileName}";
        }

    }
}
