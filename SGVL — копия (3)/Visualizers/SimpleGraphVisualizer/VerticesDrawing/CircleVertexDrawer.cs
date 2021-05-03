using System;
using System.Drawing;
using System.Windows.Forms;
using SGVL.Graphs;

namespace SGVL.Visualizers.SimpleGraphVisualizer.VerticesDrawing {
    /// <summary>
    /// Класс, занимающийся рисованием вершин графа в виде кружочков
    /// </summary>
    class CircleVertexDrawer : IVertexDrawer {
        // ----Атрибуты
        /// <summary>
        /// Используемые настройки рисования графа
        /// </summary>
        public DrawingSettings Settings { get; private set; }


        // ----Константы
        const float defaultLineWidth = 1;
        const float boldCoefficient = 2;


        // ----Конструктор
        /// <summary>
        /// Инициализирует объект с заданными настройками рисования графа
        /// </summary>
        /// <param name="drawingSettings">Настройки рисования графа</param>
        public CircleVertexDrawer(DrawingSettings drawingSettings) {
            Settings = drawingSettings;
        }


        // ----Методы
        /// <summary>
        /// Нарисовать вершину в тех цветах, которые определяются текущим состоянием вершины
        /// (подсвеченная или неподсвеченная) 
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        /// <param name="vertex">Вершина, которую необходимо нарисовать</param>
        /// <param name="isVertexSelected">Флаг, показывающий, должна ли вершина быть подсвечена</param>
        private void DrawVertex(Graphics g, Vertex vertex, bool isVertexSelected) {
            // Параметры рисования вершины
            float width = vertex.Bold ? defaultLineWidth * boldCoefficient : defaultLineWidth;
            // В зависимости от того, выделена ли вершина, меняются некоторые цвета
            Color borderColor = isVertexSelected ? Settings.VertexSelectingColor : vertex.BorderColor;
            Color numberColor = isVertexSelected ? Settings.VertexNumberSelectingColor : Settings.VertexNumberColor;
            Color labelColor = isVertexSelected ? Settings.VertexLabelSelectingColor : Settings.VertexLabelColor;
            // Находим диаметр вершины (чтоб меньше считать)
            float radius = Settings.VertexRadius;
            float diameter = radius * 2;
            // Находим левую верхнюю точку прямоугольника, описанного вокруг эллипса (чтоб меньше считать)
            PointF leftUpperPoint = new PointF(vertex.DrawingCoords.X - radius, vertex.DrawingCoords.Y - radius);
            // Создаём нужный шрифт для меток
            Font font = new Font(Settings.FontName, Settings.FontSize);

            // Вычисляем расположение номера вершины и её метки (они зависят от пропорций вершины)
            PointF numberPosition = new PointF(leftUpperPoint.X + radius / 3, leftUpperPoint.Y + radius / 3); // делить на 4 тоже хорошо
            PointF labelPosition = new PointF(leftUpperPoint.X, vertex.DrawingCoords.Y);

            // Рисуем вершину
            g.FillEllipse(new SolidBrush(vertex.FillColor), leftUpperPoint.X, leftUpperPoint.Y, diameter, diameter); // заливаем место под вершиной
            g.DrawEllipse(new Pen(borderColor, width), leftUpperPoint.X, leftUpperPoint.Y, diameter, diameter); // рисуем границы вершины
            // Выводим номер вершины и её метку
            TextRenderer.DrawText(g, vertex.Number.ToString(), font, 
                new Point((int)numberPosition.X, (int)numberPosition.Y), numberColor, vertex.FillColor);
            if (Settings.IsVertexLabelVisible)
                TextRenderer.DrawText(g, vertex.Label, font, 
                    new Point((int)labelPosition.X, (int)labelPosition.Y), labelColor, vertex.FillColor);
        }

        public void DrawSelectedVertex(Graphics g, Vertex vertex) {
            DrawVertex(g, vertex, true);
        }

        public void DrawVertex(Graphics g, Vertex vertex) {
            DrawVertex(g, vertex, false);
        }

        public bool IsCoordinatesOnVertex(Vertex vertex, PointF coords) {
            // Согласно уравнению окружности
            return Math.Pow(coords.X - vertex.DrawingCoords.X, 2) + Math.Pow(coords.Y - vertex.DrawingCoords.Y, 2) <= Math.Pow(Settings.VertexRadius, 2);
        }
    }
}
