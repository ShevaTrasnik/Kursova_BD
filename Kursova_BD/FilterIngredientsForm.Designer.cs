namespace Kursova_BD
{
    partial class FilterIngredientsForm
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
            this.chkSupplier = new System.Windows.Forms.CheckBox();
            this.cbSupplier = new System.Windows.Forms.ComboBox();
            this.chkShelfLife = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpShelfLifeTo = new System.Windows.Forms.DateTimePicker();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkSupplier
            // 
            this.chkSupplier.AutoSize = true;
            this.chkSupplier.Location = new System.Drawing.Point(13, 23);
            this.chkSupplier.Name = "chkSupplier";
            this.chkSupplier.Size = new System.Drawing.Size(164, 17);
            this.chkSupplier.TabIndex = 0;
            this.chkSupplier.Text = "Фільтр за постачальником";
            this.chkSupplier.UseVisualStyleBackColor = true;
            this.chkSupplier.CheckedChanged += new System.EventHandler(this.chkSupplier_CheckedChanged);
            // 
            // cbSupplier
            // 
            this.cbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSupplier.FormattingEnabled = true;
            this.cbSupplier.Location = new System.Drawing.Point(13, 47);
            this.cbSupplier.Name = "cbSupplier";
            this.cbSupplier.Size = new System.Drawing.Size(121, 21);
            this.cbSupplier.TabIndex = 1;
            // 
            // chkShelfLife
            // 
            this.chkShelfLife.AutoSize = true;
            this.chkShelfLife.Location = new System.Drawing.Point(13, 102);
            this.chkShelfLife.Name = "chkShelfLife";
            this.chkShelfLife.Size = new System.Drawing.Size(190, 17);
            this.chkShelfLife.TabIndex = 2;
            this.chkShelfLife.Text = "Фільтр за терміном придатності";
            this.chkShelfLife.UseVisualStyleBackColor = true;
            this.chkShelfLife.CheckedChanged += new System.EventHandler(this.chkShelfLife_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "До";
            // 
            // dtpShelfLifeTo
            // 
            this.dtpShelfLifeTo.Location = new System.Drawing.Point(16, 143);
            this.dtpShelfLifeTo.Name = "dtpShelfLifeTo";
            this.dtpShelfLifeTo.Size = new System.Drawing.Size(200, 20);
            this.dtpShelfLifeTo.TabIndex = 4;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(40, 221);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(118, 23);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Застосувати";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(234, 221);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрити";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FilterIngredientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(380, 277);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.dtpShelfLifeTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkShelfLife);
            this.Controls.Add(this.cbSupplier);
            this.Controls.Add(this.chkSupplier);
            this.Name = "FilterIngredientsForm";
            this.Text = "FilterIngredientsForm";
            this.Load += new System.EventHandler(this.FilterIngredientsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSupplier;
        private System.Windows.Forms.ComboBox cbSupplier;
        private System.Windows.Forms.CheckBox chkShelfLife;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpShelfLifeTo;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
    }
}