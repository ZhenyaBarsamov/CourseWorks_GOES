namespace SGVL.Graphs.DisplayedData {
    /// <summary>
    /// Интерфейс, который должен реализовывать класс данных,
    /// размещаемых в вершине или ребре графа
    /// </summary>
    public interface IDisplayedData {
        /// <summary>
        /// Метод, преобразовывающий данные объекта в текст для отображения
        /// </summary>
        /// <returns>Текстовое представление данных</returns>
        string ToDisplayedText();
    }
}
