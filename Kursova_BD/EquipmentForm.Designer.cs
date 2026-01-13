namespace Kursova_BD
{
    partial class EquipmentForm
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
            this.dataGridViewEquipment = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTypeInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStatusInfo = new System.Windows.Forms.TextBox();
            this.dtpServiceDateInfo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnResetFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipment)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEquipment
            // 
            this.dataGridViewEquipment.AllowUserToAddRows = false;
            this.dataGridViewEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEquipment.Location = new System.Drawing.Point(13, 69);
            this.dataGridViewEquipment.Name = "dataGridViewEquipment";
            this.dataGridViewEquipment.ReadOnly = true;
            this.dataGridViewEquipment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEquipment.Size = new System.Drawing.Size(685, 177);
            this.dataGridViewEquipment.TabIndex = 0;
            this.dataGridViewEquipment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEquipment_CellContentClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(105, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Додати";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(124, 32);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(105, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Редагувати";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(235, 32);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(105, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Видалити";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(409, 32);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(121, 20);
            this.txtSearch.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(536, 32);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Шукати";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(623, 32);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(75, 23);
            this.btnClearSearch.TabIndex = 6;
            this.btnClearSearch.Text = "Очистити";
            this.btnClearSearch.UseVisualStyleBackColor = true;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Назва";
            // 
            // txtNameInfo
            // 
            this.txtNameInfo.Location = new System.Drawing.Point(18, 308);
            this.txtNameInfo.Name = "txtNameInfo";
            this.txtNameInfo.ReadOnly = true;
            this.txtNameInfo.Size = new System.Drawing.Size(157, 20);
            this.txtNameInfo.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Тип";
            // 
            // txtTypeInfo
            // 
            this.txtTypeInfo.Location = new System.Drawing.Point(218, 308);
            this.txtTypeInfo.Name = "txtTypeInfo";
            this.txtTypeInfo.ReadOnly = true;
            this.txtTypeInfo.Size = new System.Drawing.Size(157, 20);
            this.txtTypeInfo.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Стан";
            // 
            // txtStatusInfo
            // 
            this.txtStatusInfo.Location = new System.Drawing.Point(409, 308);
            this.txtStatusInfo.Name = "txtStatusInfo";
            this.txtStatusInfo.ReadOnly = true;
            this.txtStatusInfo.Size = new System.Drawing.Size(157, 20);
            this.txtStatusInfo.TabIndex = 12;
            // 
            // dtpServiceDateInfo
            // 
            this.dtpServiceDateInfo.Location = new System.Drawing.Point(18, 357);
            this.dtpServiceDateInfo.Name = "dtpServiceDateInfo";
            this.dtpServiceDateInfo.Size = new System.Drawing.Size(200, 20);
            this.dtpServiceDateInfo.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Дата обслуговування";
            // 
            // cmbSort
            // 
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(254, 252);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(121, 21);
            this.cmbSort.TabIndex = 15;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Сортування";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(455, 250);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 17;
            this.btnFilter.Text = "Фільтр";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(536, 250);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(100, 23);
            this.btnResetFilter.TabIndex = 18;
            this.btnResetFilter.Text = "Очистити фільтр";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // EquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(710, 419);
            this.Controls.Add(this.btnResetFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpServiceDateInfo);
            this.Controls.Add(this.txtStatusInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTypeInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNameInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridViewEquipment);
            this.Name = "EquipmentForm";
            this.Text = "Обладнання";
            this.Load += new System.EventHandler(this.EquipmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEquipment;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTypeInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStatusInfo;
        private System.Windows.Forms.DateTimePicker dtpServiceDateInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnResetFilter;
    }
}