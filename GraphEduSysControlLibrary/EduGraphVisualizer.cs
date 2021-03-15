using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MyClassLibrary.GraphClasses;

namespace GraphEduSysControlLibrary {
    public partial class EduGraphVisualizer : PictureBox {
        //-------------------ПОЛЯ ЭЛЕМЕНТА УПРАВЛЕНИЯ-------------------
        /// <summary>
        /// Ссылка на информацию об отображаемом графе
        /// </summary>
        public EduGraph Graph { get; private set; }

        /// <summary>
        /// Переключение режимов отображения графа (интерактивный/неинтерактивный)
        /// </summary>
        public bool InteractiveMode { get; set; }

        /// <summary>
        /// Флаг - необходимо ли отображать метки дуг графа (ТЕСТ)
        /// </summary>
        private bool isEdgesMarksVisible;

        /// <summary>
        /// Флаг - необходимо ли отображать направление дуг (ТЕСТ)
        /// </summary>
        private bool isArrowsVisible;

        /// <summary>
        /// Подсвеченная вершина
        /// </summary>
        private VertexInfo selectedVertex;

        /// <summary>
        /// Подсвеченная дуга
        /// </summary>
        private EdgeInfo selectedEdge;

        /// <summary>
        /// Переопределённые цвета рисования вершин 
        /// (цвета подсвечивания выбранных вершин)
        /// </summary>
        private Dictionary<VertexInfo, Color> verticesMarkingColors;

        /// <summary>
        /// Переопределённые цвета рисования дуг 
        /// (цвета подсвечивания выбранных дуг)
        /// </summary>
        private Dictionary<EdgeInfo, Color> edgesMarkingColors;
        //---------------------------------------------------------------


        //--------------------ИНИЦИАЛИЗАЦИЯ ЭЛЕМЕНТА УПРАВЛЕНИЯ----------------------
        /// <summary>
        /// Конструктор
        /// </summary>
        public EduGraphVisualizer(){
            InitializeComponent();
        }

        /// <summary>
        /// Метод инициализации элемента управления EduGraphVisualizer.
        /// Элемент управления должен быть инициализирован перед своим использованием.
        /// Также метод позволяет сменить визуализируемый граф
        /// </summary>
        /// <param name="visualizingGraph">Граф, который необходимо визуализировать</param>
        /// <param name="interactiveMode">Флаг, включающий интерактивный режим элемента управления</param>
        public void Initialize(EduGraph visualizingGraph, bool interactiveMode = false, bool isEdgesMarksVisible = true, bool isArrowsVisible = true) {
            // Инициализируем поля контрола
            selectedVertex = null;
            selectedEdge = null;
            verticesMarkingColors = new Dictionary<VertexInfo, Color>();
            edgesMarkingColors = new Dictionary<EdgeInfo, Color>();
            InteractiveMode = interactiveMode;
            this.isEdgesMarksVisible = isEdgesMarksVisible;
            this.isArrowsVisible = isArrowsVisible;
            // Сохраняем информацию о графе
            Graph = visualizingGraph;
            // Создаём под изображение битмап, чтобы оно не стиралось после перерисовки
            Image = new Bitmap(Width, Height);
            // Рисуем граф и запрашиваем перерисовку контрола
            DrawGraph();
        }
        //---------------------------------------------------------------------------


