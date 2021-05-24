namespace GOES.Problems.MaxFlow {
    /// <summary>
    /// Перечисление для ошибок, которые могут быть допущены учеником 
    /// при решении задачи о максимальном потоке
    /// </summary>
    enum MaxFlowError {
        /// <summary>
        /// Начало построения аугментальной цепи не с истока
        /// </summary>
        StartOnNonSourceVertex,
        /// <summary>
        /// Попытка добавить к аугментальной цепи вершину, не соединённую с последней вершиной цепи
        /// </summary>
        MoveToFarVertex,
        /// <summary>
        /// Прямая дуга, по которой ученик хочет пойти, уже имеем максимальный поток
        /// </summary>
        ForwardEdgeIsFull,
        /// <summary>
        /// Обратная дуга, по которой ученик хочет пойти, имеет нулевой поток
        /// </summary>
        BackEdgeIsEmpty,
        /// <summary>
        /// Неправильный формат метки вершины
        /// </summary>
        IncorrectVertexLabelFormat,
        /// <summary>
        /// Неправильная метка вершины
        /// </summary>
        IncorrectVertexLabel,
        /// <summary>
        /// Неправильный формат для значения дополнительного потока
        /// </summary>
        IncorrectFlowRaiseFormat,
        /// <summary>
        /// Неправильная величина дополнительного потока
        /// </summary>
        IncorrectFlowRaise,
        /// <summary>
        /// Неправильный формат для значения макисмального потока
        /// </summary>
        IncorrectMaxFlowFormat,
        /// <summary>
        /// Неправильная величина максимального потока в сети
        /// </summary>
        IncorrectMaxFlowValue,
        /// <summary>
        /// Выбранная дуга не входит в минимальный разрез сети
        /// </summary>
        IncorrectMinCutEdge
    }
}
