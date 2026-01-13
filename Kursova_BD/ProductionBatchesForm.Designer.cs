namespace Kursova_BD
{
    partial class ProductionBatchesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewBatches = new System.Windows.Forms.DataGridView();
            this.btnAddBatch = new System.Windows.Forms.Button();
            this.btnEditBatch = new System.Windows.Forms.Button();
            this.btnDeleteBatch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBatches)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBatches
            // 
            this.dataGridViewBatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBatches.Location = new System.Drawing.Point(12, 27);
            this.dataGridViewBatches.Name = "dataGridViewBatches";
            this.dataGridViewBatches.Size = new System.Drawing.Size(629, 226);
            this.dataGridViewBatches.TabIndex = 0;
            // 
            // btnAddBatch
            // 
            this.btnAddBatch.Location = new System.Drawing.Point(36, 271);
            this.btnAddBatch.Name = "btnAddBatch";
            this.btnAddBatch.Size = new System.Drawing.Size(128, 23);
            this.btnAddBatch.TabIndex = 1;
            this.btnAddBatch.Text = "Додати партію";
            this.btnAddBatch.UseVisualStyleBackColor = true;
            this.btnAddBatch.Click += new System.EventHandler(this.btnAddBatch_Click);
            // 
            // btnEditBatch
            // 
            this.btnEditBatch.Location = new System.Drawing.Point(170, 270);
            this.btnEditBatch.Name = "btnEditBatch";
            this.btnEditBatch.Size = new System.Drawing.Size(127, 23);
            this.btnEditBatch.TabIndex = 2;
            this.btnEditBatch.Text = "Переглянути";
            this.btnEditBatch.UseVisualStyleBackColor = true;
            this.btnEditBatch.Click += new System.EventHandler(this.btnViewBatc_Click);
            // 
            // btnDeleteBatch
            // 
            this.btnDeleteBatch.Location = new System.Drawing.Point(303, 270);
            this.btnDeleteBatch.Name = "btnDeleteBatch";
            this.btnDeleteBatch.Size = new System.Drawing.Size(105, 23);
            this.btnDeleteBatch.TabIndex = 3;
            this.btnDeleteBatch.Text = "Видалити";
            this.btnDeleteBatch.UseVisualStyleBackColor = true;
            this.btnDeleteBatch.Click += new System.EventHandler(this.btnDeleteBatch_Click);
            // 
            // ProductionBatchesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(656, 347);
            this.Controls.Add(this.btnDeleteBatch);
            this.Controls.Add(this.btnEditBatch);
            this.Controls.Add(this.btnAddBatch);
            this.Controls.Add(this.dataGridViewBatches);
            this.Name = "ProductionBatchesForm";
            this.Text = "Журнал виробничих партій";
            this.Load += new System.EventHandler(this.ProductionBatchesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBatches)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBatches;
        private System.Windows.Forms.Button btnAddBatch;
        private System.Windows.Forms.Button btnEditBatch;
        private System.Windows.Forms.Button btnDeleteBatch;
    }
}