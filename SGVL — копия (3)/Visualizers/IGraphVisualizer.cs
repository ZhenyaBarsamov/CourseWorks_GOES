using SGVL.Graphs;

namespace SGVL.Visualizers {
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
        /// <summary>
        /// Настройки визуализации
        /// </summary>
        DrawingSettings Settings { get; }


        // ----Методы
        /// <summary>
        /// Инициализировать визуализацию для заданного графа и начать построение визуализации
        /// Необходимо вызывать каждый раз при замещении визуализируемого графа новым.
        /// </summary>
        /// <param name="graph">Граф, для которого необходимо строить визуализацию</param>
        void Initialize(Graph graph);

        /// <summary>
        /// Сбросить цвет границ всех вершин на цвет по умолчанию (Settings.VertexBorderColor)
        /// </summary>
        void ResetVerticesBorderColor();

        /// <summary>
        /// Сбросить цвет заливки всех вершин на цвет по умолчанию (Settings.VertexFillingColor)
        /// </summary>
        void ResetVerticesFillColor();
        
        /// <summary>
        /// Сбросить цвет всех рёбер на цвет по умолчанию (Settings.EdgeColor)
        /// </summary>
        void ResetEdgesColor();

        /// <summary>
        /// Сбросить выделение всех вершин жирным
        /// </summary>
        void ResetVerticesBold();

        /// <summary>
        /// Сбросить выделение всех рёбер жирным
        /// </summary>
        void ResetEdgesBold();


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
