using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GraphVisualization.GraphVizLib {
    /// <summary>
    /// Класс графа
    /// </summary>
    class Graph {
        /// <summary>
        /// Матрица смежности графа
        /// </summary>
        public int[,] AdjMatrix { get; private set; }

        /// <summary>
        /// Словарь вершин графа
        /// ID вершины -> Вершина
        /// </summary>
        public Dictionary<int, Vertex> Vertecies { get; private set; }

        /// <summary>
        /// Словарь рёбер графа
        /// Начало ребра -> Конец ребра -> Ребро
        /// </summary>
        public Dictionary<int,Dictionary<int, Edge>> Edges { get; private set; }

        public Graph(int[,] adjMatrix) {
            AdjMatrix = adjMatrix;

            // Создаём вершины
            Vertecies = new Dictionary<int, Vertex>();
            for (int i = 1; i <= adjMatrix.GetLength(0); i++) {
                var vertexPos = PointF.Empty;
                if (i % 2 == 0) {
                    vertexPos.X = 100 * i / 2;
                    vertexPos.Y = 240;
                }
                else {
                    vertexPos.X = 100 * i / 2;
                    vertexPos.Y = 40;
                }
                Vertecies[i] = new Vertex(i, i.ToString(), $"Ура x{i}!", Color.Black, vertexPos, 20);
            }

            // Создаём рёбра
            Edges = new Dictionary<int, Dictionary<int, Edge>>();
            for (int i = 0; i <  AdjMatrix.GetLength(0); i++)
                for (int j = 0; j < AdjMatrix.GetLength(1); j++) {
                    if (AdjMatrix[i,j] != 0) {
                        if (!Edges.ContainsKey(i+1))
                            Edges[i+1] = new Dictionary<int, Edge>();
                        Edges[i+1][j+1] = new Edge(1, "dada", Color.Black);
                    }
                }
        }

        public void Draw(Graphics canvas) {
            // Рисуем рёбра
            foreach(var begVert in Edges.Keys)
                foreach(var endVert in Edges[begVert].Keys) {
                    var startVert = Vertecies[begVert];
                    var finishVert = Vertecies[endVert];
                    var startPoint = new PointF(startVert.CenterPos.X, startVert.CenterPos.Y);
                    var finishPoint = new PointF(finishVert.CenterPos.X, finishVert.CenterPos.Y);
                    canvas.DrawLine(new Pen(Color.Black), startPoint, finishPoint);
                }
            // Рисуем вершины
            foreach (var vert in Vertecies.Values)
                vert.Draw(canvas);
        }
    }
}
