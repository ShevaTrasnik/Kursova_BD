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
    public partial class ProductionBatchesForm : Form
    {
        private readonly string _cs =
    "Server=127.0.0.1;Port=3306;Database=bakery_db_clean;User Id=root;Password=VFRcbvrf2007;SslMode=Preferred;AllowPublicKeyRetrieval=True;";
        public ProductionBatchesForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadBatches();
        }
        private void LoadBatches()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                string sql = @"
                SELECT 
                    b.batch_id,
                    b.date,
                    b.shift,
                    e.full_name AS employee,
                    b.total_weight
                FROM production_batches b
                JOIN employees e ON e.employee_id = b.employee_id
                ORDER BY b.date DESC, b.batch_id DESC";
                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridViewBatches.DataSource = table;
                dataGridViewBatches.Columns["batch_id"].HeaderText = "№ партії";
                dataGridViewBatches.Columns["date"].HeaderText = "Дата";
                dataGridViewBatches.Columns["shift"].HeaderText = "Зміна";
                dataGridViewBatches.Columns["employee"].HeaderText = "Працівник";
                dataGridViewBatches.Columns["total_weight"].HeaderText = "Загальна вага";

                dataGridViewBatches.ReadOnly = true;
                dataGridViewBatches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewBatches.AllowUserToAddRows = false;
            }
        }
        private void ProductionBatchesForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddBatch_Click(object sender, EventArgs e)
        {
            var form = new ProductionBatchForm(_cs);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadBatches();
            }
        }

        private void btnViewBatc_Click(object sender, EventArgs e)
        {
            if (dataGridViewBatches.CurrentRow == null)
                return;
            int batchId = Convert.ToInt32(
                dataGridViewBatches.CurrentRow.Cells["batch_id"].Value
            );
            var form = new ProductionBatchForm(_cs, batchId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadBatches();
            }
        }

        private void btnDeleteBatch_Click(object sender, EventArgs e)
        {
            if (dataGridViewBatches.CurrentRow == null)
                return;

            int batchId = Convert.ToInt32(
                dataGridViewBatches.CurrentRow.Cells["batch_id"].Value
            );

            if (MessageBox.Show(
                "Видалити вибрану партію?",
                "Підтвердження",
                MessageBoxButtons.YesNo
            ) != DialogResult.Yes)
                return;

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var cmd = new MySqlCommand(
                    "DELETE FROM production_batches WHERE batch_id = @id",
                    conn
                );
                cmd.Parameters.AddWithValue("@id", batchId);
                cmd.ExecuteNonQuery();
            }

            LoadBatches();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
