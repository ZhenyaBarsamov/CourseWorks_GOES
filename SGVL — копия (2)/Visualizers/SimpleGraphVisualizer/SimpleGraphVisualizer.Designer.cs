namespace SGVL.Visualizers.SimpleGraphVisualizer {
    partial class SimpleGraphVisualizer {
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // SimpleGraphVisualizer
            // 
            this.SizeChanged += new System.EventHandler(this.SimpleGraphVisualizer_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SimpleGraphVisualizer_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SimpleGraphVisualizer_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SimpleGraphVisualizer_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
