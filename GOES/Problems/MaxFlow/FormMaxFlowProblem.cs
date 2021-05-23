using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using SGVL.Visualizers;
using SGVL.Graphs;
using GOES.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace GOES.Problems.MaxFlow {
    public partial class FormMaxFlowProblem : Form, IProblem {
        // ----Атрибуты задачи
        MaxFlowProblemExample maxFlowExample;
        ProblemMode problemMode;
        MaxFlowProblemState problemState;
        IGraphVisualizer graphVisInterface;
        Graph visGraph;


        // ----Атрибуты сети
        int sourceVertexIndex;
        int targetVertexIndex;
        int[,] capacityMatrix;
        int[,] flowMatrix;
        int verticesCount;
        // ----Атрибуты для алгоритма решения
        List<int> curAugmentalPath;
        int curAugmentalFlowValue;
        List<Tuple<int, int>> curCutEdges;
        // ----Атрибуты с правильными ответами (правильной величиной максимального потока правильными рёбрами минимального разреза)
        int correctMaxFlowValue;
        List<Tuple<int, int>> correctMinimalCut;


        // ----Интерфейс задачи
        public void InitializeProblem(ProblemExample example, ProblemMode mode) {
            // Если требуется случайная генерация, а её нет, говорим, что не реализовано
            if (example == null && !ProblemDescriptor.IsRandomExampleAvailable)
                throw new NotImplementedException("Случайная генерация примеров не реализована");
            // Если нам дан пример не задачи о максимальном потоке - ошибка
            maxFlowExample = example as MaxFlowProblemExample;
            if (maxFlowExample == null)
                throw new ArgumentException("Ошибка в выбранном примере. Его невозможно открыть.");
            // В зависимости от режима задачи (решение/демонстрация) меняем некоторые элементы управления
            problemMode = mode;
            if (problemMode == ProblemMode.Solution) {
                buttonReloadIteration.Text = "К началу итерации";
                buttonReloadProblem.Text = "Начать заново";
                groupBoxAnswers.Enabled = true;
            }
            else if (problemMode == ProblemMode.Demonstration) {
                buttonReloadIteration.Text = "Сделать шаг";
                buttonReloadProblem.Text = "Начать заново";
                groupBoxAnswers.Enabled = false;
            }
            // Создаём граф для визуализации по примеру (если нужна генерация, генерируем)
            if (example != null) {
                visGraph = new Graph(maxFlowExample.GraphMatrix, maxFlowExample.IsGraphDirected);
                // Задаём расположение вершин графа
                for (int i = 0; i < visGraph.VerticesCount; i++)
                    visGraph.Vertices[i].DrawingCoords = maxFlowExample.DefaultGraphLayout[i];
            }
            else
                visGraph = new Graph(maxFlowExample.GraphMatrix, maxFlowExample.IsGraphDirected); // TODO: генерация
            graphVisInterface.Initialize(visGraph);
            // Сохраняем матрицу графа, исток и сток для решения. Создаём нужные коллекции
            capacityMatrix = (int[,])maxFlowExample.CapacityMatrix.Clone();
            verticesCount = maxFlowExample.GraphMatrix.GetLength(0);
            sourceVertexIndex = maxFlowExample.SourceVertexIndex;
            targetVertexIndex = maxFlowExample.TargetVertexIndex;
            flowMatrix = new int[verticesCount, verticesCount];
            curAugmentalPath = new List<int>();
            curCutEdges = new List<Tuple<int, int>>();
            // Пишем условие задачи - номер истока и номер стока
            textLabelExampleDescription.Text = 
                $"Исток: {maxFlowExample.SourceVertexIndex + 1}; Сток: {maxFlowExample.TargetVertexIndex + 1}";
            // Выделяем исток и сток жирным
            visGraph.Vertices[maxFlowExample.SourceVertexIndex].Bold = true;
            visGraph.Vertices[maxFlowExample.TargetVertexIndex].Bold = true;
            // Отображаем метки величины потока на дугах
            UpdateEdgesLabels();
            // Получаем решение задачи
            Algorithm.GetMaxFlowSolve(capacityMatrix, sourceVertexIndex, targetVertexIndex, out correctMinimalCut, out correctMaxFlowValue);
            // Ставим решение в состояние ожидания начала
            SetStartWaitingState();
        }

        public IProblemDescriptor ProblemDescriptor => new MaxFlowProblemDescriptor();


        // ----Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        public FormMaxFlowProblem() {
            InitializeComponent();
            graphVisInterface = graphVisualizer;
            graphVisInterface.EdgeSelectedEvent += EdgeSelectedHandler;
            graphVisInterface.VertexSelectedEvent += VertexSelectedHandler;
        }


        // ----Методы для работы с ходом решения/демонстрации
        // Начать новую итерацию решения
        private void StartNewIteration() {
            // Очищаем текущий маршрут и величину аугментального потока
            curAugmentalPath.Clear();
            curAugmentalFlowValue = 0;
            // Убираем метки с вершин графа
            foreach (var vertex in visGraph.Vertices)
                vertex.Label = "";
            graphVisInterface.ResetVerticesBorderColor();
            graphVisInterface.ResetEdgesColor();
        }


        // ----Методы для отображения сообщений и подсказок
        // Вывести обычное сообщение
        void ShowStandardTip(string message) {
            textLabelTip.Text = message;
            groupBoxTip.ForeColor = SystemColors.ControlText; // стандартный цвет для надписи на GroupBox
        }

        // Вывести сообщение об ошибке
        void ShowErrorTip(string message) {
            textLabelTip.Text = message;
            groupBoxTip.ForeColor = Color.Red;
        }

        // Вывести сообщение об успехе
        void ShowSuccessTip(string message) {
            textLabelTip.Text = message;
            groupBoxTip.ForeColor = Color.Green;
        }


        // ----Методы для работы с блоком ответов
        // Заблокировать блок для ответов
        void LockAnswerGroupBox() {
            textBoxAnswer.Text = string.Empty;
            groupBoxAnswers.Enabled = false;
        }

        // Разблокировать блок для ответов
        void UnlockAnswerGroupBox() {
            textBoxAnswer.Text = string.Empty;
            groupBoxAnswers.Enabled = true;
        }

        // ----Методы для работы с полем ввода ответов
        // Проверить ответ на соответствие формату ввода в зависимости от требуемого ответа (метки вершины, числа)
        private bool IsAnswerCorrect() {
            string answer = textBoxAnswer.Text.Trim().ToLower();
            Regex regex;
            switch (problemState) {
                case MaxFlowProblemState.PathVertexLabelWaiting:
                    // Метки можно не ставить
                    if (answer == string.Empty)
                        return true;
                    // Первым символом должен быть знак (+ или -)
                    // После знака должно идти целое число, затем через пробел ещё одно целое число либо "inf" (для первой вершины)
                    regex = new Regex(@"^(\+|-)\d+ (\d+|inf)$");
                    return regex.IsMatch(answer);
                case MaxFlowProblemState.FlowRaiseWaiting:
                    // Должно быть одно целое число
                    regex = new Regex(@"^\d+$");
                    return regex.IsMatch(answer);
                case MaxFlowProblemState.MaximalFlowWaiting:
                    // Должно быть одно целое число
                    regex = new Regex(@"^\d+$");
                    return regex.IsMatch(answer);
                default:
                    return false;
            }
        }


        // ----Методы для работы с визуализацией графа
        // Обновить метки на дугах графа вида <текущий поток>/<пропускная способность>
        void UpdateEdgesLabels() {
            for (int sourceVertex = 0; sourceVertex < verticesCount; sourceVertex++)
                for (int targetVertex = 0; targetVertex < verticesCount; targetVertex++)
                    if (capacityMatrix[sourceVertex, targetVertex] != 0)
                        visGraph.GetEdge(sourceVertex, targetVertex).Label =
                            $"{flowMatrix[sourceVertex, targetVertex]}/{capacityMatrix[sourceVertex, targetVertex]}";
        }


        // ----Методы изменения состояния (этапа) решения задания
        // Перевести задачу в состояние ожидания начала
        void SetStartWaitingState() {
            problemState = MaxFlowProblemState.StartWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message =
                        "Добро пожаловать в задание \"Максимальный поток в сети\"!" + Environment.NewLine +
                        "Ваша задача - найти максимальный поток в представленной сети, а затем - " +
                        "её минимальный разрез." + Environment.NewLine +
                        "Стройте аугментальные маршруты от истока к стоку. " +
                        "Для этого выбирайте вершины маршрута, начав с истока, и закончив в стоке." + Environment.NewLine +
                        $"Исток - вершина {maxFlowExample.SourceVertexIndex + 1}, сток - вершина {maxFlowExample.TargetVertexIndex + 1}." + Environment.NewLine +
                        "Если при построении маршрута Вы зашли в тупик, можете сбросить построенный маршрут кнопкой \"К началу итерации\", " +
                        "это не повлияет на оценку." + Environment.NewLine +
                        "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                        "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                        "Чтобы начать решение, нажмите кнопку \"К началу итерации\".";
            ShowSuccessTip(message);
            LockAnswerGroupBox();
        }

        // Перевести задачу в состояние ожидания следующей вершины для аугментального пути
        void SetNextPathVertexWaiting() {
            problemState = MaxFlowProblemState.NextPathVertexWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.Interactive;
            graphVisInterface.IsVerticesMoving = true;
            string message =
                "Выберите следующую вершину для аугментального пути." + Environment.NewLine;
            if (curAugmentalPath.Count != 0) {
                // Формируем строку с текущим аугментальным маршрутом
                StringBuilder pathStr = new StringBuilder();
                foreach (var pathVertex in curAugmentalPath)
                    pathStr.Append($"{pathVertex + 1}-");
                // Удаляем последнюю чёрточку
                pathStr.Remove(pathStr.Length - 1, 1);
                message += $"Текущий аугментальный путь: {pathStr}";
            }
            ShowStandardTip(message);
            LockAnswerGroupBox();
        }

        // Перевести задачу в состояние ожидания метки выбранной вершины для аугментального пути
        void SetPathVertexLabelWaiting() {
            problemState = MaxFlowProblemState.PathVertexLabelWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message =
                "Введите метку для вершины в поле для ответов, а затем нажмите кнопку \"Принять ответ\"." + Environment.NewLine +
                "Метка вводится в формате:" + Environment.NewLine +
                "\"<знак><предыдущая вершина> <текущий аугментальный поток>\"," + Environment.NewLine +
                "например: \"+1 inf\" или \"-7 19\"." + Environment.NewLine +
                "Для вершины-истока текущий аугментальный поток следует принять бесконечным: \"inf\"." + Environment.NewLine +
                "Метка проверяется на правильность, и ошибочная метка будет отмечена как ошибка (кроме опечаток)." + Environment.NewLine +
                "Вы можете не ставить метку: для этого оставьте поле ввода ответа пустым.";
            ShowStandardTip(message);
            UnlockAnswerGroupBox();
        }

        // Перевести задачу в состояние ожидания величины аугментального потока для построенного аугментального пути
        void SetFlowRaiseWaiting() {
            problemState = MaxFlowProblemState.FlowRaiseWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message =
                "Аугментальный путь построен." + Environment.NewLine +
                "Введите в поле для ответов величину дополнительного потока, который можно пустить по построенному аугментальному пути, в виде одного целого числа.";
            ShowSuccessTip(message);
            UnlockAnswerGroupBox();
        }

        // Перевести задачу в состояние ожидания величины максимального потока для построеннного максимального потока сети
        void SetMaximalFlowWaiting() {
            problemState = MaxFlowProblemState.MaximalFlowWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message =
                "Максимальный поток в сети построен." + Environment.NewLine +
                "Введите в поле для ответов величину построенного максимального потока в виде одного целого числа.";
            ShowSuccessTip(message);
            UnlockAnswerGroupBox();
        }

        // Перевести задачу в состояние ожидания следующей дуги минимального разреза
        void SetNextCutEdgeWaiting() {
            problemState = MaxFlowProblemState.NextCutEdgeWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.Interactive;
            graphVisInterface.IsVerticesMoving = true;
            string message =
                "Выберите все дуги, которые входят в минимальный разрез данной сети." + Environment.NewLine;
            if (curCutEdges.Count != 0) {
                // Формируем строку с текущими выбранными дугами разреза
                StringBuilder cutEdgesStr = new StringBuilder();
                foreach (var cutEdge in curCutEdges)
                    cutEdgesStr.Append($"{{{cutEdge.Item1 + 1}-{cutEdge.Item2 + 1}}}, ");
                // Удаляем последнюю запятую и пробел
                cutEdgesStr.Remove(cutEdgesStr.Length - 2, 2);
                message += $"Уже отмеченные дуги минимального разреза: {cutEdgesStr}.";
            }
            ShowStandardTip(message);
            LockAnswerGroupBox();
        }

        // Перевести задачу в состояние выполненной задачи
        void SetProblemFinish() {
            problemState = MaxFlowProblemState.ProblemFinish;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message = 
                "Максимальный поток и минимальный разрез в сети найдены." + Environment.NewLine +
                "Задание успешно выполнено!";
            ShowSuccessTip(message);
            LockAnswerGroupBox();
        }


        // ----Методы для работы с ошибками студента
        // Отметить допущенную студентом ошибку
        void MarkError(MaxFlowError error) {
            string errorMessage;
            switch (error) {
                case MaxFlowError.StartOnNonSourceVertex:
                    errorMessage = "Построение аугментального пути необходимо начинать с истока! Какая вершина является для данной сети истоком?";
                    break;
                case MaxFlowError.MoveToFarVertex:
                    errorMessage =
                        "Вы попытались перейти в вершину, которая не соединена дугой с последней вершиной построенного маршрута." + Environment.NewLine +
                        $"Последней (текущей) вершиной маршрута является вершина {curAugmentalPath.Last() + 1}.";
                    break;
                case MaxFlowError.ForwardEdgeIsFull:
                    errorMessage =
                        "Вы попытались пройти по прямой дуге, поток в которой уже максимален, и увеличить его невозможно.";
                    break;
                case MaxFlowError.BackEdgeIsEmpty:
                    errorMessage =
                        "Вы попытались пройти по обратной дуге, поток в которой нулевой, и уменьшить его невозможно.";
                    break;
                case MaxFlowError.IncorrectVertexLabelFormat:
                    errorMessage =
                        "Неправильный формат метки." +
                        "Метка вводится в формате:" + Environment.NewLine +
                        "\"<знак><предыдущая вершина> <текущий аугментальный поток>\"," + Environment.NewLine +
                        "например: \"+1 inf\" или \"-7 19\"." + Environment.NewLine +
                        "Для вершины-истока текущий аугментальный поток следует принять бесконечным: \"inf\".";
                    break;
                case MaxFlowError.IncorrectVertexLabel:
                    errorMessage =
                        "Ошибка в метке. Перепроверьте её, и попробуйте снова." + Environment.NewLine +
                        "Знак в метке отражает направление добавленной к маршруту дуги: прямое (+) или обратное (-)." + 
                        " Это также указывает на то, как необходимо поступать с потоком в добавленной дуге: увеличивать (+) или уменьшать (-) его." + Environment.NewLine +
                        "Предыдущая вершина - это предыдущая посещённая вершина." + Environment.NewLine + 
                        "Аугментальный поток - это поток, который дополнительно можно провести по текущему построенному маршруту." + Environment.NewLine +
                        "Формат ввода: <знак><предыдущая вершина> <аугментальный поток>.";

                    break;
                case MaxFlowError.IncorrectFlowRaiseFormat:
                    errorMessage =
                        "Неправильный формат величины аугментального потока." + Environment.NewLine +
                        "Величина аугментального потока вводится в формате одного целого числа.";
                    break;
                case MaxFlowError.IncorrectFlowRaise:
                    errorMessage =
                        "Неправильная величина аугментального потока. Пересчитайте её, и попробуйте снова.";
                    break;
                case MaxFlowError.IncorrectMaxFlowFormat:
                    errorMessage =
                        "Неправильный формат величины максимального потока." + Environment.NewLine +
                        "Величина максимального потока вводится в формате одного целого числа.";
                    break;
                case MaxFlowError.IncorrectMaxFlowValue:
                    errorMessage =
                        "Неправильная величина построенного максимального потока. Пересчитайте её, и попробуйте снова.";
                    break;
                case MaxFlowError.IncorrectMinCutEdge:
                    errorMessage =
                        "Выбранная Вами дуга не входит в минимальный разрез. Подумайте снова.";
                    break;
                default:
                    errorMessage = string.Empty;
                    break;
            }
            ShowErrorTip(errorMessage);
        }
        

        // ----Методы для проверки ответов студента
        // Проверить выбранную в аугментальный путь вершину
        private void CheckAugmentalPathVertex(Vertex vertex) {
            int selectedVertexIndex = vertex.Number - 1;
            // Если это первая вершина пути, она обязательно должна быть вершиной-истоком
            if (curAugmentalPath.Count == 0) {
                if (selectedVertexIndex == sourceVertexIndex) {
                    curAugmentalPath.Add(selectedVertexIndex);
                    curAugmentalFlowValue = int.MaxValue;
                    vertex.BorderColor = Color.Red;
                }
                else {
                    MarkError(MaxFlowError.StartOnNonSourceVertex);
                    return;
                }
            }
            else {
                int lastVertexIndex = curAugmentalPath.Last();
                // Если между выбранной вершиной и текущей вершиной нет дуги - ошибка
                if (capacityMatrix[lastVertexIndex, selectedVertexIndex] == 0 && capacityMatrix[selectedVertexIndex, lastVertexIndex] == 0) {
                    MarkError(MaxFlowError.MoveToFarVertex);
                    return;
                }
                else {
                    bool isForwardEdge = capacityMatrix[lastVertexIndex, selectedVertexIndex] != 0;
                    // Если есть прямая дуга
                    if (isForwardEdge) {
                        int edgeAugmentalFlow = capacityMatrix[lastVertexIndex, selectedVertexIndex] - flowMatrix[lastVertexIndex, selectedVertexIndex];
                        if (edgeAugmentalFlow == 0) {
                            MarkError(MaxFlowError.ForwardEdgeIsFull);
                            return;
                        }
                        visGraph.GetEdge(lastVertexIndex, selectedVertexIndex).Color = Color.Red;
                        curAugmentalFlowValue = Math.Min(curAugmentalFlowValue, edgeAugmentalFlow);
                    }
                    // Если есть обратная дуга
                    else {
                        int edgeFlow = flowMatrix[selectedVertexIndex, lastVertexIndex];
                        if (edgeFlow == 0) {
                            MarkError(MaxFlowError.BackEdgeIsEmpty);
                            return;
                        }
                        visGraph.GetEdge(selectedVertexIndex, lastVertexIndex).Color = Color.Red;
                        curAugmentalFlowValue = Math.Min(curAugmentalFlowValue, edgeFlow);
                    }
                    curAugmentalPath.Add(selectedVertexIndex);
                    vertex.BorderColor = Color.Red;
                }
            }
            SetPathVertexLabelWaiting();
        }

        // Проверить поставленную на вершину аугментального маршрута метку
        private void CheckVertexLabel() {
            if (!IsAnswerCorrect()) {
                MarkError(MaxFlowError.IncorrectVertexLabelFormat);
                return;
            }
            bool isCorrect = true;
            string answer = textBoxAnswer.Text.Trim().ToLower();
            if (answer != string.Empty) {
                char sign = answer[0];
                string[] numbersStr = answer.Substring(1).Split(' ');
                int prevVertex = int.Parse(numbersStr[0]);
                string augmentalFlowStr;
                if (curAugmentalPath.Count > 1) {
                    if (prevVertex - 1 != curAugmentalPath[curAugmentalPath.Count - 2])
                        isCorrect = false;
                    int augmentalFlowValue = int.Parse(numbersStr[1]);
                    if (augmentalFlowValue != curAugmentalFlowValue)
                        isCorrect = false;
                    bool isForwardEdge = capacityMatrix[curAugmentalPath[curAugmentalPath.Count - 2], curAugmentalPath[curAugmentalPath.Count - 1]] != 0;
                    if (isForwardEdge && sign == '-' || !isForwardEdge && sign == '+')
                        isCorrect = false;
                    augmentalFlowStr = augmentalFlowValue.ToString();
                }
                else {
                    if (sign != '+')
                        isCorrect = false;
                    if (prevVertex - 1 != curAugmentalPath[0])
                        isCorrect = false;
                    if (numbersStr[1] != "inf")
                        isCorrect = false;
                    augmentalFlowStr = "inf";
                }
                if (isCorrect)
                    visGraph.Vertices[curAugmentalPath.Last()].Label = $"({sign}{prevVertex};{augmentalFlowStr})";
            }
            if (isCorrect) {
                if (curAugmentalPath.Last() == targetVertexIndex)
                    SetFlowRaiseWaiting();
                else
                    SetNextPathVertexWaiting();
            }
            else
                MarkError(MaxFlowError.IncorrectVertexLabel);
        }

        // Проверить величину аугментального потока для построенного аугментального маршрута
        private void CheckFlowRaise() {
            if (!IsAnswerCorrect()) {
                MarkError(MaxFlowError.IncorrectFlowRaiseFormat);
                return;
            }
            string answer = textBoxAnswer.Text.Trim();
            int augmentalFlow = int.Parse(answer);
            if (augmentalFlow == curAugmentalFlowValue) {
                Algorithm.RaiseFlowOnAugmentalPath(capacityMatrix, flowMatrix, curAugmentalPath, curAugmentalFlowValue);
                UpdateEdgesLabels();
                StartNewIteration();
                if (correctMaxFlowValue == Algorithm.GetCurNetworkFlowValue(flowMatrix, verticesCount, sourceVertexIndex)) {
                    SetMaximalFlowWaiting();
                }
                else {
                    SetNextPathVertexWaiting();
                }
            }
            else {
                MarkError(MaxFlowError.IncorrectFlowRaise);
            }
        }

        // Проверить величину максимального потока
        private void CheckMaxFlowValue() {
            if (!IsAnswerCorrect()) {
                MarkError(MaxFlowError.IncorrectMaxFlowFormat);
                return;
            }
            string answer = textBoxAnswer.Text.Trim();
            int maxFlowValue = int.Parse(answer);
            if (maxFlowValue == correctMaxFlowValue) {
                SetNextCutEdgeWaiting();
            }
            else {
                MarkError(MaxFlowError.IncorrectMaxFlowValue);
            }
        }

        // Проверить выбранное ребро на принадлежность минимальному разрезу сети
        private void CheckMinimalCutEdge(Edge edge) {
            int edgeSourceIndex = edge.SourceVertex.Number - 1;
            int edgeTargetIndex = edge.TargetVertex.Number - 1;
            bool isCorrect = false;
            // Проверяем, взята ли уже эта дуга. Если да - выходим
            foreach (var cutEdge in curCutEdges)
                if (cutEdge.Item1 == edgeSourceIndex && cutEdge.Item2 == edgeTargetIndex)
                    return;
            // Проверяем, входит ли выбранная дуга в минимальный разрез
            foreach (var cutEdge in correctMinimalCut) {
                if (cutEdge.Item1 == edgeSourceIndex && cutEdge.Item2 == edgeTargetIndex) {
                    isCorrect = true;
                    break;
                }
            }
            if (isCorrect) {
                curCutEdges.Add(new Tuple<int, int>(edgeSourceIndex, edgeTargetIndex));
                edge.Color = Color.Blue;
                if (curCutEdges.Count < correctMinimalCut.Count)
                    SetNextCutEdgeWaiting();
                else
                    SetProblemFinish();
            }
            else {
                MarkError(MaxFlowError.IncorrectMinCutEdge);
            }
        }


        // ----Обработчики событий
        // Вызов окна с лекцией
        private void buttonLecture_Click(object sender, EventArgs e) {
            FormLectureViewer formLecture;
            try {
                formLecture = new FormLectureViewer("Theory.html");
            }
            catch {
                MessageBox.Show(
                    "Не удаётся открыть файл с лекцией. Файл с лекцией должен называться \"Theory.html\" и должен " +
                    "находиться в каталоге приложения. Пожалуйста, убедитесь в том, что такой файл действительно существует," +
                    "проверьте его целостность и повторите попытку снова.",
                    "Не удалось открыть лекцию", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            formLecture.Show();
        }

        // Выбор вершины графа
        private void VertexSelectedHandler(Vertex vertex) {
            if (problemState == MaxFlowProblemState.NextPathVertexWaiting)
                CheckAugmentalPathVertex(vertex);
            else
                return;
        }

        // Выбор дуги графа
        private void EdgeSelectedHandler(Edge edge) {
            if (problemState == MaxFlowProblemState.NextCutEdgeWaiting)
                CheckMinimalCutEdge(edge);
            else
                return;
        }

        // Принятие ответа
        private void buttonAcceptAnswer_Click(object sender, EventArgs e) {
            if (problemState == MaxFlowProblemState.PathVertexLabelWaiting)
                CheckVertexLabel();
            else if (problemState == MaxFlowProblemState.FlowRaiseWaiting)
                CheckFlowRaise();
            else if (problemState == MaxFlowProblemState.MaximalFlowWaiting)
                CheckMaxFlowValue();
        }

        // Начало новой итерации решения
        private void buttonReloadIteration_Click(object sender, EventArgs e) {
            if (problemState == MaxFlowProblemState.StartWaiting)
                SetNextPathVertexWaiting();
            else
                StartNewIteration();
        }

        // Перезагрузка задачи
        private void buttonReloadProblem_Click(object sender, EventArgs e) {
            InitializeProblem(maxFlowExample, problemMode);
        }
    }
}
