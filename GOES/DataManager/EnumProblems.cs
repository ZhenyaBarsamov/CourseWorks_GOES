namespace GOES.DataManager {
    /// <summary>
    /// Перечисление для доступных в обучающей системе оптимизационных задач
    /// </summary>
    public enum Problem {
        /// <summary>
        /// Задача о максимальном потоке (и задача о минимальном разрезе) в сети
        /// </summary>
        MaximalFlow,
        /// <summary>
        /// Задача о максимальном паросочетании в двудольном графе
        /// </summary>
        MaximalBipartiteMatching
    }
}
