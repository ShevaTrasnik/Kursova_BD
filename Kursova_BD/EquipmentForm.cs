using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova_BD
{
    public partial class EquipmentForm : Form
    {
        private readonly string _cs =
    "Server=127.0.0.1;Port=3306;Database=bakery_db_clean;User Id=root;Password=VFRcbvrf2007;SslMode=Preferred;AllowPublicKeyRetrieval=True;";
        private DataTable equipmentTable = new DataTable();
        private string filterName;
        private string filterType;
        private string filterStatus;
        private DateTime? filterServiceFrom;
        private DateTime? filterServiceTo;
        public EquipmentForm()
        {
            InitializeComponent();
            InitSortCombo();
            LoadEquipment();
        }
        private void LoadEquipment(string whereClause = "", string orderBy = "")
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                string sql = @"
            SELECT 
                equipment_id,
                name,
                type,
                status,
                last_service_date
            FROM equipment
        ";
                if (!string.IsNullOrWhiteSpace(whereClause))
                    sql += " WHERE " + whereClause;

                if (!string.IsNullOrWhiteSpace(orderBy))
                    sql += " ORDER BY " + orderBy;
                var adapter = new MySqlDataAdapter(sql, conn);
                equipmentTable.Clear();
                adapter.Fill(equipmentTable);
                dataGridViewEquipment.DataSource = equipmentTable;
            }
            SetupEquipmentGrid();
        }
        private void SetupEquipmentGrid()
        {
            dataGridViewEquipment.Columns["equipment_id"].Visible = false;

            dataGridViewEquipment.Columns["name"].HeaderText = "Назва";
            dataGridViewEquipment.Columns["type"].HeaderText = "Тип";
            dataGridViewEquipment.Columns["status"].HeaderText = "Стан";
            dataGridViewEquipment.Columns["last_service_date"].HeaderText = "Дата обслуговування";

            foreach (DataGridViewColumn col in dataGridViewEquipment.Columns)
                col.ReadOnly = true;
        }
        private void EquipmentForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewEquipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewEquipment.CurrentRow == null)
                return;
            var row = dataGridViewEquipment.CurrentRow;
            txtNameInfo.Text = row.Cells["name"].Value?.ToString() ?? "";
            txtTypeInfo.Text = row.Cells["type"].Value?.ToString() ?? "";
            txtStatusInfo.Text = row.Cells["status"].Value?.ToString() ?? "";
            if (row.Cells["last_service_date"].Value != DBNull.Value)
                dtpServiceDateInfo.Value = Convert.ToDateTime(row.Cells["last_service_date"].Value);
        }
        private void InitSortCombo()
        {
            cmbSort.Items.Clear();
            cmbSort.Items.Add("Назва (А-Я)");
            cmbSort.Items.Add("Тип");
            cmbSort.Items.Add("Стан");
            cmbSort.Items.Add("Дата обслуговування");

            cmbSort.SelectedIndex = 0;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEditEquipmentForm(_cs);

            if (form.ShowDialog() == DialogResult.OK)
                LoadEquipment();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewEquipment.CurrentRow == null)
                return;
            int id = Convert.ToInt32(
                dataGridViewEquipment.CurrentRow.Cells["equipment_id"].Value);
            var form = new AddEditEquipmentForm(_cs, id);
            if (form.ShowDialog() == DialogResult.OK)
                LoadEquipment();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEquipment.CurrentRow == null)
            {
                MessageBox.Show("Оберіть обладнання для видалення");
                return;
            }
            string name = dataGridViewEquipment.CurrentRow
                .Cells["name"].Value.ToString();
            var result = MessageBox.Show(
                $"Ви дійсно хочете видалити обладнання:\n\n{name} ?",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result != DialogResult.Yes)
                return;
            int equipmentId = Convert.ToInt32(
                dataGridViewEquipment.CurrentRow.Cells["equipment_id"].Value);
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                string sql = "DELETE FROM equipment WHERE equipment_id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", equipmentId);

                cmd.ExecuteNonQuery();
            }
            LoadEquipment();
            txtNameInfo.Clear();
            txtTypeInfo.Clear();
            txtStatusInfo.Clear();
            dtpServiceDateInfo.Value = DateTime.Today;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                ResetRowColors();
                return;
            }
            foreach (DataGridViewRow row in dataGridViewEquipment.Rows)
            {
                if (row.IsNewRow) continue;
                bool match =
                    row.Cells["name"].Value?.ToString().ToLower().Contains(search) == true ||
                    row.Cells["type"].Value?.ToString().ToLower().Contains(search) == true ||
                    row.Cells["status"].Value?.ToString().ToLower().Contains(search) == true;
                row.DefaultCellStyle.BackColor =
                    match ? Color.LightGreen : Color.LightCoral;
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ResetRowColors();
        }
        private void ResetRowColors()
        {
            foreach (DataGridViewRow row in dataGridViewEquipment.Rows)
                row.DefaultCellStyle.BackColor =
                    dataGridViewEquipment.DefaultCellStyle.BackColor;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var form = new EquipmentFilterForm(
                _cs,
                filterName,
                filterType,
                filterStatus,
                filterServiceFrom,
                filterServiceTo
            );
            if (form.ShowDialog() == DialogResult.OK)
            {
                filterName = form.NameValue;
                filterType = form.TypeValue;
                filterStatus = form.StatusValue;
                filterServiceFrom = form.ServiceFrom;
                filterServiceTo = form.ServiceTo;
                LoadEquipment(BuildWhereClause());
            }
        }
        private string BuildWhereClause()
        {
            List<string> cond = new List<string>();

            if (!string.IsNullOrEmpty(filterName))
                cond.Add($"name = '{filterName.Replace("'", "''")}'");

            if (!string.IsNullOrEmpty(filterType))
                cond.Add($"type = '{filterType.Replace("'", "''")}'");

            if (!string.IsNullOrEmpty(filterStatus))
                cond.Add($"status = '{filterStatus.Replace("'", "''")}'");

            if (filterServiceFrom.HasValue && filterServiceTo.HasValue)
                cond.Add(
                    $"last_service_date BETWEEN '{filterServiceFrom:yyyy-MM-dd}' AND '{filterServiceTo:yyyy-MM-dd}'");

            return string.Join(" AND ", cond);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            filterName = null;
            filterType = null;
            filterStatus = null;
            filterServiceFrom = null;
            filterServiceTo = null;

            LoadEquipment();
        }
        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderBy = "";
            switch (cmbSort.SelectedIndex)
            {
                case 0: orderBy = "name ASC"; break;
                case 1: orderBy = "type ASC"; break;
                case 2: orderBy = "status ASC"; break;
                case 3: orderBy = "last_service_date DESC"; break;
            }
            LoadEquipment(BuildWhereClause(), orderBy);
        }
    }
}
