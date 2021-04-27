using System.Windows.Forms;
using System.Drawing;
using SGVL.Types.Graphs;

namespace SgvlTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            msaglGraphVisualizer1.Initialize(new SGVL.Types.Graphs.Graph(new bool[,] {
                {false, true, true},
                {true, false, true},
                {true, true, false}
            }, true));
            msaglGraphVisualizer1.EdgeSelectedEvent += (Edge edge) => edge.Color = Color.Red;
            msaglGraphVisualizer1.VertexSelectedEvent += (Vertex vertex) => vertex.BorderColor = Color.Red;
            msaglGraphVisualizer1.EdgeSelectedEvent += (Edge edge) => {
                if (edge.SourceVertex.Number == 1 && edge.TargetVertex.Number == 2 || edge.SourceVertex.Number == 2 && edge.TargetVertex.Number == 1)
                    edge.Label = "Medved";
            };
            msaglGraphVisualizer1.VertexSelectedEvent += (Vertex vertex) => {
                if (vertex.Number == 3)
                    vertex.Label = "(+3;15)";
            };
            msaglGraphVisualizer1.IsInteractiveUpdating = false;
        }
    }
}
