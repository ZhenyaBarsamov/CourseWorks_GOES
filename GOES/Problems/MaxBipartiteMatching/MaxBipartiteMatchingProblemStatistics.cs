using System;

namespace GOES.Problems.MaxBipartiteMatching {
    /// <summary>
    /// Класс, представляющий статистику решения задачи о максимальном паросочетании в двудольном графе: 
    /// счётчики ошибок, и т.д.
    /// </summary>
    class MaxBipartiteMatchingProblemStatistics : IProblemStatistics {
        /// <summary>
        /// Начало построения аугментальной цепи с вершины, покрытой паросочетанием
        /// </summary>
        public int StartOnMatchedVertexCount { get; set; }
        /// <summary>
        /// Попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи
        /// </summary>
        public int MoveToFarVertexCount { get; set; }
        /// <summary>
        /// Нарушение чередования при построении аугментальной цепи - добавление паросочетанного ребра к предыдущему паросочетанному
        /// </summary>
        public int AlternationBreakingOnMatchedEdgeCount { get; set; }
        /// <summary>
        /// Нарушение чередования при построении аугментальной цепи - добавление непаросочетанного ребра к предыдущему непаросочетанному
        /// </summary>
        public int AlternationBreakingOnNotMatchedEdgeCount { get; set; }
        /// <summary>
        /// Попытка провести чередование по недостроенному аугментальному пути (путь из одной вершины или пустой путь)
        /// </summary>
        public int AugmentalPathIsNotFinishedCount { get; set; }
        /// <summary>
        /// Попытка провести чередование по неправильному аугментальному пути 
        /// (цепь должна начинаться и оканчиваться непокрытыми вершинами, и чередовать непаросочетанное ребро с паросочетанным)
        /// </summary>
        public int IncorrectAugmentalPathCount { get; set; }
        /// <summary>
        /// Неправильный формат для значения мощности максимального паросочетания
        /// </summary>
        public int IncorrectMaxMatchingCardinalityFormatCount { get; set; }
        /// <summary>
        /// Неправильное значение мощности максимального паросочетания
        /// </summary>
        public int IncorrectMaxMatchingCardinalityCount { get; set; }

        public int TotalErrorsCount => StartOnMatchedVertexCount + MoveToFarVertexCount + AlternationBreakingOnMatchedEdgeCount + AlternationBreakingOnNotMatchedEdgeCount +
            AugmentalPathIsNotFinishedCount + IncorrectAugmentalPathCount + IncorrectMaxMatchingCardinalityFormatCount + IncorrectMaxMatchingCardinalityCount;

        public int TotalNecessaryErrorsCount => StartOnMatchedVertexCount + MoveToFarVertexCount + AlternationBreakingOnMatchedEdgeCount + AlternationBreakingOnNotMatchedEdgeCount +
            AugmentalPathIsNotFinishedCount + IncorrectAugmentalPathCount + IncorrectMaxMatchingCardinalityCount;

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
            $"Начало построения аугментальной цепи с вершины, покрытой паросочетанием: {StartOnMatchedVertexCount}" + Environment.NewLine +
            $"Попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи: {MoveToFarVertexCount}" + Environment.NewLine +
            $"Нарушение чередования при построении аугментальной цепи: {AlternationBreakingOnMatchedEdgeCount + AlternationBreakingOnNotMatchedEdgeCount}" + Environment.NewLine +
            $"Попытка провести чередование по недостроенному аугментальному пути: {AugmentalPathIsNotFinishedCount}" + Environment.NewLine +
            $"Попытка провести чередование по неправильному аугментальному пути: {IncorrectAugmentalPathCount}" + Environment.NewLine +
            $"Неправильный формат для значения мощности максимального паросочетания: {IncorrectMaxMatchingCardinalityFormatCount}" + Environment.NewLine +
            $"Неправильное значение мощности максимального паросочетания: {IncorrectMaxMatchingCardinalityCount}" + Environment.NewLine +
            Environment.NewLine +
            $"Всего ошибок: {TotalErrorsCount}" + Environment.NewLine +
            $"Из них ошибок, влияющих на оценку: {TotalNecessaryErrorsCount}" + Environment.NewLine +
            Environment.NewLine +
            $"Оценка: {Mark}";
    }
}
