using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ConsoleGame
{
    class Game
       {
        //==================================================== Global variables ========
        static int xPositionTank;                                   // Tank X position
        static int yPositionTank = (int)Console.WindowHeight - 2;   // Tank Y position
        static List<int[]> bomb = new List<int[]>();                // Shoot list
        static long score = 0;                                      // Score 
        static int speed = 0; // Speed of game
        static ConsoleKeyInfo key;
        private static long LevelScoore = 0;
        private static bool levelRunning = true;
        private static int currentLevel = 1;
        private static int Bullets = 0;
        private static int bulletCount = 0;
        private static System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        [STAThread]



        //==================================================== MAIN ====================
        static void Main()
        {
            MainMenu();
        
        }
        static void MainGame()
        {
            player.SoundLocation = "gamemusic.wav";
            while (currentLevel <= 3)
            {
               
                Countdown();
                //player.PlayLooping();
                Run(currentLevel);
                currentLevel++;
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Gongratz! You defeated the game!");
        }
        //======================================Main Menu ============================
        static void MainMenu()
        {

            int maxMenuItems = 2;
            int selector = 0;
            bool good = false;
            while (selector != maxMenuItems)
            {
                Console.Clear();
                DrawTitle();
                DrawMenu();
                good = int.TryParse(Console.ReadLine(), out selector);
                if (good)
                {
                    switch (selector)
                    {
                        case 1:
                            Console.Clear();
                            DificultySelect();


                            break;
                        case 2:
                            Environment.Exit(0);
                            break;

                        default:
                            if (selector != maxMenuItems)
                            {
                                ErrorMessage();
                            }
                            break;
                    }
                }
                else
                {
                    ErrorMessage();
                }
                Console.ReadKey();
            }
        }
        private static void ErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong Command");
        }

        private static void DrawTitle()
        {

            Console.WriteLine("*********************************************************************");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("XXXXXXXXXXXXXX");
            Console.WriteLine("XXXXXXXXXXXXXXX");
            Console.WriteLine("XXXX       XXXXX");
            Console.WriteLine("XXXX        XXXX");
            Console.WriteLine("XXXX        XXXX   xxxx  xxxx    xxxx    xxxxxxxx     xxxx   xxxx");
            Console.WriteLine("XXXX       XXXXX   xxxxxxxxxxxx  xxxx  xxxxxxxxxxxx   xxxx  xxxx");
            Console.WriteLine("XXXXXXXXXXXXXXX    xxxxxx    xx  xxxx xxxx     xxxx   xxxx xxxx");
            Console.WriteLine("XXXXXXXXXXXXXX     xxxxx              xxxx            xxxxxxxx");
            Console.WriteLine("XXXXXXXXXXXXXX     xxxx          xxxx xxxx            xxxxxxx");
            Console.WriteLine("XXXXXXXXXXXXXXX    xxxx          xxxx xxxx            xxxxxx");
            Console.WriteLine("XXXX       XXXXX   xxxx          xxxx xxxx            xxxxxx");
            Console.WriteLine("XXXX        XXXX   xxxx          xxxx xxxx            xxxxxxx");
            Console.WriteLine("XXXX        XXXX   xxxx          xxxx xxxx            xxxxxxxx");
            Console.WriteLine("XXXX       XXXXX   xxxx          xxxx xxxx     xxxx   xxxx xxxx");
            Console.WriteLine("XXXXXXXXXXXXXXX    xxxx          xxxx  xxxxxxxxxxxx   xxxx  xxxx");
            Console.WriteLine("XXXXXXXXXXXXXX     xxxx          xxxx    xxxxxxxx     xxxx   xxxx");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("       xxxx                xxxx      xxxxxxxx    xxx xxxx");
            Console.WriteLine("       xxxx                xxxx     xxxxxxxxxx   xxxxxxxxxx");
            Console.WriteLine("       xxxx                xxxx     xx      xx   xxxx    xx");
            Console.WriteLine("       xxxx                xxxx     xx      xx   xxx");
            Console.WriteLine("       xxxx     xxxxxxx    xxxx     xx      xx   xxx");
            Console.WriteLine("       xxxx    xxxxxxxxx   xxxx     xxxxxxxxxx   xxx");
            Console.WriteLine("        xxxx  xxxx   xxxx xxxx      xxxxxxxxxx   xxx");
            Console.WriteLine("         xxxxxxxx     xxxxxxx       xx      xx   xxx");
            Console.WriteLine("          xxxxxx       xxxxx        xx      xx   xxx");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ");
            Console.WriteLine("*********************************************************************");

        }
        private static void DrawMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" 1. Start Game");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("2. Exit Game");

        }

        public static void DificultySelect()
        {
            // Change to your number of menuitems.
            
            const int maxMenuItems = 3;
            int selector = 0;
            bool good = false;
            while (selector != maxMenuItems)
            {
                Console.Clear();
                DrawHead();

                DrawMenu2(maxMenuItems);
                good = int.TryParse(Console.ReadLine(), out selector);
                if (good)
                {
                    switch (selector)
                    {
                        case 1:
                            Bullets = 40;
                            MainGame();
                            break;
                        case 2:
                            Bullets = 55;
                            MainGame();
                            break;
                        case 3: 
                            Bullets = 1000;
                            MainGame();
                            break;
                        default:
                            if (selector != maxMenuItems)
                            {
                                Error();
                            }
                            break;
                    }
                }
                else
                {
                    Error();
                }
                Console.ReadKey();
            }
        }
        private static void Error()
        {
            Console.WriteLine("Typing error, press key to continue.");
        }

        private static void DrawHead()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("+++  Select Dificulty   +++");
            Console.WriteLine("***************************");
        }
        private static void DrawMenu2(int maxitems)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("***************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" 1. Hard (40 shots)");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" 2. Medium (55 shots)");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(" 3. Tourist (1000 shots)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("***************************");
            Console.WriteLine("Make your choice: type 1, 2,... or {0} for exit", maxitems);
            Console.WriteLine("***************************");
        }

        //==================================================== Runing task =============
        static void Run(int levelNumber)
        {
            Level level = new Level("level" + levelNumber + ".txt");
            Settings();
            Play();
            level.LoadLevel(); // loading the level from level.txt
            LevelScoore = level.GetAllPoints();
            while (levelRunning)
            {
                PrintingAndViewingTank();
                try
                {
                    level.Animate(); // method moving the bricks
                }
                catch (ArgumentOutOfRangeException )
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    int xWindow = 80;                               // Window length as parameter
                    int yWindow = 40;                               // Window high as parameter
                    Console.SetWindowSize(xWindow, yWindow);        // Window configuration
                    Console.SetBufferSize(xWindow, yWindow);        // Window buffer configuration
                }
                Shoots();
                ReadingKeys();
                Thread.Sleep(100 - speed);
            }
        }

        //==================================================== SETTINGS ================
        static void Settings()
        {
            bulletCount = Bullets;
            score = 0;
            bomb.Clear();
            levelRunning = true;
            int xWindow = 80;                               // Window length as parameter
            int yWindow = 40;                               // Window high as parameter
            Console.Title = "Console Game from Alice team"; // Logo of the console
            Console.CursorVisible = false;                  // Cursor unvisible
            Console.SetWindowSize(xWindow, yWindow);        // Window configuration
            Console.SetBufferSize(xWindow, yWindow);        // Window buffer configuration
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            xPositionTank = Console.WindowWidth / 2;        // First tank position 
            speed = 90;
        }

        //==================================================== Countdown before level start ============
        public static void Countdown()
        {
            for (int i = 5; i >= 1; i--)
            {
                Console.SetCursorPosition(1, 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Level " + currentLevel + " starts in: " + i + " seconds!");
                Thread.Sleep(1000);
            }
            Console.Clear();
        }

        //==================================================== Reading Keys ============
        static void ReadingKeys()
        {
            if (Console.KeyAvailable)
            {
                CheckGameOver();
                key = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                if (key.Key == ConsoleKey.LeftArrow)        // Pressed left arrow
                {
                    xPositionTank--;
                }
                if (key.Key == ConsoleKey.RightArrow)       // Pressed right arrow
                {
                    xPositionTank++;
                }
                if (key.Key == ConsoleKey.Spacebar)         // Pressed spacebar
                {
                    int[] i = new int[2];
                    i[0] = xPositionTank;
                    i[1] = yPositionTank - 2;
                    if (bomb.Count < 2)
                    {
                        bomb.Add(i);
                        bulletCount--;
                        Console.SetCursorPosition(0, 0);
                        Console.Write("Bullets left: {0:D2} ", bulletCount);
                    }
                }
            }
        }

        //==================================================== Check for bomb and brick collision ============
        private static void bombCollision()
        {

            List<int[]> bombsToRemove = new List<int[]>();
            foreach (var bombVar in bomb)
            {
                if (bombVar[1] > Level.Bricks.Length)
                {
                    continue;
                }
                for (int i = Level.Bricks.Length - 1; i >= 0; i--)
                {
                    for (int j = Level.Bricks[i].Length - 1; j >= 0; j--)
                    {
                        if (Level.Bricks[i][j] != null)
                        {
                            if (Level.Bricks[i][j].PositionY == bombVar[1] &&
                                bombVar[0] >= Level.Bricks[i][j].PositionX &&
                                bombVar[0] < Level.Bricks[i][j].PositionX + 5)
                            {
                                ConsoleColor currentBackgroundColor = Console.BackgroundColor;
                                ConsoleColor currentForegroundColor = Console.ForegroundColor;
                                Console.SetCursorPosition(Level.Bricks[i][j].PositionX - 1, Level.Bricks[i][j].PositionY); // Setting the cursor to the current brick position                                
                                Console.BackgroundColor = ConsoleColor.Black;  // setting the color to the brick's color
                                Console.ForegroundColor = ConsoleColor.Black;  // inside text color
                                Console.WriteLine("       ");
                                Console.BackgroundColor = currentBackgroundColor;
                                Console.ForegroundColor = currentForegroundColor;
                                score += Level.Bricks[i][j].Points;
                                Console.SetCursorPosition(Console.WindowWidth - ("Score: " + score).Length, 0);
                                Console.Write("Score: " + score);
                                if (Level.Bricks[i][j].brickText == "bombs")
                                    bulletCount += 20;
                                CheckBrickArray();
                                Level.Bricks[i][j] = null;
                                bombsToRemove.Add(bombVar);
                            }
                        }
                    }
                }
            }
            foreach (var bombToRemove in bombsToRemove)
            {
                bomb.Remove(bombToRemove);
            }
        }

        //==================================================== Checks wheather the bricks are destroyed ============
        public static void CheckBrickArray()
        {
            if (score == LevelScoore || bulletCount == 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Victory!");
                levelRunning = false;
            }
        }

        //==================================================== Checks if out of ammo ============
        public static void CheckGameOver()
        {

            if (bulletCount <= 0)
            {
                Console.Clear();
                Console.SetCursorPosition(5, Console.WindowHeight / 2);
                Console.WriteLine("Game Over! Score: " + score + "!");
                Thread.Sleep(2000);
                Environment.Exit(0);
                levelRunning = false;
            }
        }

        //==================================================== Moving tank =============
        static void Shoots()
        {
            if (bomb.Count != 0)
            {
                for (int i = 0; i < bomb.Count; i++)            // clear last position
                {
                    Console.SetCursorPosition(bomb[i][0], bomb[i][1]);
                    Console.WriteLine(" ");
                }
                for (int i = 0; i < bomb.Count; i++)            // Y++
                {
                    bomb[i][1]--;
                }

                if (bomb[0][1] > 0)                             // print in the new position
                {
                    for (int i = 0; i < bomb.Count; i++)
                    {
                        Console.SetCursorPosition(bomb[i][0], bomb[i][1]);
                        Console.WriteLine(":");
                    }
                }
                else
                {
                    bomb.RemoveAt(0);
                }
                bombCollision();
            }
        }

        //==================================================== Shooting ================
        static void PrintingAndViewingTank()
        {
            if (xPositionTank < 4)                                      // Left movement limiter 
            {
                xPositionTank = 4;                                      // Right movement limiter
            }
            if (xPositionTank > Console.WindowWidth - 5)
            {
                xPositionTank = Console.WindowWidth - 5;
            }
            string[] tankString = {   "  |  "  ,
                                      "( o )"  ,};
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xPositionTank - 3, yPositionTank - 1);
            Console.WriteLine(new string(' ', 7));
            Console.SetCursorPosition(xPositionTank - 2, yPositionTank - 1);
            Console.WriteLine(tankString[0]);
            Console.SetCursorPosition(xPositionTank - 3, yPositionTank);
            Console.WriteLine(new string(' ', 7));
            Console.SetCursorPosition(xPositionTank - 2, yPositionTank);
            Console.WriteLine(tankString[1]);
        }
        static void Play()
        {
            Music n = new Music();
            int volume = 80;
            n.Player(volume);
        }
    }
    public class Music
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, int bla);

        [STAThread]
        public void Player(int volume)
        {
            mciSendString("open \"" + @"..\..\music1.mp3" + "\" type mpegvideo alias MediaFile", null, 0, 0);

            mciSendString("play MediaFile REPEAT", null, 0, 0);
            // Volume  sound
            mciSendString(string.Concat("setaudio MediaFile volume to ", volume), null, 0, 0);
        }
    }
}