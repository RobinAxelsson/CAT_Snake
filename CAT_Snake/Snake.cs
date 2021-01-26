using HybridShapeTypeLib;
using PARTITF;
using INFITF;
using MECMOD;
using System;
using System.Collections.Generic;

namespace CAT_Snake
{
    public static partial class _CATPart
    {
        public class Snake
        {

            public static List<Snake> Snakes { get; set; } = new List<Snake>();
            public static Cube HelperCube { get; set; }
            public int Player { get; }
            public Body pieceBodyLink { get; }
            public Body SnakeBody { get; }
            public HybridShapePointCoord SpawnPoint { get; }
            public List<(int x, int y)> bodyCoord { get; set; }
            private Snake((int x, int y) spawnPoint)
            {
                Snakes.Add(this);
                Player = Snakes.FindIndex(x => x == this) + 1;
                SnakeBody = Create.Body($"Player.{Player}");
                SpawnPoint = Create.PointCoord((spawnPoint.x, spawnPoint.y, 0.0), hybridBodyStream);                
                _part.Update();
                selection.Clear();
                Body pieceCopy = CopyPasteBody(HelperCube.body, CATPasteType.CATPrtResult);
                //Body pieceCopy = (Body)_part.InWorkObject;
                _part.InWorkObject = pieceCopy;
                Translate translate1 = (Translate)shapeFactory.AddNewTranslate2(0.0);
                HybridShapeTranslate hybridTranslate1 = (HybridShapeTranslate)translate1.HybridShape;
                hybridTranslate1.VectorType = 1;
                hybridTranslate1.FirstPoint = GetRefFromObject(HelperCube.originPt);
                hybridTranslate1.SecondPoint = GetRefFromObject(SpawnPoint);
                _part.InWorkObject = hybridTranslate1;
                _part.Update();
                _part.InWorkObject = SnakeBody;
                shapeFactory.AddNewAdd(pieceCopy);
                _part.Update();
                _part.InWorkObject = pieceCopy;
            }
            public static void CreateSnakes(int players, Cube helperCube)
            {
                HelperCube = helperCube;
                Random rand = new Random();
                for (int i = 0; i < players; i++)
                {
                    int X = rand.Next(0, Globals.LengthXPieces);
                    int Y = rand.Next(0, Globals.LengthYPieces);
                    Snake s = new Snake((X, Y));
                }
            }
        }
    }
}
