using System.Windows.Forms;
using SGVL.Types.Visualizers;
using SGVL.Types.Graphs;
using System.Drawing;

namespace SGVL.Visualizers.MsaglGraphVisualizer {
    /// <summary>
    /// Элемент управления для визуализации графа на основе элемента управления GViewer
    /// библиотеки MSAGL
    /// </summary>
    public partial class MsaglGraphVisualizer : UserControl, IGraphVisualizer {
        // ----Свойства
        private MsaglGraphWrapper MsaglGraphWrapper { get; set; }

        public Graph Graph => MsaglGraphWrapper.SgvlGraph;

        public InteractiveMode InteractiveMode { get; set; }

        public bool IsInteractiveUpdating { get; set; }

        public bool IsVerticesMoving { 
            get => gViewer.LayoutEditingEnabled;
            set {
                gViewer.LayoutEditingEnabled = value;
            }
        }

        // ----Конструктор
        public MsaglGraphVisualizer() {
            InitializeComponent();
            // Отключаем ненужные кнопки у MSAGL-визуализатора
            gViewer.EdgeInsertButtonVisible = false;
            gViewer.LayoutAlgorithmSettingsButtonVisible = false;
            gViewer.SaveButtonVisible = false;
            gViewer.SaveGraphButtonVisible = false;
            gViewer.UndoRedoButtonsVisible = false;
            gViewer.CustomOpenButtonPressed += (sender, e) => e.Handled = true;
        }

        // ----Методы
        public void Initialize(Graph graph) {
            // Отписываемся от текущего графа, если нужно
            if (MsaglGraphWrapper != null)
                Graph.GraphChainged -= OnGraphChanged;
            // Создаём обёртку для графа MSAGL по этому графу
            MsaglGraphWrapper = new MsaglGraphWrapper(graph);
            // Подписываемся на изменения графа 
            graph.GraphChainged += OnGraphChanged;
            // MSAGL-визуализатору даём MSAGL-граф из обёртки
            gViewer.Graph = MsaglGraphWrapper.MsaglGraph;
            // Задаём настройки визуализации
            InteractiveMode = InteractiveMode.Interactive;
            IsVerticesMoving = true;
            IsInteractiveUpdating = true;
        }

        private void OnGraphChanged(Graph graph) {
            if (IsInteractiveUpdating)
                gViewer.Invalidate();
        }

        // ----События
        public event VertexSelectedEventHandler VertexSelectedEvent;
        public event EdgeSelectedEventHandler EdgeSelectedEvent;

        // ----Обработка событий выбора вершины/ребра
        private Point mouseDownPosition = Point.Empty;
        private void gViewer_MouseDown(object sender, MouseEventArgs e) {
            if (InteractiveMode == InteractiveMode.NonInteractive)
                return;
            mouseDownPosition = e.Location;
        }

        private void gViewer_MouseUp(object sender, MouseEventArgs e) {
            if (mouseDownPosition == e.Location) {
                if (InteractiveMode == InteractiveMode.NonInteractive)
                    return;
                if (gViewer.SelectedObject is Microsoft.Msagl.Drawing.Node) {
                    if (InteractiveMode == InteractiveMode.OnlyEdges)
                        return;
                    Microsoft.Msagl.Drawing.Node node = gViewer.SelectedObject as Microsoft.Msagl.Drawing.Node;
                    int vertexIndex = int.Parse(node.Id) - 1;
                    VertexSelectedEvent?.Invoke(Graph.Vertices[vertexIndex]);
                }
                else if (gViewer.SelectedObject is Microsoft.Msagl.Drawing.Edge) {
                    if (InteractiveMode == InteractiveMode.OnlyVertices)
                        return;
                    Microsoft.Msagl.Drawing.Edge edge = gViewer.SelectedObject as Microsoft.Msagl.Drawing.Edge;
                    int sourceIndex = int.Parse(edge.Source) - 1;
                    int targetIndex = int.Parse(edge.Target) - 1;
                    EdgeSelectedEvent?.Invoke(Graph.GetEdge(sourceIndex, targetIndex));
                }
            }
            // Костыль на случай, если была перенесена вершина. Иначе изображение лагает, и само не перерисовывается
            else {
                if (IsVerticesMoving)
                    gViewer.Invalidate();
            }
            mouseDownPosition = Point.Empty;
        }
    }
}
