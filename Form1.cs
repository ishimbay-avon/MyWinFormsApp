using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace MyWinFormsApp;

public partial class Form1 : Form
{
    private Pay payData;
    private Employees employeesData;

    private string inputFileName = "Data1.xml";
    private string outputFileName = "Employees.xml";

    public Form1()
    {
        InitializeComponent();

        payData = new Pay();
        employeesData = new Employees();
    }

    //1) Запускает xslt-преобразование, написанное в задании I.
    private void button1_Click(object sender, EventArgs e)
    {
        LoadDataFromXML();
    }

    private void LoadDataFromXML()
    {
        payData = LoadPayFromXml(inputFileName);

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


        SavePaysToXml(payData, inputFileName);

        //-----------------------------------------------------------------

        employeesData = GroupPayToEmployees(payData);

        var employeesDisplay = employeesData.EmployeeList.Select(emp => new
        {
            Имя = emp.Name,
            Фамилия = emp.Surname
        }).ToList();

        dataGridViewEmployees.DataSource = employeesDisplay;

        SaveEmployeesToXml(employeesData, outputFileName);

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

            SavePaysToXml(payData, inputFileName);

            MessageBox.Show("Данные успешно добавлены!", "Успех");

            LoadDataFromXML();
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
            //return (Pay)serializer.Deserialize(stream);
            var result = serializer.Deserialize(stream) as Pay;
            return result ?? throw new InvalidOperationException("Не удалось десериализовать XML файл");
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
