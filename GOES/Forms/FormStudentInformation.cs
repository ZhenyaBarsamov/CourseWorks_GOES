using System;
using System.Windows.Forms;

namespace GOES.Forms {
    public partial class FormStudentInformation : Form {
        public string StudentName { get; private set; }
        public string StudentGroup { get; private set; }

        public FormStudentInformation() {
            InitializeComponent();
        }

        private void buttonAccept_Click(object sender, EventArgs e) {
            // Валидируем: имя и группа не должны быть пустыми и не могут состоять только из пробелов
            if (string.IsNullOrWhiteSpace(comboBoxStudentName.Text) || string.IsNullOrWhiteSpace(comboBoxStudentGroup.Text)) {
                MessageBox.Show("Поля \"Имя\" и \"Группа, класс\" не могут быть пустыми и не могут состоять из одних пробельных символов",
                    "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Если всё хорошо - всё хорошо
            StudentName = comboBoxStudentName.Text.Trim();
            StudentGroup = comboBoxStudentGroup.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
