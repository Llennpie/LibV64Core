using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibV64Core
{
    public class Types
    {
        #region Color Codes
        public struct ColorCode
        {
            public ColorPart Shirt;
            public ColorPart Overalls;
            public ColorPart Gloves;
            public ColorPart Shoes;
            public ColorPart Skin;
            public ColorPart Hair;
        }
        public struct ColorPart
        {
            public Light Main;
            public Light Shading;
        }
        public struct Light
        {
            public int R;
            public int G;
            public int B;
        }
        #endregion
    }
}
