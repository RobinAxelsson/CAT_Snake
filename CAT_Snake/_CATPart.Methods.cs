using HybridShapeTypeLib;
using INFITF;
using MECMOD;
using System;
using System.Collections.Generic;

namespace CAT_Snake
{
    public static partial class _CATPart
    {
            public static void PrintAppInfo()
            {
                Window activeWindow = Catia.ActiveWindow;
                Console.WriteLine($"Catia Active Window: {activeWindow.get_Name()}");
                Console.WriteLine($"Catia Active Document: {Catia.ActiveDocument.get_Name()}");

                Console.WriteLine($"Open Windows({windows.Count}):");
                for (int i = 1; i <= windows.Count; i++)
                {
                    Console.WriteLine(windows.Item(i).get_Name());
                }
                Console.WriteLine();

                Console.WriteLine($"Views({activeWindow.Viewers.Count})");
                Viewers viewers = activeWindow.Viewers;

                for (int i = 1; i <= activeWindow.Viewers.Count; i++)
                {
                    Console.WriteLine(viewers.Item(i).get_Name());
                }
                Console.WriteLine();

                Console.WriteLine($"Documents in use({documents.Count}):");
                for (int i = 1; i <= documents.Count; i++)
                {
                    Console.WriteLine(((Document)documents.Item(i)).get_Name());
                }
                Console.WriteLine();
            }
            public static void PrintAllElements()
            {
                var selElements = SelectElementsByName();
                foreach (var item in selElements)
                {
                    AnyObject obj = (AnyObject)item.Value;
                    AnyObject parent = (AnyObject)obj.Parent;
                    Console.WriteLine($"{obj.get_Name()} (type={item.Type}) (Parent={parent.get_Name()})");
                    //string test = ((Collection)item.Parent).
                }
                Console.WriteLine();
            }
            public static void PrintException(Exception ex, ref int errorCount)
            {
                errorCount++;
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                Console.WriteLine("TargetSite: " + ex.TargetSite);
                Console.WriteLine("Inner Exception: " + ex.InnerException);
            }
            public static void ResetPartDocument()
            {
                Selection selection = partDocument.Selection;
                HybridBodies hybridBodies = _part.HybridBodies;
                Bodies bodies = _part.Bodies;
                int countHBs = hybridBodies.Count;
                while (countHBs > 0)
                {
                    selection.Clear();
                    selection.Add(hybridBodies.Item(1));
                    selection.Delete();
                    countHBs--;
                }
                Body newPartBody = bodies.Add();
                _part.MainBody = newPartBody;
                int countBs = bodies.Count;
                int iBs = 1;
                while (countBs > 1)
                {
                    selection.Clear();
                    Body body = bodies.Item(iBs);
                    if (body.get_Name() != _part.MainBody.get_Name())
                    {
                        selection.Add(body);
                        selection.Delete();
                        countBs--;
                    }
                    else
                    {
                        iBs++;
                    }
                }

                newPartBody.set_Name("PartBody");
                int errorCount = 0;
                AxisSystems axisSystems = _part.AxisSystems;
                try
                {
                    selection.Clear();
                    selection.Add(axisSystems.Item(2));
                    selection.Delete();
                }
                catch (Exception ex)
                {
                    PrintException(ex, ref errorCount);
                }
                _part.Update();
            }
            public static object[] GetCoordinates(Point point)
            {
                object[] coords = new object[3];
                point.GetCoordinates(coords);
                return coords;
            }
            public static Reference GetRefFromObject(AnyObject anyObject)
            {
                return _CATPart._part.CreateReferenceFromObject(anyObject);
            }
            public static HybridShapeDirection GetAxisDirection(Globals.Axis axisDir)
            {
                switch (axisDir)
                {
                    case Globals.Axis.X:
                        return _CATPart.hybridShapeFactory.AddNewDirection(_CATPart.AbsoluteAxisSystem.XAxisDirection);
                    case Globals.Axis.Y:
                        return _CATPart.hybridShapeFactory.AddNewDirection(_CATPart.AbsoluteAxisSystem.YAxisDirection);
                    case Globals.Axis.Z:
                        return _CATPart.hybridShapeFactory.AddNewDirection(_CATPart.AbsoluteAxisSystem.ZAxisDirection);
                    default:
                        return null;
                }
            }
            public static HybridBody ClearHybridBody(HybridBody hybridBody)
            {
                _CATPart.selection.Clear();
                string name = hybridBody.get_Name();
                _CATPart.selection.Add(hybridBody);
                _CATPart.selection.Delete();
                HybridBody newHybridBody = _CATPart.hybridBodies.Add();
                newHybridBody.set_Name(name);
                return newHybridBody;
            }
            public static List<SelectedElement> SelectElementsByName(string name = "")
            {
                var selElements = new List<SelectedElement>();
                selection.Clear();
                string searchString = $"Name={name}*, all";
                selection.Search(ref searchString);

                for (int i = 1; i <= selection.Count; i++)
                {
                    try
                    {
                        SelectedElement selElement = selection.Item(i);
                        selElements.Add(selElement);
                    }
                    catch (Exception)
                    {
                    }
                }
                selection.Clear();
                return selElements;
            }
            public static void RenameSelection(int index, string name)
            {
                int max = selection.Count;
                if (max == 0)
                {
                    throw new Exception("Nothing in selection");
                }
                if (!(index <= max && index >= 1))
                {
                    Console.WriteLine($"Wrong index of selection, type an new index between {1} and {max}");
                    index = int.Parse(Console.ReadLine());
                }
               ((AnyObject)selection.Item(index).Value).set_Name(name);
            }
            public static void HideAny(params AnyObject[] objs)
            {
                selection.Clear();
                foreach (var obj in objs)
                {
                    selection.Add(obj);
                }
                VisPropertySet objsVisProps = selection.VisProperties;
                objsVisProps.SetShow(CatVisPropertyShow.catVisPropertyNoShowAttr);
            }
            public static void CleanHybridBody(AnyObject[] exception, HybridBody hybridBody)
            {
                selection.Clear();

                if (exception != null)
                {
                    foreach (var obj in exception)
                    {
                        selection.Add(obj);
                    }
                    selection.Copy();
                }

                int countHB = hybridBody.HybridBodies.Count;
                while (countHB > 0)
                {
                    selection.Clear();
                    selection.Add(hybridBody.HybridBodies.Item(1));
                    selection.Delete();
                    countHB--;
                }
                if (exception != null) selection.Paste();
                _CATPart._part.Update();
            }
            //public static AnyObject PasteSpecial(AnyObject obj)
            //{
            //    selection.Clear();
            //    selection.Add(obj);
            //    SelectedElement selEl = selection.Item(1);
            //    string type = selEl.Type;
            //    selection.Copy();
            //    AnyObject explicitObj;
            //    if (type.Contains("HybridShape"))
            //    {
            //        if (hybridHome != null)
            //        {
            //            selection.Clear();
            //            selection.Add(hybridHome);
            //        }
            //        else
            //        {
            //            selection.Clear();
            //            selection.Add(hybridBodyResults);
            //        }
            //        selection.PasteSpecial("CATPrtResultWithOutLink");
            //        explicitObj = (AnyObject)selection.Item(1).selection;
            //        selection.Clear();
            //    }
            //    else //Shape assumed
            //    {
            //        selection.PasteSpecial("CATPrtResultWithOutLink");
            //        explicitObj = (AnyObject)selection.Item(1).selection;
            //        selection.Clear();
            //    }
            //    _part.Update();
            //    return explicitObj;
            //}
            // Dim params()
            //CATIA.SystemService.ExecuteScript"_part.CATPart", catScriptLibraryTypeDocument, "Macro1.catvbs", "CATMain", params
    }
}
