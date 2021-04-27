using System.Drawing;

namespace SGVL.Types.Graphs {
    /// <summary>
    /// Класс ребра графа
    /// </summary>
    public class Edge {
        // ----Свойства ребра
        /// <summary>
        /// Начальная вершина ребра
        /// </summary>
        public Vertex SourceVertex { get; private set; }

        /// <summary>
        /// Конечная вершина ребра
        /// </summary>
        public Vertex TargetVertex { get; private set; }

        private string label;
        /// <summary>
        /// Строковая метка ребра, отображающаяся рядом с ним
        /// </summary>
        public string Label {
            get => label;
            set {
                label = value;
                LabelChanged?.Invoke(this);
                EdgeChainged?.Invoke(this);
            }
        }

        private Color color;
        /// <summary>
        /// Цвет рисования ребра
        /// </summary>
        public Color Color {
            get => color;
            set {
                color = value;
                ColorChanged?.Invoke(this);
                EdgeChainged?.Invoke(this);
            }
        }

        // ----Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sourceNode">Начальная вершина ребра</param>
        /// <param name="targetNode">Конечная вершина ребра</param>
        public Edge(Vertex sourceNode, Vertex targetNode, string label = "") {
            SourceVertex = sourceNode;
            TargetVertex = targetNode;
            Label = label;
            Color = Color.Black;
        }

        // ----ToString
        public override string ToString() {
            return $"({SourceVertex}-{TargetVertex}) ({Label})";
        }

        // ----События
        /// <summary>
        /// Событие изменения свойств ребра
        /// </summary>
        public event EdgeChaingedEventHandler EdgeChainged;

        /// <summary>
        /// Событие изменения метки ребра
        /// </summary>
        public event EdgeChaingedEventHandler LabelChanged;

        /// <summary>
        /// Событие изменения цвета ребра
        /// </summary>
        public event EdgeChaingedEventHandler ColorChanged;
    }
}
