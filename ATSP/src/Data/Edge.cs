using System;
using System.Globalization;

namespace ATSP.Data
{
    public class Edge
    {
        [System.Xml.Serialization.XmlText(typeof(int))]
        public int ID {get;set;}

        [System.Xml.Serialization.XmlAttribute("cost")]
        public string CostFormatted
        {
            get
            {
                return this.Cost.ToString();
            }

            set
            {
                this.Cost = Decimal.Parse(value, NumberStyles.Float | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public decimal Cost { get; set; }
    }
}