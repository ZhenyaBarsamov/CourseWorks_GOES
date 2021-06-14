using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGVL.Graphs;
using SGVL.Visualizers;
using GOES.Forms;
using System.Text.RegularExpressions;
using System.Media;

namespace GOES.Problems.MaxBipartiteMatching {
    public partial class FormMaxBipartiteMatchingProblem : Form, IProblem {
        // ----Атрибуты задачи
        private MaxBipartiteMatchingProblemExample maxBipartiteMatchingExample;
        private ProblemMode problemMode;
        private MaxBipartiteMatchingProblemState problemState;
        private IGraphVisualizer graphVisInterface;
        private Graph visGraph;

        // ----Атрибуты графа
        private bool[,] graph; // матрица смежности графа
        private int verticesCount; // количество вершин графа
        // ----Атрибуты для алгоритма решения
        private List<int> curAugmentalPath; // текущий строящийся аугментальный маршрут
        private int[] matchingPairsArray; // текущее паросочетание, заданное массивом пар: по индексу вершины хранится индекс пары этой вершины (-1, если пары нет)
        // ----Атрибуты для демонстрации
        private List<int> selectedAugmentalPath; // аугментальный путь, который выбрал компьютер для демонстрации текущей итерации (или пустой список, если его нет)
        private int selectedPathVertexIndex; // индекс текущей вершины из selectedAugmentalPath, до которой дошёл компьютер
        // ----Атрибуты с правильными ответами
        private int correctMaximalMatchingCardinality; // правильная мощность максимального паросочетания в данном графе (количество рёбер в нём)


        // ----Интерфейс задач
        public void InitializeProblem(ProblemExample example, ProblemMode mode) {
            // Если требуется случайная генерация
            if (example == null) {
                // Если случайной генерации нет, говорим, что не реализовано
                if (!ProblemDescriptor.IsRandomExampleAvailable)
                    throw new NotImplementedException("Случайная генерация примеров не реализована");
                // Иначе - генерируем задание
                else
                    example = ExampleGenerator.GenerateExample();
            }
            // Если нам дан пример не задачи о максимальном паросочетании в двудольном графе - ошибка
            maxBipartiteMatchingExample = example as MaxBipartiteMatchingProblemExample;
            if (maxBipartiteMatchingExample == null)
                throw new ArgumentException("Ошибка в выбранном примере. Его невозможно открыть.");
            // В зависимости от режима задачи (решение/демонстрация) меняем некоторые элементы управления
            problemMode = mode;
            if (problemMode == ProblemMode.Solution) {
                SetAnswerGroupBoxSolutionMode();
            }
            else if (problemMode == ProblemMode.Demonstration) {
                SetAnswerGroupBoxDemonstrationMode();
            }
            // Создаём граф для визуализации по примеру
            visGraph = new Graph(maxBipartiteMatchingExample.GraphMatrix, maxBipartiteMatchingExample.IsGraphDirected);
            // Задаём расположение вершин графа
            for (int i = 0; i < visGraph.VerticesCount; i++)
                visGraph.Vertices[i].DrawingCoords = maxBipartiteMatchingExample.DefaultGraphLayout[i];
            graphVisInterface.Initialize(visGraph);
            // Сохраняем матрицу графа для решения. Создаём нужные коллекции
            graph = (bool[,])maxBipartiteMatchingExample.GraphMatrix.Clone();
            verticesCount = maxBipartiteMatchingExample.GraphMatrix.GetLength(0);
            curAugmentalPath = new List<int>();
            matchingPairsArray = new int[verticesCount];
            for (int i = 0; i < verticesCount; i++)
                matchingPairsArray[i] = -1;
            // Пишем условие задачи - номер истока и номер стока
            textLabelExampleDescription.Text =
                "Максимальное паросочетание?";
            // Получаем решение задачи
            correctMaximalMatchingCardinality = Algorithm.GetMatchingCardinality(Algorithm.GetMaximalMatching(graph, verticesCount));
            // Инициализируем статистику решения
            if (problemMode == ProblemMode.Solution)
                maxBipartiteMatchingProblemStatistics = new MaxBipartiteMatchingProblemStatistics();
            // Ставим решение в состояние ожидания начала
            SetStartWaitingState();
        }

        public IProblemDescriptor ProblemDescriptor => new MaxBipartiteMatchingProblemDescriptor();
        public ProblemExample ProblemExample => maxBipartiteMatchingExample;

