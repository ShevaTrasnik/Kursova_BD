using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kursova_BD
{
    public partial class AddEditTechnologyStepForm : Form
    {
        private readonly string _cs;
        private readonly int _productId;
        private readonly int? _stepNumberToEdit = null;
        public AddEditTechnologyStepForm(string cs, int productId)
        {
            InitializeComponent();
            _cs = cs;
            _productId = productId;
        }
        public AddEditTechnologyStepForm(string cs, int productId, int stepNumber)
        {
            InitializeComponent();
            _cs = cs;
            _productId = productId;
            _stepNumberToEdit = stepNumber;
            LoadStepForEdit();
        }
        private void LoadStepForEdit()
        {
            string sql = @"
                SELECT step_name, description, duration_min
                FROM technology_steps
                WHERE product_id = @productId
                  AND step_number = @stepNumber;
            ";
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", _productId);
                cmd.Parameters.AddWithValue("@stepNumber", _stepNumberToEdit.Value);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Крок не знайдено");
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }
                    txtStepName.Text = reader.GetString("step_name");
                    txtDescription.Text = reader.GetString("description");
                    txtDuration.Text = reader.GetInt32("duration_min").ToString();
                }
            }
        }

        private void AddEditTechnologyStepForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStepName.Text))
            {
                MessageBox.Show("Введіть назву кроку");
                return;
            }

            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Некоректна тривалість");
                return;
            }

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                if (_stepNumberToEdit != null)
                {
                    var updateCmd = new MySqlCommand(@"
                        UPDATE technology_steps
                        SET step_name = @name,
                            description = @desc,
                            duration_min = @dur
                        WHERE product_id = @productId
                          AND step_number = @stepNumber;
                    ", conn);
                    updateCmd.Parameters.AddWithValue("@name", txtStepName.Text);
                    updateCmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                    updateCmd.Parameters.AddWithValue("@dur", duration);
                    updateCmd.Parameters.AddWithValue("@productId", _productId);
                    updateCmd.Parameters.AddWithValue("@stepNumber", _stepNumberToEdit.Value);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    var numCmd = new MySqlCommand(@"
                        SELECT COALESCE(MAX(step_number), 0) + 1
                        FROM technology_steps
                        WHERE product_id = @productId;
                    ", conn);
                    numCmd.Parameters.AddWithValue("@productId", _productId);
                    int nextStepNumber = Convert.ToInt32(numCmd.ExecuteScalar());
                    var insertCmd = new MySqlCommand(@"
                        INSERT INTO technology_steps
                            (product_id, step_number, step_name, description, duration_min)
                        VALUES
                            (@productId, @stepNumber, @name, @desc, @dur);
                    ", conn);
                    insertCmd.Parameters.AddWithValue("@productId", _productId);
                    insertCmd.Parameters.AddWithValue("@stepNumber", nextStepNumber);
                    insertCmd.Parameters.AddWithValue("@name", txtStepName.Text);
                    insertCmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                    insertCmd.Parameters.AddWithValue("@dur", duration);

                    insertCmd.ExecuteNonQuery();
                }
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
