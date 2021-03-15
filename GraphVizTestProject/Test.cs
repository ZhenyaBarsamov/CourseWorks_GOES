using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MyClassLibrary.GraphClasses;


namespace GraphVizTestProject {
    public partial class Test : Form {
        EduGraph graph; // граф для тестирования

        int graphID;
        int mf;

        private void TestVertexMarking(VertexInfo vertex) {
            egViz.MarkVertex(vertex, Color.MediumVioletRed);
        }

        private void TestEdgeMarking(EdgeInfo edge) {
            egViz.MarkEdge(edge, Color.MediumVioletRed);
        }

        private void SearchingFlowThread() {
            Solver s = new Solver();
            int mf;
            int endV;
            if (graphID == 1)
                endV = 7;
            else
                endV = 4;
            mf = s.FindMaximalFlow(graph, 1, endV, out List<EdgeInfo> ms, egViz);
        }

        public Test() {
            InitializeComponent();
            // Данные тестового графа 1
            //int[,] adjacencyMatrix = {
            //    {0, 15, 0, 25, 0, 0, 0 },
            //    {0, 0, 16, 7, 0, 0, 0 },
            //    {0, 0, 0, 0, 4, 0, 9 },
            //    {0, 0, 0, 0, 18, 3, 0 },
            //    {0, 0, 0, 0, 0, 6, 25 },
            //    {0, 2, 3, 0, 0, 0, 0 },
            //    {0, 0, 0, 0, 0, 0, 0 }
            //};
            //PointF[] verticesCoordinates = {
            //    new PointF(50, 200), new PointF(200, 50), new PointF(350, 50), new PointF(200, 350),
            //    new PointF(350, 350), new PointF(275, 200), new PointF(500, 200)};
            //graphID = 1;

            // Данные тестового графа 2
            //int[,] adjacencyMatrix = {
            //    {0, 7, 0, 0, 5, 0, 3, 0 },
            //    {0, 0, 2, 0, 0, 0, 0, 3 },
            //    {0, 0, 0, 6, 0, 0, 0, 0 },
            //    {0, 0, 0, 0, 0, 0, 0, 0 },
            //    {0, 0, 2, 0, 0, 6, 0, 0 },
            //    {0, 0, 0, 4, 0, 0, 0, 1 },
            //    {0, 0, 1, 0, 2, 0, 0, 6 },
            //    {0, 0, 0, 5, 0, 0, 0, 0 }
            //};
            //PointF[] verticesCoordinates = {
            //    new PointF(50, 200), new PointF(250, 50), new PointF(450, 50), new PointF(650, 200),
            //    new PointF(180, 200), new PointF(500, 200), new PointF(250, 400), new PointF(450, 400)};
            //graphID = 2;

            // Данные тестового графа 3
            int[,] adjacencyMatrix = {
                {0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 1, 0, 1, 0, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0, 1, 0, 1, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 0, 1, 0, 1, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
            PointF[] verticesCoordinates = {
                new PointF(200, 50), new PointF(350, 50), new PointF(200, 150), new PointF(350, 150),
                new PointF(200, 250), new PointF(350, 250), new PointF(200, 350), new PointF(350, 350),
                new PointF(50, 175), new PointF(500, 175)
            };
            graphID = 3;

            EduGraph testGraph = new EduGraph(adjacencyMatrix, verticesCoordinates);
            graph = testGraph;
            //MessageBox.Show("Ура!");
            egViz.Initialize(graph, true); // инициализируем интерактивный контрол визуализации
            // Подписка на события
            egViz.VertexSelectedEvent += TestVertexMarking;
            egViz.EdgeSelectedEvent += TestEdgeMarking;
        }


        private void egViz_Click(object sender, EventArgs e) {
            // egViz.DrawGraph();
        }

        private void снятьВыделениеВершинToolStripMenuItem_Click(object sender, EventArgs e) {
            egViz.ClearVerticesMarking();
        }

        private void снятьВыделениеДугToolStripMenuItem_Click(object sender, EventArgs e) {
            egViz.ClearEdgesMarking();
        }

        private void поискВГлубинуToolStripMenuItem_Click(object sender, EventArgs e) {
            // Чистим граф
            graph.ClearVerticesOutsideLabels();
            egViz.ClearEdgesMarking();
            egViz.ClearVerticesMarking();
            egViz.DrawGraph();
            // Решаем в отдельном потоке
            //Thread thread = new Thread(SearchingFlowThread);
            //thread.Start();
            // Решаем в этом потоке
            Solver s = new Solver();
            int mf;
            int startV = 1;
            int endV;
            if (graphID == 1)
                endV = 7;
            else if (graphID == 2)
                endV = 4;
            else {
                startV = 9;
                endV = 10;
            }
                
            mf = s.FindMaximalFlow(graph, startV, endV, out List<EdgeInfo> mc, egViz);
            foreach (var eg in mc)
                egViz.MarkEdge(eg, Color.BlueViolet);
            MessageBox.Show($"Минимальный разрез выделен.\nМаксимальный поток между вершинами {startV} и {endV}  = {mf.ToString()}.", "Максимальный поток и минимальный разрез");
        }
    }
}
