using HybridShapeTypeLib;
using INFITF;
using MECMOD;
using PARTITF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT_Snake
{
    public static partial class _CATPart
    {
        public static class Create
        {
            public static Body Body(string name)
            {
                Body body = bodies.Add();
                body.set_Name(name);
                _part.Update();
                return body;
            }
            public static HybridShapeExtrude SimpleExtrude((double X, double Y, double Z) pointCoord, double y, double z, HybridBody hybridBody)
            {
                HybridShapeLinePtDir line1 = LinePtDir(pointCoord, Axis.Z, z, hybridBodyStream);
                Reference lineRef = GetRefFromObject(line1);
                var dir1 = GetAxisDirection(Axis.Y);
                HybridShapeExtrude extrude = hybridShapeFactory.AddNewExtrude(lineRef, y, 0.0, dir1);
                extrude.SymmetricalExtension = false;
                hybridBody.AppendHybridShape(extrude);
                _part.Update();
                return extrude;
            }
            public static HybridShapeLinePtDir LinePtDir((double X, double Y, double Z) pointCoord, Axis axisDir, double length, HybridBody hybridBody)
            {
                var point = PointCoord(pointCoord, hybridBody);
                var pointRef = GetRefFromObject(point);
                var dir = GetAxisDirection(axisDir);
                HybridShapeLinePtDir line = hybridShapeFactory.AddNewLinePtDir(pointRef, dir, 0.0, length, false);
                hybridBody.AppendHybridShape(line);
                _part.Update();
                return line;
            }
            public static HybridShapePointCoord PointCoord((double X, double Y, double Z) pointCoord, HybridBody hybridBody, AxisSystem axisSystem = null)
            {
                HybridShapePointCoord point1 = hybridShapeFactory.AddNewPointCoord(pointCoord.X, pointCoord.Y, pointCoord.Z);
                if (axisSystem != null)
                {
                    point1.RefAxisSystem = GetRefFromObject(axisSystem);
                }
                hybridBody.AppendHybridShape(point1);
                _part.Update();
                return point1;
            }
            public static HybridShapeLinePtPt PtPtLine((double X, double Y, double Z) pointCoord1, (double X, double Y, double Z) pointCoord2, HybridBody hybridBody)
            {
                var point1 = PointCoord(pointCoord1, hybridBody);
                var point2 = PointCoord(pointCoord2, hybridBody);
                Reference ref1 = GetRefFromObject(point1);
                Reference ref2 = GetRefFromObject(point2);
                HybridShapeLinePtPt line = hybridShapeFactory.AddNewLinePtPt(ref1, ref2);
                hybridBody.AppendHybridShape(line);
                _part.Update();
                return line;
            }
            public static AxisSystem _AxisSystem(Point originPoint, string name)
            {
                var pointCoord = Parse.GetDouble3dFromPt(originPoint);
                pointCoord.X += 1.0;
                var pointXdir = PointCoord(pointCoord, hybridBodyStream);
                pointCoord.X -= 1.0;
                pointCoord.Y += 1.0;
                var pointYdir = PointCoord(pointCoord, hybridBodyStream);
                pointCoord.Y -= 1.0;
                pointCoord.Z += 1.0;
                var pointZdir = PointCoord(pointCoord, hybridBodyStream);
                
                AxisSystem axisSystem = axisSystems.Add();
                axisSystem.set_Name(name);
                axisSystem.Type = CATAxisSystemMainType.catAxisSystemStandard;
                axisSystem.OriginType = CATAxisSystemOriginType.catAxisSystemOriginByPoint;
                axisSystem.OriginPoint = GetRefFromObject(originPoint);
                axisSystem.XAxisType = CATAxisSystemAxisType.catAxisSystemAxisByCoordinates;
                axisSystem.XAxisDirection = GetRefFromObject(pointXdir);
                axisSystem.YAxisType = CATAxisSystemAxisType.catAxisSystemAxisByCoordinates;
                axisSystem.YAxisDirection = GetRefFromObject(pointYdir);
                axisSystem.ZAxisType = CATAxisSystemAxisType.catAxisSystemAxisByCoordinates;
                axisSystem.ZAxisDirection = GetRefFromObject(pointZdir);
                _part.Update();

                return axisSystem;

            }
            public static void Cube(Body body, (double X, double Y, double Z) coord, double length)
            {
                selection.Clear();
                _part.InWorkObject = body;
                var extrude1 = SimpleExtrude(coord, length, length, hybridBodyStream);
                var extrudeRef1 = GetRefFromObject(extrude1);
                ThickSurface thickSurface = shapeFactory.AddNewThickSurface(extrudeRef1, 0, 0.0, length);
                _part.Update();
            }
            public static Body GameCube(Point originPoint)
            {
                Body body = bodies.Add();
                _part.InWorkObject = body;
                var extrude1 = Create.SimpleExtrude(Parse.GetDouble3dFromPt(originPoint), Globals.PieceLengthDouble, Globals.PieceLengthDouble, hybridBodyStream);
                var extrudeRef1 = GetRefFromObject(extrude1);
                ThickSurface thickSurface = shapeFactory.AddNewThickSurface(extrudeRef1, 0, 0.0, Globals.PieceLengthDouble);
                _part.Update();

                return body;
            }
        }
    }
}
