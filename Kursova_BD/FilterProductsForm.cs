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
using System.Globalization;

namespace Kursova_BD
{
    public partial class FilterProductsForm : Form
    {
        private readonly string _cs;
        public int? CategoryId { get; private set; }
        public decimal? MinWeight { get; private set; }
        public decimal? MaxWeight { get; private set; }
        public FilterProductsForm(
            string connectionString,
            int? categoryId,
            decimal? minWeight,
            decimal? maxWeight)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadCategories();
            if (categoryId.HasValue)
            {
                chkCategory.Checked = true;
                cbCategory.SelectedValue = categoryId.Value;
            }
            if (minWeight.HasValue || maxWeight.HasValue)
            {
                chkWeight.Checked = true;

                if (minWeight.HasValue)
                    txtMinWeight.Text = minWeight.Value.ToString();

                if (maxWeight.HasValue)
                    txtMaxWeight.Text = maxWeight.Value.ToString();
            }
        }
        private void LoadCategories()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var adapter = new MySqlDataAdapter(
                    "SELECT category_id, name FROM categories ORDER BY name",
                    conn);

                var table = new DataTable();
                adapter.Fill(table);

                cbCategory.DataSource = table;
                cbCategory.DisplayMember = "name";
                cbCategory.ValueMember = "category_id";
            }
        }
        private void FilterProductsForm_Load(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (chkCategory.Checked)
            {
                CategoryId = Convert.ToInt32(cbCategory.SelectedValue);
            }
            else
            {
                CategoryId = null;
            }
            if (chkWeight.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtMinWeight.Text))
                {
                    if (!decimal.TryParse(
                        txtMinWeight.Text.Replace(',', '.'),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out decimal min))
                    {
                        MessageBox.Show("Некоректна мінімальна вага");
                        return;
                    }
                    MinWeight = min;
                }
                if (!string.IsNullOrWhiteSpace(txtMaxWeight.Text))
                {
                    if (!decimal.TryParse(
                        txtMaxWeight.Text.Replace(',', '.'),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out decimal max))
                    {
                        MessageBox.Show("Некоректна максимальна вага");
                        return;
                    }
                    MaxWeight = max;
                }
            }
            else
            {
                MinWeight = null;
                MaxWeight = null;
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
