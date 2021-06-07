using System;
using System.Windows.Forms;
using GOES.Problems;

namespace GOES.Forms {
    public partial class FormProblemStatistics : Form {
        public FormProblemStatistics() {
            InitializeComponent();
        }

        public FormProblemStatistics(IProblemStatistics statistics) : this() {
            textLabelStatistics.Text = statistics.GetStatisticsText();
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
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
