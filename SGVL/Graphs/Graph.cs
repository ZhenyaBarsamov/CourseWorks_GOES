using System.Collections.Generic;
using System;

namespace SGVL.Graphs {
    /// <summary>
    /// Класс графа
    /// </summary>
    /// <typeparam name="TVertexData">Тип данных, которые будут привязываться к вершинам графа</typeparam>
    /// <typeparam name="TEdgeData">Тип данных, которые будут привязываться к рёбрам графа</typeparam>
    public class Graph {
        // ----Свойства графа
        private readonly Vertex[] vertices;
        /// <summary>
        /// Список вершин графа, только для чтения
        /// </summary>
        public IList<Vertex> Vertices => Array.AsReadOnly(vertices);
        /// <summary>
        /// Количество вершин графа
        /// </summary>
        public int VerticesCount => vertices.Length;

        private readonly Edge[] edges;
        /// <summary>
        /// Список рёбер графа, только для чтения
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
        /// Получить ребро графа по его начальной и конечной вершинам
        /// </summary>
        /// <param name="sourceVertexIndex">Начальная вершина ребра</param>
        /// <param name="targetVertexIndex">Конечная вершина ребра</param>
        /// <returns></returns>
        public Edge GetEdge(int sourceVertexIndex, int targetVertexIndex) => matrix[sourceVertexIndex, targetVertexIndex];

        public readonly GraphType type;
        /// <summary>
        /// Тип графа
        /// </summary>
        public GraphType Type => type;

        // ----Индексаторы для доступа к вершинам и рёбрам графа
        /// <summary>
        /// Индексатор вершин графа
        /// </summary>
        public Vertex this[int vertexIndex] => vertices[vertexIndex];

        /// <summary>
        /// Инексатор дуг графа
        /// </summary>
        public Edge this[int sourceVertexIndex, int targetVertexIndex] => matrix[sourceVertexIndex, targetVertexIndex];

        // ----Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adjacencyMatrix">Матрица смежности графа, на пересечении строки и столбца - флаг присутствия соответствующего ребра</param>
        /// <param name="type">Тип графа</param>
        public Graph(bool[,] adjacencyMatrix, GraphType graphType) {
            type = graphType;
            int verticesCount = adjacencyMatrix.GetLength(0); // количество вершин в графе (матрица квадратная)
            // Создаём вершины графа
            vertices = new Vertex[verticesCount];
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++) {
                int vertexNum = vertexIndex + 1;
                var vertex = new Vertex(vertexNum);
                vertex.VertexChainged += GraphPartChaingedHandler;
                vertices[vertexIndex] = vertex;
            }
            // Создаём рёбра графа, заполняем матрицу рёбер и список рёбер
            matrix = new Edge[verticesCount, verticesCount];
            List<Edge> edgesList = new List<Edge>();
            // Если граф ориентированный, придётся проходиться по всей матрице
            if (graphType == GraphType.Directed) {
                for (int rowIndex = 0; rowIndex < verticesCount; rowIndex++) {
                    int rowNum = rowIndex + 1;
                    for (int columnIndex = 0; columnIndex < verticesCount; columnIndex++) {
                        int columnNum = columnIndex + 1;
                        if (adjacencyMatrix[rowIndex, columnIndex]) {
                            var edge = new Edge(rowNum, columnNum, true);
                            edge.EdgeChainged += GraphPartChaingedHandler;
                            matrix[rowIndex, columnIndex] = edge;
                            edgesList.Add(edge);
                        }  
                    }
                }
            }
            // Если граф неориентированный, матрица симметрична
            else if (graphType == GraphType.Undirected) {
                for (int rowIndex = 0; rowIndex < verticesCount; rowIndex++) {
                    int rowNum = rowIndex + 1;
                    for (int columnIndex = rowIndex; columnIndex < verticesCount; columnIndex++) {
                        int columnNum = columnIndex + 1;
                        if (adjacencyMatrix[rowIndex, columnIndex]) {
                            var edge = new Edge(rowNum, columnNum, false);
                            edge.EdgeChainged += GraphPartChaingedHandler;
                            matrix[rowIndex, columnIndex] = matrix[columnIndex, rowIndex] = edge;
                            edgesList.Add(edge);
                        }

                    }
                }
            }
            // Если граф смешанный, то необходим другой конструктор - нужны дополнительные данные о том, какие рёбра ориентированные, какие - нет
            else
                throw new NotImplementedException("Для смешанного графа должен быть другой конструктор!");
            edges = edgesList.ToArray();
        }

        // ----События
        /// <summary>
        /// Обработчик события изменения любой части графа: вершины или ребра
        /// </summary>
        /// <param name="chaingedObject">Вызвавший событие объект</param>
        private void GraphPartChaingedHandler(object chaingedObject) {
            GraphChainged(this);
        }

        /// <summary>
        /// Делегат для обработчика события изменения графа
        /// </summary>
        /// <param name="edge">Вызвавший событие граф</param>
        public delegate void GraphChaingedEventHandler(Graph graph);
        /// <summary>
        /// Событие изменения свойств графа
        /// </summary>
        public event GraphChaingedEventHandler GraphChainged;
    }
}
