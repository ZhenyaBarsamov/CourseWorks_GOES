using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GOES.Problems;

namespace GOES.Forms {
    /// <summary>
    /// Форма, предназначенная для выбора задачи
    /// </summary>
    public partial class FormProblemSelector : Form {
        // ----Атрибуты
        private readonly List<Tuple<string, List<string>>> problems;

        /// <summary>
        /// Индекс выбранной пользователем задачи
        /// </summary>
        public int ProblemIndex { get; private set; }
        /// <summary>
        /// Индекс выбранной пользователем постановки задачи
        /// </summary>
        public int StatementIndex { get; private set; }
        /// <summary>
        /// Режим работы с задачей
        /// </summary>
        public ProblemMode Mode { get; private set; }


        // ----Вспомогательные методы
        // Заполнить таблицу с задачами
        private void FillProblems() {
            listBoxProblems.BeginUpdate();
            listBoxProblems.Items.Clear();
            foreach (var problem in problems)
                listBoxProblems.Items.Add(problem.Item1);
            listBoxProblems.EndUpdate();
        }

        // Заполнить таблицу с постановками задачи, имеющей заданный индекс в списке
        private void FillProblemStatements(int problemIndex) {
            if (problemIndex < 0)
                return;
            listBoxProblemStatements.BeginUpdate();
            listBoxProblemStatements.Items.Clear();
            foreach (var statement in problems[problemIndex].Item2)
                listBoxProblemStatements.Items.Add(statement);
            listBoxProblemStatements.EndUpdate();
        }

        // Обновить состояние кнопок формы
        private void UpdateControlsState() {
            // Если выбрана задача, и её конкретная постановка - доступны кнопки начала решения или демонстрации
            buttonSolution.Enabled = buttonDemonstration.Enabled =
                listBoxProblems.SelectedIndex != -1 && listBoxProblemStatements.SelectedIndex != -1;
        }

        // ----Конструкторы
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public FormProblemSelector() {
            InitializeComponent();
        }

        /// <summary>
        /// Инициализировать форму выбора задач
        /// </summary>
        /// <param name="problems">Список кортежей формата (название задачи; список постановок задачи)</param>
        public FormProblemSelector(List<Tuple<string, List<string>>> problems) {
            InitializeComponent();
            this.problems = problems;
            FillProblems();
            FillProblemStatements(listBoxProblems.SelectedIndex);
            UpdateControlsState();
        }

        // ----Обработчики событий
        private void listBoxProblems_SelectedIndexChanged(object sender, EventArgs e) {
            // Заполняем таблицу с постановками задачи соответствующими постановками и обновляем состояние кнопок
            FillProblemStatements(listBoxProblems.SelectedIndex);
            UpdateControlsState();
        }

        private void listBoxProblemStatements_SelectedIndexChanged(object sender, EventArgs e) {
            // Обновляем состояние кнопок
            UpdateControlsState();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSolution_Click(object sender, EventArgs e) {
            ProblemIndex = listBoxProblems.SelectedIndex;
            StatementIndex = listBoxProblemStatements.SelectedIndex;
            Mode = ProblemMode.Solution;
        }

        private void buttonDemonstration_Click(object sender, EventArgs e) {
            ProblemIndex = listBoxProblems.SelectedIndex;
            StatementIndex = listBoxProblemStatements.SelectedIndex;
            Mode = ProblemMode.Demonstration;
        }
    }
}
