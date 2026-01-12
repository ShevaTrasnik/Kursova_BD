using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova_BD
{
    public partial class AddEditRecipeIngredientForm : Form
    {
        private readonly string _cs;
        private readonly int _productId;
        private readonly int? _recipeId;
        private readonly string _ingredientNameToEdit;
        private int? _ingredientIdToEdit = null;
        public AddEditRecipeIngredientForm(string cs, int productId)
        {
            InitializeComponent();
            _cs = cs;
            _productId = productId;
            txtUnit.Text = "г";
            _recipeId = null;
            LoadIngredients();

        }
        public AddEditRecipeIngredientForm(string cs, int productId, string ingredientName)
        {
            InitializeComponent();
            _cs = cs;
            _productId = productId;
            _ingredientNameToEdit = ingredientName;
            txtUnit.Text = "г";
            txtUnit.ReadOnly = true;
            LoadIngredients();
            LoadExistingByName();
        }
        private void LoadIngredients()
        {
            string sql = @"
                SELECT ingredient_id, name, unit
                FROM ingredients
                ORDER BY name;
            ";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);

                cbIngredient.DataSource = table;
                cbIngredient.DisplayMember = "name";
                cbIngredient.ValueMember = "ingredient_id";
                cbIngredient.SelectedIndex = -1;
            }
        }
        private void LoadRecipeIngredient()
        {
            if (_recipeId == null)
                return;
                string sql = @"
                SELECT
                    r.recipe_id AS recipe_id,
                    i.name AS ingredient_name,
                    r.quantity,
                    i.unit
                FROM recipes r
                JOIN ingredients i ON r.ingredient_id = i.ingredient_id
                WHERE r.product_id = @productId
                ORDER BY i.name;
            ";
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", _recipeId.Value);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cbIngredient.SelectedValue = reader.GetInt32("ingredient_id");
                        txtQuantity.Text = reader.GetDecimal("quantity").ToString();
                    }
                }
            }
        }

        private void AddEditRecipeIngredientForm_Load(object sender, EventArgs e)
        {

        }

        private void cbIngredient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIngredient.SelectedIndex < 0)
            {
                txtUnit.Text = "";
                return;
            }
            var row = (cbIngredient.SelectedItem as DataRowView);
            txtUnit.Text = row["unit"]?.ToString() ?? "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(
                    txtQuantity.Text.Replace(',', '.'),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out decimal quantity))
            {
                MessageBox.Show("Некоректна кількість");
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Кількість повинна бути більшою за 0");
                return;
            }
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                if (_ingredientIdToEdit != null)
                {
                    var updateCmd = new MySqlCommand(@"
                UPDATE recipes
                SET quantity = @quantity
                WHERE product_id = @productId
                  AND ingredient_id = @ingredientId;
            ", conn);

                    updateCmd.Parameters.AddWithValue("@productId", _productId);
                    updateCmd.Parameters.AddWithValue("@ingredientId", _ingredientIdToEdit.Value);
                    updateCmd.Parameters.Add("@quantity", MySqlDbType.Decimal).Value = quantity;

                    updateCmd.ExecuteNonQuery();

                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                if (cbIngredient.SelectedValue == null)
                {
                    MessageBox.Show("Оберіть інгредієнт");
                    return;
                }
                int ingredientId = Convert.ToInt32(cbIngredient.SelectedValue);
                var checkCmd = new MySqlCommand(@"
            SELECT COUNT(*)
            FROM recipes
            WHERE product_id = @productId
              AND ingredient_id = @ingredientId;
        ", conn);

                checkCmd.Parameters.AddWithValue("@productId", _productId);
                checkCmd.Parameters.AddWithValue("@ingredientId", ingredientId);

                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (exists > 0)
                {
                    MessageBox.Show(
                        "Цей інгредієнт уже доданий до рецепту",
                        "Увага",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                var insertCmd = new MySqlCommand(@"
            INSERT INTO recipes (product_id, ingredient_id, quantity)
            VALUES (@productId, @ingredientId, @quantity);
        ", conn);

                insertCmd.Parameters.AddWithValue("@productId", _productId);
                insertCmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                insertCmd.Parameters.Add("@quantity", MySqlDbType.Decimal).Value = quantity;

                insertCmd.ExecuteNonQuery();
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void LoadExistingByName()
        {
            string sql = @"
        SELECT
            r.ingredient_id,
            r.quantity
        FROM recipes r
        JOIN ingredients i ON r.ingredient_id = i.ingredient_id
        WHERE r.product_id = @productId
          AND i.name = @name
        LIMIT 1;
    ";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", _productId);
                cmd.Parameters.AddWithValue("@name", _ingredientNameToEdit);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Запис рецепту не знайдено для редагування");
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }
                    _ingredientIdToEdit = reader.GetInt32("ingredient_id");
                    decimal qty = reader.GetDecimal("quantity");
                    txtQuantity.Text = qty.ToString();
                }
            }
            if (_ingredientIdToEdit != null)
                cbIngredient.SelectedValue = _ingredientIdToEdit.Value;
            cbIngredient.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
