using System;
using System.Windows.Forms;

namespace FNLSystem.Forms {
    /// <summary>
    /// Форма для отображения статистики задания на поиск максимального потока
    /// </summary>
    public partial class TaskStatisticsForm : Form {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="incorrectWay">Количество ошибок "неправильный путь"</param>
        /// <param name="incorrectMark">Количество ошибок "неправильная метка"</param>
        /// <param name="incorrectFlowIncrease">Количество ошибок "неправильное увеличение потока"</param>
        /// <param name="incorrectMinCutEdge">Количество ошибок "неравильная дуга минимального разреза</param>
        public TaskStatisticsForm(int incorrectWay, int incorrectMark, int incorrectFlowIncrease, int incorrectMinCutEdge) {
            InitializeComponent();
            lIncorrectWay.Text = $"Количество неправильно выбранных путей: {incorrectWay}";
            lIncorrectMark.Text = $"Количество неправильно поставленных меток: {incorrectMark}";
            lIncorrectFlowIncrease.Text = $"Количество неправильных увеличений потока: {incorrectFlowIncrease}";
            lIncorrectMinCutEdge.Text = $"Количество неправильно выделенных дуг разреза: {incorrectMinCutEdge}";
            int mistakesSum = incorrectWay + incorrectMark + incorrectFlowIncrease + incorrectMinCutEdge;
            lMistakesSum.Text = $"Общее количество ошибок: {mistakesSum}";
            string rank;
            if (mistakesSum == 0)
                rank = "Отличник! ヾ(⌐■_■)ノ♪";
            else if (mistakesSum <= 2)
                rank = @"Ударник ᕦ(ò_óˇ)ᕤ";
            else if (mistakesSum <= 4)
                rank = @"Хорошист \ (•◡•) /";
            else if (mistakesSum <= 6)
                rank = @"Ученик ʕ•ᴥ•ʔ";
            else
                rank = @"Новичок ¯\(°_o)/¯";
            lVerdict.Text = $"Ваш ранг: {rank}";
        }

        private void bAccept_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
