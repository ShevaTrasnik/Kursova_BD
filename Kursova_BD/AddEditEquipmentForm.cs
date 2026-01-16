using MySql.Data.MySqlClient;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova_BD
{
    public partial class AddEditEquipmentForm : Form
    {
        private readonly string _cs;
        private readonly int? _equipmentId;
        public AddEditEquipmentForm(string connectionString, int? equipmentId = null)
        {
            InitializeComponent();
            _cs = connectionString;
            _equipmentId = equipmentId;
            if (cmbStatus.Items.Count == 0)
            {
                cmbStatus.Items.Add("Працює");
                cmbStatus.Items.Add("На обслуговуванні");
                cmbStatus.Items.Add("Несправне");
            }
            if (_equipmentId.HasValue)
            {
                Text = "Редагувати обладнання";
                LoadEquipment();
            }
            else
            {
                Text = "Додати обладнання";
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedIndex = 0;
            }
        }
        private void LoadEquipment()
        {
            string sql = @"
                SELECT name, type, status, last_service_date
                FROM equipment
                WHERE equipment_id = @id
            ";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", _equipmentId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader["name"].ToString();
                        txtType.Text = reader["type"].ToString();
                        cmbStatus.SelectedItem = reader["status"].ToString();

                        if (reader["last_service_date"] != DBNull.Value)
                        {
                            dtpServiceDate.Value = Convert.ToDateTime(reader["last_service_date"]);
                            dtpServiceDate.Checked = true;
                        }
                        else
                        {
                            dtpServiceDate.Checked = false;
                        }
                    }
                }
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(
                    "Введіть назву обладнання",
                    "Помилка валідації",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtName.Focus();
                return false;
            }
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Оберіть стан обладнання",
                    "Помилка валідації",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                cmbStatus.Focus();
                return false;
            }
            if (dtpServiceDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show(
                    "Дата обслуговування не може бути в майбутньому",
                    "Помилка валідації",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtpServiceDate.Focus();
                return false;
            }

            return true;
        }

        private void AddEditEquipmentForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Вкажіть назву обладнання");
                return;
            }

            string sql;

            if (_equipmentId.HasValue)
            {
                sql = @"
                    UPDATE equipment
                    SET name = @name,
                        type = @type,
                        status = @status,
                        last_service_date = @serviceDate
                    WHERE equipment_id = @id
                ";
            }
            else
            {
                sql = @"
                    INSERT INTO equipment (name, type, status, last_service_date)
                    VALUES (@name, @type, @status, @serviceDate)
                ";
            }
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@type", txtType.Text.Trim());
                cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem?.ToString());
                if (dtpServiceDate.Checked)
                    cmd.Parameters.AddWithValue("@serviceDate", dtpServiceDate.Value.Date);
                else
                    cmd.Parameters.AddWithValue("@serviceDate", DBNull.Value);
                if (_equipmentId.HasValue)
                    cmd.Parameters.AddWithValue("@id", _equipmentId);

                cmd.ExecuteNonQuery();
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
