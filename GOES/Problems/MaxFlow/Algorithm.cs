using System;
using System.Collections.Generic;
using System.Linq;

namespace GOES.Problems.MaxFlow {
    static class Algorithm {
        /// <summary>
        /// Получить аугментальный маршрут и его аугментальный поток для заданной сети
        /// </summary>
        /// <param name="capacityMatrix">Матрица пропускных способностей сети</param>
        /// <param name="flowMatrix">Матрица потоков сети</param>
        /// <param name="sourceIndex">Индекс вершины-истока</param>
        /// <param name="targetIndex">Индекс вершины-стока</param>
        /// <param name="augmentalFlowValue">Величина аугментального потока полученного аугментального маршрута</param>
        /// <returns>Список индексов вершин, составляющих аугментальный маршрут (первая - исток, последняя - сток)</returns>
        public static List<int> GetAugmentalPath(int[,] capacityMatrix, int[,] flowMatrix, int sourceIndex, int targetIndex, out int augmentalFlowValue) {
            int verticesCount = capacityMatrix.GetLength(0);
            bool[] verticesInPath = new bool[verticesCount];
            bool[] visitedVertices = new bool[verticesCount];
            List<int> pathVertices = new List<int>();
            // Строим аугментальный путь
            BuildAugmentalPath(capacityMatrix, flowMatrix, verticesCount, sourceIndex, targetIndex, verticesInPath, visitedVertices, pathVertices);
            // Отдельно находим аугментальный поток
            augmentalFlowValue = GetAugmentalFlowValue(capacityMatrix, flowMatrix, verticesCount, pathVertices);
            return pathVertices;
        }

        /// <summary>
        /// Произвести увеличение потока в сети по заданному аугментальному пути на заданную величину аугментального потока
        /// </summary>
        /// <param name="capacityMatrix">Матрица пропускных способностей сети</param>
        /// <param name="flowMatrix">Матрица потоков сети</param>
        /// <param name="augmentalPath">Построенный аугментальный маршрут</param>
        /// <param name="augmentalFlowValue">Дополнительный поток для построенного аугментального маршрута</param>
        public static void RaiseFlowOnAugmentalPath(int[,] capacityMatrix, int[,] flowMatrix, List<int> augmentalPath, int augmentalFlowValue) {
            // Увеличиваем поток по аугментальному маршруту (по прямым увеличиваем, по обратным уменьшаем) 
            for (int i = 0; i < augmentalPath.Count - 1; i++) {
                int curVertexIndex = augmentalPath[i];
                int nextVertexIndex = augmentalPath[i + 1];
                if (capacityMatrix[curVertexIndex, nextVertexIndex] != 0)
                    flowMatrix[curVertexIndex, nextVertexIndex] += augmentalFlowValue;
                else
                    flowMatrix[nextVertexIndex, curVertexIndex] -= augmentalFlowValue;
            }
        }

        /// <summary>
        /// Получить решение задачи о максимальном потоке
        /// </summary>
        /// <param name="capacityMatrix">Матрица пропускных способностей сети</param>
        /// <param name="sourceIndex">Индекс вершины-истока</param>
        /// <param name="targetIndex">Индекс вершины-стока</param>
        /// <param name="minCut">Список дуг минимального разреза в виде пар "начальная вершина-конечная вершина"</param>
        /// <param name="maxFlowValue">Величина максимального потока в сети</param>
        /// <returns>Матрица, содержащая величины потоков в дугах сети</returns>
        public static int[,] GetMaxFlowSolve(int[,] capacityMatrix, int sourceIndex, int targetIndex, out List<Tuple<int, int>> minCut, out int maxFlowValue) {
            int verticesCount = capacityMatrix.GetLength(0);
            int[,] flowMatrix = new int[verticesCount, verticesCount]; // все элементы автоматически будут проинициализированы нулями (что нам и надо)
            while (true) {
                // Находим аугментальный поток
                bool[] visitedVertices = new bool[verticesCount];
                List<int> augmentalPath = new List<int>();
                BuildAugmentalPath(capacityMatrix, flowMatrix, verticesCount, sourceIndex, targetIndex, new bool[verticesCount], visitedVertices, augmentalPath);
                // Если аугментальный путь не был найден, конец алгоритма. Формируем множество дуг минимального разреза и считаем максимальный поток
                if (augmentalPath.Count == 0) {
                    minCut = GetMinimalCut(capacityMatrix, flowMatrix, verticesCount, visitedVertices);
                    maxFlowValue = 0;
                    for (int nextVertexIndex = 0; nextVertexIndex < verticesCount; nextVertexIndex++)
                        maxFlowValue += flowMatrix[sourceIndex, nextVertexIndex];
                    break;
                }
                // Получаем величину аугментального потока
                int augmentalFlowValue = GetAugmentalFlowValue(capacityMatrix, flowMatrix, verticesCount, augmentalPath);
                RaiseFlowOnAugmentalPath(capacityMatrix, flowMatrix, augmentalPath, augmentalFlowValue);
            }
            return flowMatrix;
        }

