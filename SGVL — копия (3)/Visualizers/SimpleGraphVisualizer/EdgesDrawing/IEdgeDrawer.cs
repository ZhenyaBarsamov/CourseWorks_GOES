using System.Drawing;
using SGVL.Graphs;

namespace SGVL.Visualizers.SimpleGraphVisualizer.EdgesDrawing {
    /// <summary>
    /// Интерфейс объекта, занимающегося рисованием рёбер графа 
    /// и проверкой принадлежности координат рёбрам
    /// </summary>
    public interface IEdgeDrawer {
        /// <summary>
        /// Нарисовать ребро графа
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        /// <param name="edge">Ребро, которое необходимо нарисовать</param>
        void DrawEdge(Graphics g, Edge edge);
        /// <summary>
        /// Нарисовать ребро, на которое была наведена мышь
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        /// <param name="edge">Ребро, которое необходимо нарисовать</param>
        void DrawSelectedEdge(Graphics g, Edge edge);
        /// <summary>
        /// Проверить координаты на принадлежность заданному ребру
        /// </summary>
        /// <param name="edge">Проверяемое ребро</param>
        /// <param name="coords">Проверяемые координаты</param>
        bool IsCoordinatesOnEdge(Edge edge, PointF coords);
    }
}
