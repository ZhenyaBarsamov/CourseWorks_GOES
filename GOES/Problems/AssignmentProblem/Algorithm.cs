using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BipartiteMatching = GOES.Problems.MaxBipartiteMatching;

namespace GOES.Problems.AssignmentProblem {
    static class Algorithm {
        /// <summary>
        /// Получить размерности матрицы смежности двудольного графа по количеству вершин графа.
        /// Считается, что в полной матрице смежности нечётные вершины принадлежат первой доле, а чётные - второй
        /// </summary>
        /// <param name="verticesCount">Количество вершин графа</param>
        /// <param name="rowsCount">Результат - количество строк в матрице смежности двудольного графа</param>
        /// <param name="colsCount">Результат - количество столбцов в матрице смежности двудольного графа</param>
        public static void GetCostsMatrixDimensions(int verticesCount, out int rowsCount, out int colsCount) {
            rowsCount = verticesCount / 2 + verticesCount % 2;
            colsCount = verticesCount / 2;
        }

        /// <summary>
        /// Получить индекс вершины первой доли в полной матрице смежности по индексу строки в матрице смежности двудольного графа
        /// </summary>
        /// <param name="row">Индекс строки (вершины первой доли) в матрице смежности двудольного графа</param>
        /// <param name="rowInAdjacencyMatrix">Результат - индекс вершины в полной матрице смежности графа</param>
        private static void GetAdjacencyMatrixRowIndex(int row, out int rowInAdjacencyMatrix) {
            rowInAdjacencyMatrix = row * 2;
        }

        /// <summary>
        /// Получить индекс вершины второй доли в полной матрице смежности по индексу столбца в матрице смежности двудольного графа
        /// </summary>
        /// <param name="col">Индекс столбца (вершины второй доли) в матрице смежности двудольного графа</param>
        /// <param name="colInAdjacencyMatrix">Результат - индекс вершины в полной матрице смежности графа</param>
        private static void GetAdjacencyMatrixColIndex(int col, out int colInAdjacencyMatrix) {
            colInAdjacencyMatrix = col * 2 + 1;
        }

        /// <summary>
        /// Получить индексы вершин первой и второй доли в полной матрице смежности графа по их индексам в матрице смежности двудольного графа
        /// </summary>
        /// <param name="row">Индекс строки (вершины первой доли) в матрице смежности двудольного графа</param>
        /// <param name="col">Индекс столбца (вершины второй доли) в матрице смежности двудольного графа</param>
        /// <param name="rowInAdjacencyMatrix">Результат - индекс вершины первой доли в полной матрице смежности графа</param>
        /// <param name="colInAdjacencyMatrix">Результат - индекс вершины второй доли в полной матрице смежности графа</param>
        public static void GetAdjacencyMatrixIndices(int row, int col, out int rowInAdjacencyMatrix, out int colInAdjacencyMatrix) {
            GetAdjacencyMatrixRowIndex(row, out rowInAdjacencyMatrix);
            GetAdjacencyMatrixColIndex(col, out colInAdjacencyMatrix);
        }

        /// <summary>
        /// По индексам строки и столбца в матрице смежности двудольного графа получить значение из полной матрицы смежности графа
        /// </summary>
        /// <param name="adjacencyMatrix">Полная матрица смежности графа</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        /// <param name="row">Индекс строки (вершины первой доли) в матрице смежности двудольного графа</param>
        /// <param name="col">Индекс столбца (вершины второй доли) в матрице смежности двудольного графа</param>
        /// <returns></returns>
        public static int GetCostFromAdjacencyMatrix(int[,] adjacencyMatrix, int verticesCount, int row, int col) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            if (row < 0 || row >= rowsCount || col < 0 || col >= colsCount)
                throw new ArgumentException("Неправильные индексы");
            GetAdjacencyMatrixIndices(row, col, out int rowInAdjacencyMatrix, out int colInAdjacencyMatrix);
            return adjacencyMatrix[rowInAdjacencyMatrix, colInAdjacencyMatrix];
        }

