using System.Drawing;
using SGVL.Graphs;

namespace SGVL.Visualization.VertexDrawers {
    /// <summary>
    /// Интерфейс, представляющий объект, занимающийся рисованием вершины графа и проверкой 
    /// принадлежности координат данной вершины
    /// </summary>
    interface IVertexDrawer {
        /// <summary>
        /// Нарисовать вершину
        /// </summary>
        /// <param name="vertex">Объект отрисовываемой вершины</param>
        void DrawVertex(Vertex vertex);
        /// <summary>
        /// Проверить, принадлежат ли координаты заданной вершины
        /// </summary>
        /// <param name="vertex">Объект вершины, которую нужно проверить</param>
        /// <param name="coordinates">Координаты</param>
        /// <returns></returns>
        bool IsCoordinatesInVertex(Vertex vertex, PointF coordinates);
    }
}
