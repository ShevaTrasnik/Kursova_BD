using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Kursova_BD
{
    public partial class MainForm : Form
    {
        private readonly string _cs =
    "Server=127.0.0.1;Port=3306;Database=bakery_db_clean;User Id=root;Password=VFRcbvrf2007;SslMode=Preferred;AllowPublicKeyRetrieval=True;";
        private MySqlConnection conn;
        private MySqlDataAdapter _adapter;
        private DataTable _table;
        public MainForm()
        {
            InitializeComponent();
            conn = new MySqlConnection(_cs);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
            Application.UseWaitCursor = false;
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                string sql = @"
            SELECT 
                p.product_id,
                p.name AS product_name,
                c.name AS category_name,
                p.weight,
                p.unit,
                p.description,
                p.created_at
            FROM bakery_products p
            JOIN categories c ON p.category_id = c.category_id
            ORDER BY p.name;
        ";

                using (var conn = new MySqlConnection(_cs))
                {
                    conn.Open();

                    _adapter = new MySqlDataAdapter(sql, conn);
                    _table = new DataTable();
                    _adapter.Fill(_table);
                }

                dataGridProducts.DataSource = _table;

                dataGridProducts.Columns["product_id"].Visible = false;
                dataGridProducts.Columns["description"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка завантаження продуктів");
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnViewRecipe_Click(object sender, EventArgs e)
        {

        }

        private void dataGridProducts_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridProducts.CurrentRow == null)
                return;

            txtProductName.Text =
                dataGridProducts.CurrentRow.Cells["product_name"].Value?.ToString() ?? "";

            txtCategory.Text =
                dataGridProducts.CurrentRow.Cells["category_name"].Value?.ToString() ?? "";

            txtWeight.Text =
                dataGridProducts.CurrentRow.Cells["weight"].Value?.ToString() ?? "";

            txtUnit.Text =
                dataGridProducts.CurrentRow.Cells["unit"].Value?.ToString() ?? "";

            txtDescription.Text =
                dataGridProducts.CurrentRow.Cells["description"].Value?.ToString() ?? "";
        }
    }
}
