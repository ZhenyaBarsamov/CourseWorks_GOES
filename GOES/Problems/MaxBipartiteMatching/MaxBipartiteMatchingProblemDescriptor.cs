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
            new MaxBipartiteMatchingProblemExample("Пример 1", "Пример 1",
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
                        )
        };

        public bool IsRandomExampleAvailable => false;
    }
}
