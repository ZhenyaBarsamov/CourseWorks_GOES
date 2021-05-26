using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOES.Problems.MaxBipartiteMatching {
    /// <summary>
    /// Статический класс, предоставляющий методы для решения 
    /// задачи о максимальном паросочетании в двудольном графе (полное решение и решение её этапов)
    /// </summary>
    static class Algorithm {
        private static bool BuildAugmentalPath(bool[,] graph, int verticesCount, int curVertex, bool[] visitedVertices, int[] matchingPairsArray, List<int> path) {
            if (visitedVertices[curVertex])
                return false;
            visitedVertices[curVertex] = true;
            path.Add(curVertex);

            for (int nextVertex = 0; nextVertex < verticesCount; nextVertex++) {
                if (graph[curVertex, nextVertex] == false)
                    continue;
                if (matchingPairsArray[nextVertex] == -1) {
                    matchingPairsArray[nextVertex] = curVertex;
                    matchingPairsArray[curVertex] = nextVertex;
                    path.Add(nextVertex);
                    return true;
                }
                else if (BuildAugmentalPath(graph, verticesCount, matchingPairsArray[nextVertex], visitedVertices, matchingPairsArray, path)) {
                    matchingPairsArray[nextVertex] = curVertex;
                    matchingPairsArray[curVertex] = nextVertex;
                    return true;
                }
            }
            path.RemoveAt(path.Count - 1);
            return false;
        }

        public static List<int> GetAugmentalPath(bool[,] graph, int verticesCount, int curVertex, int[] matchingPairsArray) {
            bool[] visitedVertices = new bool[verticesCount]; // автоматически заполнится значениями false
            List<int> augmentalPath = new List<int>(verticesCount);
            BuildAugmentalPath(graph, verticesCount, curVertex, visitedVertices, matchingPairsArray, augmentalPath);
            return augmentalPath;
        }

        public static int[] GetMaximalMatching(bool[,] graph, int verticesCount) {
            int[] matchingPair = new int[verticesCount];
            for (int i = 0; i < verticesCount; i++)
                matchingPair[i] = -1;
            for (int curVertex = 0; curVertex < verticesCount; curVertex++)
                GetAugmentalPath(graph, verticesCount, curVertex, matchingPair);
            return matchingPair;
        }

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
    }
}
