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
    public partial class RecipeViewForm : Form
    {
        private readonly string _cs;
        private readonly int _productId;

        public RecipeViewForm(string connectionString, int productId, string productName)
        {
            InitializeComponent();
            _cs = connectionString;
            _productId = productId;
            lblProductName.Text = productName;
            LoadIngredients();
            LoadSteps();
        }

        private void LoadIngredients()
        {
            string sql = @"
        SELECT 
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
                cmd.Parameters.AddWithValue("@productId", _productId);
                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridIngredients.DataSource = table;
                dataGridIngredients.Columns["ingredient_name"].HeaderText = "Інгредієнт";
                dataGridIngredients.Columns["quantity"].HeaderText = "Кількість";
                dataGridIngredients.Columns["unit"].HeaderText = "Одиниця";
            }
        }
        private void LoadSteps()
        {
            string sql = @"
        SELECT
            step_number,
            step_name,
            description,
            duration_min
        FROM technology_steps
        WHERE product_id = @productId
        ORDER BY step_number;
    ";
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", _productId);
                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridSteps.DataSource = table;
                dataGridSteps.Columns["step_number"].HeaderText = "№";
                dataGridSteps.Columns["step_name"].HeaderText = "Назва кроку";
                dataGridSteps.Columns["description"].HeaderText = "Опис";
                dataGridSteps.Columns["duration_min"].HeaderText = "Тривалість (хв)";
            }
        }
        private void LoadRecipeIngredients()
        {
            string sql = @"
            SELECT
                r.ingredient_id,
                i.name AS ingredient_name,
                r.quantity,
                i.unit
            FROM recipes r
            JOIN ingredients i ON r.ingredient_id = i.ingredient_id
            WHERE r.product_id = @productId
            ORDER BY i.name;";
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", _productId);

                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);

                dataGridIngredients.DataSource = null;
                dataGridIngredients.Columns.Clear();
                dataGridIngredients.AutoGenerateColumns = true;

                dataGridIngredients.DataSource = table;
                dataGridIngredients.Columns[0].Visible = false;

                dataGridIngredients.Columns[1].HeaderText = "Інгредієнт";
                dataGridIngredients.Columns[2].HeaderText = "Кількість";
                dataGridIngredients.Columns[3].HeaderText = "Одиниця";

                foreach (DataGridViewColumn col in dataGridIngredients.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        private void RecipeViewForm_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            using (var f = new AddEditRecipeIngredientForm(_cs, _productId))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadRecipeIngredients();
                }
            }
        }

        private void btnEditIngredient_Click(object sender, EventArgs e)
        {
            if (dataGridIngredients.CurrentRow == null)
            {
                MessageBox.Show("Оберіть інгредієнт");
                return;
            }

            // Беремо назву інгредієнта з гріда
            string ingredientName =
                dataGridIngredients.CurrentRow.Cells["ingredient_name"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(ingredientName))
            {
                MessageBox.Show("Не вдалося визначити інгредієнт");
                return;
            }
            using (var f = new AddEditRecipeIngredientForm(_cs, _productId, ingredientName))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadRecipeIngredients();
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
            string ingredientName =
                dataGridIngredients.CurrentRow.Cells["ingredient_name"].Value.ToString();

            var confirm = MessageBox.Show(
                $"Видалити інгредієнт \"{ingredientName}\" з рецепту?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes)
                return;
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
            DELETE r
            FROM recipes r
            JOIN ingredients i ON r.ingredient_id = i.ingredient_id
            WHERE r.product_id = @productId
              AND i.name = @ingredientName;
        ", conn);
                cmd.Parameters.AddWithValue("@productId", _productId);
                cmd.Parameters.AddWithValue("@ingredientName", ingredientName);
                cmd.ExecuteNonQuery();
            }
            LoadRecipeIngredients();
        }
        private void LoadTechnologySteps()
        {
            string sql = @"
        SELECT
            step_number,
            step_name,
            description,
            duration_min
        FROM technology_steps
        WHERE product_id = @productId
        ORDER BY step_number;
    ";

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", _productId);

                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);

                dataGridSteps.DataSource = table;

                dataGridSteps.Columns["step_number"].HeaderText = "№";
                dataGridSteps.Columns["step_name"].HeaderText = "Назва кроку";
                dataGridSteps.Columns["description"].HeaderText = "Опис";
                dataGridSteps.Columns["duration_min"].HeaderText = "Тривалість (хв)";

                foreach (DataGridViewColumn col in dataGridSteps.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            using (var f = new AddEditTechnologyStepForm(_cs, _productId))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadTechnologySteps();
                }
            }
        }

        private void btnEditStep_Click(object sender, EventArgs e)
        {
            if (dataGridSteps.CurrentRow == null)
            {
                MessageBox.Show("Оберіть крок для редагування");
                return;
            }
            int stepNumber = Convert.ToInt32(
                dataGridSteps.CurrentRow.Cells["step_number"].Value
            );
            using (var f = new AddEditTechnologyStepForm(_cs, _productId, stepNumber))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadTechnologySteps();
                }
            }
        }

        private void btnDeleteStep_Click(object sender, EventArgs e)
        {
            if (dataGridSteps.CurrentRow == null)
            {
                MessageBox.Show("Оберіть крок для видалення");
                return;
            }
            int stepNumber = Convert.ToInt32(
                dataGridSteps.CurrentRow.Cells["step_number"].Value
            );
            var confirm = MessageBox.Show(
                "Видалити цей крок?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes)
                return;
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
            DELETE FROM technology_steps
            WHERE product_id = @productId
              AND step_number = @stepNumber;
        ", conn);
                cmd.Parameters.AddWithValue("@productId", _productId);
                cmd.Parameters.AddWithValue("@stepNumber", stepNumber);
                cmd.ExecuteNonQuery();
            }
            LoadTechnologySteps();
        }

    }
}
