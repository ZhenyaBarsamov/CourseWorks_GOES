using System.Drawing;
using SGVL.Graphs;

namespace SGVL.Visualization.AbstractTypes.EdgeDrawer {
    /// <summary>
    /// Интерфейс, представляющий объект, занимающийся рисованием ребра графа и проверкой 
    /// принадлежности координат данному ребру
    /// </summary>
    interface IEdgeDrawer {
        /// <summary>
        /// Нарисовать ориентированное ребро
        /// </summary>
        /// <param name="edge">Объект отрисовываемого ребра</param>
        void DrawEdge(Edge edge);
        /// <summary>
        /// Проверить, принадлежат ли координаты заданному ребру
        /// </summary>
        /// <param name="edge">Объект ребра, которое нужно проверить</param>
        /// <param name="coordinates">Координаты</param>
        /// <returns></returns>
        bool IsCoordinatesInEdge(Edge edge, PointF coordinates);
    }
}
