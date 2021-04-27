using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using SGVL.Types.Graphs;
using SGVL.Visualizers;

namespace GOES.Problems.MaximalBipartiteMatching {
    public partial class FormMaximalBipartiteMatching : Form {
        // ----Атрибуты
        private bool[,] graph;
        private Graph visualizingGraph;
        private int graphSize => graph.GetLength(0);

        public FormMaximalBipartiteMatching() {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор, создающий задачу поиска максимального паросочетания в двудольном графе с заданным условием
        /// </summary>
        /// <param name="statement">Постановка задачи</param>
        public FormMaximalBipartiteMatching(MaximalBipartiteMatchingStatement statement) {
            InitializeComponent();
            graph = new bool[statement.GraphMatrix.GetLength(0), statement.GraphMatrix.GetLength(1)];
            for (int row = 0; row < statement.GraphMatrix.GetLength(0); row++)
                for (int col = 0; col < statement.GraphMatrix.GetLength(1); col++)
                    graph[row, col] = statement.GraphMatrix[row, col] != 0;
            visualizingGraph = new Graph(graph, false);
            msaglGraphVisualizer.Initialize(visualizingGraph);
        }

        bool isDemonstrationStarted = false;
        int curVertexIndex;
        bool[] usedVertices;
        int[] matching;

        private void ClearVerticesMarking() {
            foreach (var vertex in visualizingGraph.Vertices)
                vertex.BorderColor = Color.Black;
        }

        private void ClearEdgesMarking() {
            foreach (var edge in visualizingGraph.Edges)
                edge.Color = Color.Black;
        }

        bool DFS(int vertexIndex) {
            visualizingGraph.Vertices[vertexIndex].BorderColor = Color.BlueViolet;
            if (usedVertices[vertexIndex])
                return false;
            usedVertices[vertexIndex] = true;
            for (int nextVertexIndex = 0; nextVertexIndex < graphSize; nextVertexIndex++) {
                if (graph[vertexIndex, nextVertexIndex] == false)
                    continue;
                if (matching[nextVertexIndex] == -1 || DFS(matching[nextVertexIndex])) {
                    matching[nextVertexIndex] = vertexIndex;
                    return true;
                }
            }
            return false;
        }

        private void step() {
            if (!isDemonstrationStarted) {
                ClearVerticesMarking();
                ClearEdgesMarking();
                isDemonstrationStarted = true;
                matching = new int[graphSize]; // +1, т.к. нумерация вершин в графе у нас с 1
                for (int i = 0; i < graphSize; i++)
                    matching[i] = -1;
                usedVertices = new bool[graphSize];
                curVertexIndex = 0;
            }
            ClearVerticesMarking();
            if (curVertexIndex >= graphSize)
                return;
            visualizingGraph.Vertices[curVertexIndex].BorderColor = Color.Red;
            for (int i = 0; i < graphSize; i++)
                usedVertices[i] = false;
            // В конце шага отмечает, какие рёбра на текущий момент входят в паросочетание
            if (DFS(curVertexIndex)) {
                ClearEdgesMarking();
                for (int i = 0; i < graphSize; i++)
                    if (matching[i] != -1)
                        visualizingGraph.GetEdge(i, matching[i]).Color = Color.LightGreen;
            }
            curVertexIndex++;
        }

        private void buttonNextStep_Click(object sender, EventArgs e) {
            Thread thread = new Thread(step);
            thread.Start();
        }

        private void buttonToStart_Click(object sender, EventArgs e) {
            isDemonstrationStarted = false;
        }
    }
}