        private MaxBipartiteMatchingProblemStatistics maxBipartiteMatchingProblemStatistics;
        public IProblemStatistics ProblemStatistics => maxBipartiteMatchingProblemStatistics;



        // ----Конструкторы
        public FormMaxBipartiteMatchingProblem() {
            InitializeComponent();
            graphVisInterface = graphVisualizer;
            graphVisInterface.EdgeSelectedEvent += EdgeSelectedHandler;
            graphVisInterface.VertexSelectedEvent += VertexSelectedHandler;
        }


        // ----Методы для работы с ходом решения/демонстрации
        // Начать новую итерацию решения
        private void StartNewIteration() {
            // Очищаем текущий маршрут
            curAugmentalPath.Clear();
            // Убираем выделение маршрута цветом
            graphVisInterface.ResetVerticesBorderColor();
            graphVisInterface.ResetEdgesColor();
            // Если это демонстрация, мы должны обнулить уже рассматриваемое решение
            selectedAugmentalPath = null;
            SetNextPathVertexWaitingState();
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
            SystemSounds.Exclamation.Play();
        }

        // Вывести сообщение об успехе
        void ShowSuccessTip(string message) {
            textLabelTip.Text = message;
            groupBoxTip.ForeColor = Color.Green;
            SystemSounds.Asterisk.Play();
        }


        // ----Методы для работы с блоком ответов
        // Задать блоку для ответов состояние для демонстрации решения
        void SetAnswerGroupBoxDemonstrationMode() {
            groupBoxAnswers.Text = "Демонстрация";
            buttonAcceptAnswer.Text = "Сделать шаг";
        }

        // Задать блоку для ответов состояние для решения задачи
        void SetAnswerGroupBoxSolutionMode() {
            groupBoxAnswers.Text = "Ответы";
            buttonAcceptAnswer.Text = "Принять ответ";
        }

        // Задать блоку для ответа нужное состояние
        void SetAnswerGroupBoxState(bool isGroupBoxEnabled, bool isTextBoxEnabled, string buttonText) {
            textBoxAnswer.Text = string.Empty;
            textBoxAnswer.Enabled = isTextBoxEnabled;
            groupBoxAnswers.Enabled = isGroupBoxEnabled;
            buttonAcceptAnswer.Text = buttonText;
        }

        // ----Методы для работы с полем ввода ответов
        // Проверить ответ на соответствие формату ввода в зависимости от требуемого ответа (метки вершины, числа)
        private bool IsAnswerCorrect(string answer) {
            Regex regex;
            switch (problemState) {
                case MaxBipartiteMatchingProblemState.MaxMatchingCardinalityWaiting:
                    // Должно быть одно целое число
                    regex = new Regex(@"^\d+$");
                    return regex.IsMatch(answer);
                default:
                    return false;
            }
        }


        // ----Методы для работы с визуализацией графа
        // Обновить отображение текущего паросочетания
        private void UpdateGraphMatching() {
            // Снимаем старое выделение жирным с рёбер и вершин
            graphVisInterface.ResetEdgesBold();
            graphVisInterface.ResetVerticesBold();
            // Проходим по всем вершинам и выделяем рёбра паросочетания и покрытые им вершины жирным
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++) {
                int vertexPairIndex = matchingPairsArray[vertexIndex];
                if (vertexPairIndex != -1) {
                    visGraph.GetEdge(vertexIndex, vertexPairIndex).Bold = true;
                    visGraph.Vertices[vertexIndex].Bold = true;
                    visGraph.Vertices[vertexPairIndex].Bold = true;
                }
            }
        }


