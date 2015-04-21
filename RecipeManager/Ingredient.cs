using System;
using System.Windows;
using System.Xml;
namespace RecipeManager
{
    [Serializable()]
    public class Ingredient
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = XmlConvert.EncodeLocalName(value);
            }
        }

        public string DecodedName
        {
            get
            {
                return XmlConvert.DecodeName(_name);
            }
        }

        private string _quantity;

        public string Quanity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = XmlConvert.EncodeLocalName(value);
            }
        }

        public string DecodedQuantity
        {
            get
            {
                return XmlConvert.DecodeName(_quantity);
            }
        }

        private string _unit;

        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = XmlConvert.EncodeLocalName(value);
            }
        }

        public string DecodedUnit
        {
            get
            {
                return XmlConvert.DecodeName(_unit);
            }
        }
    }
}
