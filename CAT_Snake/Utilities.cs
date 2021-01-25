using System;

namespace CAT_Snake
{
    public static class Utilities
    {
        public static object[] XYZParse((int X, int Y) points, double Z = 0.0)
        {
            return new object[] { points.X * Globals.PieceLengthDouble, points.Y * Globals.PieceLengthDouble, Z };
        }
        public static (int X, int Y) XYParse(object[] CoordXYZ)
        {
            double X = (double)CoordXYZ[0] / Globals.PieceLengthDouble;
            double Y = (double)CoordXYZ[1] / Globals.PieceLengthDouble;
            return ((int)X, (int)Y);
        }
        public static void CastDoubleArray(ref object[] coordinates)
        {
            if (coordinates.Length != 3)
            {
                throw new Exception("Coordinates length not valid!");
            }
            for (int i = 0; i < 3; i++)
            {
                if (coordinates[i].GetType() == typeof(int))
                {
                    coordinates[i] = (int)coordinates[i] * 1.0;
                }
                else
                {
                    if (coordinates[i].GetType() != typeof(double))
                    {
                        throw new Exception("Coordinates type is not valid!");
                    }
                }
            }
        }        
    }
}
