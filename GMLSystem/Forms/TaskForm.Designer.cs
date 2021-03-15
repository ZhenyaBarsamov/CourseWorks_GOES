namespace GMLSystem.Forms {
    partial class TaskForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskForm));
            this.gbGraphViz = new System.Windows.Forms.GroupBox();
            this.eGraphViz = new GraphEduSysControlLibrary.EduGraphVisualizer();
            this.gbTip = new System.Windows.Forms.GroupBox();
            this.tbTip = new System.Windows.Forms.TextBox();
            this.gbWork = new System.Windows.Forms.GroupBox();
            this.bClearAll = new System.Windows.Forms.Button();
            this.bDoStep = new System.Windows.Forms.Button();
            this.gbHelp = new System.Windows.Forms.GroupBox();
            this.bLecture = new System.Windows.Forms.Button();
            this.ttCommonTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbGraphViz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eGraphViz)).BeginInit();
            this.gbTip.SuspendLayout();
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
            this.gbTip.Size = new System.Drawing.Size(279, 279);
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
            this.tbTip.Size = new System.Drawing.Size(273, 256);
            this.tbTip.TabIndex = 0;
            this.tbTip.Text = "Текст подсказки";
            // 
            // gbWork
            // 
            this.gbWork.Controls.Add(this.bClearAll);
            this.gbWork.Controls.Add(this.bDoStep);
            this.gbWork.Location = new System.Drawing.Point(738, 299);
            this.gbWork.Name = "gbWork";
            this.gbWork.Size = new System.Drawing.Size(279, 177);
            this.gbWork.TabIndex = 3;
            this.gbWork.TabStop = false;
            this.gbWork.Text = "Решение";
            // 
            // bClearAll
            // 
            this.bClearAll.Location = new System.Drawing.Point(60, 109);
            this.bClearAll.Name = "bClearAll";
            this.bClearAll.Size = new System.Drawing.Size(162, 49);
            this.bClearAll.TabIndex = 1;
            this.bClearAll.Text = "К началу решения";
            this.bClearAll.UseVisualStyleBackColor = true;
            this.bClearAll.Click += new System.EventHandler(this.bClearAll_Click);
            // 
            // bDoStep
            // 
            this.bDoStep.Location = new System.Drawing.Point(60, 34);
            this.bDoStep.Name = "bDoStep";
            this.bDoStep.Size = new System.Drawing.Size(162, 48);
            this.bDoStep.TabIndex = 0;
            this.bDoStep.Text = "Выполнить шаг";
            this.bDoStep.UseVisualStyleBackColor = true;
            this.bDoStep.Click += new System.EventHandler(this.bDoStep_Click);
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
            // TaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 563);
            this.Controls.Add(this.gbHelp);
            this.Controls.Add(this.gbWork);
            this.Controls.Add(this.gbTip);
            this.Controls.Add(this.gbGraphViz);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Поиск максимального паросочетания в двудольном графе";
            this.gbGraphViz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eGraphViz)).EndInit();
            this.gbTip.ResumeLayout(false);
            this.gbTip.PerformLayout();
            this.gbWork.ResumeLayout(false);
            this.gbHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox gbGraphViz;
        protected System.Windows.Forms.GroupBox gbTip;
        protected System.Windows.Forms.GroupBox gbWork;
        protected System.Windows.Forms.GroupBox gbHelp;
        protected GraphEduSysControlLibrary.EduGraphVisualizer eGraphViz;
        protected System.Windows.Forms.TextBox tbTip;
        protected System.Windows.Forms.Button bClearAll;
        protected System.Windows.Forms.Button bDoStep;
        protected System.Windows.Forms.Button bLecture;
        private System.Windows.Forms.ToolTip ttCommonTip;
    }
}