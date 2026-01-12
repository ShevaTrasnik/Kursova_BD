using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Kursova_BD
{
    public partial class EmployeesFilterForm : Form
    {
        public string FullName { get; private set; }
        public string Position { get; private set; }
        public DateTime? HireDateFrom { get; private set; }
        public DateTime? HireDateTo { get; private set; }

        private readonly string _cs;

        public EmployeesFilterForm(string connectionString,
                                   string fullName,
                                   string position,
                                   DateTime? hireDateFrom,
                                   DateTime? hireDateTo)
        {
            InitializeComponent();

            _cs = connectionString;
            LoadNames();
            LoadPositions();
            dtpFrom.MaxDate = DateTime.Today;
            dtpTo.MaxDate = DateTime.Today;
            if (!string.IsNullOrEmpty(fullName))
            {
                chkName.Checked = true;
                cmbName.SelectedValue = fullName;
            }

            if (!string.IsNullOrEmpty(position))
            {
                chkPosition.Checked = true;
                cmbPosition.SelectedValue = position;
            }

            if (hireDateFrom.HasValue || hireDateTo.HasValue)
            {
                chkHireDate.Checked = true;
                if (hireDateFrom.HasValue) dtpFrom.Value = hireDateFrom.Value;
                if (hireDateTo.HasValue) dtpTo.Value = hireDateTo.Value;
            }
            cmbName.Enabled = chkName.Checked;
            cmbPosition.Enabled = chkPosition.Checked;
            dtpFrom.Enabled = chkHireDate.Checked;
            dtpTo.Enabled = chkHireDate.Checked;
        }
        private void chkHireDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = chkHireDate.Checked;
            dtpTo.Enabled = chkHireDate.Checked;
        }
        private void LoadNames()
        {
            try
            {
                string sql = "SELECT DISTINCT full_name FROM employees ORDER BY full_name";

                using (var conn = new MySqlConnection(_cs))
                {
                    conn.Open();

                    var adapter = new MySqlDataAdapter(sql, conn);
                    var table = new DataTable();
                    adapter.Fill(table);

                    cmbName.DataSource = table;
                    cmbName.DisplayMember = "full_name";
                    cmbName.ValueMember = "full_name";
                    cmbName.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadNames помилка:\n" + ex.Message);
            }
        }
        private void LoadPositions()
        {
            try
            {
                string sql = "SELECT DISTINCT position FROM employees WHERE position IS NOT NULL AND position <> '' ORDER BY position";

                using (var conn = new MySqlConnection(_cs))
                {
                    conn.Open();

                    var adapter = new MySqlDataAdapter(sql, conn);
                    var table = new DataTable();
                    adapter.Fill(table);

                    cmbPosition.DataSource = table;
                    cmbPosition.DisplayMember = "position";
                    cmbPosition.ValueMember = "position";
                    cmbPosition.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadPositions помилка:\n" + ex.Message);
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            FullName =
                chkName.Checked && cmbName.SelectedIndex >= 0
                    ? cmbName.SelectedValue?.ToString()
                    : null;
            Position =
                chkPosition.Checked && cmbPosition.SelectedIndex >= 0
                    ? cmbPosition.SelectedValue?.ToString()
                    : null;
            if (chkHireDate.Checked)
            {
                HireDateFrom = dtpFrom.Value.Date;
                HireDateTo = dtpTo.Value.Date;
            }
            else
            {
                HireDateFrom = null;
                HireDateTo = null;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void EmployeesFilterForm_Load(object sender, EventArgs e)
        {
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbName.SelectedIndex >= 0)
            {
                chkName.Checked = true;
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPosition.SelectedIndex >= 0)
            {
                chkPosition.Checked = true;
            }
        }
    }
}
