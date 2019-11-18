using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    // Class to design a brick
    class Brick
    {        
        public int PositionX { get; set; }  // Holding the current X coordinate of the brick
        public int PositionY { get; set; }  // Holding the current Y coordinate of the brick
        public ConsoleColor BrickColor { get; set; } // Holding the color of the brick
        public bool Direction { get; set; } // true to move Left, false to move Right
        public int Points { get; set; }  // Variable holding the points of a brick
        public string brickText = "";  // The text inside the brick (printing the points inside the brick)
        private ConsoleColor foregroundBrickColor;  //The color of the text inside the brick

        // Constructor of the Brick
        public Brick(int positionX, int positionY, ConsoleColor color, bool direction)
        {
            // Setting the position, color, movement direction, points etc.
            PositionX = positionX;
            PositionY = positionY;
            BrickColor = color;
            Direction = direction;
            switch (BrickColor)
            {                
                case ConsoleColor.Blue:
                    Points = 100;
                    brickText = " 100 ";
                    foregroundBrickColor = ConsoleColor.White;
                    break;               
                case ConsoleColor.DarkGreen:
                    Points = 200;
                    brickText = " 200 ";
                    foregroundBrickColor = ConsoleColor.White;
                    break;                
                case ConsoleColor.Red:
                    Points = 300;
                    brickText = " 300 ";
                    foregroundBrickColor = ConsoleColor.White;
                    break;                
                case ConsoleColor.Yellow:
                    Points = 400;
                    brickText = " 400 ";
                    foregroundBrickColor = ConsoleColor.Black;
                    break;
                case ConsoleColor.White:
                    Points = 0;
                    brickText = "bombs";
                    foregroundBrickColor = ConsoleColor.Black;
                    break;
            }
        }

        // Method moving the brick with one step
        public void MovingBrick()
        {
            //if Direction == true -> move the brick to left, else move the brick to right
            if (Direction)
            {
                Console.SetCursorPosition(PositionX, PositionY); // Setting the cursor to the current brick position
                Console.BackgroundColor = BrickColor;  // setting the color to the brick's color
                Console.ForegroundColor = foregroundBrickColor;  // inside text color
                Console.Write(brickText); // printing the brick
                Console.BackgroundColor = ConsoleColor.Black; // removing one space from the oposite direction
                Console.ForegroundColor = ConsoleColor.White;  // inside text color
                Console.SetCursorPosition(PositionX + 5, PositionY);
                Console.Write(' ');

                //if the brick hits the end, move it to the other side
                if (PositionX < 1)
                {
                    PositionX = Console.WindowWidth - 6;
                    for (int i = 0; i < 5; i++)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(i, PositionY);
                        Console.Write(' ');
                    }
                }
                // Move the brick with one space
                else
                {
                    PositionX = PositionX - 1;
                }
            }
            else
            {
                // Moving the brick to right
                Console.SetCursorPosition(PositionX, PositionY);
                Console.BackgroundColor = BrickColor;
                Console.ForegroundColor = foregroundBrickColor;
                Console.Write(brickText);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(PositionX - 1, PositionY);
                Console.Write(' ');
                if (PositionX > Console.WindowWidth - 6)
                {
                    for (int i = PositionX; i < PositionX + 5; i++)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(i, PositionY);
                        Console.Write(' ');
                    }
                    PositionX = 2;
                }
                else
                {
                    PositionX = PositionX + 1;
                }        
            }                    
        }
    }
}
