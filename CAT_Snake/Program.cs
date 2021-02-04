using HybridShapeTypeLib;
using MECMOD;
using PARTITF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CAT_Snake
{
    using static _CATPart;
    class Program
    {
        [DllImport("NativeLib.dll")]
        public static extern void HelloWorld();

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, HIDE);
            InitGameSetUp();
            //PrintAllElements();
            //Console.WriteLine($"Finnished with {Globals.errorCount} errors!");
            //Console.ReadLine();

        }
        public static void InitGameSetUp()
        {
            var helperCube = new Cube("helperCube", (0, 0, 100));
            //Body bodyCopy = CopyPasteBody(helperCube.body, CATPasteType.CATPrtResult);
            Random rand = new Random();
            int X = rand.Next(0, Globals.LengthXPieces);
            int Y = rand.Next(0, Globals.LengthYPieces);
            Snake snake = new Snake((X, Y), helperCube);
            //snakeZigZag(snake, 5, 10);
            snakeRandom(snake, 10);
            //for (int i = 0; i < 10; i++)
            //{
            //    snake.UpdateBody();
            //}
            //snake.TurnRight();
            //for (int i = 0; i < 10; i++)
            //{
            //    snake.UpdateBody();
            //}

        }
        public static void snakeRandom(Snake snake, int turns)
        {
            Random rand = new Random();
            int steps;
            for (int i = 0; i < turns; i++)
            {
                steps = rand.Next(1, 10);
                for (int j = 0; j < steps; j++)
                {
                    snake.UpdateBody();
                }
                if (steps % 2 == 0) snake.TurnRight();
                else snake.TurnLeft();
            }


        }
        public static void snakeZigZag(Snake snake, int leg, int turns)
        {
            for (int i = 0; i < turns; i++)
            {
                for (int j = 0; j < leg; j++)
                {
                    snake.UpdateBody();
                }
                if (i % 2 == 0) snake.TurnRight();
                else snake.TurnLeft();
            }
        }
    }
}
