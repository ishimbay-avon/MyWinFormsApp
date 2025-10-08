using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyWinFormsApp
{
    [XmlRoot("Pay")]
    public class Pay
    {
        [XmlElement("item")]
        public List<PayItem> Items { get; set; }= [];

        [XmlAttribute("totalAmount")]
        public decimal TotalAmount { get; set; }
    }

    public class PayItem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }= string.Empty;

        [XmlAttribute("surname")]
        public string Surname { get; set; }= string.Empty;

        [XmlAttribute("amount")]
        public string AmountString { get; set; }= string.Empty;

        [XmlIgnore]
        public decimal Amount 
        { 
            get 
            {
                string amount = AmountString?.Replace(".", ",") ?? "0";
                return decimal.Parse(amount);
            }
        }

        [XmlAttribute("mount")]
        public string Month { get; set; }= string.Empty;
    }
}
