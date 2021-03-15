using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using GraphEduSysControlLibrary;
using MyClassLibrary.GraphClasses;


namespace GMLSystem.Classes {
    public class Solver {
        /// <summary>
        /// Метод поиска аугментальной цепи между заданными вершинами сети.
        /// Результат возвращается в stackPath в обратном порядке
        /// </summary>
        /// <param name="graph">Сеть (граф)</param>
        /// <param name="curVertexID">Начало аугментальной цепи</param>
        /// <param name="findingVertexID">Конец аугментальной цепи</param>
        /// <param name="verticesInPath">Вспомогательный массив для пометки вершин, включённых в цепь</param>
        /// <param name="visitedVertices">Массив для пометки когда-либо посещённых вершин (для построения первого множества для разреза)</param>
        /// <param name="stackPath">Стек, в который добавляются вершины по мере формирования пути. Содержит результат в обратном порядке</param>
        private void BuildAugmentalPath(EduGraph graph, int curVertexID, int findingVertexID, bool[] verticesInPath, bool[] visitedVertices, Stack<int> stackPath) {
            // Помечаем вершину как посещённую
            visitedVertices[curVertexID - 1] = true;
            // Берём текущую вершину в стоящийся путь
            verticesInPath[curVertexID - 1] = true;
            stackPath.Push(curVertexID);
            
            // Если текущая вершина искомая - выходим
            if (curVertexID == findingVertexID)
                return;

            // Ищем следующую вершину - проверяем прямые рёбра
            for (int nextVertexID = 1; nextVertexID <= graph.VerticesInfo.Count ; nextVertexID++) {
                // Проверяем прямое ребро: есть, не взято, допускает увеличение потока
                if (graph[curVertexID, nextVertexID] != null && !verticesInPath[nextVertexID - 1] && graph[curVertexID, nextVertexID].GetAugmentalFlow() > 0)
                    BuildAugmentalPath(graph, nextVertexID, findingVertexID, verticesInPath, visitedVertices, stackPath);
                // Проверим, не закончен ли поиск
                if (stackPath.Peek() == findingVertexID)
                    return;
                // Проверяем обратное ребро: есть, не взято, допускает уменьшение потока
                if (graph[nextVertexID, curVertexID] != null && !verticesInPath[nextVertexID - 1] && graph[nextVertexID, curVertexID].Flow > 0)
                    BuildAugmentalPath(graph, nextVertexID, findingVertexID, verticesInPath, visitedVertices, stackPath);
                // Проверим, не закончен ли поиск
                if (stackPath.Peek() == findingVertexID)
                    return;
            }


            // Убираем текущую вершину из строящегося пути, если она не искомая (если сверху искомая - мы нашли путь)
            if (stackPath.Peek() != findingVertexID) {
                verticesInPath[curVertexID - 1] = false;
                stackPath.Pop();
            }
        }

        /// <summary>
        /// Инвертировать аугментальный путь из стека
        /// </summary>
        /// <param name="stackAugmentalPath">Аугментальный путь в стеке</param>
        /// <returns>Аугментальный путь. Если нет - null</returns>
        public int[] GetAugmentalPathFromStack(Stack<int> stackAugmentalPath) {
            int[] augmentalPath;
            // Если путь есть, инвертируем его из стека. Иначе - null
            if (stackAugmentalPath.Count != 0) {
                augmentalPath = new int[stackAugmentalPath.Count];
                for (int i = stackAugmentalPath.Count - 1; i >= 0; i--)
                    augmentalPath[i] = stackAugmentalPath.Pop();
            }
            else
                augmentalPath = null;
            return augmentalPath;
        }

        /// <summary>
        /// Метод получения аугментальной цепи между заданными вершинами сети.
        /// Возвращаемый массив содержит искомую цепь в виде последовательности ID вершин
        /// </summary>
        /// <param name="graph">Сеть (граф)</param>
        /// <param name="startVertex">Начальная вершина цепи</param>
        /// <param name="finishVertex">Конечная вершина цепи</param>
        /// <param name="visitedVertices">Массив, содержащий пометки обо всех посещённых вершинах</param>
        /// <returns></returns>
        public int[] GetAugmentalPath(EduGraph graph, int startVertex, int finishVertex, bool[] visitedVertices) {  
            Stack<int> stackPath = new Stack<int>();
            BuildAugmentalPath(graph, startVertex, finishVertex, new bool[graph.VerticesInfo.Count], visitedVertices, stackPath);

            int[] augmentalPath = GetAugmentalPathFromStack(stackPath);

            return augmentalPath;
        }

