namespace Kursova_BD
{
    partial class StatisticsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelBatchesText = new System.Windows.Forms.Label();
            this.lblBatchCount = new System.Windows.Forms.Label();
            this.labelTotalWeightText = new System.Windows.Forms.Label();
            this.lblTotalWeight = new System.Windows.Forms.Label();
            this.lblAvgWeight = new System.Windows.Forms.Label();
            this.labelAvgWeightText = new System.Windows.Forms.Label();
            this.lblMaxWeight = new System.Windows.Forms.Label();
            this.labelMaxWeightText = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Статистика виробництва";
            // 
            // labelBatchesText
            // 
            this.labelBatchesText.AutoSize = true;
            this.labelBatchesText.Location = new System.Drawing.Point(16, 76);
            this.labelBatchesText.Name = "labelBatchesText";
            this.labelBatchesText.Size = new System.Drawing.Size(151, 13);
            this.labelBatchesText.TabIndex = 1;
            this.labelBatchesText.Text = "Кількість виробничих партій:";
            // 
            // lblBatchCount
            // 
            this.lblBatchCount.AutoSize = true;
            this.lblBatchCount.Location = new System.Drawing.Point(237, 76);
            this.lblBatchCount.Name = "lblBatchCount";
            this.lblBatchCount.Size = new System.Drawing.Size(13, 13);
            this.lblBatchCount.TabIndex = 2;
            this.lblBatchCount.Text = "0";
            // 
            // labelTotalWeightText
            // 
            this.labelTotalWeightText.AutoSize = true;
            this.labelTotalWeightText.Location = new System.Drawing.Point(16, 122);
            this.labelTotalWeightText.Name = "labelTotalWeightText";
            this.labelTotalWeightText.Size = new System.Drawing.Size(133, 13);
            this.labelTotalWeightText.TabIndex = 3;
            this.labelTotalWeightText.Text = "Загальна вага продукції:";
            // 
            // lblTotalWeight
            // 
            this.lblTotalWeight.AutoSize = true;
            this.lblTotalWeight.Location = new System.Drawing.Point(237, 122);
            this.lblTotalWeight.Name = "lblTotalWeight";
            this.lblTotalWeight.Size = new System.Drawing.Size(13, 13);
            this.lblTotalWeight.TabIndex = 4;
            this.lblTotalWeight.Text = "0";
            // 
            // lblAvgWeight
            // 
            this.lblAvgWeight.AutoSize = true;
            this.lblAvgWeight.Location = new System.Drawing.Point(237, 174);
            this.lblAvgWeight.Name = "lblAvgWeight";
            this.lblAvgWeight.Size = new System.Drawing.Size(13, 13);
            this.lblAvgWeight.TabIndex = 6;
            this.lblAvgWeight.Text = "0";
            // 
            // labelAvgWeightText
            // 
            this.labelAvgWeightText.AutoSize = true;
            this.labelAvgWeightText.Location = new System.Drawing.Point(16, 174);
            this.labelAvgWeightText.Name = "labelAvgWeightText";
            this.labelAvgWeightText.Size = new System.Drawing.Size(110, 13);
            this.labelAvgWeightText.TabIndex = 5;
            this.labelAvgWeightText.Text = "Середня вага партії:";
            // 
            // lblMaxWeight
            // 
            this.lblMaxWeight.AutoSize = true;
            this.lblMaxWeight.Location = new System.Drawing.Point(237, 221);
            this.lblMaxWeight.Name = "lblMaxWeight";
            this.lblMaxWeight.Size = new System.Drawing.Size(13, 13);
            this.lblMaxWeight.TabIndex = 8;
            this.lblMaxWeight.Text = "0";
            // 
            // labelMaxWeightText
            // 
            this.labelMaxWeightText.AutoSize = true;
            this.labelMaxWeightText.Location = new System.Drawing.Point(16, 221);
            this.labelMaxWeightText.Name = "labelMaxWeightText";
            this.labelMaxWeightText.Size = new System.Drawing.Size(138, 13);
            this.labelMaxWeightText.TabIndex = 7;
            this.labelMaxWeightText.Text = "Максимальна вага партії:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(51, 317);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(103, 23);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Оновити";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(240, 317);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Закрити";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(411, 365);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblMaxWeight);
            this.Controls.Add(this.labelMaxWeightText);
            this.Controls.Add(this.lblAvgWeight);
            this.Controls.Add(this.labelAvgWeightText);
            this.Controls.Add(this.lblTotalWeight);
            this.Controls.Add(this.labelTotalWeightText);
            this.Controls.Add(this.lblBatchCount);
            this.Controls.Add(this.labelBatchesText);
            this.Controls.Add(this.label1);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBatchesText;
        private System.Windows.Forms.Label lblBatchCount;
        private System.Windows.Forms.Label labelTotalWeightText;
        private System.Windows.Forms.Label lblTotalWeight;
        private System.Windows.Forms.Label lblAvgWeight;
        private System.Windows.Forms.Label labelAvgWeightText;
        private System.Windows.Forms.Label lblMaxWeight;
        private System.Windows.Forms.Label labelMaxWeightText;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
    }
}