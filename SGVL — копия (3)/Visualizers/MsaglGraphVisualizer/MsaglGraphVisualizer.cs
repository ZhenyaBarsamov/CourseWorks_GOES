using System.Windows.Forms;
using SGVL.Graphs;
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

        public DrawingSettings Settings { get; private set; }
        private MsaglSettingsWrapper SettingsWrapper { get; set; }


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
        private void OnGraphChanged(Graph graph) {
            if (IsInteractiveUpdating)
                gViewer.Invalidate();
        }

        public void Initialize(Graph graph) {
            // Отписываемся от текущего графа, если он был
            if (MsaglGraphWrapper != null)
                Graph.GraphChainged -= OnGraphChanged;
            // Создаём обёртку для графа MSAGL по этому графу
            MsaglGraphWrapper = new MsaglGraphWrapper(graph);
            // Подписываемся на изменения графа 
            Graph.GraphChainged += OnGraphChanged;
            // MSAGL-визуализатору даём MSAGL-граф из обёртки
            gViewer.Graph = MsaglGraphWrapper.MsaglGraph;
            // Задаём настройки визуализации
            InteractiveMode = InteractiveMode.Interactive;
            IsVerticesMoving = true;
            IsInteractiveUpdating = true;
            Settings = new DrawingSettings();
            SettingsWrapper = new MsaglSettingsWrapper(gViewer, MsaglGraphWrapper.MsaglGraph, MsaglGraphWrapper.SgvlGraph, Settings);
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


        // ----События
        public event VertexSelectedEventHandler VertexSelectedEvent;
        public event EdgeSelectedEventHandler EdgeSelectedEvent;


        // ----Обработка событий выбора вершины/ребра
        private Point mouseDownPosition = Point.Empty;
        private void gViewer_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left)
                return;
            // При нажатии левой кнопки мыши, если есть хотя бы какая-то интерактивность, сохраняем точку нажатия
            if (InteractiveMode == InteractiveMode.NonInteractive)
                return;
            mouseDownPosition = e.Location;
        }

        private void gViewer_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left)
                return;
            // При отпускании левой кнопки мыши, если было нажатие в интерактивном режиме, создаём событие нажатия нужного объекта графа
            if (mouseDownPosition == e.Location) {
                if (InteractiveMode == InteractiveMode.NonInteractive)
                    return;
                object selectedObject = gViewer.SelectedObject;
                if (selectedObject is Microsoft.Msagl.Drawing.Node) {
                    if (InteractiveMode == InteractiveMode.OnlyEdges)
                        return;
                    Microsoft.Msagl.Drawing.Node node = selectedObject as Microsoft.Msagl.Drawing.Node;
                    int vertexIndex = int.Parse(node.Id) - 1;
                    VertexSelectedEvent?.Invoke(Graph.Vertices[vertexIndex]);
                }
                else if (selectedObject is Microsoft.Msagl.Drawing.Edge) {
                    if (InteractiveMode == InteractiveMode.OnlyVertices)
                        return;
                    Microsoft.Msagl.Drawing.Edge edge = selectedObject as Microsoft.Msagl.Drawing.Edge;
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
