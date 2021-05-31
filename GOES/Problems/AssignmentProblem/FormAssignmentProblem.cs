using GOES.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOES.Problems.AssignmentProblem {
    public partial class FormAssignmentProblem : Form, IProblem {
        public FormAssignmentProblem() {
            InitializeComponent();
        }

        public IProblemDescriptor ProblemDescriptor => new AssignmentProblemDescriptor();

        public void InitializeProblem(ProblemExample example, ProblemMode mode) {
            //throw new NotImplementedException();
            matrixDataGridViewExampleMatrix.FillMatrix(new int[,] {
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }
                
            });
        }


        // ----Обработчики событий
        // Вызов окна с лекцией
        private void buttonLecture_Click(object sender, EventArgs e) {
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

        // Очистить черновик
        private void buttonDraftClear_Click(object sender, EventArgs e) {
            textBoxDraft.Clear();
        }

        // Принятие ответа
        private void buttonAcceptAnswer_Click(object sender, EventArgs e) {

        }

        // Начало новой итерации решения
        private void buttonReloadIteration_Click(object sender, EventArgs e) {
            
        }

        // Перезагрузка задачи
        private void buttonReloadProblem_Click(object sender, EventArgs e) {
            //InitializeProblem(maxBipartiteMatchingExample, problemMode);
        }
    }
}
