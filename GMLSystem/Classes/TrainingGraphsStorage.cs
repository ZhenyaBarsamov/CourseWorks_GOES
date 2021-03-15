using System.Collections.Generic;
using System.Drawing;

namespace GMLSystem.Classes {
    /// <summary>
    /// Структура для хранения данных о графах-примерах
    /// </summary>
    public struct GraphStruct {
        /// <summary>
        /// Матрица смежности графа
        /// </summary>
        public int[,] adjacencyMatrix;
        /// <summary>
        /// Координаты отрисовки вершин графа, по порядку, с первой по последнюю
        /// </summary>
        public PointF[] verticesCoordinates;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adjacencyMatrix">Матрица смежности</param>
        /// <param name="verticesCoordinates">Массив координат отрисовки вершин</param>
        public GraphStruct(int[,] adjacencyMatrix, PointF[] verticesCoordinates) {
            this.adjacencyMatrix = adjacencyMatrix;
            this.verticesCoordinates = verticesCoordinates;
        }
    }

    /// <summary>
    /// Класс-хранилище данных о тестовых графах
    /// </summary>
    public class TrainingGraphsStorage {
        /// <summary>
        /// Список, содержащий данные о тестовых графах
        /// </summary>
        public List<GraphStruct> Graphs { get; private set; }
        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="index">Номер графа в массиве</param>
        /// <returns></returns>
        public GraphStruct this[int index] => Graphs[index];

        /// <summary>
        /// Количество тренировочных графов
        /// </summary>
        public int GraphsCount => Graphs.Count;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        public TrainingGraphsStorage() {
            // Создаём список
            Graphs = new List<GraphStruct>();

            // Для информации о добавляемых графах
            int[,] adjacencyMatrix;
            PointF[] verticesCoordinates;

            // Данные тестового графа 1
            adjacencyMatrix = new[,] {
                {0, 1, 0, 1, 0, 0, 0, 0},
                {1, 0, 1, 0, 0, 0, 0, 0},
                {0, 1, 0, 1, 0, 0, 0, 1},
                {1, 0, 1, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 1, 0, 0},
                {0, 0, 0, 0, 1, 0, 1, 0},
                {0, 0, 0, 0, 0, 1, 0, 1},
                {0, 0, 1, 0, 0, 0, 1, 0}
            };
            verticesCoordinates = new PointF[] {
                new PointF(225, 50), new PointF(475, 50), new PointF(225, 175), new PointF(475, 175),
                new PointF(225, 290), new PointF(475, 290), new PointF(225, 450), new PointF(475, 450)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates));

            // Данные тестового графа 2
            adjacencyMatrix = new[,] {
                {0, 1, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0, 1, 0},
            };
            verticesCoordinates = new PointF[] {
                new PointF(225, 50), new PointF(475, 50), new PointF(225, 175), new PointF(475, 175),
                new PointF(225, 290), new PointF(475, 290), new PointF(225, 450), new PointF(475, 450)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates));

            // Данные тестового графа 3
            adjacencyMatrix = new[,] {
                {0, 0, 0, 1, 0, 1, 0, 1},
                {0, 0, 1, 0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0, 0, 0, 1},
                {1, 0, 1, 0, 0, 0, 1, 0},
                {0, 1, 0, 1, 0, 1, 0, 0},
                {1, 0, 1, 0, 1, 0, 0, 0},
            };
            verticesCoordinates = new PointF[] {
                new PointF(225, 50), new PointF(475, 50), new PointF(225, 175), new PointF(475, 175),
                new PointF(225, 290), new PointF(475, 290), new PointF(225, 450), new PointF(475, 450)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates));

            // Данные тестового графа 4
            adjacencyMatrix = new[,] {
                {0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0},
                {0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 0},
            };
            verticesCoordinates = new PointF[] {
                new PointF(225, 100), new PointF(475, 100),
                new PointF(225, 270), new PointF(475, 270), new PointF(225, 440), new PointF(475, 440)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates));
            
            // Данные тестового графа 5
            adjacencyMatrix = new[,] {
                {0, 1, 0, 1},
                {1, 0, 1, 0},
                {0, 1, 0, 1},
                {1, 0, 1, 0},
            };
            verticesCoordinates = new PointF[] {
                new PointF(225, 100), new PointF(475, 100),
                new PointF(225, 270), new PointF(475, 270)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates));
        }
    }
}
