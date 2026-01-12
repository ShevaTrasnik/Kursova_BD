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
    public partial class EmployeesForm : Form
    {
        private readonly string _cs =
    "Server=127.0.0.1;Port=3306;Database=bakery_db_clean;User Id=root;Password=VFRcbvrf2007;SslMode=Preferred;AllowPublicKeyRetrieval=True;";
        private DataTable employeesTable = new DataTable();
        private string filterFullName;
        private string filterPosition;
        private DateTime? filterHireFrom;
        private DateTime? filterHireTo;
        public EmployeesForm()
        {
            InitializeComponent();
            InitSortCombo();
            LoadEmployees();
        }
        private void LoadEmployees(string whereClause = "", string orderBy = "")
        {
            using (MySqlConnection conn = new MySqlConnection(_cs))
            {
                conn.Open();
                string query = @"
            SELECT 
                employee_id,
                full_name,
                position,
                phone,
                hire_date,
                fired_date
            FROM employees
        ";
                if (!string.IsNullOrWhiteSpace(whereClause))
                    query += " WHERE " + whereClause;
                if (!string.IsNullOrWhiteSpace(orderBy))
                    query += " ORDER BY " + orderBy;
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                employeesTable.Clear();
                adapter.Fill(employeesTable);
                dataGridViewEmployees.DataSource = employeesTable;
            }
            SetupEmployeeGrid();
        }
        private void SetupEmployeeGrid()
        {
            if (dataGridViewEmployees.Columns.Count == 0) return;

            dataGridViewEmployees.Columns["employee_id"].Visible = false;
            dataGridViewEmployees.Columns["full_name"].HeaderText = "ПІБ";
            dataGridViewEmployees.Columns["position"].HeaderText = "Посада";
            dataGridViewEmployees.Columns["phone"].HeaderText = "Телефон";
            dataGridViewEmployees.Columns["hire_date"].HeaderText = "Дата прийняття";
            dataGridViewEmployees.Columns["fired_date"].HeaderText = "Дата звільнення";
        }
        private void InitSortCombo()
        {
            cmbSort.Items.Clear();

            cmbSort.Items.Add("ПІБ (А-Я)");
            cmbSort.Items.Add("ПІБ (Я-А)");
            cmbSort.Items.Add("Посада");
            cmbSort.Items.Add("Дата прийняття");
            cmbSort.SelectedIndex = 0;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditEmployeeForm form = new AddEditEmployeeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.CurrentRow == null) return;

            int employeeId = Convert.ToInt32(
                dataGridViewEmployees.CurrentRow.Cells["employee_id"].Value);

            AddEditEmployeeForm form = new AddEditEmployeeForm(employeeId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.CurrentRow == null) return;

            int employeeId = Convert.ToInt32(
                dataGridViewEmployees.CurrentRow.Cells["employee_id"].Value);

            DialogResult result = MessageBox.Show(
                "Ви дійсно хочете видалити обраного працівника?",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            using (MySqlConnection conn = new MySqlConnection(_cs))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "DELETE FROM employees WHERE employee_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", employeeId);
                cmd.ExecuteNonQuery();
            }
            LoadEmployees();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                ResetRowColors();
                return;
            }
            foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
            {
                if (row.IsNewRow) continue;
                bool match = false;
                if (row.Cells["full_name"].Value != null &&
                    row.Cells["full_name"].Value.ToString().ToLower().Contains(searchText))
                    match = true;
                if (row.Cells["position"].Value != null &&
                    row.Cells["position"].Value.ToString().ToLower().Contains(searchText))
                    match = true;
                if (row.Cells["phone"].Value != null &&
                    row.Cells["phone"].Value.ToString().ToLower().Contains(searchText))
                    match = true;
                if (match)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void ResetRowColors()
        {
            foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
            {
                if (row.IsNewRow) continue;

                row.DefaultCellStyle.BackColor = dataGridViewEmployees.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dataGridViewEmployees.DefaultCellStyle.ForeColor;
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadEmployees();
        }


        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderBy = "";
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    orderBy = "full_name ASC";
                    break;
                case 1:
                    orderBy = "full_name DESC";
                    break;
                case 2:
                    orderBy = "position ASC";
                    break;
                case 3:
                    orderBy = "hire_date DESC";
                    break;
            }
            LoadEmployees("", orderBy);
        }


        private void dataGridViewEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewEmployees.CurrentRow == null)
                return;

            DataGridViewRow row = dataGridViewEmployees.CurrentRow;

            txtFullNameInfo.Text = row.Cells["full_name"].Value?.ToString() ?? "";
            txtPositionInfo.Text = row.Cells["position"].Value?.ToString() ?? "";
            txtPhoneInfo.Text = row.Cells["phone"].Value?.ToString() ?? "";
            if (row.Cells["hire_date"].Value != DBNull.Value)
            {
                dtpHireDateInfo.Value = Convert.ToDateTime(row.Cells["hire_date"].Value);
            }
            if (row.Cells["fired_date"].Value != DBNull.Value)
            {
                dtpFiredDateInfo.Checked = true;
                dtpFiredDateInfo.Value = Convert.ToDateTime(row.Cells["fired_date"].Value);
            }
            else
            {
                dtpFiredDateInfo.Checked = false;
            }
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
        {

        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            var form = new EmployeesFilterForm(
                _cs,
                filterFullName,
                filterPosition,
                filterHireFrom,
                filterHireTo);

            if (form.ShowDialog() == DialogResult.OK)
            {
                filterFullName = form.FullName;
                filterPosition = form.Position;
                filterHireFrom = form.HireDateFrom;
                filterHireTo = form.HireDateTo;

                ApplyEmployeeFilter();
            }
        }
        private void ApplyEmployeeFilter()
        {
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(filterFullName))
                conditions.Add($"full_name = '{filterFullName}'");

            if (!string.IsNullOrEmpty(filterPosition))
                conditions.Add($"position = '{filterPosition}'");

            if (filterHireFrom.HasValue && filterHireTo.HasValue)
                conditions.Add(
                    $"hire_date BETWEEN '{filterHireFrom:yyyy-MM-dd}' AND '{filterHireTo:yyyy-MM-dd}'");

            string where = string.Join(" AND ", conditions);
            LoadEmployees(where);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            filterFullName = null;
            filterPosition = null;
            filterHireFrom = null;
            filterHireTo = null;

            LoadEmployees();
        }
    }
}
