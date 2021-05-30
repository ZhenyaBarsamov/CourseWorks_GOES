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
            throw new NotImplementedException();
        }
    }
}
