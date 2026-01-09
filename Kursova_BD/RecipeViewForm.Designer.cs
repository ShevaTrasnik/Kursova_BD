namespace Kursova_BD
{
    partial class RecipeViewForm
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblIngredients = new System.Windows.Forms.Label();
            this.dataGridIngredients = new System.Windows.Forms.DataGridView();
            this.lblSteps = new System.Windows.Forms.Label();
            this.dataGridSteps = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridIngredients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSteps)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(22, 24);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(87, 13);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Назва продукту";
            // 
            // lblIngredients
            // 
            this.lblIngredients.AutoSize = true;
            this.lblIngredients.Location = new System.Drawing.Point(22, 64);
            this.lblIngredients.Name = "lblIngredients";
            this.lblIngredients.Size = new System.Drawing.Size(64, 13);
            this.lblIngredients.TabIndex = 1;
            this.lblIngredients.Text = "Інгредієнти";
            // 
            // dataGridIngredients
            // 
            this.dataGridIngredients.AllowUserToAddRows = false;
            this.dataGridIngredients.AllowUserToDeleteRows = false;
            this.dataGridIngredients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridIngredients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridIngredients.Location = new System.Drawing.Point(25, 80);
            this.dataGridIngredients.MultiSelect = false;
            this.dataGridIngredients.Name = "dataGridIngredients";
            this.dataGridIngredients.ReadOnly = true;
            this.dataGridIngredients.RowHeadersVisible = false;
            this.dataGridIngredients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridIngredients.Size = new System.Drawing.Size(240, 132);
            this.dataGridIngredients.TabIndex = 2;
            // 
            // lblSteps
            // 
            this.lblSteps.AutoSize = true;
            this.lblSteps.Location = new System.Drawing.Point(22, 241);
            this.lblSteps.Name = "lblSteps";
            this.lblSteps.Size = new System.Drawing.Size(134, 13);
            this.lblSteps.TabIndex = 3;
            this.lblSteps.Text = "Технологія приготування";
            // 
            // dataGridSteps
            // 
            this.dataGridSteps.AllowUserToAddRows = false;
            this.dataGridSteps.AllowUserToDeleteRows = false;
            this.dataGridSteps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSteps.Location = new System.Drawing.Point(25, 257);
            this.dataGridSteps.MultiSelect = false;
            this.dataGridSteps.Name = "dataGridSteps";
            this.dataGridSteps.ReadOnly = true;
            this.dataGridSteps.RowHeadersVisible = false;
            this.dataGridSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridSteps.Size = new System.Drawing.Size(240, 132);
            this.dataGridSteps.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(267, 405);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(171, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Закрити";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // RecipeViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(450, 442);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridSteps);
            this.Controls.Add(this.lblSteps);
            this.Controls.Add(this.dataGridIngredients);
            this.Controls.Add(this.lblIngredients);
            this.Controls.Add(this.lblProductName);
            this.Name = "RecipeViewForm";
            this.Text = "RecipeViewForm";
            this.Load += new System.EventHandler(this.RecipeViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridIngredients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSteps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblIngredients;
        private System.Windows.Forms.DataGridView dataGridIngredients;
        private System.Windows.Forms.Label lblSteps;
        private System.Windows.Forms.DataGridView dataGridSteps;
        private System.Windows.Forms.Button btnClose;
    }
}