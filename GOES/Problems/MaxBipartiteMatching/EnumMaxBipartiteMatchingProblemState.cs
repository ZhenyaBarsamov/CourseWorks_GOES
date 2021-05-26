namespace GOES.Problems.MaxBipartiteMatching {
    /// <summary>
    /// Состояния решения задачи о максимальном паросочетании в двудольном графе
    /// </summary>
    enum MaxBipartiteMatchingProblemState {
        /// <summary>
        /// Ожидание от ученика старта задания
        /// </summary>
        StartWaiting,
        /// <summary>
        /// Ожидание от ученика следующей вершины при построении аугментального пути
        /// </summary>
        NextPathVertexWaiting,
        /// <summary>
        /// Ожидание от ученика значения мощности найденного максимального паросочетания
        /// </summary>
        MaxMatchingCardinalityWaiting,
        /// <summary>
        /// Выполнение задания завершено
        /// </summary>
        ProblemFinish,
    }
}
