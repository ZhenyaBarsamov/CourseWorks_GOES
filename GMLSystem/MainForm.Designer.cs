namespace GMLSystem {
    partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gbMenu = new System.Windows.Forms.GroupBox();
            this.bAboutProgram = new System.Windows.Forms.Button();
            this.bLecture = new System.Windows.Forms.Button();
            this.bDemonstrationExample = new System.Windows.Forms.Button();
            this.gbMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMenu
            // 
            this.gbMenu.AutoSize = true;
            this.gbMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbMenu.Controls.Add(this.bAboutProgram);
            this.gbMenu.Controls.Add(this.bLecture);
            this.gbMenu.Controls.Add(this.bDemonstrationExample);
            this.gbMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbMenu.Location = new System.Drawing.Point(114, 60);
            this.gbMenu.Name = "gbMenu";
            this.gbMenu.Size = new System.Drawing.Size(305, 240);
            this.gbMenu.TabIndex = 1;
            this.gbMenu.TabStop = false;
            this.gbMenu.Text = "Меню";
            // 
            // bAboutProgram
            // 
            this.bAboutProgram.Location = new System.Drawing.Point(7, 180);
            this.bAboutProgram.Name = "bAboutProgram";
            this.bAboutProgram.Size = new System.Drawing.Size(292, 37);
            this.bAboutProgram.TabIndex = 3;
            this.bAboutProgram.Text = "О программе";
            this.bAboutProgram.UseVisualStyleBackColor = true;
            this.bAboutProgram.Click += new System.EventHandler(this.bAboutProgram_Click);
            // 
            // bLecture
            // 
            this.bLecture.Location = new System.Drawing.Point(7, 124);
            this.bLecture.Name = "bLecture";
            this.bLecture.Size = new System.Drawing.Size(292, 37);
            this.bLecture.TabIndex = 2;
            this.bLecture.Text = "Лекция по теме";
            this.bLecture.UseVisualStyleBackColor = true;
            this.bLecture.Click += new System.EventHandler(this.bLecture_Click);
            // 
            // bDemonstrationExample
            // 
            this.bDemonstrationExample.Location = new System.Drawing.Point(7, 23);
            this.bDemonstrationExample.Name = "bDemonstrationExample";
            this.bDemonstrationExample.Size = new System.Drawing.Size(292, 63);
            this.bDemonstrationExample.TabIndex = 0;
            this.bDemonstrationExample.Text = "Демонстрационный пример";
            this.bDemonstrationExample.UseVisualStyleBackColor = true;
            this.bDemonstrationExample.Click += new System.EventHandler(this.bDemonstrationExample_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(554, 398);
            this.Controls.Add(this.gbMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Паросочетания в графах";
            this.gbMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMenu;
        private System.Windows.Forms.Button bLecture;
        private System.Windows.Forms.Button bDemonstrationExample;
        private System.Windows.Forms.Button bAboutProgram;
    }
}

