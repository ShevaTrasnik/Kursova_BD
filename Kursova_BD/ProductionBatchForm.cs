using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
    public partial class ProductionBatchForm : Form
    {
        private readonly string _cs;

        private DataTable itemsTable = new DataTable();
        private DataTable equipmentTable = new DataTable();
        private decimal totalWeight = 0;
        private int? _batchId;
        private bool _batchHasEquipmentIdColumn = false;
        public ProductionBatchForm(string connectionString, int? batchId = null)
        {
            InitializeComponent();

            _cs = connectionString;
            _batchId = batchId;

            InitShift();
            LoadEmployees();

            InitItemsTable();
            InitEquipmentTable();

            _batchHasEquipmentIdColumn = CheckBatchEquipmentColumnExists();

            if (_batchId.HasValue)
            {
                Text = "Редагувати виробничу партію";
                LoadBatchData();
            }
            else
            {
                Text = "Додати виробничу партію";
            }
        }
        private bool CheckBatchEquipmentColumnExists()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    SELECT COUNT(*)
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME = 'production_batches'
                      AND COLUMN_NAME = 'equipment_id';", conn);

                var count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        private void LoadEmployees()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                var adapter = new MySqlDataAdapter(
                    "SELECT employee_id, full_name FROM employees ORDER BY full_name",
                    conn);

                var table = new DataTable();
                adapter.Fill(table);

                cmbEmployee.DataSource = table;
                cmbEmployee.DisplayMember = "full_name";
                cmbEmployee.ValueMember = "employee_id";
                cmbEmployee.SelectedIndex = -1;
            }
        }
        private void LoadBatchData()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                // 1) Шапка партії
                var cmdBatch = new MySqlCommand(@"
                    SELECT employee_id, date, shift, total_weight
                    FROM production_batches
                    WHERE batch_id = @id;", conn);

                cmdBatch.Parameters.AddWithValue("@id", _batchId.Value);

                using (var reader = cmdBatch.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cmbEmployee.SelectedValue = Convert.ToInt32(reader["employee_id"]);
                        dtpDate.Value = Convert.ToDateTime(reader["date"]);
                        cmbShift.SelectedItem = reader["shift"].ToString();

                        totalWeight = reader["total_weight"] != DBNull.Value
                            ? Convert.ToDecimal(reader["total_weight"])
                            : 0;

                        txtTotalWeight.Text = totalWeight.ToString("0.00");
                    }
                }

                // 2) Вироби партії
                itemsTable.Clear();

                var cmdItems = new MySqlCommand(@"
                    SELECT 
                        pi.product_id,
                        p.name AS product_name,
                        pi.produced_qty
                    FROM production_items pi
                    JOIN bakery_products p ON p.product_id = pi.product_id
                    WHERE pi.batch_id = @id;", conn);

                cmdItems.Parameters.AddWithValue("@id", _batchId.Value);

                using (var reader = cmdItems.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itemsTable.Rows.Add(
                            Convert.ToInt32(reader["product_id"]),
                            reader["product_name"].ToString(),
                            Convert.ToDecimal(reader["produced_qty"])
                        );
                    }
                }

                // 3) Обладнання партії
                equipmentTable.Clear();

                var cmdEq = new MySqlCommand(@"
                    SELECT 
                        ue.equipment_id,
                        e.name AS equipment_name,
                        ue.start_time,
                        ue.end_time
                    FROM used_equipment ue
                    JOIN equipment e ON e.equipment_id = ue.equipment_id
                    WHERE ue.batch_id = @id;", conn);

                cmdEq.Parameters.AddWithValue("@id", _batchId.Value);

                using (var reader = cmdEq.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        equipmentTable.Rows.Add(
                            Convert.ToInt32(reader["equipment_id"]),
                            reader["equipment_name"].ToString(),
                            Convert.ToDateTime(reader["start_time"]),
                            Convert.ToDateTime(reader["end_time"])
                        );
                    }
                }
            }
        }
        private void InitItemsTable()
        {
            itemsTable = new DataTable();

            itemsTable.Columns.Add("product_id", typeof(int));
            itemsTable.Columns.Add("product_name", typeof(string));
            itemsTable.Columns.Add("quantity", typeof(decimal));

            dataGridViewItems.DataSource = itemsTable;

            dataGridViewItems.Columns["product_id"].Visible = false;
            dataGridViewItems.Columns["product_name"].HeaderText = "Виріб";
            dataGridViewItems.Columns["quantity"].HeaderText = "Кількість";

            dataGridViewItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewItems.ReadOnly = true;
            dataGridViewItems.AllowUserToAddRows = false;
            dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void InitShift()
        {
            cmbShift.Items.Add("morning");
            cmbShift.Items.Add("evening");
            cmbShift.Items.Add("night");
            cmbShift.SelectedIndex = 0;
        }
        private void InitEquipmentTable()
        {
            equipmentTable = new DataTable();

            // ВАЖЛИВО: зберігаємо equipment_id, щоб не шукати по назві
            equipmentTable.Columns.Add("equipment_id", typeof(int));
            equipmentTable.Columns.Add("equipment_name", typeof(string));
            equipmentTable.Columns.Add("start_time", typeof(DateTime));
            equipmentTable.Columns.Add("end_time", typeof(DateTime));

            dataGridViewEquipment.DataSource = equipmentTable;

            dataGridViewEquipment.Columns["equipment_id"].Visible = false;
            dataGridViewEquipment.Columns["equipment_name"].HeaderText = "Обладнання";
            dataGridViewEquipment.Columns["start_time"].HeaderText = "Початок";
            dataGridViewEquipment.Columns["end_time"].HeaderText = "Завершення";

            dataGridViewEquipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEquipment.ReadOnly = true;
            dataGridViewEquipment.AllowUserToAddRows = false;
            dataGridViewEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void ProductionBatchForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        int batchId;
                        int? mainEquipmentId = null;
                        if (_batchHasEquipmentIdColumn && equipmentTable.Rows.Count > 0)
                        {
                            mainEquipmentId = Convert.ToInt32(equipmentTable.Rows[0]["equipment_id"]);
                        }

                        if (_batchId.HasValue)
                        {
                            batchId = _batchId.Value;

                            string updateSql = _batchHasEquipmentIdColumn
                                ? @"UPDATE production_batches
                                      SET employee_id = @emp,
                                          date = @date,
                                          shift = @shift,
                                          total_weight = @weight,
                                          equipment_id = @eq
                                    WHERE batch_id = @id;"
                                : @"UPDATE production_batches
                                      SET employee_id = @emp,
                                          date = @date,
                                          shift = @shift,
                                          total_weight = @weight
                                    WHERE batch_id = @id;";

                            var cmdUpdate = new MySqlCommand(updateSql, conn, tx);

                            cmdUpdate.Parameters.AddWithValue("@emp", cmbEmployee.SelectedValue);
                            cmdUpdate.Parameters.AddWithValue("@date", dtpDate.Value.Date);
                            cmdUpdate.Parameters.AddWithValue("@shift", cmbShift.SelectedItem.ToString());
                            cmdUpdate.Parameters.AddWithValue("@weight", totalWeight);
                            cmdUpdate.Parameters.AddWithValue("@id", batchId);

                            if (_batchHasEquipmentIdColumn)
                                cmdUpdate.Parameters.AddWithValue("@eq", mainEquipmentId.Value);

                            cmdUpdate.ExecuteNonQuery();
                            var cmdDelItems = new MySqlCommand(
                                "DELETE FROM production_items WHERE batch_id = @id;",
                                conn, tx);
                            cmdDelItems.Parameters.AddWithValue("@id", batchId);
                            cmdDelItems.ExecuteNonQuery();

                            var cmdDelEq = new MySqlCommand(
                                "DELETE FROM used_equipment WHERE batch_id = @id;",
                                conn, tx);
                            cmdDelEq.Parameters.AddWithValue("@id", batchId);
                            cmdDelEq.ExecuteNonQuery();
                        }
                        else
                        {
                            string insertSql = _batchHasEquipmentIdColumn
                                ? @"INSERT INTO production_batches (employee_id, date, shift, total_weight, equipment_id)
                                   VALUES (@emp, @date, @shift, @weight, @eq);"
                                : @"INSERT INTO production_batches (employee_id, date, shift, total_weight)
                                   VALUES (@emp, @date, @shift, @weight);";

                            var cmdInsert = new MySqlCommand(insertSql, conn, tx);

                            cmdInsert.Parameters.AddWithValue("@emp", cmbEmployee.SelectedValue);
                            cmdInsert.Parameters.AddWithValue("@date", dtpDate.Value.Date);
                            cmdInsert.Parameters.AddWithValue("@shift", cmbShift.SelectedItem.ToString());
                            cmdInsert.Parameters.AddWithValue("@weight", totalWeight);

                            if (_batchHasEquipmentIdColumn)
                                cmdInsert.Parameters.AddWithValue("@eq", mainEquipmentId.Value);

                            cmdInsert.ExecuteNonQuery();
                            batchId = (int)cmdInsert.LastInsertedId;
                        }
                        foreach (DataRow row in itemsTable.Rows)
                        {
                            var cmdItem = new MySqlCommand(@"
                                INSERT INTO production_items (batch_id, product_id, produced_qty)
                                VALUES (@batch, @product, @qty);",
                                conn, tx);

                            cmdItem.Parameters.AddWithValue("@batch", batchId);
                            cmdItem.Parameters.AddWithValue("@product", row["product_id"]);
                            cmdItem.Parameters.AddWithValue("@qty", row["quantity"]);

                            cmdItem.ExecuteNonQuery();
                        }
                        foreach (DataRow row in equipmentTable.Rows)
                        {
                            int equipmentId = Convert.ToInt32(row["equipment_id"]);

                            var cmdEq = new MySqlCommand(@"
                                INSERT INTO used_equipment (batch_id, equipment_id, start_time, end_time)
                                VALUES (@batch, @eq, @start, @end);",
                                conn, tx);

                            cmdEq.Parameters.AddWithValue("@batch", batchId);
                            cmdEq.Parameters.AddWithValue("@eq", equipmentId);
                            cmdEq.Parameters.AddWithValue("@start", row["start_time"]);
                            cmdEq.Parameters.AddWithValue("@end", row["end_time"]);

                            cmdEq.ExecuteNonQuery();
                        }

                        tx.Commit();
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Помилка при збереженні партії:\n" + ex.Message);
                    }
                }
            }
        }
        private bool ValidateForm()
        {
            if (cmbEmployee.SelectedIndex < 0)
            {
                MessageBox.Show("Оберіть працівника");
                return false;
            }

            if (itemsTable.Rows.Count == 0)
            {
                MessageBox.Show("Додайте хоча б один виріб");
                return false;
            }
            if (_batchHasEquipmentIdColumn && equipmentTable.Rows.Count == 0)
            {
                MessageBox.Show("Додайте хоча б одне обладнання (у БД поле equipment_id є обов’язковим).");
                return false;
            }

            return true;
        }
        private void RecalculateTotalWeight()
        {
            totalWeight = 0;

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();

                foreach (DataRow row in itemsTable.Rows)
                {
                    int productId = Convert.ToInt32(row["product_id"]);
                    decimal quantity = Convert.ToDecimal(row["quantity"]);

                    var cmd = new MySqlCommand(
                        "SELECT weight FROM bakery_products WHERE product_id = @id",
                        conn);

                    cmd.Parameters.AddWithValue("@id", productId);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        decimal productWeight = Convert.ToDecimal(result);
                        totalWeight += productWeight * quantity;
                    }
                }
            }

            txtTotalWeight.Text = totalWeight.ToString("0.00");
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var form = new AddBatchItemForm(_cs);
            if (form.ShowDialog() == DialogResult.OK)
            {
                itemsTable.Rows.Add(form.ProductId, form.ProductName, form.Quantity);
                RecalculateTotalWeight();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.CurrentRow == null)
                return;

            int idx = dataGridViewItems.CurrentRow.Index;
            if (idx < 0 || idx >= itemsTable.Rows.Count)
                return;

            itemsTable.Rows.RemoveAt(idx);
            RecalculateTotalWeight();
        }
        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            var form = new AddUsedEquipmentForm(_cs);
            if (form.ShowDialog() == DialogResult.OK)
            {
                equipmentTable.Rows.Add(
                    form.EquipmentId,
                    form.EquipmentName,
                    form.StartTime,
                    form.EndTime
                );
            }
        }
        private void btnRemoveEquipment_Click(object sender, EventArgs e)
        {
            if (dataGridViewEquipment.CurrentRow == null)
                return;

            int idx = dataGridViewEquipment.CurrentRow.Index;
            if (idx < 0 || idx >= equipmentTable.Rows.Count)
                return;

            equipmentTable.Rows.RemoveAt(idx);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
