using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SGVL.Graphs;
using SGVL.Visualizers.SimpleGraphVisualizer.VerticesDrawing;
using SGVL.Visualizers.SimpleGraphVisualizer.EdgesDrawing;

namespace SGVL.Visualizers.SimpleGraphVisualizer {
    /// <summary>
    /// Элемент управления для визуализации графа на основе стандартного элемента управления PictureBox
    /// </summary>
    public partial class SimpleGraphVisualizer : PictureBox, IGraphVisualizer {
        // TODO: хорошо бы работать не с координатами мыши на PictureBox,
        // а с координатами мыши на изображении, с координатами пикселей.
        // Это даст возможность добавлять полосы прокрутки для больших графов, и т.д.
        // Это сработает(?):  Point p = pictureBox1.PointToClient(System.Windows.Forms.Cursor.Position);
        // TODO: хорошо бы рисовать не на битмапе, а на самом элементе управления. Это будет в тыщу раз быстрее.
        // Ещё для оптимизации можно использовать квадродерево.
        // TODO: хорошо бы перейти от абсолютных координат вершин к координатам относительным (от 0 до 1, к примеру).
        // Это позволит рисовать граф на холсте любых размеров с сохранением пропорций как-бы.


        // ----Атрибуты
        public Graph Graph { get; private set; }
        public InteractiveMode InteractiveMode { get; set; }
        public bool IsVerticesMoving { get; set; }
        public bool IsInteractiveUpdating { get; set; }
        public DrawingSettings Settings { get; private set; }
        private IVertexDrawer VertexDrawer { get; set; }
        private IEdgeDrawer EdgeDrawer { get; set; }
        /// <summary>
        /// Вершина, которая находится под мышью
        /// </summary>
        private Vertex SelectedVertex { get; set; }
        /// <summary>
        /// Ребро, которое находится под мышью
        /// </summary>
        private Edge SelectedEdge { get; set; }


        // ----Конструктор
        public SimpleGraphVisualizer() {
            InitializeComponent();
            // Включаем двойную буферизацию при перерисовке
            DoubleBuffered = true;
            // Создаём объект с настройками и подписываемся на их изменение, чтоб, если что, перерисовывать граф
            Settings = new DrawingSettings();
            Settings.SettingsChanged += OnSettingsChanged;
        }


        public void Initialize(Graph graph) {
            // Отписываемся от текущего графа, если нужно
            if (Graph != null)
                Graph.GraphChainged -= OnGraphChanged;
            Graph = graph;
            // Подписываемся на изменение графа, чтоб, если что, его перерисовывать
            graph.GraphChainged += OnGraphChanged;
            // Задаём настройки отображения графа
            InteractiveMode = InteractiveMode.Interactive;
            IsVerticesMoving = true;
            IsInteractiveUpdating = true;
            // Создаём под изображение битмап, чтобы оно не стиралось после перерисовки
            Image = new Bitmap(Width, Height);
            // Создаём объекты для рисования элементов графа
            VertexDrawer = new CircleVertexDrawer(Settings);
            if (Graph.IsDirected)
                EdgeDrawer = new DirectedLineEdgeDrawer(Settings);
            else
                EdgeDrawer = new UndirectedLineEdgeDrawer(Settings);
            // Обнуляем подсвеченные вершину и ребро
            SelectedVertex = null;
            SelectedEdge = null;
            // Рисуем граф и запрашиваем перерисовку контрола
            DrawGraph();
        }

        public void ResetVerticesBorderColor() {
            if (!IsInteractiveUpdating)
                Graph.SetVerticesBorderColor(Settings.VertexBorderColor);
            else {
                IsInteractiveUpdating = false;
                Graph.SetVerticesBorderColor(Settings.VertexBorderColor);
                IsInteractiveUpdating = true;
                OnGraphChanged(Graph);
            }
        }

        public void ResetVerticesFillColor() {
            if (!IsInteractiveUpdating)
                Graph.SetVerticesBorderColor(Settings.VertexBorderColor);
            else {
                IsInteractiveUpdating = false;
                Graph.SetVerticesFillColor(Settings.VertexFillColor);
                IsInteractiveUpdating = true;
                OnGraphChanged(Graph);
            }
        }

        public void ResetEdgesColor() {
            if (!IsInteractiveUpdating)
                Graph.SetVerticesBorderColor(Settings.VertexBorderColor);
            else {
                IsInteractiveUpdating = false;
                Graph.SetEdgesColor(Settings.EdgeColor);
                IsInteractiveUpdating = true;
                OnGraphChanged(Graph);
            }
        }

