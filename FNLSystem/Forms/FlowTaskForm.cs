using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FNLSystem.Classes;
using MyClassLibrary.GraphClasses;

namespace FNLSystem.Forms {
    /// <summary>
    /// Форма с заданием на поиск максимального потока
    /// </summary>
    public partial class FlowTaskForm : Form {
        // Настроечные атрибуты
        /// <summary>
        /// Режим задания: запущено ли оно для демонстрации
        /// </summary>
        private bool IsDemonstrationMode { get; set; }
        /// <summary>
        /// Отображаются ли метки вершин на графе
        /// </summary>
        private bool IsMarksVisible { get => cbShowMarks.Checked; }
        // Атрибуты, связанные с заданием
        /// <summary>
        /// Решатель - для демонстраций и проверки решения
        /// </summary>
        private Solver Solver { get; set; }
        /// <summary>
        /// Граф
        /// </summary>
        private EduGraph Graph { get; set; }
        /// <summary>
        /// Номера истока
        /// </summary>
        private int StartVertexIndex { get => (int)nudStartVertex.Value; }
        /// <summary>
        /// Номера стока
        /// </summary>
        private int FinishVertexIndex { get => (int)nudFinishVertex.Value; }
        /// <summary>
        /// Максимальный поток для графа-примера
        /// </summary>
        private int GraphMaximalFlow { get; set; }
        /// <summary>
        /// Минимальный разрез для графа-примера
        /// </summary>
        private List<EdgeInfo> GraphMinimalCut { get; set; }
        /// <summary>
        /// Строящийся аугментальный путь (решение задания)
        /// </summary>
        private Stack<int> CurrentAugmentalPathStack { get; set; }
        /// <summary>
        /// Аугментальный поток строящегося аугментального пути (решение задания)
        /// </summary>
        private int CurrentAugmentalFlow { get; set; }
        private List<EdgeInfo> CurrentMinimalCut { get; set; }

        #region Подсчёт ошибок
        private struct MistakesCounter {
            public int incorrectWay;
            public int incorrectMark;
            public int incorrectFlowIncrease;
            public int incorrectMinCutEdge;
        }
        MistakesCounter mistakesCounter;
        #endregion Подсчёт ошибок



        #region Конструктор
        // Метод настройки формы для демонстрации
        private void ConfigurateDemonstrationForm() {
            Text = "Поиск максимального потока - демонстрация"; // заголовок формы
            tbTip.Text =
@"- Добро пожаловать!
- В данном окне вы можете просмотреть демонстрацию алгоритма поиска максимального потока.
- В разделе ""Настройки"" вы можете выбрать номера вершин, между которыми хотите пустить максимальный поток.
- В разделе ""Решение"" находятся кнопки, позволяющие управлять демонстрацией.
Как только максимальный поток будет найден, синим цветом на графе будет выделен минимальный разрез.
Максимальный поток в данном случае будет текущим потоком в графе.
-В разделе ""Помощь"" вы можете открыть текст лекции.
";
            bCommon.Text = "Выполнить шаг";
            ttCommonTip.SetToolTip(bCommon, "Приступить к следующей итерации решения");
            cbShowMarks.Enabled = false;
        }
        

