using HybridShapeTypeLib;
using System;

namespace CAT_Snake
{
    public static class Parse
    {
        public static object[] GetCATArrayCoordFromPt(Point point)
        {
            object[] coords = new object[3];
            point.GetCoordinates(coords);
            return coords;
        }
        public static object[] Int2dPtToCATArray((int X, int Y) pointCoord)
        {
            return new object[] { pointCoord.X *1.0, pointCoord.Y*1.0, 0.0 };
        }
        public static object[] Double3dPtToCATArray((double X, double Y, double Z) pointCoord)
        {
            return new object[] { pointCoord.X, pointCoord.Y, pointCoord.Z };
        }
        public static (int X, int Y) CATPtArrayToInt2d(object[] CoordXYZ)
        {
            double X = (double)CoordXYZ[0] / Globals.PieceLengthDouble;
            double Y = (double)CoordXYZ[1] / Globals.PieceLengthDouble;
            return ((int)X, (int)Y);
        }
        public static (double X, double Y, double Z)? CATPtArrayToDouble3d(object[] CoordXYZ)
        {
            if (CoordXYZ.Length != 3)
            {
                return null;
            }
            (double X, double Y, double Z) pointCoord = (0, 0, 0);
            try
            {
                pointCoord.X = (double)CoordXYZ[0];
                pointCoord.Y = (double)CoordXYZ[1];
                pointCoord.Z = (double)CoordXYZ[2];
            }
            catch (Exception)
            {
                return null;
            }
            return pointCoord;
        }
        public static (double X, double Y, double Z) GetDouble3dFromPt(Point point)
        {            
            return ((double X, double Y, double Z))CATPtArrayToDouble3d(GetCATArrayCoordFromPt(point));
        }
    }
}
