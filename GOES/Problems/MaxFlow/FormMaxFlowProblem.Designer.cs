namespace GOES.Problems.MaxFlow {
    partial class FormMaxFlowProblem {
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
            this.graphVisualizer = new SGVL.Visualizers.SimpleGraphVisualizer.SimpleGraphVisualizer();
            this.groupBoxTip = new System.Windows.Forms.GroupBox();
            this.textLabelTip = new GOES.Controls.TextLabel();
            this.groupBoxHelp = new System.Windows.Forms.GroupBox();
            this.buttonLecture = new System.Windows.Forms.Button();
            this.groupBoxAnswers = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.buttonAcceptAnswer = new System.Windows.Forms.Button();
            this.groupBoxSolution = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonReloadIteration = new System.Windows.Forms.Button();
            this.buttonReloadProblem = new System.Windows.Forms.Button();
            this.groupBoxExampleDescription = new System.Windows.Forms.GroupBox();
            this.textLabelExampleDescription = new GOES.Controls.TextLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxVisualization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphVisualizer)).BeginInit();
            this.groupBoxTip.SuspendLayout();
            this.groupBoxHelp.SuspendLayout();
            this.groupBoxAnswers.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxSolution.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBoxExampleDescription.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTip, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxHelp, 7, 9);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxAnswers, 7, 5);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSolution, 7, 7);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxExampleDescription, 7, 4);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1029, 569);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxVisualization
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxVisualization, 7);
            this.groupBoxVisualization.Controls.Add(this.graphVisualizer);
            this.groupBoxVisualization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxVisualization.Location = new System.Drawing.Point(3, 3);
            this.groupBoxVisualization.Name = "groupBoxVisualization";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxVisualization, 10);
            this.groupBoxVisualization.Size = new System.Drawing.Size(708, 563);
            this.groupBoxVisualization.TabIndex = 0;
            this.groupBoxVisualization.TabStop = false;
            this.groupBoxVisualization.Text = "Визуализация графа";
            // 
            // graphVisualizer
            // 
            this.graphVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphVisualizer.InteractiveMode = SGVL.Visualizers.InteractiveMode.NonInteractive;
            this.graphVisualizer.IsInteractiveUpdating = false;
            this.graphVisualizer.IsVerticesMoving = false;
            this.graphVisualizer.Location = new System.Drawing.Point(3, 20);
            this.graphVisualizer.Name = "graphVisualizer";
            this.graphVisualizer.Size = new System.Drawing.Size(702, 540);
            this.graphVisualizer.TabIndex = 0;
            this.graphVisualizer.TabStop = false;
            // 
            // groupBoxTip
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxTip, 3);
            this.groupBoxTip.Controls.Add(this.textLabelTip);
            this.groupBoxTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTip.Location = new System.Drawing.Point(717, 3);
            this.groupBoxTip.Name = "groupBoxTip";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxTip, 4);
            this.groupBoxTip.Size = new System.Drawing.Size(309, 218);
            this.groupBoxTip.TabIndex = 1;
            this.groupBoxTip.TabStop = false;
            this.groupBoxTip.Text = "Сообщения";
            // 
            // textLabelTip
            // 
            this.textLabelTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLabelTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLabelTip.Location = new System.Drawing.Point(3, 20);
            this.textLabelTip.Multiline = true;
            this.textLabelTip.Name = "textLabelTip";
            this.textLabelTip.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLabelTip.Size = new System.Drawing.Size(303, 195);
            this.textLabelTip.TabIndex = 0;
            // 
            // groupBoxHelp
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxHelp, 3);
            this.groupBoxHelp.Controls.Add(this.buttonLecture);
            this.groupBoxHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHelp.Location = new System.Drawing.Point(717, 507);
            this.groupBoxHelp.Name = "groupBoxHelp";
            this.groupBoxHelp.Size = new System.Drawing.Size(309, 59);
            this.groupBoxHelp.TabIndex = 4;
            this.groupBoxHelp.TabStop = false;
            this.groupBoxHelp.Text = "Помощь";
            // 
            // buttonLecture
            // 
            this.buttonLecture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLecture.Location = new System.Drawing.Point(3, 20);
            this.buttonLecture.Name = "buttonLecture";
            this.buttonLecture.Size = new System.Drawing.Size(303, 36);
            this.buttonLecture.TabIndex = 0;
            this.buttonLecture.Text = "Текст лекции";
            this.buttonLecture.UseVisualStyleBackColor = true;
            this.buttonLecture.Click += new System.EventHandler(this.buttonLecture_Click);
            // 
            // groupBoxAnswers
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxAnswers, 3);
            this.groupBoxAnswers.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxAnswers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAnswers.Location = new System.Drawing.Point(717, 283);
            this.groupBoxAnswers.Name = "groupBoxAnswers";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxAnswers, 2);
            this.groupBoxAnswers.Size = new System.Drawing.Size(309, 106);
            this.groupBoxAnswers.TabIndex = 3;
            this.groupBoxAnswers.TabStop = false;
            this.groupBoxAnswers.Text = "Ответы";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxAnswer, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonAcceptAnswer, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(303, 83);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAnswer.Location = new System.Drawing.Point(3, 3);
            this.textBoxAnswer.Multiline = true;
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(297, 35);
            this.textBoxAnswer.TabIndex = 1;
            // 
            // buttonAcceptAnswer
            // 
            this.buttonAcceptAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAcceptAnswer.Location = new System.Drawing.Point(3, 44);
            this.buttonAcceptAnswer.Name = "buttonAcceptAnswer";
            this.buttonAcceptAnswer.Size = new System.Drawing.Size(297, 36);
            this.buttonAcceptAnswer.TabIndex = 1;
            this.buttonAcceptAnswer.Text = "Принять ответ";
            this.buttonAcceptAnswer.UseVisualStyleBackColor = true;
            this.buttonAcceptAnswer.Click += new System.EventHandler(this.buttonAcceptAnswer_Click);
            // 
            // groupBoxSolution
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxSolution, 3);
            this.groupBoxSolution.Controls.Add(this.tableLayoutPanel3);
            this.groupBoxSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSolution.Location = new System.Drawing.Point(717, 395);
            this.groupBoxSolution.Name = "groupBoxSolution";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxSolution, 2);
            this.groupBoxSolution.Size = new System.Drawing.Size(309, 106);
            this.groupBoxSolution.TabIndex = 5;
            this.groupBoxSolution.TabStop = false;
            this.groupBoxSolution.Text = "Ход решения";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.buttonReloadIteration, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonReloadProblem, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(303, 83);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // buttonReloadIteration
            // 
            this.buttonReloadIteration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReloadIteration.Location = new System.Drawing.Point(3, 3);
            this.buttonReloadIteration.Name = "buttonReloadIteration";
            this.buttonReloadIteration.Size = new System.Drawing.Size(297, 35);
            this.buttonReloadIteration.TabIndex = 0;
            this.buttonReloadIteration.Text = "К началу итерации";
            this.buttonReloadIteration.UseVisualStyleBackColor = true;
            this.buttonReloadIteration.Click += new System.EventHandler(this.buttonReloadIteration_Click);
            // 
            // buttonReloadProblem
            // 
            this.buttonReloadProblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReloadProblem.Location = new System.Drawing.Point(3, 44);
            this.buttonReloadProblem.Name = "buttonReloadProblem";
            this.buttonReloadProblem.Size = new System.Drawing.Size(297, 36);
            this.buttonReloadProblem.TabIndex = 1;
            this.buttonReloadProblem.Text = "Начать заново";
            this.buttonReloadProblem.UseVisualStyleBackColor = true;
            this.buttonReloadProblem.Click += new System.EventHandler(this.buttonReloadProblem_Click);
            // 
            // groupBoxExampleDescription
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxExampleDescription, 3);
            this.groupBoxExampleDescription.Controls.Add(this.textLabelExampleDescription);
            this.groupBoxExampleDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxExampleDescription.Location = new System.Drawing.Point(717, 227);
            this.groupBoxExampleDescription.Name = "groupBoxExampleDescription";
            this.groupBoxExampleDescription.Size = new System.Drawing.Size(309, 50);
            this.groupBoxExampleDescription.TabIndex = 6;
            this.groupBoxExampleDescription.TabStop = false;
            this.groupBoxExampleDescription.Text = "Условие задачи";
            // 
            // textLabelExampleDescription
            // 
            this.textLabelExampleDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLabelExampleDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLabelExampleDescription.Location = new System.Drawing.Point(3, 20);
            this.textLabelExampleDescription.Multiline = true;
            this.textLabelExampleDescription.Name = "textLabelExampleDescription";
            this.textLabelExampleDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLabelExampleDescription.Size = new System.Drawing.Size(303, 27);
            this.textLabelExampleDescription.TabIndex = 0;
            // 
            // FormMaxFlowProblem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 569);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "FormMaxFlowProblem";
            this.Text = "Задачи о максимальном потоке и минимальном разрезе";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxVisualization.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.graphVisualizer)).EndInit();
            this.groupBoxTip.ResumeLayout(false);
            this.groupBoxTip.PerformLayout();
            this.groupBoxHelp.ResumeLayout(false);
            this.groupBoxAnswers.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxSolution.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBoxExampleDescription.ResumeLayout(false);
            this.groupBoxExampleDescription.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxVisualization;
        private System.Windows.Forms.GroupBox groupBoxTip;
        private System.Windows.Forms.GroupBox groupBoxAnswers;
        private System.Windows.Forms.GroupBox groupBoxHelp;
        private SGVL.Visualizers.SimpleGraphVisualizer.SimpleGraphVisualizer graphVisualizer;
        private System.Windows.Forms.GroupBox groupBoxSolution;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAcceptAnswer;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonReloadIteration;
        private System.Windows.Forms.Button buttonReloadProblem;
        private System.Windows.Forms.Button buttonLecture;
        private System.Windows.Forms.GroupBox groupBoxExampleDescription;
        private Controls.TextLabel textLabelTip;
        private Controls.TextLabel textLabelExampleDescription;
    }
}