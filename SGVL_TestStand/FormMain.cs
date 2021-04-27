using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGVL.Types.Graphs;
using SGVL.Types.Visualizers;
using SGVL.Visualizers.SimpleGraphVisualizer;
using SGVL.Visualizers.MsaglGraphVisualizer;

namespace SGVL_TestStand {
    public partial class FormMain : Form {
        // ----Атрибуты
        private Graph visualizingGraph;
        private IGraphVisualizer visualizer;

        // ----Методы
        private bool[,] ReadGraph(out int demension, out PointF[] layoutPoints) {
            // Получаем размерность 
            demension = textBoxGraphMatrix.Lines.First().Split(' ').Length;
            // Считываем матрицу смежности
            var matrix = new bool[demension, demension];
            if (checkBoxDirectedGraph.Checked) {
                for (int row = 0; row < demension; row++) {
                    var lineElems = textBoxGraphMatrix.Lines[row].Trim().Split(' ');
                    for (int col = 0; col < demension; col++)
                        matrix[row, col] = lineElems[col] == "1";
                }
            }
            else {
                for (int row = 0; row < demension; row++) {
                    var lineElems = textBoxGraphMatrix.Lines[row].Trim().Split(' ');
                    for (int col = 0; col <= row; col++)
                        matrix[row, col] = lineElems[col] == "1";
                }
            }
            // Считываем (demension+1) строку с координатами вершин в формате: x1,y1 x2,y2 x3,y3 ... (если она есть)
            if (textBoxGraphMatrix.Lines.Length > demension) {
                layoutPoints = new PointF[demension];
                var lineElems = textBoxGraphMatrix.Lines[demension + 1].Trim().Split(' ');
                for (int elemIndex = 0; elemIndex < demension; elemIndex++) {
                    var coords = lineElems[elemIndex].Split(',');
                    layoutPoints[elemIndex] = new PointF {
                        X = Convert.ToSingle(coords[0]),
                        Y = Convert.ToSingle(coords[1])
                    };
                }
            }
            else
                layoutPoints = null;
            return matrix;
        }

        public FormMain() {
            InitializeComponent();
            visualizingGraph = null;
            SetVisualizer();
        }

        // Задать граф
        private void buttonInitializeGraph_Click(object sender, EventArgs e) {
            bool[,] matrix;
            int demension;
            PointF[] layout;
            try {
                matrix = ReadGraph(out demension, out layout);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            visualizingGraph = new Graph(matrix, checkBoxDirectedGraph.Checked);
            // Задаём вершинам координаты
            if (layout != null)
                for (var vertIndex = 0; vertIndex < demension; vertIndex++)
                    visualizingGraph.Vertices[vertIndex].DrawingCoords = new PointF {
                        X = layout[vertIndex].X,
                        Y = layout[vertIndex].Y
                    };
            // Задаём вершинам метки
            foreach (var vertex in visualizingGraph.Vertices)
                vertex.Label = "в";
            // Задаём рёбрам метки
            foreach (var edge in visualizingGraph.Edges)
                edge.Label = "р";
            visualizer.Initialize(visualizingGraph);
        }

        private Color GenerateRandomColor() {
            Random rand = new Random();
            return Color.Purple;
        }

        private void OnSelectedVertex(Vertex vertex) {
            Color color = GenerateRandomColor();
            if (radioButtonActionColor.Checked)
                if (vertex.BorderColor == color)
                    vertex.BorderColor = Color.Black;
                else
                    vertex.BorderColor = color;
        }

        private void OnSelectedEdge(Edge edge) {
            Color color = GenerateRandomColor();
            if (radioButtonActionColor.Checked)
                if (edge.Color == color)
                    edge.Color = Color.Black;
                else
                    edge.Color = color;
            else if (radioButtonActionBold.Checked)
                edge.Bold = !edge.Bold;
        }

        // --Изменение визуализатора
        private void SetVisualizer() {
            SuspendLayout();
            // Удаляем старый элемент управления
            if (visualizer != null) {
                groupBoxViz.Controls.Clear();
                visualizer.VertexSelectedEvent -= OnSelectedVertex;
                visualizer.EdgeSelectedEvent -= OnSelectedEdge;
            }
            // Добавляем новый
            if (radioButtonVisualizatorSgvl.Checked) {
                SimpleGraphVisualizer sgv = new SimpleGraphVisualizer();
                sgv.Dock = DockStyle.Fill;
                groupBoxViz.Controls.Add(sgv);
                visualizer = sgv;
            }
            else {
                MsaglGraphVisualizer msaglv = new MsaglGraphVisualizer();
                msaglv.Dock = DockStyle.Fill;
                groupBoxViz.Controls.Add(msaglv);
                visualizer = msaglv;
            }
            ResumeLayout();
            // Инициализируем его графом
            if (visualizingGraph != null)
                visualizer.Initialize(visualizingGraph);
            groupBoxViz.Invalidate();
            // Задаём настройки
            SetVisualizerInteractiveMode();
            // Подписываемся на события
            visualizer.VertexSelectedEvent += OnSelectedVertex;
            visualizer.EdgeSelectedEvent += OnSelectedEdge;
        }

        private void radioButtonVisualizatorSgvl_CheckedChanged(object sender, EventArgs e) {
            SetVisualizer();
        }

        private void radioButtonVisualizatorMsagl_CheckedChanged(object sender, EventArgs e) {
            SetVisualizer();
        }

        // --Изменение режима интерактивности
        private void SetVisualizerInteractiveMode() {
            if (radioButtonInteractiveAll.Checked)
                visualizer.InteractiveMode = InteractiveMode.Interactive;
            else if (radioButtonInteractiveVertices.Checked)
                visualizer.InteractiveMode = InteractiveMode.OnlyVertices;
            else if (radioButtonInteractiveNone.Checked)
                visualizer.InteractiveMode = InteractiveMode.NonInteractive;
            else if (radioButtonInteractiveEdges.Checked)
                visualizer.InteractiveMode = InteractiveMode.OnlyEdges;
        }

        private void radioButtonInteractiveAll_CheckedChanged(object sender, EventArgs e) {
            SetVisualizerInteractiveMode();
        }

        private void radioButtonInteractiveVertices_CheckedChanged(object sender, EventArgs e) {
            SetVisualizerInteractiveMode();
        }

        private void radioButtonInteractiveEdges_CheckedChanged(object sender, EventArgs e) {
            SetVisualizerInteractiveMode();
        }

        private void radioButtonInteractiveNone_CheckedChanged(object sender, EventArgs e) {
            SetVisualizerInteractiveMode();
        }

        // --Изменение режима изменения переноса вершин
        private void SetVisualizerLayoutMode() {
            if (radioButtonLayoutDynamic.Checked)
                visualizer.IsVerticesMoving = true;
            else if (radioButtonLayoutStatic.Checked)
                visualizer.IsVerticesMoving = false;
        }

        private void radioButtonLayoutDynamic_CheckedChanged(object sender, EventArgs e) {
            SetVisualizerLayoutMode();
        }

        private void radioButtonLayoutStatic_CheckedChanged(object sender, EventArgs e) {
            SetVisualizerLayoutMode();
        }
    }
}
