using Microsoft.Msagl.Drawing;

namespace MAGL_Test.GraphWrapper {
    public class MsaglEdgeWrapper {
        // ----Свойства ребра
        /// <summary>
        /// Ребро MSAGL, которое оборачивает данный объект
        /// </summary>
        public Edge Edge { get; private set; }

        /// <summary>
        /// Начальная вершина ребра
        /// </summary>
        public MsaglNodeWrapper SourceVertex { get; private set; }

        /// <summary>
        /// Конечная вершина ребра
        /// </summary>
        public MsaglNodeWrapper TargetVertex { get; private set; }

        /// <summary>
        /// Строковая метка ребра, отображающаяся рядом с ним
        /// </summary>
        public string Label {
            get => Edge.LabelText;
            set { 
                Edge.LabelText = value;
                EdgeChainged?.Invoke(this);
            }
        }

        /// <summary>
        /// Цвет рисования ребра
        /// </summary>
        public System.Drawing.Color DrawingColor {
            get => System.Drawing.Color.FromArgb(Edge.Attr.Color.A, Edge.Attr.Color.R, Edge.Attr.Color.G, Edge.Attr.Color.B);
            set { 
                Edge.Attr.Color = new Microsoft.Msagl.Drawing.Color(value.A, value.R, value.G, value.B);
                EdgeChainged?.Invoke(this);
            }
        }

        // ----Конструкторы
        /// <summary>
        /// Конструктор для создания объекта-обёртки для объекта Msagl.Drawing.Edge
        /// </summary>
        /// <param name="edge">Объект Msagl.Drawing.Edge, для которого создаётся обёртка</param>
        /// <param name="sourceNode">Обёртка для начальной вершины ребра</param>
        /// <param name="targetNode">Обёртка для конечной вершины ребра</param>
        public MsaglEdgeWrapper(Edge edge, MsaglNodeWrapper sourceNode, MsaglNodeWrapper targetNode) {
            Edge = edge;
            SourceVertex = sourceNode;
            TargetVertex = targetNode;
        }

        // ----ToString
        public override string ToString() {
            return $"({SourceVertex}-{TargetVertex}) ({Label})";
        }

        // ----События
        /// <summary>
        /// Делегат для обработчика события изменения ребра
        /// </summary>
        /// <param name="edge">Вызвавшая событие вершина</param>
        public delegate void EdgeChaingedEventHandler(MsaglEdgeWrapper edge);
        /// <summary>
        /// Событие изменения свойств ребра
        /// </summary>
        public event EdgeChaingedEventHandler EdgeChainged;
    }
}
