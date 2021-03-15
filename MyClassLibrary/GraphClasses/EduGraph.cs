using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.GraphClasses {
    /// <summary>
    /// Класс обучающего графа
    /// </summary>
    public class EduGraph {
        /// <summary>
        /// Матрица графа, представляющая собой видоизменённую матрицу смежности,
        /// на пересечении строк и столбцов которой не числа, а структуры,
        /// хранящие информацию о данном ребре (если ребра нет - null).
        /// Нумерация вершин - с единицы.
        /// </summary>
        public Dictionary<int, Dictionary<int, EdgeInfo>> Matrix {get; set;}

        /// <summary>
        /// Словарь, содержащий информацию о вершинах графа
        /// </summary>
        public Dictionary<int, VertexInfo> VerticesInfo { get; private set; }

        /// <summary>
        /// Индексатор доступа к вершинам графа.
        /// Аналог свойства VerticesInfo
        /// </summary>
        /// <param name="vertexID">Идентификатор вершины</param>
        /// <returns></returns>
        public VertexInfo this[int vertexID] => VerticesInfo[vertexID];

        /// <summary>
        /// Инексатор доступа к дугам графа.
        /// Аналог свойства Matrix
        /// </summary>
        /// <param name="startVertexID">ID вершины, являющейся началом дуги</param>
        /// <param name="finishVertexID">ID вершины, являющейся концом дуги</param>
        /// <returns></returns>
        public EdgeInfo this[int startVertexID, int finishVertexID] {
            get => Matrix[startVertexID][finishVertexID];
            set => Matrix[startVertexID][finishVertexID] = value;
        }

        /// <summary>
        /// Количество вершин графа
        /// </summary>
        public int VerticesCount => VerticesInfo.Count;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adjacencyMatrix">Матрица смежности графа, на пересечении строки и столбца - величина пропускной способности</param>
        /// <param name="verticesCoordinates">Массив координат отрисовки вершин (по порядку, с первой вершины до последней)</param>
        public EduGraph(int[,] adjacencyMatrix,  PointF[] verticesCoordinates) {
            int verticesCount = adjacencyMatrix.GetLength(0); // количество вершин в графе (матрица квадратная)
            // Создаём и заполняем матрицу графа на основе матрицы смежности
            Matrix = new Dictionary<int, Dictionary<int, EdgeInfo>>();
            for (int row = 0; row < verticesCount; row++) {
                Matrix[row + 1] = new Dictionary<int, EdgeInfo>(); // +1, потому что в Matrix нумерация с единицы
                for (int column = 0; column < verticesCount; column++) {
                    // Если пропускная способность не нулевая (подразумевается, что положительная)
                    if (adjacencyMatrix[row, column] != 0)
                        Matrix[row + 1][column + 1] = new EdgeInfo(row + 1, column + 1, adjacencyMatrix[row, column]);
                    else
                        Matrix[row + 1][column + 1] = null;
                }
            }
            // Создаём и заполняем информацию о вершинах графа на основе массива координат
            VerticesInfo = new Dictionary<int, VertexInfo>();
            for (int vertex = 0; vertex < verticesCount; vertex++)
                VerticesInfo[vertex + 1] = new VertexInfo(vertex + 1, verticesCoordinates[vertex]); // +1, т.к. нумерация в словаре с единицы
        }

        /// <summary>
        /// Получить текущий поток в сети, исходящий из заданного истока
        /// </summary>
        /// <param name="startVertexID">Исток</param>
        /// <returns></returns>
        public int GetCurrentFlow(int startVertexID) {
            int curFlow = 0;
            for (int column = 1; column <= VerticesCount; column++)
                if (Matrix[startVertexID][column] != null)
                    curFlow += Matrix[startVertexID][column].Flow;
            return curFlow;
        }


        /// <summary>
        /// Очистить внешние метки всех вершин графа
        /// </summary>
        public void ClearVerticesOutsideLabels() {
            foreach (var v in VerticesInfo.Values)
                v.OutsideLabel = null;
        }

        /// <summary>
        /// Обнулить поток всех дуг графа
        /// </summary>
        public void ClearGraphFlow() {
            for (int row = 1; row <= VerticesCount; row++)
                for (int column = 1; column <= VerticesCount; column++)
                    if (Matrix[row][column] == null)
                        continue;
                    else
                        Matrix[row][column].Flow = 0;
        }
    }
}
