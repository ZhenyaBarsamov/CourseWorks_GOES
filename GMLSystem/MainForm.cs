using System;
using System.Windows.Forms;
using GMLSystem.Forms;
using GMLSystem.Classes;

namespace GMLSystem {
    public partial class MainForm : Form {
        // Поля формы
        /// <summary>
        /// Хранилище графов-примеров
        /// </summary>
        private TrainingGraphsStorage graphsStorage;

        // Конструктор
        public MainForm() {
            InitializeComponent();
            graphsStorage = new TrainingGraphsStorage();
        }

        // Запуск демонстрационного примера
        private void bDemonstrationExample_Click(object sender, EventArgs e) {
            var gsForm = new GraphSelectorForm(graphsStorage.GraphsCount);
            // Открываем форму и ждём выбора. Отмена - всё, выходим
            if (gsForm.ShowDialog() == DialogResult.Cancel)
                return;
            var dExampleForm = new TaskForm(graphsStorage[gsForm.SelectedGraphIndex]);
            dExampleForm.ShowDialog();
        }

        // Открытие текста лекции
        private void bLecture_Click(object sender, EventArgs e) {
            // Открываем файл лекции
            var lectureBox = new LectureBox();
            lectureBox.ShowLecture();
        }

        // Открытие окна "О программе"
        private void bAboutProgram_Click(object sender, EventArgs e) {
            AboutSystem aboutSystem = new AboutSystem();
            aboutSystem.ShowDialog();
        }
    }
}
