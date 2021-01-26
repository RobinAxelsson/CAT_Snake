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
            ShowWindow(ThisConsole, MINIMIZE);
            InitGameSetUp();
            PrintAllElements();
            //try
            //{
            //    Snake.CreateSnakes(6);
            //}
            //catch (Exception ex)
            //{
            //    PrintException(ex, ref Globals.errorCount);
            //    ShowWindow(ThisConsole, RESTORE);
            //}
            //PrintAllElements();
            Console.WriteLine($"Finnished with {Globals.errorCount} errors!");
            Console.ReadLine();
        }
        public static void InitGameSetUp()
        {
            HybridShapePointCoord HelperOriginPt = Create.PointCoord(( 0, 0, 50 ), hybridBodyStream);
            Body helperCube = Create.Body("helperCube");
            
            AxisSystem HelperAxisSystem = Create._AxisSystem(HelperOriginPt, "HelperAxisSystem");

            HybridShapePointCoord OriginPoint = Create.PointCoord((0, 0, 0), hybridBodyStream);
            OriginPoint.set_Name("OriginPoint");
            selection.Clear();
        }

        
    }
}
