using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova_BD
{
    public partial class AddEditEmployeeForm : Form
    {
        private int? _employeeId = null;
        private MySqlConnection conn;
        private readonly string _cs =
            "Server=127.0.0.1;Port=3306;Database=bakery_db_clean;User Id=root;Password=VFRcbvrf2007;SslMode=Preferred;AllowPublicKeyRetrieval=True;";

        public AddEditEmployeeForm()
        {
            InitializeComponent();
            ConfigureAddMode();
        }
        public AddEditEmployeeForm(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadEmployee();
            ConfigureEditMode();
        }
        private void ConfigureAddMode()
        {
            Text = "Додати працівника";
            dtpFiredDate.Enabled = false;
            dtpFiredDate.Checked = false;
        }
        private void ConfigureEditMode()
        {
            Text = "Редагувати працівника";
            dtpFiredDate.Enabled = true;
        }
        private void LoadEmployee()
        {
            using (MySqlConnection conn = new MySqlConnection(_cs))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    @"SELECT full_name, position, phone, hire_date, fired_date
              FROM employees
              WHERE employee_id = @id", conn);

                cmd.Parameters.AddWithValue("@id", _employeeId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtFullName.Text = reader["full_name"].ToString();
                        txtPosition.Text = reader["position"].ToString();
                        txtPhone.Text = reader["phone"].ToString();
                        dtpHireDate.Value = Convert.ToDateTime(reader["hire_date"]);

                        if (reader["fired_date"] != DBNull.Value)
                        {
                            dtpFiredDate.Checked = true;
                            dtpFiredDate.Value = Convert.ToDateTime(reader["fired_date"]);
                        }
                        else
                        {
                            dtpFiredDate.Checked = false;
                        }
                    }
                }
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введіть ПІБ працівника", "Помилка валідації");
                txtFullName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Введіть посаду працівника", "Помилка валідації");
                txtPosition.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Введіть номер телефону", "Помилка валідації");
                txtPhone.Focus();
                return false;
            }
            string phone = txtPhone.Text.Trim();

            if (!Regex.IsMatch(phone, @"^\+\d{10,15}$"))
            {
                MessageBox.Show(
                    "Номер телефону має бути у форматі +380XXXXXXXXX",
                    "Помилка валідації"
                );
                txtPhone.Focus();
                return false;
            }
            if (dtpHireDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show(
                    "Дата прийняття не може бути в майбутньому",
                    "Помилка валідації"
                );
                dtpHireDate.Focus();
                return false;
            }
            if (dtpFiredDate.Enabled)
            {
                if (dtpFiredDate.Value.Date < dtpHireDate.Value.Date)
                {
                    MessageBox.Show(
                        "Дата звільнення не може бути раніше дати прийняття",
                        "Помилка валідації"
                    );
                    dtpFiredDate.Focus();
                    return false;
                }
            }

            return true;
        }

        private void AddEditEmployeeForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Поле ПІБ є обов'язковим.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(_cs))
            {
                conn.Open();
                MySqlCommand cmd;
                if (_employeeId == null)
                {
                    cmd = new MySqlCommand(
                        @"INSERT INTO employees 
                  (full_name, position, phone, hire_date, fired_date)
                  VALUES (@name, @position, @phone, @hire, NULL)", conn);
                }
                else
                {
                    cmd = new MySqlCommand(
                        @"UPDATE employees SET
                    full_name = @name,
                    position = @position,
                    phone = @phone,
                    hire_date = @hire,
                    fired_date = @fired
                  WHERE employee_id = @id", conn);

                    cmd.Parameters.AddWithValue("@id", _employeeId);

                    if (dtpFiredDate.Checked)
                        cmd.Parameters.AddWithValue("@fired", dtpFiredDate.Value);
                    else
                        cmd.Parameters.AddWithValue("@fired", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@hire", dtpHireDate.Value);

                cmd.ExecuteNonQuery();
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
