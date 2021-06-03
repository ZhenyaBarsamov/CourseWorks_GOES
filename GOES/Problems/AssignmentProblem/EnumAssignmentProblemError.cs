namespace GOES.Problems.AssignmentProblem {
    /// <summary>
    /// Перечисление для ошибок, которые могут быть допущены учеником 
    /// при решении задачи о назначениях
    /// </summary>
    enum AssignmentProblemError {
        /// <summary>
        /// Неправильный формат ввода матрицы
        /// </summary>
        IncorrectNextMatrixFormat,
        /// <summary>
        /// Неправильная матрица на первом шаге решения (вычесть из строк минимальные элементы)
        /// </summary>
        FirstStageIncorrectNextMatrix,
        /// <summary>
        /// Неправильная матрица на втором шаге решения (вычесть из столбцов минимальные элементы)
        /// </summary>
        SecondStageIncorrectNextMatrix,
        /// <summary>
        /// Третий шаг решения: начало построения аугментальной цепи с вершины, покрытой паросочетанием
        /// </summary>
        ThirdStageStartOnMatchedVertex,
        /// <summary>
        /// Третий шаг решения: попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи
        /// </summary>
        ThirdStageMoveToFarVertex,
        /// <summary>
        /// Третий шаг решения: нарушение чередования при построении аугментальной цепи - добавление паросочетанного ребра к предыдущему паросочетанному
        /// </summary>
        ThirdStageAlternationBreakingOnMatchedEdge,
        /// <summary>
        /// Третий шаг решения: нарушение чередования при построении аугментальной цепи - добавление непаросочетанного ребра к предыдущему непаросочетанному
        /// </summary>
        ThirdStageAlternationBreakingOnNotMatchedEdge,
        /// <summary>
        /// Третий шаг решения: попытка провести чередование по недостроенному аугментальному пути (путь из одной вершины или пустой путь)
        /// </summary>
        ThirdStageAugmentalPathIsNotFinished,
        /// <summary>
        /// Третий шаг решения: попытка провести чередование по неправильному аугментальному пути 
        /// (цепь должна начинаться и оканчиваться непокрытыми вершинами, и чередовать непаросочетанное ребро с паросочетанным)
        /// </summary>
        ThirdStageIncorrectAugmentalPath,
        /// <summary>
        /// Неправильная матрица на четвёртом шаге решения
        /// </summary>
        FourthStageIncorrectNextMatrix,
        /// <summary>
        /// Неправильный формат ввода величины стоимости назначения
        /// </summary>
        IncorrectAssignmentCostFormat,
        /// <summary>
        /// Неправильная стоимость назначения
        /// </summary>
        IncorrectAssignmentCost,
    }
}
