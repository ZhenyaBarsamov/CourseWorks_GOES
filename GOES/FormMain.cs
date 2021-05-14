using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GOES.Forms;
using GOES.Problems;
using GOES.Problems.MaxFlow;
using GOES.Problems.MaxBipartiteMatching;

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
                new FormMaxBipartiteMatchingProblem()
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
            problemForm.ShowDialog();
        }
    }
}
