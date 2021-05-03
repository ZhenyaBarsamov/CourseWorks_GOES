using System;
using System.Collections.Generic;
using System.Drawing;

namespace SGVL.Graphs {
    /// <summary>
    /// Класс графа
    /// </summary>
    public class Graph {
        // ----Свойства графа
        private readonly Vertex[] vertices;
        /// <summary>
        /// Список вершин графа, изменение коллекции недопустимо
        /// </summary>
        public IList<Vertex> Vertices => Array.AsReadOnly(vertices);
        /// <summary>
        /// Количество вершин графа
        /// </summary>
        public int VerticesCount => vertices.Length;

        private readonly Edge[] edges;
        /// <summary>
        /// Список рёбер графа, изменение коллекции недопустимо
        /// </summary>
        public IList<Edge> Edges => Array.AsReadOnly(edges);
        /// <summary>
        /// Количество рёбер графа
        /// </summary>
        public int EdgesCount => edges.Length;

        /// <summary>
        /// Матрица графа, предоставляющая доступ к рёбрам графа по индексу соединяемых вершин
        /// </summary>
        private readonly Edge[,] matrix;
        /// <summary>
        /// Получить ребро графа по его начальной и конечной вершинам. Если ребро не существует - null
        /// </summary>
        /// <param name="sourceVertexIndex">Начальная вершина ребра</param>
        /// <param name="targetVertexIndex">Конечная вершина ребра</param>
        /// <returns></returns>
        public Edge GetEdge(int sourceVertexIndex, int targetVertexIndex) => matrix[sourceVertexIndex, targetVertexIndex];

        // ----Индексаторы для доступа к вершинам и рёбрам графа
        /// <summary>
        /// Индексатор вершин графа
        /// </summary>
        public Vertex this[int vertexIndex] => vertices[vertexIndex];

        /// <summary>
        /// Инексатор дуг графа
        /// </summary>
        public Edge this[int sourceVertexIndex, int targetVertexIndex] => matrix[sourceVertexIndex, targetVertexIndex];

        /// <summary>
        /// Флаг. Истинен, если данный граф является ориентированным
        /// </summary>
        public bool IsDirected { get; private set; }


        // ----Методы
        /// <summary>
        /// Задать цвет границ всем вершинам
        /// </summary>
        /// <param name="color">Цвет</param>
        public void SetVerticesBorderColor(Color color) {
            foreach (var vertex in Vertices)
                vertex.BorderColor = color;
        }

        /// <summary>
        /// Задать цвет заливки всем вершинам
        /// </summary>
        /// <param name="color">Цвет</param>
        public void SetVerticesFillColor(Color color) {
            foreach (var vertex in Vertices)
                vertex.FillColor = color;
        }

        /// <summary>
        /// Задать цвет всем рёбрам
        /// </summary>
        /// <param name="color">Цвет</param>
        public void SetEdgesColor(Color color) {
            foreach (var edge in Edges)
                edge.Color = color;
        }

        /// <summary>
        /// Задать выделение жирным всем вершинам
        /// </summary>
        /// <param name="isBold">Флаг, показывающий, необходимо ли включить выделение жирным</param>
        public void SetVerticesBold(bool isBold) {
            foreach (var vertex in Vertices)
                vertex.Bold = isBold;
        }

        /// <summary>
        /// Задать выделение жирным всем рёбрам
        /// </summary>
        /// <param name="isBold">Флаг, показывающий, необходимо ли включить выделение жирным</param>
        public void SetEdgesBold(bool isBold) {
            foreach (var edge in Edges)
                edge.Bold = isBold;
        }


        // ----Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adjacencyMatrix">Матрица смежности графа, на пересечении строки и столбца - флаг присутствия соответствующего ребра</param>
        /// <param name="type">Тип графа - ориентированный ли он</param>
        public Graph(bool[,] adjacencyMatrix, bool isDirected) {
            IsDirected = isDirected;
            int verticesCount = adjacencyMatrix.GetLength(0); // количество вершин в графе (матрица квадратная)
            // Создаём вершины графа
            vertices = new Vertex[verticesCount];
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++) {
                int vertexNum = vertexIndex + 1;
                // Строим объект вершины и добавляем его в список вершин
                var vertex = new Vertex(vertexNum);
                vertex.VertexChainged += GraphPartChaingedHandler;
                vertices[vertexIndex] = vertex;
            }
            // Создаём рёбра графа, заполняем матрицу рёбер и список рёбер
            matrix = new Edge[verticesCount, verticesCount];
            var edgesList = new List<Edge>();
            // Если граф ориентированный, придётся проходиться по всей матрице
            if (isDirected) {
                for (int rowIndex = 0; rowIndex < verticesCount; rowIndex++) {
                    int rowNum = rowIndex + 1;
                    for (int columnIndex = 0; columnIndex < verticesCount; columnIndex++) {
                        int columnNum = columnIndex + 1;
                        if (adjacencyMatrix[rowIndex, columnIndex]) {
                            // Создаём ребро и добавляем его в список и в матрицу
                            var edge = new Edge(vertices[rowIndex], vertices[columnIndex]);
                            edge.EdgeChanged += GraphPartChaingedHandler;
                            matrix[rowIndex, columnIndex] = edge;
                            edgesList.Add(edge);
                        }
                    }
                }
            }
            // Если граф неориентированный, матрица симметрична
            else {
                for (int rowIndex = 0; rowIndex < verticesCount; rowIndex++) {
                    int rowNum = rowIndex + 1;
                    for (int columnIndex = 0; columnIndex <= rowIndex; columnIndex++) {
                        int columnNum = columnIndex + 1;
                        if (adjacencyMatrix[rowIndex, columnIndex]) {
                            // Создаём ребро и добавляем его в список и в матрицу
                            var edge = new Edge(vertices[rowIndex], vertices[columnIndex]);
                            edge.EdgeChanged += GraphPartChaingedHandler;
                            matrix[rowIndex, columnIndex] = matrix[columnIndex, rowIndex] = edge;
                            edgesList.Add(edge);
                        }

                    }
                }
            }
            edges = edgesList.ToArray();
        }


        // ----События
        /// <summary>
        /// Обработчик события изменения любой части графа: вершины или ребра
        /// </summary>
        /// <param name="chaingedObject">Вызвавший событие объект</param>
        private void GraphPartChaingedHandler(object chaingedObject) {
            GraphChainged?.Invoke(this);
        }

        /// <summary>
        /// Событие изменения свойств графа
        /// </summary>
        public event GraphChaingedEventHandler GraphChainged;
    }
}
