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
            this.btnAddIngredient = new System.Windows.Forms.Button();
            this.btnEditIngredient = new System.Windows.Forms.Button();
            this.btnDeleteIngredient = new System.Windows.Forms.Button();
            this.btnAddStep = new System.Windows.Forms.Button();
            this.btnEditStep = new System.Windows.Forms.Button();
            this.btnDeleteStep = new System.Windows.Forms.Button();
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
            // btnAddIngredient
            // 
            this.btnAddIngredient.Location = new System.Drawing.Point(282, 80);
            this.btnAddIngredient.Name = "btnAddIngredient";
            this.btnAddIngredient.Size = new System.Drawing.Size(156, 23);
            this.btnAddIngredient.TabIndex = 6;
            this.btnAddIngredient.Text = "Додати інгредієнт";
            this.btnAddIngredient.UseVisualStyleBackColor = true;
            this.btnAddIngredient.Click += new System.EventHandler(this.btnAddIngredient_Click);
            // 
            // btnEditIngredient
            // 
            this.btnEditIngredient.Location = new System.Drawing.Point(282, 125);
            this.btnEditIngredient.Name = "btnEditIngredient";
            this.btnEditIngredient.Size = new System.Drawing.Size(156, 23);
            this.btnEditIngredient.TabIndex = 7;
            this.btnEditIngredient.Text = "Редагувати";
            this.btnEditIngredient.UseVisualStyleBackColor = true;
            this.btnEditIngredient.Click += new System.EventHandler(this.btnEditIngredient_Click);
            // 
            // btnDeleteIngredient
            // 
            this.btnDeleteIngredient.Location = new System.Drawing.Point(282, 173);
            this.btnDeleteIngredient.Name = "btnDeleteIngredient";
            this.btnDeleteIngredient.Size = new System.Drawing.Size(156, 23);
            this.btnDeleteIngredient.TabIndex = 8;
            this.btnDeleteIngredient.Text = "Видалити";
            this.btnDeleteIngredient.UseVisualStyleBackColor = true;
            this.btnDeleteIngredient.Click += new System.EventHandler(this.btnDeleteIngredient_Click);
            // 
            // btnAddStep
            // 
            this.btnAddStep.Location = new System.Drawing.Point(282, 257);
            this.btnAddStep.Name = "btnAddStep";
            this.btnAddStep.Size = new System.Drawing.Size(156, 23);
            this.btnAddStep.TabIndex = 9;
            this.btnAddStep.Text = "Додати крок";
            this.btnAddStep.UseVisualStyleBackColor = true;
            this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);
            // 
            // btnEditStep
            // 
            this.btnEditStep.Location = new System.Drawing.Point(282, 299);
            this.btnEditStep.Name = "btnEditStep";
            this.btnEditStep.Size = new System.Drawing.Size(156, 23);
            this.btnEditStep.TabIndex = 10;
            this.btnEditStep.Text = "Редагувати";
            this.btnEditStep.UseVisualStyleBackColor = true;
            this.btnEditStep.Click += new System.EventHandler(this.btnEditStep_Click);
            // 
            // btnDeleteStep
            // 
            this.btnDeleteStep.Location = new System.Drawing.Point(282, 348);
            this.btnDeleteStep.Name = "btnDeleteStep";
            this.btnDeleteStep.Size = new System.Drawing.Size(156, 23);
            this.btnDeleteStep.TabIndex = 11;
            this.btnDeleteStep.Text = "Видалити";
            this.btnDeleteStep.UseVisualStyleBackColor = true;
            this.btnDeleteStep.Click += new System.EventHandler(this.btnDeleteStep_Click);
            // 
            // RecipeViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(450, 442);
            this.Controls.Add(this.btnDeleteStep);
            this.Controls.Add(this.btnEditStep);
            this.Controls.Add(this.btnAddStep);
            this.Controls.Add(this.btnDeleteIngredient);
            this.Controls.Add(this.btnEditIngredient);
            this.Controls.Add(this.btnAddIngredient);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridSteps);
            this.Controls.Add(this.lblSteps);
            this.Controls.Add(this.dataGridIngredients);
            this.Controls.Add(this.lblIngredients);
            this.Controls.Add(this.lblProductName);
            this.Name = "RecipeViewForm";
            this.Text = "Рецепт";
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
        private System.Windows.Forms.Button btnAddIngredient;
        private System.Windows.Forms.Button btnEditIngredient;
        private System.Windows.Forms.Button btnDeleteIngredient;
        private System.Windows.Forms.Button btnAddStep;
        private System.Windows.Forms.Button btnEditStep;
        private System.Windows.Forms.Button btnDeleteStep;
    }
}