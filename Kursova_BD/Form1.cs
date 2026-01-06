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

        private void LoadProducts(string searchText = "")
        {
            try
            {
                string sql = @"
            SELECT 
                p.product_id,
                p.name AS product_name,
                c.name AS category_name,
                p.category_id,
                p.weight,
                p.unit,
                p.description,
                p.created_at
            FROM bakery_products p
            JOIN categories c ON p.category_id = c.category_id
            WHERE (@search = '' OR p.name LIKE @pattern)
            ORDER BY p.name;
        ";

                using (var conn = new MySqlConnection(_cs))
                {
                    conn.Open();
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@search", searchText);
                    cmd.Parameters.AddWithValue("@pattern", "%" + searchText + "%");
                    var adapter = new MySqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);
                    dataGridProducts.DataSource = table;

                    dataGridProducts.Columns["product_id"].Visible = false;
                    dataGridProducts.Columns["category_id"].Visible = false;
                    dataGridProducts.Columns["description"].Visible = false;

                    dataGridProducts.Columns["product_name"].HeaderText = "Назва";
                    dataGridProducts.Columns["category_name"].HeaderText = "Категорія";
                    dataGridProducts.Columns["weight"].HeaderText = "Вага";
                    dataGridProducts.Columns["unit"].HeaderText = "Одиниця";
                    dataGridProducts.Columns["created_at"].HeaderText = "Створено";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка пошуку");
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var f = new AddEditProductForm(_cs))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
        }


        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.CurrentRow == null)
                return;

            int id = Convert.ToInt32(
                dataGridProducts.CurrentRow.Cells["product_id"].Value
            );

            using (var f = new AddEditProductForm(_cs, id))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
        }


        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Оберіть продукт для видалення",
                    "Увага",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string productName =
                dataGridProducts.CurrentRow.Cells["product_name"].Value.ToString();

            var result = MessageBox.Show(
                $"Ви дійсно бажаєте видалити продукт:\n\n{productName} ?",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            int productId = Convert.ToInt32(
                dataGridProducts.CurrentRow.Cells["product_id"].Value
            );

            try
            {
                using (var conn = new MySqlConnection(_cs))
                {
                    conn.Open();

                    var cmd = new MySqlCommand(
                        "DELETE FROM bakery_products WHERE product_id = @id",
                        conn);

                    cmd.Parameters.AddWithValue("@id", productId);

                    cmd.ExecuteNonQuery();
                }
                LoadProducts();
                MessageBox.Show(
                    "Продукт успішно видалено",
                    "Готово",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (MySqlException ex){
                MessageBox.Show(
                    "Неможливо видалити продукт, оскільки він використовується\n" +
                    "у рецептах або виробничих даних.",
                    "Помилка видалення",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadProducts();
        }
    }
}