        //-------------КОНСТАНТНЫЕ ПАРАМЕТРЫ РИСОВАНИЯ ГРАФА------------------
        /// <summary>
        /// Цвет фона рисования
        /// </summary>
        private Color backgroundColor = SystemColors.Control;
        /// <summary>
        /// Цвет границы вершины
        /// </summary>
        private Color vertexColor = Color.Black;
        /// <summary>
        /// Цвет подсвечивания вершины при выборе
        /// </summary>
        private Color vertexSelectingColor = Color.Red;
        /// <summary>
        /// Цвет заливки вершины
        /// </summary>
        private Color vertexFillingColor = Color.White;
        /// <summary>
        /// Радиус вершины
        /// </summary>
        private const float vertexRadius = 27; // 20 - неплохо
        /// <summary>
        /// Цвет дуги
        /// </summary>
        private Color edgeColor = Color.Black;
        /// <summary>
        /// Цвет подсвечивания дуги при выборе
        /// </summary>
        private Color edgeSelectingColor = Color.Blue;
        /// <summary>
        /// Название шрифта меток
        /// </summary>
        private string fontName = "Calibri";
        /// <summary>
        /// Размер шрифта меток
        /// </summary>
        private const float fontSize = 10f;
        /// <summary>
        /// Цвет меток дуги
        /// </summary>
        private Color edgeLabelColor = Color.Green;
        /// <summary>
        /// Цвет внутренней метки вершины (с номером вершины)
        /// </summary>
        private Color insideVertexLabelColor = Color.Green;
        /// <summary>
        /// Цвет внешней метки вершины (с меткой поиска потока)
        /// </summary>
        private Color outsideVertexLabelColor = Color.Blue;
        //---------------------------------------------------------------------


        //----------------ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ--------------
        /// <summary>
        /// Метод вычисления параметров дуги
        /// </summary>
        /// <param name="edge">Дуга</param>
        /// <param name="sPoint">Начальная точка рисования дуги</param>
        /// <param name="fPoint">Конечная точка рисования дуги</param>
        /// <param name="lineVector">Направляющий вектор дуги в направлении конечной вершине</param>
        /// <param name="lineVectorMod">Норма направляющего вектора дуги, направленного к конечной вершине</param>
        /// <param name="normedLineVector">Нормированный направляющий вектор дуги, направленный к конечной вершине</param>
        /// <param name="normedLineVectorNormal">Нормированный вектор нормали к направляющему вектору дуги</param>
        private void GetEdgeParams(EdgeInfo edge, out PointF sPoint, out PointF fPoint, 
           out PointF lineVector, out float lineVectorMod, out PointF normedLineVector, out PointF normedLineVectorNormal) {
            VertexInfo sVertex = Graph[edge.StartVertexID]; 
            VertexInfo fVertex = Graph[edge.FinishVertexID];
            // Вычисляем направляющий вектор прямой-дуги: прямую между вершинами превращаем в вектор, находим его координаты
            lineVector = new PointF(fVertex.DrawingCoords.X - sVertex.DrawingCoords.X,
                fVertex.DrawingCoords.Y - sVertex.DrawingCoords.Y);
            // Находим модуль этого направляющего вектора
            lineVectorMod = (float)Math.Sqrt(lineVector.X * lineVector.X + lineVector.Y * lineVector.Y);
            // Нормируем этот направляющий вектор
            normedLineVector = new PointF(lineVector.X / lineVectorMod, lineVector.Y / lineVectorMod);

            // Вычисляем координаты начала и конца рисования - стрелка соединяет не центры, а границы вершин
            // Движемся от центра вершины-начала по направляющему вектору на расстояние радиуса вершины
            sPoint = new PointF(sVertex.DrawingCoords.X + normedLineVector.X * vertexRadius, sVertex.DrawingCoords.Y + normedLineVector.Y * vertexRadius);
            // Движемся от центра вершины-конца против направляющего вектора на расстояние радиуса вершины
            fPoint = new PointF(fVertex.DrawingCoords.X - normedLineVector.X * vertexRadius, fVertex.DrawingCoords.Y - normedLineVector.Y * vertexRadius);

            // Находим нормальный вектор к нашему направляющему вектору (он нормирован, т.к. нормирован сам вектор)
            normedLineVectorNormal = new PointF(normedLineVector.Y, -normedLineVector.X);
        }

