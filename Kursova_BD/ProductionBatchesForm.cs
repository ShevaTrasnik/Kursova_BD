using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova_BD
{
    public partial class ProductionBatchesForm : Form
    {
        private readonly string _cs =
    "Server=127.0.0.1;Port=3306;Database=bakery_db_clean;User Id=root;Password=VFRcbvrf2007;SslMode=Preferred;AllowPublicKeyRetrieval=True;";
        public ProductionBatchesForm(string connectionString)
        {
            InitializeComponent();
            _cs = connectionString;
            LoadBatches();
        }
        private void LoadBatches()
        {
            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                string sql = @"
                SELECT 
                    b.batch_id,
                    b.date,
                    b.shift,
                    e.full_name AS employee,
                    b.total_weight
                FROM production_batches b
                JOIN employees e ON e.employee_id = b.employee_id
                ORDER BY b.date DESC, b.batch_id DESC";
                var adapter = new MySqlDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridViewBatches.DataSource = table;
                dataGridViewBatches.Columns["batch_id"].HeaderText = "№ партії";
                dataGridViewBatches.Columns["date"].HeaderText = "Дата";
                dataGridViewBatches.Columns["shift"].HeaderText = "Зміна";
                dataGridViewBatches.Columns["employee"].HeaderText = "Працівник";
                dataGridViewBatches.Columns["total_weight"].HeaderText = "Загальна вага";

                dataGridViewBatches.ReadOnly = true;
                dataGridViewBatches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewBatches.AllowUserToAddRows = false;
            }
        }
        private void ProductionBatchesForm_Load(object sender, EventArgs e)
        {

        }
        private void GenerateBatchPdfReport(int batchId)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PDF files (*.pdf)|*.pdf";
                dialog.FileName = $"Zvit_Partiia_{batchId}.pdf";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                using (var conn = new MySqlConnection(_cs))
                {
                    conn.Open();

                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Create))
                    {
                        Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();
                        string fontPath = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "Fonts",
                            "arial.ttf"
                        );

                        BaseFont baseFont = BaseFont.CreateFont(
                            fontPath,
                            BaseFont.IDENTITY_H,
                            BaseFont.EMBEDDED
                        );

                        iTextSharp.text.Font titleFont =
                            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);

                        iTextSharp.text.Font sectionFont =
                            new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);

                        iTextSharp.text.Font normalFont =
                            new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
                        Paragraph title = new Paragraph("Звіт виробничої партії", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);
                        doc.Add(new Paragraph("\n"));
                        var cmdBatch = new MySqlCommand(@"
                    SELECT 
                        pb.batch_id,
                        pb.date,
                        pb.shift,
                        IFNULL(e.full_name, '—') AS employee_name
                    FROM production_batches pb
                    LEFT JOIN employees e ON e.employee_id = pb.employee_id
                    WHERE pb.batch_id = @id", conn);

                        cmdBatch.Parameters.AddWithValue("@id", batchId);

                        using (var reader = cmdBatch.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                doc.Add(new Paragraph($"Номер партії: {reader["batch_id"]}", normalFont));
                                doc.Add(new Paragraph($"Дата: {Convert.ToDateTime(reader["date"]).ToShortDateString()}", normalFont));
                                doc.Add(new Paragraph($"Зміна: {reader["shift"]}", normalFont));
                                doc.Add(new Paragraph($"Працівник: {reader["employee_name"]}", normalFont));
                            }
                        }
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph("Виготовлена продукція", sectionFont));

                        PdfPTable productsTable = new PdfPTable(3);
                        productsTable.WidthPercentage = 100;

                        productsTable.AddCell(new Phrase("Виріб", sectionFont));
                        productsTable.AddCell(new Phrase("Кількість", sectionFont));
                        productsTable.AddCell(new Phrase("Вага", sectionFont));

                        decimal totalWeight = 0;

                        var cmdProducts = new MySqlCommand(@"
                    SELECT 
                        IFNULL(bp.name, '—') AS product_name,
                        pi.produced_qty,
                        IFNULL(bp.weight, 0) AS unit_weight
                    FROM production_items pi
                    LEFT JOIN bakery_products bp ON bp.product_id = pi.product_id
                    WHERE pi.batch_id = @id", conn);

                        cmdProducts.Parameters.AddWithValue("@id", batchId);

                        using (var reader = cmdProducts.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productName = reader["product_name"].ToString();
                                decimal qty = Convert.ToDecimal(reader["produced_qty"]);
                                decimal unitWeight = Convert.ToDecimal(reader["unit_weight"]);

                                decimal productWeight = qty * unitWeight;
                                totalWeight += productWeight;

                                productsTable.AddCell(new Phrase(productName, normalFont));
                                productsTable.AddCell(new Phrase(qty.ToString(), normalFont));
                                productsTable.AddCell(new Phrase(productWeight.ToString("0.00"), normalFont));
                            }
                        }

                        doc.Add(productsTable);
                        doc.Add(new Paragraph($"Загальна вага партії: {totalWeight:0.00}", sectionFont));
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph("Використане обладнання", sectionFont));

                        PdfPTable equipTable = new PdfPTable(3);
                        equipTable.WidthPercentage = 100;

                        equipTable.AddCell(new Phrase("Обладнання", sectionFont));
                        equipTable.AddCell(new Phrase("Початок", sectionFont));
                        equipTable.AddCell(new Phrase("Завершення", sectionFont));

                        var cmdEquip = new MySqlCommand(@"
                    SELECT 
                        IFNULL(e.name, CONCAT('ID ', ue.equipment_id)) AS equipment_name,
                        ue.start_time,
                        ue.end_time
                    FROM used_equipment ue
                    LEFT JOIN equipment e ON e.equipment_id = ue.equipment_id
                    WHERE ue.batch_id = @id", conn);

                        cmdEquip.Parameters.AddWithValue("@id", batchId);

                        using (var reader = cmdEquip.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                equipTable.AddCell(new Phrase(reader["equipment_name"].ToString(), normalFont));
                                equipTable.AddCell(new Phrase(reader["start_time"].ToString(), normalFont));
                                equipTable.AddCell(new Phrase(reader["end_time"].ToString(), normalFont));
                            }
                        }

                        doc.Add(equipTable);
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph(
                            $"Звіт сформовано: {DateTime.Now}",
                            normalFont));

                        doc.Close();
                    }
                }

                MessageBox.Show("PDF-звіт успішно сформовано");
            }
        }



        private void btnAddBatch_Click(object sender, EventArgs e)
        {
            var form = new ProductionBatchForm(_cs);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadBatches();
            }
        }

        private void btnViewBatc_Click(object sender, EventArgs e)
        {
            if (dataGridViewBatches.CurrentRow == null)
                return;
            int batchId = Convert.ToInt32(
                dataGridViewBatches.CurrentRow.Cells["batch_id"].Value
            );
            var form = new ProductionBatchForm(_cs, batchId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadBatches();
            }
        }

        private void btnDeleteBatch_Click(object sender, EventArgs e)
        {
            if (dataGridViewBatches.CurrentRow == null)
                return;

            int batchId = Convert.ToInt32(
                dataGridViewBatches.CurrentRow.Cells["batch_id"].Value
            );

            if (MessageBox.Show(
                "Видалити партію разом з усіма виробами та обладнанням?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            ) != DialogResult.Yes)
                return;

            using (var conn = new MySqlConnection(_cs))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd1 = new MySqlCommand(
                            "DELETE FROM production_items WHERE batch_id = @id", conn, tx))
                        {
                            cmd1.Parameters.AddWithValue("@id", batchId);
                            cmd1.ExecuteNonQuery();
                        }

                        using (var cmd2 = new MySqlCommand(
                            "DELETE FROM used_equipment WHERE batch_id = @id", conn, tx))
                        {
                            cmd2.Parameters.AddWithValue("@id", batchId);
                            cmd2.ExecuteNonQuery();
                        }

                        using (var cmd3 = new MySqlCommand(
                            "DELETE FROM production_batches WHERE batch_id = @id", conn, tx))
                        {
                            cmd3.Parameters.AddWithValue("@id", batchId);
                            cmd3.ExecuteNonQuery();
                        }

                        tx.Commit();
                        LoadBatches();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Помилка видалення:\n" + ex.Message);
                    }
                }
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (dataGridViewBatches.CurrentRow == null)
            {
                MessageBox.Show("Оберіть виробничу партію");
                return;
            }

            int batchId = Convert.ToInt32(
                dataGridViewBatches.CurrentRow.Cells["batch_id"].Value
            );

            GenerateBatchPdfReport(batchId);
        }

    }
}
