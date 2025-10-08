using System.Xml.Serialization;

namespace MyWinFormsApp
{
    [XmlRoot("Employees")]
    public class Employees
    {
        [XmlElement("Employee")]
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();
    }

    [XmlType("Employee")]
    public class Employee
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("surname")]
        public string Surname { get; set; } = string.Empty;

        [XmlElement("salary")]
        public List<Salary> Salaries { get; set; } = new List<Salary>();

        [XmlAttribute("sumTotal")]
        public decimal SumTotal { get; set; }
    }

    public class Salary
    {
        [XmlAttribute("amount")]
        public string AmountString { get; set; } = string.Empty;

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
        public string Month { get; set; } = string.Empty;
    }
}