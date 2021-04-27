namespace GOES.Forms {
    partial class FormLectureViewer {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLectureViewer));
            this.webBrowserLecture = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserLecture
            // 
            this.webBrowserLecture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserLecture.Location = new System.Drawing.Point(0, 0);
            this.webBrowserLecture.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserLecture.Name = "webBrowserLecture";
            this.webBrowserLecture.Size = new System.Drawing.Size(800, 450);
            this.webBrowserLecture.TabIndex = 0;
            // 
            // FormLectureViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webBrowserLecture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLectureViewer";
            this.Text = "Лекции";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserLecture;
    }
}