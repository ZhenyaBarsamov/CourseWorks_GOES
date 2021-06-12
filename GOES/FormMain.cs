using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GOES.Forms;
using GOES.Problems;
using GOES.Problems.MaxFlow;
using GOES.Problems.MaxBipartiteMatching;
using GOES.Problems.AssignmentProblem;

namespace GOES {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
        }


        // Вызов окна "О программе"
        private void buttonAbout_Click(object sender, EventArgs e) {
            var formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        // Открытие окна с текстом лекции
        private void buttonLectures_Click(object sender, EventArgs e) {
            FormLectureViewer formLecture;
            try {
                formLecture = new FormLectureViewer("Theory.html");
            }
            catch {
                MessageBox.Show(
                    "Не удаётся открыть файл с лекцией. Файл с лекцией должен называться \"Theory.html\" и должен " +
                    "находиться в каталоге приложения. Пожалуйста, убедитесь в том, что такой файл действительно существует," +
                    "проверьте его целостность и повторите попытку снова.",
                    "Не удалось открыть лекцию", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            formLecture.Show();
        }

        // Запуск задач
        private void buttonProblems_Click(object sender, EventArgs e) {
            FormProblemSelector formProblemsSelector = new FormProblemSelector(new List<IProblem> { 
                new FormMaxFlowProblem(),
                new FormMaxBipartiteMatchingProblem(),
                new FormAssignmentProblem()
            });
            DialogResult dialogResult = formProblemsSelector.ShowDialog();
            if (dialogResult == DialogResult.Cancel)
                return;
            IProblem problemInterface = formProblemsSelector.SelectedProblem;
            Form problemForm = problemInterface as Form;
            if (problemForm == null) {
                MessageBox.Show("Выбранное задание невозможно отобразить, так как оно не является формой", "Ошибка запуска задания");
                return;
            }
            try {
                problemInterface.InitializeProblem(formProblemsSelector.SelectedExample, formProblemsSelector.SelectedMode);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Ошибка запуска задания");
                return;
            }
            bool isProblemFinished = false;
            do {
                problemForm.ShowDialog();
                // Если у задачи есть статистика, и задача была выполнена (т.е. статистика полная - отображаем её)
                if (problemInterface.ProblemStatistics != null && problemInterface.ProblemStatistics.IsSolved) {
                    FormProblemStatistics formStatistics = new FormProblemStatistics(
                        problemInterface.ProblemDescriptor, problemInterface.ProblemExample, problemInterface.ProblemStatistics);
                    DialogResult dlgRes = formStatistics.ShowDialog();
                    // Завершение выполнения задачи
                    if (dlgRes == DialogResult.OK || dlgRes == DialogResult.Cancel) {
                        isProblemFinished = true;
                    }
                    // Возврат к решённой задаче
                    else if (dlgRes == DialogResult.Retry) {
                        isProblemFinished = false;
                    }
                }
                // Иначе - работа задачи завершена
                else {
                    isProblemFinished = true;
                }
            }
            while (!isProblemFinished);
        }
    }
}
