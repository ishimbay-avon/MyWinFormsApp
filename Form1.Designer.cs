using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MyWinFormsApp
{
    partial class Form1
    {
        private Pay payData;
        private Employees employeesData;

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.DataGridView dataGridViewPay;
        private System.Windows.Forms.GroupBox groupBoxPay;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // 4) Имеет GUI с кнопкой запуска программы и отображением списка всех Employee и сумму всех выплат (amount) по месяцам (mount).            
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewPay = new System.Windows.Forms.DataGridView();
            this.groupBoxPay = new System.Windows.Forms.GroupBox();

            this.SuspendLayout();

            this.button1.Location = new System.Drawing.Point(10, 10);
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.Text = "Преобразовать";
            this.button1.Click += new System.EventHandler(this.button1_Click);

            this.groupBoxPay.Location = new System.Drawing.Point(10, 60);
            this.groupBoxPay.Size = new System.Drawing.Size(420, 240);
            this.groupBoxPay.Text = "Данные из Data1.xml";
            this.groupBoxPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.dataGridViewPay.Location = new System.Drawing.Point(10, 25);
            this.dataGridViewPay.Size = new System.Drawing.Size(400, 190);
            this.dataGridViewPay.ReadOnly = true;
            this.dataGridViewPay.AllowUserToAddRows = false;
            this.dataGridViewPay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.groupBoxPay.Controls.Add(this.dataGridViewPay);

            //---------------------------------------------------------------

            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxPay);

            this.ClientSize = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "XSLT-преобразование";

            this.ResumeLayout(false);
        }

        //1) Запускает xslt-преобразование, написанное в задании I.
        private void button1_Click(object sender, EventArgs e)
        {
            payData = LoadPayFromXml("Data1.xml");

            var displayData = payData.Items.Select(item => new
            {
                Имя = item.Name,
                Фамилия = item.Surname,
                Сумма = item.Amount,
                Месяц = item.Month
            }).ToList();

            dataGridViewPay.DataSource = displayData;
          
            //-----------------------------------------------------------------

            employeesData = GroupPayToEmployees(payData);

            SaveEmployeesToXml(employeesData, "Employees.xml");

            MessageBox.Show("Данные успешно преобразованы!", "Успех");
        }

        public Pay LoadPayFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Pay));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                return (Pay)serializer.Deserialize(stream);
            }
        }

        public Employees GroupPayToEmployees(Pay payData)
        {
            var employees = new Employees
            {
                EmployeeList = payData.Items
                    .GroupBy(item => new { item.Name, item.Surname })
                    .Select(group => new Employee
                    {
                        Name = group.Key.Name,
                        Surname = group.Key.Surname,
                        Salaries = group.Select(item => new Salary
                        {
                            AmountString = item.AmountString,
                            Month = item.Month
                        }).ToList(),
                        //2) После xslt-преобразования, дописывает в элемент Employee атрибут, который отражает сумму всех amount/@salary этого элемента
                        SumTotal = group.Sum(item => item.Amount)
                    })
                    .ToList()
            };

            return employees;
        }

        public void SaveEmployeesToXml(Employees employees, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Employees));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF8
            };

            using (var writer = XmlWriter.Create(filePath, settings))
            {
                serializer.Serialize(writer, employees, ns);
            }
        }


    }
}