        /// <summary>
        /// Метод проверки нахождения указателя мыши над вершиной
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="mouseX">Координата X мыши</param>
        /// <param name="mouseY">Координата Y мыши</param>
        /// <returns>true - да, false - нет</returns>
        private bool IsMouseOnVertex(VertexInfo vertex, float mouseX, float mouseY) {
            return Math.Pow(mouseX - vertex.DrawingCoords.X, 2) + Math.Pow(mouseY - vertex.DrawingCoords.Y, 2) <= Math.Pow(vertexRadius, 2);
        }

        /// <summary>
        /// Метод проверки нахождения указателя мыши над дугой
        /// </summary>
        /// <param name="edge">Дуга</param>
        /// <param name="mouseX">Координата X мыши</param>
        /// <param name="mouseY">Координата Y мыши</param>
        /// <returns>true - да, false - нет</returns>
        private bool IsMouseOnEdge(EdgeInfo edge, float mouseX, float mouseY) {
            // Получаем параметры дуги: точки начала и конца рисования
            GetEdgeParams(edge, out PointF sPoint, out PointF fPoint, out _, out _, out _, out _);
            // Из координат X начальной и конечной точек выбираем max и min (для удобства дальнейших сравнений)
            float maxX = Math.Max(sPoint.X, fPoint.X);
            float minX = Math.Min(sPoint.X, fPoint.X);
            // Из координат Y начальной и конечной точек выбираем max и min (для удобства дальнейших сравнений)
            float maxY = Math.Max(sPoint.Y, fPoint.Y);
            float minY = Math.Min(sPoint.Y, fPoint.Y);

            // Проверяем точку на принадлежность отрезку (с учётом погрешностей)
            bool isBelogToEdge = false; // принадлежит ли точка отрезку
            // Смотрим, подходит ли проверяемый отрезок по координатам X или Y
            if (minX <= mouseX && mouseX <= maxX || minY <= mouseY && mouseY <= maxY)
                // Проверям принадлежность прямой, если она вертикальная
                if (sPoint.X == fPoint.X && minY <= mouseY && mouseY <= maxY && Math.Abs(sPoint.X - mouseX) <= 2)
                    isBelogToEdge = true;
                // Проверям принадлежность прямой, если она горизонтальная
                else if (sPoint.Y == fPoint.Y && minX <= mouseX && mouseX <= maxX && Math.Abs(sPoint.Y - mouseY) <= 2)
                    isBelogToEdge = true;
                // Проверяем с помощью уравнения прямой
                else if (Math.Abs((mouseX - sPoint.X) / (fPoint.X - sPoint.X) - (mouseY - sPoint.Y) / (fPoint.Y - sPoint.Y)) <= 0.1)
                    isBelogToEdge = true;
            return isBelogToEdge;
        }
        //----------------------------------------------------


        //----------------ВИЗУАЛИЗАЦИЯ-----------------
        /// <summary>
        /// Получить поверхность для рисования
        /// </summary>
        /// <returns></returns>
        private Graphics GetGraphics() {
            // Получаем поверхность рисования
            var g = Graphics.FromImage(Image);
            // Задаём сглаживание при рисовании
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            return g;
        }

