using System;
using System.Drawing;

namespace GOES.Problems.MaxBipartiteMatching {
    /// <summary>
    /// Статический класс, реализующий метод случайной генерации примера для задачи о максимальном паросочетании
    /// </summary>
    static class ExampleGenerator {
        // Для двудольного графа мы генерируем всю матрицу смежности. При этом
        // считаем, что чётные вершины (0, 2, 4) - это вершины первой доли, а нечётные (1, 3, 5) - вершины второй доли.

        /// <summary>
        /// Минимальная размерность задачи о максимальном паросочетании (столько пар вершин)
        /// </summary>
        private static readonly int minDimension = 3;
        /// <summary>
        /// Максимальная размерность задачи о максимальном паросочетании (столько пар вершин)
        /// </summary>
        private static readonly int maxDimension = 5;

        /// <summary>
        /// Предопределённые координаты вершин для сгенерированных графов
        /// </summary>
        private static readonly PointF[] verticesCoords = new PointF[] {
            new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
            new PointF(75, 250), new PointF(425, 250), new PointF(75, 350), new PointF(425, 350),
            new PointF(75, 450), new PointF(425, 450), new PointF(75, 550), new PointF(425, 550)
        };

        /// <summary>
        /// Сгенерировать случайный пример задачи о максимальном паросочетании
        /// </summary>
        public static MaxBipartiteMatchingProblemExample GenerateExample() {
            Random rand = new Random();
            int dimension = rand.Next(minDimension, maxDimension + 1);
            int verticesCount = dimension * 2;
            int[,] graphMatrix = new int[verticesCount, verticesCount];
            // Заполняем матрицу смежности случайным образом, так, чтобы граф был двудольным
            // При этом следим, чтобы не было изолированных вершин первой доли
            byte[] randByte = new byte[1];
            for (int row = 0; row < verticesCount; row += 2) {
                bool isConnected = false;
                for (int col = 1; col < verticesCount; col += 2) {
                    rand.NextBytes(randByte);
                    int val = randByte[0] >= 128 ? 1 : 0;
                    if (val != 0)
                        isConnected = true;
                    graphMatrix[row, col] = graphMatrix[col, row] = val;
                }
                // Если получилось так, что вершина первой доли (row) осталась изолированной, соединяем её со случайной вершиной второй доли
                if (!isConnected) {
                    int col = 1 + rand.Next(dimension) * 2;
                    graphMatrix[row, col] = graphMatrix[col, row] = 1;
                }
            }
            // После заполнения матрицы проверяем, есть ли изолированные вершины второй доли
            for (int col = 1; col < verticesCount; col += 2) {
                bool isConnected = false;
                for (int row = 0; row < verticesCount; row += 2) {
                    if (graphMatrix[row, col] != 0)
                        isConnected = true;
                }
                // Если получилось так, что вершина второй доли (col) осталась изолированной, соединяем её со случайной вершиной первой доли
                if (!isConnected) {
                    int row = 0 + rand.Next(dimension) * 2;
                    graphMatrix[row, col] = graphMatrix[col, row] = 1;
                }
            }
            PointF[] graphLayout = new PointF[verticesCount];
            Array.Copy(verticesCoords, graphLayout, verticesCount);
            return new MaxBipartiteMatchingProblemExample("Случайный пример", $"Двудольный граф из {verticesCount} вершин",
                graphMatrix, graphLayout);
        }
    }
}
