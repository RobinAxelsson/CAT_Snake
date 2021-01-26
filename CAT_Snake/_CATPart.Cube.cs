using HybridShapeTypeLib;
using INFITF;
using MECMOD;
using System;
using System.Collections.Generic;

namespace CAT_Snake
{
        public class Cube
        {
            public Body body { get; private set; }
            public Point originPt { get; private set; }
            public double length { get; private set; }
            public Cube(string name, (double X, double Y, double Z) pointCoord, double length)
            {
            }
        //return new object[] { points.X * Globals.PieceLengthDouble, points.Y * Globals.PieceLengthDouble, Z };

    }

}
