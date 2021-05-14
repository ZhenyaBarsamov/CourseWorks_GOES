namespace GOES.Problems.MaxBipartiteMatching {
    partial class FormMaxBipartiteMatchingProblem {
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
            this.groupBoxVisualization = new System.Windows.Forms.GroupBox();
            this.msaglGraphVisualizer = new SGVL.Visualizers.MsaglGraphVisualizer.MsaglGraphVisualizer();
            this.groupBoxHelp = new System.Windows.Forms.GroupBox();
            this.groupBoxTip = new System.Windows.Forms.GroupBox();
            this.groupBoxSolution = new System.Windows.Forms.GroupBox();
            this.buttonToStart = new System.Windows.Forms.Button();
            this.buttonNextStep = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxVisualization.SuspendLayout();
            this.groupBoxSolution.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxVisualization, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxHelp, 7, 9);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTip, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSolution, 7, 4);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1029, 563);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxVisualization
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxVisualization, 7);
            this.groupBoxVisualization.Controls.Add(this.msaglGraphVisualizer);
            this.groupBoxVisualization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxVisualization.Location = new System.Drawing.Point(3, 3);
            this.groupBoxVisualization.Name = "groupBoxVisualization";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxVisualization, 10);
            this.groupBoxVisualization.Size = new System.Drawing.Size(708, 557);
            this.groupBoxVisualization.TabIndex = 0;
            this.groupBoxVisualization.TabStop = false;
            this.groupBoxVisualization.Text = "Визуализация графа";
            // 
            // msaglGraphVisualizer
            // 
            this.msaglGraphVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msaglGraphVisualizer.InteractiveMode = SGVL.Visualizers.InteractiveMode.NonInteractive;
            this.msaglGraphVisualizer.IsInteractiveUpdating = false;
            this.msaglGraphVisualizer.IsVerticesMoving = true;
            this.msaglGraphVisualizer.Location = new System.Drawing.Point(3, 18);
            this.msaglGraphVisualizer.Name = "msaglGraphVisualizer";
            this.msaglGraphVisualizer.Size = new System.Drawing.Size(702, 536);
            this.msaglGraphVisualizer.TabIndex = 0;
            // 
            // groupBoxHelp
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxHelp, 3);
            this.groupBoxHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHelp.Location = new System.Drawing.Point(717, 507);
            this.groupBoxHelp.Name = "groupBoxHelp";
            this.groupBoxHelp.Size = new System.Drawing.Size(309, 53);
            this.groupBoxHelp.TabIndex = 1;
            this.groupBoxHelp.TabStop = false;
            this.groupBoxHelp.Text = "Помощь";
            // 
            // groupBoxTip
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxTip, 3);
            this.groupBoxTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTip.Location = new System.Drawing.Point(717, 3);
            this.groupBoxTip.Name = "groupBoxTip";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxTip, 4);
            this.groupBoxTip.Size = new System.Drawing.Size(309, 218);
            this.groupBoxTip.TabIndex = 2;
            this.groupBoxTip.TabStop = false;
            this.groupBoxTip.Text = "Подсказка";
            // 
            // groupBoxSolution
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxSolution, 3);
            this.groupBoxSolution.Controls.Add(this.buttonToStart);
            this.groupBoxSolution.Controls.Add(this.buttonNextStep);
            this.groupBoxSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSolution.Location = new System.Drawing.Point(717, 227);
            this.groupBoxSolution.Name = "groupBoxSolution";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxSolution, 3);
            this.groupBoxSolution.Size = new System.Drawing.Size(309, 162);
            this.groupBoxSolution.TabIndex = 3;
            this.groupBoxSolution.TabStop = false;
            this.groupBoxSolution.Text = "Решение";
            // 
            // buttonToStart
            // 
            this.buttonToStart.Location = new System.Drawing.Point(30, 96);
            this.buttonToStart.Name = "buttonToStart";
            this.buttonToStart.Size = new System.Drawing.Size(257, 46);
            this.buttonToStart.TabIndex = 1;
            this.buttonToStart.Text = "Начать заново";
            this.buttonToStart.UseVisualStyleBackColor = true;
            this.buttonToStart.Click += new System.EventHandler(this.buttonToStart_Click);
            // 
            // buttonNextStep
            // 
            this.buttonNextStep.Location = new System.Drawing.Point(30, 31);
            this.buttonNextStep.Name = "buttonNextStep";
            this.buttonNextStep.Size = new System.Drawing.Size(257, 46);
            this.buttonNextStep.TabIndex = 0;
            this.buttonNextStep.Text = "Следующий шаг";
            this.buttonNextStep.UseVisualStyleBackColor = true;
            this.buttonNextStep.Click += new System.EventHandler(this.buttonNextStep_Click);
            // 
            // FormMaximalBipartiteMatching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 563);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormMaximalBipartiteMatching";
            this.Text = "Задача о максимальном паросочетании в двудольном графе";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxVisualization.ResumeLayout(false);
            this.groupBoxSolution.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxVisualization;
        private System.Windows.Forms.GroupBox groupBoxHelp;
        private System.Windows.Forms.GroupBox groupBoxTip;
        private System.Windows.Forms.GroupBox groupBoxSolution;
        private SGVL.Visualizers.MsaglGraphVisualizer.MsaglGraphVisualizer msaglGraphVisualizer;
        private System.Windows.Forms.Button buttonToStart;
        private System.Windows.Forms.Button buttonNextStep;
    }
}