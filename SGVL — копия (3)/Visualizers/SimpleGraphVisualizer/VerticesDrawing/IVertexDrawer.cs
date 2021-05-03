using System.Drawing;
using SGVL.Graphs;

namespace SGVL.Visualizers.SimpleGraphVisualizer.VerticesDrawing {
    /// <summary>
    /// Интерфейс объекта, занимающегося рисованием вершин графа 
    /// и проверкой принадлежности координат вершинам
    /// </summary>
    public interface IVertexDrawer {
        /// <summary>
        /// Нарисовать вершину графа
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        /// <param name="vertex">Вершина, которую необходимо нарисовать</param>
        void DrawVertex(Graphics g, Vertex vertex);
        /// <summary>
        /// Нарисовать вершину, на которую была наведена мышь
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        /// <param name="vertex">Вершина, которую необходимо нарисовать</param>
        void DrawSelectedVertex(Graphics g, Vertex vertex);
        /// <summary>
        /// Проверить координаты на принадлежность заданной вершины
        /// </summary>
        /// <param name="vertex">Проверяемая вершина</param>
        /// <param name="coords">Проверяемые координаты</param>
        bool IsCoordinatesOnVertex(Vertex vertex, PointF coords);
    }
}
