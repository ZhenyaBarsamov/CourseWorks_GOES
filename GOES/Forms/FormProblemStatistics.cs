using System;
using System.Windows.Forms;
using GOES.Problems;
using GOES.Forms;

namespace GOES.Forms {
    public partial class FormProblemStatistics : Form {
        public FormProblemStatistics() {
            InitializeComponent();
        }

        public FormProblemStatistics(IProblemStatistics statistics) : this() {
            textLabelStatistics.Text = statistics.GetStatisticsText();
            DialogResult = DialogResult.OK; // по умолчанию будет выход из задания
        }

        private void buttonAccept_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonRestart_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void buttonSend_Click(object sender, EventArgs e) {
            var studentInfoForm = new FormStudentInformation();
            studentInfoForm.ShowDialog();

        }
    }
}
