using GOES.Controls;
using GOES.Forms;
using SGVL.Graphs;
using SGVL.Visualizers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOES.Problems.AssignmentProblem {
    public partial class FormAssignmentProblem : Form, IProblem {
        // ----Атрибуты задачи
        private AssignmentProblemExample assignmentProblemExample;
        private ProblemMode problemMode;
        private AssignmentProblemState problemState;
        private IGraphVisualizer graphVisInterface;
        private Graph visGraph;

        // ----Атрибуты графа
        private int[,] adjacencyMatrix; // полная матрица смежности графа, содержащая стоимости назначений
        private int verticesCount; // полное количество вершин графа
        // ----Атрибуты для алгоритма решения (этап поиска паросочетания)
        private bool[,] matchingGraph; 
        private List<int> curAugmentalPath; // текущий строящийся аугментальный маршрут
        private int[] matchingPairsArray; // текущее паросочетание, заданное массивом пар: по индексу вершины хранится индекс пары этой вершины (-1, если пары нет)
        // ----Атрибуты для демонстрации
        private List<int> selectedAugmentalPath; // аугментальный путь, который выбрал компьютер для демонстрации текущей итерации (или пустой список, если его нет)
        private int selectedPathVertexIndex; // индекс текущей вершины из selectedAugmentalPath, до которой дошёл компьютер
        // ----Атрибуты с правильными ответами
        private int correctAssignmentMinCost; // правильная минимальная стоимость назначения
        private int correctMaximalMatchingCardinality; // правильная мощность максимального паросочетания в текущем графе


        // ----Конструкторы
        public FormAssignmentProblem() {
            InitializeComponent();
            graphVisInterface = graphVisualizer;
            graphVisInterface.EdgeSelectedEvent += EdgeSelectedHandler;
            graphVisInterface.VertexSelectedEvent += VertexSelectedHandler;
        }


        // ----Интерфейс задач
        public void InitializeProblem(ProblemExample example, ProblemMode mode) {
            // Если требуется случайная генерация, а её нет, говорим, что не реализовано
            if (example == null && !ProblemDescriptor.IsRandomExampleAvailable)
                throw new NotImplementedException("Случайная генерация примеров не реализована");
            // Если нам дан пример не задачи о максимальном паросочетании в двудольном графе - ошибка
            assignmentProblemExample = example as AssignmentProblemExample;
            if (assignmentProblemExample == null)
                throw new ArgumentException("Ошибка в выбранном примере. Его невозможно открыть.");
            // В зависимости от режима задачи (решение/демонстрация) меняем некоторые элементы управления
            problemMode = mode;
            if (problemMode == ProblemMode.Solution) {
                SetSolutionMode();
            }
            else if (problemMode == ProblemMode.Demonstration) {
                SetDemonstrationMode();
            }
            // Создаём граф для визуализации по примеру (если нужна генерация, генерируем)
            //if (example != null) {
            //    DisplayFullGraph();
            //}
            //else
            //    visGraph = new Graph(assignmentProblemExample.GraphMatrix, assignmentProblemExample.IsGraphDirected); // TODO: генерация
            //graphVisInterface.Initialize(visGraph);
            // Сохраняем матрицу графа для решения. Создаём нужные коллекции
            adjacencyMatrix = (int[,])assignmentProblemExample.CostsMatrix.Clone();
            verticesCount = assignmentProblemExample.GraphMatrix.GetLength(0);
            curAugmentalPath = new List<int>();
            matchingPairsArray = new int[verticesCount];
            for (int i = 0; i < verticesCount; i++)
                matchingPairsArray[i] = -1;
            // Пишем условие задачи
            textLabelExampleDescription.Text =
                "Задана матрица стоимостей назначений. Найдите самое выгодное назначение - назначение с минимальной стоимостью.";
            DisplayCostsMatrix(matrixDataGridViewExampleMatrix, assignmentProblemExample.CostsMatrix, verticesCount);
            matrixDataGridViewExampleMatrix.ReadOnly = true;
            // Получаем решение задачи
            correctAssignmentMinCost = 0; // TODO
            // Ставим решение в состояние ожидания начала
            SetStartWaitingState();
        }

        public IProblemDescriptor ProblemDescriptor => new AssignmentProblemDescriptor();


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
            //SetNextPathVertexWaitingState();
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
        void SetDemonstrationMode() {
            // Таблицы с текущей и следующей матрицами тоже делаем только для чтения
            matrixDataGridViewCurMatrix.ReadOnly = matrixDataGridViewNextMatrix.ReadOnly = true;
            groupBoxAnswers.Text = "Демонстрация";
            buttonAcceptAnswer.Text = "Сделать шаг";
        }

        // Задать блоку для ответов состояние для решения задачи
        void SetSolutionMode() {
            // Таблица с текущей матрицей только для чтения, со следующей - изменяемая
            matrixDataGridViewCurMatrix.ReadOnly = true;
            matrixDataGridViewNextMatrix.ReadOnly = false;
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
                //case AssignmentProblemState.FirstStage:
                //    // В матрице Должно быть одно целое число
                //    regex = new Regex(@"^\d+$");
                default:
                    return false;
            }
        }


        // ----Методы для работы с визуализацией графа
        // Задать вершинам графа метки с нумерацией в пределах их доли
        private void SetVerticesNumberLabels() {
            graphVisInterface.Settings.IsVertexNumberVisible = false;
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
                visGraph.Vertices[vertexIndex].Label = $"{vertexIndex / 2 + 1}";
        }

        private void SetEdgesCostsLabels() {
            for (int leftPairVertexIndex = 0; leftPairVertexIndex < verticesCount; leftPairVertexIndex += 2)
                for (int rightPairVertexIndex = 1; rightPairVertexIndex < verticesCount; rightPairVertexIndex += 2)
                    visGraph.GetEdge(leftPairVertexIndex, rightPairVertexIndex).Label = 
                        adjacencyMatrix[leftPairVertexIndex, rightPairVertexIndex].ToString();
        }

        private void DisplayGraph() {
            visGraph = new Graph(matchingGraph, assignmentProblemExample.IsGraphDirected);
            graphVisInterface.Initialize(visGraph);
            // Задаём расположение вершин графа
            for (int i = 0; i < visGraph.VerticesCount; i++)
                visGraph.Vertices[i].DrawingCoords = assignmentProblemExample.DefaultGraphLayout[i];
            // Ставим на вершины метки с их нумерацией в пределах своей доли
            SetVerticesNumberLabels();
        }

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


        // ----Методы для работы с отображением матриц
        // Отобразить матрицу стоимостей в виде матрицы смежности двудольного графа в заданном элементе управления
        private void DisplayCostsMatrix(MatrixDataGridView matrixDgv, int[,] adjacencyMatrix, int verticesCount) {
            Algorithm.GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            int[,] bipartiteGraphMatrix = new int[rowsCount, colsCount];
            for (int row = 0; row < rowsCount; row++)
                for (int col = 0; col < colsCount; col++)
                    bipartiteGraphMatrix[row, col] = Algorithm.GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
            matrixDgv.FillMatrix(bipartiteGraphMatrix);
        }

        // Проверить matrixDataGridViewNextMatrix на правильность ввода (все элементы должны быть целыми неотрицательными числами),
        // и отметить в ней ошибки
        private bool IsNextMatrixCorrect() {
            bool isCorrect = true;
            // В матрице должны быть только целые числа
            Regex regex = new Regex(@"^\d+$");
            foreach (DataGridViewRow row in matrixDataGridViewNextMatrix.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if (!regex.IsMatch(cell.Value as string)) {
                        isCorrect = false;
                        cell.ErrorText = "В ячейке матрицы может быть только целое неотрицательное число";
                    }
                    else {
                        cell.ErrorText = string.Empty;
                    }
            return isCorrect;
        }


        // ----Методы изменения состояния (этапа) решения задания
        // Перевести задачу в состояние ожидания начала
        private void SetStartWaitingState() {
            problemState = AssignmentProblemState.StartWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            // Отображаем текущую матрицу и текущую матрицу для внесения в неё изменений
            DisplayCostsMatrix(matrixDataGridViewCurMatrix, adjacencyMatrix, verticesCount);
            DisplayCostsMatrix(matrixDataGridViewNextMatrix, adjacencyMatrix, verticesCount);
            matrixDataGridViewNextMatrix.ReadOnly = true;
            // Убираем все отмеченные ошибки, которые были (матрица правильная, уберутся автоматически)
            IsNextMatrixCorrect();
            string message = "";
            if (problemMode == ProblemMode.Solution)
                message =
                    "Добро пожаловать в задание \"Задача о назначениях\"!" + Environment.NewLine +
                    "Ваша задача - найти оптимальное назначение в соответствии с заданной матрицей стоимостей назначений." + Environment.NewLine +
                    "Задача будет решаться в четыре шага. " + Environment.NewLine +
                    "На первом и втором шагах Вам необходимо добиться, чтобы в каждой строке и в каждом столбце матрицы был хотя бы один нуль." + Environment.NewLine +
                    "На третьем шаге Вам необходимо сделать попытку построить оптимальное назначение, " +
                    "используя алгоритм Куна для поиска максимального паросочетания в двудольном графе." + Environment.NewLine +
                    "Если попытка неудачна, на четвёртом шаге происходит перераспределение нулей в матрице и " +
                    "затем решение возвращается к третьему шагу." + Environment.NewLine +
                    "Для внесения изменений в матрицу используйте матрицу в блоке \"Следующая матрица\"." + Environment.NewLine +
                    "Для поиска максимального паросочетания в двудольном графе используйте визуализацию графа." + Environment.NewLine +
                    "Для каких-либо заметок Вы можете использовать текстовое поле в блоке \"Черновик\". Он не проверяется." + Environment.NewLine +
                    "Если Вы хотите вернуться к началу текущего шага, нажмите кнопку \"К началу шага\". Это не повлияет на оценку." + Environment.NewLine +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать решение, нажмите кнопку \"К началу шага\".";
            else if (problemMode == ProblemMode.Demonstration)
                message =
                    "Добро пожаловать в демонстрацию решения задачи \"Задача о назначениях\"!" + Environment.NewLine +
                    "Здесь у Вас есть возможность просмотреть пошаговую демонстрацию решения задачи о назначениях." + Environment.NewLine +
                    "Для того, чтобы сделать очередной шаг решения, нажимайте кнопку \"Сделать шаг\"." + Environment.NewLine +
                    "Вы можете вернуться к началу текущего шага кнопкой \"К началу шага\"." +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать демонстрацию решения, нажмите кнопку \"К началу шага\", " +
                    "а затем используйте кнопки \"Сделать шаг\", \"К началу шага\" и \"Начать заново\" для управления ходом решения.";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(false, true, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(false, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
        }

        // Перевести задачу в состояние ожидания матрицы первого шага
        private void SetFirstStageState() {
            problemState = AssignmentProblemState.FirstStage;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            // Отображаем текущую матрицу и текущую матрицу для внесения в неё изменений
            DisplayCostsMatrix(matrixDataGridViewCurMatrix, adjacencyMatrix, verticesCount);
            DisplayCostsMatrix(matrixDataGridViewNextMatrix, adjacencyMatrix, verticesCount);
            matrixDataGridViewNextMatrix.ReadOnly = false;
            // Убираем все отмеченные ошибки, которые были (матрица правильная, уберутся автоматически)
            IsNextMatrixCorrect();
            string message = "";
            if (problemMode == ProblemMode.Solution)
                message =
                    "Шаг первый - добиться, чтобы в каждой строке матрицы был хотя бы один нулевой элемент." + Environment.NewLine +
                    "Для этого необходимо вычесть из каждой строки матрицы её минимальный элемент." + Environment.NewLine +
                    "В блоке \"Текущая матрица\" отображается матрица, которая имеется на текущий момент. " +
                    "Проведите все нужные действия в матрице, находящейся в блоке \"Следующая матрица\", " +
                    "и нажмите кнопку \"Принять ответ\"." + Environment.NewLine +
                    "Для каких-либо заметок Вы можете использовать текстовое поле в блоке \"Черновик\". Он не проверяется." + Environment.NewLine +
                    "Если Вы хотите сбросить значения в матрице блока \"Следующая матрица\" на текущие, " +
                    "нажмите кнопку \"К началу шага\". Это не повлияет на оценку." + Environment.NewLine +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать решение, нажмите кнопку \"К началу шага\".";
            else if (problemMode == ProblemMode.Demonstration)
                message =
                    "Шаг первый - добиться, чтобы в каждой строке матрицы был хотя бы один нулевой элемент." + Environment.NewLine +
                    "Для этого необходимо вычесть из каждой строки матрицы её минимальный элемент." + Environment.NewLine +
                    "Минимальные элементы в каждой строке выделены цветом." + Environment.NewLine +
                    "Для того, чтобы сделать очередной шаг решения, нажимайте кнопку \"Сделать шаг\"." + Environment.NewLine +
                    "Вы можете вернуться к началу текущего шага кнопкой \"К началу шага\"." +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine;
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, false, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
        }

        // Перевести задачу в состояние ожидания матрицы второго шага
        private void SetSecondStageState() {
            problemState = AssignmentProblemState.SecondStage;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            // Отображаем текущую матрицу и текущую матрицу для внесения в неё изменений
            DisplayCostsMatrix(matrixDataGridViewCurMatrix, adjacencyMatrix, verticesCount);
            DisplayCostsMatrix(matrixDataGridViewNextMatrix, adjacencyMatrix, verticesCount);
            matrixDataGridViewNextMatrix.ReadOnly = false;
            // Убираем все отмеченные ошибки, которые были (матрица правильная, уберутся автоматически)
            IsNextMatrixCorrect();
            string message = "";
            if (problemMode == ProblemMode.Solution)
                message =
                    "Шаг второй - добиться, чтобы в каждом столбце матрицы был хотя бы один нулевой элемент." + Environment.NewLine +
                    "Для этого необходимо вычесть из каждого столбца матрицы его минимальный элемент." + Environment.NewLine +
                    "В блоке \"Текущая матрица\" отображается матрица, которая имеется на текущий момент. " +
                    "Проведите все нужные действия в матрице, находящейся в блоке \"Следующая матрица\", " +
                    "и нажмите кнопку \"Принять ответ\"." + Environment.NewLine +
                    "Для каких-либо заметок Вы можете использовать текстовое поле в блоке \"Черновик\". Он не проверяется." + Environment.NewLine +
                    "Если Вы хотите сбросить значения в матрице блока \"Следующая матрица\" на текущие, " +
                    "нажмите кнопку \"К началу шага\". Это не повлияет на оценку." + Environment.NewLine +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать решение, нажмите кнопку \"К началу шага\".";
            else if (problemMode == ProblemMode.Demonstration)
                message =
                    "Шаг первый - добиться, чтобы в каждом столбце матрицы был хотя бы один нулевой элемент." + Environment.NewLine +
                    "Для этого необходимо вычесть из каждого столбца матрицы его минимальный элемент." + Environment.NewLine +
                    "Минимальные элементы в каждой строке выделены цветом." + Environment.NewLine +
                    "Для того, чтобы сделать очередной шаг решения, нажимайте кнопку \"Сделать шаг\"." + Environment.NewLine +
                    "Вы можете вернуться к началу текущего шага кнопкой \"К началу шага\"." +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine;
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, false, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
        }

        // Перевести задачу в состояние ожидания вершины для аугментального маршрута
        private void SetNextPathVertexWaitingState() {
            problemState = AssignmentProblemState.NextPathVertexWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.Interactive;
            graphVisInterface.IsVerticesMoving = true;
            string message = ""; 
            if (curAugmentalPath.Count == 0 && Algorithm.GetMatchingCardinality(matchingPairsArray) == 0) 
                message = 
                    "Шаг третий - попытка построить оптимальное назначение, опираясь на текущую матрицу." + Environment.NewLine +
                    "Для этого строится двудольный граф, вершины первой доли - работники, вершины правой доли - работы. Рёбра в этом графе " +
                    "соответствуют нулевым элементам в текущей матрице. Нужный граф построен автоматически. " + Environment.NewLine +
                    "Для такого графа с помощью алгоритма Куна ищется максимальное паросочетание. Если найденное паросочетание является совершенным, " +
                    "то оптимальное назначение найдено - на него указывают рёбра найденного паросочетания." + Environment.NewLine +
                    "Ваша очередь: используя алгоритм Куна (см. задача о максимальном паросочетании в двудольном графе), постройте " +
                    "максимальное паросочетание в данном графе." + Environment.NewLine;
            if (problemMode == ProblemMode.Solution) {
                message +=
                    "Выберите следующую вершину для аугментального пути." + Environment.NewLine;
            }
            else if (problemMode == ProblemMode.Demonstration) {
                if (curAugmentalPath.Count > 0) {
                    int lastVertexIndex = curAugmentalPath.Last();
                    message += $"Последней выбрана вершина {lastVertexIndex + 1}." + Environment.NewLine;
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
            if (curAugmentalPath.Count == 0 && Algorithm.GetMatchingCardinality(matchingPairsArray) == 0)
                ShowSuccessTip(message);
            else
                ShowStandardTip(message); ;
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, false, "Провести чередование");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
        }


        // ----Методы для работы с ошибками студента
        // Отметить допущенную студентом ошибку
        void MarkError(AssignmentProblemError error) {
            int prevLastVertex;
            int lastVertex;
            string prevEdge;
            string errorMessage;
            switch (error) {
                default:
                    errorMessage = string.Empty;
                    break;
            }
            ShowErrorTip(errorMessage);
        }


        // ----Методы для проверки ответов студента
        // Проверить следующую матрицу стоимостей на соответствие заданной матрице
        private bool CheckNextMatrixSimilarTo(int[,] adjacencyMatrix, int verticesCount) {
            Algorithm.GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            bool isSimilar = true; ;
            for (int row = 0; row < rowsCount; row++) {
                for (int col = 0; col < colsCount; col++) {
                    int correctCostValue = Algorithm.GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col);
                    int factCostValue = int.Parse(matrixDataGridViewNextMatrix[col, row].Value as string);
                    if (correctCostValue != factCostValue) {
                        isSimilar = false;
                        matrixDataGridViewNextMatrix[col, row].ErrorText = "Ошибка";
                    }
                    else {
                        matrixDataGridViewNextMatrix[col, row].ErrorText = string.Empty;
                    }
                }
            }
            return isSimilar;
        }

        // Проверить следующую матрицу, построенную на первом шаге
        private void CheckFirstStage() {
            if (!IsNextMatrixCorrect()) {
                MarkError(AssignmentProblemError.IncorrectNextMatrixFormat);
                return;
            }
            int[,] correctNextMatrix = (int[,])adjacencyMatrix.Clone();
            Algorithm.FirstStage(correctNextMatrix, verticesCount);
            if (!CheckNextMatrixSimilarTo(correctNextMatrix, verticesCount)) {
                MarkError(AssignmentProblemError.FirstStageIncorrectNextMatrix);
                return;
            }
            adjacencyMatrix = correctNextMatrix;
            SetSecondStageState();
        }

        // Проверить следующую матрицу, построенную на втором шаге
        private void CheckSecondStage() {
            if (!IsNextMatrixCorrect()) {
                MarkError(AssignmentProblemError.IncorrectNextMatrixFormat);
                return;
            }
            int[,] correctNextMatrix = (int[,])adjacencyMatrix.Clone();
            Algorithm.SecondStage(correctNextMatrix, verticesCount);
            if (!CheckNextMatrixSimilarTo(correctNextMatrix, verticesCount)) {
                MarkError(AssignmentProblemError.FirstStageIncorrectNextMatrix);
                return;
            }
            adjacencyMatrix = correctNextMatrix;
            SetNextPathVertexWaitingState();
        }

        private void SetMatchingGraph() {
            matchingGraph = new bool[verticesCount, verticesCount];
            Algorithm.GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
            for (int row = 0; row < rowsCount; row++) {
                Algorithm.GetAdjacencyMatrixRowIndex(row, out int rowInAdjacency);
                for (int col = 0; col < colsCount; col++) {
                    Algorithm.GetAdjacencyMatrixColIndex(row, out int colInAdjacency);
                    if (Algorithm.GetCostFromAdjacencyMatrix(adjacencyMatrix, verticesCount, row, col) == 0)
                        matchingGraph[rowInAdjacency, colInAdjacency] = true;
                }
            }
        }

        // Проверить выбранную в аугментальный путь вершину
        private void CheckAugmentalPathVertex(Vertex vertex) {
            int selectedVertexIndex = vertex.Number - 1;
            // Если по этой вершине уже проходит путь - выходим
            if (curAugmentalPath.Contains(selectedVertexIndex))
                return;
            // Если цепь уже построена из двух непокрытых вершин, то её уже не продолжить - ошибка
            if (curAugmentalPath.Count == 2 && matchingPairsArray[curAugmentalPath.Last()] == -1)
                MarkError(AssignmentProblemError.ThirdStageAlternationBreakingOnNotMatchedEdge);
            // Если это первая вершина пути, она обязательно должна быть непокрытой паросочетанием
            if (curAugmentalPath.Count == 0) {
                if (matchingPairsArray[selectedVertexIndex] != -1) {
                    MarkError(AssignmentProblemError.ThirdStageStartOnMatchedVertex);
                    return;
                }
                curAugmentalPath.Add(selectedVertexIndex);
                vertex.BorderColor = Color.Red;
            }
            else {
                int lastVertexIndex = curAugmentalPath.Last();
                // Если ребра нет, то ошибка - перейти нельзя
                if (!matchingGraph[lastVertexIndex, selectedVertexIndex]) {
                    MarkError(AssignmentProblemError.ThirdStageMoveToFarVertex);
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
                        MarkError(AssignmentProblemError.ThirdStageAlternationBreakingOnMatchedEdge);
                        return;
                    }
                    // И наоборот
                    else if (!isPrevEdgeInMatching && !isCurEdgeInMatching) {
                        MarkError(AssignmentProblemError.ThirdStageAlternationBreakingOnNotMatchedEdge);
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


        // ----Обработчики событий
        // Выбор вершины графа
        private void VertexSelectedHandler(Vertex vertex) {
            if (problemState == AssignmentProblemState.NextPathVertexWaiting)
                CheckAugmentalPathVertex(vertex);
            else
                return;
        }

        // Выбор ребра графа
        private void EdgeSelectedHandler(Edge selectedEdge) {
            return;
        }


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

        // Очистить черновик
        private void buttonDraftClear_Click(object sender, EventArgs e) {
            textBoxDraft.Clear();
        }

        // Принятие ответа
        private void buttonAcceptAnswer_Click(object sender, EventArgs e) {
            if (problemState == AssignmentProblemState.FirstStage) {
                CheckFirstStage();
            }
            else if (problemState == AssignmentProblemState.SecondStage) {
                CheckSecondStage();
            }
        }

        // Начало новой итерации решения
        private void buttonReloadIteration_Click(object sender, EventArgs e) {
            if (problemState == AssignmentProblemState.StartWaiting)
                SetFirstStageState();
            else
                StartNewIteration();
        }

        // Перезагрузка задачи
        private void buttonReloadProblem_Click(object sender, EventArgs e) {
            //InitializeProblem(maxBipartiteMatchingExample, problemMode);
        }
    }
}
