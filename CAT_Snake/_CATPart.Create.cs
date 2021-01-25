﻿using HybridShapeTypeLib;
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
            public static HybridShapeExtrude SimpleExtrude(object[] OriginPt, double y, double z, HybridBody hybridBody)
            {
                HybridShapeLinePtDir line1 = LinePtDir(OriginPt, Globals.Axis.Z, z, hybridBodyStream);
                Reference lineRef = GetRefFromObject(line1);
                var dir1 = GetAxisDirection(Globals.Axis.Y);
                HybridShapeExtrude extrude1 = hybridShapeFactory.AddNewExtrude(lineRef, y, 0.0, dir1);
                extrude1.SymmetricalExtension = false;
                hybridBody.AppendHybridShape(extrude1);
                _part.Update();
                return extrude1;
            }
            public static HybridShapeLinePtDir LinePtDir(object[] coord, Globals.Axis axisDir, double length, HybridBody hybridBody)
            {
                HybridBody trueHybridBody = hybridBody;
                var point = PointCoord(coord, hybridBody);
                var pointRef = GetRefFromObject(point);
                var dir = GetAxisDirection(axisDir);
                HybridShapeLinePtDir line = hybridShapeFactory.AddNewLinePtDir(pointRef, dir, 0.0, length, false);
                hybridBody.AppendHybridShape(line);
                _part.Update();
                return line;
            }
            public static HybridShapePointCoord PointCoord(object[] Coord, HybridBody hybridBody, AxisSystem axisSystem = null)
            {
                Utilities.CastDoubleArray(ref Coord);
                HybridShapePointCoord point1 = hybridShapeFactory.AddNewPointCoord((double)Coord[0], (double)Coord[1], (double)Coord[2]);
                if (axisSystem != null)
                {
                    point1.RefAxisSystem = GetRefFromObject(axisSystem);
                }
                hybridBody.AppendHybridShape(point1);
                _part.Update();
                return point1;
            }
            public static HybridShapeLinePtPt PtPtLine(object[] Coord1, object[] Coord2, HybridBody hybridBody)
            {
                var point1 = PointCoord(Coord1, hybridBody);
                var point2 = PointCoord(Coord2, hybridBody);
                Reference ref1 = GetRefFromObject(point1);
                Reference ref2 = GetRefFromObject(point2);
                HybridShapeLinePtPt line1 = hybridShapeFactory.AddNewLinePtPt(ref1, ref2);
                hybridBody.AppendHybridShape(line1);
                _part.Update();
                return line1;
            }
            public static AxisSystem _AxisSystem(Point originPoint, string name)
            {
                var coordRef = new object[3];
                originPoint.GetCoordinates(coordRef);
                var pointXdir = PointCoord(new object[] { (double)coordRef[0] + 1.0, coordRef[1], (double)coordRef[2] }, hybridBodyStream);
                var pointYdir = PointCoord(new object[] { (double)coordRef[0], (double)coordRef[1] + 1.0, (double)coordRef[2] }, hybridBodyStream);
                var pointZdir = PointCoord(new object[] { coordRef[0], coordRef[1], (double)coordRef[2] + 1.0 }, hybridBodyStream);
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
            public static void Cube(Body body, object[] originCoord, double length)
            {
                selection.Clear();
                _part.InWorkObject = body;
                var extrude1 = SimpleExtrude(originCoord, length, length, hybridBodyStream);
                var extrudeRef1 = GetRefFromObject(extrude1);
                ThickSurface thickSurface = shapeFactory.AddNewThickSurface(extrudeRef1, 0, 0.0, length);
                _part.Update();
            }
            public static Body GameCube(Point originPoint)
            {
                Body body = bodies.Add();
                _part.InWorkObject = body;
                var extrude1 = Create.SimpleExtrude(GetCoordinates(originPoint), Globals.PieceLengthDouble, Globals.PieceLengthDouble, hybridBodyStream);
                var extrudeRef1 = GetRefFromObject(extrude1);
                ThickSurface thickSurface = shapeFactory.AddNewThickSurface(extrudeRef1, 0, 0.0, Globals.PieceLengthDouble);
                _part.Update();

                return body;
            }
        }
    }
}