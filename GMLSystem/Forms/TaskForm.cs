using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMLSystem.Classes;
using MyClassLibrary.GraphClasses;

namespace GMLSystem.Forms {
    /// <summary>
    /// Форма с заданием на поиск максимального потока
    /// </summary>
    public partial class TaskForm : Form {
        /// <summary>
        /// Граф
        /// </summary>
        private EduGraph Graph { get; set; }

        

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="graphStruct">Структура, содержащая информацию о графе-примере</param>
        public TaskForm(GraphStruct graphStruct) {
            InitializeComponent();
            // Создаём по данным графа объект EduGraph
            Graph = new EduGraph(graphStruct.adjacencyMatrix, graphStruct.verticesCoordinates);
            // Инициализируем статическую визуализацию
            eGraphViz.Initialize(Graph, isEdgesMarksVisible: false, isArrowsVisible: false);
            // Задаём общие подсказки для элементов управления
            ttCommonTip.SetToolTip(bClearAll, "Сбросить текущее решение и начать заново");
            ttCommonTip.SetToolTip(bLecture, "Открыть текст лекции");
            ttCommonTip.SetToolTip(bDoStep, "Приступить к следующей итерации решения");
            // Задаём текст подсказки
            tbTip.Text =
@"- Добро пожаловать!
- В данном окне вы можете просмотреть демонстрацию алгоритма поиска максимального потока.
- В разделе ""Настройки"" вы можете выбрать номера вершин, между которыми хотите пустить максимальный поток.
- В разделе ""Решение"" находятся кнопки, позволяющие управлять демонстрацией.
Как только максимальный поток будет найден, синим цветом на графе будет выделен минимальный разрез.
Максимальный поток в данном случае будет текущим потоком в графе.
-В разделе ""Помощь"" вы можете открыть текст лекции.
";
            // Выделяем общую кнопку, чтобы после изменения текста tbTip он не был выделен
            bDoStep.Select();
        }

        bool isDemonstrationStarted = false;
        int curVertex;
        bool[] usedVertices;
        int[] matching;

        bool DFS (int vertex) {
            if (usedVertices[vertex])
                return false;
            usedVertices[vertex] = true;
            for (int nextVertex = 1; nextVertex <= Graph.VerticesCount; nextVertex++) {
                if (Graph.Matrix[vertex][nextVertex] == null)
                    continue;
                if (matching[nextVertex] == -1 || DFS(matching[nextVertex])) {
                    matching[nextVertex] = vertex;
                    return true;
                }
            }
            return false;
        }

        // Сделать шаг демонстрационного решения
        private void bDoStep_Click(object sender, EventArgs e) {
            if (!isDemonstrationStarted) {
                eGraphViz.ClearVerticesMarking();
                eGraphViz.ClearEdgesMarking();
                isDemonstrationStarted = true;
                matching = new int[Graph.VerticesCount + 1]; // +1, т.к. нумерация вершин в графе у нас с 1
                for (int i = 1; i <= Graph.VerticesCount; i++)
                    matching[i] = -1;
                usedVertices = new bool[Graph.VerticesCount + 1];
                curVertex = 1;
            }
            if (curVertex > Graph.VerticesCount)
                return;
            eGraphViz.ClearVerticesMarking();
            eGraphViz.MarkVertex(Graph.VerticesInfo[curVertex], Color.Red);
            for (int i = 1; i <= Graph.VerticesCount; i++)
                usedVertices[i] = false;
            if (DFS(curVertex)) {
                eGraphViz.ClearEdgesMarking();
                eGraphViz.Update();
                for (int i = 1; i <= Graph.VerticesCount; i++)
                    if (matching[i] != -1)
                        eGraphViz.MarkEdge(Graph[i, matching[i]], Color.LightGreen);
                eGraphViz.DrawGraph();
                eGraphViz.Update();
            }

            curVertex++;
        }

        private void bClearAll_Click(object sender, EventArgs e) {
            isDemonstrationStarted = false;
        }

        private void DoDemonstrationStep() {
            //// Находим аугментальную цепь
            //int[] AugmentalPath = this.Solver.GetAugmentalPath(Graph, 0, 0, visitedVertices);
            //int augmentalFlow; // аугментальный поток
            //// Если нашлась цепь - визуализируем, получаем аугментальный поток, пускаем его в граф
            //if (AugmentalPath != null) {
            //    augmentalFlow = this.Solver.GetAugmentalFlow(Graph, AugmentalPath, eGraphViz);
            //    // Запуск аугментального потока
            //    this.Solver.StartAugmentalFlow(Graph, AugmentalPath, augmentalFlow);
            //    eGraphViz.DrawGraph();
            //}
            //// Иначе - показываем минимальный разрез
            //else {
            //    List<EdgeInfo> minimalCut = this.Solver.BuildMinimalCut(Graph, visitedVertices);
            //    foreach (var edge in minimalCut)
            //        eGraphViz.MarkEdge(edge, Color.Blue);
            //    bCommon.Enabled = false; // закрываем доступ к кнопке следущего шага
            //    MessageBox.Show($"Величина максимального потока: {Graph.GetCurrentFlow(0)}",
            //        "Максимальный поток", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //// Открываем управление демонстрацией
            //gbWork.Enabled = gbTaskOptions.Enabled = true;
        }


        // Вызывать текст лекции
        private void bLecture_Click(object sender, EventArgs e) {
            // Открываем файл лекции
            var lectureBox = new LectureBox();
            lectureBox.ShowLecture();
        }
    }
}