        /// <summary>
        /// Метод нахождения величины дополнительного потока, который можно
        /// пустить по аугментальной цепи
        /// </summary>
        /// <param name="graph">Граф (сеть)</param>
        /// <param name="augmentalPath">Аугментальный путь в виде последовательности ID вершин</param>
        /// <param name="egVis">Элемент управления визуализации графа. В случае null визуализация отсутствует</param>
        /// <returns></returns>
        public int GetAugmentalFlow(EduGraph graph, int[] augmentalPath, EduGraphVisualizer egVis = null) {
            int augmentalFlow = int.MaxValue; // искомое значение аугментального потока
            int augmentalPathLength = augmentalPath.Length;
            for (int curVertexIndex = 0; curVertexIndex < augmentalPathLength; curVertexIndex++) {
                // Визуализация => засыпаем на время
                if (egVis != null)
                    Thread.Sleep(400);

                // ID текущей вершины
                int curVertexID = augmentalPath[curVertexIndex];

                // Визуализация => отмечаем текущую вершину
                if (egVis != null) {
                    egVis.MarkVertex(graph[curVertexID], Color.Red);
                    egVis.Update();
                }

                // Если вершина первая - просто ставим метку и идём к следующей вершине
                if (curVertexIndex == 0) {
                    // Визуализация => ставим метку
                    if (egVis != null) {
                        // Засыпаем на время
                        Thread.Sleep(400);
                        graph[curVertexID].OutsideLabel = $"(+{curVertexID};inf)";
                        egVis.DrawGraph();
                        egVis.Update();
                    }
                    continue;
                }

                // ID предыдущей вершины
                int prevVertexID = augmentalPath[curVertexIndex - 1];

                // Выясняем, по какому ребру мы прошли - прямому, или обратному
                bool isForwardEdge; // прямое ли ребро
                if (graph[prevVertexID, curVertexID] != null)
                    isForwardEdge = true;
                else
                    isForwardEdge = false;

                // Визуализация => отмечаем дугу, по которой пришли
                if (egVis != null) {
                    if (isForwardEdge)
                        egVis.MarkEdge(graph[prevVertexID, curVertexID], Color.Red);
                    else
                        egVis.MarkEdge(graph[curVertexID, prevVertexID], Color.Red);
                    egVis.Update();
                }

                // Выясняем, какой поток можно пропустить по пути, которым мы прошли
                if (isForwardEdge)
                    augmentalFlow = Math.Min(augmentalFlow, graph[prevVertexID, curVertexID].GetAugmentalFlow());
                else
                    augmentalFlow = Math.Min(augmentalFlow, graph[curVertexID, prevVertexID].Flow);

                // Визуализация => ставим метку на эту вершину
                if (egVis != null) {
                    // Определяем знак в метке, который ставится перед предыдущей вершиной
                    char sign = isForwardEdge ? '+' : '-';
                    // Засыпаем на время
                    Thread.Sleep(400);
                    // Ставим на вершину метку
                    graph[curVertexID].OutsideLabel = $"({sign}{prevVertexID};{augmentalFlow})";
                    egVis.DrawGraph();
                    egVis.Update();
                }
            }
            return augmentalFlow;
        }

        /// <summary>
        /// Метод запуска дополнительного потока по аугментальной цепи
        /// </summary>
        /// <param name="graph">Граф (сеть)</param>
        /// <param name="augmentalPath">Аугментальный путь в виде массива ID вершин</param>
        /// <param name="augmentalFlow">Величина увеличения потока в этой цепи</param>
        public void StartAugmentalFlow(EduGraph graph, int[] augmentalPath, int augmentalFlow) {
            // Идём со второй вершины, и смотрим на предшествующую ей дугу
            for (int curVertexIndex = 1; curVertexIndex < augmentalPath.Length; curVertexIndex++) {
                // ID текущей вершины
                int curVertexID = augmentalPath[curVertexIndex];
                // ID предыдущей вершины
                int prevVertexID = augmentalPath[curVertexIndex - 1];
                // Выясняем, по какой дуге мы прошли - прямой, или обратной
                bool isForwardEdge; // прямая ли дуга
                if (graph[prevVertexID, curVertexID] != null)
                    isForwardEdge = true;
                else
                    isForwardEdge = false;
                // Пускаем поток по этой дуге
                if (isForwardEdge)
                    graph[prevVertexID, curVertexID].Flow += augmentalFlow;
                else
                    graph[curVertexID, prevVertexID].Flow -= augmentalFlow;
            }
        }

