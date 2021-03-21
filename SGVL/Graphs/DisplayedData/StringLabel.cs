namespace SGVL.Graphs.DisplayedData {
    /// <summary>
    /// Простая строковая метка, которую можно приписать к вершине или ребру графа
    /// </summary>
    class StringLabel : IDisplayedData {
        /// <summary>
        /// Значение метки
        /// </summary>
        public string Label { get; set; }

        public string ToDisplayedText() {
            return Label;
        }
    }
}
