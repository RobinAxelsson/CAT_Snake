using HybridShapeTypeLib;
using INFITF;
using MECMOD;
using PARTITF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CAT_Snake
{
    public static partial class _CATPart
    {
        static _CATPart()
        {
            Catia = (Application)Marshal.GetActiveObject("Catia.Application");
            documents = Catia.Documents;
            windows = Catia.Windows;

            Document document = (Document)Catia.ActiveDocument;
            var file = new FileInfo(document.FullName);
            string ext = file.Extension;
            if (ext == ".CATPart")
            {
                partDocument = (PartDocument)Catia.ActiveDocument;
                _part = partDocument.Part;
                ResetPartDocument();
            }
            else
            {
                partDocument = (PartDocument)documents.Add("Part");
                partDocument.Activate();
                _part = partDocument.Part;
            }
            hybridShapeFactory = (HybridShapeFactory)_part.HybridShapeFactory;
            hybridBodies = _part.HybridBodies;
            axisSystems = _part.AxisSystems;
            AbsoluteAxisSystem = _part.AxisSystems.Item(1);
            selection = partDocument.Selection;
            hybridBodyStream = hybridBodies.Add();
            hybridBodyStream.set_Name("hybridBodyStream");
        }
        public static Application Catia { get; private set; }
        public static Documents documents { get; private set; }
        public static Windows windows { get; private set; }
        public static PartDocument partDocument { get; private set; }
        public static Part _part;
        public static Selection selection { get; private set; }
        public static AxisSystems axisSystems { get; private set; }
        public static HybridShapeFactory hybridShapeFactory { get; private set; }
        public static ShapeFactory shapeFactory { get; private set; }
        public static HybridBodies hybridBodies { get; private set; }
        public static Bodies bodies { get; private set; }
        public static AxisSystem AbsoluteAxisSystem { get; set; }
        public static Window activeWindow { get; set; }
        public static HybridBody hybridBodyStream { get; set; }

    }
}