        /// <summary>
        /// Метод рисования направленной дуги между вершинами с заданным цветом
        /// </summary>
        /// <param name="g">Контекст рисования</param>
        /// <param name="edge">Дуга</param>
        /// <param name="color">Цвет</param>
        private void DrawEdge(Graphics g, EdgeInfo edge, Color color) {
            // Параметры рисования стрелки
            const float arrowL = 10; // длина усиков вдоль стрелки
            const float arrowH = 4; // расстояние от линии стрелки до крайней точки усиков (высота усиков)
            const float arrowWidth = 2; // толщина стрелки

            // Получаем параметры дуги: начальную и конечную точку рисования, направляющий вектор дуги (исходный и нормированный),
            // модуль направляющего вектора дуги, и нормаль к направляющему вектору (так же нормированную)
            GetEdgeParams(edge, out PointF sPoint, out PointF fPoint,
                out PointF lineVector, out float lineVectorMod, out PointF normedLineVector, out PointF normedLineVectorNormal);

            // Находим точку O, от которой к крайним точкам усиков будем проводить перпендикуляры: fPoint - NormedLineVector * arrowL
            PointF oPoint = new PointF(fPoint.X - normedLineVector.X * arrowL, fPoint.Y - normedLineVector.Y * arrowL);
            // Вычисляем крайние точки усиков: от точки O в направлении нормали и против неё (в обе стороны, плюс и минус) проходим расстояние arrowH
            PointF p1 = new PointF(oPoint.X + normedLineVectorNormal.X * arrowH, oPoint.Y + normedLineVectorNormal.Y * arrowH);
            PointF p2 = new PointF(oPoint.X - normedLineVectorNormal.X * arrowH, oPoint.Y - normedLineVectorNormal.Y * arrowH);

            // Рисуем линию стрелки (от sP до fP)
            g.DrawLine(new Pen(new SolidBrush(color), arrowWidth), sPoint.X, sPoint.Y, fPoint.X, fPoint.Y);
            if (isArrowsVisible) {
                // Рисуем усики стрелки (от fP к p1 и от fP к p2)
                g.DrawLine(new Pen(new SolidBrush(color), arrowWidth), fPoint.X, fPoint.Y, p1.X, p1.Y);
                g.DrawLine(new Pen(new SolidBrush(color), arrowWidth), fPoint.X, fPoint.Y, p2.X, p2.Y);
            }

            // Создаём нужный шрифт для меток
            Font font = new Font(fontName, fontSize);

            // Вычисляем координаты метки
            var labelCoords = new PointF(sPoint.X + normedLineVector.X * lineVectorMod / 4, sPoint.Y + normedLineVector.Y * lineVectorMod / 4);
            labelCoords.X += normedLineVectorNormal.X * 4; // и немного проходим в направлении нормали
            labelCoords.Y += normedLineVectorNormal.Y * 4;

            // Выводим метку
            if (isEdgesMarksVisible)
                TextRenderer.DrawText(g, $"{Graph[edge.StartVertexID, edge.FinishVertexID].Label}", font,
                    new Point((int)labelCoords.X, (int)labelCoords.Y), Color.Green, SystemColors.Control);
        }

        /// <summary>
        /// Метод рисования направленной дуги между вершинами с настройками по умолчанию
        /// </summary>
        /// <param name="g">Контекст рисования</param>
        /// <param name="edge">Дуга</param>
        private void DrawEdge(Graphics g, EdgeInfo edge) {
            // Узнаём цвет дуги и рисуем
            var eColor =  edgesMarkingColors.ContainsKey(edge) ? edgesMarkingColors[edge] : edgeColor;
            DrawEdge(g, edge, eColor);
        }

        /// <summary>
        /// Метод рисования вершины с заданным цветом
        /// </summary>
        /// <param name="g">Контекст рисования</param>
        /// <param name="vertex">Вершина</param>
        /// <param name="color">Цвет</param>
        private void DrawVertex(Graphics g, VertexInfo vertex, Color color) {
            const float diameter = vertexRadius * 2; // диаметр вершины (чтоб меньше считать)
            // левая верхняя точка прямоугольника, описанного вокруг эллипса (чтоб меньше считать)
            PointF leftUpperPoint = new PointF(vertex.DrawingCoords.X - vertexRadius, vertex.DrawingCoords.Y - vertexRadius);
            // Создаём нужный шрифт для меток
            Font font = new Font(fontName, fontSize);

            // Вычисляем расположение внутренней и внешней меток (они зависят от пропорций вершины)
            PointF insideLabelPosition = new PointF(leftUpperPoint.X + vertexRadius / 3, leftUpperPoint.Y + vertexRadius / 3); // 4
            PointF outsideLabelPosition = new PointF(leftUpperPoint.X, vertex.DrawingCoords.Y);

            // Рисуем вершину
            g.FillEllipse(new SolidBrush(vertexFillingColor), leftUpperPoint.X, leftUpperPoint.Y, diameter, diameter); // заливаем место под вершиной
            g.DrawEllipse(new Pen(color, 2), leftUpperPoint.X, leftUpperPoint.Y, diameter, diameter); // рисуем границы вершины
            // Выводим метки
            TextRenderer.DrawText(g, vertex.InsideLabel, font, new Point((int)insideLabelPosition.X, (int)insideLabelPosition.Y), insideVertexLabelColor, vertexFillingColor);
            TextRenderer.DrawText(g, vertex.OutsideLabel, font, new Point((int)outsideLabelPosition.X, (int)outsideLabelPosition.Y), outsideVertexLabelColor, vertexFillingColor);
        }

