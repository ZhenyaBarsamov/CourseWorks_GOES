using System.Drawing;

namespace GOES.Problems.AssignmentProblem {
    public class AssignmentProblemExample : ProblemExample {
        // ----Атрибуты
        /// <summary>
        /// Полная матрица смежности графа, содержащая стоимости назначений (нечётные вершины - вершины первой доли, чётные вершины - вершины второй доли)
        /// </summary>
        public int[,] CostsMatrix { get; private set; }

        // ----Свойства
        /// <summary>
        /// Матрица смежности графа, вместо стоимостей хранящая признаки существования рёбер
        /// </summary>
        public bool[,] GraphMatrix {
            get {
                var graphMatrix = new bool[CostsMatrix.GetLength(0), CostsMatrix.GetLength(1)];
                for (int row = 0; row < CostsMatrix.GetLength(0); row++)
                    for (int col = 0; col < CostsMatrix.GetLength(1); col++)
                        graphMatrix[row, col] = CostsMatrix[row, col] != 0;
                return graphMatrix;
            }
        }


        // ----Конструктор
        /// <summary>
        /// Конструктор, создающий экземпляр постановки задачи о максимальном потоке и минимальном разрезе
        /// </summary>
        /// <param name="name">Название постановки задачи</param>
        /// <param name="description">Описание постановки задачи</param>
        /// <param name="costMatrix">Матрица смежности графа, т.е. матрица стоимостей работ 
        /// (нечётные вершины - работники, чётные вершины - работы)</param>
        /// <param name="defaultGraphLayout">Массив точек, задающих расположение вершин графа по умолчанию</param>
        public AssignmentProblemExample(string name, string description, int[,] costMatrix,
            PointF[] defaultGraphLayout) : base(name, description, false, defaultGraphLayout) {
            int verticesCount = costMatrix.GetLength(0);
            CostsMatrix = new int[verticesCount, verticesCount];
            for (int row = 0; row < verticesCount; row++)
                for (int col = 0; col < verticesCount; col++)
                    CostsMatrix[row, col] = costMatrix[row, col];
        }
    }
}