        public void ResetVerticesBold() {
            if (!IsInteractiveUpdating)
                Graph.SetVerticesBorderColor(Settings.VertexBorderColor);
            else {
                IsInteractiveUpdating = false;
                Graph.SetVerticesBold(false);
                IsInteractiveUpdating = true;
                OnGraphChanged(Graph);
            }
        }

        public void ResetEdgesBold() {
            if (!IsInteractiveUpdating)
                Graph.SetVerticesBorderColor(Settings.VertexBorderColor);
            else {
                IsInteractiveUpdating = false;
                Graph.SetEdgesBold(false);
                IsInteractiveUpdating = true;
                OnGraphChanged(Graph);
            }
        }


        // ----Методы
        private void OnGraphChanged(Graph graph) {
            if (IsInteractiveUpdating)
                DrawGraph();
        }

        private void OnSettingsChanged(DrawingSettings drawingSettings) {
            DrawGraph();
        }

        /// <summary>
        /// Получить поверхность для рисования
        /// </summary>
        private Graphics GetGraphics() {
            // Получаем поверхность рисования
            var g = Graphics.FromImage(Image);
            // Задаём сглаживание при рисовании
            g.SmoothingMode = SmoothingMode.HighQuality;
            return g;
        }

        /// <summary>
        /// Метод отрисовки графа
        /// </summary>
        public void DrawGraph() {
            if (Graph == null)
                return;
            using (var g = GetGraphics()) {
                // Очищаем поверхность и заливаем заданным фоновым цветом
                g.Clear(Settings.BackgroundColor);
                // Рисуем вершины
                foreach (var vertex in Graph.Vertices)
                    if (vertex == SelectedVertex)
                        VertexDrawer.DrawSelectedVertex(g, vertex);
                    else
                        VertexDrawer.DrawVertex(g, vertex);
                // Рисуем рёбра
                foreach (var edge in Graph.Edges)
                    if (edge == SelectedEdge)
                        EdgeDrawer.DrawSelectedEdge(g, edge);
                    else
                        EdgeDrawer.DrawEdge(g, edge);
                // Вызываем перерисовку элемента управления
                Invalidate();
            }
        }


        // ----События
        public event VertexSelectedEventHandler VertexSelectedEvent;
        public event EdgeSelectedEventHandler EdgeSelectedEvent;


        // ----Обработчики событий
        /// <summary>
        /// Привести в соответствие с координатами мыши выделение вершин графа.
        /// Та, которая под мышью, будет выделенной, а та, с которой мышь ушла, станет обычной
        /// </summary>
        /// <param name="mousePoint">Координаты мыши</param>
        private void ChangeVerticesSelecting(PointF mousePoint) {
            // Если была выделенная вершина, проверяем, покинула ли мышь её. Если нет - конец. Если да - убираем подсветку
            if (SelectedVertex != null)
                if (!VertexDrawer.IsCoordinatesOnVertex(SelectedVertex, mousePoint)) {
                    // Для экономии перерисовываем не всё, а только эту вершину
                    VertexDrawer.DrawVertex(GetGraphics(), SelectedVertex);
                    SelectedVertex = null;
                    Invalidate();
                }
                else
                    return;
            // Смотрим, оказалась ли теперь мышь над одной из вершин
            foreach (var vertex in Graph.Vertices) {
                // Если да - выделяем и выходим (больше искать нечего)
                if (VertexDrawer.IsCoordinatesOnVertex(vertex, mousePoint)) {
                    SelectedVertex = vertex;
                    VertexDrawer.DrawSelectedVertex(GetGraphics(), vertex);
                    Invalidate();
                    return;
                }
            }
        }

        /// <summary>
        /// Привести в соответствие с координатами мыши выделение рёбер графа.
        /// То, которое под мышью, будет выделенным, а то, с которого мышь ушла, станет обычным
        /// </summary>
        /// <param name="mousePoint">Координаты мыши</param>
        private void ChangeEdgesSelecting(PointF mousePoint) {
            // Если было выделенное ребро, проверяем, покинула ли мышь его. Если нет - конец. Если да - убираем подсветку
            if (SelectedEdge != null) {
                if (!EdgeDrawer.IsCoordinatesOnEdge(SelectedEdge, mousePoint)) {
                    // Для экономии перерисовываем не всё, а только это ребро
                    EdgeDrawer.DrawEdge(GetGraphics(), SelectedEdge);
                    SelectedEdge = null;
                    Invalidate();
                }
                else
                    return;
            }
            // Смотрим, оказалась ли мышь над одним из рёбер
            foreach (var edge in Graph.Edges) {
                // Если да - выделяем и выходим (искать больше нечего)
                if (EdgeDrawer.IsCoordinatesOnEdge(edge, mousePoint)) {
                    SelectedEdge = edge;
                    EdgeDrawer.DrawSelectedEdge(GetGraphics(), edge);
                    Invalidate();
                    return;
                }
            }
        }

