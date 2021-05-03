using MsaglGraphs = Microsoft.Msagl.Drawing;
using SgvlGraphs = SGVL.Types.Graphs;

namespace SGVL.Visualizers.MsaglGraphVisualizer {
    /// <summary>
    /// Класс-обёртка, связывающий класс SGVL.Types.Graphs.Graph
    /// с классом Microsoft.Msagl.Drawing.Graph и поддерживаюший соответствие их атрибутов.
    /// Связь односторонняя - изменения в SGVL-графе отражаются на MSAGL-графе, но не наоборот.
    /// </summary>
    class MsaglGraphWrapper {
        // ----Свойства
        /// <summary>
        /// Граф SGVL, для которого создан соответствующий граф MSAGL
        /// </summary>
        public SgvlGraphs.Graph SgvlGraph { get; private set; }
        /// <summary>
        /// Граф MSAGL, соответствующий графу SGVL
        /// </summary>
        public MsaglGraphs.Graph MsaglGraph { get; private set; }


        // ----Конструктор
        /// <summary>
        /// Конструктор, создающий класс, связывающий SGVL граф с вновь созданным 
        /// соответствующим графом MSAGL
        /// </summary>
        /// <param name="graph">SGVL граф, для которого необходим соответствующий MSAGL граф</param>
        public MsaglGraphWrapper(SgvlGraphs.Graph graph) {
            SgvlGraph = graph;
            MsaglGraph = new MsaglGraphs.Graph("graph");
            MsaglGraph.Directed = graph.IsDirected;
            // Создаём вершины графа Msagl
            foreach (var vertex in graph.Vertices) {
                var msaglNode = MsaglGraph.AddNode(vertex.Number.ToString());
                // Приводим в соответствие значения их атрибутов
                UpdateMsaglNode(msaglNode, vertex);
                // Задаём форму в виде круга
                msaglNode.Attr.Shape = MsaglGraphs.Shape.Circle;
                // Задаём размер шрифта
                msaglNode.Label.FontSize = 5;
                // Подписываемся на события изменения атрибутов вершины
                vertex.LabelChanged += OnVertexLabelChanged;
                vertex.BorderColorChanged += OnVertexBorderColorChanged;
                vertex.FillColorChanged += OnVertexFillColorChanged;
                // На изменение координат мы не подписываемся - это пока не работает
            }
            // Создаём рёбра графа Msagl
            foreach (var edge in graph.Edges) {
                var msaglEdge = MsaglGraph.AddEdge(edge.SourceVertex.Number.ToString(), edge.TargetVertex.Number.ToString());
                // Задаём ребру id для быстрого поиска
                msaglEdge.Attr.Id = $"{edge.SourceVertex.Number}-{edge.TargetVertex.Number}";
                // Если граф неориентированный, убираем с конца стрелку
                if (!graph.IsDirected)
                    msaglEdge.Attr.ArrowheadAtTarget = MsaglGraphs.ArrowStyle.None;
                // Приводим в соответствие значения их атрибутов
                UpdateMsaglEdge(msaglEdge, edge);
                // Задаём размер шрифта
                msaglEdge.Label.FontSize = 5;
                // Подписываемся на события изменения атрибутов ребра
                edge.LabelChanged += OnEdgeLabelChanged;
                edge.ColorChanged += OnEdgeColorChanged;
                edge.BoldChanged += OnEdgeBoldChanged;
            }
        }


        // ----Методы
        private void UpdateMsaglNodeId(MsaglGraphs.Node node, SgvlGraphs.Vertex vertex) {
            node.Id = vertex.Number.ToString();
        }

        private void UpdateMsaglNodeLabel(MsaglGraphs.Node node, SgvlGraphs.Vertex vertex) {
            // Если метка пустая - номер вершины, если непустая, то ставим метку рядом с номером (иначе никак)
            if (string.IsNullOrEmpty(vertex.Label))
                node.LabelText = node.Id.ToString();
            else
                node.LabelText = $"{node.Id}   {vertex.Label}";
        }

