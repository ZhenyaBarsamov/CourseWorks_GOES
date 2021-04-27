using System.Drawing;
using System.Windows.Forms;
using SGVL.Types.Graphs;

namespace SGVL.Visualizers.SimpleGraphVisualizer.EdgesDrawing {
    /// <summary>
    /// Класс, занимающийся рисованием ориентированных рёбер (дуг) графа
    /// </summary>
    class DirectedLineEdgeDrawer : UndirectedLineEdgeDrawer, IEdgeDrawer {
        // ----Атрибуты наследуются


        // ----Конструктор
        /// <summary>
        /// Инициализирует объект с заданными настройками рисования графа
        /// </summary>
        /// <param name="drawingSettings">Настройки рисования графа</param>
        public DirectedLineEdgeDrawer(DrawingSettings drawingSettings) : base(drawingSettings) {}


        // ----Методы
        /// <summary>
        /// Нарисовать ребро в тех цветах, которые определяются текущим состоянием ребра
        /// (подсвеченное или неподсвеченное) 
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        /// <param name="edge">Ребро, которое необходимо нарисовать</param>
        /// <param name="isEdgeSelected">Флаг, показывающий, должно ли ребро быть подсвечено</param>
        private void DrawEdge(Graphics g, Edge edge, bool isEdgeSelected) {
            // Параметры рисования ребра
            const float arrowL = 10; // длина усиков стрелки вдоль отрезка
            const float arrowH = 4; // расстояние от отрезка до крайних точек усиков стрелки (высота усиков)
            const float edgeWidth = 2; // толщина ребра
            float width = edge.Bold ? edgeWidth * 2 : edgeWidth;

            // В зависимости от того, выделено ли ребро, меняем некоторые цвета
            Color color = isEdgeSelected ? Settings.EdgeSelectingColor : edge.Color;
            Color labelColor = isEdgeSelected ? Settings.EdgeLabelSelectingColor : Settings.EdgeLabelColor;

            // Получаем начальную и конечную точки рисования ребра, направляющий вектор ребра (исходный и нормированный),
            // модуль направляющего вектора ребра и нормаль к направляющему вектору (также нормированную)
            CalcEdgeVectors(edge, out _, out float lineVectorMod, out PointF normedLineVector, out PointF normedLineVectorNormal);
            CalcEdgeEnds(edge, normedLineVector ,out PointF sPoint, out PointF tPoint);

            // Находим точку O, от которой к крайним точкам усиков будем проводить перпендикуляры: tPoint - NormedLineVector * arrowL
            PointF p0 = new PointF(tPoint.X - normedLineVector.X * arrowL, tPoint.Y - normedLineVector.Y * arrowL);
            // Вычисляем крайние точки усиков: от точки O в направлении нормали и против неё (в обе стороны, плюс и минус) проходим расстояние arrowH
            PointF p1 = new PointF(p0.X + normedLineVectorNormal.X * arrowH, p0.Y + normedLineVectorNormal.Y * arrowH);
            PointF p2 = new PointF(p0.X - normedLineVectorNormal.X * arrowH, p0.Y - normedLineVectorNormal.Y * arrowH);

            // Рисуем линию стрелки (от sP до tP)
            g.DrawLine(new Pen(new SolidBrush(color), width), sPoint.X, sPoint.Y, tPoint.X, tPoint.Y);
            // Рисуем усики стрелки (от tP к p1 и от tP к p2)
            g.DrawLine(new Pen(new SolidBrush(color), width), tPoint.X, tPoint.Y, p1.X, p1.Y);
            g.DrawLine(new Pen(new SolidBrush(color), width), tPoint.X, tPoint.Y, p2.X, p2.Y);

            // Выводим метку
            if (Settings.IsEdgeLabelVisible) {
                // Создаём нужный шрифт для метки
                Font font = new Font(Settings.FontName, Settings.FontSize);

                // Вычисляем координаты метки
                var labelCoords = new PointF(sPoint.X + normedLineVector.X * lineVectorMod / 4, sPoint.Y + normedLineVector.Y * lineVectorMod / 4);
                labelCoords.X += normedLineVectorNormal.X * 4; // и немного проходим в направлении нормали
                labelCoords.Y += normedLineVectorNormal.Y * 4;

                // Рисуем метку
                TextRenderer.DrawText(g, edge.Label, font,
                    new Point((int)labelCoords.X, (int)labelCoords.Y), labelColor, Settings.BackgroundColor);
            }
        }

        public new void DrawEdge(Graphics g, Edge edge) {
            DrawEdge(g, edge, false);
        }

        public new void DrawSelectedEdge(Graphics g, Edge edge) {
            DrawEdge(g, edge, true);
        }
    }
}
