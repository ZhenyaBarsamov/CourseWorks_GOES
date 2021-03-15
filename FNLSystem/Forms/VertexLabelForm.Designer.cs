namespace FNLSystem.Forms {
    partial class VertexLabelForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VertexLabelForm));
            this.bAccept = new System.Windows.Forms.Button();
            this.gbSign = new System.Windows.Forms.GroupBox();
            this.cbSign = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudPrevVertex = new System.Windows.Forms.NumericUpDown();
            this.gbAugmentalFlow = new System.Windows.Forms.GroupBox();
            this.nudAugmentalFlow = new System.Windows.Forms.NumericUpDown();
            this.bCancel = new System.Windows.Forms.Button();
            this.gbSign.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrevVertex)).BeginInit();
            this.gbAugmentalFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAugmentalFlow)).BeginInit();
            this.SuspendLayout();
            // 
            // bAccept
            // 
            this.bAccept.ForeColor = System.Drawing.Color.ForestGreen;
            this.bAccept.Location = new System.Drawing.Point(186, 116);
            this.bAccept.Name = "bAccept";
            this.bAccept.Size = new System.Drawing.Size(90, 33);
            this.bAccept.TabIndex = 2;
            this.bAccept.Text = "Принять";
            this.bAccept.UseVisualStyleBackColor = true;
            this.bAccept.Click += new System.EventHandler(this.bAccept_Click);
            // 
            // gbSign
            // 
            this.gbSign.Controls.Add(this.cbSign);
            this.gbSign.Location = new System.Drawing.Point(14, 22);
            this.gbSign.Name = "gbSign";
            this.gbSign.Size = new System.Drawing.Size(92, 58);
            this.gbSign.TabIndex = 4;
            this.gbSign.TabStop = false;
            this.gbSign.Text = "Знак";
            // 
            // cbSign
            // 
            this.cbSign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSign.FormattingEnabled = true;
            this.cbSign.Items.AddRange(new object[] {
            "+",
            "-"});
            this.cbSign.Location = new System.Drawing.Point(3, 20);
            this.cbSign.Name = "cbSign";
            this.cbSign.Size = new System.Drawing.Size(86, 26);
            this.cbSign.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudPrevVertex);
            this.groupBox1.Location = new System.Drawing.Point(112, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 58);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пред. вершина";
            // 
            // nudPrevVertex
            // 
            this.nudPrevVertex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudPrevVertex.Location = new System.Drawing.Point(3, 20);
            this.nudPrevVertex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrevVertex.Name = "nudPrevVertex";
            this.nudPrevVertex.Size = new System.Drawing.Size(132, 24);
            this.nudPrevVertex.TabIndex = 0;
            this.nudPrevVertex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gbAugmentalFlow
            // 
            this.gbAugmentalFlow.Controls.Add(this.nudAugmentalFlow);
            this.gbAugmentalFlow.Location = new System.Drawing.Point(258, 22);
            this.gbAugmentalFlow.Name = "gbAugmentalFlow";
            this.gbAugmentalFlow.Size = new System.Drawing.Size(128, 58);
            this.gbAugmentalFlow.TabIndex = 6;
            this.gbAugmentalFlow.TabStop = false;
            this.gbAugmentalFlow.Text = "Аугм. поток";
            // 
            // nudAugmentalFlow
            // 
            this.nudAugmentalFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudAugmentalFlow.Location = new System.Drawing.Point(3, 20);
            this.nudAugmentalFlow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAugmentalFlow.Name = "nudAugmentalFlow";
            this.nudAugmentalFlow.Size = new System.Drawing.Size(122, 24);
            this.nudAugmentalFlow.TabIndex = 0;
            this.nudAugmentalFlow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.ForeColor = System.Drawing.Color.OrangeRed;
            this.bCancel.Location = new System.Drawing.Point(293, 116);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(90, 33);
            this.bCancel.TabIndex = 7;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // VertexLabelForm
            // 
            this.AcceptButton = this.bAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(399, 161);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.gbAugmentalFlow);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSign);
            this.Controls.Add(this.bAccept);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "VertexLabelForm";
            this.Text = "Метка вершины";
            this.gbSign.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudPrevVertex)).EndInit();
            this.gbAugmentalFlow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAugmentalFlow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bAccept;
        private System.Windows.Forms.GroupBox gbSign;
        private System.Windows.Forms.ComboBox cbSign;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudPrevVertex;
        private System.Windows.Forms.GroupBox gbAugmentalFlow;
        private System.Windows.Forms.NumericUpDown nudAugmentalFlow;
        private System.Windows.Forms.Button bCancel;
    }
}