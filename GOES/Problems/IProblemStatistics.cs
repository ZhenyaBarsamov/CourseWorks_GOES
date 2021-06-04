namespace GOES.Problems {
    /// <summary>
    /// Интерфейс объектов статистики решения задач
    /// </summary>
    public interface IProblemStatistics {
        /// <summary>
        /// Флаг, показывающий, было ли завершено решение задачи
        /// </summary>
        bool IsSolved { get; set; }
        /// <summary>
        /// Общее количество ошибок
        /// </summary>
        int TotalErrorsCount { get; }
        /// <summary>
        /// Общее количество ошибок, которые повлияли на оценку
        /// </summary>
        int TotalNecessaryErrorsCount { get; }
        /// <summary>
        /// Оценка задания
        /// </summary>
        int Mark { get; }
        /// <summary>
        /// Получить текст статистики решения задачи
        /// </summary>
        string GetStatisticsText();
    }
}
