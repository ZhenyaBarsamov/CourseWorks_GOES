using SGVL.Types.Graphs;

namespace SGVL.Types.Visualizers {
    /// <summary>
    /// Делегат для события выбора вершины
    /// </summary>
    /// <param name="selectedEdge">Выбранная вершина</param>
    public delegate void EdgeSelectedEventHandler(Edge selectedEdge);

    /// <summary>
    /// Делегат для события выбора ребра
    /// </summary>
    /// <param name="selectedVertex">Выбранное ребро</param>
    public delegate void VertexSelectedEventHandler(Vertex selectedVertex);
}
