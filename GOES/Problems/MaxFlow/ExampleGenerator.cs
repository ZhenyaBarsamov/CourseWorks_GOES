using System;

namespace GOES.Problems.MaxFlow {
    /// <summary>
    /// Статический класс, реализующий метод случайной генерации примера для задачи о максимальном потоке (и минимальном разрезе) в сети
    /// </summary>
    class ExampleGenerator {
        // Будем считать, что исток - первая вершина, сток - последняя

        /// <summary>
        /// Минимальное количество вершин в сети
        /// </summary>
        private static readonly int minVerticesCount = 5;
        /// <summary>
        /// Максимальное количество вершин в сети
        /// </summary>
        private static readonly int maxVerticesCount = 8;
        /// <summary>
        /// Минимальное количество маршрутов от истока к стоку
        /// </summary>
        private static readonly int minPathsCount = 2;
        /// <summary>
        /// Минимальная пропускная способность, которая может быть у дуги
        /// </summary>
        private static readonly int minCapacity = 1;
        /// <summary>
        /// Максимальная пропускная способность, которая может быть у дуги
        /// </summary>
        private static readonly int maxCapacity = 20;

        // Получить случайный маршрут от истока к стоку
        private static int[] GetRandomSourceTargetPath(int verticesCount) {
            Random rand = new Random();
            int[] path = new int[verticesCount];
            // Заполняем путь вершинами по порядку. В дальнейшем переставляем все вершины кроме первой и последней,
            // они у нас исток и сток
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
                path[vertexIndex] = vertexIndex;
            // Вычисляем середину пути - для того, чтоб сеть была не слишком разветвлённой, мы 
            // будем переставлять вершины между собой только внутри левой и правой половинок пути
            int border = (int)Math.Ceiling(verticesCount / 2.0);
            // Переставляем вершины в левой половинке (исток не трогаем, начинаем со второй)
            for (int vertexIndex = 1; vertexIndex < border - 1; vertexIndex++) {
                int r = rand.Next(vertexIndex, border);
                int tmp = path[vertexIndex];
                path[vertexIndex] = path[r];
                path[r] = tmp;
            }
            // Переставляем вершины в правой половинке (сток не трогаем, заканчиваем предпоследней)
            for (int vertexIndex = border; vertexIndex < verticesCount - 2; vertexIndex++) {
                int r = rand.Next(vertexIndex, verticesCount - 1);
                int tmp = path[vertexIndex];
                path[vertexIndex] = path[r];
                path[r] = tmp;
            }
            return path;
        }

        /// <summary>
        /// Сгенерировать случайный пример задачи о максимальном потоке (и минимальном разрезе) в сети
        /// </summary>
        public static MaxFlowProblemExample GenerateExample() {
            Random rand = new Random();
            int verticesCount = rand.Next(minVerticesCount, maxVerticesCount + 1);
            int[,] capacityMatrix = new int[verticesCount, verticesCount];
            // Случайно выбираем количество разных маршрутов от истока к стоку
            int pathsCount = rand.Next(minPathsCount,  verticesCount - 2);
            int pathCounter = 0;
            // Случайным образом строим нужное количество уникальных маршрутов
            while (pathCounter <= pathsCount) {
                // Получаем случайный маршрут
                int[] path = GetRandomSourceTargetPath(verticesCount);
                bool isUnique = false;
                // Создаём уникальные части построенного маршрута
                for (int curVertexIndex = 0; curVertexIndex < path.Length - 1; curVertexIndex++) {
                    int curVertex = path[curVertexIndex];
                    int nextVertex = path[curVertexIndex + 1];
                    if (capacityMatrix[curVertex, nextVertex] == 0) {
                        isUnique = true;
                        capacityMatrix[curVertex, nextVertex] = rand.Next(minCapacity, maxCapacity + 1);
                    }
                }
                // Если построенный маршрут был уникальным (отличался от остальных хотя бы одной вершиной) - засчитываем
                // Иначе - надо генерировать заново
                if (isUnique)
                    pathCounter++;
            }
            return new MaxFlowProblemExample("Случайный пример", $"Сеть из {verticesCount} вершин",
                0, verticesCount - 1, capacityMatrix, null);
        }
    }
}