        /// <summary>
        /// По индексам строки и столбца в матрице смежности двудольного графа присвоить значение в полную матрицу смежности графа
        /// </summary>
        /// <param name="adjacencyMatrix">Полная матрица смежности графа</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        /// <param name="row">Индекс строки (вершины первой доли) в матрице смежности двудольного графа</param>
        /// <param name="col">Индекс столбца (вершины второй доли) в матрице смежности двудольного графа</param>
        /// <param name="value">Присваиваемое значение</param>
        public static void SetCostToAdjacencyMatrix(int[,] adjacencyMatrix, int verticesCount, int row, int col, int value) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            if (row < 0 || row >= rowsCount || col < 0 || col >= colsCount)
                throw new ArgumentException("Неправильные индексы");
            GetAdjacencyMatrixIndices(row, col, out int rowInAdjacencyMatrix, out int colInAdjacencyMatrix);
            adjacencyMatrix[rowInAdjacencyMatrix, colInAdjacencyMatrix] =
                adjacencyMatrix[colInAdjacencyMatrix, rowInAdjacencyMatrix] = value;
        }

        /// <summary>
        /// Преобразовать полную матрицу смежности графа в строку в виде матрицы смежности двудольного графа
        /// </summary>
        /// <param name="adjacencyMatrix">Полная матрица смежности графа</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        /// <returns></returns>
        public static string GetCostsMatrixString(int[,] adjacencyMatrix, int verticesCount) {
            StringBuilder strBuilder = new StringBuilder();
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            for (int row = 0; row < rowsCount; row++) {
                for (int col = 0; col < colsCount; col++)
                    strBuilder.Append($"{GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col)} ");
                strBuilder.Remove(strBuilder.Length - 1, 1);
                strBuilder.Append(Environment.NewLine);
            }
            strBuilder.Remove(strBuilder.Length - 1, 1);
            return strBuilder.ToString();
        }

        /// <summary>
        /// По индексу вершины первой доли (строки матрицы смежности двудольного графа) получить индекс вершины второй доли, парной ей (или -1, если пары нет)
        /// </summary>
        /// <param name="matchingPairsArray">Массив, по индексу вершины (индекс вершины в полной матрице смежности) хранящий индекс её пары (так же).
        /// Если пары нет, хранится -1</param>
        /// <param name="row">Индекс вершины первой доли (индекс строки матрицы смежности двудольного графа)</param>
        /// <returns></returns>
        public static int GetPairFromMatchingPairsArray(int[] matchingPairsArray, int row) {
            GetAdjacencyMatrixRowIndex(row, out int rowInAdjacecnyMatrix);
            GetAdjacencyMatrixColIndex(matchingPairsArray[rowInAdjacecnyMatrix], out int colInAdjacecnyMatrix);
            return colInAdjacecnyMatrix;
        }

        /// <summary>
        /// Первый шаг решения задачи о назначениях - вычесть из каждой строки матрицы смежности двудольного графа её минимальное значение
        /// </summary>
        /// <param name="adjacencyMatrix">Полная матрица смежности графа</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        public static void FirstStage(int[,] adjacencyMatrix, int verticesCount) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            // Находим минимальную стоимость в каждой строке и вычитаем её из этой строки
            for (int row = 0; row < rowsCount; row++) {
                // Находим минимум строки
                int rowMin = int.MaxValue;
                for (int col = 0; col < colsCount; col++)
                    if (GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col) < rowMin)
                        rowMin = GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                // И вычитаем его
                if (rowMin != 0)
                    for (int col = 0; col < colsCount; col++) {
                        int prevCost = GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                        SetCostToAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col, prevCost - rowMin);
                    }
            }
        }

        /// <summary>
        /// Второй шаг решения задачи о назначениях - вычесть из каждого столбца его минимальное значение
        /// </summary>
        /// <param name="adjacencyMatrix">Полная матрица смежности графа</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        public static void SecondStage(int[,] adjacencyMatrix, int verticesCount) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            // Находим минимальную стоимость в каждом столбце и вычитаем её из этого столбца
            for (int col = 0; col < colsCount; col++) {
                // Находим минимум столбца
                int colMin = int.MaxValue;
                for (int row = 0; row < rowsCount; row++)
                    if (GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col) < colMin)
                        colMin = GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                // И вычитаем его
                if (colMin != 0)
                    for (int row = 0; row < rowsCount; row++) {
                        int prevCost = GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                        SetCostToAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col, prevCost - colMin);
                    };
            }
        }

        /// <summary>
        /// Получить стоимость произведённого назначения, заданного матрицей пар
        /// </summary>
        /// <param name="adjacencyMatrix">Полная матрица смежности графа, содержащая стоимости назначений</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        /// <param name="matchingPairsArray">Массив, по индексу вершины (индекс вершины в полной матрице смежности) хранящий индекс её пары (так же).
        /// <returns></returns>
        public static int GetCostOfAssignment(int[,] adjacencyMatrix, int verticesCount, int[] matchingPairsArray) {
            int cost = 0;
            for (int leftPartVertex = 0; leftPartVertex < verticesCount; leftPartVertex += 2)
                if (matchingPairsArray[leftPartVertex] != -1) {
                    int rightPartVertex = matchingPairsArray[leftPartVertex];
                    cost += adjacencyMatrix[leftPartVertex, rightPartVertex];
                }
            return cost;
        }

        public static int[] GetMaximalMatching(int[,] adjacencyMatrix, int verticesCount) {
            bool[,] graphMatrix = new bool[verticesCount, verticesCount];
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            for (int row = 0; row < rowsCount; row++) {
                GetAdjacencyMatrixRowIndex(row, out int rowInAdjacencyMatrix);
                for (int col = 0; col < colsCount; col++) {
                    GetAdjacencyMatrixColIndex(col, out int colInAdjacencyMatrix);
                    if (adjacencyMatrix[rowInAdjacencyMatrix, colInAdjacencyMatrix] == 0)
                        graphMatrix[rowInAdjacencyMatrix, colInAdjacencyMatrix] = true;
                }
            }
            var matchingPairsArray = BipartiteMatching.Algorithm.GetMaximalMatching(graphMatrix, verticesCount);
            int matchingCardinality = BipartiteMatching.Algorithm.GetMatchingCardinality(matchingPairsArray);
            return matchingPairsArray;
        }
    }
}
