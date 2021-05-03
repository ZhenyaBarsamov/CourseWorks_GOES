using SGVL.Graphs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SGVL.Visualizers.SimpleGraphVisualizer.EdgesDrawing {
    /// <summary>
    /// Класс, занимающийся рисованием неориентированных рёбер графа
    /// </summary>
    class UndirectedLineEdgeDrawer : IEdgeDrawer {
        // ----Атрибуты
        /// <summary>
        /// Используемые настройки рисования графа
        /// </summary>
        public DrawingSettings Settings { get; private set; }

        // ----Константы
        /// <summary>
        /// Толщина линии по умолчанию
        /// </summary>
        protected const float defaultLineWidth = 2;
        /// <summary>
        /// Коэффициент, на который умножается толщина линии при выделении жирным
        /// </summary>
        protected const float boldCoefficient = 2;


        // ----Конструктор
        /// <summary>
        /// Инициализирует объект с заданными настройками рисования графа
        /// </summary>
        /// <param name="drawingSettings">Настройки рисования графа</param>
        public UndirectedLineEdgeDrawer(DrawingSettings drawingSettings) {
            Settings = drawingSettings;
        }


        // ----Методы
        /// <summary>
        /// Вычислить векторы для заданного ребра
        /// </summary>
        /// <param name="edge">Ребро, для которого необходимо вычислить векторы</param>
        /// <param name="lineVector">Направляющий вектор ребра</param>
        /// <param name="lineVectorMod">Модуль направляющего вектора ребра</param>
        /// <param name="normedLineVector">Нормированный направляющий вектор ребра</param>
        /// <param name="normedLineVectorNormal">Нормаль к нормированному направляющему вектору ребра</param>
        protected void CalcEdgeVectors(Edge edge, out PointF lineVector, out float lineVectorMod, out PointF normedLineVector, out PointF normedLineVectorNormal) {
            Vertex sVertex = edge.SourceVertex;
            Vertex tVertex = edge.TargetVertex;
            // Вычисляем направляющий вектор прямой-ребра: прямую между вершинами превращаем в вектор, находим его координаты
            lineVector = new PointF {
                X = tVertex.DrawingCoords.X - sVertex.DrawingCoords.X,
                Y = tVertex.DrawingCoords.Y - sVertex.DrawingCoords.Y
            };

            // Находим модуль этого направляющего вектора
            lineVectorMod = (float)Math.Sqrt(lineVector.X * lineVector.X + lineVector.Y * lineVector.Y);
            // Нормируем этот направляющий вектор
            normedLineVector = new PointF {
                X = lineVector.X / lineVectorMod,
                Y = lineVector.Y / lineVectorMod
            };

            // Находим нормальный вектор к нашему направляющему вектору (он нормирован, т.к. нормирован сам вектор)
            normedLineVectorNormal = new PointF(normedLineVector.Y, -normedLineVector.X);
        }

        /// <summary>
        /// Вычислить координаты начала и конца ребра (ребро соединяет не середины вершин, а их края)
        /// </summary>
        /// <param name="edge">Ребро, для которого необходимо вычислить координаты концов</param>
        /// <param name="normedLineVector">Нормированный направляющий вектор ребра</param>
        /// <param name="sPoint">Точка начала ребра</param>
        /// <param name="tPoint">Точка конца ребра</param>
        protected void CalcEdgeEnds(Edge edge, PointF normedLineVector, out PointF sPoint, out PointF tPoint) {
            float vertexRadius = Settings.VertexRadius;
            Vertex sVertex = edge.SourceVertex;
            Vertex tVertex = edge.TargetVertex;
            // Вычисляем координаты начала и конца рисования - ребро соединяет не центры, а границы вершин
            // Движемся от центра вершины-начала по направляющему вектору на расстояние радиуса вершины
            sPoint = new PointF {
                X = sVertex.DrawingCoords.X + normedLineVector.X * vertexRadius,
                Y = sVertex.DrawingCoords.Y + normedLineVector.Y * vertexRadius
            };
            // Движемся от центра вершины-конца против направляющего вектора на расстояние радиуса вершины
            tPoint = new PointF {
                X = tVertex.DrawingCoords.X - normedLineVector.X * vertexRadius,
                Y = tVertex.DrawingCoords.Y - normedLineVector.Y * vertexRadius
            };
        }

        /// <summary>
        /// Нарисовать ребро в тех цветах, которые определяются текущим состоянием ребра
        /// (подсвеченное или неподсвеченное) 
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        /// <param name="edge">Ребро, которое необходимо нарисовать</param>
        /// <param name="isEdgeSelected">Флаг, показывающий, должно ли ребро быть подсвечено</param>
        private void DrawEdge(Graphics g, Edge edge, bool isEdgeSelected) {
            // Параметры рисования ребра
            float width = edge.Bold ? defaultLineWidth * boldCoefficient : defaultLineWidth;

            // В зависимости от того, выделено ли ребро, меняются некоторые цвета
            Color color = isEdgeSelected ? Settings.EdgeSelectingColor : edge.Color;
            Color labelColor = isEdgeSelected ? Settings.EdgeLabelSelectingColor : Settings.EdgeLabelColor;

            // Получаем начальную и конечную точки рисования ребра, направляющий вектор ребра (исходный и нормированный),
            // модуль направляющего вектора ребра и нормаль к направляющему вектору (также нормированную)
            CalcEdgeVectors(edge, out _, out float lineVectorMod, out PointF normedLineVector, out PointF normedLineVectorNormal);
            CalcEdgeEnds(edge, normedLineVector, out PointF sPoint, out PointF tPoint);

            // Рисуем линию ребра (от sP до tP)
            g.DrawLine(new Pen(new SolidBrush(color), width), sPoint.X, sPoint.Y, tPoint.X, tPoint.Y);

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

        public void DrawEdge(Graphics g, Edge edge) {
            DrawEdge(g, edge, false);
        }

        public void DrawSelectedEdge(Graphics g, Edge edge) {
            DrawEdge(g, edge, true);
        }

        public bool IsCoordinatesOnEdge(Edge edge, PointF coords) {
            // Погрешности сравнения (вычислены эмпирически)
            const float floatCompareError = 4f; // погрешность сравнения вещественных чисел
            const float specialCaseError = 2f; // погрешность для частных случаев (горизонтальная/вертикалная прямые)
            const float generalCaseError = 0.1f; // погрешность для общего случая (произвольная прямая)
            // Вычисляем точки начала и конца рисования
            CalcEdgeVectors(edge, out _, out _, out PointF normedLineVector, out _);
            CalcEdgeEnds(edge, normedLineVector, out PointF sPoint, out PointF tPoint);
            // Из координат X начальной и конечной точек выбираем max и min (для удобства дальнейших сравнений)
            float maxX = Math.Max(sPoint.X, tPoint.X);
            float minX = Math.Min(sPoint.X, tPoint.X);
            // Из координат Y начальной и конечной точек выбираем max и min (для удобства дальнейших сравнений)
            float maxY = Math.Max(sPoint.Y, tPoint.Y);
            float minY = Math.Min(sPoint.Y, tPoint.Y);

            // Проверяем точку на принадлежность отрезку (с учётом погрешностей)
            bool isBelogToEdge = false; // принадлежит ли точка отрезку
            // Смотрим, подходит ли проверяемый отрезок по координатам X или Y
            if (minX <= coords.X && coords.X <= maxX || minY <= coords.Y && coords.Y <= maxY)
                // Проверям принадлежность прямой, если она вертикальная
                if (Math.Abs(sPoint.X - tPoint.X) < floatCompareError && minY <= coords.Y && coords.Y <= maxY && Math.Abs(sPoint.X - coords.X) < specialCaseError)
                    isBelogToEdge = true;
                // Проверям принадлежность прямой, если она горизонтальная
                else if (Math.Abs(sPoint.Y - tPoint.Y) < floatCompareError && minX <= coords.X && coords.X <= maxX && Math.Abs(sPoint.Y - coords.Y) < specialCaseError)
                    isBelogToEdge = true;
                // Для произвольной прямой (не вертикальной и не горизонтальной) проверяем с помощью уравнения прямой
                else if (Math.Abs((coords.X - sPoint.X) / (tPoint.X - sPoint.X) - (coords.Y - sPoint.Y) / (tPoint.Y - sPoint.Y)) < generalCaseError)
                    isBelogToEdge = true;
            return isBelogToEdge;
        }
    }
}
