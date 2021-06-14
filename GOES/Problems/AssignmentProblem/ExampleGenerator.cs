using System;
using System.Drawing;

namespace GOES.Problems.AssignmentProblem {
    /// <summary>
    /// Статический класс, реализующий метод случайной генерации примера для задачи о назначениях
    /// </summary>
    static class ExampleGenerator {
        // Для двудольного графа мы генерируем всю матрицу смежности. При этом
        // считаем, что чётные вершины (0, 2, 4) - это вершины первой доли, а нечётные (1, 3, 5) - вершины второй доли.

        /// <summary>
        /// Минимальная размерность задачи о назначениях (столько пар вершин "работник"-"работа")
        /// </summary>
        private static readonly int minDimension = 3;
        /// <summary>
        /// Максимальная размерность задачи о назначениях (столько пар вершин "работник"-"работа")
        /// </summary>
        private static readonly int maxDimension = 6;
        /// <summary>
        /// Минимально возможная стоимость работы
        /// </summary>
        private static readonly int minCost = 1;
        /// <summary>
        /// Максимально возможная стоимость работы
        /// </summary>
        private static readonly int maxCost = 25;

        /// <summary>
        /// Предопределённые координаты вершин для сгенерированных графов
        /// </summary>
        private static readonly PointF[] verticesCoords = new PointF[] {
            new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
            new PointF(75, 250), new PointF(425, 250), new PointF(75, 350), new PointF(425, 350),
            new PointF(75, 450), new PointF(425, 450), new PointF(75, 550), new PointF(425, 550)
        };

        /// <summary>
        /// Сгенерировать случайный пример задачи о назначениях
        /// </summary>
        public static AssignmentProblemExample GenerateExample() {
            Random rand = new Random();
            int dimension = rand.Next(minDimension, maxDimension + 1);
            int verticesCount = dimension * 2;
            int[,] costMatrix = new int[verticesCount, verticesCount];
            // Заполняем матрицу случайными стоимостями от minCost до maxCost
            // Граф двудольный, создаём рёбра между всеми парами вершин (полный двудольный граф)
            for (int row = 0; row < verticesCount; row += 2) {
                for (int col = 1; col < verticesCount; col += 2) {
                    costMatrix[row, col] = costMatrix[col, row] = rand.Next(minCost, maxCost + 1);
                }
            }
            PointF[] graphLayout = new PointF[verticesCount];
            Array.Copy(verticesCoords, graphLayout, verticesCount);
            return new AssignmentProblemExample("Случайный пример", $"{dimension} работников и {dimension} работ", 
                costMatrix, graphLayout);
        }
    }
}
