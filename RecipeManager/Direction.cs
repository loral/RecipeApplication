using System;
using System.Windows;
using System.Xml;

namespace RecipeManager
{
    public class Direction
    {
        private string _direction;

        public string direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = XmlConvert.EncodeLocalName(value);
            }
        }

        public string decodedDirection
        {
            get
            {
                return XmlConvert.DecodeName(_direction);
            }
        }
    }
}
