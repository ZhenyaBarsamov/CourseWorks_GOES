using System;
using System.Windows.Forms;

namespace FNLSystem.Forms {
    /// <summary>
    /// Форма, предназначенная для считывания величины увеличения потока
    /// и её проверки
    /// </summary>
    public partial class AugmentalFlowForm : Form {
        private int CorrectAugmentalFlow { get; set; }
        /// <summary>
        /// Количество допущенных ошибок
        /// </summary>
        public int MistakesCount { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="correctAugmentalFlow">Правильный аугментальный поток</param>
        public AugmentalFlowForm(int correctAugmentalFlow) {
            InitializeComponent();
            CorrectAugmentalFlow = correctAugmentalFlow;
            MistakesCount = 0;
        }

        private void bAccept_Click(object sender, EventArgs e) {
            if (nudAugmentalFlow.Value != CorrectAugmentalFlow) {
                MistakesCount++;
                MessageBox.Show("Подумай! Ты не зря метки расставлял!", "Упс!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
