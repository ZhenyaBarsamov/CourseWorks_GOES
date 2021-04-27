using Microsoft.Msagl.Drawing;

namespace MAGL_Test.GraphWrapper {
    public class MsaglNodeWrapper {
        // ----Свойства вершины
        /// <summary>
        /// Вершина MSAGL, которую оборачивает данный объект
        /// </summary>
        public Node Node { get; private set; }

        /// <summary>
        /// Номер вершины
        /// </summary>
        public int Number {
            get => int.Parse(Node.Id);
            set { 
                Node.Id = value.ToString();
                VertexChainged?.Invoke(this);
            }
        }

        private string label;
        /// <summary>
        /// Строковая метка вершины, отображающаяся рядом с ней
        /// </summary>
        public string Label {
            get => label;
            set {
                label = value;
                if (string.IsNullOrEmpty(label))
                    Node.LabelText = Node.Id.ToString();
                else
                    Node.LabelText = $"{Node.Id} | {label}";
                VertexChainged?.Invoke(this);
            }
        }

        /// <summary>
        /// Цвет рисования границ вершины
        /// </summary>
        public System.Drawing.Color BorderColor {
            get => System.Drawing.Color.FromArgb(Node.Attr.Color.A, Node.Attr.Color.R, Node.Attr.Color.G, Node.Attr.Color.B);
            set { 
                Node.Attr.Color = new Microsoft.Msagl.Drawing.Color(value.A, value.R, value.G, value.B);
                VertexChainged?.Invoke(this);
            }
        }

        /// <summary>
        /// Цвет заливки вершины
        /// </summary>
        public System.Drawing.Color FillColor {
            get => System.Drawing.Color.FromArgb(Node.Attr.FillColor.A, Node.Attr.FillColor.R, Node.Attr.FillColor.G, Node.Attr.FillColor.B);
            set { 
                Node.Attr.FillColor = new Microsoft.Msagl.Drawing.Color(value.A, value.R, value.G, value.B);
                VertexChainged?.Invoke(this);
            }
        }

        /// <summary>
        /// Конструктор для создания объекта-обёртки для объекта Msagl.Drawing.Node
        /// </summary>
        public MsaglNodeWrapper(Node node) {
            Node = node;
            Label = null;
        }

        // ----ToString
        public override string ToString() {
            return Node.Id.ToString();
        }

        // ----События
        /// <summary>
        /// Делегат для обработчика события изменения вершины
        /// </summary>
        /// <param name="vertex">Вызвавшая событие вершина</param>
        public delegate void VertexChaingedEventHandler(MsaglNodeWrapper vertex);
        /// <summary>
        /// Событие изменения свойств вершины
        /// </summary>
        public event VertexChaingedEventHandler VertexChainged;
    }
}
