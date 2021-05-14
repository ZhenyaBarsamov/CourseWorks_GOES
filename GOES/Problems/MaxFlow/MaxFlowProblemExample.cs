using System.Drawing;

namespace GOES.Problems.MaxFlow {
    /// <summary>
    /// Класс, предназначенный для хранения примера задачи о максимальном потоке и минимальном разрезе
    /// </summary>
    public class MaxFlowProblemExample : ProblemExample {
        // ----Атрибуты
        /// <summary>
        /// Матрица пропускных способностей сети
        /// </summary>
        public int[,] CapacityMatrix { get; private set; }
        /// <summary>
        /// Индекс вершины-истока
        /// </summary>
        public int SourceVertexIndex { get; private set; }
        /// <summary>
        /// Индекс вершины-стока
        /// </summary>
        public int TargetVertexIndex { get; private set; }


        // ----Свойства
        /// <summary>
        /// Матрица смежности графа, вместо пропускных способностей дуг хранящая признаки их существования
        /// </summary>
        public bool[,] GraphMatrix {
            get {
                var graphMatrix = new bool[CapacityMatrix.GetLength(0), CapacityMatrix.GetLength(1)];
                for (int row = 0; row < CapacityMatrix.GetLength(0); row++)
                    for (int col = 0; col < CapacityMatrix.GetLength(1); col++)
                        graphMatrix[row, col] = CapacityMatrix[row, col] != 0;
                return graphMatrix;
            }
        }


        // ----Конструктор
        /// <summary>
        /// Конструктор, создающий экземпляр примера задачи о максимальном потоке и минимальном разрезе
        /// </summary>
        /// <param name="name">Название примера</param>
        /// <param name="description">Описание примера</param>
        /// <param name="sourceIndex">Индекс вершины-истока</param>
        /// <param name="targetIndex">Индекс вершины-стока</param>
        /// <param name="capacityMatrix">Матрица пропускных способностей сети</param>
        /// <param name="defaultGraphLayout">Массив точек, задающих расположение вершин графа по умолчанию</param>
        public MaxFlowProblemExample(string name, string description, int sourceIndex, int targetIndex, int[,] capacityMatrix, 
            PointF[] defaultGraphLayout) : base(name, description, true, defaultGraphLayout) {
            CapacityMatrix = capacityMatrix;
            SourceVertexIndex = sourceIndex;
            TargetVertexIndex = targetIndex;
        }
    }
}
