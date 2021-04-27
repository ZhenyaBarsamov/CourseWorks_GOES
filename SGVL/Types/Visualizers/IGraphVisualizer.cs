using SGVL.Types.Graphs;

namespace SGVL.Types.Visualizers {
    /// <summary>
    /// Интерфейс для визуализатора графа
    /// </summary>
    public interface IGraphVisualizer {
        // ----Свойства
        /// <summary>
        /// Отображаемый граф
        /// </summary>
        Graph Graph { get; }

        /// <summary>
        /// Режим интерактивной визуализации
        /// </summary>
        InteractiveMode InteractiveMode { get; set; }

        /// <summary>
        /// Флаг, указывающий, возможно ли перемещение вершин графа
        /// </summary>
        bool IsVerticesMoving { get; set; }

        /// <summary>
        /// Флаг, задающий способ обновления визуализации графа.
        /// Если он установлен, визуализация будет мгновенно реагировать на любое изменение в графе и
        /// сразу же перерисовывать его
        /// </summary>
        bool IsInteractiveUpdating { get; set; }


        // ----Методы
        /// <summary>
        /// Инициализировать визуализацию для заданного графа и начать построение визуализации
        /// Необходимо вызывать каждый раз при замещении визуализируемого графа новым.
        /// </summary>
        /// <param name="graph">Граф, для которого необходимо строить визуализацию</param>
        void Initialize(Graph graph);


        // ----События
        /// <summary>
        /// Событие выбора вершины графа
        /// </summary>
        event VertexSelectedEventHandler VertexSelectedEvent;

        /// <summary>
        /// Событие выбора дуги графа
        /// </summary>
        event EdgeSelectedEventHandler EdgeSelectedEvent;
    }
}
