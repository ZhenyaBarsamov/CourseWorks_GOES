namespace FNLSystem.Forms {
    partial class AugmentalFlowForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AugmentalFlowForm));
            this.bAccept = new System.Windows.Forms.Button();
            this.nudAugmentalFlow = new System.Windows.Forms.NumericUpDown();
            this.bCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudAugmentalFlow)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bAccept
            // 
            this.bAccept.ForeColor = System.Drawing.Color.ForestGreen;
            this.bAccept.Location = new System.Drawing.Point(146, 98);
            this.bAccept.Name = "bAccept";
            this.bAccept.Size = new System.Drawing.Size(90, 33);
            this.bAccept.TabIndex = 8;
            this.bAccept.Text = "Принять";
            this.bAccept.UseVisualStyleBackColor = true;
            this.bAccept.Click += new System.EventHandler(this.bAccept_Click);
            // 
            // nudAugmentalFlow
            // 
            this.nudAugmentalFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudAugmentalFlow.Location = new System.Drawing.Point(3, 20);
            this.nudAugmentalFlow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAugmentalFlow.Name = "nudAugmentalFlow";
            this.nudAugmentalFlow.Size = new System.Drawing.Size(325, 24);
            this.nudAugmentalFlow.TabIndex = 0;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.ForeColor = System.Drawing.Color.OrangeRed;
            this.bCancel.Location = new System.Drawing.Point(253, 98);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 33);
            this.bCancel.TabIndex = 8;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudAugmentalFlow);
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 55);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Величина дополнительного потока";
            // 
            // AugmentalFlowForm
            // 
            this.AcceptButton = this.bAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(355, 143);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bAccept);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AugmentalFlowForm";
            this.Text = "Сток достигнут";
            ((System.ComponentModel.ISupportInitialize)(this.nudAugmentalFlow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bAccept;
        private System.Windows.Forms.NumericUpDown nudAugmentalFlow;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}