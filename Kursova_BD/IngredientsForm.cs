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
    public partial class IngredientsForm : Form
    {
        private readonly string _cs;
        private int? _currentIngredientId = null;
        private int? _filterSupplierId = null;
        private DateTime? _filterShelfLifeTo = null;

        public IngredientsForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;

        }
        private void IngredientsForm_Load(object sender, EventArgs e)
        {
            LoadIngredients();
            cbSort.Items.AddRange(new object[]
            {
                "Назва (А-Я)",
                "Назва (Я-А)",
                "Кількість менш. - більш.",
                "Кількість більш. - менш.",
                "Термін придатності ↑",
                "Термін придатності ↓"
            });
            cbSort.SelectedIndex = 0;


        }
        private void LoadIngredients(string orderBy = "i.name ASC")
        {
            string sql = $@"
                SELECT
                    i.ingredient_id,
                    i.name AS ingredient_name,
                    s.name AS supplier_name,
                    i.stock_qty,
                    i.unit,
                    i.shelf_life,
                    i.supplier_id
                FROM ingredients i
                LEFT JOIN suppliers s ON i.supplier_id = s.supplier_id
                ORDER BY {orderBy};
            ";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);

                dataGridIngredients.DataSource = table;
                dataGridIngredients.Columns["ingredient_id"].Visible = false;
                dataGridIngredients.Columns["supplier_id"].Visible = false;
                dataGridIngredients.Columns["ingredient_name"].HeaderText = "Назва";
                dataGridIngredients.Columns["supplier_name"].HeaderText = "Постачальник";
                dataGridIngredients.Columns["stock_qty"].HeaderText = "Кількість";
                dataGridIngredients.Columns["unit"].HeaderText = "Одиниця";
                dataGridIngredients.Columns["shelf_life"].HeaderText = "Термін придатності";

                foreach (DataGridViewColumn col in dataGridIngredients.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void dataGridIngredients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridIngredients.CurrentRow == null)
                return;

            var row = dataGridIngredients.CurrentRow;

            txtName.Text = row.Cells["ingredient_name"].Value?.ToString() ?? "";
            txtSupplier.Text = row.Cells["supplier_name"].Value?.ToString() ?? "";
            txtStockQty.Text = row.Cells["stock_qty"].Value?.ToString() ?? "";
            txtUnit.Text = row.Cells["unit"].Value?.ToString() ?? "";

            if (row.Cells["shelf_life"].Value != DBNull.Value)
                dtpShelfLife.Value = Convert.ToDateTime(row.Cells["shelf_life"].Value);
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            using (var f = new AddEditIngredientForm(_cs))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadIngredients();
                }
            }
        }

        private void btnEditIngredient_Click(object sender, EventArgs e)
        {
            if (dataGridIngredients.CurrentRow == null)
                return;

            int id = Convert.ToInt32(
                dataGridIngredients.CurrentRow.Cells["ingredient_id"].Value);

            using (var f = new AddEditIngredientForm(_cs, id))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadIngredients();
                }
            }
        }


        private void btnDeleteIngredient_Click(object sender, EventArgs e)
        {
            if (dataGridIngredients.CurrentRow == null)
            {
                MessageBox.Show("Оберіть інгредієнт");
                return;
            }
            int ingredientId = Convert.ToInt32(
                dataGridIngredients.CurrentRow.Cells["ingredient_id"].Value);
            var res = MessageBox.Show(
                "Ви дійсно бажаєте видалити інгредієнт?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (res != DialogResult.Yes)
                return;
            try
            {
                using (var conn = new MySqlConnection(_cs))
                {
                    conn.Open();

                    var cmd = new MySqlCommand(
                        "DELETE FROM ingredients WHERE ingredient_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ingredientId);

                    cmd.ExecuteNonQuery();
                }
                LoadIngredients();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1451) 
                {
                    MessageBox.Show(
                        "Неможливо видалити інгредієнт, оскільки він використовується в рецептах.",
                        "Обмеження цілісності",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ApplySearch(string text)
        {
            text = text.Trim().ToLower();

            foreach (DataGridViewRow row in dataGridIngredients.Rows)
            {
                if (row.IsNewRow) continue;

                string ingredient =
                    row.Cells["ingredient_name"].Value?.ToString().ToLower() ?? "";

                string supplier =
                    row.Cells["supplier_name"].Value?.ToString().ToLower() ?? "";

                bool match =
                    ingredient.Contains(text) ||
                    supplier.Contains(text);

                row.DefaultCellStyle.BackColor =
                    match || string.IsNullOrEmpty(text)
                        ? Color.LightGreen
                        : Color.LightCoral;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearch(txtSearch.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            foreach (DataGridViewRow row in dataGridIngredients.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (var f = new FilterIngredientsForm(
                _cs,
                _filterSupplierId,
                _filterShelfLifeTo))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _filterSupplierId = f.SupplierId;
                    _filterShelfLifeTo = f.ShelfLifeTo;

                    ApplyFilter();
                }
            }
        }
        private void ApplyFilter()
        {
            dataGridIngredients.CurrentCell = null;

            foreach (DataGridViewRow row in dataGridIngredients.Rows)
            {
                if (row.IsNewRow) continue;

                bool visible = true;

                if (_filterSupplierId.HasValue)
                {
                    int supplierId =
                        Convert.ToInt32(row.Cells["supplier_id"].Value);

                    if (supplierId != _filterSupplierId.Value)
                        visible = false;
                }

                if (_filterShelfLifeTo.HasValue)
                {
                    if (row.Cells["shelf_life"].Value != DBNull.Value)
                    {
                        DateTime shelf =
                            Convert.ToDateTime(row.Cells["shelf_life"].Value);

                        if (shelf > _filterShelfLifeTo.Value)
                            visible = false;
                    }
                }

                row.Visible = visible;
            }
        }
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            _filterSupplierId = null;
            _filterShelfLifeTo = null;
            dataGridIngredients.CurrentCell = null;

            foreach (DataGridViewRow row in dataGridIngredients.Rows)
            {
                if (!row.IsNewRow)
                    row.Visible = true;
            }
        }
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderBy;

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    orderBy = "i.name ASC";
                    break;
                case 1:
                    orderBy = "i.name DESC";
                    break;
                case 2:
                    orderBy = "i.stock_qty ASC";
                    break;
                case 3:
                    orderBy = "i.stock_qty DESC";
                    break;
                case 4:
                    orderBy = "i.shelf_life ASC";
                    break;
                case 5:
                    orderBy = "i.shelf_life DESC";
                    break;
                default:
                    orderBy = "i.name ASC";
                    break;
            }

            LoadIngredients(orderBy);
        }
    }
}
