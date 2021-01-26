using HybridShapeTypeLib;
using INFITF;
using MECMOD;
using PARTITF;
using System;
using System.Collections.Generic;

namespace CAT_Snake
{
    public static partial class _CATPart
    {
        public class Cube
        {

            public Body body { get; private set; }
            public Point originPt { get; private set; }
            public double length { get; private set; }
            public Cube(string name, (double X, double Y, double Z) pointCoord)
            {
                body = Create.Body(name);
                originPt = Create.PointCoord(pointCoord, hybridBodyStream);
                _part.InWorkObject = body;
                var extrude = Create.SimpleExtrude(Parse.GetDouble3dFromPt(originPt), Globals.PieceLengthDouble, Globals.PieceLengthDouble, hybridBodyStream);
                var extrudeRef1 = GetRefFromObject(extrude);
                shapeFactory.AddNewThickSurface(extrudeRef1, 0, 0.0, Globals.PieceLengthDouble);
                _part.Update();
            }
        }
        //public static void Cube(Body body, (double X, double Y, double Z) coord, double length)
        //{
        //    selection.Clear();
        //    _part.InWorkObject = body;
        //    var extrude1 = SimpleExtrude(coord, length, length, hybridBodyStream);
        //    var extrudeRef1 = GetRefFromObject(extrude1);
        //    ThickSurface thickSurface = shapeFactory.AddNewThickSurface(extrudeRef1, 0, 0.0, length);
        //    _part.Update();
        //}
    }
}