        // Метод настройки формы для решения
        private void ConfiguratePracticalForm() {
            Text = "Поиск максимального потока - практика"; // заголовок формы
            tbTip.Text =
@"- Добро пожаловать!
- В данном окне вы можете в интерактивном режиме отработать нахождение максимального потока.
- В разделе ""Условие задачи"" вы можете выбрать номера вершин, между которыми хотите пустить поток.
Там же вы можете отключить отображение и проверку меток вершин.
- В разделе ""Решение"" находятся кнопки, позволяющие управлять ходом решения задачи.
Решение разбивается на два шага: на поиск аугментальной цепи и запуск дополнительного потока.
Как только вы поймёте, что больше аугментальных цепей от истока к стоку нет, и найденный поток максимален, выберите все дуги,
входящие в минимальный разрез. Решение заканчивается при выборе всех дуг минимального разреза.
- В разделе ""Помощь"" вы можете открыть текст лекции.
";
            bCommon.Text = "К началу шага";
            ttCommonTip.SetToolTip(bCommon, "Перейти к началу текущей итерации решения");
            cbShowMarks.Enabled = true;
            // Включаем интерактивную визуализацию
            eGraphViz.InteractiveMode = true;
            // Подписываемся на события интерактивной визуализации
            eGraphViz.VertexSelectedEvent += CheckSelectedVertex;
            eGraphViz.EdgeSelectedEvent += CheckSelectedEdge;
            // Создаём все коллекции, получаем правильное решение
            GraphMaximalFlow = Solver.FindMaximalFlow(Graph, StartVertexIndex, FinishVertexIndex, out List<EdgeInfo> graphMinimalCut);
            GraphMinimalCut = graphMinimalCut;
            Graph.ClearGraphFlow(); // очищаем поток в графе после применения алгоритма (там максимальный поток)
            CurrentAugmentalPathStack = new Stack<int>();
            CurrentMinimalCut = new List<EdgeInfo>();
            mistakesCounter = new MistakesCounter(); // стандартный констуктор структуры, присваивает полям значения по умолчанию (нули, у нас)
        }


        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="graphStruct">Структура, содержащая информацию о графе-примере</param>
        /// <param name="isDemonstrationMode">Значение, задающее режим задания. True включает режим демонстрации</param>
        public FlowTaskForm(GraphStruct graphStruct, bool isDemonstrationMode = false) {
            InitializeComponent();
            IsDemonstrationMode = isDemonstrationMode;
            // Создаём по данным графа объект EduGraph
            Graph = new EduGraph(graphStruct.adjacencyMatrix, graphStruct.verticesCoordinates);
            // Инициализируем статическую визуализацию
            eGraphViz.Initialize(Graph);
            // Создаём решатель
            this.Solver = new Solver();
            // Настраиваем поля для выбора истока и стока
            nudStartVertex.Maximum = nudFinishVertex.Maximum = Graph.VerticesCount;
            nudStartVertex.Minimum = nudFinishVertex.Minimum = 1;
            // Указываем исток и сток по умолчанию
            nudStartVertex.Value = graphStruct.startVertexIndex;
            nudFinishVertex.Value = graphStruct.finishVertexIndex;
            // Отмечаем, что по умолчанию метки на вершинах графа будут отображаться
            cbShowMarks.Checked = true;
            // Задаём общие подсказки для элементов управления
            ttCommonTip.SetToolTip(bClearAll, "Сбросить текущее решение и начать заново");
            ttCommonTip.SetToolTip(bLecture, "Открыть текст лекции");
            // Настраиваем форму в зависимости от режима задания - меняем надписи, подписываемся на события...
            if (isDemonstrationMode)
                ConfigurateDemonstrationForm();
            else
                ConfiguratePracticalForm();
            // Выделяем общую кнопку, чтобы после изменения текста tbTip он не был выделен
            bCommon.Select();
        }
        #endregion Конструктор


        #region Общие методы
        // Очистить все метки графа
        private void ClearGraphMarks() {
            eGraphViz.ClearEdgesMarking();
            eGraphViz.ClearVerticesMarking();
            Graph.ClearVerticesOutsideLabels();
            eGraphViz.DrawGraph();
        }

        // Очистить решение
        private void ClearAllSolution() {
            Graph.ClearGraphFlow();
            ClearGraphMarks();
            // Очищаем все коллекции и обнуляем аугментальный поток, если они созданы (возможно, вызов был из конструктора - до создания списков)
            if (CurrentAugmentalPathStack != null) {
                CurrentAugmentalPathStack.Clear();
                CurrentMinimalCut.Clear();
                CurrentAugmentalFlow = 0;
            }
            // Обнуляем все ошибки - пересоздаём структуру
            mistakesCounter = new MistakesCounter();
        }
        #endregion Общие методы



