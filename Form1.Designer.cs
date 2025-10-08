namespace MyWinFormsApp
{
    partial class Form1
    {
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
    }
}