        /// <summary>
        /// Построение минимального разреза из массива разбиения множества вершин
        /// </summary>
        /// <param name="graph">Граф (сеть)</param>
        /// <param name="divisionOfVertices">Массив разбиения множества вершин</param>
        /// <returns></returns>
        public List<EdgeInfo> BuildMinimalCut(EduGraph graph, bool[] divisionOfVertices) {
            List<EdgeInfo> res = new List<EdgeInfo>();
            int verticesCount = graph.VerticesCount;
            // Составляем списки из индексов вершин обоих подмножеств
            List<int> firstDivision = new List<int>();
            List<int> secondDivision = new List<int>();
            for (int i = 0; i < verticesCount; i++)
                if (divisionOfVertices[i])
                    firstDivision.Add(i + 1);
                else
                    secondDivision.Add(i + 1);
            // Добавляем в разрез дуги, соединяющие разбиение
            foreach (var firstDivVertexID in firstDivision)
                foreach (var secondDivVertexID in secondDivision)
                    if (graph[firstDivVertexID, secondDivVertexID] != null)
                        res.Add(graph[firstDivVertexID, secondDivVertexID]);
                    else if (graph[secondDivVertexID, firstDivVertexID] != null)
                        res.Add(graph[secondDivVertexID, firstDivVertexID]);
            return res;
        }

        /// <summary>
        /// Поиск максимального потока
        /// </summary>
        /// <param name="graph">Граф (сеть)</param>
        /// <param name="startVertex">Начальная вершина цепи</param>
        /// <param name="finishVertex">Конечная вершина цепи</param>
        /// <param name="divisionOfVertices">Разбиение множества вершин на два - для построения минимального разреза</param>
        /// <param name="egVis">Элемент управления визуализации графа. В случае null визуализация отсутствует</param>
        /// <returns></returns>
        public int FindMaximalFlow(EduGraph graph, int startVertex, int finishVertex, out List<EdgeInfo> minimalCut, EduGraphVisualizer egVis = null) {
            int maximalFlow = 0; // искомая величина максимального потока
            bool[] divisionOfVertices = new bool[graph.VerticesCount]; // разбиение множества вершин на два подмножества (true/false) - для нахождения разреза
            int[] augmentalPath;
            // Пока у нас есть аугментальная цепь
            while ((augmentalPath = GetAugmentalPath(graph, startVertex, finishVertex, divisionOfVertices)) != null) {
                int augmentalFlow = GetAugmentalFlow(graph, augmentalPath, egVis);
                // Визуализация => засыпаем на время
                if (egVis != null)
                    Thread.Sleep(750);
                StartAugmentalFlow(graph, augmentalPath, augmentalFlow);

                // Визуализация => Рисуем изменения
                if (egVis != null) {
                    egVis.DrawGraph();
                    egVis.Update();
                }
                // Визуализация => засыпаем на время, чистим метки
                if (egVis != null) {
                    Thread.Sleep(1500);
                    graph.ClearVerticesOutsideLabels();
                    egVis.ClearEdgesMarking();
                    egVis.ClearVerticesMarking();
                    egVis.DrawGraph();
                    egVis.Update();
                    Thread.Sleep(1000);
                }
                // Пересоздаём массив посещённых вершин к следующему разу. Если вдруг цепь не найдётся - он сохранится
                divisionOfVertices = new bool[graph.VerticesCount];
            }
            // Суммируем исходящий из истока поток - это и будет максимальным потоком
            foreach (var vertexEdge in graph.Matrix[startVertex].Values)
                maximalFlow += vertexEdge != null ? vertexEdge.Flow : 0;

            // Находим минимальный разрез
            minimalCut = BuildMinimalCut(graph, divisionOfVertices);

            return maximalFlow;
        }
    }
}
