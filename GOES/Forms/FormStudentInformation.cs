using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GOES.Forms {
    /// <summary>
    /// Форма, предназначенная для ввода студентом своей информации для сбора статистики решений
    /// </summary>
    public partial class FormStudentInformation : Form {
        /// <summary>
        /// Имя, введённое студентом
        /// </summary>
        public string StudentName { get; private set; }
        /// <summary>
        /// Группа (класс), введённая студентом
        /// </summary>
        public string StudentGroup { get; private set; }


        public FormStudentInformation() {
            InitializeComponent();
            // Загружаем историю ввода в комбобоксы
            LoadInformationHistory();
            FillInformationHistory();
        }

        // ----Для заполнения истории вводов в комбобоксы
        /// <summary>
        /// Класс для сериализации/десериализации истории вводов в JSON-файл
        /// </summary>
        private class StudentsInformationHistory {
            public List<string> Names { get; set; }
            public List<string> Groups { get; set; }
        }

        /// <summary>
        /// Объект, содержащий историю вводов в комбобоксы
        /// </summary>
        private StudentsInformationHistory informationHistory;

        /// <summary>
        /// Путь до конфигурационного файла, содержащего историю вводов
        /// </summary>
        private static readonly string configPath = "StudentsInfoHistory.json";

        /// <summary>
        /// Количество записей истории, которые мы будем хранить (последние 10, и т.д.)
        /// </summary>
        private const int historyLong = 10;

        /// <summary>
        /// Загрузить историю вводов в комбобоксы из файла.
        /// В результате будет означено поле informationHistory, возвращается флаг успеха
        /// </summary>
        private bool LoadInformationHistory() {
            bool isSuccess = true;
            StreamReader reader;
            try {
                reader = new StreamReader(configPath);
                informationHistory = JsonConvert.DeserializeObject<StudentsInformationHistory>(reader.ReadToEnd());
                reader.Close();
            }
            catch {
                isSuccess = false;
                informationHistory = new StudentsInformationHistory();
                informationHistory.Names = new List<string>();
                informationHistory.Groups = new List<string>();
            }
            return isSuccess;
        }

        /// <summary>
        /// Сохранить историю вводов в комбобоксы в файл. Проводится сохранение из объекта informationHistory,
        /// максимум historyLong записей, остальные удаляются.
        /// Возвращается флаг успеха
        /// </summary>
        private bool SaveInformationHistory() {
            bool isSuccess = true;
            // Добавляем только что введённое имя (но дубликаты нам не нужны)
            if (comboBoxStudentName.SelectedIndex == -1)
                informationHistory.Names.Insert(0, StudentName);
            // Если список имён не пустой - проверяем: нам нужны только последние historyLong записей
            if (informationHistory.Names.Count > historyLong)
                informationHistory.Names.RemoveRange(historyLong, informationHistory.Names.Count - historyLong);
            // Добавляем только что введённую группу (но дубликаты нам не нужны)
            if (comboBoxStudentGroup.SelectedIndex == -1)
                informationHistory.Groups.Insert(0, StudentGroup);
            // Если список групп не пустой - проверяем: нам нужны только последние historyLong записей
            if (informationHistory.Groups.Count > historyLong)
                informationHistory.Groups.RemoveRange(historyLong, informationHistory.Groups.Count - historyLong);
            StreamWriter writer;
            try {
                writer = new StreamWriter(configPath);
                string json = JsonConvert.SerializeObject(informationHistory, Formatting.Indented);
                writer.Write(json);
                writer.Close();
            }
            catch {
                isSuccess = false;
            }
            return isSuccess;
        }

        /// <summary>
        /// Заполнить списки комбобоксов историей вводов
        /// </summary>
        private void FillInformationHistory() {
            // Приостанавливаем обновление комбобоксов, заполняем их списки, возобновляем обновление
            comboBoxStudentName.SuspendLayout();
            comboBoxStudentName.Items.Clear();
            foreach (var name in informationHistory.Names)
                comboBoxStudentName.Items.Add(name);
            comboBoxStudentName.ResumeLayout();

            comboBoxStudentGroup.SuspendLayout();
            comboBoxStudentGroup.Items.Clear();
            foreach (var group in informationHistory.Groups)
                comboBoxStudentGroup.Items.Add(group);
            comboBoxStudentGroup.ResumeLayout();
        }

        // Принятие ввода
        private void buttonAccept_Click(object sender, EventArgs e) {
            // Валидируем: имя и группа не могут быть пустыми и не могут состоять только из пробелов
            if (string.IsNullOrWhiteSpace(comboBoxStudentName.Text) || string.IsNullOrWhiteSpace(comboBoxStudentGroup.Text)) {
                MessageBox.Show("Поля \"Имя\" и \"Группа, класс\" не могут быть пустыми и не могут состоять из одних пробельных символов",
                    "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Если всё хорошо - всё хорошо. Означиваем свойства с введёнными именем/группой
            StudentName = comboBoxStudentName.Text.Trim();
            StudentGroup = comboBoxStudentGroup.Text.Trim();
            // Сохраняем историю ввода в файл, включая только что введённые данные
            SaveInformationHistory();
            DialogResult = DialogResult.OK;
            Close();
        }

        // Отмена ввода
        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
