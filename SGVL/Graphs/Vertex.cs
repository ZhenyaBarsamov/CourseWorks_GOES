using System.Drawing;
using SGVL.Graphs.DisplayedData;

namespace SGVL.Graphs {
    /// <summary>
    /// Класс вершины графа
    /// </summary>
    /// <typeparam name="TData">Тип данных, которые будут привязываться к вершине</typeparam>
    public class Vertex {
        // ----Свойства вершины
        /// <summary>
        /// Номер вершины
        /// </summary>
        public int Number { get; private set; }

        private IDisplayedData data;
        /// <summary>
        /// Данные, прикреплённые к вершине
        /// </summary>
        public IDisplayedData Data {
            get => data;
            set {
                data = value;
                VertexChainged(this);
            } 
        }

        /// <summary>
        /// Строковая метка вершины, отображающаяся рядом с ней
        /// </summary>
        public string Label => Data != null ? Data.ToDisplayedText() : Number.ToString();

        private PointF drawingCoordinates;
        /// <summary>
        /// Координаты рисования вершины
        /// </summary>
        public PointF DrawingCoordinates {
            get => drawingCoordinates; 
            set {
                drawingCoordinates = value;
                VertexChainged(this);
            }
        }

        private Color drawingColor;
        /// <summary>
        /// Цвет рисования вершины
        /// </summary>
        public Color DrawingColor {
            get => drawingColor;
            set {
                drawingColor = value;
                VertexChainged(this);
            }
        }

        // ----Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="number">Номер вершины</param>
        /// <param name="drawingCoordinates">Координаты рисования вершины</param>
        public Vertex(int number, IDisplayedData data = default) {
            Number = number;
            Data = data;
        }

        // ----ToString
        public override string ToString() {
            return $"{Number} ({Label})";
        }

        // ----События
        /// <summary>
        /// Делегат для обработчика события изменения вершины
        /// </summary>
        /// <param name="vertex">Вызвавшая событие вершина</param>
        public delegate void VertexChaingedEventHandler(Vertex vertex);
        /// <summary>
        /// Событие изменения свойств вершины
        /// </summary>
        public event VertexChaingedEventHandler VertexChainged;
    }
}
