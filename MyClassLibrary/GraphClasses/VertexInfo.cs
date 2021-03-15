using System.Drawing;

namespace MyClassLibrary.GraphClasses {
    /// <summary>
    /// Класс, содержащий информацию о вершине графа EduGraph.
    /// В частности - координаты отрисовки вершины
    /// </summary>
    public class VertexInfo {
        /// <summary>
        /// Номер вершины
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Внутренняя метка, отображающая номер вершины
        /// </summary>
        public string InsideLabel => ID.ToString();

        /// <summary>
        /// Внешняя метка, ставящаяся при решении задачи о максимальном потоке
        /// </summary>
        public string OutsideLabel { get; set; }

        /// <summary>
        /// Координаты отрисовки вершины
        /// </summary>
        public PointF DrawingCoords { get; private set; }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="iD">Номер вершины</param>
        /// <param name="drawingCoordinates">Координаты отрисовки вершины</param>
        public VertexInfo(int id, PointF drawingCoordinates) {
            ID = id;
            DrawingCoords = drawingCoordinates;
        }
    }
}