        private Point mouseDownPosition = Point.Empty;
        private Point prevMouseMovingPosition = Point.Empty;

        /// <summary>
        /// Произвести перетаскивание вершины, если требуется
        /// </summary>
        /// <param name="mousePoint">Координаты мыши</param>
        private void DoVertexMove(PointF mousePoint) {
            // Если вершины нельзя перетаскивать - выходим
            if (!IsVerticesMoving)
                return;
            // Если левая кнопка не была нажата, то и перетаскиваний нет
            if (mouseDownPosition == PointF.Empty)
                return;
            // Если никакая вершина не выбрана, или если текущие координаты - это координаты нажатия, то выходим
            if (SelectedVertex == null || mousePoint == mouseDownPosition)
                return;
            // Иначе - меняем координату выбранной вершины в соответствии и изменениями координат
            PointF delta = new PointF { 
                X = mousePoint.X - prevMouseMovingPosition.X,
                Y = mousePoint.Y - prevMouseMovingPosition.Y
            };
            SelectedVertex.DrawingCoords = new PointF { 
                X = SelectedVertex.DrawingCoords.X + delta.X,
                Y = SelectedVertex.DrawingCoords.Y + delta.Y
            };
            prevMouseMovingPosition = new Point((int)mousePoint.X, (int)mousePoint.Y);
        }

        private void DoClickWork(PointF mousePoint) {
            // Если координаты нажатия и отжатия мыши различаются - это не нажатие
            if (mouseDownPosition != mousePoint)
                return;
            // Если неинтерактивный режим, выходим
            if (InteractiveMode == InteractiveMode.NonInteractive)
                return;
            // Если была выбрана вершина или ребро - поднимаем соответствующее событие
            if (SelectedEdge != null && (InteractiveMode == InteractiveMode.Interactive || InteractiveMode == InteractiveMode.OnlyEdges))
                EdgeSelectedEvent?.Invoke(SelectedEdge);
            else if (SelectedVertex != null && (InteractiveMode == InteractiveMode.Interactive || InteractiveMode == InteractiveMode.OnlyVertices))
                VertexSelectedEvent?.Invoke(SelectedVertex);
        }

        private void SimpleGraphVisualizer_MouseMove(object sender, MouseEventArgs e) {
            if (Graph == null)
                return;

            PointF mousePoint = new PointF(e.X, e.Y);

            // Проверяем, перетаскивается ли вершина, и перетаскиваем её
            DoVertexMove(mousePoint);
            // Проверяем выделение рёбер. Выделяем то, которое под мышкой, убираем выделение с того, которое теперь не выделено
            ChangeEdgesSelecting(mousePoint);
            // Проверяем выделение вершин. Выделяем ту, которая под мышкой, убираем выделение с той, которая теперь не выделена
            ChangeVerticesSelecting(mousePoint);
        }

        private void SimpleGraphVisualizer_MouseDown(object sender, MouseEventArgs e) {
            // Обрабатываем нажатие левой кнопки мыши.
            if (e.Button != MouseButtons.Left)
                return;
            mouseDownPosition = e.Location;
            prevMouseMovingPosition = e.Location;
        }

        private void SimpleGraphVisualizer_MouseUp(object sender, MouseEventArgs e) {
            // Обрабатываем отпускание левой кнопки мыши.
            if (e.Button != MouseButtons.Left)
                return;
            PointF mousePoint = new PointF(e.X, e.Y);
            // Если было нажатие (координаты MouseDown совпадают с координатами MouseUp), 
            // проверяем, на что нажали
            DoClickWork(mousePoint);
            mouseDownPosition = Point.Empty;
            prevMouseMovingPosition = Point.Empty;
        }

        private void SimpleGraphVisualizer_SizeChanged(object sender, System.EventArgs e) {
            if (Width != 0 && Height != 0) {
                Image = new Bitmap(Width, Height);
                DrawGraph();
            }
        }
    }
}
