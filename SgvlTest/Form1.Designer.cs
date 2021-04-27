namespace SgvlTest {
    partial class Form1 {
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
            this.msaglGraphVisualizer1 = new SGVL.Visualizers.MsaglGraphVisualizer.MsaglGraphVisualizer();
            this.SuspendLayout();
            // 
            // msaglGraphVisualizer1
            // 
            this.msaglGraphVisualizer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msaglGraphVisualizer1.InteractiveMode = SGVL.Types.Visualizers.InteractiveMode.NonInteractive;
            this.msaglGraphVisualizer1.IsVerticesMoving = true;
            this.msaglGraphVisualizer1.Location = new System.Drawing.Point(0, 0);
            this.msaglGraphVisualizer1.Name = "msaglGraphVisualizer1";
            this.msaglGraphVisualizer1.Size = new System.Drawing.Size(800, 450);
            this.msaglGraphVisualizer1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.msaglGraphVisualizer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SGVL.Visualizers.MsaglGraphVisualizer.MsaglGraphVisualizer msaglGraphVisualizer1;
    }
}

