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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProblemSelector));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSolution = new System.Windows.Forms.Button();
            this.buttonDemonstration = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxProblems = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxProblems = new System.Windows.Forms.ListBox();
            this.labelProblemDescription = new System.Windows.Forms.Label();
            this.groupBoxExamples = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxExamples = new System.Windows.Forms.ListBox();
            this.labelExampleDescription = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxProblems.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxExamples.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.groupBoxExamples, 2, 0);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1039, 520);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonSolution
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonSolution, 2);
            this.buttonSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSolution.Location = new System.Drawing.Point(3, 419);
            this.buttonSolution.Name = "buttonSolution";
            this.buttonSolution.Size = new System.Drawing.Size(512, 46);
            this.buttonSolution.TabIndex = 1;
            this.buttonSolution.Text = "Решить задачу";
            this.buttonSolution.UseVisualStyleBackColor = true;
            this.buttonSolution.Click += new System.EventHandler(this.buttonSolution_Click);
            // 
            // buttonDemonstration
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonDemonstration, 2);
            this.buttonDemonstration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDemonstration.Location = new System.Drawing.Point(521, 419);
            this.buttonDemonstration.Name = "buttonDemonstration";
            this.buttonDemonstration.Size = new System.Drawing.Size(515, 46);
            this.buttonDemonstration.TabIndex = 2;
            this.buttonDemonstration.Text = "Посмотреть демонстрацию";
            this.buttonDemonstration.UseVisualStyleBackColor = true;
            this.buttonDemonstration.Click += new System.EventHandler(this.buttonDemonstration_Click);
            // 
            // buttonCancel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCancel, 2);
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(521, 471);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(515, 46);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxProblems
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxProblems, 2);
            this.groupBoxProblems.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxProblems.Location = new System.Drawing.Point(3, 3);
            this.groupBoxProblems.Name = "groupBoxProblems";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxProblems, 8);
            this.groupBoxProblems.Size = new System.Drawing.Size(512, 410);
            this.groupBoxProblems.TabIndex = 5;
            this.groupBoxProblems.TabStop = false;
            this.groupBoxProblems.Text = "Задачи";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.Controls.Add(this.listBoxProblems, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelProblemDescription, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(506, 387);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // listBoxProblems
            // 
            this.listBoxProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxProblems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxProblems.FormattingEnabled = true;
            this.listBoxProblems.ItemHeight = 18;
            this.listBoxProblems.Location = new System.Drawing.Point(3, 3);
            this.listBoxProblems.Name = "listBoxProblems";
            this.listBoxProblems.Size = new System.Drawing.Size(500, 187);
            this.listBoxProblems.TabIndex = 0;
            this.listBoxProblems.SelectedIndexChanged += new System.EventHandler(this.listBoxProblems_SelectedIndexChanged);
            // 
            // labelProblemDescription
            // 
            this.labelProblemDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProblemDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProblemDescription.Location = new System.Drawing.Point(3, 193);
            this.labelProblemDescription.Name = "labelProblemDescription";
            this.labelProblemDescription.Size = new System.Drawing.Size(500, 194);
            this.labelProblemDescription.TabIndex = 1;
            // 
            // groupBoxExamples
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxExamples, 2);
            this.groupBoxExamples.Controls.Add(this.tableLayoutPanel3);
            this.groupBoxExamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxExamples.Location = new System.Drawing.Point(521, 3);
            this.groupBoxExamples.Name = "groupBoxExamples";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxExamples, 8);
            this.groupBoxExamples.Size = new System.Drawing.Size(515, 410);
            this.groupBoxExamples.TabIndex = 6;
            this.groupBoxExamples.TabStop = false;
            this.groupBoxExamples.Text = "Примеры";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.Controls.Add(this.listBoxExamples, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelExampleDescription, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(509, 387);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // listBoxExamples
            // 
            this.listBoxExamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxExamples.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxExamples.FormattingEnabled = true;
            this.listBoxExamples.ItemHeight = 18;
            this.listBoxExamples.Location = new System.Drawing.Point(3, 3);
            this.listBoxExamples.Name = "listBoxExamples";
            this.listBoxExamples.Size = new System.Drawing.Size(503, 187);
            this.listBoxExamples.TabIndex = 4;
            this.listBoxExamples.SelectedIndexChanged += new System.EventHandler(this.listBoxExamples_SelectedIndexChanged);
            // 
            // labelExampleDescription
            // 
            this.labelExampleDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelExampleDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelExampleDescription.Location = new System.Drawing.Point(3, 193);
            this.labelExampleDescription.Name = "labelExampleDescription";
            this.labelExampleDescription.Size = new System.Drawing.Size(503, 194);
            this.labelExampleDescription.TabIndex = 5;
            // 
            // FormProblemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 520);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormProblemSelector";
            this.Text = "Выбор задачи";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxProblems.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBoxExamples.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBoxProblems;
        private System.Windows.Forms.Button buttonSolution;
        private System.Windows.Forms.Button buttonDemonstration;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBoxExamples;
        private System.Windows.Forms.GroupBox groupBoxProblems;
        private System.Windows.Forms.GroupBox groupBoxExamples;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelProblemDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelExampleDescription;
    }
}