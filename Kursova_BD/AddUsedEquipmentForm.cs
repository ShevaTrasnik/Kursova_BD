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
    public partial class AddUsedEquipmentForm : Form
    {
        private readonly string _cs;

        public int EquipmentId { get; private set; }
        public string EquipmentName { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public AddUsedEquipmentForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadEquipment();
        }
        private void LoadEquipment()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var adapter = new MySqlDataAdapter(
                    "SELECT equipment_id, name FROM equipment ORDER BY name",
                    conn);

                var table = new DataTable();
                adapter.Fill(table);

                cmbEquipment.DataSource = table;
                cmbEquipment.DisplayMember = "name";
                cmbEquipment.ValueMember = "equipment_id";
                cmbEquipment.SelectedIndex = -1;
            }
        }
        private void AddUsedEquipmentForm_Load(object sender, EventArgs e)
        {

        }
        private void cmbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbEquipment.SelectedIndex < 0)
            {
                MessageBox.Show("Оберіть обладнання");
                return;
            }

            if (dtpEnd.Value <= dtpStart.Value)
            {
                MessageBox.Show("Час завершення має бути пізніше за початок");
                return;
            }

            EquipmentId = Convert.ToInt32(cmbEquipment.SelectedValue);
            EquipmentName = cmbEquipment.Text;
            StartTime = dtpStart.Value;
            EndTime = dtpEnd.Value;

            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