        /// <summary>
        /// Метод рисования вершины с настройками по умолчанию
        /// </summary>
        /// <param name="g">Контекст рисования</param>
        /// <param name="vertex">Вершина</param>
        private void DrawVertex(Graphics g, VertexInfo vertex) {
            // Узнаём цвет вершины и рисуем
            var vertColor = verticesMarkingColors.ContainsKey(vertex) ? verticesMarkingColors[vertex] : vertexColor;
            DrawVertex(g, vertex, vertColor);
        }

        /// <summary>
        /// Метод отрисовки графа
        /// </summary>
        public void DrawGraph() {
            var g = GetGraphics();
            // Очищаем поверхность и заливаем заданным фоновым цветом
            g.Clear(backgroundColor);
            // Рисуем дуги графа
            int verticesCount = Graph.VerticesCount;
            for (int startVertID = 1; startVertID <= verticesCount; startVertID++)
                for (int finishVertID = 1; finishVertID <= verticesCount; finishVertID++)
                    if (Graph[startVertID, finishVertID] != null)
                        DrawEdge(g, Graph[startVertID, finishVertID]);
            // Рисуем вершины
            foreach (var v in Graph.VerticesInfo.Values)
                DrawVertex(g, v);
            // Вызываем перерисовку элемента управления
            Invalidate();
        }
        //---------------------------------------------


        //------------------ВЫДЕЛЕНИЕ ВЕРШИН---------------------
        /// <summary>
        /// Метод очистки цветного выделения всех вершин
        /// </summary>
        public void ClearVerticesMarking() {
            verticesMarkingColors.Clear();
            // Перерисовываем все вершины
            var g = GetGraphics();
            foreach (var v in Graph.VerticesInfo.Values)
                DrawVertex(g, v);
            Invalidate();
        }

        /// <summary>
        /// Метод очистки цветного выделения всех дуг
        /// </summary>
        public void ClearEdgesMarking() {
            edgesMarkingColors.Clear();
            // Перерисовываем все дуги
            var g = GetGraphics();
            int verticesCount = Graph.VerticesCount;
            for (int startVertID = 1; startVertID <= verticesCount; startVertID++)
                for (int finishVertID = 1; finishVertID <= verticesCount; finishVertID++)
                    if (Graph[startVertID, finishVertID] != null)
                        DrawEdge(g, Graph[startVertID, finishVertID]);
            Invalidate();
        }

        /// <summary>
        /// Метод выделения дуги цветом
        /// </summary>
        /// <param name="edge">Дуга</param>
        /// <param name="color">Цвет</param>
        public void MarkEdge(EdgeInfo edge, Color color) {
            edgesMarkingColors[edge] = color;
            DrawEdge(GetGraphics(), edge);
            Invalidate();
        }

        /// <summary>
        /// Метод снятия выделения с дуги
        /// </summary>
        /// <param name="edge">Дуга</param>
        public void ClearEdgeMarking(EdgeInfo edge) {
            edgesMarkingColors[edge] = edgeColor;
            DrawEdge(GetGraphics(), edge);
            Invalidate();
        }

        /// <summary>
        /// Метод выделения вершины цветом
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="color">Цвет</param>
        public void MarkVertex(VertexInfo vertex, Color color) {
            verticesMarkingColors[vertex] = color;
            DrawVertex(GetGraphics(), vertex);
            Invalidate();
        }

