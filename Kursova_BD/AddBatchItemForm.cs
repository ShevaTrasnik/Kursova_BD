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
    public partial class AddBatchItemForm : Form
    {
        private readonly string _cs;
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Quantity { get; private set; }
        public AddBatchItemForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadProducts();
        }
        private void LoadProducts()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var adapter = new MySqlDataAdapter(
                    "SELECT product_id, name FROM bakery_products ORDER BY name",
                    conn);
                var table = new DataTable();
                adapter.Fill(table);
                cmbProduct.DataSource = table;
                cmbProduct.DisplayMember = "name";
                cmbProduct.ValueMember = "product_id";
                cmbProduct.SelectedIndex = -1;
            }
        }
        private void AddBatchItemForm_Load(object sender, EventArgs e)
        {

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex < 0)
            {
                MessageBox.Show("Оберіть виріб");
                return;
            }

            ProductId = Convert.ToInt32(cmbProduct.SelectedValue);
            ProductName = cmbProduct.Text;
            Quantity = nudQuantity.Value;

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
