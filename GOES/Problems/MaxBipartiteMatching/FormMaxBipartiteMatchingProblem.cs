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
using SGVL.Graphs;
using SGVL.Visualizers;

namespace GOES.Problems.MaxBipartiteMatching {
    public partial class FormMaxBipartiteMatchingProblem : Form, IProblem {
        // ----Атрибуты
        private bool[,] graph;
        private Graph visGraph;
        private int verticesCount;

        public IProblemDescriptor ProblemDescriptor => new MaxBipartiteMatchingProblemDescriptor();

        private MaxBipartiteMatchingProblemExample maxBipartiteMatchingExample;
        private ProblemMode problemMode;

        public void InitializeProblem(ProblemExample example, ProblemMode mode) {
            // Если требуется случайная генерация, а её нет, говорим, что не реализовано
            if (example == null && !ProblemDescriptor.IsRandomExampleAvailable)
                throw new NotImplementedException("Случайная генерация примеров не реализована");
            // Если нам дан пример не задачи о максимальном потоке - ошибка
            if (!(example is MaxBipartiteMatchingProblemExample))
                throw new ArgumentException("Ошибка в выбранном примере. Его невозможно открыть.");
            this.maxBipartiteMatchingExample = example as MaxBipartiteMatchingProblemExample;
            this.problemMode = mode;
        }

        public FormMaxBipartiteMatchingProblem() {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор, создающий задачу поиска максимального паросочетания в двудольном графе с заданным условием
        /// </summary>
        /// <param name="statement">Постановка задачи</param>
        public FormMaxBipartiteMatchingProblem(MaxBipartiteMatchingProblemExample statement) {
            InitializeComponent();
            graph = new bool[statement.GraphMatrix.GetLength(0), statement.GraphMatrix.GetLength(1)];
            for (int row = 0; row < statement.GraphMatrix.GetLength(0); row++)
                for (int col = 0; col < statement.GraphMatrix.GetLength(1); col++)
                    graph[row, col] = statement.GraphMatrix[row, col] != 0;
            visGraph = new Graph(graph, false);
            graphVisualizer.Initialize(visGraph);
        }

        bool isDemonstrationStarted = false;
        int curVertexIndex;
        bool[] usedVertices;
        int[] matching;

        private void ClearVerticesMarking() {
            foreach (var vertex in visGraph.Vertices)
                vertex.BorderColor = Color.Black;
        }

        private void ClearEdgesMarking() {
            foreach (var edge in visGraph.Edges)
                edge.Color = Color.Black;
        }

        bool DFS(int vertexIndex) {
            visGraph.Vertices[vertexIndex].BorderColor = Color.BlueViolet;
            if (usedVertices[vertexIndex])
                return false;
            usedVertices[vertexIndex] = true;
            for (int nextVertexIndex = 0; nextVertexIndex < verticesCount; nextVertexIndex++) {
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
                matching = new int[verticesCount]; // +1, т.к. нумерация вершин в графе у нас с 1
                for (int i = 0; i < verticesCount; i++)
                    matching[i] = -1;
                usedVertices = new bool[verticesCount];
                curVertexIndex = 0;
            }
            ClearVerticesMarking();
            if (curVertexIndex >= verticesCount)
                return;
            visGraph.Vertices[curVertexIndex].BorderColor = Color.Red;
            for (int i = 0; i < verticesCount; i++)
                usedVertices[i] = false;
            // В конце шага отмечает, какие рёбра на текущий момент входят в паросочетание
            if (DFS(curVertexIndex)) {
                ClearEdgesMarking();
                for (int i = 0; i < verticesCount; i++)
                    if (matching[i] != -1)
                        visGraph.GetEdge(i, matching[i]).Color = Color.LightGreen;
            }
            curVertexIndex++;
        }
    }
}