        /// <summary>
        /// Метод снятия выделения с вершины
        /// </summary>
        /// <param name="vertex">Вершина</param>
        public void ClearVertexMarking(VertexInfo vertex) {
            verticesMarkingColors[vertex] = vertexColor;
            DrawVertex(GetGraphics(), vertex);
            Invalidate();
        }
        //---------------------------------------------------


        //----------------------СОБЫТИЯ---------------------------
        public delegate void VertexSelectedEventHandler(VertexInfo selectedVertex);
        /// <summary>
        /// Событие выбора вершины графа
        /// </summary>
        public event VertexSelectedEventHandler VertexSelectedEvent;

        public delegate void EdgeSelectedEventHandler(EdgeInfo selectedEdge);
        /// <summary>
        /// Событие выбора дуги графа
        /// </summary>
        public event EdgeSelectedEventHandler EdgeSelectedEvent;
        //--------------------------------------------------------


        //-------------------------ОБРАБОТЧИКИ СОБЫТИЙ------------------------------
        // Обработчик на событие движения мыши на контроле
        private void EduGraphVisualizer_MouseMove(object sender, MouseEventArgs e) {
            // Если интерактивный режим выключен - подсветка отключена
            if (!InteractiveMode)
                return;
            // Если была выделенная вершина, проверяем, покинула ли мышь её. Если нет - конец. Если да - убираем подсветку
            if (selectedVertex != null)
                if (!IsMouseOnVertex(selectedVertex, e.X, e.Y)) {
                    DrawVertex(GetGraphics(), selectedVertex);
                    Invalidate();
                    selectedVertex = null;
                }
                else
                    return;
            // Если была выделенная дуга, проверяем, покинула ли мышь её. Если нет - конец. Если да - убираем подсветку
            if (selectedEdge != null) {
                if (!IsMouseOnEdge(selectedEdge, e.X, e.Y)) {
                    DrawEdge(GetGraphics(), selectedEdge);
                    Invalidate();
                    selectedEdge = null;
                }
                else
                    return;
            }

            // Смотрим, оказалась ли мышь над одной из вершин
            foreach (var vertex in Graph.VerticesInfo.Values) {
                // Проверяем, используя уравнение окружности. Если да - выделяем и выходим (больше искать нечего)
                if (IsMouseOnVertex(vertex, e.X, e.Y)) {
                    selectedVertex = vertex;
                    DrawVertex(GetGraphics(), vertex, vertexSelectingColor);
                    Invalidate();
                    return;
                }
            }
            // Смотрим, оказалась ли мышь над одной из дуг
            int verticesCount = Graph.VerticesCount;
            for (int startVertID = 1; startVertID <= verticesCount; startVertID++) {
                for (int finishVertID = 1; finishVertID <= verticesCount; finishVertID++) {
                    // Если ребра нет - идём дальше
                    if (Graph[startVertID, finishVertID] == null)
                        continue;
                    // Ребро
                    var edge = Graph[startVertID, finishVertID];

                    // Если принадлежит - выделяем и выходим (искать больше нечего)
                    if (IsMouseOnEdge(edge, e.X, e.Y)) {
                        selectedEdge = edge;
                        DrawEdge(GetGraphics(), edge, edgeSelectingColor);
                        Invalidate();
                        return;
                    }
                }
            }
        }

        // Обработчик на событие нажатия кнопки мыши на контроле
        private void EduGraphVisualizer_MouseDown(object sender, MouseEventArgs e) {
            // Если интерактивный режим выключен - выбор элементов графа отключён
            if (!InteractiveMode)
                return;
            // Если были выделенная вершина/дуга - оповещаем
            if (selectedVertex != null)
                VertexSelectedEvent?.Invoke(selectedVertex);
            else if (selectedEdge != null)
                EdgeSelectedEvent?.Invoke(selectedEdge);
        }
        //---------------------------------------------------------------------------
    }
}
