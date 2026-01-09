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
    public partial class FilterIngredientsForm : Form
    {
        public int? SupplierId { get; private set; }
        public DateTime? ShelfLifeTo { get; private set; }

        private readonly string _cs;
        public FilterIngredientsForm(
    string connectionString,
    int? supplierId,
    DateTime? shelfLifeTo)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadSuppliers();
            if (supplierId.HasValue)
            {
                chkSupplier.Checked = true;
                cbSupplier.SelectedValue = supplierId.Value;
            }
            if (shelfLifeTo.HasValue)
            {
                chkShelfLife.Checked = true;
                dtpShelfLifeTo.Value = shelfLifeTo.Value;
            }
        }


        private void chkSupplier_CheckedChanged(object sender, EventArgs e)
        {
            cbSupplier.Enabled = chkSupplier.Checked;
        }

        private void chkShelfLife_CheckedChanged(object sender, EventArgs e)
        {
            dtpShelfLifeTo.Enabled = chkShelfLife.Checked;
        }
        private void LoadSuppliers()
        {
            string sql = "SELECT supplier_id, name FROM suppliers ORDER BY name";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);

                cbSupplier.DataSource = table;
                cbSupplier.DisplayMember = "name";
                cbSupplier.ValueMember = "supplier_id";
                cbSupplier.SelectedIndex = -1;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SupplierId =
        chkSupplier.Checked && cbSupplier.SelectedIndex >= 0
            ? (int?)cbSupplier.SelectedValue
            : null;

            ShelfLifeTo =
                chkShelfLife.Checked
                    ? (DateTime?)dtpShelfLifeTo.Value.Date
                    : null;

            DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
