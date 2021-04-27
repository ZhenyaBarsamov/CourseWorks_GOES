using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MAGL_Test.GraphWrapper;

namespace MAGL_Test.MsaglViewerWrapper {
    /// <summary>
    /// Перечисление для режимов интерактивности визуализации графа
    /// </summary>
    public enum InteractiveMode {
        /// <summary>
        /// Неинтерактивная визуализация, реакции на выбор вершин/рёбер нет
        /// </summary>
        NonInteractive,
        /// <summary>
        /// Интерактивны только вершины. Реакции на выбор рёбер нет
        /// </summary>
        OnlyVertices,
        /// <summary>
        /// Интерактивны только рёбра. Реакции на выбор вершин нет
        /// </summary>
        OnlyEdges,
        /// <summary>
        /// Полностью интерактивная визуализация, допускающая выбор вершин и дуг
        /// </summary>
        Interactive
    }

    public partial class MsaglViewerWrapper : UserControl {
        public MsaglGraphWrapper Graph { get; set; }

        public InteractiveMode InteractiveMode { get; set; }
        //public VisualizationSettings Settings { get; set; }

        public MsaglViewerWrapper() {
            InitializeComponent();
        }

        // ----Методы
        /// <summary>
        /// Инициализировать визуализацию для заданного графа и начать построение визуализации
        /// Необходимо вызывать каждый раз при замещении визуализируемого графа новым.
        /// </summary>
        /// <param name="graph">Граф, для которого необходимо строить визуализацию</param>
        public void Initialize(MsaglGraphWrapper graph) {
            Graph = graph;
            Graph.GraphChainged += OnGraphChanged;
            gViewer.Graph = graph.Graph;
            InteractiveMode = InteractiveMode.Interactive;
        }
        /// <summary>
        /// Начать (продолжить) построение визуализации. Будет возобновлено отслеживание изменений в графе.
        /// </summary>
        public void StartUpdate() {
            Graph.GraphChainged += OnGraphChanged;
        }
        /// <summary>
        /// Приостановить построение визуализации. Будет приостановлено отслеживание изменений в графе.
        /// </summary>
        public void StopUpdate() {
            Graph.GraphChainged -= OnGraphChanged;
        }
        /// <summary>
        /// Задать алгоритм укладки графа, который будет использоваться при визуализации графа
        /// </summary>
        /// <param name="layoutAlgorithm">Объект алгоритма укладки</param>
        public void SetLayout(ILayoutAlgorithm layoutAlgorithm) {
            throw new NotImplementedException();
        }

        private void OnGraphChanged(MsaglGraphWrapper graph) {
            gViewer.Invalidate();
        }

        // ----События
        public delegate void VertexSelectedEventHandler(MsaglNodeWrapper selectedVertex);
        /// <summary>
        /// Событие выбора вершины графа
        /// </summary>
        public event VertexSelectedEventHandler VertexSelectedEvent;

        public delegate void EdgeSelectedEventHandler(MsaglEdgeWrapper selectedEdge);
        /// <summary>
        /// Событие выбора дуги графа
        /// </summary>
        public event EdgeSelectedEventHandler EdgeSelectedEvent;


        private Point mouseDownPosition = Point.Empty;
        private void gViewer_MouseUp(object sender, MouseEventArgs e) {
            if (InteractiveMode == InteractiveMode.NonInteractive)
                return;
            if (mouseDownPosition == e.Location) {
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
            mouseDownPosition = Point.Empty;
        }

        private void gViewer_MouseDown(object sender, MouseEventArgs e) {
            if (InteractiveMode == InteractiveMode.NonInteractive)
                return;
            mouseDownPosition = e.Location;
        }
    }
}
