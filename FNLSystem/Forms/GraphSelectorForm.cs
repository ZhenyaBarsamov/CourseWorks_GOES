using System;
using System.Windows.Forms;

namespace FNLSystem.Forms {
    /// <summary>
    /// Форма, предоставляющая выбор графов
    /// </summary>
    public partial class GraphSelectorForm : Form {
        // Поля формы
        public int SelectedGraphIndex { get; private set; }

        public GraphSelectorForm(int graphsCount) {
            InitializeComponent();
            // Говорим, сколько будет графов для выбора
            nudGraphNumber.Maximum = graphsCount;
            // Ставим вместо индекса выбранного графа значение "не выбран"
            SelectedGraphIndex = -1;
        }

        private void bAccept_Click(object sender, EventArgs e) {
            // Присваиваем соответствующий индекс
            SelectedGraphIndex = (int)nudGraphNumber.Value - 1;
            // Ставим соответствующий результат диалога
            DialogResult = DialogResult.OK;
            // Закрываем форму
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e) {
            // Ставим соответствующий результат диалога
            DialogResult = DialogResult.Cancel;
            // Закрываем форму
            Close();
        }
    }
}
