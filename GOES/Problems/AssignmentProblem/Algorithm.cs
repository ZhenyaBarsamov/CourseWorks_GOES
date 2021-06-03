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
        public static void GetAdjacencyMatrixRowIndex(int row, out int rowInAdjacencyMatrix) {
            rowInAdjacencyMatrix = row * 2;
        }

        /// <summary>
        /// Получить индекс вершины второй доли в полной матрице смежности по индексу столбца в матрице смежности двудольного графа
        /// </summary>
        /// <param name="col">Индекс столбца (вершины второй доли) в матрице смежности двудольного графа</param>
        /// <param name="colInAdjacencyMatrix">Результат - индекс вершины в полной матрице смежности графа</param>
        public static void GetAdjacencyMatrixColIndex(int col, out int colInAdjacencyMatrix) {
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

        public static int[] GetMaximalMatching(bool[,] adjacencyMatrix, int verticesCount) {
            var matchingPairsArray = BipartiteMatching.Algorithm.GetMaximalMatching(adjacencyMatrix, verticesCount);
            return matchingPairsArray;
        }

        public static void AlternateOnAugmentalPath(List<int> augmentalPath, int[] matchingPairsArray) {
            BipartiteMatching.Algorithm.AlternateOnAugmentalPath(augmentalPath, matchingPairsArray);
        }

        /// <summary>
        /// Получить мощность паросочетания, заданного массивом пар вершин
        /// </summary>
        /// <param name="matchingPairsArray"></param>
        /// <returns></returns>
        public static int GetMatchingCardinality(int[] matchingPairsArray) {
            return BipartiteMatching.Algorithm.GetMatchingCardinality(matchingPairsArray);
        }

        public static HashSet<Tuple<int, int>> GetRedZeroes(int[] matchingPairsArray, int verticesCount) {
            // Аккуратней с кортежом во множестве. Сравнение кортежей, кажется, появилось в C# 7.3
            var res = new HashSet<Tuple<int, int>>();
            for (int leftPairIndex = 0; leftPairIndex < verticesCount; leftPairIndex+=2)
                if (matchingPairsArray[leftPairIndex] != -1) {
                    int row = leftPairIndex / 2;
                    int col = matchingPairsArray[leftPairIndex] / 2;
                    res.Add(new Tuple<int, int>(row, col));
                }
            return res;
        }

        public static HashSet<int> GetRowsWithoutRedZeroes(HashSet<Tuple<int, int>> redZeroesCoords, int verticesCount) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            var res = new HashSet<int>();
            for (int row = 0; row < rowsCount; row++)
                res.Add(row);
            var redZeroesRows = new HashSet<int>();
            foreach (var redZeroCoords in redZeroesCoords)
                redZeroesRows.Add(redZeroCoords.Item1);
            res.ExceptWith(redZeroesRows);
            return res;
        }

        public static HashSet<int> GetColsWithZeroesInMarkedRows(int[,] adjacencyMatrix, int verticesCount, HashSet<int> rowsWithoutRedZeroesCoords) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            var res = new HashSet<int>();
            for (int col = 0; col < colsCount; col++)
                foreach (var row in rowsWithoutRedZeroesCoords)
                    if (GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col) == 0)
                        res.Add(col);
            return res;
        }

        public static HashSet<int> GetRowsWithRedZeroesInMarkedCols(int[,] adjacencyMatrix, int verticesCount, HashSet<int> markedCols, HashSet<Tuple<int, int>> redZeroes) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            var res = new HashSet<int>();
            for (int row = 0; row < rowsCount; row++)
                foreach (var col in markedCols)
                    if (redZeroes.Contains(new Tuple<int, int>(row, col)))
                        res.Add(row);
            return res;
        }

        public static void FourthStage(int[,] adjacencyMatrix, int[] matchingPairsArray, int verticesCount) {
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            if (rowsCount == GetMatchingCardinality(matchingPairsArray))
                return;
            // Получаем индексы красных нулей
            var redZeroes = GetRedZeroes(matchingPairsArray, verticesCount);
            // Отмечаем строки, в которых нет красных нулей
            var rowsWithoutRedZeroes = GetRowsWithoutRedZeroes(redZeroes, verticesCount);
            // Отмечаем все столбцы с нулями в этих строках
            var colsWithZeroesInMarkedRows = GetColsWithZeroesInMarkedRows(adjacencyMatrix, verticesCount, rowsWithoutRedZeroes);
            // Отмечаем все строки с красными нулями в этих столбцах
            var rowsWithRedZeroesInMarkedCols = GetRowsWithRedZeroesInMarkedCols(adjacencyMatrix, verticesCount, colsWithZeroesInMarkedRows, redZeroes);
            // Вычёркиваем все отмеченные столбцы и все неотмеченные строки
            var markedRows = new HashSet<int>(rowsWithoutRedZeroes.Union(rowsWithRedZeroesInMarkedCols));
            var markedCols = new HashSet<int>(colsWithZeroesInMarkedRows);
            // Ищем минимальный невычеркнутый элемент
            int min = int.MaxValue;
            for (int row = 0; row < rowsCount; row++) {
                if (!markedRows.Contains(row))
                    continue;
                for (int col = 0; col < colsCount; col++) {
                    if (markedCols.Contains(col))
                        continue;
                    int cost = GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                    if (cost < min)
                        min = cost;
                }
            }
            // Вычитаем найденный минимум из всех невычеркнутых элементов
            for (int row = 0; row < rowsCount; row++) {
                if (!markedRows.Contains(row))
                    continue;
                for (int col = 0; col < colsCount; col++) {
                    if (markedCols.Contains(col))
                        continue;
                    int cost = GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                    SetCostToAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col, cost - min);
                }
            }
            // Прибавляем найденный минимум к элементам, стоящим на пересечении линий
            for (int row = 0; row < rowsCount; row++) {
                if (markedRows.Contains(row))
                    continue;
                for (int col = 0; col < colsCount; col++) {
                    if (!markedCols.Contains(col))
                        continue;
                    int cost = GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                    SetCostToAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col, cost + min);
                }
            }
        }

        public static bool[,] getMatchingGraphAdjacencyMatrix(int[,] adjacencyMatrix, int verticesCount) {
            bool[,] matchingGraphAdjacencyMatrix = new bool[verticesCount, verticesCount];
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            for (int row = 0; row < rowsCount; row++) {
                GetAdjacencyMatrixRowIndex(row, out int rowInAdjacency);
                for (int col = 0; col < colsCount; col++) {
                    GetAdjacencyMatrixColIndex(col, out int colInAdjacency);
                    if (GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col) == 0)
                        matchingGraphAdjacencyMatrix[rowInAdjacency, colInAdjacency] =
                            matchingGraphAdjacencyMatrix[colInAdjacency, rowInAdjacency] = true;
                }
            }
            return matchingGraphAdjacencyMatrix;
        }

        public static int[] GetAssignmentProblemSolution(int[,] adjacencyMatrix, int verticesCount, out int assignmentCost) {
            int[,] matrix = (int[,])adjacencyMatrix.Clone();
            GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            FirstStage(matrix, verticesCount);
            SecondStage(matrix, verticesCount);
            int[] matchingPairsArray;
            while (true) {
                bool[,] matchingGraphAdjacencyMatrix = getMatchingGraphAdjacencyMatrix(matrix, verticesCount);

                matchingPairsArray = GetMaximalMatching(matchingGraphAdjacencyMatrix, verticesCount);
                if (GetMatchingCardinality(matchingPairsArray) < rowsCount)
                    FourthStage(matrix, matchingPairsArray, verticesCount);
                else
                    break;
            }
            assignmentCost = GetCostOfAssignment(adjacencyMatrix, verticesCount, matchingPairsArray);
            return matchingPairsArray;
        }
    }
}
