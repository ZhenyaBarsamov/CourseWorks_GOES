using System;
using System.Windows.Forms;
using GOES.Problems;
using GOES.StatisticsSend;

namespace GOES.Forms {
    /// <summary>
    /// Форма, предназначенная для отображения статистики решения задачи.
    /// Предоставляет также возможность отправки статистики на сервер.
    /// </summary>
    public partial class FormProblemStatistics : Form {
        IProblemDescriptor problemDescriptor;
        ProblemExample problemExample;
        IProblemStatistics problemStatistics;

        public FormProblemStatistics() {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="problemDescriptor">Объект, содержащий описание решённой задачи</param>
        /// <param name="problemExample">Объект, содержащий решённый пример</param>
        /// <param name="problemStatistics">Объект, содержащий статистику решённой задачи (примера)</param>
        public FormProblemStatistics(IProblemDescriptor problemDescriptor, ProblemExample problemExample, IProblemStatistics problemStatistics) : this() {
            // Сохраняем всю информацию
            this.problemDescriptor = problemDescriptor;
            this.problemStatistics = problemStatistics;
            this.problemExample = problemExample;
            // Отображаем статистику
            textLabelStatistics.Text = problemStatistics.GetStatisticsText();
        }

        // Выход из задания
        private void buttonAccept_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        // Вернуться к решённому заданию
        private void buttonRestart_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Retry;
            Close();
        }

        // Отправить статистику
        private void buttonSend_Click(object sender, EventArgs e) {
            // Запрашиваем у студента его имя/группу
            FormStudentInformation studentInfoForm = new FormStudentInformation();
            studentInfoForm.ShowDialog();
            // Если студент отменил ввод - отправка отменена
            if (studentInfoForm.DialogResult != DialogResult.OK)
                return;
            // Если ввод принят - получаем конфиг с информацией о сервере
            var serverConfig = ServerConfig.GetServerConfig(out string errorMessage);
            if (serverConfig == null) {
                MessageBox.Show($"Ошибка при получении информации о сервере из конфигурационного файла.{Environment.NewLine}{errorMessage}", 
                    "Отправка результатов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var statSender = new StatisticsSender(serverConfig);
            // Отправляем
            bool isSuccess = 
                statSender.Send(problemDescriptor, problemExample, problemStatistics, studentInfoForm.StudentName, studentInfoForm.StudentGroup, out errorMessage);
            if (!isSuccess) {
                MessageBox.Show($"Ошибка при отправке результатов на сервер.{Environment.NewLine}{errorMessage}",
                    "Отправка результатов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Если всё прошло хорошо - говорим об этом
            MessageBox.Show($"Результаты успешно отправлены",
                    "Отправка результатов", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Запрещаем повторную отправку результата
            buttonSend.Enabled = false;
        }
    }
}
