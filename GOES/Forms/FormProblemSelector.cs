using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GOES.Problems;

namespace GOES.Forms {
    /// <summary>
    /// Форма, предназначенная для выбора задачи
    /// </summary>
    public partial class FormProblemSelector : Form {
        // ----Атрибуты
        private readonly List<IProblem> problems;

        /// <summary>
        /// Выбранная пользователем задача
        /// </summary>
        public IProblem SelectedProblem { get; private set; }
        /// <summary>
        /// Выбранный пользователем пример задачи
        /// </summary>
        public ProblemExample SelectedExample { get; private set; }
        /// <summary>
        /// Выбранный пользователем режим работы с задачей
        /// </summary>
        public ProblemMode SelectedMode { get; private set; }


        // ----Вспомогательные методы
        // Заполнить таблицу с задачами
        private void FillProblems() {
            listBoxProblems.BeginUpdate();
            listBoxProblems.Items.Clear();
            foreach (var problem in problems)
                listBoxProblems.Items.Add(problem.ProblemDescriptor.Name);
            listBoxProblems.EndUpdate();
        }

        // Обновить описание для выбранной задачи
        private void UpdateProblemDescription() {
            int selectedProblemIndex = listBoxProblems.SelectedIndex;
            if (selectedProblemIndex < 0) {
                labelProblemDescription.Text = "";
                return;
            }
            IProblem selectedProblem = problems[selectedProblemIndex];
            labelProblemDescription.Text = selectedProblem.ProblemDescriptor.Description;
        }

        // Заполнить таблицу с примерами выбранной задачи
        private void FillExamples() {
            int selectedProblemIndex = listBoxProblems.SelectedIndex;
            if (selectedProblemIndex < 0)
                return;
            IProblem selectedProblem = problems[selectedProblemIndex];
            listBoxExamples.BeginUpdate();
            listBoxExamples.Items.Clear();
            foreach (var example in selectedProblem.ProblemDescriptor.ProblemExamples)
                listBoxExamples.Items.Add(example.Name);
            if (selectedProblem.ProblemDescriptor.IsRandomExampleAvailable)
                listBoxExamples.Items.Add("Случайный пример");
            listBoxExamples.EndUpdate();
        }

        // Обновить описание для выбранного примера задачи
        private void UpdateExampleDescription() {
            int selectedExampleIndex = listBoxExamples.SelectedIndex;
            if (selectedExampleIndex < 0) {
                labelExampleDescription.Text = "";
                return;
            }
            ProblemExample[] selectedProblemExamples = problems[listBoxProblems.SelectedIndex].ProblemDescriptor.ProblemExamples;
            if (selectedExampleIndex < selectedProblemExamples.Length)
                labelExampleDescription.Text = selectedProblemExamples[selectedExampleIndex].Description;
            else
                labelExampleDescription.Text = "Автоматически сгенерировать пример задачи";
        }

        // Обновить состояние кнопок формы
        private void UpdateControlsState() {
            // Если выбрана задача, и её пример - доступны кнопки начала решения или демонстрации
            buttonSolution.Enabled = buttonDemonstration.Enabled =
                listBoxProblems.SelectedIndex >= 0 && listBoxExamples.SelectedIndex >= 0;
        }

        // ----Конструкторы
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public FormProblemSelector() {
            InitializeComponent();
        }

        /// <summary>
        /// Инициализировать форму выбора задачи
        /// </summary>
        /// <param name="problems">Список задач, которые будут представлены на форме</param>
        public FormProblemSelector(List<IProblem> problems) {
            InitializeComponent();
            this.problems = problems;
            FillProblems();
            FillExamples();
            UpdateProblemDescription();
            UpdateExampleDescription();
            UpdateControlsState();
        }

        // ----Обработчики событий
        private void listBoxProblems_SelectedIndexChanged(object sender, EventArgs e) {
            // Заполняем таблицу с примерами выбранной задачи, обновляем описания и обновляем состояние кнопок
            FillExamples();
            UpdateProblemDescription();
            UpdateExampleDescription();
            UpdateControlsState();
        }

        private void listBoxExamples_SelectedIndexChanged(object sender, EventArgs e) {
            // Обновляем описание выбранного примера и обновляем состояние кнопок
            UpdateExampleDescription();
            UpdateControlsState();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Сохранить выбор пользователя
        private void SaveSelectedOptions(ProblemMode mode) {
            IProblem selectedProblem = problems[listBoxProblems.SelectedIndex];
            ProblemExample[] selectedProblemExamples = selectedProblem.ProblemDescriptor.ProblemExamples;
            SelectedProblem = selectedProblem;
            // Если был выбран пример "Случаный пример", то ему соответствует null (его нет в списке примеров)
            if (listBoxExamples.SelectedIndex < selectedProblemExamples.Length)
                SelectedExample = selectedProblemExamples[listBoxExamples.SelectedIndex];
            else
                SelectedExample = null;
            SelectedMode = mode;
        }

        private void buttonSolution_Click(object sender, EventArgs e) {
            SaveSelectedOptions(ProblemMode.Solution);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonDemonstration_Click(object sender, EventArgs e) {
            SaveSelectedOptions(ProblemMode.Demonstration);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
