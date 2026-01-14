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

namespace Kursova_BD
{
    public partial class StatisticsForm : Form
    {
        private readonly string _cs =
    "Server=127.0.0.1;Port=3306;Database=bakery_db_clean;User Id=root;Password=VFRcbvrf2007;SslMode=Preferred;AllowPublicKeyRetrieval=True;";
        public StatisticsForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadStatistics();
        }
        private void LoadStatistics()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM production_batches", conn))
                {
                    lblBatchCount.Text = cmd.ExecuteScalar().ToString();
                }
                using (var cmd = new MySqlCommand(@"
                    SELECT IFNULL(SUM(pi.produced_qty * bp.weight), 0)
                    FROM production_items pi
                    JOIN bakery_products bp ON bp.product_id = pi.product_id", conn))
                {
                    decimal totalWeight = Convert.ToDecimal(cmd.ExecuteScalar());
                    lblTotalWeight.Text = $"{totalWeight:F2} кг";
                }
                using (var cmd = new MySqlCommand(@"
                    SELECT IFNULL(AVG(batch_weight), 0)
                    FROM (
                        SELECT SUM(pi.produced_qty * bp.weight) AS batch_weight
                        FROM production_items pi
                        JOIN bakery_products bp ON bp.product_id = pi.product_id
                        GROUP BY pi.batch_id
                    ) t", conn))
                {
                    decimal avgWeight = Convert.ToDecimal(cmd.ExecuteScalar());
                    lblAvgWeight.Text = $"{avgWeight:F2} кг";
                }
                using (var cmd = new MySqlCommand(@"
                    SELECT IFNULL(MAX(batch_weight), 0)
                    FROM (
                        SELECT SUM(pi.produced_qty * bp.weight) AS batch_weight
                        FROM production_items pi
                        JOIN bakery_products bp ON bp.product_id = pi.product_id
                        GROUP BY pi.batch_id
                    ) t", conn))
                {
                    decimal maxWeight = Convert.ToDecimal(cmd.ExecuteScalar());
                    lblMaxWeight.Text = $"{maxWeight:F2} кг";
                }
            }
        }
        private void StatisticsForm_Load(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
