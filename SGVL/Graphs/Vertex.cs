using System.Drawing;

namespace SGVL.Graphs {
    /// <summary>
    /// Класс вершины графа
    /// </summary>
    public class Vertex {
        // ----Свойства вершины
        /// <summary>
        /// Номер вершины
        /// </summary>
        public int Number { get; private set; }

        private string label;
        /// <summary>
        /// Строковая метка вершины, отображающаяся рядом с ней
        /// </summary>
        public string Label {
            get => label;
            set {
                label = value;
                LabelChanged?.Invoke(this);
                VertexChainged?.Invoke(this);
            }
        }

        private Color borderColor;
        /// <summary>
        /// Цвет рисования границ вершины
        /// </summary>
        public Color BorderColor {
            get => borderColor;
            set {
                borderColor = value;
                BorderColorChanged?.Invoke(this);
                VertexChainged?.Invoke(this);
            }
        }

        private Color fillColor;
        /// <summary>
        /// Цвет заливки вершины
        /// </summary>
        public Color FillColor {
            get => fillColor;
            set {
                fillColor = value;
                FillColorChanged?.Invoke(this);
                VertexChainged?.Invoke(this);
            }
        }

        private bool bold;
        /// <summary>
        /// Флаг, показывающий, необходимо ли рисовать вершину толстой линией
        /// </summary>
        public bool Bold {
            get => bold;
            set {
                bold = value;
                BoldChanged?.Invoke(this);
                VertexChainged?.Invoke(this);
            }
        }

        private PointF drawingCoords;
        /// <summary>
        /// Координаты рисования вершины
        /// </summary>
        public PointF DrawingCoords {
            get => drawingCoords;
            set {
                drawingCoords = value;
                DrawingCoordsChainged?.Invoke(this);
                VertexChainged?.Invoke(this);
            }
        }

        
        // ----Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="number">Номер вершины</param>
        /// <param name="label">Метка вершины</param>
        public Vertex(int number, string label = "") {
            Number = number;
            Label = label;
            BorderColor = Color.Black;
            FillColor = Color.White;
            Bold = false;
            DrawingCoords = new PointF(0, 0);
        }

        // ----ToString
        public override string ToString() {
            return Number.ToString();
        }

        // ----События
        /// <summary>
        /// Событие изменения свойств вершины
        /// </summary>
        public event VertexChaingedEventHandler VertexChainged;

        /// <summary>
        /// Событие изменения метки вершины
        /// </summary>
        public event VertexChaingedEventHandler LabelChanged;

        /// <summary>
        /// Событие изменения цвета границы вершины
        /// </summary>
        public event VertexChaingedEventHandler BorderColorChanged;

        /// <summary>
        /// Событие изменения цвета заливки вершины
        /// </summary>
        public event VertexChaingedEventHandler FillColorChanged;

        /// <summary>
        /// Событие изменения веделения вершины жирным
        /// </summary>
        public event VertexChaingedEventHandler BoldChanged;

        /// <summary>
        /// Событие изменения координат рисования вершины
        /// </summary>
        public event VertexChaingedEventHandler DrawingCoordsChainged;
    }
}
