namespace FNLSystem.Forms {
    partial class TaskStatisticsForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.lIncorrectWay = new System.Windows.Forms.Label();
            this.lIncorrectMark = new System.Windows.Forms.Label();
            this.lIncorrectFlowIncrease = new System.Windows.Forms.Label();
            this.lIncorrectMinCutEdge = new System.Windows.Forms.Label();
            this.lVerdict = new System.Windows.Forms.Label();
            this.bAccept = new System.Windows.Forms.Button();
            this.lMistakesSum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(139, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ваши ошибки:";
            // 
            // lIncorrectWay
            // 
            this.lIncorrectWay.AutoSize = true;
            this.lIncorrectWay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lIncorrectWay.Location = new System.Drawing.Point(12, 57);
            this.lIncorrectWay.Name = "lIncorrectWay";
            this.lIncorrectWay.Size = new System.Drawing.Size(177, 20);
            this.lIncorrectWay.TabIndex = 1;
            this.lIncorrectWay.Text = "Неправильный путь";
            // 
            // lIncorrectMark
            // 
            this.lIncorrectMark.AutoSize = true;
            this.lIncorrectMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lIncorrectMark.Location = new System.Drawing.Point(12, 97);
            this.lIncorrectMark.Name = "lIncorrectMark";
            this.lIncorrectMark.Size = new System.Drawing.Size(188, 20);
            this.lIncorrectMark.TabIndex = 2;
            this.lIncorrectMark.Text = "Неправильная метка";
            // 
            // lIncorrectFlowIncrease
            // 
            this.lIncorrectFlowIncrease.AutoSize = true;
            this.lIncorrectFlowIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lIncorrectFlowIncrease.Location = new System.Drawing.Point(12, 137);
            this.lIncorrectFlowIncrease.Name = "lIncorrectFlowIncrease";
            this.lIncorrectFlowIncrease.Size = new System.Drawing.Size(299, 20);
            this.lIncorrectFlowIncrease.TabIndex = 3;
            this.lIncorrectFlowIncrease.Text = "Неправильное увеличение потока";
            // 
            // lIncorrectMinCutEdge
            // 
            this.lIncorrectMinCutEdge.AutoSize = true;
            this.lIncorrectMinCutEdge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lIncorrectMinCutEdge.Location = new System.Drawing.Point(12, 177);
            this.lIncorrectMinCutEdge.Name = "lIncorrectMinCutEdge";
            this.lIncorrectMinCutEdge.Size = new System.Drawing.Size(375, 20);
            this.lIncorrectMinCutEdge.TabIndex = 4;
            this.lIncorrectMinCutEdge.Text = "Неправильная дуга минимального разреза";
            // 
            // lVerdict
            // 
            this.lVerdict.AutoSize = true;
            this.lVerdict.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lVerdict.Location = new System.Drawing.Point(12, 267);
            this.lVerdict.Name = "lVerdict";
            this.lVerdict.Size = new System.Drawing.Size(82, 20);
            this.lVerdict.TabIndex = 5;
            this.lVerdict.Text = "Вердикт";
            // 
            // bAccept
            // 
            this.bAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bAccept.ForeColor = System.Drawing.Color.ForestGreen;
            this.bAccept.Location = new System.Drawing.Point(152, 320);
            this.bAccept.Name = "bAccept";
            this.bAccept.Size = new System.Drawing.Size(100, 41);
            this.bAccept.TabIndex = 6;
            this.bAccept.Text = "Хорошо";
            this.bAccept.UseVisualStyleBackColor = true;
            this.bAccept.Click += new System.EventHandler(this.bAccept_Click);
            // 
            // lMistakesSum
            // 
            this.lMistakesSum.AutoSize = true;
            this.lMistakesSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lMistakesSum.Location = new System.Drawing.Point(12, 217);
            this.lMistakesSum.Name = "lMistakesSum";
            this.lMistakesSum.Size = new System.Drawing.Size(237, 20);
            this.lMistakesSum.TabIndex = 7;
            this.lMistakesSum.Text = "Общее количество ошибок";
            // 
            // TaskStatisticsForm
            // 
            this.AcceptButton = this.bAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 378);
            this.Controls.Add(this.lMistakesSum);
            this.Controls.Add(this.bAccept);
            this.Controls.Add(this.lVerdict);
            this.Controls.Add(this.lIncorrectMinCutEdge);
            this.Controls.Add(this.lIncorrectFlowIncrease);
            this.Controls.Add(this.lIncorrectMark);
            this.Controls.Add(this.lIncorrectWay);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TaskStatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Статистика решения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lIncorrectWay;
        private System.Windows.Forms.Label lIncorrectMark;
        private System.Windows.Forms.Label lIncorrectFlowIncrease;
        private System.Windows.Forms.Label lIncorrectMinCutEdge;
        private System.Windows.Forms.Label lVerdict;
        private System.Windows.Forms.Button bAccept;
        private System.Windows.Forms.Label lMistakesSum;
    }
}