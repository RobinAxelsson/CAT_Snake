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
            public enum SnakeDirection
            {
                negX,
                posY,                
                posX,
                negY
            }
            public static List<Snake> Snakes { get; set; } = new List<Snake>();
            public static Cube HelperCube { get; set; }
            public int Player { get; }
            public SnakeDirection Direction { get; private set; }
            public Body pieceBodyLink { get; }
            public Body SnakeBody { get; }
            public List<(int X, int Y)> bodyCoord { get; set; } = new List<(int X, int Y)>();
            public Snake((int X, int Y) spawnPoint, Cube helperCube)
            {

                HelperCube = helperCube;
                Direction = SnakeDirection.posY;
                Snakes.Add(this);
                Player = Snakes.FindIndex(x => x == this) + 1;
                SnakeBody = Create.Body($"Player.{Player}");
                bodyCoord.Add(spawnPoint);
                _part.Update();
                selection.Clear();
                UpdateBody(true);
            }
            public void UpdateBody(bool init = false)
            {
                var spawnPoint = bodyCoord[bodyCoord.Count-1];
                
                if (!init)
                {
                    switch (Direction)
                    {
                        case SnakeDirection.posX:
                            spawnPoint.X++;
                            break;
                        case SnakeDirection.posY:
                            spawnPoint.Y++;
                            break;
                        case SnakeDirection.negX:
                            spawnPoint.X--;
                            break;
                        case SnakeDirection.negY:
                            spawnPoint.Y--;
                            break;
                        default:
                            break;
                    }
                    bodyCoord.Add((spawnPoint.X, spawnPoint.Y));
                }
                HybridShapePointCoord SpawnPoint = Create.PointCoord((spawnPoint.X * Globals.PieceLengthDouble, spawnPoint.Y * Globals.PieceLengthDouble, 0.0), hybridBodyStream);
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
                _part.InWorkObject = SnakeBody;
            }
            public void TurnRight()
            {
                int x = (int)Direction;
                if (x != 3)
                {
                    x++;
                }
                else
                {
                    x = 0;
                }
                Direction = (SnakeDirection)x;
            }
            public void TurnLeft()
            {
                int x = (int)Direction;
                if (x != 0)
                {
                    x--;
                }
                else
                {
                    x = 0;
                }
                Direction = (SnakeDirection)x;
            }

        }
    }
}
