namespace GOES.Forms {
    partial class FormStudentInformation {
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
            this.groupBoxStudentName = new System.Windows.Forms.GroupBox();
            this.groupBoxStudentGroup = new System.Windows.Forms.GroupBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxStudentName = new System.Windows.Forms.ComboBox();
            this.comboBoxStudentGroup = new System.Windows.Forms.ComboBox();
            this.groupBoxStudentName.SuspendLayout();
            this.groupBoxStudentGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStudentName
            // 
            this.groupBoxStudentName.Controls.Add(this.comboBoxStudentName);
            this.groupBoxStudentName.Location = new System.Drawing.Point(12, 12);
            this.groupBoxStudentName.Name = "groupBoxStudentName";
            this.groupBoxStudentName.Size = new System.Drawing.Size(688, 58);
            this.groupBoxStudentName.TabIndex = 0;
            this.groupBoxStudentName.TabStop = false;
            this.groupBoxStudentName.Text = "Имя";
            // 
            // groupBoxStudentGroup
            // 
            this.groupBoxStudentGroup.Controls.Add(this.comboBoxStudentGroup);
            this.groupBoxStudentGroup.Location = new System.Drawing.Point(12, 76);
            this.groupBoxStudentGroup.Name = "groupBoxStudentGroup";
            this.groupBoxStudentGroup.Size = new System.Drawing.Size(688, 56);
            this.groupBoxStudentGroup.TabIndex = 1;
            this.groupBoxStudentGroup.TabStop = false;
            this.groupBoxStudentGroup.Text = "Класс, группа";
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(370, 160);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(149, 43);
            this.buttonAccept.TabIndex = 2;
            this.buttonAccept.Text = "Принять";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(548, 160);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(149, 43);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxStudentName
            // 
            this.comboBoxStudentName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxStudentName.FormattingEnabled = true;
            this.comboBoxStudentName.Location = new System.Drawing.Point(3, 20);
            this.comboBoxStudentName.Name = "comboBoxStudentName";
            this.comboBoxStudentName.Size = new System.Drawing.Size(682, 26);
            this.comboBoxStudentName.TabIndex = 0;
            // 
            // comboBoxStudentGroup
            // 
            this.comboBoxStudentGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxStudentGroup.FormattingEnabled = true;
            this.comboBoxStudentGroup.Location = new System.Drawing.Point(3, 20);
            this.comboBoxStudentGroup.Name = "comboBoxStudentGroup";
            this.comboBoxStudentGroup.Size = new System.Drawing.Size(682, 26);
            this.comboBoxStudentGroup.TabIndex = 0;
            // 
            // FormStudentInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 215);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.groupBoxStudentGroup);
            this.Controls.Add(this.groupBoxStudentName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormStudentInformation";
            this.Text = "Личные данные студента";
            this.groupBoxStudentName.ResumeLayout(false);
            this.groupBoxStudentGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxStudentName;
        private System.Windows.Forms.GroupBox groupBoxStudentGroup;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxStudentName;
        private System.Windows.Forms.ComboBox comboBoxStudentGroup;
    }
}