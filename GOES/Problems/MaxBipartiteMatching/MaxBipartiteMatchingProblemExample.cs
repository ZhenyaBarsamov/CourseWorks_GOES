using System.Drawing;

namespace GOES.Problems.MaxBipartiteMatching {
    /// <summary>
    /// Класс, предназначенный для хранения постановки задачи о максимальном паросочетании в двудольном графе
    /// </summary>
    public class MaxBipartiteMatchingProblemExample : ProblemExample {
        // ----Атрибуты
        /// <summary>
        /// Матрица смежности графа (нечётные вершины - вершины первой доли, чётные вершины - вершины второй доли)
        /// </summary>
        public bool[,] GraphMatrix { get; private set; }


        // ----Конструктор
        /// <summary>
        /// Конструктор, создающий экземпляр постановки задачи о максимальном потоке и минимальном разрезе
        /// </summary>
        /// <param name="name">Название постановки задачи</param>
        /// <param name="description">Описание постановки задачи</param>
        /// <param name="graphMatrix">Матрица смежности графа, содержащая 1, если соответствующее ребро существует, и 0 - иначе</param>
        /// <param name="defaultGraphLayout">Массив точек, задающих расположение вершин графа по умолчанию</param>
        public MaxBipartiteMatchingProblemExample(string name, string description, int[,] graphMatrix,
            PointF[] defaultGraphLayout) : base(name, description, false, defaultGraphLayout) {
            int verticesCount = graphMatrix.GetLength(0);
            GraphMatrix = new bool[verticesCount, verticesCount];
            for (int row = 0; row < verticesCount; row++)
                for (int col = 0; col < verticesCount; col++)
                    GraphMatrix[row, col] = graphMatrix[row, col] == 1;
        }
    }
}
