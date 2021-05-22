namespace GOES.Problems.MaxFlow {
    /// <summary>
    /// Состояния решения задачи о максимальном потоке (и минимальном разрезе)
    /// </summary>
    public enum MaxFlowProblemState {
        /// <summary>
        /// Ожидание от ученика старта задания
        /// </summary>
        StartWaiting,
        /// <summary>
        /// Ожидание от ученика следующей вершины при построении аугментального пути
        /// </summary>
        NextPathVertexWaiting,
        /// <summary>
        /// Ожидание от ученика метки для выбранной вершины
        /// </summary>
        PathVertexLabelWaiting,
        /// <summary>
        /// Ожидание от ученика величины дополнительного потока для построенной аугментальной цепи
        /// </summary>
        FlowRaiseWaiting,
        /// <summary>
        /// Ожидание от ученика значения величины найденного максимального потока
        /// </summary>
        MaximalFlowWaiting,
        /// <summary>
        /// Ожидание от ученика следующего ребра при построении минимального разреза
        /// </summary>
        NextCutEdgeWaiting,
        /// <summary>
        /// Выполнение задания завершено
        /// </summary>
        ProblemFinish
    }
}
