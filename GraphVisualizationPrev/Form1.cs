using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphVisualization.GraphVizLib;

namespace GraphVisualization {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void canvas_Click(object sender, EventArgs e) {
            var g = canvas.CreateGraphics();

            int[,] m = new int[3, 3];
            m[0, 0] = 1;
            m[0, 1] = 1;
            m[0, 2] = 1;
            m[1, 0] = 1;
            m[1, 1] = 1;
            m[1, 2] = 1;
            m[2, 0] = 1;
            m[2, 1] = 1;
            m[2, 2] = 1;
            Graph gph = new Graph(m);
            gph.Draw(g);
        }
    }
}
