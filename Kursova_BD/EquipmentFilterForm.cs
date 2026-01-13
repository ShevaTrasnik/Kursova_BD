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
    public partial class EquipmentFilterForm : Form
    {
        public string NameValue { get; private set; }
        public string TypeValue { get; private set; }
        public string StatusValue { get; private set; }
        public DateTime? ServiceFrom { get; private set; }
        public DateTime? ServiceTo { get; private set; }
        private readonly string _cs;
        public EquipmentFilterForm(
            string connectionString,
            string name,
            string type,
            string status,
            DateTime? serviceFrom,
            DateTime? serviceTo)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadNames();
            LoadTypes();
            LoadStatuses();
            if (!string.IsNullOrEmpty(name))
            {
                chkName.Checked = true;
                cmbName.SelectedValue = name;
            }
            if (!string.IsNullOrEmpty(type))
            {
                chkType.Checked = true;
                cmbType.SelectedValue = type;
            }
            if (!string.IsNullOrEmpty(status))
            {
                chkStatus.Checked = true;
                cmbStatus.SelectedValue = status;
            }
            if (serviceFrom.HasValue || serviceTo.HasValue)
            {
                chkServiceDate.Checked = true;
                if (serviceFrom.HasValue) dtpFrom.Value = serviceFrom.Value;
                if (serviceTo.HasValue) dtpTo.Value = serviceTo.Value;
            }
            cmbName.Enabled = chkName.Checked;
            cmbType.Enabled = chkType.Checked;
            cmbStatus.Enabled = chkStatus.Checked;
            dtpFrom.Enabled = chkServiceDate.Checked;
            dtpTo.Enabled = chkServiceDate.Checked;
        }

        private void EquipmentFilterForm_Load(object sender, EventArgs e)
        {

        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkName_CheckedChanged(object sender, EventArgs e)
        {
            cmbName.Enabled = chkName.Checked;
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            cmbType.Enabled = chkType.Checked;
        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            cmbStatus.Enabled = chkStatus.Checked;
        }

        private void chkServiceDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = chkServiceDate.Checked;
            dtpTo.Enabled = chkServiceDate.Checked;
        }
        private void LoadNames()
        {
            LoadCombo(
                "SELECT DISTINCT name FROM equipment ORDER BY name",
                cmbName,
                "name");
        }
        private void LoadTypes()
        {
            LoadCombo(
                "SELECT DISTINCT type FROM equipment WHERE type IS NOT NULL AND type <> '' ORDER BY type",
                cmbType,
                "type");
        }
        private void LoadStatuses()
        {
            LoadCombo(
                "SELECT DISTINCT status FROM equipment WHERE status IS NOT NULL AND status <> '' ORDER BY status",
                cmbStatus,
                "status");
        }
        private void LoadCombo(string sql, ComboBox combo, string column)
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);

                combo.DataSource = table;
                combo.DisplayMember = column;
                combo.ValueMember = column;
                combo.SelectedIndex = -1;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            NameValue =
                chkName.Checked && cmbName.SelectedIndex >= 0
                    ? cmbName.SelectedValue?.ToString()
                    : null;

            TypeValue =
                chkType.Checked && cmbType.SelectedIndex >= 0
                    ? cmbType.SelectedValue?.ToString()
                    : null;

            StatusValue =
                chkStatus.Checked && cmbStatus.SelectedIndex >= 0
                    ? cmbStatus.SelectedValue?.ToString()
                    : null;

            if (chkServiceDate.Checked)
            {
                ServiceFrom = dtpFrom.Value.Date;
                ServiceTo = dtpTo.Value.Date;
            }
            else
            {
                ServiceFrom = null;
                ServiceTo = null;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
