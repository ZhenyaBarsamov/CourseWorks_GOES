namespace SGVL.Types.Graphs {
    /// <summary>
    /// Делегат для обработчика события изменения ребра
    /// </summary>
    /// <param name="edge">Вызвавшая событие вершина</param>
    public delegate void EdgeChaingedEventHandler(Edge edge);

    /// <summary>
    /// Делегат для обработчика события изменения вершины
    /// </summary>
    /// <param name="vertex">Вызвавшая событие вершина</param>
    public delegate void VertexChaingedEventHandler(Vertex vertex);

    /// <summary>
    /// Делегат для обработчика события изменения графа
    /// </summary>
    /// <param name="graph">Вызвавший событие граф</param>
    public delegate void GraphChaingedEventHandler(Graph graph);
}
