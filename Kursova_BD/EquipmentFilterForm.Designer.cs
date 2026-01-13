namespace Kursova_BD
{
    partial class EquipmentFilterForm
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
            this.chkName = new System.Windows.Forms.CheckBox();
            this.chkType = new System.Windows.Forms.CheckBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.chkServiceDate = new System.Windows.Forms.CheckBox();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkName
            // 
            this.chkName.AutoSize = true;
            this.chkName.Location = new System.Drawing.Point(21, 33);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(118, 17);
            this.chkName.TabIndex = 0;
            this.chkName.Text = "Фільтр за назвою";
            this.chkName.UseVisualStyleBackColor = true;
            this.chkName.CheckedChanged += new System.EventHandler(this.chkName_CheckedChanged);
            // 
            // chkType
            // 
            this.chkType.AutoSize = true;
            this.chkType.Location = new System.Drawing.Point(21, 113);
            this.chkType.Name = "chkType";
            this.chkType.Size = new System.Drawing.Size(111, 17);
            this.chkType.TabIndex = 1;
            this.chkType.Text = "Фільтр за типом";
            this.chkType.UseVisualStyleBackColor = true;
            this.chkType.CheckedChanged += new System.EventHandler(this.chkType_CheckedChanged);
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(21, 196);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(117, 17);
            this.chkStatus.TabIndex = 2;
            this.chkStatus.Text = "Фільтр за станом";
            this.chkStatus.UseVisualStyleBackColor = true;
            this.chkStatus.CheckedChanged += new System.EventHandler(this.chkStatus_CheckedChanged);
            // 
            // chkServiceDate
            // 
            this.chkServiceDate.AutoSize = true;
            this.chkServiceDate.Location = new System.Drawing.Point(21, 273);
            this.chkServiceDate.Name = "chkServiceDate";
            this.chkServiceDate.Size = new System.Drawing.Size(195, 17);
            this.chkServiceDate.TabIndex = 3;
            this.chkServiceDate.Text = "Фільтр за датою обслуговування";
            this.chkServiceDate.UseVisualStyleBackColor = true;
            this.chkServiceDate.CheckedChanged += new System.EventHandler(this.chkServiceDate_CheckedChanged);
            // 
            // cmbName
            // 
            this.cmbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(31, 56);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(121, 21);
            this.cmbName.TabIndex = 4;
            this.cmbName.SelectedIndexChanged += new System.EventHandler(this.cmbName_SelectedIndexChanged);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(31, 137);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 21);
            this.cmbType.TabIndex = 5;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(31, 220);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbStatus.TabIndex = 6;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(31, 309);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 7;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(31, 365);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 293);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Від";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "До";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(64, 416);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "Застосувати";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(223, 416);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Закрити";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // EquipmentFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(371, 479);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbName);
            this.Controls.Add(this.chkServiceDate);
            this.Controls.Add(this.chkStatus);
            this.Controls.Add(this.chkType);
            this.Controls.Add(this.chkName);
            this.Name = "EquipmentFilterForm";
            this.Text = "Фільтр обладнання";
            this.Load += new System.EventHandler(this.EquipmentFilterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkName;
        private System.Windows.Forms.CheckBox chkType;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.CheckBox chkServiceDate;
        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
    }
}