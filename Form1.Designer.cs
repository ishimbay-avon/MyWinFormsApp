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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label totalLabel;

        private System.Windows.Forms.DataGridView dataGridViewPay;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.DataGridView dataGridViewSalarys;
        private System.Windows.Forms.GroupBox groupBoxPay;
        private System.Windows.Forms.GroupBox groupBoxEmployees;
        private System.Windows.Forms.GroupBox groupBoxAddItem;
        private System.Windows.Forms.Label employeeDetailsLabel;

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.ComboBox cmbMonth;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblMonth;

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
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewPay = new System.Windows.Forms.DataGridView();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.dataGridViewSalarys = new System.Windows.Forms.DataGridView();
            this.groupBoxPay = new System.Windows.Forms.GroupBox();
            this.groupBoxEmployees = new System.Windows.Forms.GroupBox();
            this.groupBoxAddItem = new System.Windows.Forms.GroupBox();
            this.totalLabel = new System.Windows.Forms.Label();
            this.employeeDetailsLabel = new System.Windows.Forms.Label();

            this.SuspendLayout();

            this.button1.Location = new System.Drawing.Point(10, 10);
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.Text = "Преобразовать";
            this.button1.Click += new System.EventHandler(this.button1_Click);

            this.groupBoxPay.Location = new System.Drawing.Point(10, 60);
            this.groupBoxPay.Size = new System.Drawing.Size(420, 260);
            this.groupBoxPay.Text = "Данные из Data1.xml";
            this.groupBoxPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.totalLabel.Name = "totalAmountLabel";
            this.totalLabel.Location = new System.Drawing.Point(10, 230);
            this.totalLabel.Size = new System.Drawing.Size(400, 30);
            this.totalLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.totalLabel.ForeColor = Color.DarkGreen;

            this.dataGridViewPay.Location = new System.Drawing.Point(10, 25);
            this.dataGridViewPay.Size = new System.Drawing.Size(400, 190);
            this.dataGridViewPay.ReadOnly = true;
            this.dataGridViewPay.AllowUserToAddRows = false;
            this.dataGridViewPay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.groupBoxPay.Controls.Add(this.dataGridViewPay);
            this.groupBoxPay.Controls.Add(this.totalLabel);

            this.groupBoxEmployees.Location = new System.Drawing.Point(440, 60);
            this.groupBoxEmployees.Size = new System.Drawing.Size(370, 460);
            this.groupBoxEmployees.Text = "Результат XSLT-преобразования (Employees)";
            this.groupBoxEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            this.dataGridViewEmployees.Location = new System.Drawing.Point(10, 25);
            this.dataGridViewEmployees.Size = new System.Drawing.Size(350, 185);
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.AllowUserToAddRows = false;
            this.dataGridViewEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmployees.SelectionChanged += new EventHandler(this.dataGridViewEmployees_SelectionChanged);
            this.groupBoxEmployees.Controls.Add(this.dataGridViewEmployees);


            this.employeeDetailsLabel.Location = new System.Drawing.Point(10, 225);
            this.employeeDetailsLabel.Size = new System.Drawing.Size(420, 30);
            this.employeeDetailsLabel.Text = "Зарплаты выбранного сотрудника";
            this.employeeDetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.employeeDetailsLabel.ForeColor = Color.DarkGreen;
            this.groupBoxEmployees.Controls.Add(this.employeeDetailsLabel);

            this.dataGridViewSalarys.Location = new System.Drawing.Point(10, 255);
            this.dataGridViewSalarys.Size = new System.Drawing.Size(350, 185);
            this.dataGridViewSalarys.ReadOnly = true;
            this.dataGridViewSalarys.AllowUserToAddRows = false;
            this.dataGridViewSalarys.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSalarys.DataSource = null;
            this.groupBoxEmployees.Controls.Add(this.dataGridViewSalarys);

            //---------------------------------------------------------------

            this.txtName = new TextBox { Location = new System.Drawing.Point(100, 20), Width = 150 };
            this.txtSurname = new TextBox { Location = new System.Drawing.Point(100, 50), Width = 150 };
            this.txtAmount = new TextBox { Location = new System.Drawing.Point(100, 80), Width = 150 };

            this.cmbMonth = new ComboBox
            {
                Location = new System.Drawing.Point(100, 110),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.cmbMonth.Items.AddRange(new[] { "january", "february", "march", "april", "may", "june",
                                       "july", "august", "september", "october", "november", "december" });

            this.button2.Location = new System.Drawing.Point(100, 150);
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.Text = "Добавить";
            this.button2.Click += new System.EventHandler(this.button2_Click);

            this.lblName = new Label { Location = new System.Drawing.Point(10, 22), Width = 150, Text = "Имя" };
            this.lblSurname = new Label { Location = new System.Drawing.Point(10, 52), Width = 150, Text = "Фамилия" };
            this.lblAmount = new Label { Location = new System.Drawing.Point(10, 82), Width = 150, Text = "Сумма" };
            this.lblMonth = new Label { Location = new System.Drawing.Point(10, 112), Width = 150, Text = "Месяц" };

            this.groupBoxAddItem.Location = new System.Drawing.Point(10, 320);
            this.groupBoxAddItem.Size = new System.Drawing.Size(420, 200);
            this.groupBoxAddItem.Text = "Добавить Item в Data1.xml";
            this.groupBoxAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxAddItem.Controls.AddRange(new Control[] { txtName, txtSurname, txtAmount, cmbMonth, button2, lblName, lblSurname, lblAmount, lblMonth });
            this.groupBoxAddItem.Enabled = false;


            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxPay);
            this.Controls.Add(this.groupBoxAddItem);
            this.Controls.Add(this.groupBoxEmployees);

            this.ClientSize = new System.Drawing.Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "XSLT-преобразование";

            this.ResumeLayout(false);
        }

        //1) Запускает xslt-преобразование, написанное в задании I.
        private void button1_Click(object sender, EventArgs e)
        {
            payData = LoadPayFromXml("Data1.xml");

            decimal totalAmount = payData.Items.Sum(item => item.Amount);
            // 3) В исходный файл Data1.xml в элемент Pay дописывает атрибут, который отражает сумму всех amount
            payData.TotalAmount = totalAmount;

            var displayData = payData.Items.Select(item => new
            {
                Имя = item.Name,
                Фамилия = item.Surname,
                Сумма = item.Amount,
                Месяц = item.Month
            }).ToList();

            dataGridViewPay.DataSource = displayData;

            totalLabel.Text = $"Общая сумма всех выплат: {totalAmount:F2} руб.";

            SavePaysToXml(payData, "Data1.xml");

            //-----------------------------------------------------------------

            employeesData = GroupPayToEmployees(payData);

            var employeesDisplay = employeesData.EmployeeList.Select(emp => new
            {
                Имя = emp.Name,
                Фамилия = emp.Surname
            }).ToList();

            dataGridViewEmployees.DataSource = employeesDisplay;

            SaveEmployeesToXml(employeesData, "Employees.xml");

            this.groupBoxAddItem.Enabled = true;

            MessageBox.Show("Данные успешно преобразованы!", "Успех");
        }

        // 5) * (НЕ ОБЯЗАТЕЛЬНЫЙ ПУНКТ) Реализовать добавление в файл Data1.xml данных для строки item с возможность пересчета всех данных (с 1 по 4 пункты).
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите имя сотрудника", "Ошибка");
                    txtName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSurname.Text))
                {
                    MessageBox.Show("Введите фамилию сотрудника", "Ошибка");
                    txtSurname.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    MessageBox.Show("Введите сумму зарплаты", "Ошибка");
                    txtAmount.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbMonth.Text))
                {
                    MessageBox.Show("Выберите месяц", "Ошибка");
                    cmbMonth.Focus();
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text, out decimal amount))
                {
                    MessageBox.Show("Сумма должна быть числом", "Ошибка");
                    txtAmount.Focus();
                    txtAmount.SelectAll();
                    return;
                }

                if (amount <= 0)
                {
                    MessageBox.Show("Сумма должна быть положительным числом", "Ошибка");
                    txtAmount.Focus();
                    txtAmount.SelectAll();
                    return;
                }

                PayItem newItem = new PayItem
                {
                    Name = txtName.Text.Trim(),
                    Surname = txtSurname.Text.Trim(),
                    AmountString = amount.ToString("F2"),
                    Month = cmbMonth.Text
                };

                payData.Items.Add(newItem);

                payData.TotalAmount = payData.Items.Sum(item => item.Amount);

                SavePaysToXml(payData, "Data1.xml");

                MessageBox.Show("Данные успешно добавлены!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка");
            }
        }
        private void dataGridViewEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.CurrentRow != null &&
                dataGridViewEmployees.CurrentRow.DataBoundItem != null &&
                employeesData != null)
            {
                var selectedEmployee = dataGridViewEmployees.CurrentRow.DataBoundItem;
                var nameProperty = selectedEmployee.GetType().GetProperty("Имя");
                var surnameProperty = selectedEmployee.GetType().GetProperty("Фамилия");

                if (nameProperty != null && surnameProperty != null)
                {
                    string name = nameProperty.GetValue(selectedEmployee)?.ToString() ?? "";
                    string surname = surnameProperty.GetValue(selectedEmployee)?.ToString() ?? "";

                    var employee = employeesData.EmployeeList
                        .FirstOrDefault(emp => emp.Name == name && emp.Surname == surname);

                    if (employee != null)
                    {
                        var salaryDisplay = employee.Salaries.Select(salary => new
                        {
                            Месяц = salary.Month,
                            Сумма = salary.Amount
                        }).ToList();

                        dataGridViewSalarys.DataSource = salaryDisplay;

                        // Обновляем заголовок GroupBox
                        employeeDetailsLabel.Text = $"Зарплаты: {name} {surname}";
                    }
                }
            }
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

        public void SavePaysToXml(Pay pays, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Pay));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF8
            };

            using (var writer = XmlWriter.Create(filePath, settings))
            {
                serializer.Serialize(writer, pays, ns);
            }
        }

    }
}
