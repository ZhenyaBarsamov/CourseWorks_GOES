using SGVL.Graphs.DisplayedData;

namespace SGVL.Graphs {
    /// <summary>
    /// Класс ребра графа
    /// </summary>
    /// <typeparam name="TData">Тип данных, которые будут привязываться к ребру</typeparam>
    public class Edge {
        // ----Свойства ребра
        /// <summary>
        /// Номер начальной вершины ребра
        /// </summary>
        public int SourceVertexNumber { get; private set; }

        /// <summary>
        /// Номер конечной вершины ребра
        /// </summary>
        public int TargetVertexNumber { get; private set; }

        /// <summary>
        /// Флаг, показывающий, явлется ли ребро ориентированным
        /// </summary>
        public bool IsDirected { get; private set; }

        private IDisplayedData data;
        /// <summary>
        /// Данные, прикреплённые к ребру
        /// </summary>
        public IDisplayedData Data {
            get => data;
            set {
                data = value;
                EdgeChainged(this);
            }
        }

        /// <summary>
        /// Строковая метка ребра, отображающаяся рядом с ним
        /// </summary>
        public string Label => Data != null ? Data.ToDisplayedText() : string.Empty;

        // ----Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sourceVertexNumber">Номер начальной вершины ребра</param>
        /// <param name="targetVertexNumber">Номер конечной вершины ребра</param>
        /// <param name="data">Прикреплённые к ребру данные</param>
        public Edge(int sourceVertexNumber, int targetVertexNumber, bool isDirected, IDisplayedData data = default) {
            SourceVertexNumber = sourceVertexNumber;
            TargetVertexNumber = targetVertexNumber;
            IsDirected = isDirected;
            Data = data;
        }

        // ----ToString
        public override string ToString() {
            return $"({SourceVertexNumber}-{TargetVertexNumber}) ({Label})";
        }

        // ----События
        /// <summary>
        /// Делегат для обработчика события изменения ребра
        /// </summary>
        /// <param name="edge">Вызвавшая событие вершина</param>
        public delegate void EdgeChaingedEventHandler(Edge edge);
        /// <summary>
        /// Событие изменения свойств ребра
        /// </summary>
        public event EdgeChaingedEventHandler EdgeChainged;
    }
}
