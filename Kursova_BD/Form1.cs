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
        private int? _filterCategoryId = null;
        private decimal? _filterMinWeight = null;
        private decimal? _filterMaxWeight = null;
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
            cbSortProducts.Items.AddRange(new object[]
            {
                "Назва (А-Я)",
                "Назва (Я-А)",
                "Категорія (А-Я)",
                "Вага ↑",
                "Вага ↓",
                "Дата створення ↑",
                "Дата створення ↓"
            });
            cbSortProducts.SelectedIndex = 0;
        }

        private void LoadProducts(string orderBy = "p.name ASC")
        {
            string sql = $@"
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
        ORDER BY {orderBy};
    ";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridProducts.DataSource = table;
                dataGridProducts.Columns["product_id"].Visible = false;
                dataGridProducts.Columns["product_name"].HeaderText = "Назва";
                dataGridProducts.Columns["category_name"].HeaderText = "Категорія";
                dataGridProducts.Columns["weight"].HeaderText = "Вага";
                dataGridProducts.Columns["unit"].HeaderText = "Одиниця";
                dataGridProducts.Columns["description"].HeaderText = "Опис продукту";
                dataGridProducts.Columns["created_at"].HeaderText = "Створено";
                foreach (DataGridViewColumn col in dataGridProducts.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
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
            int productId = Convert.ToInt32(
                dataGridProducts.CurrentRow.Cells["product_id"].Value);

            string productName =
                dataGridProducts.CurrentRow.Cells["product_name"].Value.ToString();

            var form = new RecipeViewForm(_cs, productId, productName);
            form.ShowDialog();
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
            string search = txtSearch.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dataGridProducts.Rows)
            {
                if (row.IsNewRow) continue;
                string productName = row.Cells["product_name"].Value
                    ?.ToString().ToLower() ?? "";
                string categoryName = row.Cells["category_name"].Value
                    ?.ToString().ToLower() ?? "";
                if (string.IsNullOrEmpty(search))
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (
                    productName.Contains(search) ||
                    categoryName.Contains(search)
                )
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


        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadProducts();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var form = new FilterProductsForm(
                _cs,
                _filterCategoryId,
                _filterMinWeight,
                _filterMaxWeight
            );

            if (form.ShowDialog() == DialogResult.OK)
            {
                _filterCategoryId = form.CategoryId;
                _filterMinWeight = form.MinWeight;
                _filterMaxWeight = form.MaxWeight;

                ApplyFilter(
                    _filterCategoryId,
                    _filterMinWeight,
                    _filterMaxWeight
                );
            }
        }
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            LoadProducts();
            _filterCategoryId = null;
            _filterMinWeight = null;
            _filterMaxWeight = null;
        }
        private void ApplyFilter(int? categoryId, decimal? minWeight, decimal? maxWeight)
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
        WHERE 1=1";
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;
                if (categoryId.HasValue)
                {
                    sql += " AND p.category_id = @cat";
                    cmd.Parameters.AddWithValue("@cat", categoryId.Value);
                }
                if (minWeight.HasValue)
                {
                    sql += " AND p.weight >= @min";
                    cmd.Parameters.AddWithValue("@min", minWeight.Value);
                }
                if (maxWeight.HasValue)
                {
                    sql += " AND p.weight <= @max";
                    cmd.Parameters.AddWithValue("@max", maxWeight.Value);
                }
                sql += " ORDER BY p.name";
                cmd.CommandText = sql;
                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridProducts.DataSource = table;
            }
        }

        private void btnIngredients_Click(object sender, EventArgs e)
        {
            var form = new IngredientsForm(_cs);
            form.ShowDialog();
        }

        private void cbSortProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderBy;

            switch (cbSortProducts.SelectedIndex)
            {
                case 0:
                    orderBy = "p.name ASC";
                    break;
                case 1:
                    orderBy = "p.name DESC";
                    break;
                case 2:
                    orderBy = "c.name ASC";
                    break;
                case 3:
                    orderBy = "p.weight ASC";
                    break;
                case 4:
                    orderBy = "p.weight DESC";
                    break;
                case 5:
                    orderBy = "p.created_at ASC";
                    break;
                case 6:
                    orderBy = "p.created_at DESC";
                    break;
                default:
                    orderBy = "p.name ASC";
                    break;
            }
            LoadProducts(orderBy);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            EmployeesForm form = new EmployeesForm();
            form.ShowDialog();
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            EquipmentForm form = new EquipmentForm();
            form.ShowDialog();
        }

        private void btnProductionBatches_Click(object sender, EventArgs e)
        {
            var form = new ProductionBatchesForm(_cs);
            form.ShowDialog();
        }
    }
}
