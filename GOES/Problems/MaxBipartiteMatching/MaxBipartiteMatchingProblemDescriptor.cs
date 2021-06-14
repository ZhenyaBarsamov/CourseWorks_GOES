using System.Drawing;

namespace GOES.Problems.MaxBipartiteMatching {
    /// <summary>
    /// Дескриптор задачи о максимальном паросочетании в двудольном графе
    /// </summary>
    class MaxBipartiteMatchingProblemDescriptor : IProblemDescriptor {
        public string Name => "Задача о максимальном паросочетании в двудольном графе";

        public string Description => 
            "Классическая задача на нахождение максимального (наибольшего по мощности) " +
            "паросочетания в двудольном графе. Предполагается решение задачи с помощью " +
            "классического алгоритма Куна";

        public ProblemExample[] ProblemExamples => new ProblemExample[] {
            new MaxBipartiteMatchingProblemExample("Пример 1", "Двудольный граф из десяти вершин в виде цепочки",
                            new[,] {
                                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                                {1, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                                {0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                                {0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
                                {0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                                {0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                                {0, 0, 0, 0, 0, 1, 0, 1, 0, 0},
                                {0, 0, 0, 0, 0, 0, 1, 0, 1, 0},
                                {0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                                {0, 0, 0, 0, 0, 0, 0, 0, 1, 0}},
                            new PointF[] {
                                new PointF(225, 50), new PointF(475, 50), new PointF(225, 150), new PointF(475, 150),
                                new PointF(225, 250), new PointF(475, 250), new PointF(225, 350), new PointF(475, 350),
                                new PointF(225, 450), new PointF(475, 450)
                            }
                        ),
            new MaxBipartiteMatchingProblemExample("Пример 2", "Двудольный граф из восьми вершин",
                            new[,] {
                                {0, 1, 0, 1, 0, 0, 0, 0},
                                {1, 0, 1, 0, 0, 0, 0, 0},
                                {0, 1, 0, 1, 0, 0, 0, 1},
                                {1, 0, 1, 0, 1, 0, 0, 0},
                                {0, 0, 0, 1, 0, 1, 0, 0},
                                {0, 0, 0, 0, 1, 0, 1, 0},
                                {0, 0, 0, 0, 0, 1, 0, 1},
                                {0, 0, 1, 0, 0, 0, 1, 0}},
                            new PointF[] {
                                new PointF(225, 50), new PointF(475, 50), new PointF(225, 175), new PointF(475, 175),
                                new PointF(225, 290), new PointF(475, 290), new PointF(225, 450), new PointF(475, 450)
                            }
                        ),
            new MaxBipartiteMatchingProblemExample("Пример 3", "Двудольный граф из восьми вершин",
                            new[,] {
                                {0, 1, 0, 1, 0, 0, 0, 1},
                                {1, 0, 1, 0, 1, 0, 1, 0},
                                {0, 1, 0, 1, 0, 1, 0, 1},
                                {1, 0, 1, 0, 1, 0, 0, 0},
                                {0, 1, 0, 1, 0, 0, 0, 1},
                                {0, 0, 1, 0, 0, 0, 1, 0},
                                {0, 1, 0, 0, 0, 1, 0, 1},
                                {1, 0, 1, 0, 1, 0, 1, 0}},
                            new PointF[] {
                                new PointF(125, 150), new PointF(125, 400), new PointF(275, 150), new PointF(275, 400),
                                new PointF(425, 150), new PointF(425, 400), new PointF(575, 150), new PointF(575, 400)
                            }
                        ),
            new MaxBipartiteMatchingProblemExample("Пример 4", "Двудольный граф из десяти вершин",
                            new[,] {
                                {0, 0, 0, 1, 0, 1, 0, 1, 1, 0},
                                {0, 0, 1, 0, 1, 0, 1, 0, 0, 0},
                                {0, 1, 0, 0, 0, 1, 0, 1, 1, 1},
                                {1, 0, 0, 0, 0, 0, 1, 0, 1, 0},
                                {0, 1, 0, 0, 0, 0, 0, 0, 1, 1},
                                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                                {0, 1, 0, 1, 0, 0, 0, 0, 1, 1},
                                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                                {0, 0, 0, 1, 0, 1, 0, 1, 1, 0},
                                {0, 0, 1, 0, 1, 0, 1, 0, 0, 0}},
                            new PointF[] {
                                new PointF(150, 50), new PointF(550, 50), new PointF(150, 150), new PointF(550, 150),
                                new PointF(150, 250), new PointF(550, 250), new PointF(150, 350), new PointF(550, 350),
                                new PointF(150, 450), new PointF(550, 450)
                            }
                        ),
        };

        public bool IsRandomExampleAvailable => true;
    }
}
