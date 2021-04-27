namespace GOES.Forms {
    partial class FormProblemSelector {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSolution = new System.Windows.Forms.Button();
            this.buttonDemonstration = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxProblems = new System.Windows.Forms.GroupBox();
            this.listBoxProblems = new System.Windows.Forms.ListBox();
            this.groupBoxStatements = new System.Windows.Forms.GroupBox();
            this.listBoxProblemStatements = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxProblems.SuspendLayout();
            this.groupBoxStatements.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.buttonSolution, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.buttonDemonstration, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxProblems, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxStatements, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonSolution
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonSolution, 2);
            this.buttonSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSolution.Location = new System.Drawing.Point(3, 363);
            this.buttonSolution.Name = "buttonSolution";
            this.buttonSolution.Size = new System.Drawing.Size(394, 39);
            this.buttonSolution.TabIndex = 1;
            this.buttonSolution.Text = "Решить задачу";
            this.buttonSolution.UseVisualStyleBackColor = true;
            this.buttonSolution.Click += new System.EventHandler(this.buttonSolution_Click);
            // 
            // buttonDemonstration
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonDemonstration, 2);
            this.buttonDemonstration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDemonstration.Location = new System.Drawing.Point(403, 363);
            this.buttonDemonstration.Name = "buttonDemonstration";
            this.buttonDemonstration.Size = new System.Drawing.Size(394, 39);
            this.buttonDemonstration.TabIndex = 2;
            this.buttonDemonstration.Text = "Посмотреть демонстрацию";
            this.buttonDemonstration.UseVisualStyleBackColor = true;
            this.buttonDemonstration.Click += new System.EventHandler(this.buttonDemonstration_Click);
            // 
            // buttonCancel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCancel, 2);
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(403, 408);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(394, 39);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxProblems
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxProblems, 2);
            this.groupBoxProblems.Controls.Add(this.listBoxProblems);
            this.groupBoxProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxProblems.Location = new System.Drawing.Point(3, 3);
            this.groupBoxProblems.Name = "groupBoxProblems";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxProblems, 8);
            this.groupBoxProblems.Size = new System.Drawing.Size(394, 354);
            this.groupBoxProblems.TabIndex = 5;
            this.groupBoxProblems.TabStop = false;
            this.groupBoxProblems.Text = "Задачи";
            // 
            // listBoxProblems
            // 
            this.listBoxProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxProblems.FormattingEnabled = true;
            this.listBoxProblems.ItemHeight = 16;
            this.listBoxProblems.Location = new System.Drawing.Point(3, 18);
            this.listBoxProblems.Name = "listBoxProblems";
            this.listBoxProblems.Size = new System.Drawing.Size(388, 333);
            this.listBoxProblems.TabIndex = 0;
            this.listBoxProblems.SelectedIndexChanged += new System.EventHandler(this.listBoxProblems_SelectedIndexChanged);
            // 
            // groupBoxStatements
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxStatements, 2);
            this.groupBoxStatements.Controls.Add(this.listBoxProblemStatements);
            this.groupBoxStatements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxStatements.Location = new System.Drawing.Point(403, 3);
            this.groupBoxStatements.Name = "groupBoxStatements";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxStatements, 8);
            this.groupBoxStatements.Size = new System.Drawing.Size(394, 354);
            this.groupBoxStatements.TabIndex = 6;
            this.groupBoxStatements.TabStop = false;
            this.groupBoxStatements.Text = "Примеры";
            // 
            // listBoxProblemStatements
            // 
            this.listBoxProblemStatements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxProblemStatements.FormattingEnabled = true;
            this.listBoxProblemStatements.ItemHeight = 16;
            this.listBoxProblemStatements.Location = new System.Drawing.Point(3, 18);
            this.listBoxProblemStatements.Name = "listBoxProblemStatements";
            this.listBoxProblemStatements.Size = new System.Drawing.Size(388, 333);
            this.listBoxProblemStatements.TabIndex = 4;
            this.listBoxProblemStatements.SelectedIndexChanged += new System.EventHandler(this.listBoxProblemStatements_SelectedIndexChanged);
            // 
            // FormProblemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormProblemSelector";
            this.Text = "Выбор задачи";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxProblems.ResumeLayout(false);
            this.groupBoxStatements.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBoxProblems;
        private System.Windows.Forms.Button buttonSolution;
        private System.Windows.Forms.Button buttonDemonstration;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBoxProblemStatements;
        private System.Windows.Forms.GroupBox groupBoxProblems;
        private System.Windows.Forms.GroupBox groupBoxStatements;
    }
}