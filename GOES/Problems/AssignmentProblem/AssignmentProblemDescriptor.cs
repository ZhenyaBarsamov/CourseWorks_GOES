using System.Drawing;

namespace GOES.Problems.AssignmentProblem {
    class AssignmentProblemDescriptor : IProblemDescriptor {
        public string Name => "Задача о назначениях";

        public string Description => "Классическая задача о назначениях (линейная задача о назначениях). " +
            "Предполагается решение задачи с помощью классического Венгерского алгоритма.";

        public ProblemExample[] ProblemExamples => new ProblemExample[] { 
            new AssignmentProblemExample("Пример 1", "Три работника и три работы",
                            new[,] {
                                {0, 5, 0, 4, 0, 7},
                                {5, 0, 6, 0, 8, 0},
                                {0, 6, 0, 7, 0, 3},
                                {4, 0, 7, 0, 11, 0},
                                {0, 8, 0, 11, 0, 2},
                                {7, 0, 3, 0, 2, 0}},
                            new PointF[] {
                                new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
                                new PointF(75, 250), new PointF(425, 250)
                            }
                        ),
            new AssignmentProblemExample("Пример 2", "Четыре работника и четыре работы",
                            new[,] {
                                {0, 1, 0, 7, 0, 1, 0, 3},
                                {1, 0, 1, 0, 17, 0, 1, 0},
                                {0, 1, 0, 6, 0, 4, 0, 6},
                                {7, 0, 6, 0, 1, 0, 6, 0},
                                {0, 17, 0, 1, 0, 5, 0, 1},
                                {1, 0, 4, 0, 5, 0, 10, 0},
                                {0, 1, 0, 6, 0, 10, 0, 4},
                                {3, 0, 6, 0, 1, 0, 4, 0}},
                            new PointF[] {
                                new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
                                new PointF(75, 250), new PointF(425, 250), new PointF(75, 350), new PointF(425, 350)
                            }
                        ),
            new AssignmentProblemExample("Пример 3", "Четыре работника и четыре работы",
                            new[,] {
                                {0, 1, 0, 4, 0, 6, 0, 3},
                                {1, 0, 9, 0, 4, 0, 8, 0},
                                {0, 9, 0, 7, 0, 10, 0, 9},
                                {4, 0, 7, 0, 4, 0, 7, 0},
                                {0, 4, 0, 4, 0, 11, 0, 7},
                                {6, 0, 10, 0, 11, 0, 8, 0},
                                {0, 8, 0, 7, 0, 8, 0, 5},
                                {3, 0, 9, 0, 7, 0, 5, 0}},
                            new PointF[] {
                                new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
                                new PointF(75, 250), new PointF(425, 250), new PointF(75, 350), new PointF(425, 350)
                            }
                        ),
            new AssignmentProblemExample("Пример 4", "Четыре работника и четыре работы",
                            new[,] {
                                {0, 1, 0, 4, 0, 6, 0, 3},
                                {1, 0, 9, 0, 4, 0, 8, 0},
                                {0, 9, 0, 7, 0, 10, 0, 9},
                                {4, 0, 7, 0, 5, 0, 7, 0},
                                {0, 4, 0, 5, 0, 11, 0, 7},
                                {6, 0, 10, 0, 11, 0, 8, 0},
                                {0, 8, 0, 7, 0, 8, 0, 5},
                                {3, 0, 9, 0, 7, 0, 5, 0}},
                            new PointF[] {
                                new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
                                new PointF(75, 250), new PointF(425, 250), new PointF(75, 350), new PointF(425, 350),
                            }
                        ),
            new AssignmentProblemExample("Пример 5", "Шесть работников и шесть работ",
                            new[,] {
                                {0, 5, 0, 6, 0, 2, 0, 4, 0, 4, 0, 5},
                                {5, 0, 1, 0, 9, 0, 4, 0, 8, 0, 3, 0},
                                {0, 1, 0, 4, 0, 6, 0, 3, 0, 2, 0, 3},
                                {6, 0, 4, 0, 7, 0, 5, 0, 7, 0, 2, 0},
                                {0, 9, 0, 7, 0, 10, 0, 9, 0, 8, 0, 5},
                                {2, 0, 6, 0, 10, 0, 11, 0, 8, 0, 5, 0},
                                {0, 4, 0, 5, 0, 11, 0, 7, 0, 6, 0, 2},
                                {4, 0, 3, 0, 9, 0, 7, 0, 5, 0, 1, 0},
                                {0, 8, 0, 7, 0, 8, 0, 5, 0, 1, 0, 7},
                                {4, 0, 2, 0, 8, 0, 6, 0, 1, 0, 3, 0},
                                {0, 3, 0, 2, 0, 5, 0, 1, 0, 3, 0, 4},
                                {5, 0, 3, 0, 5, 0, 2, 0, 7, 0, 4, 0}},
                            new PointF[] {
                                new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
                                new PointF(75, 250), new PointF(425, 250), new PointF(75, 350), new PointF(425, 350),
                                new PointF(75, 450), new PointF(425, 450), new PointF(75, 550), new PointF(425, 550)
                            }
                        ),
            new AssignmentProblemExample("Тест", "Бесконечное решение - всегда находит паросочетание мощности 4. В общем, влияет порядок обхода графа.",
                            new[,] {
                                {0, 8, 0, 23, 0, 4, 0, 18, 0, 17},
                                {8, 0, 2, 0, 10, 0, 24, 0, 19, 0},
                                {0, 2, 0, 13, 0, 10, 0, 16, 0, 4},
                                {23, 0, 13, 0, 14, 0, 15, 0, 20, 0},
                                {0, 10, 0, 14, 0, 2, 0, 18, 0, 16},
                                {4, 0, 10, 0, 2, 0, 19, 0, 10, 0},
                                {0, 24, 0, 15, 0, 19, 0, 21, 0, 12},
                                {18, 0, 16, 0, 18, 0, 21, 0, 24, 0},
                                {0, 19, 0, 20, 0, 10, 0, 24, 0, 8},
                                {17, 0, 4, 0, 16, 0, 12, 0, 8, 0}},
                            new PointF[] {
                                new PointF(75, 50), new PointF(425, 50), new PointF(75, 150), new PointF(425, 150),
                                new PointF(75, 250), new PointF(425, 250), new PointF(75, 350), new PointF(425, 350),
                                new PointF(75, 450), new PointF(425, 450)
                            }
                        ),
        };

        public bool IsRandomExampleAvailable => true;
    }
}
