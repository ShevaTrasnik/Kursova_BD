using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Kursova_BD
{
    public partial class AddEditProductForm : Form
    {
        private readonly string _cs;
        private int? _productId;
        public AddEditProductForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadCategories();
            LoadUnits();
        }

        public AddEditProductForm(string connectionString, int productId)
        {
            InitializeComponent();
            _cs = connectionString;
            _productId = productId;
            LoadCategories();
            LoadUnits();
            LoadProduct(productId);
        }


        public AddEditProductForm(
            string connectionString,
            int productId,
            string name,
            int categoryId,
            decimal weight,
            string unit,
            string description)
        {
            InitializeComponent();
            _cs = connectionString;
            _productId = productId;

            LoadCategories();

            txtName.Text = name;
            cbCategory.SelectedValue = categoryId;
            txtWeight.Text = weight.ToString();
            cbUnit.Text = unit;
            txtDescription.Text = description;
        }

        private void LoadCategories()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                string sql = "SELECT category_id, name FROM categories ORDER BY name";

                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);
                cbCategory.DataSource = null;
                cbCategory.Items.Clear();
                cbCategory.DisplayMember = "name";
                cbCategory.ValueMember = "category_id";
                cbCategory.DataSource = table;
            }
        }

        private void LoadProduct(int productId)
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var cmd = new MySqlCommand(@"
            SELECT name, category_id, weight, unit, description
            FROM bakery_products
            WHERE product_id = @id", conn);

                cmd.Parameters.AddWithValue("@id", productId);

                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        return;

                    txtName.Text = r["name"].ToString();
                    txtWeight.Text = Convert
                        .ToDecimal(r["weight"])
                        .ToString(System.Globalization.CultureInfo.InvariantCulture);
                    txtDescription.Text = r["description"].ToString();
                    cbCategory.SelectedValue = Convert.ToInt32(r["category_id"]);
                    cbUnit.SelectedItem = r["unit"].ToString();
                }
            }
        }

        private void LoadUnits()
        {
            cbUnit.Items.Clear();

            cbUnit.Items.Add("г");
            cbUnit.Items.Add("кг");
            cbUnit.Items.Add("мл");
            cbUnit.Items.Add("л");
            cbUnit.Items.Add("шт");
            cbUnit.Items.Add("уп");

            cbUnit.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву продукту", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            if (cbCategory.SelectedValue == null)
            {
                MessageBox.Show("Оберіть категорію", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCategory.Focus();
                return;
            }
            if (!decimal.TryParse(
                txtWeight.Text.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out decimal weight))
            {
                MessageBox.Show("Вага має бути числом", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWeight.Focus();
                return;
            }

            if (weight <= 0)
            {
                MessageBox.Show("Вага має бути більшою за 0", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWeight.Focus();
                return;
            }
            if (cbUnit.SelectedItem == null)
            {
                MessageBox.Show("Оберіть одиницю виміру", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbUnit.Focus();
                return;
            }
            if (txtDescription.Text.Length > 500)
            {
                MessageBox.Show("Опис занадто довгий (макс. 500 символів)", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return;
            }
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                MySqlCommand cmd;

                if (_productId == null)
                {
                    cmd = new MySqlCommand(@"
                INSERT INTO bakery_products
                (name, category_id, weight, unit, description)
                VALUES (@name, @category, @weight, @unit, @description)", conn);
                }
                else
                {
                    cmd = new MySqlCommand(@"
                UPDATE bakery_products
                SET name=@name,
                    category_id=@category,
                    weight=@weight,
                    unit=@unit,
                    description=@description
                WHERE product_id=@id", conn);

                    cmd.Parameters.AddWithValue("@id", _productId.Value);
                }

                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@category", cbCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@unit", cbUnit.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());

                cmd.ExecuteNonQuery();
            }

            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
