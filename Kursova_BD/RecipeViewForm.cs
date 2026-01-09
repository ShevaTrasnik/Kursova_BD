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

        private void RecipeViewForm_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
