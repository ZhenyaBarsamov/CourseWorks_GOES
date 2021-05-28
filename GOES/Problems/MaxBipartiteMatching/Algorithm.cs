using System;
using System.Collections.Generic;
using System.Linq;

namespace GOES.Problems.MaxBipartiteMatching {
    /// <summary>
    /// Статический класс, предоставляющий методы для решения 
    /// задачи о максимальном паросочетании в двудольном графе (полное решение и решение её этапов)
    /// </summary>
    static class Algorithm {
        // Получить случайную перестановку вершин для графа с заданным количеством вершин
        private static int[] GetRandomVerticesPermutation(int verticesCount) {
            Random permutation = new Random();
            int[] res = new int[verticesCount];
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
                res[vertexIndex] = vertexIndex;
            for (int vertexIndex = 0; vertexIndex < verticesCount - 1; vertexIndex++) {
                int r = permutation.Next(vertexIndex, verticesCount);
                int tmp = res[vertexIndex];
                res[vertexIndex] = res[r];
                res[r] = tmp;
            }
            return res;
        }

        // Метод, реализующий обход в глубину для построения аугментальной цепи
        private static bool BuildAugmentalPath(bool[,] graph, int verticesCount, int curVertex, bool[] visitedVertices, int[] matchingPairsArray, List<int> path) {
            // Если эта вершина уже была посещена - откатываемся назад
            if (visitedVertices[curVertex])
                return false;
            // Иначе - добавляем её в цепь
            visitedVertices[curVertex] = true;
            path.Add(curVertex);
            // Если мы встали на непокрытую вершину, т.е. построили аугментальную цепь - мы закончили
            if (path.Count > 1 && matchingPairsArray[path.Last()] == -1)
                return true;
            // Если мы стоим на покрытой паросочетанием вершине, и ещё не были в парной ей вершине - идём в пару
            if (matchingPairsArray[curVertex] != -1 && !visitedVertices[matchingPairsArray[curVertex]]) {
                int vertexPair = matchingPairsArray[curVertex];
                if (BuildAugmentalPath(graph, verticesCount, vertexPair, visitedVertices, matchingPairsArray, path))
                    return true;
            }
            // Если же мы стоим на непокрытой вершине (первая в цепи) или же уже были в парной вершине - смотрим, куда можем пойти дальше
            else {
                int[] verticesPermutation = GetRandomVerticesPermutation(verticesCount);
                foreach (var nextVertex in verticesPermutation) {
                    if (!graph[curVertex, nextVertex])
                        continue;
                    if (BuildAugmentalPath(graph, verticesCount, nextVertex, visitedVertices, matchingPairsArray, path))
                        return true;
                }
            }
            path.RemoveAt(path.Count - 1);
            return false;
        }

        // Получить аугментальный путь из вершины curVertex
        private static List<int> GetAugmentalPath(bool[,] graph, int verticesCount, int curVertex, int[] matchingPairsArray) {
            bool[] visitedVertices = new bool[verticesCount]; // автоматически заполнится значениями false
            List<int> augmentalPath = new List<int>();
            if (matchingPairsArray[curVertex] == -1)
                BuildAugmentalPath(graph, verticesCount, curVertex, visitedVertices, matchingPairsArray, augmentalPath);
            return augmentalPath;
        }

        /// <summary>
        /// Получить случайный аугментальный путь для заданного графа и заданного для него текущего паросочетания
        /// </summary>
        /// <param name="graph">Матрица смежности графа</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        /// <param name="matchingPairsArray">Массив, по индексу (индекс вершины) хранящий индекс её пары</param>
        /// <returns>Список индексов вершин, входящих в аугментальный путь (или пустой список, если пути не существует)</returns>
        public static List<int> GetRandomAugmentalPath(bool[,] graph, int verticesCount, int[] matchingPairsArray) {
            List<int> augmentalPath = new List<int>();
            int[] verticesPermutation = GetRandomVerticesPermutation(verticesCount);
            foreach (var curVertex in verticesPermutation) {
                if (matchingPairsArray[curVertex] == -1)
                    augmentalPath = GetAugmentalPath(graph, verticesCount, curVertex, matchingPairsArray);
                if (augmentalPath.Count != 0)
                    break;
            }
            return augmentalPath;
        }

        /// <summary>
        /// Получить одно из максимальных паросочетаний для заданного двудольного графа
        /// </summary>
        /// <param name="graph">Матрица смежности графа</param>
        /// <param name="verticesCount">Количество вершин в графе</param>
        /// <returns></returns>
        public static int[] GetMaximalMatching(bool[,] graph, int verticesCount) {
            int[] matchingPair = new int[verticesCount];
            for (int i = 0; i < verticesCount; i++)
                matchingPair[i] = -1;
            for (int curVertex = 0; curVertex < verticesCount; curVertex++) {
                List<int> augmentalPath = GetAugmentalPath(graph, verticesCount, curVertex, matchingPair);
                AlternateOnAugmentalPath(augmentalPath, matchingPair);
            }
            return matchingPair;
        }

        /// <summary>
        /// Провести чередование по заданному аугментальному пути при текущем паросочетании matchingPairsArray
        /// </summary>
        /// <param name="augmentalPath">Аугментальный путь</param>
        /// <param name="matchingPairsArray">Массив, по индексу (индекс вершины) хранящий индекс её пары</param>
        public static void AlternateOnAugmentalPath(List<int> augmentalPath, int[] matchingPairsArray) {
            // Проводим чередование по построенной аугментальной цепи
            // Чётные по счёту рёбра убираем из паросочетания
            for (int curVertexIndex = 1; curVertexIndex < augmentalPath.Count - 1; curVertexIndex += 2) {
                int curVertex = augmentalPath[curVertexIndex];
                int nextVertex = augmentalPath[curVertexIndex + 1];
                matchingPairsArray[curVertex] = -1;
                matchingPairsArray[nextVertex] = -1;
            }
            // А нечётные - добавляем в паросочетание
            for (int curVertexIndex = 0; curVertexIndex < augmentalPath.Count - 1; curVertexIndex += 2) {
                int curVertex = augmentalPath[curVertexIndex];
                int nextVertex = augmentalPath[curVertexIndex + 1];
                matchingPairsArray[curVertex] = nextVertex;
                matchingPairsArray[nextVertex] = curVertex;
            }
        }

        /// <summary>
        /// Получить мощность паросочетания, заданного массивом пар вершин
        /// </summary>
        /// <param name="matchingPairsArray">Массив, по индексу (индекс вершины) хранящий индекс её пары</param>
        public static int GetMatchingCardinality(int[] matchingPairsArray) {
            int cardinality = 0;
            bool[] isChecked = new bool[matchingPairsArray.Length];
            for (int vertexIndex = 0; vertexIndex < matchingPairsArray.Length; vertexIndex++) {
                if (matchingPairsArray[vertexIndex] != -1 && !isChecked[vertexIndex]) {
                    int vertexPairIndex = matchingPairsArray[vertexIndex];
                    cardinality++;
                    isChecked[vertexIndex] = isChecked[vertexPairIndex] = true;
                }
            }
            return cardinality;
        }
    }
}
