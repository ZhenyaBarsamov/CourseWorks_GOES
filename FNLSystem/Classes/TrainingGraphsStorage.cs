using System.Collections.Generic;
using System.Drawing;

namespace FNLSystem.Classes {
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
        /// Вершина-исток по умолчанию
        /// </summary>
        public int startVertexIndex;
        /// <summary>
        /// Вершина-сток по умолчанию
        /// </summary>
        public int finishVertexIndex;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adjacencyMatrix">Матрица смежности</param>
        /// <param name="verticesCoordinates">Массив координат отрисовки вершин</param>
        public GraphStruct(int[,] adjacencyMatrix, PointF[] verticesCoordinates, int startVertexIndex, int finishVertexIndex) {
            this.adjacencyMatrix = adjacencyMatrix;
            this.verticesCoordinates = verticesCoordinates;
            this.startVertexIndex = startVertexIndex;
            this.finishVertexIndex = finishVertexIndex;
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
            adjacencyMatrix = new int[,] {
                {0, 15, 0, 25, 0, 0, 0 },
                {0, 0, 16, 7, 0, 0, 0 },
                {0, 0, 0, 0, 4, 0, 9 },
                {0, 0, 0, 0, 18, 3, 0 },
                {0, 0, 0, 0, 0, 6, 25 },
                {0, 2, 3, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 }
            };
            verticesCoordinates = new PointF[] {
                new PointF(125, 250), new PointF(275, 100), new PointF(425, 100), new PointF(275, 400),
                new PointF(425, 400), new PointF(350, 250), new PointF(575, 250)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates, 1, 7));

            // Данные тестового графа 2
            adjacencyMatrix = new [,] {
                {0, 7, 0, 0, 5, 0, 3, 0 },
                {0, 0, 2, 0, 0, 0, 0, 3 },
                {0, 0, 0, 6, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 2, 0, 0, 6, 0, 0 },
                {0, 0, 0, 4, 0, 0, 0, 1 },
                {0, 0, 1, 0, 2, 0, 0, 6 },
                {0, 0, 0, 5, 0, 0, 0, 0 }
            };
            verticesCoordinates = new PointF[] {
                new PointF(50, 225), new PointF(250, 75), new PointF(450, 75), new PointF(650, 225),
                new PointF(180, 225), new PointF(500, 225), new PointF(250, 425), new PointF(450, 425)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates, 1, 4));
            
            // Данные тестового графа 3 - парк Кирова
            adjacencyMatrix = new[,] {
                {0, 4, 3, 7, 0, 0, 0},
                {0, 0, 0, 1, 3, 0, 0},
                {0, 0, 0, 0, 0, 3, 0},
                {0, 0, 0, 0, 4, 4, 0},
                {0, 0, 0, 0, 0, 0, 8},
                {0, 0, 0, 0, 1, 0, 6},
                {0, 0, 0, 0, 0, 0, 0},
            };
            verticesCoordinates = new PointF[] {
                new PointF(80, 235), new PointF(210, 105), new PointF(250, 385), new PointF(295, 235),
                new PointF(475, 235), new PointF(425, 385), new PointF(615, 195)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates, 1, 7));

            // Данные тестового графа 4
            adjacencyMatrix = new[,] {
                {0, 7, 9, 4, 0, 0, 0, 0, 0},
                {0, 0, 0, 8, 3, 0, 0, 6, 0},
                {0, 0, 0, 5, 0, 8, 4, 0, 0},
                {0, 0, 0, 0, 0, 0, 9, 2, 0},
                {0, 0, 0, 0, 0, 0, 0, 2, 0},
                {0, 0, 0, 0, 0, 0, 5, 0, 3},
                {0, 0, 0, 0, 0, 0, 0, 7, 6},
                {0, 0, 0, 0, 0, 0, 0, 0, 8},
                {0, 0, 0, 0, 0, 0, 0, 0, 0},
            };
            verticesCoordinates = new PointF[] {
                new PointF(80, 265), new PointF(210, 135), new PointF(250, 415), new PointF(295, 265),
                new PointF(355, 85), new PointF(415, 430), new PointF(450, 285), new PointF(475, 175), new PointF(615, 310)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates, 1, 9));
            // Данные тестового графа 5
            adjacencyMatrix = new[,] {
                {0, 1, 0, 1, 0, 1, 0, 1, 0, 0},
                {0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 1, 0, 1, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0, 0, 1, 0, 1, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
            verticesCoordinates = new PointF[] {
                new PointF(100, 250), new PointF(250, 50), new PointF(450, 50), new PointF(250, 175), new PointF(450, 175),
                new PointF(250, 290), new PointF(450, 290), new PointF(250, 450), new PointF(450, 450), new PointF(600, 250)
            };
            // Создаём структуру и добавляем
            Graphs.Add(new GraphStruct(adjacencyMatrix, verticesCoordinates, 1, 10));
        }
    }
}