        // Сделать шаг демонстрационного решения
        private void DoDemonstrationStep() {
            // Блокируем управление демонстрацией - чтоб было видно, что демонстрация идёт
            gbWork.Enabled = gbTaskOptions.Enabled = false;
            // Чистим метки в графах
            ClearGraphMarks();
            bool[] visitedVertices = new bool[Graph.VerticesCount];
            // Находим аугментальную цепь
            int[] AugmentalPath = this.Solver.GetAugmentalPath(Graph, StartVertexIndex, FinishVertexIndex, visitedVertices);
            int augmentalFlow; // аугментальный поток
            // Если нашлась цепь - визуализируем, получаем аугментальный поток, пускаем его в граф
            if (AugmentalPath != null) {
                augmentalFlow = this.Solver.GetAugmentalFlow(Graph, AugmentalPath, eGraphViz);
                // Запуск аугментального потока
                this.Solver.StartAugmentalFlow(Graph, AugmentalPath, augmentalFlow);
                eGraphViz.DrawGraph();
            }
            // Иначе - показываем минимальный разрез
            else {
                List<EdgeInfo> minimalCut = this.Solver.BuildMinimalCut(Graph, visitedVertices);
                foreach (var edge in minimalCut)
                    eGraphViz.MarkEdge(edge, Color.Blue);
                bCommon.Enabled = false; // закрываем доступ к кнопке следущего шага
                MessageBox.Show($"Величина максимального потока: {Graph.GetCurrentFlow(StartVertexIndex)}",
                    "Максимальный поток", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Открываем управление демонстрацией
            gbWork.Enabled = gbTaskOptions.Enabled = true;
        }


        #region Решение
        /// <summary>
        /// Получение дуги (прямой или обратной), соединяющие вершины v1 и v2
        /// </summary>
        /// <param name="firstVertexIndex">Индекс первой вершины</param>
        /// <param name="secondVertexIndex">Индекс второй вершины</param>
        /// <param name="isForwardEdge">true - если дуга от v1 к v2; false - если дуга от v2 к v1</param>
        /// <returns>Дуга</returns>
        private EdgeInfo GetEdgeBetweenVertices(int firstVertexIndex, int secondVertexIndex, out bool isForwardEdge) {
            // Если есть прямая дуга - возвращаем
            if (Graph[firstVertexIndex, secondVertexIndex] != null) {
                isForwardEdge = true;
                return Graph[firstVertexIndex, secondVertexIndex];
            }
            // Если есть обратная дуга - возврщаем
            else if (Graph[secondVertexIndex, firstVertexIndex] != null) {
                isForwardEdge = false;
                return Graph[secondVertexIndex, firstVertexIndex];
            }
            // Если нет ни той, ни другой - ничего
            else {
                isForwardEdge = true;
                return null;
            }
        }


        // Метод проверки первой добавляемой в аугментальный путь вершины (подметод для метода CheckAugmentalPath)
        // Вернёт true - если всё правильно, false - иначе
        private bool CheckFirstVertexInAugPath(VertexInfo selectedVertex) {
            if (selectedVertex.ID != StartVertexIndex) {
                mistakesCounter.incorrectWay++;
                MessageBox.Show("Начинать надо от истока, и искать путь к стоку!", "Упс!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else {
                CurrentAugmentalFlow = int.MaxValue;
                CurrentAugmentalPathStack.Push(selectedVertex.ID);
                eGraphViz.MarkVertex(selectedVertex, Color.Red);
                // Если метки учитываются - ставим
                if (IsMarksVisible)
                    Graph[selectedVertex.ID].OutsideLabel = $"(+{selectedVertex.ID};inf)";
                eGraphViz.DrawGraph();
            }
            return true;
        }

        // Проверить, правильно ли была выбрана следующая вершина с точки зрения дуги от последней вершины пути к ней (подметод для метода CheckAugmentalPath)
        // Вернёт true - если переход к этой вершине допустим, false - иначе
        private bool CheckEdgeToSelectedVertex(VertexInfo selectedVertex, EdgeInfo edge, bool isForwardEdge) {
            // Если есть прямая дуга
            if (isForwardEdge) {
                if (edge.GetAugmentalFlow() == 0) {
                    mistakesCounter.incorrectWay++;
                    MessageBox.Show("Дополнительный поток по этой дуге нулевой - больше не пройдёт. Выбери другую вершину!", "Упс!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else {
                    eGraphViz.MarkEdge(edge, Color.Red);
                    eGraphViz.MarkVertex(selectedVertex, Color.Red);
                    CurrentAugmentalFlow = Math.Min(CurrentAugmentalFlow, edge.GetAugmentalFlow());
                    CurrentAugmentalPathStack.Push(selectedVertex.ID);
                }
            }
            // Если есть обратная дуга
            else {
                if (edge.Flow == 0) {
                    mistakesCounter.incorrectWay++;
                    MessageBox.Show("Поток по этой обратной дуге нулевой - не пройти. Выбери другую вершину!", "Упс!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else {
                    eGraphViz.MarkEdge(edge, Color.Red);
                    eGraphViz.MarkVertex(selectedVertex, Color.Red);
                    CurrentAugmentalFlow = Math.Min(CurrentAugmentalFlow, edge.Flow);
                    CurrentAugmentalPathStack.Push(selectedVertex.ID);
                }
            }
            return true;
        }

        // Метод проверки метки на вершины (подметод для метода CheckAugmentalPath)
        // Вернёт true - если верно, false - иначе
        private bool CheckVertexMark(int prevVertexIndex, VertexInfo selectedVertex, EdgeInfo edge, bool isForwardEdge, int prevAugmentalFlow) {
            // Если метки не учитываются - выходим
            if (!IsMarksVisible)
                return true;
            char sign = isForwardEdge ? '+' : '-';
            VertexLabelForm vlForm = new VertexLabelForm(Graph.VerticesCount, sign, prevVertexIndex, CurrentAugmentalFlow);
            vlForm.ShowDialog();
            mistakesCounter.incorrectMark += vlForm.MistakesCount;
            if (vlForm.DialogResult != DialogResult.OK) {
                eGraphViz.ClearVertexMarking(selectedVertex);
                eGraphViz.ClearEdgeMarking(edge);
                CurrentAugmentalPathStack.Pop();
                CurrentAugmentalFlow = prevAugmentalFlow;
                return false;
            }
            Graph[selectedVertex.ID].OutsideLabel = $"({(isForwardEdge ? '+' : '-')}{prevVertexIndex};{CurrentAugmentalFlow})";
            eGraphViz.DrawGraph();
            return true;
        }

        // Метод проверки увеличения потока после построения аугментальной цепи (подметод для метода CheckAugmentalPath)
        // Вернёт true - если верно, false - иначе
        private bool CheckLastVertexInAugPath(VertexInfo selectedVertex, EdgeInfo edge, int prevAugmentalFlow) {
            MessageBox.Show("Сток достигнут. Время увеличивать поток!", "Ура!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AugmentalFlowForm afForm = new AugmentalFlowForm(CurrentAugmentalFlow);
            afForm.ShowDialog();
            mistakesCounter.incorrectFlowIncrease += afForm.MistakesCount;
            if (afForm.DialogResult != DialogResult.OK) {
                eGraphViz.ClearVertexMarking(selectedVertex);
                eGraphViz.ClearEdgeMarking(edge);
                CurrentAugmentalPathStack.Pop();
                CurrentAugmentalFlow = prevAugmentalFlow;
                // Если метки учитываются
                if (IsMarksVisible)
                    Graph[selectedVertex.ID].OutsideLabel = "";
                return false;
            }
            Solver.StartAugmentalFlow(Graph, Solver.GetAugmentalPathFromStack(CurrentAugmentalPathStack), CurrentAugmentalFlow);
            ClearGraphMarks();
            return true;
        }


        // Метод контроля построения аугментального потока и увеличения потока в графе
        private void CheckAugmentalPath(VertexInfo selectedVertex) {
            // Если добавляется первая вершина
            if (CurrentAugmentalPathStack.Count == 0) {
                // Проверяем правильность. Если неверно - выходим
                if (!CheckFirstVertexInAugPath(selectedVertex))
                    return;
            }
            // Если добавляется промежуточная вершина
            else {
                int prevAugmentalFlow = CurrentAugmentalFlow;
                int prevVertexIndex = CurrentAugmentalPathStack.Peek();
                // Получаем дугу
                EdgeInfo edge = GetEdgeBetweenVertices(prevVertexIndex, selectedVertex.ID, out bool isForwardEdge);
                // Если между выбранной вершиной нет дуги с предыдущей
                if (edge == null) {
                    mistakesCounter.incorrectWay++;
                    MessageBox.Show("Путь нужно строить последовательно, переход допускается только между соседними вершинами!", "Упс!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    // Проверяем допустимость перехода. Если нельзя - выходим
                    if (!CheckEdgeToSelectedVertex(selectedVertex, edge, isForwardEdge))
                        return;
                    // Ставим метку на вершину. Если отменилось - выходим
                    if (!CheckVertexMark(prevVertexIndex, selectedVertex, edge, isForwardEdge, prevAugmentalFlow))
                        return;
                    // Если был выбран сток
                    if (selectedVertex.ID == FinishVertexIndex) {
                        if (!CheckLastVertexInAugPath(selectedVertex, edge, prevAugmentalFlow))
                            return;
                    }
                }
            }
        }

        // Метод контроля построения минимального разреза
        private void CheckMinimalCut(EdgeInfo edge) {
            if (Graph.GetCurrentFlow(StartVertexIndex) != GraphMaximalFlow) {
                mistakesCounter.incorrectMinCutEdge++;
                MessageBox.Show("Находить минимальный разрез ещё рано! Поток не максимален!", "Упс!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!GraphMinimalCut.Contains(edge)) {
                mistakesCounter.incorrectMinCutEdge++;
                MessageBox.Show("Эта дуга не входит в минимальный разрез!", "Упс!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else {
                eGraphViz.MarkEdge(edge, Color.Blue);
                CurrentMinimalCut.Add(edge);
                // Проверим, а не нашёл ли студент весь минимальный разрез
                if (GraphMinimalCut.Count == CurrentMinimalCut.Count) {
                    MessageBox.Show("Задание выполнено! Максимальный поток и минимальный разрез найдены!", "Ура!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    eGraphViz.InteractiveMode = false;
                    bCommon.Enabled = false;
                    var statForm = new TaskStatisticsForm(mistakesCounter.incorrectWay, mistakesCounter.incorrectMark, 
                        mistakesCounter.incorrectFlowIncrease, mistakesCounter.incorrectMinCutEdge);
                    statForm.ShowDialog();
                }
            }
        }
        #endregion Решение



        #region Обработчики событий контролов
        // Вызывать текст лекции
        private void bLecture_Click(object sender, EventArgs e) {
            // Открываем файл лекции
            var lectureBox = new LectureBox();
            lectureBox.ShowLecture();
        }

        // Кнопка, общая для демонстрации (следующий шаг) и решения (к началу шага)
        private void bCommon_Click(object sender, EventArgs e) {
            if (IsDemonstrationMode)
                DoDemonstrationStep();
            else {
                ClearGraphMarks();
                CurrentAugmentalPathStack.Clear();
                CurrentAugmentalFlow = 0;
                CurrentMinimalCut.Clear();
            }
        }

        // Сбросить всё решение
        private void bClearAll_Click(object sender, EventArgs e) {
            ClearAllSolution();
            // Если вдруг задание было решено и сброшено, возвращаем активность кнопке сброса шага
            bCommon.Enabled = true;
            // И вновь включаем интерактивный режим для решения задания
            if (!IsDemonstrationMode)
                eGraphViz.InteractiveMode = true;
        }

        // При изменении условия задачи всё решение сбрасывается
        private void nudStartVertex_ValueChanged(object sender, EventArgs e) {
            ClearAllSolution();
        }

        // При изменении условия задачи всё решение сбрасывается
        private void nudFinishVertex_ValueChanged(object sender, EventArgs e) {
            ClearAllSolution();
        }

        // Проверка ответа - обработчик на событие выбора вершины графа
        private void CheckSelectedVertex(VertexInfo selectedVertex) {
            // Проверим, не выбрана ли уже выбранная вершина. Их мы откидываем
            foreach (int vertIndex in CurrentAugmentalPathStack)
                if (vertIndex == selectedVertex.ID)
                    return;
            // Если всё хорошо - проверяем дальше
            CheckAugmentalPath(selectedVertex);
        }

        // Проверка ответа - обработчик на событие выбора дуги графа
        private void CheckSelectedEdge(EdgeInfo selectedEdge) {
            // Проверим, не выбрана ли уже выбранная дуга
            if (CurrentMinimalCut.Contains(selectedEdge))
                return;
            // Если всё хорошо - проверяем дальше
            CheckMinimalCut(selectedEdge);
        }
        #endregion Обработчики событий контролов 
    }
}
