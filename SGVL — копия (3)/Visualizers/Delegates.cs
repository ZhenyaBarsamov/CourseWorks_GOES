using SGVL.Graphs;

namespace SGVL.Visualizers {
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

    /// <summary>
    /// Делегат для события изменения настроек визуализации
    /// </summary>
    public delegate void DrawingSettingsChangedEventHandler(DrawingSettings drawingSettings);
}
