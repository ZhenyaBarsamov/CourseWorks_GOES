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
                                new PointF(225, 50), new PointF(475, 50), new PointF(225, 150), new PointF(475, 150),
                                new PointF(225, 250), new PointF(475, 250)
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
                                new PointF(225, 50), new PointF(475, 50), new PointF(225, 150), new PointF(475, 150),
                                new PointF(225, 250), new PointF(475, 250), new PointF(225, 350), new PointF(475, 350)
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
                                new PointF(225, 50), new PointF(475, 50), new PointF(225, 150), new PointF(475, 150),
                                new PointF(225, 250), new PointF(475, 250), new PointF(225, 350), new PointF(475, 350)
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
                                new PointF(225, 50), new PointF(475, 50), new PointF(225, 150), new PointF(475, 150),
                                new PointF(225, 250), new PointF(475, 250), new PointF(225, 350), new PointF(475, 350),
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
                                new PointF(225, 50), new PointF(475, 50), new PointF(225, 150), new PointF(475, 150),
                                new PointF(225, 250), new PointF(475, 250), new PointF(225, 350), new PointF(475, 350),
                                new PointF(225, 450), new PointF(475, 450), new PointF(225, 550), new PointF(475, 550)
                            }
                        ),
        };

        public bool IsRandomExampleAvailable => false;
    }
}
