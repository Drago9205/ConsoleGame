using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    //Class to design the level of the game
    class Level
    {       
        public string FileName { get; set; }  // string to load the file
        public static Brick[][] Bricks { get; set; } // jugged array with Brick objects

        // Constructor
        public Level(string fileName)
        {
            FileName = fileName;            
        }

        // Method to move the Bricks
        public void Animate()
        {
            foreach (var arrayOfBricks in Bricks)
            {
                foreach (var brick in arrayOfBricks)
                {                    
                    if (brick != null)
                    {
                        brick.MovingBrick();                     
                    }
                }
            }
            //System.Threading.Thread.Sleep(100);           
        }

        public long GetAllPoints()
        {
            long totalLevelPoints = 0;
            foreach (var brickLine in Bricks)
            {
                if (brickLine != null)
                {
                    foreach (var brick in brickLine)
                    {
                        if (brick != null)
                            totalLevelPoints += brick.Points;
                    }
                }
            }
            return totalLevelPoints;
        }
        // Method that fill the jugged array of Brick objects 
        public void LoadLevel()
        {
            StreamReader reader = new StreamReader(FileName); // StreamReader to read the file
            List<string> fileLines = new List<string>();  // List holding each line of the file
            using (reader)
            {
                try
                {
                    string line = reader.ReadLine(); // Reading the first line
                    while (line != null)
                    {
                        fileLines.Add(line); // Adding the line to the list
                        line = reader.ReadLine(); // Reading the next line
                    }
                }
                catch (IOException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                catch (OutOfMemoryException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
            Bricks = new Brick[fileLines.Count][]; // Create the jugged array
            for (int line = 0; line < fileLines.Count; line++)
            {
                int bricksCount = 0; // Counter of the bricks in a line
                bool direction = true;  // Variable holding the movement direction of Bricks in a line

                // Getting the value from the file
                if (fileLines[line].Length != 0 && fileLines[line][0] == 'l')
                {
                    direction = true;
                }
                else if (fileLines[line].Length != 0 && fileLines[line][0] == 'r')
                {
                    direction = false;
                }

                //Create the arrays of the jugged array
                Bricks[line] = new Brick[fileLines[line].Length];
                
                for (int i = 0; i < fileLines[line].Length; i++)
                {
                    //Initialize the jugged array of Brick ojects with objects
                    // * == Blue brick
                    // @ == Yellow brick
                    // + == Red brick
                    // - == DarkGreen brick
                    if (fileLines[line][i] == '*')
                    {
                        Bricks[line][bricksCount] = new Brick(i * 5, line, ConsoleColor.Blue,direction);
                        bricksCount++;
                    }
                    if (fileLines[line][i] == '@')
                    {
                        Bricks[line][bricksCount] = new Brick(i * 5, line, ConsoleColor.Yellow,direction);
                        bricksCount++;
                    }
                    if (fileLines[line][i] == '%')
                    {
                        Bricks[line][bricksCount] = new Brick(i * 5, line, ConsoleColor.White, direction);
                        bricksCount++;
                    }
                    if (fileLines[line][i] == '+')
                    {
                        Bricks[line][bricksCount] = new Brick(i * 5, line, ConsoleColor.Red,direction);
                        bricksCount++;
                    }
                    if (fileLines[line][i] == '-')
                    {
                        Bricks[line][bricksCount] = new Brick(i * 5, line, ConsoleColor.DarkGreen,direction);
                        bricksCount++;
                    }
                }
            }   
        }
    }
}
