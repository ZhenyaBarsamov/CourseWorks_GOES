namespace GraphVizTestProject {
    partial class Test {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.снятьВыделениеВершинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.снятьВыделениеДугToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискВГлубинуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.egViz = new GraphEduSysControlLibrary.EduGraphVisualizer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.egViz)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.снятьВыделениеВершинToolStripMenuItem,
            this.снятьВыделениеДугToolStripMenuItem,
            this.поискВГлубинуToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(966, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // снятьВыделениеВершинToolStripMenuItem
            // 
            this.снятьВыделениеВершинToolStripMenuItem.Name = "снятьВыделениеВершинToolStripMenuItem";
            this.снятьВыделениеВершинToolStripMenuItem.Size = new System.Drawing.Size(203, 24);
            this.снятьВыделениеВершинToolStripMenuItem.Text = "Снять выделение вершин";
            this.снятьВыделениеВершинToolStripMenuItem.Click += new System.EventHandler(this.снятьВыделениеВершинToolStripMenuItem_Click);
            // 
            // снятьВыделениеДугToolStripMenuItem
            // 
            this.снятьВыделениеДугToolStripMenuItem.Name = "снятьВыделениеДугToolStripMenuItem";
            this.снятьВыделениеДугToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.снятьВыделениеДугToolStripMenuItem.Text = "Снять выделение дуг";
            this.снятьВыделениеДугToolStripMenuItem.Click += new System.EventHandler(this.снятьВыделениеДугToolStripMenuItem_Click);
            // 
            // поискВГлубинуToolStripMenuItem
            // 
            this.поискВГлубинуToolStripMenuItem.Name = "поискВГлубинуToolStripMenuItem";
            this.поискВГлубинуToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.поискВГлубинуToolStripMenuItem.Text = "Найти максимальный поток";
            this.поискВГлубинуToolStripMenuItem.Click += new System.EventHandler(this.поискВГлубинуToolStripMenuItem_Click);
            // 
            // egViz
            // 
            this.egViz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.egViz.Image = ((System.Drawing.Image)(resources.GetObject("egViz.Image")));
            this.egViz.Location = new System.Drawing.Point(0, 28);
            this.egViz.Name = "egViz";
            this.egViz.Size = new System.Drawing.Size(966, 549);
            this.egViz.TabIndex = 0;
            this.egViz.TabStop = false;
            this.egViz.Click += new System.EventHandler(this.egViz_Click);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 577);
            this.Controls.Add(this.egViz);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Test";
            this.Text = "Test";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.egViz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphEduSysControlLibrary.EduGraphVisualizer egViz;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem снятьВыделениеВершинToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem снятьВыделениеДугToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискВГлубинуToolStripMenuItem;
    }
}