        private void UpdateMsaglNodeBorderColor(MsaglGraphs.Node node, SgvlGraphs.Vertex vertex) {
            node.Attr.Color = new MsaglGraphs.Color(vertex.BorderColor.A, vertex.BorderColor.R, vertex.BorderColor.G, vertex.BorderColor.B);
        }

        private void UpdateMsaglNodeFillColor(MsaglGraphs.Node node, SgvlGraphs.Vertex vertex) {
            node.Attr.FillColor = new MsaglGraphs.Color(vertex.FillColor.A, vertex.FillColor.R, vertex.FillColor.G, vertex.FillColor.B);
        }

        private void UpdateMsaglNode(MsaglGraphs.Node node, SgvlGraphs.Vertex vertex) {
            // Идентификатор вершины не меняется - обновлять не надо
            // Метка
            UpdateMsaglNodeLabel(node, vertex);
            // Цвет границ вершины
            UpdateMsaglNodeBorderColor(node, vertex);
            // Цвет заливки вершины
            UpdateMsaglNodeFillColor(node, vertex);
        }

        private void UpdateMsaglEdgeLabel(MsaglGraphs.Edge msaglEdge, SgvlGraphs.Edge sgvlEdge) {
            msaglEdge.LabelText = sgvlEdge.Label;
        }

        private void UpdateMsaglEdgeColor(MsaglGraphs.Edge msaglEdge, SgvlGraphs.Edge sgvlEdge) {
            msaglEdge.Attr.Color = new MsaglGraphs.Color(sgvlEdge.Color.A, sgvlEdge.Color.R, sgvlEdge.Color.G, sgvlEdge.Color.B);
        }

        private void UpdateMsaglEdgeBold(MsaglGraphs.Edge msaglEdge, SgvlGraphs.Edge sgvlEdge) {
            if (sgvlEdge.Bold)
                msaglEdge.Attr.LineWidth = 2;
            else
                msaglEdge.Attr.LineWidth = 1;
        }

        private void UpdateMsaglEdge(MsaglGraphs.Edge msaglEdge, SgvlGraphs.Edge sgvlEdge) {
            // Метка ребра
            UpdateMsaglEdgeLabel(msaglEdge, sgvlEdge);
            // Цвет ребра 
            UpdateMsaglEdgeColor(msaglEdge, sgvlEdge);
            // Толщина ребра
            UpdateMsaglEdgeBold(msaglEdge, sgvlEdge);
        }


        // ----Обработчики событий изменений в вершинах графа SGVL
        private void OnVertexLabelChanged(SgvlGraphs.Vertex vertex) {
            var node = MsaglGraph.FindNode(vertex.Number.ToString());
            UpdateMsaglNodeLabel(node, vertex);
        }

        private void OnVertexBorderColorChanged(SgvlGraphs.Vertex vertex) {
            var node = MsaglGraph.FindNode(vertex.Number.ToString());
            UpdateMsaglNodeBorderColor(node, vertex);
        }

        private void OnVertexFillColorChanged(SgvlGraphs.Vertex vertex) {
            var node = MsaglGraph.FindNode(vertex.Number.ToString());
            UpdateMsaglNodeFillColor(node, vertex);
        }

        // ----Обработчики событий изменений в рёбрах графа SGVL
        private void OnEdgeLabelChanged(SgvlGraphs.Edge edge) {
            var msaglEdge = MsaglGraph.EdgeById($"{edge.SourceVertex.Number}-{edge.TargetVertex.Number}");
            UpdateMsaglEdgeLabel(msaglEdge, edge);
        }

        private void OnEdgeColorChanged(SgvlGraphs.Edge edge) {
            var msaglEdge = MsaglGraph.EdgeById($"{edge.SourceVertex.Number}-{edge.TargetVertex.Number}");
            UpdateMsaglEdgeColor(msaglEdge, edge);
        }

        private void OnEdgeBoldChanged(SgvlGraphs.Edge edge) {
            var msaglEdge = MsaglGraph.EdgeById($"{edge.SourceVertex.Number}-{edge.TargetVertex.Number}");
            UpdateMsaglEdgeBold(msaglEdge, edge);
        }
    }
}
