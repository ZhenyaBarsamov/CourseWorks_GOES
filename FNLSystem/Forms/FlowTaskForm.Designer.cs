namespace FNLSystem.Forms {
    partial class FlowTaskForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowTaskForm));
            this.gbGraphViz = new System.Windows.Forms.GroupBox();
            this.eGraphViz = new GraphEduSysControlLibrary.EduGraphVisualizer();
            this.gbTip = new System.Windows.Forms.GroupBox();
            this.tbTip = new System.Windows.Forms.TextBox();
            this.gbTaskOptions = new System.Windows.Forms.GroupBox();
            this.cbShowMarks = new System.Windows.Forms.CheckBox();
            this.nudFinishVertex = new System.Windows.Forms.NumericUpDown();
            this.nudStartVertex = new System.Windows.Forms.NumericUpDown();
            this.lFinishV = new System.Windows.Forms.Label();
            this.lStartV = new System.Windows.Forms.Label();
            this.gbWork = new System.Windows.Forms.GroupBox();
            this.bClearAll = new System.Windows.Forms.Button();
            this.bCommon = new System.Windows.Forms.Button();
            this.gbHelp = new System.Windows.Forms.GroupBox();
            this.bLecture = new System.Windows.Forms.Button();
            this.ttCommonTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbGraphViz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eGraphViz)).BeginInit();
            this.gbTip.SuspendLayout();
            this.gbTaskOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishVertex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartVertex)).BeginInit();
            this.gbWork.SuspendLayout();
            this.gbHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbGraphViz
            // 
            this.gbGraphViz.Controls.Add(this.eGraphViz);
            this.gbGraphViz.Location = new System.Drawing.Point(14, 14);
            this.gbGraphViz.Name = "gbGraphViz";
            this.gbGraphViz.Size = new System.Drawing.Size(717, 537);
            this.gbGraphViz.TabIndex = 0;
            this.gbGraphViz.TabStop = false;
            this.gbGraphViz.Text = "Визуализация графа";
            // 
            // eGraphViz
            // 
            this.eGraphViz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eGraphViz.InteractiveMode = false;
            this.eGraphViz.Location = new System.Drawing.Point(3, 20);
            this.eGraphViz.Name = "eGraphViz";
            this.eGraphViz.Size = new System.Drawing.Size(711, 514);
            this.eGraphViz.TabIndex = 0;
            this.eGraphViz.TabStop = false;
            // 
            // gbTip
            // 
            this.gbTip.Controls.Add(this.tbTip);
            this.gbTip.Location = new System.Drawing.Point(738, 14);
            this.gbTip.Name = "gbTip";
            this.gbTip.Size = new System.Drawing.Size(279, 210);
            this.gbTip.TabIndex = 1;
            this.gbTip.TabStop = false;
            this.gbTip.Text = "Подсказка";
            // 
            // tbTip
            // 
            this.tbTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTip.Location = new System.Drawing.Point(3, 20);
            this.tbTip.Multiline = true;
            this.tbTip.Name = "tbTip";
            this.tbTip.ReadOnly = true;
            this.tbTip.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTip.Size = new System.Drawing.Size(273, 187);
            this.tbTip.TabIndex = 0;
            this.tbTip.Text = "Текст подсказки";
            // 
            // gbTaskOptions
            // 
            this.gbTaskOptions.Controls.Add(this.cbShowMarks);
            this.gbTaskOptions.Controls.Add(this.nudFinishVertex);
            this.gbTaskOptions.Controls.Add(this.nudStartVertex);
            this.gbTaskOptions.Controls.Add(this.lFinishV);
            this.gbTaskOptions.Controls.Add(this.lStartV);
            this.gbTaskOptions.Location = new System.Drawing.Point(738, 230);
            this.gbTaskOptions.Name = "gbTaskOptions";
            this.gbTaskOptions.Size = new System.Drawing.Size(279, 125);
            this.gbTaskOptions.TabIndex = 2;
            this.gbTaskOptions.TabStop = false;
            this.gbTaskOptions.Text = "Настройки";
            // 
            // cbShowMarks
            // 
            this.cbShowMarks.AutoSize = true;
            this.cbShowMarks.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowMarks.Location = new System.Drawing.Point(52, 96);
            this.cbShowMarks.Name = "cbShowMarks";
            this.cbShowMarks.Size = new System.Drawing.Size(218, 22);
            this.cbShowMarks.TabIndex = 5;
            this.cbShowMarks.Text = "Отображать метки вершин";
            this.cbShowMarks.UseVisualStyleBackColor = true;
            // 
            // nudFinishVertex
            // 
            this.nudFinishVertex.Location = new System.Drawing.Point(150, 60);
            this.nudFinishVertex.Name = "nudFinishVertex";
            this.nudFinishVertex.Size = new System.Drawing.Size(120, 24);
            this.nudFinishVertex.TabIndex = 3;
            this.nudFinishVertex.ValueChanged += new System.EventHandler(this.nudFinishVertex_ValueChanged);
            // 
            // nudStartVertex
            // 
            this.nudStartVertex.Location = new System.Drawing.Point(150, 22);
            this.nudStartVertex.Name = "nudStartVertex";
            this.nudStartVertex.Size = new System.Drawing.Size(120, 24);
            this.nudStartVertex.TabIndex = 2;
            this.nudStartVertex.ValueChanged += new System.EventHandler(this.nudStartVertex_ValueChanged);
            // 
            // lFinishV
            // 
            this.lFinishV.AutoSize = true;
            this.lFinishV.Location = new System.Drawing.Point(9, 62);
            this.lFinishV.Name = "lFinishV";
            this.lFinishV.Size = new System.Drawing.Size(106, 18);
            this.lFinishV.TabIndex = 1;
            this.lFinishV.Text = "Вершина-сток";
            // 
            // lStartV
            // 
            this.lStartV.AutoSize = true;
            this.lStartV.Location = new System.Drawing.Point(9, 24);
            this.lStartV.Name = "lStartV";
            this.lStartV.Size = new System.Drawing.Size(114, 18);
            this.lStartV.TabIndex = 0;
            this.lStartV.Text = "Вершина-исток";
            // 
            // gbWork
            // 
            this.gbWork.Controls.Add(this.bClearAll);
            this.gbWork.Controls.Add(this.bCommon);
            this.gbWork.Location = new System.Drawing.Point(738, 361);
            this.gbWork.Name = "gbWork";
            this.gbWork.Size = new System.Drawing.Size(279, 115);
            this.gbWork.TabIndex = 3;
            this.gbWork.TabStop = false;
            this.gbWork.Text = "Решение";
            // 
            // bClearAll
            // 
            this.bClearAll.Location = new System.Drawing.Point(60, 68);
            this.bClearAll.Name = "bClearAll";
            this.bClearAll.Size = new System.Drawing.Size(162, 37);
            this.bClearAll.TabIndex = 1;
            this.bClearAll.Text = "К началу решения";
            this.bClearAll.UseVisualStyleBackColor = true;
            this.bClearAll.Click += new System.EventHandler(this.bClearAll_Click);
            // 
            // bCommon
            // 
            this.bCommon.Location = new System.Drawing.Point(60, 23);
            this.bCommon.Name = "bCommon";
            this.bCommon.Size = new System.Drawing.Size(162, 37);
            this.bCommon.TabIndex = 0;
            this.bCommon.Text = "Общая кнопка";
            this.bCommon.UseVisualStyleBackColor = true;
            this.bCommon.Click += new System.EventHandler(this.bCommon_Click);
            // 
            // gbHelp
            // 
            this.gbHelp.Controls.Add(this.bLecture);
            this.gbHelp.Location = new System.Drawing.Point(738, 482);
            this.gbHelp.Name = "gbHelp";
            this.gbHelp.Size = new System.Drawing.Size(279, 69);
            this.gbHelp.TabIndex = 4;
            this.gbHelp.TabStop = false;
            this.gbHelp.Text = "Помощь";
            // 
            // bLecture
            // 
            this.bLecture.Location = new System.Drawing.Point(60, 23);
            this.bLecture.Name = "bLecture";
            this.bLecture.Size = new System.Drawing.Size(162, 37);
            this.bLecture.TabIndex = 0;
            this.bLecture.Text = "Лекция по теме";
            this.bLecture.UseVisualStyleBackColor = true;
            this.bLecture.Click += new System.EventHandler(this.bLecture_Click);
            // 
            // ttCommonTip
            // 
            this.ttCommonTip.AutoPopDelay = 5000;
            this.ttCommonTip.InitialDelay = 1000;
            this.ttCommonTip.IsBalloon = true;
            this.ttCommonTip.ReshowDelay = 100;
            // 
            // FlowTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 563);
            this.Controls.Add(this.gbHelp);
            this.Controls.Add(this.gbWork);
            this.Controls.Add(this.gbTaskOptions);
            this.Controls.Add(this.gbTip);
            this.Controls.Add(this.gbGraphViz);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FlowTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Поиск максимального потока";
            this.gbGraphViz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eGraphViz)).EndInit();
            this.gbTip.ResumeLayout(false);
            this.gbTip.PerformLayout();
            this.gbTaskOptions.ResumeLayout(false);
            this.gbTaskOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFinishVertex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartVertex)).EndInit();
            this.gbWork.ResumeLayout(false);
            this.gbHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox gbGraphViz;
        protected System.Windows.Forms.GroupBox gbTip;
        protected System.Windows.Forms.GroupBox gbTaskOptions;
        protected System.Windows.Forms.GroupBox gbWork;
        protected System.Windows.Forms.GroupBox gbHelp;
        protected GraphEduSysControlLibrary.EduGraphVisualizer eGraphViz;
        protected System.Windows.Forms.TextBox tbTip;
        protected System.Windows.Forms.NumericUpDown nudFinishVertex;
        protected System.Windows.Forms.NumericUpDown nudStartVertex;
        protected System.Windows.Forms.Label lFinishV;
        protected System.Windows.Forms.Label lStartV;
        protected System.Windows.Forms.Button bClearAll;
        protected System.Windows.Forms.Button bCommon;
        protected System.Windows.Forms.Button bLecture;
        protected System.Windows.Forms.CheckBox cbShowMarks;
        private System.Windows.Forms.ToolTip ttCommonTip;
    }
}