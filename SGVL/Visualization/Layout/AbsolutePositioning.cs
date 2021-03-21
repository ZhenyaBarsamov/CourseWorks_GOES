using SGVL.Graphs;
using System.Collections.Generic;
using System.Drawing;

namespace SGVL.Visualization.Layout {
    /// <summary>
    /// Класс, предназначенный для задания укладки графа в соответствие со списокм переданному ему координат вершин
    /// </summary>
    public class AbsolutePositioning : ILayoutAlgorithm {
        private List<PointF> VerticesCoordinates { get; set; }
        public int VerticesCoordinatesCount => VerticesCoordinates.Count;

        /// <summary>
        /// Конструктор, задающий список координат вершин, которые будут присваиваться вершинам графа
        /// </summary>
        /// <param name="verticesCoordinates">Координаты вершин графа</param>
        public AbsolutePositioning(List<PointF> verticesCoordinates) {
            VerticesCoordinates = verticesCoordinates;
        }

        public void BuildGraphLayout(Graph graph) {

            for (int vertexInex = 0; vertexInex < graph.Vertices.Count; vertexInex++)
                graph.Vertices[vertexInex].DrawingCoordinates = VerticesCoordinates[vertexInex];
        }
    }
}
