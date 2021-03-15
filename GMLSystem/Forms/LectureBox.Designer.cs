namespace GMLSystem.Forms {
    partial class LectureBox {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LectureBox));
            this.wbLecture = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbLecture
            // 
            this.wbLecture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbLecture.Location = new System.Drawing.Point(0, 0);
            this.wbLecture.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbLecture.Name = "wbLecture";
            this.wbLecture.Size = new System.Drawing.Size(800, 450);
            this.wbLecture.TabIndex = 0;
            // 
            // LectureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wbLecture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LectureBox";
            this.Text = "Лекция";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbLecture;
    }
}