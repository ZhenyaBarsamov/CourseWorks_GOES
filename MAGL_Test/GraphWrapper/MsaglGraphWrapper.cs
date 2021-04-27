using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace MAGL_Test.GraphWrapper {
    public class MsaglGraphWrapper {
        // ----Свойства графа
        /// <summary>
        /// Граф MSAGL, который оборачивает данный объект
        /// </summary>
        public Graph Graph { get; private set; }

        private readonly MsaglNodeWrapper[] vertices;
        /// <summary>
        /// Список вершин графа, только для чтения
        /// </summary>
        public IList<MsaglNodeWrapper> Vertices => Array.AsReadOnly(vertices);
        /// <summary>
        /// Количество вершин графа
        /// </summary>
        public int VerticesCount => vertices.Length;

        private readonly MsaglEdgeWrapper[] edges;
        /// <summary>
        /// Список рёбер графа, только для чтения
        /// </summary>
        public IList<MsaglEdgeWrapper> Edges => Array.AsReadOnly(edges);
        /// <summary>
        /// Количество рёбер графа
        /// </summary>
        public int EdgesCount => edges.Length;

        /// <summary>
        /// Матрица графа, предоставляющая доступ к рёбрам графа по индексу соединяемых вершин
        /// </summary>
        private readonly MsaglEdgeWrapper[,] matrix;
        /// <summary>
        /// Получить ребро графа по его начальной и конечной вершинам
        /// </summary>
        /// <param name="sourceVertexIndex">Начальная вершина ребра</param>
        /// <param name="targetVertexIndex">Конечная вершина ребра</param>
        /// <returns></returns>
        public MsaglEdgeWrapper GetEdge(int sourceVertexIndex, int targetVertexIndex) => matrix[sourceVertexIndex, targetVertexIndex];

        // ----Индексаторы для доступа к вершинам и рёбрам графа
        /// <summary>
        /// Индексатор вершин графа
        /// </summary>
        public MsaglNodeWrapper this[int vertexIndex] => vertices[vertexIndex];

        /// <summary>
        /// Инексатор дуг графа
        /// </summary>
        public MsaglEdgeWrapper this[int sourceVertexIndex, int targetVertexIndex] => matrix[sourceVertexIndex, targetVertexIndex];

        /// <summary>
        /// Флаг. Истинен, если данный граф является ориентированным
        /// </summary>
        public bool IsDirected => Graph.Directed;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adjacencyMatrix">Матрица смежности графа, на пересечении строки и столбца - флаг присутствия соответствующего ребра</param>
        /// <param name="type">Тип графа</param>
        public MsaglGraphWrapper(bool[,] adjacencyMatrix, bool isDirected) {
            Graph = new Graph("graph");
            Graph.Directed = isDirected;
            int verticesCount = adjacencyMatrix.GetLength(0); // количество вершин в графе (матрица квадратная)
            // Создаём вершины графа
            vertices = new MsaglNodeWrapper[verticesCount];
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++) {
                int vertexNum = vertexIndex + 1;
                // Создаём узел Msagl.Drawing.Node и добавляем его в граф
                var node = Graph.AddNode(vertexNum.ToString());
                // Строим обёртку для созданного узла Msagl.Drawing.Node, и добавляем её в список вершин
                var nodeWrapper = new MsaglNodeWrapper(node);
                nodeWrapper.VertexChainged += GraphPartChaingedHandler;
                vertices[vertexIndex] = nodeWrapper;
            }
            // Создаём рёбра графа, заполняем матрицу рёбер и список рёбер
            matrix = new MsaglEdgeWrapper[verticesCount, verticesCount];
            var edgesList = new List<MsaglEdgeWrapper>();
            // Если граф ориентированный, придётся проходиться по всей матрице
            if (isDirected) {
                for (int rowIndex = 0; rowIndex < verticesCount; rowIndex++) {
                    int rowNum = rowIndex + 1;
                    for (int columnIndex = 0; columnIndex < verticesCount; columnIndex++) {
                        int columnNum = columnIndex + 1;
                        if (adjacencyMatrix[rowIndex, columnIndex]) {
                            // Создаём ребро Msagl.Drawing.Edge (стрелка на конце по умолчанию есть)
                            var edge = Graph.AddEdge(rowNum.ToString(), columnNum.ToString());
                            // Создаём для ребра Msagl.Drawing.Edge обёртку и добавляем её в список
                            var edgeWrapper = new MsaglEdgeWrapper(edge, vertices[rowIndex], vertices[columnIndex]);
                            edgeWrapper.EdgeChainged += GraphPartChaingedHandler;
                            matrix[rowIndex, columnIndex] = edgeWrapper;
                            edgesList.Add(edgeWrapper);
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
                            // Создаём ребро Msagl.Drawing.Edge и убираем стрелку на конце
                            var edge = Graph.AddEdge(rowNum.ToString(), columnNum.ToString());
                            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
                            // Создаём для ребра Msagl.Drawing.Edge обёртку и добавляем её в список
                            var edgeWrapper = new MsaglEdgeWrapper(edge, vertices[rowIndex], vertices[columnIndex]);
                            edgeWrapper.EdgeChainged += GraphPartChaingedHandler;
                            matrix[rowIndex, columnIndex] = matrix[columnIndex, rowIndex] = edgeWrapper;
                            edgesList.Add(edgeWrapper);
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
        /// Делегат для обработчика события изменения графа
        /// </summary>
        /// <param name="graph">Вызвавший событие граф</param>
        public delegate void GraphChaingedEventHandler(MsaglGraphWrapper graph);
        /// <summary>
        /// Событие изменения свойств графа
        /// </summary>
        public event GraphChaingedEventHandler GraphChainged;
    }
}
