namespace Kursova_BD
{
    partial class FilterProductsForm
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
            this.chkCategory = new System.Windows.Forms.CheckBox();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.chkWeight = new System.Windows.Forms.CheckBox();
            this.txtMinWeight = new System.Windows.Forms.TextBox();
            this.txtMaxWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkCategory
            // 
            this.chkCategory.AutoSize = true;
            this.chkCategory.Location = new System.Drawing.Point(34, 34);
            this.chkCategory.Name = "chkCategory";
            this.chkCategory.Size = new System.Drawing.Size(136, 17);
            this.chkCategory.TabIndex = 0;
            this.chkCategory.Text = "Фільтр за категорією";
            this.chkCategory.UseVisualStyleBackColor = true;
            // 
            // cbCategory
            // 
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(34, 58);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(136, 21);
            this.cbCategory.TabIndex = 1;
            // 
            // chkWeight
            // 
            this.chkWeight.AutoSize = true;
            this.chkWeight.Location = new System.Drawing.Point(34, 108);
            this.chkWeight.Name = "chkWeight";
            this.chkWeight.Size = new System.Drawing.Size(111, 17);
            this.chkWeight.TabIndex = 2;
            this.chkWeight.Text = "Фільтр за вагою";
            this.chkWeight.UseVisualStyleBackColor = true;
            // 
            // txtMinWeight
            // 
            this.txtMinWeight.Location = new System.Drawing.Point(34, 142);
            this.txtMinWeight.Name = "txtMinWeight";
            this.txtMinWeight.Size = new System.Drawing.Size(136, 20);
            this.txtMinWeight.TabIndex = 3;
            // 
            // txtMaxWeight
            // 
            this.txtMaxWeight.Location = new System.Drawing.Point(34, 181);
            this.txtMaxWeight.Name = "txtMaxWeight";
            this.txtMaxWeight.Size = new System.Drawing.Size(136, 20);
            this.txtMaxWeight.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Від";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "До";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(49, 289);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(121, 23);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Застосувати";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(237, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Закрити";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FilterProductsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(412, 360);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaxWeight);
            this.Controls.Add(this.txtMinWeight);
            this.Controls.Add(this.chkWeight);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.chkCategory);
            this.Name = "FilterProductsForm";
            this.Text = "Фільтр продуктів";
            this.Load += new System.EventHandler(this.FilterProductsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCategory;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.CheckBox chkWeight;
        private System.Windows.Forms.TextBox txtMinWeight;
        private System.Windows.Forms.TextBox txtMaxWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}