namespace SGVL.Types.Visualizers {
    /// <summary>
    /// Перечисление для режимов интерактивности визуализации графа
    /// </summary>
    public enum InteractiveMode {
        /// <summary>
        /// Неинтерактивная визуализация, реакции на выбор вершин/рёбер нет
        /// </summary>
        NonInteractive,
        /// <summary>
        /// Интерактивны только вершины. Реакции на выбор рёбер нет
        /// </summary>
        OnlyVertices,
        /// <summary>
        /// Интерактивны только рёбра. Реакции на выбор вершин нет
        /// </summary>
        OnlyEdges,
        /// <summary>
        /// Полностью интерактивная визуализация, допускающая выбор вершин и дуг
        /// </summary>
        Interactive
    }
}
