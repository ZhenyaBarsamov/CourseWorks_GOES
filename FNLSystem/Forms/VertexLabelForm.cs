using System;
using System.Windows.Forms;

namespace FNLSystem.Forms {
    /// <summary>
    /// Форма, предназначенная для считывания метки вершины,
    /// и её проверки
    /// </summary>
    public partial class VertexLabelForm : Form {
        /// <summary>
        /// Правильный знак в метке
        /// </summary>
        private char CorrectSign { get; set; }
        /// <summary>
        /// Правильная предыдущая вершина в метке
        /// </summary>
        private int CorrectPrevVertex { get; set; }
        /// <summary>
        /// Правильный аугментальный поток в метке
        /// </summary>
        private int CorrectAugmentalFlow { get; set; }
        /// <summary>
        /// Количество допущенных ошибок
        /// </summary>
        public int MistakesCount { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="verticesCount">Количество вершин в графе</param>
        /// <param name="correctSign">Правильный знак в метке</param>
        /// <param name="correctPrevVertex">Правильная предыдущая вершина</param>
        /// <param name="correctAugmentalFlow">Правильный аугменатльный поток</param>
        public VertexLabelForm(int verticesCount, char correctSign, int correctPrevVertex, int correctAugmentalFlow) {
            InitializeComponent();
            cbSign.SelectedIndex = 0;
            nudPrevVertex.Maximum = verticesCount;

            CorrectSign = correctSign;
            CorrectPrevVertex = correctPrevVertex;
            CorrectAugmentalFlow = correctAugmentalFlow;
            MistakesCount = 0;
        }

        private void bAccept_Click(object sender, EventArgs e) {
            if (CorrectSign != cbSign.Text[0]) {
                MistakesCount++;
                MessageBox.Show("Подумай над знаком! Если ребро прямого направления - знак '+', если обратного - '-'.", 
                    "Упс!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CorrectPrevVertex != (int)nudPrevVertex.Value) {
                MistakesCount++;
                MessageBox.Show("Подумай над предыдущей вершиной!.. Ой, подсказал : (", 
                    "Упс!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CorrectAugmentalFlow != (int)nudAugmentalFlow.Value) {
                MistakesCount++;
                MessageBox.Show("Подумай над дополнительным потоком! По дуге прямого направления ты не можешь провести больше потока, чем она может принять. " +
                    "Из потока дуги обратного направления ты не можешь убрать больше, чем по ней уже идёт", 
                    "Упс!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
