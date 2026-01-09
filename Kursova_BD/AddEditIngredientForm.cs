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
    public partial class AddEditIngredientForm : Form
    {
        private readonly string _cs;
        private readonly int? _ingredientId;
        public AddEditIngredientForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;
            _ingredientId = null;
            LoadSuppliers();
            LoadUnits();
        }
        public AddEditIngredientForm(string connectionString, int ingredientId)
        {
            InitializeComponent();

            _cs = connectionString;
            _ingredientId = ingredientId;

            LoadSuppliers();
            LoadUnits();
            LoadIngredient();
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
        private void LoadUnits()
        {
            cbUnit.Items.AddRange(new object[]
            {
        "г", "кг", "мл", "л", "шт"
            });
        }
        private void LoadIngredient()
        {
            string sql = @"
        SELECT name, supplier_id, stock_qty, unit, shelf_life
        FROM ingredients
        WHERE ingredient_id = @id";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", _ingredientId);

                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return;

                    txtName.Text = r["name"].ToString();
                    txtStockQty.Text = r["stock_qty"].ToString();
                    cbUnit.Text = r["unit"].ToString();
                    cbSupplier.SelectedValue = r["supplier_id"];
                    dtpShelfLife.Value = Convert.ToDateTime(r["shelf_life"]);
                }
            }
        }


        private void AddEditIngredientForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву інгредієнта");
                return;
            }

            if (!decimal.TryParse(txtStockQty.Text, out decimal qty))
            {
                MessageBox.Show("Некоректна кількість");
                return;
            }

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                MySqlCommand cmd;

                if (_ingredientId == null)
                {
                    cmd = new MySqlCommand(@"
                INSERT INTO ingredients
                (name, supplier_id, stock_qty, unit, shelf_life)
                VALUES (@name, @supplier, @qty, @unit, @date)", conn);
                }
                else
                {
                    cmd = new MySqlCommand(@"
                UPDATE ingredients SET
                    name = @name,
                    supplier_id = @supplier,
                    stock_qty = @qty,
                    unit = @unit,
                    shelf_life = @date
                WHERE ingredient_id = @id", conn);

                    cmd.Parameters.AddWithValue("@id", _ingredientId);
                }

                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@supplier", cbSupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@unit", cbUnit.Text);
                cmd.Parameters.AddWithValue("@date", dtpShelfLife.Value);

                cmd.ExecuteNonQuery();
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