        // ----Методы изменения состояния (этапа) решения задания
        // Перевести задачу в состояние ожидания начала
        private void SetStartWaitingState() {
            problemState = MaxBipartiteMatchingProblemState.StartWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message = "";
            if (problemMode == ProblemMode.Solution)
                message =
                    "Добро пожаловать в задание \"Максимальное паросочетание в двудольном графе\"!" + Environment.NewLine +
                    "Ваша задача - найти максимальное паросочетание в представленном двудольном графе и указать его мощность." + Environment.NewLine +
                    "Последовательно стройте аугментальные маршруты и постепенно увеличивайте мощность текущего паросочетания. " +
                    "Для построения чередующихся (аугментальных) цепей последовательно выбирайте вершины маршрута, начав с непокрытой паросочетанием вершины " +
                    "и закончив такой же непокрытой вершиной." + Environment.NewLine +
                    "Если при построении маршрута Вы зашли в тупик, можете сбросить построенный маршрут кнопкой \"К началу итерации\", " +
                    "это не повлияет на оценку." + Environment.NewLine +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать решение, нажмите кнопку \"К началу итерации\".";
            else if (problemMode == ProblemMode.Demonstration)
                message =
                    "Добро пожаловать в демонстрацию решения задачи \"Максимальное паросочетание в двудольном графе\"!" + Environment.NewLine +
                    "Здесь у Вас есть возможность просмотреть пошаговую демонстрацию решения задачи о максимальном паросочетании " +
                    "в двудольном графе." + Environment.NewLine +
                    "Для того, чтобы сделать очередной шаг решения, нажимайте кнопку \"Сделать шаг\"." + Environment.NewLine +
                    "Вы можете начать итерацию решения заново кнопкой \"К началу итерации\"." +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать демонстрацию решения, нажмите кнопку \"К началу итерации\", " +
                    "а затем используйте кнопки \"Сделать шаг\", \"К началу итерации\" и \"Начать заново\" для управления ходом решения.";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(false, true, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(false, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
        }

        // Перевести задачу в состояние ожидания вершины для аугментального маршрута
        private void SetNextPathVertexWaitingState() {
            problemState = MaxBipartiteMatchingProblemState.NextPathVertexWaiting;
            if (problemMode == ProblemMode.Solution) {
                graphVisInterface.InteractiveMode = InteractiveMode.Interactive;
                graphVisInterface.IsVerticesMoving = true;
            }
            string message = "";
            if (problemMode == ProblemMode.Solution) {
                message =
                    "Выберите следующую вершину для аугментального пути." + Environment.NewLine;
            }
            else if (problemMode == ProblemMode.Demonstration) {
                if (curAugmentalPath.Count > 0) {
                    int lastVertexIndex = curAugmentalPath.Last();
                    message = $"Последней выбрана вершина {lastVertexIndex + 1}." + Environment.NewLine;
                }
            }
            if (curAugmentalPath.Count > 0) {
                // Формируем строку с текущим аугментальным маршрутом
                StringBuilder pathStr = new StringBuilder();
                foreach (var pathVertex in curAugmentalPath)
                    pathStr.Append($"{pathVertex + 1}-");
                // Удаляем последнюю чёрточку
                pathStr.Remove(pathStr.Length - 1, 1);
                message += $"Текущий аугментальный путь: {pathStr}." + Environment.NewLine;
            }
            message +=
                "Жирным выделены рёбра, которые принадлежат текущему паросочетанию. " +
                "Зелёным при построении аугментальной цепи выделяются рёбра, которые после проведения чередования должны войти в паросочетание. " +
                "Красным при построении аугментальной цепи выделяются рёбра, которые после проведения чередования должны удалиться из паросочетания." + Environment.NewLine;
            if (problemMode == ProblemMode.Demonstration)
                message +=
                    "Нажмите кнопку \"Сделать шаг\", чтобы добавить следующую вершину в строящийся аугментальный путь.";
            ShowStandardTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, false, "Провести чередование");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
        }

        // Перевести задачу в псевдосостояние ожидания чередования по построенному аугментальному пути (для режима демонстрации)
        private void SetAlternationWaitState() {
            problemState = MaxBipartiteMatchingProblemState.NextPathVertexWaiting; // Само состояние совпадает с состоянием ожидания очередной вершины
            string message = 
                "Аугментальная цепь построена." + Environment.NewLine +
                "Нажмите кнопку \"Сделать шаг\", чтобы провести чередование рёбер по построенному аугментальному пути. " + 
                "При этом рёбра, входящие в паросочетание (выделены жирным и красным), будут удалены из него, " +
                "а оставшиеся рёбра цепи (выделены зелёным), наоборот, будут добавлены в него. Таким образом мощность паросочетания увеличится на единицу.";
            ShowSuccessTip(message);
            SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
        }

        // Перевести задачу в состояние ожидания значения мощности найденного максимального паросочетания
        private void SetMaxMatchingCardinalityWaitingState() {
            problemState = MaxBipartiteMatchingProblemState.MaxMatchingCardinalityWaiting;
            if (problemMode == ProblemMode.Solution) {
                graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
                graphVisInterface.IsVerticesMoving = false;
            }
            string message = "";
            if (problemMode == ProblemMode.Solution)
                message =
                    "Максимальное паросочетание построено." + Environment.NewLine +
                    "Введите в поле для ответов мощность построенного максимального паросочетания в виде одного целого числа.";
            else if (problemMode == ProblemMode.Demonstration)
                message =
                    "Максимальное паросочетание построено." + Environment.NewLine +
                    $"Мощность максимального (текущего) паросочетания для этого двудольного графа равна {correctMaximalMatchingCardinality}.";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, true, "Принять ответ");
            buttonReloadIteration.Enabled = false;
        }

