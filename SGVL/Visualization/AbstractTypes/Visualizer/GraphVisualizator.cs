using SGVL.Graphs.DisplayedData;
using SGVL.Graphs;
using System.Drawing;
using SGVL.Visualization.AbstractTypes.EdgeDrawer;
using SGVL.Visualization.AbstractTypes.VertexDrawer;
using SGVL.Visualization.AbstractTypes.Layout;

namespace SGVL.Visualization.AbstractTypes.Visualizer {
    /// <summary>
    /// Абстрактный класс, представляющий визуализатор графа
    /// </summary>
    public abstract class GraphVisualizator {
        // ----Свойства класса
        private IVertexDrawer VertexDrawer { get; set; }
        private IEdgeDrawer EdgeDrawer { get; set; }
        public Graph Graph { get; private set; }
        public InteractiveMode InteractiveMode { get; set; }
        public InteractiveAction InteractiveAction { get; set; }
        public VisualizationSettings Settings { get; set; }

        // ----Констукторы

        // ----Методы
        /// <summary>
        /// Инициализировать визуализацию для заданного графа и начать построение визуализации
        /// Необходимо вызывать каждый раз при замещении визуализируемого графа новым.
        /// </summary>
        /// <param name="graph">Граф, для которого необходимо строить визуализацию</param>
        public abstract void Initialize(Graph graph);
        /// <summary>
        /// Начать (продолжить) построение визуализации. Будет возобновлено отслеживание изменений в графе.
        /// </summary>
        public abstract void StartUpdate();
        /// <summary>
        /// Приостановить построение визуализации. Будет приостановлено отслеживание изменений в графе.
        /// </summary>
        public abstract void StopUpdate();
        /// <summary>
        /// Задать алгоритм укладки графа, который будет использоваться при визуализации графа
        /// </summary>
        /// <param name="layoutAlgorithm">Объект алгоритма укладки</param>
        public abstract void SetLayout(ILayoutAlgorithm layoutAlgorithm);

        // ----События
        public delegate void VertexSelectedEventHandler(Vertex selectedVertex);
        /// <summary>
        /// Событие выбора вершины графа
        /// </summary>
        public event VertexSelectedEventHandler VertexSelectedEvent;

        public delegate void EdgeSelectedEventHandler(Edge selectedEdge);
        /// <summary>
        /// Событие выбора дуги графа
        /// </summary>
        public event EdgeSelectedEventHandler EdgeSelectedEvent;
    }
}
