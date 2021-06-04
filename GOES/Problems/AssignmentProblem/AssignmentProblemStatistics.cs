using System;

namespace GOES.Problems.AssignmentProblem {
    class AssignmentProblemStatistics : IProblemStatistics {
        /// <summary>
        /// Неправильный формат ввода матрицы
        /// </summary>
        public int IncorrectNextMatrixFormatCount { get; set; }
        /// <summary>
        /// Неправильная матрица на первом шаге решения (вычесть из строк минимальные элементы)
        /// </summary>
        public int FirstStageIncorrectNextMatrixCount { get; set; }
        /// <summary>
        /// Неправильная матрица на втором шаге решения (вычесть из столбцов минимальные элементы)
        /// </summary>
        public int SecondStageIncorrectNextMatrixCount { get; set; }
        /// <summary>
        /// Начало построения аугментальной цепи с вершины, покрытой паросочетанием
        /// </summary>
        public int ThirdStageStartOnMatchedVertexCount { get; set; }
        /// <summary>
        /// Попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи
        /// </summary>
        public int ThirdStageMoveToFarVertexCount { get; set; }
        /// <summary>
        /// Нарушение чередования при построении аугментальной цепи - добавление паросочетанного ребра к предыдущему паросочетанному
        /// </summary>
        public int ThirdStageAlternationBreakingOnMatchedEdgeCount { get; set; }
        /// <summary>
        /// Нарушение чередования при построении аугментальной цепи - добавление непаросочетанного ребра к предыдущему непаросочетанному
        /// </summary>
        public int ThirdStageAlternationBreakingOnNotMatchedEdgeCount { get; set; }
        /// <summary>
        /// Попытка провести чередование по недостроенному аугментальному пути (путь из одной вершины или пустой путь)
        /// </summary>
        public int ThirdStageAugmentalPathIsNotFinishedCount { get; set; }
        /// <summary>
        /// Попытка провести чередование по неправильному аугментальному пути 
        /// (цепь должна начинаться и оканчиваться непокрытыми вершинами, и чередовать непаросочетанное ребро с паросочетанным)
        /// </summary>
        public int ThirdStageIncorrectAugmentalPathCount { get; set; }
        /// <summary>
        /// Неправильная матрица на четвёртом шаге решения
        /// </summary>
        public int FourthStageIncorrectNextMatrixCount { get; set; }
        /// <summary>
        /// Неправильный формат ввода величины стоимости назначения
        /// </summary>
        public int IncorrectAssignmentCostFormatCount { get; set; }
        /// <summary>
        /// Неправильная стоимость назначения
        /// </summary>
        public int IncorrectAssignmentCostCount { get; set; }

        public int TotalErrorsCount => IncorrectNextMatrixFormatCount + FirstStageIncorrectNextMatrixCount + SecondStageIncorrectNextMatrixCount +
            ThirdStageStartOnMatchedVertexCount + ThirdStageMoveToFarVertexCount + ThirdStageAlternationBreakingOnMatchedEdgeCount +
            ThirdStageAlternationBreakingOnNotMatchedEdgeCount + ThirdStageAugmentalPathIsNotFinishedCount + ThirdStageIncorrectAugmentalPathCount +
            FourthStageIncorrectNextMatrixCount + IncorrectAssignmentCostFormatCount + IncorrectAssignmentCostCount;

        public int TotalNecessaryErrorsCount =>  FirstStageIncorrectNextMatrixCount + SecondStageIncorrectNextMatrixCount +
            ThirdStageStartOnMatchedVertexCount + ThirdStageMoveToFarVertexCount + ThirdStageAlternationBreakingOnMatchedEdgeCount +
            ThirdStageAlternationBreakingOnNotMatchedEdgeCount + ThirdStageAugmentalPathIsNotFinishedCount + ThirdStageIncorrectAugmentalPathCount +
            FourthStageIncorrectNextMatrixCount + IncorrectAssignmentCostCount;

        public bool IsSolved { get; set; }

        public int Mark {
            get {
                // Подсчитываем все ошибки, кроме форматных
                int necessaryErrors = TotalNecessaryErrorsCount;
                if (necessaryErrors == 0)
                    return 5;
                else if (necessaryErrors <= 3)
                    return 4;
                else if (necessaryErrors <= 6)
                    return 3;
                else
                    return 2;
            }
        }

        public string GetStatisticsText() =>
            "Допущенные ошибки:" + Environment.NewLine +
            Environment.NewLine +
            $"Неправильный формат ввода матрицы: {IncorrectNextMatrixFormatCount}" + Environment.NewLine +
            $"Неправильная матрица на первом шаге решения (вычесть из строк минимальные элементы): {FirstStageIncorrectNextMatrixCount}" + Environment.NewLine +
            $"Неправильная матрица на втором шаге решения (вычесть из столбцов минимальные элементы): {SecondStageIncorrectNextMatrixCount}" + Environment.NewLine +
            $"Начало построения аугментальной цепи с вершины, покрытой паросочетанием: {ThirdStageStartOnMatchedVertexCount}" + Environment.NewLine +
            $"Попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи: {ThirdStageMoveToFarVertexCount}" + Environment.NewLine +
            $"Нарушение чередования при построении аугментальной цепи: {ThirdStageAlternationBreakingOnMatchedEdgeCount + ThirdStageAlternationBreakingOnNotMatchedEdgeCount}" + Environment.NewLine +
            $"Попытка провести чередование по недостроенному аугментальному пути: {ThirdStageAugmentalPathIsNotFinishedCount}" + Environment.NewLine +
            $"Попытка провести чередование по неправильному аугментальному пути: {ThirdStageIncorrectAugmentalPathCount}" + Environment.NewLine +
            $"Неправильная матрица на четвёртом шаге решения: {FourthStageIncorrectNextMatrixCount}" + Environment.NewLine +
            $"Неправильный формат ввода величины стоимости назначения: {IncorrectAssignmentCostFormatCount}" + Environment.NewLine +
            $"Неправильная стоимость назначения: {IncorrectAssignmentCostCount}" +
            Environment.NewLine +
            $"Всего ошибок: {TotalErrorsCount}" + Environment.NewLine +
            $"Из них ошибок, влияющих на оценку: {TotalNecessaryErrorsCount}" + Environment.NewLine +
            Environment.NewLine +
            $"Оценка: {Mark}";
    }
}
