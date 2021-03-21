namespace SGVL.Visualization.Visualizators {
    /// <summary>
    /// Перечисление для интерактивных действий с визуализацией графа
    /// </summary>
    public enum InteractiveAction {
        /// <summary>
        /// Позволяет выбирать вершину или ребро щелчком мыши 
        /// </summary>
        Select,
        /// <summary>
        /// Позволяет перетаскивать вершину, меняя её расположение. Действия с рёбрами игнорируются
        /// </summary>
        Move
    }
}
