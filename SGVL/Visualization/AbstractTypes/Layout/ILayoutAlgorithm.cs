using SGVL.Graphs;

namespace SGVL.Visualization.AbstractTypes.Layout {
    /// <summary>
    /// Интерфейс, представляющий алгоритм укладки графа
    /// </summary>
    public interface ILayoutAlgorithm {
        /// <summary>
        /// Построить укладку для заданного графа
        /// </summary>
        /// <param name="graph">Объект графа</param>
        void BuildGraphLayout(Graph graph);
    }
}

