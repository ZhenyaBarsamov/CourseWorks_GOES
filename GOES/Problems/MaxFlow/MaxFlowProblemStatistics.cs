using System;

namespace GOES.Problems.MaxFlow {
    /// <summary>
    /// Класс, представляющий статистику решения задачи о максимальном потоке: счётчики ошибок, и т.д.
    /// </summary>
    class MaxFlowProblemStatistics : IProblemStatistics {
        /// <summary>
        /// Начало построения аугментальной цепи не с истока
        /// </summary>
        public int StartOnNonSourceVertexCount { get; set; }
        /// <summary>
        /// Попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи
        /// </summary>
        public int MoveToFarVertexCount { get; set; }
        /// <summary>
        /// Прямая дуга, по которой ученик хочет пойти, уже имеем максимальный поток
        /// </summary>
        public int ForwardEdgeIsFullCount { get; set; }
        /// <summary>
        /// Обратная дуга, по которой ученик хочет пойти, имеет нулевой поток
        /// </summary>
        public int BackEdgeIsEmptyCount { get; set; }
        /// <summary>
        /// Неправильный формат метки вершины
        /// </summary>
        public int IncorrectVertexLabelFormatCount { get; set; }
        /// <summary>
        /// Неправильная метка вершины
        /// </summary>
        public int IncorrectVertexLabelCount { get; set; }
        /// <summary>
        /// Неправильный формат для значения дополнительного потока
        /// </summary>
        public int IncorrectFlowRaiseFormatCount { get; set; }
        /// <summary>
        /// Неправильная величина дополнительного потока
        /// </summary>
        public int IncorrectFlowRaiseCount { get; set; }
        /// <summary>
        /// Неправильный формат для значения макисмального потока
        /// </summary>
        public int IncorrectMaxFlowFormatCount { get; set; }
        /// <summary>
        /// Неправильная величина максимального потока в сети
        /// </summary>
        public int IncorrectMaxFlowValueCount { get; set; }
        /// <summary>
        /// Выбранная дуга не входит в минимальный разрез сети
        /// </summary>
        public int IncorrectMinCutEdgeCount { get; set; }

        public int TotalErrorsCount => StartOnNonSourceVertexCount + MoveToFarVertexCount + ForwardEdgeIsFullCount + BackEdgeIsEmptyCount +
            IncorrectVertexLabelFormatCount + IncorrectVertexLabelCount + IncorrectFlowRaiseFormatCount + IncorrectFlowRaiseCount +
            IncorrectMaxFlowFormatCount + IncorrectMaxFlowValueCount + IncorrectMinCutEdgeCount;

        public int TotalNecessaryErrorsCount => StartOnNonSourceVertexCount + MoveToFarVertexCount + ForwardEdgeIsFullCount + BackEdgeIsEmptyCount +
            IncorrectVertexLabelCount + IncorrectFlowRaiseCount + IncorrectMaxFlowValueCount + IncorrectMinCutEdgeCount;

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
            $"Начало построения аугментальной цепи не с истока: {StartOnNonSourceVertexCount}" + Environment.NewLine +
            $"Попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи: {MoveToFarVertexCount}" + Environment.NewLine +
            $"Прямая дуга, по которой ученик хочет пойти, уже имеем максимальный поток: {ForwardEdgeIsFullCount}" + Environment.NewLine +
            $"Обратная дуга, по которой ученик хочет пойти, имеет нулевой поток: {BackEdgeIsEmptyCount}" + Environment.NewLine +
            $"Неправильный формат метки вершины: {IncorrectVertexLabelFormatCount}" + Environment.NewLine +
            $"Неправильная метка вершины: {IncorrectVertexLabelCount}" + Environment.NewLine +
            $"Неправильный формат для значения дополнительного потока: {IncorrectFlowRaiseFormatCount}" + Environment.NewLine +
            $"Неправильная величина дополнительного потока: {IncorrectFlowRaiseCount}" + Environment.NewLine +
            $"Неправильный формат для значения макисмального потока: {IncorrectMaxFlowFormatCount}" + Environment.NewLine +
            $"Неправильная величина максимального потока в сети: {IncorrectMaxFlowValueCount}" + Environment.NewLine +
            $"Выбранная дуга не входит в минимальный разрез сети: {IncorrectMinCutEdgeCount}" + Environment.NewLine +
            Environment.NewLine +
            $"Всего ошибок: {TotalErrorsCount}" + Environment.NewLine +
            $"Из них ошибок, влияющих на оценку: {TotalNecessaryErrorsCount}" + Environment.NewLine +
            Environment.NewLine +
            $"Оценка: {Mark}";
    }
}