        // Перевести задачу в состояние выполненной задачи
        void SetProblemFinish() {
            problemState = MaxBipartiteMatchingProblemState.ProblemFinish;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message =
                "Максимальное паросочетание в двудольном графе найдено." + Environment.NewLine +
                "Задание успешно выполнено!";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(false, false, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(false, false, "Сделать шаг");
            buttonReloadIteration.Enabled = false;
            // Отмечаем, что задача решена
            if (problemMode == ProblemMode.Solution) {
                maxBipartiteMatchingProblemStatistics.IsSolved = true;
                Close();
            }
        }


        // ----Методы для работы с ошибками студента
        // Отметить допущенную студентом ошибку
        void MarkError(MaxBipartiteMatchingError error) {
            int prevLastVertex;
            int lastVertex;
            string prevEdge;
            string errorMessage;
            switch (error) {
                case MaxBipartiteMatchingError.StartOnMatchedVertex:
                    maxBipartiteMatchingProblemStatistics.StartOnMatchedVertexCount += 1;
                    errorMessage = "Построение аугментального пути необходимо начинать с вершины, которая не покрыта паросочетанием";
                    break;
                case MaxBipartiteMatchingError.MoveToFarVertex:
                    maxBipartiteMatchingProblemStatistics.MoveToFarVertexCount += 1;
                    errorMessage =
                        "Вы попытались перейти в вершину, которая не соединена ребром с последней вершиной построенного маршрута." + Environment.NewLine +
                        $"Последней (текущей) вершиной маршрута является вершина {curAugmentalPath.Last() + 1}.";
                    break;
                case MaxBipartiteMatchingError.AlternationBreakingOnMatchedEdge:
                    maxBipartiteMatchingProblemStatistics.AlternationBreakingOnMatchedEdgeCount += 1;
                    prevLastVertex = curAugmentalPath[curAugmentalPath.Count - 2] + 1;
                    lastVertex = curAugmentalPath.Last() + 1;
                    prevEdge = $"{{{prevLastVertex};{lastVertex}}}";
                    errorMessage =
                        "Выбранная Вами вершина для продолжения маршрута нарушает чередование непаросочетанных рёбер с паросочетанными." + Environment.NewLine +
                        $"Последнее ребро построенного маршрута {prevEdge} входит в паросочетание. Значит, следующее ребро не должно входить в него.";
                    break;
                case MaxBipartiteMatchingError.AlternationBreakingOnNotMatchedEdge:
                    maxBipartiteMatchingProblemStatistics.AlternationBreakingOnNotMatchedEdgeCount += 1;
                    prevLastVertex = curAugmentalPath[curAugmentalPath.Count - 2] + 1;
                    lastVertex = curAugmentalPath.Last() + 1;
                    prevEdge = $"{{{prevLastVertex};{lastVertex}}}";
                    errorMessage =
                        "Выбранная Вами вершина для продолжения маршрута нарушает чередование непаросочетанных рёбер с паросочетанными." + Environment.NewLine +
                        $"Последнее ребро построенного маршрута {prevEdge} не входит в паросочетание. Значит, следующее ребро должно входить в него.";
                    break;
                case MaxBipartiteMatchingError.AugmentalPathIsNotFinished:
                    maxBipartiteMatchingProblemStatistics.AugmentalPathIsNotFinishedCount += 1;
                    errorMessage =
                        "Вы попытались провести чередование по аугментальной цепи, которая ещё не была закончена: аугментальная цепь не может быть пустой " +
                        "и не может состоять из одной вершины";
                    break;
                case MaxBipartiteMatchingError.IncorrectAugmentalPath:
                    maxBipartiteMatchingProblemStatistics.IncorrectAugmentalPathCount += 1;
                    errorMessage =
                        "Вы попытались провести чередование по неправильной (незаконченной) аугментальной цепи: аугментальная цепь должна " +
                        "начинаться и заканчиваться вершинами, не покрытыми паросочетанием, и при этом чередовать непаросочетанные рёбра " +
                        "с паросочетанными";
                    break;
                case MaxBipartiteMatchingError.IncorrectMaxMatchingCardinalityFormat:
                    maxBipartiteMatchingProblemStatistics.IncorrectMaxMatchingCardinalityFormatCount += 1;
                    errorMessage =
                        "Неправильный формат значения мощности максимального паросочетания." + Environment.NewLine +
                        "Значение мощности максимального паросочетания вводится в формате одного целого числа.";
                    break;
                case MaxBipartiteMatchingError.IncorrectMaxMatchingCardinality:
                    maxBipartiteMatchingProblemStatistics.IncorrectMaxMatchingCardinalityCount += 1;
                    errorMessage =
                        "Неправильное значение мощности построенного максимального паросочетания. Пересчитайте его, и попробуйте снова.";
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
            // Если по этой вершине уже проходит путь - выходим
            if (curAugmentalPath.Contains(selectedVertexIndex))
                return;
            // Если цепь уже построена из двух непокрытых вершин, то её уже не продолжить - ошибка
            if (curAugmentalPath.Count == 2 && matchingPairsArray[curAugmentalPath.Last()] == -1)
                MarkError(MaxBipartiteMatchingError.AlternationBreakingOnNotMatchedEdge);
            // Если это первая вершина пути, она обязательно должна быть непокрытой паросочетанием
            if (curAugmentalPath.Count == 0) {
                if (matchingPairsArray[selectedVertexIndex] != -1) {
                    MarkError(MaxBipartiteMatchingError.StartOnMatchedVertex);
                    return;
                }
                curAugmentalPath.Add(selectedVertexIndex);
                vertex.BorderColor = Color.Red;
            }
            else {
                int lastVertexIndex = curAugmentalPath.Last();
                // Если ребра нет, то ошибка - перейти нельзя
                if (!graph[lastVertexIndex, selectedVertexIndex]) {
                    MarkError(MaxBipartiteMatchingError.MoveToFarVertex);
                    return;
                }
                // Если это вторая вершина, то она может быть покрытой или не покрытой - лишь бы ребро к ней было
                // Если это промежуточная вершина, то должно быть чередование рёбер (непаросочетанное-паросочетанное-непаросочетанное-...).
                if (curAugmentalPath.Count > 1) {
                    Tuple<int, int> prevEdge = new Tuple<int, int>(curAugmentalPath[curAugmentalPath.Count - 2], curAugmentalPath[curAugmentalPath.Count - 1]);
                    Tuple<int, int> curEdge = new Tuple<int, int>(curAugmentalPath.Last(), selectedVertexIndex);
                    // Если предыдущее непаросочетанное, это должно быть паросочетанным
                    bool isPrevEdgeInMatching = matchingPairsArray[prevEdge.Item1] == prevEdge.Item2 && matchingPairsArray[prevEdge.Item2] == prevEdge.Item1;
                    bool isCurEdgeInMatching = matchingPairsArray[curEdge.Item1] == curEdge.Item2 && matchingPairsArray[curEdge.Item2] == curEdge.Item1;
                    if (isPrevEdgeInMatching && isCurEdgeInMatching) {
                        MarkError(MaxBipartiteMatchingError.AlternationBreakingOnMatchedEdge);
                        return;
                    }
                    // И наоборот
                    else if (!isPrevEdgeInMatching && !isCurEdgeInMatching) {
                        MarkError(MaxBipartiteMatchingError.AlternationBreakingOnNotMatchedEdge);
                        return;
                    }
                }
                curAugmentalPath.Add(selectedVertexIndex);
                if (curAugmentalPath.Count % 2 == 0)
                    visGraph.GetEdge(lastVertexIndex, selectedVertexIndex).Color = Color.YellowGreen;
                else
                    visGraph.GetEdge(lastVertexIndex, selectedVertexIndex).Color = Color.Red;
                vertex.BorderColor = Color.Red;
            }
            SetNextPathVertexWaitingState();
        }

        // Проверить попытку чередования по построенному аугменатльному маршруту
        private void AlternateOnAugmentalPath() {
            // Если маршрут пустой, или состоит из одной вершины - цепь не построена
            if (curAugmentalPath.Count <= 1) {
                MarkError(MaxBipartiteMatchingError.AugmentalPathIsNotFinished);
                return;
            }
            // Последняя вершина цепи должна быть непокрытой, в любом случае (как и первая, но это мы проверяем при выборе вершины)
            if (matchingPairsArray[curAugmentalPath.Last()] != -1) {
                MarkError(MaxBipartiteMatchingError.IncorrectAugmentalPath);
                return;
            }
            // Проводим чередование по построенной аугментальной цепи
            Algorithm.AlternateOnAugmentalPath(curAugmentalPath, matchingPairsArray);
            UpdateGraphMatching();
            StartNewIteration();
            // Проверяем, а не построено ли уже максимальное паросочетание
            if (Algorithm.GetMatchingCardinality(matchingPairsArray) == correctMaximalMatchingCardinality) {
                SetMaxMatchingCardinalityWaitingState();
            }
            else {
                SetNextPathVertexWaitingState();
            }
        }

        // Проверить значение мощности построенного максимального паросочетания
        private void CheckMaxMatchingCardinality(string answer) {
            int maxMatchingCardinality = int.Parse(answer);
            if (maxMatchingCardinality == correctMaximalMatchingCardinality) {
                SetProblemFinish();
            }
            else {
                MarkError(MaxBipartiteMatchingError.IncorrectMaxMatchingCardinality);
            }
        }


        // Проверить значение мощности построенного максимального паросочетания
        private void CheckMaxMatchingCardinality() {
            string answer = textBoxAnswer.Text.Trim().ToLower();
            if (!IsAnswerCorrect(answer)) {
                MarkError(MaxBipartiteMatchingError.IncorrectMaxMatchingCardinalityFormat);
                return;
            }
            CheckMaxMatchingCardinality(answer);
        }


        // ----Методы для демонстрации решения
        private void DoAnswerForDemonstration() {
            switch (problemState) {
                case MaxBipartiteMatchingProblemState.NextPathVertexWaiting:
                    // Если мы уже целиком построили аугментальный путь - проводим чередование и строим новый путь
                    if (curAugmentalPath.Count > 0 && selectedPathVertexIndex >= selectedAugmentalPath.Count) {
                        AlternateOnAugmentalPath();
                    }
                    // Иначе - продолжаем строить путь
                    else {
                        // Если аугментального пути ещё не было или он кончился, строим новый
                        if (selectedAugmentalPath == null || selectedPathVertexIndex >= selectedAugmentalPath.Count) {
                            selectedAugmentalPath =
                                Algorithm.GetRandomAugmentalPath(graph, verticesCount, matchingPairsArray);
                            selectedPathVertexIndex = 0;
                        }
                        // И добавляем вершину в путь
                        int selectedVertex = selectedAugmentalPath[selectedPathVertexIndex];
                        selectedPathVertexIndex++;
                        CheckAugmentalPathVertex(visGraph[selectedVertex]);
                        // Проверяем, а не построен ли был аугментальный путь. Если да - говорим об этом
                        if (selectedPathVertexIndex >= selectedAugmentalPath.Count)
                            SetAlternationWaitState();
                    }
                    break;
                case MaxBipartiteMatchingProblemState.MaxMatchingCardinalityWaiting:
                    string answer = correctMaximalMatchingCardinality.ToString();
                    CheckMaxMatchingCardinality(answer);
                    break;
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
            if (problemState == MaxBipartiteMatchingProblemState.NextPathVertexWaiting)
                CheckAugmentalPathVertex(vertex);
            else
                return;
        }

        // Выбор ребра графа
        private void EdgeSelectedHandler(Edge selectedEdge) {
            return;
        }


        // Принятие ответа
        private void buttonAcceptAnswer_Click(object sender, EventArgs e) {
            if (problemMode == ProblemMode.Solution) {
                if (problemState == MaxBipartiteMatchingProblemState.NextPathVertexWaiting)
                    AlternateOnAugmentalPath();
                else if (problemState == MaxBipartiteMatchingProblemState.MaxMatchingCardinalityWaiting)
                    CheckMaxMatchingCardinality();
            }
            else if (problemMode == ProblemMode.Demonstration) {
                DoAnswerForDemonstration();
            }
        }


        // Начало новой итерации решения
        private void buttonReloadIteration_Click(object sender, EventArgs e) {
            if (problemState == MaxBipartiteMatchingProblemState.StartWaiting)
                SetNextPathVertexWaitingState();
            else
                StartNewIteration();
        }

        // Перезагрузка задачи
        private void buttonReloadProblem_Click(object sender, EventArgs e) {
            InitializeProblem(maxBipartiteMatchingExample, problemMode);
        }
    }
}
