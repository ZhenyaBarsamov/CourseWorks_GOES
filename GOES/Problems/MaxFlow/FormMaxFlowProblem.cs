using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOES.Problems.MaxFlow {
    public partial class FormMaxFlowProblem : Form, IProblem {
        // ----Атрибуты
        private MaxFlowProblemExample example;
        private ProblemMode mode;

        public FormMaxFlowProblem() {
            InitializeComponent();
        }

        public IProblemDescriptor ProblemDescriptor => new MaxFlowProblemDescriptor();

        public void InitializeProblem(ProblemExample example, ProblemMode mode) {
            // Если требуется случайная генерация, а её нет, говорим, что не реализовано
            if (example == null && !ProblemDescriptor.IsRandomExampleAvailable)
                throw new NotImplementedException("Случайная генерация примеров не реализована");
            // Если нам дан пример не задачи о максимальном потоке - ошибка
            if (!(example is MaxFlowProblemExample))
                throw new ArgumentException("Ошибка в выбранном примере. Его невозможно открыть.");
            this.example = example as MaxFlowProblemExample;
            this.mode = mode;
        }
    }
}
