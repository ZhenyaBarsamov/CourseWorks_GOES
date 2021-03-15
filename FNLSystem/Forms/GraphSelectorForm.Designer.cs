namespace FNLSystem.Forms {
    partial class GraphSelectorForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphSelectorForm));
            this.bAccept = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lInput = new System.Windows.Forms.Label();
            this.nudGraphNumber = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // bAccept
            // 
            this.bAccept.ForeColor = System.Drawing.Color.ForestGreen;
            this.bAccept.Location = new System.Drawing.Point(136, 98);
            this.bAccept.Name = "bAccept";
            this.bAccept.Size = new System.Drawing.Size(90, 33);
            this.bAccept.TabIndex = 0;
            this.bAccept.Text = "Выбрать";
            this.bAccept.UseVisualStyleBackColor = true;
            this.bAccept.Click += new System.EventHandler(this.bAccept_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.ForeColor = System.Drawing.Color.OrangeRed;
            this.bCancel.Location = new System.Drawing.Point(248, 98);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 33);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lInput
            // 
            this.lInput.AutoSize = true;
            this.lInput.Location = new System.Drawing.Point(12, 32);
            this.lInput.Name = "lInput";
            this.lInput.Size = new System.Drawing.Size(172, 18);
            this.lInput.TabIndex = 2;
            this.lInput.Text = "Выберите номер графа";
            // 
            // nudGraphNumber
            // 
            this.nudGraphNumber.Location = new System.Drawing.Point(218, 30);
            this.nudGraphNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGraphNumber.Name = "nudGraphNumber";
            this.nudGraphNumber.ReadOnly = true;
            this.nudGraphNumber.Size = new System.Drawing.Size(120, 24);
            this.nudGraphNumber.TabIndex = 3;
            this.nudGraphNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GraphSelectorForm
            // 
            this.AcceptButton = this.bAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(355, 143);
            this.Controls.Add(this.nudGraphNumber);
            this.Controls.Add(this.lInput);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bAccept);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GraphSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор графа";
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bAccept;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lInput;
        private System.Windows.Forms.NumericUpDown nudGraphNumber;
    }
}