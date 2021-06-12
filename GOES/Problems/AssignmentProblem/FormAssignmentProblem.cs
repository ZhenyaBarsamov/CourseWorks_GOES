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
        // ----Атрибуты для алгоритма решения (работа с матрицей)
        private int[,] correctNextAdjacencyMatrix;
        // ----Атрибуты для алгоритма решения (этап поиска паросочетания)
        private bool[,] matchingGraphAdjacencyMatrix; // полная матрица смежности графа, для которого ищется максимальное паросочетание
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
            // Сохраняем матрицу графа для решения. Создаём нужные коллекции
            adjacencyMatrix = (int[,])assignmentProblemExample.CostsMatrix.Clone();
            verticesCount = assignmentProblemExample.GraphMatrix.GetLength(0);
            curAugmentalPath = new List<int>();
            matchingPairsArray = new int[verticesCount];
            // Пишем условие задачи
            textLabelExampleDescription.Text =
                "Задана матрица стоимостей назначений. Найдите самое выгодное назначение - назначение с минимальной стоимостью.";
            DisplayCostsMatrix(matrixDataGridViewExampleMatrix, assignmentProblemExample.CostsMatrix, verticesCount);
            matrixDataGridViewExampleMatrix.ReadOnly = true;
            matrixDataGridViewExampleMatrix.IsCellsSelectable = false;
            matrixDataGridViewCurMatrix.IsCellsSelectable = false;
            matrixDataGridViewNextMatrix.IsCellsSelectable = problemMode == ProblemMode.Solution;
            // Получаем решение задачи
            Algorithm.GetAssignmentProblemSolution(adjacencyMatrix, verticesCount, out correctAssignmentMinCost);
            // Инициализируем статистику решения
            if (problemMode == ProblemMode.Solution)
                assignmentProblemStatistics = new AssignmentProblemStatistics();
            // Ставим решение в состояние ожидания начала
            SetStartWaitingState();
        }

        public IProblemDescriptor ProblemDescriptor => new AssignmentProblemDescriptor();
        public ProblemExample ProblemExample => assignmentProblemExample;

        private AssignmentProblemStatistics assignmentProblemStatistics;
        public IProblemStatistics ProblemStatistics => assignmentProblemStatistics;



        // ----Методы для работы с ходом решения/демонстрации
        // Начать новую итерацию решения
        private void StartNewIteration() {
            if (problemState == AssignmentProblemState.FirstStage) {
                SetFirstStageState();
                // Убираем все отметки на матрице, которые были (ошибки и т.д.)
                ClearMatrixColors(matrixDataGridViewNextMatrix);
            }
            else if (problemState == AssignmentProblemState.SecondStage) {
                SetSecondStageState();
                // Убираем все отметки на матрице, которые были (ошибки и т.д.)
                ClearMatrixColors(matrixDataGridViewNextMatrix);
            }
            else if (problemState == AssignmentProblemState.NextPathVertexWaiting) {
                // Очищаем текущий маршрут
                curAugmentalPath.Clear();
                // Убираем выделение маршрута цветом
                graphVisInterface.ResetVerticesBorderColor();
                graphVisInterface.ResetEdgesColor();
                // Если это демонстрация, мы должны обнулить уже рассматриваемое решение
                selectedAugmentalPath = null;
                SetNextPathVertexWaitingState();
            }
            else if (problemState == AssignmentProblemState.FourthStage) {
                SetFourthStageState();
                // Убираем все отметки на матрице, которые были (ошибки и т.д.)
                ClearMatrixColors(matrixDataGridViewNextMatrix);
            }
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
            matrixDataGridViewCurMatrix.ReadOnly = 
                matrixDataGridViewNextMatrix.ReadOnly = true;
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
                case AssignmentProblemState.AssignmentCostWaiting:
                    // Стоимость назначения - это одно целое неотрицательное число
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

        // Подготовить всё для поиска паросочетания: создать нужный граф, отобразить его на экране, обнулить текущее паросочетание 
        // и текущую аугментальную цепь
        private void PrepareMatchingGraph() {
            // Получаем граф, для которого на текущем шаге необходимо найти максимальное паросочетание.
            // Это двудольный граф, рёбра которого соответствуют нулевым элементам текущей матрицы смежности двудольного графа
            matchingGraphAdjacencyMatrix = Algorithm.GetMatchingGraphAdjacencyMatrix(adjacencyMatrix, verticesCount);
            // Инициализируем визуализацию
            visGraph = new Graph(matchingGraphAdjacencyMatrix, false);
            graphVisInterface.Initialize(visGraph);
            // Задаём расположение вершин графа в соответствии с настройками задачи
            for (int i = 0; i < visGraph.VerticesCount; i++)
                visGraph.Vertices[i].DrawingCoords = assignmentProblemExample.DefaultGraphLayout[i];
            // Отключаем отображение номеров вершин и ставим на вершины метки с их нумерацией в пределах своей доли
            graphVisInterface.Settings.IsVertexNumberVisible = false;
            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
                visGraph.Vertices[vertexIndex].Label = $"{vertexIndex / 2 + 1}";

            // Очищаем текущее паросочетание
            for (int i = 0; i < verticesCount; i++)
                matchingPairsArray[i] = -1;
            // Очищаем текущий аугментальный путь
            curAugmentalPath.Clear();
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
                    if (cell.Value as string == null || !regex.IsMatch(cell.Value as string)) {
                        isCorrect = false;
                        matrixDataGridViewNextMatrix.SetCellColor(cell.RowIndex, cell.ColumnIndex, Color.Red);
                    }
                    else {
                        matrixDataGridViewNextMatrix.SetCellColorToDefault(cell.RowIndex, cell.ColumnIndex);
                    }
            return isCorrect;
        }

        // Очистить все цветовые метки в заданном элементе управления matrixDgv
        private void ClearMatrixColors(MatrixDataGridView matrixDgv) {
            matrixDgv.SetCellsColorsToDefault();
            matrixDgv.SetCellsFontColorToDefault();
        }


        // ----Методы изменения состояния (этапа) решения задания
        // Перевести задачу в состояние ожидания начала
        private void SetStartWaitingState() {
            problemState = AssignmentProblemState.StartWaiting;
            graphVisInterface.Initialize(null);
            // Отображаем текущую матрицу и текущую матрицу для внесения в неё изменений
            DisplayCostsMatrix(matrixDataGridViewCurMatrix, adjacencyMatrix, verticesCount);
            DisplayCostsMatrix(matrixDataGridViewNextMatrix, adjacencyMatrix, verticesCount);
            // Если демонстрация - очищаем все пометки
            if (problemMode == ProblemMode.Demonstration)
                ClearMatrixColors(matrixDataGridViewCurMatrix);
            if (problemMode == ProblemMode.Solution)
                matrixDataGridViewNextMatrix.ReadOnly = true;
            // Убираем все отметки на матрице, которые были (ошибки и т.д.)
            ClearMatrixColors(matrixDataGridViewNextMatrix);
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
            graphVisInterface.Initialize(null);
            // Отображаем текущую матрицу и текущую матрицу для внесения в неё изменений
            DisplayCostsMatrix(matrixDataGridViewCurMatrix, adjacencyMatrix, verticesCount);
            DisplayCostsMatrix(matrixDataGridViewNextMatrix, adjacencyMatrix, verticesCount);
            if (problemMode == ProblemMode.Solution)
                matrixDataGridViewNextMatrix.ReadOnly = false;
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
                    "В матрице, находящейся в блоке \"Текущая матрица\", минимальные элементы в каждой строке выделены зелёным." + Environment.NewLine +
                    "В матрице, находящейся в блоке \"Следующая матрица\", указаны значения после завершения данного шага." + Environment.NewLine +
                    "Для того, чтобы сделать очередной шаг решения, нажимайте кнопку \"Сделать шаг\"." + Environment.NewLine +
                    "Для каких-либо заметок Вы можете использовать текстовое поле в блоке \"Черновик\". Он не проверяется." + Environment.NewLine +
                    "Вы можете вернуться к началу текущего шага кнопкой \"К началу шага\"." +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine;
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, false, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
            // Сохраняем правильную следующую матрицу
            correctNextAdjacencyMatrix = (int[,])adjacencyMatrix.Clone();
            Algorithm.FirstStage(correctNextAdjacencyMatrix, verticesCount);
            // Если демонстрация
            if (problemMode == ProblemMode.Demonstration) {
                // Очищаем все пометки
                ClearMatrixColors(matrixDataGridViewCurMatrix);
                // Отмечаем в каждой строке на текущей матрице минимальные элементы
                Algorithm.GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
                for (int row = 0; row < rowsCount; row++) {
                    int col = Algorithm.GetColOfMinimumInRow(adjacencyMatrix, verticesCount, row);
                    matrixDataGridViewCurMatrix.SetCellColor(row, col, Color.Green);
                }
                // И показываем следующую матрицу
                DisplayCostsMatrix(matrixDataGridViewNextMatrix, correctNextAdjacencyMatrix, verticesCount);
            }
        }

        // Перевести задачу в состояние ожидания матрицы второго шага
        private void SetSecondStageState() {
            problemState = AssignmentProblemState.SecondStage;
            graphVisInterface.Initialize(null);
            // Отображаем текущую матрицу и текущую матрицу для внесения в неё изменений
            DisplayCostsMatrix(matrixDataGridViewCurMatrix, adjacencyMatrix, verticesCount);
            DisplayCostsMatrix(matrixDataGridViewNextMatrix, adjacencyMatrix, verticesCount);
            if (problemMode == ProblemMode.Solution)
                matrixDataGridViewNextMatrix.ReadOnly = false;
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
                    "Шаг второй - добиться, чтобы в каждом столбце матрицы был хотя бы один нулевой элемент." + Environment.NewLine +
                    "Для этого необходимо вычесть из каждого столбца матрицы его минимальный элемент." + Environment.NewLine +
                    "В матрице, находящейся в блоке \"Текущая матрица\", минимальные элементы в каждом столбце выделены зелёным." + Environment.NewLine +
                    "В матрице, находящейся в блоке \"Следующая матрица\", указаны значения после завершения данного шага." + Environment.NewLine + 
                    "Для того, чтобы сделать очередной шаг решения, нажимайте кнопку \"Сделать шаг\"." + Environment.NewLine +
                    "Для каких-либо заметок Вы можете использовать текстовое поле в блоке \"Черновик\". Он не проверяется." + Environment.NewLine +
                    "Если Вы хотите сбросить значения в матрице блока \"Следующая матрица\" на текущие, " +
                    "нажмите кнопку \"К началу шага\". Это не повлияет на оценку." + Environment.NewLine +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать решение, нажмите кнопку \"К началу шага\".";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, false, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
            // Сохраняем правильную следующую матрицу
            correctNextAdjacencyMatrix = (int[,])adjacencyMatrix.Clone();
            Algorithm.SecondStage(correctNextAdjacencyMatrix, verticesCount);
            // Если демонстрация
            if (problemMode == ProblemMode.Demonstration) {
                // Очищаем все пометки
                ClearMatrixColors(matrixDataGridViewCurMatrix);
                // Отмечаем в каждом столбце на текущей матрице минимальные элементы
                Algorithm.GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
                for (int col = 0; col < rowsCount; col++) {
                    int row = Algorithm.GetRowOfMinimumInCol(adjacencyMatrix, verticesCount, col);
                    matrixDataGridViewCurMatrix.SetCellColor(row, col, Color.Green);
                }
                // И показываем следующую матрицу
                DisplayCostsMatrix(matrixDataGridViewNextMatrix, correctNextAdjacencyMatrix, verticesCount);
            }
        }

        // Перевести задачу в состояние ожидания вершины для аугментального маршрута
        private void SetNextPathVertexWaitingState() {
            problemState = AssignmentProblemState.NextPathVertexWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.Interactive;
            graphVisInterface.IsVerticesMoving = true;
            if (problemMode == ProblemMode.Solution)
                matrixDataGridViewNextMatrix.ReadOnly = true;
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
            // Сохраняем правильную мощность текущего максимального паросочетания
            correctMaximalMatchingCardinality = Algorithm.GetMatchingCardinality(Algorithm.GetMaximalMatching(matchingGraphAdjacencyMatrix, verticesCount));
            // Если демонстрация
            if (problemMode == ProblemMode.Demonstration) {
                // Очищаем все пометки
                ClearMatrixColors(matrixDataGridViewCurMatrix);
            }
        }

        // Перевести задачу в состояние ожидания матрицы четвёртого шага
        private void SetFourthStageState() {
            problemState = AssignmentProblemState.FourthStage;
            // Отображаем текущую матрицу и текущую матрицу для внесения в неё изменений
            DisplayCostsMatrix(matrixDataGridViewCurMatrix, adjacencyMatrix, verticesCount);
            DisplayCostsMatrix(matrixDataGridViewNextMatrix, adjacencyMatrix, verticesCount);
            if (problemMode == ProblemMode.Solution)
                matrixDataGridViewNextMatrix.ReadOnly = false;
            string message = "";
            if (problemMode == ProblemMode.Solution)
                message =
                    "Шаг четвёртый - перераспределить нули матрицы, если назначение построить пока не удалось." + Environment.NewLine +
                    "Для этого нужно проделать следующее." + Environment.NewLine +
                    "1. Отметить в текущей матрице нули, соответствуюшие рёбрам найденного паросочетания. " +
                    "Назовём их \"красными\" нулями." + Environment.NewLine +
                    "2. Отметить все строки, в которых нет \"красных\" нулей." + Environment.NewLine +
                    "3. Отметить все столбцы, в которых есть нули в отмеченных строках." + Environment.NewLine +
                    "4. Отметить все строки, в которых есть \"красные\" нули в отмеченных столбцах." + Environment.NewLine +
                    "5. Вычеркнуть все отмеченные столбцы и все неотмеченные строки." + Environment.NewLine +
                    "6. Найти наименьший невычеркнутый элемент." + Environment.NewLine +
                    "7. Вычесть его из всех невычеркнутых элементов. Прибавить его к элементам, стоящим на " +
                    "пересечении вычеркнутых строк и столбцов." + Environment.NewLine +
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
                    "Шаг четвёртый - проверить построенное назначение и, если оно не является законченным, перераспределить нули матрицы." + Environment.NewLine +
                    "Если назначение полностью построено, то задача решена. Если нет, то необходимо проделать следующее." + Environment.NewLine +
                    "1. Отметить в текущей матрице нули, соответствуюшие рёбрам найденного паросочетания. " +
                    "Назовём их \"красными\" нулями." + Environment.NewLine +
                    "2. Отметить все строки, в которых нет \"красных\" нулей." + Environment.NewLine +
                    "3. Отметить все столбцы, в которых есть нули в отмеченных строках." + Environment.NewLine +
                    "4. Отметить все строки, в которых есть \"красные\" нули в отмеченных столбцах." + Environment.NewLine +
                    "5. Вычеркнуть все отмеченные столбцы и все неотмеченные строки." + Environment.NewLine +
                    "6. Найти наименьший невычеркнутый элемент." + Environment.NewLine +
                    "7. Вычесть его из всех невычеркнутых элементов. Прибавить его к элементам, стоящим на " +
                    "пересечении вычеркнутых строк и столбцов." + Environment.NewLine +
                    "В матрице, находящейся в блоке \"Текущая матрица\", сделаны отметки. Серым выделены вычеркнутые строки и столбцы, красным " +
                    "выделены клетки с \"красными\" нулями. Зелёным выделен минимальный невычеркнутый элемент." + Environment.NewLine +
                    "В матрице, находящейся в блоке \"Следующая матрица\", указаны значения после завершения данного шага." + Environment.NewLine +
                    "Для каких-либо заметок Вы можете использовать текстовое поле в блоке \"Черновик\". Он не проверяется." + Environment.NewLine +
                    "Если Вы хотите сбросить значения в матрице блока \"Следующая матрица\" на текущие, " +
                    "нажмите кнопку \"К началу шага\". Это не повлияет на оценку." + Environment.NewLine +
                    "Если Вы хотите начать задание заново, нажмите кнопку \"Начать заново\"." + Environment.NewLine +
                    "Если Вы хотите вспомнить тему, откройте текст лекции с помощью кнопки \"Текст лекции\"." + Environment.NewLine +
                    "Чтобы начать решение, нажмите кнопку \"К началу шага\".";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, false, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(true, false, "Сделать шаг");
            buttonReloadIteration.Enabled = true;
            // Сохраняем правильную следующую матрицу
            correctNextAdjacencyMatrix = (int[,])adjacencyMatrix.Clone();
            Algorithm.FourthStage(correctNextAdjacencyMatrix, matchingPairsArray, verticesCount);
            // Если демонстрация
            if (problemMode == ProblemMode.Demonstration) {
                // Очищаем все пометки
                ClearMatrixColors(matrixDataGridViewCurMatrix);
                // Отмечаем "красные" нули
                var redZeroesCells = Algorithm.GetRedZeroes(matchingPairsArray, verticesCount);
                foreach (var cellCoord in redZeroesCells)
                    matrixDataGridViewCurMatrix.SetCellColor(cellCoord.Item1, cellCoord.Item2, Color.Red);
                // Отмечаем вычеркнутые строки и столбцы, минимум среди невычеркнутых элементов
                Algorithm.GetLinedRowsAndCols(adjacencyMatrix, matchingPairsArray, verticesCount, out HashSet<int> linedRows, out HashSet<int> linedCols, out int minElemRow, out int minElemCol);
                foreach (var row in linedRows)
                    matrixDataGridViewCurMatrix.SetRowHeaderColor(row, Color.Gray);
                foreach (var col in linedCols)
                    matrixDataGridViewCurMatrix.SetColumnHeaderColor(col, Color.Gray);
                matrixDataGridViewCurMatrix.SetCellColor(minElemRow, minElemCol, Color.Green);
                // И показываем следующую матрицу
                DisplayCostsMatrix(matrixDataGridViewNextMatrix, correctNextAdjacencyMatrix, verticesCount);
            }
        }


        // Проверить значение стоимости построенного оптимального назначения
        private void SetAssignmentCostWaitingState() {
            problemState = AssignmentProblemState.AssignmentCostWaiting;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            if (problemMode == ProblemMode.Solution)
                matrixDataGridViewNextMatrix.ReadOnly = true;
            string message = "";
            if (problemMode == ProblemMode.Solution)
                message =
                    "Оптимальное назначение построено." + Environment.NewLine +
                    "Введите в поле для ответов его общую стоимость в виде одного целого числа.";
            else if (problemMode == ProblemMode.Demonstration)
                message =
                    "Оптимальное назначение построено." + Environment.NewLine +
                    $"Его общая стоимость в соответствии с условием задачи равна {correctAssignmentMinCost}.";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(true, true, "Принять ответ");
            buttonReloadIteration.Enabled = false;
        }

        // Перевести задачу в состояние выполненной задачи
        private void SetProblemFinish() {
            problemState = AssignmentProblemState.ProblemFinish;
            graphVisInterface.InteractiveMode = InteractiveMode.NonInteractive;
            graphVisInterface.IsVerticesMoving = false;
            string message =
                "Оптимальное назначение найдено." + Environment.NewLine +
                "Задание успешно выполнено!";
            ShowSuccessTip(message);
            if (problemMode == ProblemMode.Solution)
                SetAnswerGroupBoxState(false, false, "Принять ответ");
            else if (problemMode == ProblemMode.Demonstration)
                SetAnswerGroupBoxState(false, false, "Сделать шаг");
            buttonReloadIteration.Enabled = false;
            // Отмечаем, что задача была решена
            if (problemMode == ProblemMode.Solution) {
                assignmentProblemStatistics.IsSolved = true;
                Close();
            }
        }

        // Перевести задачу в псевдосостояние ожидания чередования по построенному аугментальному пути (для режима демонстрации)
        private void SetAlternationWaitState() {
            problemState = AssignmentProblemState.NextPathVertexWaiting; // Само состояние совпадает с состоянием ожидания очередной вершины
            graphVisInterface.InteractiveMode = InteractiveMode.Interactive;
            graphVisInterface.IsVerticesMoving = true;
            string message =
                "Аугментальная цепь построена." + Environment.NewLine +
                "Нажмите кнопку \"Сделать шаг\", чтобы провести чередование рёбер по построенному аугментальному пути. " +
                "При этом рёбра, входящие в паросочетание (выделены жирным и красным), будут удалены из него, " +
                "а оставшиеся рёбра цепи (выделены зелёным), наоборот, будут добавлены в него. Таким образом мощность паросочетания увеличится на единицу.";
            ShowSuccessTip(message);
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
                case AssignmentProblemError.IncorrectNextMatrixFormat:
                    assignmentProblemStatistics.IncorrectNextMatrixFormatCount += 1;
                    errorMessage = "Ошибка при заполнении матрицы. Матрица должна содержать исключительно целые неотрицательные числа. " +
                        "Ошибочные ячейки выделены красным цветом.";
                    break;
                case AssignmentProblemError.FirstStageIncorrectNextMatrix:
                    assignmentProblemStatistics.FirstStageIncorrectNextMatrixCount += 1;
                    errorMessage = 
                        "Ошибка в матрице. Выделенные красным значения не совпадают с правильными. Проверьте свои вычисления, " +
                        "и попробуйте снова." + Environment.NewLine + 
                        "На данном этапе необходимо добиться того, чтобы в каждой строке матрицы появился нулевой элемент. Для этого из каждой " +
                        "строки матрицы необходимо вычесть её минимальный элемент.";
                    break;
                case AssignmentProblemError.SecondStageIncorrectNextMatrix:
                    assignmentProblemStatistics.SecondStageIncorrectNextMatrixCount += 1;
                    errorMessage = "Ошибка в матрице. Выделенные красным значения не совпадают с правильными. Проверьте свои вычисления, " +
                        "и попробуйте снова." + Environment.NewLine +
                        "На данном этапе необходимо добиться того, чтобы в каждом столбце матрицы появился нулевой элемент. Для этого из каждого " +
                        "столбца матрицы необходимо вычесть его минимальный элемент.";
                    break;
                case AssignmentProblemError.ThirdStageStartOnMatchedVertex:
                    assignmentProblemStatistics.ThirdStageStartOnMatchedVertexCount += 1;
                    errorMessage = "Построение чередующегося пути необходимо начинать с вершины, которая не покрыта паросочетанием";
                    break;
                case AssignmentProblemError.ThirdStageMoveToFarVertex:
                    assignmentProblemStatistics.ThirdStageMoveToFarVertexCount += 1;
                    errorMessage =
                        "Вы попытались перейти в вершину, которая не соединена ребром с последней вершиной построенного маршрута." + Environment.NewLine +
                        $"Последней (текущей) вершиной маршрута является вершина {curAugmentalPath.Last() + 1}.";
                    break;
                case AssignmentProblemError.ThirdStageAlternationBreakingOnMatchedEdge:
                    assignmentProblemStatistics.ThirdStageAlternationBreakingOnMatchedEdgeCount += 1;
                    prevLastVertex = curAugmentalPath[curAugmentalPath.Count - 2] + 1;
                    lastVertex = curAugmentalPath.Last() + 1;
                    prevEdge = $"{{{prevLastVertex};{lastVertex}}}";
                    errorMessage =
                        "Выбранная Вами вершина для продолжения маршрута нарушает чередование непаросочетанных рёбер с паросочетанными." + Environment.NewLine +
                        $"Последнее ребро построенного маршрута {prevEdge} входит в паросочетание. Значит, следующее ребро не должно входить в него.";
                    break;
                case AssignmentProblemError.ThirdStageAlternationBreakingOnNotMatchedEdge:
                    assignmentProblemStatistics.ThirdStageAlternationBreakingOnNotMatchedEdgeCount += 1;
                    prevLastVertex = curAugmentalPath[curAugmentalPath.Count - 2] + 1;
                    lastVertex = curAugmentalPath.Last() + 1;
                    prevEdge = $"{{{prevLastVertex};{lastVertex}}}";
                    errorMessage =
                        "Выбранная Вами вершина для продолжения маршрута нарушает чередование непаросочетанных рёбер с паросочетанными." + Environment.NewLine +
                        $"Последнее ребро построенного маршрута {prevEdge} не входит в паросочетание. Значит, следующее ребро должно входить в него.";
                    break;
                case AssignmentProblemError.ThirdStageAugmentalPathIsNotFinished:
                    assignmentProblemStatistics.ThirdStageAugmentalPathIsNotFinishedCount += 1;
                    errorMessage =
                        "Вы попытались провести чередование по аугментальной цепи, которая ещё не была закончена: аугментальная цепь не может быть пустой " +
                        "и не может состоять из одной вершины";
                    break;
                case AssignmentProblemError.ThirdStageIncorrectAugmentalPath:
                    assignmentProblemStatistics.ThirdStageIncorrectAugmentalPathCount += 1;
                    errorMessage =
                        "Вы попытались провести чередование по неправильной (незаконченной) аугментальной цепи: аугментальная цепь должна " +
                        "начинаться и заканчиваться вершинами, не покрытыми паросочетанием, и при этом чередовать непаросочетанные рёбра " +
                        "с паросочетанными";
                    break;
                case AssignmentProblemError.FourthStageIncorrectNextMatrix:
                    assignmentProblemStatistics.FourthStageIncorrectNextMatrixCount += 1;
                    errorMessage = "Ошибка в матрице. Выделенные красным значения не совпадают с правильными. Проверьте свои вычисления, " +
                        "и попробуйте снова." + Environment.NewLine +
                        "На данном этапе необходимо перераспределить нули в матрице. Для этого:" + Environment.NewLine +
                        "1. Отметить в текущей матрице нули, соответствуюшие рёбрам найденного паросочетания. " +
                        "Назовём их \"красными\" нулями." + Environment.NewLine +
                        "2. Отметить все строки, в которых нет \"красных\" нулей." + Environment.NewLine +
                        "3. Отметить все столбцы, в которых есть нули в отмеченных строках." + Environment.NewLine +
                        "4. Отметить все строки, в которых есть \"красные\" нули в отмеченных столбцах." + Environment.NewLine +
                        "5. Вычеркнуть все отмеченные столбцы и все неотмеченные строки." + Environment.NewLine +
                        "6. Найти наименьший невычеркнутый элемент." + Environment.NewLine + 
                        "7. Вычесть его из всех невычеркнутых элементов. Прибавить его к элементам, стоящим на " +
                        "пересечении вычеркнутых строк и столбцов.";
                    break;
                case AssignmentProblemError.IncorrectAssignmentCostFormat:
                    assignmentProblemStatistics.IncorrectAssignmentCostFormatCount += 1;
                    errorMessage = "Неправильный формат значения стоимости назначения." + Environment.NewLine +
                        "Значение стоимости вводится в формате одного целого неотрицательного числа.";
                    break;
                case AssignmentProblemError.IncorrectAssignmentCost:
                    assignmentProblemStatistics.IncorrectAssignmentCostCount += 1;
                    errorMessage = "Неправильное значение стоимости назначения. Суммарная стоимость назначения - это сумма стоймостей " +
                        "выбранных назначений, которые даны в условии задачи. Пересчитайте его, и попробуйте снова.";
                    break;
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
                        matrixDataGridViewNextMatrix.SetCellColor(row, col, Color.Red);
                    }
                    else {
                        matrixDataGridViewNextMatrix.SetCellColorToDefault(row, col);
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
            if (!CheckNextMatrixSimilarTo(correctNextAdjacencyMatrix, verticesCount)) {
                MarkError(AssignmentProblemError.FirstStageIncorrectNextMatrix);
                return;
            }
            adjacencyMatrix = correctNextAdjacencyMatrix;
            SetSecondStageState();
        }

        // Проверить следующую матрицу, построенную на втором шаге
        private void CheckSecondStage() {
            if (!IsNextMatrixCorrect()) {
                MarkError(AssignmentProblemError.IncorrectNextMatrixFormat);
                return;
            }
            if (!CheckNextMatrixSimilarTo(correctNextAdjacencyMatrix, verticesCount)) {
                MarkError(AssignmentProblemError.FirstStageIncorrectNextMatrix);
                return;
            }
            adjacencyMatrix = correctNextAdjacencyMatrix;
            PrepareMatchingGraph();
            SetNextPathVertexWaitingState();
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
                if (!matchingGraphAdjacencyMatrix[lastVertexIndex, selectedVertexIndex]) {
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

        // Проверить попытку чередования по построенному аугменатльному маршруту
        private void AlternateOnAugmentalPath() {
            // Если маршрут пустой, или состоит из одной вершины - цепь не построена
            if (curAugmentalPath.Count <= 1) {
                MarkError(AssignmentProblemError.ThirdStageAugmentalPathIsNotFinished);
                return;
            }
            // Последняя вершина цепи должна быть непокрытой, в любом случае (как и первая, но это мы проверяем при выборе вершины)
            if (matchingPairsArray[curAugmentalPath.Last()] != -1) {
                MarkError(AssignmentProblemError.ThirdStageIncorrectAugmentalPath);
                return;
            }
            // Проводим чередование по построенной аугментальной цепи
            Algorithm.AlternateOnAugmentalPath(curAugmentalPath, matchingPairsArray);
            UpdateGraphMatching();
            StartNewIteration();
            // Проверяем, а не построено ли уже максимальное паросочетание
            if (Algorithm.GetMatchingCardinality(matchingPairsArray) == correctMaximalMatchingCardinality) {
                Algorithm.GetCostsMatrixDimensions(verticesCount, out int rowsCount, out int colsCount);
                // Если было построено ещё не совершенное паросочетание - проверяем следующую матрицу
                if (Algorithm.GetMatchingCardinality(matchingPairsArray) < rowsCount) {
                    SetFourthStageState();
                }
                else {
                    SetAssignmentCostWaitingState();
                }
            }
            else {
                SetNextPathVertexWaitingState();
            }
        }

        // Проверить общую стоимость назначения
        private void CheckAssignmentCost(string answer) {
            int minCost = int.Parse(answer);
            if (minCost == correctAssignmentMinCost) {
                SetProblemFinish();
            }
            else {
                MarkError(AssignmentProblemError.IncorrectAssignmentCost);
            }
        }

        // Проверить общую стоимость назначения
        private void CheckAssignmentCost() {
            string answer = textBoxAnswer.Text.Trim().ToLower();
            if (!IsAnswerCorrect(answer)) {
                MarkError(AssignmentProblemError.IncorrectAssignmentCostFormat);
                return;
            }
            CheckAssignmentCost(answer);
        }

        // Проверить следующую матрицу, построенную на втором шаге
        private void CheckFourthStage() {
            if (!IsNextMatrixCorrect()) {
                MarkError(AssignmentProblemError.IncorrectNextMatrixFormat);
                return;
            }
            if (!CheckNextMatrixSimilarTo(correctNextAdjacencyMatrix, verticesCount)) {
                MarkError(AssignmentProblemError.FourthStageIncorrectNextMatrix);
                return;
            }
            adjacencyMatrix = correctNextAdjacencyMatrix;
            PrepareMatchingGraph();
            SetNextPathVertexWaitingState();
        }


        // ----Методы для демонстрации решения
        private void DoAnswerForDemonstration() {
            switch (problemState) {
                case AssignmentProblemState.FirstStage:
                    CheckFirstStage();
                    break;
                case AssignmentProblemState.SecondStage:
                    CheckSecondStage();
                    break;
                case AssignmentProblemState.NextPathVertexWaiting:
                    // Если мы уже целиком построили аугментальный путь - проводим чередование и строим новый путь
                    if (curAugmentalPath.Count > 0 && selectedPathVertexIndex >= selectedAugmentalPath.Count) {
                        AlternateOnAugmentalPath();
                    }
                    // Иначе - продолжаем строить путь
                    else {
                        // Если аугментального пути ещё не было или он кончился, строим новый
                        if (selectedAugmentalPath == null || selectedPathVertexIndex >= selectedAugmentalPath.Count) {
                            selectedAugmentalPath =
                                Algorithm.GetRandomAugmentalPath(matchingGraphAdjacencyMatrix, verticesCount, matchingPairsArray);
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
                case AssignmentProblemState.FourthStage:
                    CheckFourthStage();
                    break;
                case AssignmentProblemState.AssignmentCostWaiting:
                    string answer = correctAssignmentMinCost.ToString();
                    CheckAssignmentCost(answer);
                    break;
                default:
                    break;
            }
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
            if (problemMode == ProblemMode.Solution) {
                if (problemState == AssignmentProblemState.FirstStage) {
                    CheckFirstStage();
                }
                else if (problemState == AssignmentProblemState.SecondStage) {
                    CheckSecondStage();
                }
                else if (problemState == AssignmentProblemState.NextPathVertexWaiting) {
                    AlternateOnAugmentalPath();
                }
                else if (problemState == AssignmentProblemState.FourthStage) {
                    CheckFourthStage();
                }
                else if (problemState == AssignmentProblemState.AssignmentCostWaiting) {
                    CheckAssignmentCost();
                }
            }
            else if (problemMode == ProblemMode.Demonstration) {
                DoAnswerForDemonstration();
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
            InitializeProblem(assignmentProblemExample, problemMode);
        }
    }
}