        public static int GetCurNetworkFlowValue(int[,] flowMatrix, int verticesCount, int sourceIndex) {
            int sumFlow = 0;
            for (int nextVertex = 0; nextVertex < verticesCount; nextVertex++)
                sumFlow += flowMatrix[sourceIndex, nextVertex];
            return sumFlow;
        }

        private static void BuildAugmentalPath(int[,] capacityMatrix, int[,] flowMatrix, int verticesCount, int curVertexIndex, int targetVertexIndex, bool[] verticesInPath, bool[] visitedVertices, List<int> pathVertices) {
            // Помечаем вершину как посещённую
            visitedVertices[curVertexIndex] = true;
            // Берём текущую вершину в стоящийся путь
            verticesInPath[curVertexIndex] = true;
            pathVertices.Add(curVertexIndex);

            // Если текущая вершина искомая - выходим
            if (curVertexIndex == targetVertexIndex)
                return;

            // Ищем следующую вершину
            for (int nextVertexIndex = 0; nextVertexIndex < verticesCount; nextVertexIndex++) {
                // Проверяем прямое ребро: есть, не взято, допускает увеличение потока
                if (capacityMatrix[curVertexIndex, nextVertexIndex] != 0
                    && !verticesInPath[nextVertexIndex]
                    && capacityMatrix[curVertexIndex, nextVertexIndex] - flowMatrix[curVertexIndex, nextVertexIndex] > 0)
                    BuildAugmentalPath(capacityMatrix, flowMatrix, verticesCount, nextVertexIndex, targetVertexIndex, verticesInPath, visitedVertices, pathVertices);
                // Проверим, не закончен ли поиск (на случай, если путь уже найден)
                if (pathVertices.Last() == targetVertexIndex)
                    return;
                // Проверяем обратное ребро: есть, не взято, допускает уменьшение потока
                if (capacityMatrix[nextVertexIndex, curVertexIndex] != 0
                    && !verticesInPath[nextVertexIndex]
                    && flowMatrix[nextVertexIndex, curVertexIndex] > 0)
                    BuildAugmentalPath(capacityMatrix, flowMatrix, verticesCount, nextVertexIndex, targetVertexIndex, verticesInPath, visitedVertices, pathVertices);
                // Проверим, не закончен ли поиск (на случай, если путь уже найден)
                if (pathVertices.Last() == targetVertexIndex)
                    return;
            }
            // Убираем текущую вершину из строящегося пути, если она не искомая (если сверху искомая - мы нашли путь)
            // На случай, если аугментальный путь из этой вершины не был найден
            if (pathVertices.Last() != targetVertexIndex) {
                verticesInPath[curVertexIndex] = false;
                pathVertices.RemoveAt(pathVertices.Count - 1);
            }
        }

        private static int GetAugmentalFlowValue(int[,] capacityMatrix, int[,] flowMatrix, int verticesCount, List<int> augmentalPath) {
            // Если аугментального пути нет, то аугментальный поток в нём - нулевой
            if (augmentalPath.Count == 0)
                return 0;
            int augmentalFlowValue = int.MaxValue;
            for (int i = 0; i < augmentalPath.Count - 1; i++) {
                int curVertexIndex = augmentalPath[i];
                int nextVertexIndex = augmentalPath[i + 1];
                int curAugmentalFlow;
                // Смотрим, на сколько мы можем увеличить (или уменьшить) поток по этой дуге
                if (capacityMatrix[curVertexIndex, nextVertexIndex] - flowMatrix[curVertexIndex, nextVertexIndex] > 0)
                    curAugmentalFlow = capacityMatrix[curVertexIndex, nextVertexIndex] - flowMatrix[curVertexIndex, nextVertexIndex];
                else
                    curAugmentalFlow = flowMatrix[curVertexIndex, nextVertexIndex];
                augmentalFlowValue = Math.Min(augmentalFlowValue, curAugmentalFlow);
            }
            return augmentalFlowValue;
        }

        private static List<Tuple<int, int>> GetMinimalCut(int[,] capacityMatrix, int[,] flowMatrix, int verticesCount, bool[] divisionOfVertices) {
            List<Tuple<int, int>> res = new List<Tuple<int, int>>();
            // Составляем списки из индексов вершин обоих подмножеств
            List<int> firstDivision = new List<int>();
            List<int> secondDivision = new List<int>();
            for (int i = 0; i < verticesCount; i++)
                if (divisionOfVertices[i])
                    firstDivision.Add(i);
                else
                    secondDivision.Add(i);
            // Добавляем в разрез дуги, соединяющие разбиение
            foreach (var firstDivVertexIndex in firstDivision)
                foreach (var secondDivVertexIndex in secondDivision)
                    if (capacityMatrix[firstDivVertexIndex, secondDivVertexIndex] != 0)
                        res.Add(new Tuple<int, int>(firstDivVertexIndex, secondDivVertexIndex));
                    else if (capacityMatrix[secondDivVertexIndex, firstDivVertexIndex] != 0)
                        res.Add(new Tuple<int, int>(secondDivVertexIndex, firstDivVertexIndex));
            return res;
        }
    }
}
