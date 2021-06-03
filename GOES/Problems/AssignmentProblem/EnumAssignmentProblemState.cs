namespace GOES.Problems.AssignmentProblem {
    /// <summary>
    /// Состояния решения задачи о назначениях
    /// </summary>
    enum AssignmentProblemState {
        /// <summary>
        /// Ожидание от ученика старта задания
        /// </summary>
        StartWaiting,
        /// <summary>
        /// Первый этап задания - вычитание из строк матрицы их минимальных элементов
        /// </summary>
        FirstStage,
        /// <summary>
        /// Второй этап задания - вычитание из столбцов матрицы их минимальных элементов
        /// </summary>
        SecondStage,
        /// <summary>
        /// Ожидание от ученика следующей вершины при построении чередующегося пути при поиске паросочетания
        /// </summary>
        NextPathVertexWaiting,
        /// <summary>
        /// Четвёртый этап задания - перераспределение нулей
        /// </summary>
        FourthStage,
        /// <summary>
        /// Ожидание от ученика стоимости назначений
        /// </summary>
        AssignmentCostWaiting,
        /// <summary>
        /// Выполнение задания завершено
        /// </summary>
        ProblemFinish,
    }
}
