﻿namespace GOES {
    partial class FormMain {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxMenu = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonProblems = new System.Windows.Forms.Button();
            this.buttonLectures = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxMenu.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxMenu, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(754, 455);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxMenu
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxMenu, 2);
            this.groupBoxMenu.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMenu.Location = new System.Drawing.Point(191, 116);
            this.groupBoxMenu.Name = "groupBoxMenu";
            this.tableLayoutPanel1.SetRowSpan(this.groupBoxMenu, 2);
            this.groupBoxMenu.Size = new System.Drawing.Size(370, 220);
            this.groupBoxMenu.TabIndex = 0;
            this.groupBoxMenu.TabStop = false;
            this.groupBoxMenu.Text = "Меню";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonProblems, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonLectures, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonAbout, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(364, 199);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // buttonProblems
            // 
            this.buttonProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonProblems.Location = new System.Drawing.Point(3, 3);
            this.buttonProblems.Name = "buttonProblems";
            this.buttonProblems.Size = new System.Drawing.Size(358, 43);
            this.buttonProblems.TabIndex = 0;
            this.buttonProblems.Text = "Задачи";
            this.buttonProblems.UseVisualStyleBackColor = true;
            this.buttonProblems.Click += new System.EventHandler(this.buttonProblems_Click);
            // 
            // buttonLectures
            // 
            this.buttonLectures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLectures.Location = new System.Drawing.Point(3, 52);
            this.buttonLectures.Name = "buttonLectures";
            this.buttonLectures.Size = new System.Drawing.Size(358, 43);
            this.buttonLectures.TabIndex = 1;
            this.buttonLectures.Text = "Лекции";
            this.buttonLectures.UseVisualStyleBackColor = true;
            this.buttonLectures.Click += new System.EventHandler(this.buttonLectures_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAbout.Location = new System.Drawing.Point(3, 150);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(358, 46);
            this.buttonAbout.TabIndex = 2;
            this.buttonAbout.Text = "О программе";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 455);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Обучающая система \"Оптимизационные задачи на графах\"";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxMenu.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonProblems;
        private System.Windows.Forms.Button buttonLectures;
        private System.Windows.Forms.Button buttonAbout;
    }
}

