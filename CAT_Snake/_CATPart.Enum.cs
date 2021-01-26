using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT_Snake
{
    public static partial class _CATPart
    {
        public enum CATPasteType
        {
            CATPrtCont, //"As Specified In Part Document"
            CATPrtResultWithOutLink, //As Result
            CATPrtResult //As result with link
        }
        public enum Axis
        {
            X,
            Y,
            Z
        }
    }
}
