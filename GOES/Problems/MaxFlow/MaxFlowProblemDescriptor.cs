﻿using System.Drawing;

namespace GOES.Problems.MaxFlow {
    /// <summary>
    /// Дескриптор задачи о максимальном потоке (и минимальном разрезе)
    /// </summary>
    class MaxFlowProblemDescriptor : IProblemDescriptor {
        public string Name => "Задача о максимальном потоке (и минимальном разрезе) в сети";

        public string Description =>
            "Классическая задача на нахождение максимального потока и минимального " +
            "разреза в сети. Предполагается решение задачи с помощью классического " +
            "алгоритма Форда-Фалкерсона";

        public ProblemExample[] ProblemExamples => new MaxFlowProblemExample[] {
            new MaxFlowProblemExample("Пример 1", "Пример 1", 0, 6,
                            new int[,] {
                                {0, 15, 0, 25, 0, 0, 0 },
                                {0, 0, 16, 7, 0, 0, 0 },
                                {0, 0, 0, 0, 4, 0, 9 },
                                {0, 0, 0, 0, 18, 3, 0 },
                                {0, 0, 0, 0, 0, 6, 25 },
                                {0, 2, 3, 0, 0, 0, 0 },
                                {0, 0, 0, 0, 0, 0, 0 }},
                            new PointF[] {
                                new PointF(125, 250), new PointF(275, 100), new PointF(425, 100), new PointF(275, 400),
                                new PointF(425, 400), new PointF(350, 250), new PointF(575, 250)
                            }
                        ),
            new MaxFlowProblemExample("Пример 2", "Пример 2", 0, 3,
                            new [,] {
                                {0, 7, 0, 0, 5, 0, 3, 0 },
                                {0, 0, 2, 0, 0, 0, 0, 3 },
                                {0, 0, 0, 6, 0, 0, 0, 0 },
                                {0, 0, 0, 0, 0, 0, 0, 0 },
                                {0, 0, 2, 0, 0, 6, 0, 0 },
                                {0, 0, 0, 4, 0, 0, 0, 1 },
                                {0, 0, 1, 0, 2, 0, 0, 6 },
                                {0, 0, 0, 5, 0, 0, 0, 0 }},
                            new PointF[] {
                                new PointF(50, 225), new PointF(250, 75), new PointF(450, 75), new PointF(650, 225),
                                new PointF(180, 225), new PointF(500, 225), new PointF(250, 425), new PointF(450, 425)
                            }
                        ),
            new MaxFlowProblemExample("Пример 3", "Парк Кирова", 0, 6,
                            new[,] {
                                {0, 4, 3, 7, 0, 0, 0},
                                {0, 0, 0, 1, 3, 0, 0},
                                {0, 0, 0, 0, 0, 3, 0},
                                {0, 0, 0, 0, 4, 4, 0},
                                {0, 0, 0, 0, 0, 0, 8},
                                {0, 0, 0, 0, 1, 0, 6},
                                {0, 0, 0, 0, 0, 0, 0}},
                            new PointF[] {
                                new PointF(80, 235), new PointF(210, 105), new PointF(250, 385), new PointF(295, 235),
                                new PointF(475, 235), new PointF(425, 385), new PointF(615, 195)
                            }
                        ),
            new MaxFlowProblemExample("Пример 4", "Пример 4", 0, 8,
                            new[,] {
                                {0, 7, 9, 4, 0, 0, 0, 0, 0},
                                {0, 0, 0, 8, 3, 0, 0, 6, 0},
                                {0, 0, 0, 5, 0, 8, 4, 0, 0},
                                {0, 0, 0, 0, 0, 0, 9, 2, 0},
                                {0, 0, 0, 0, 0, 0, 0, 2, 0},
                                {0, 0, 0, 0, 0, 0, 5, 0, 3},
                                {0, 0, 0, 0, 0, 0, 0, 7, 6},
                                {0, 0, 0, 0, 0, 0, 0, 0, 8},
                                {0, 0, 0, 0, 0, 0, 0, 0, 0}},
                            new PointF[] {
                                new PointF(80, 265), new PointF(210, 135), new PointF(250, 415), new PointF(295, 265),
                                new PointF(355, 85), new PointF(415, 430), new PointF(450, 285), new PointF(475, 175), new PointF(615, 310)
                            }
                        ),
            new MaxFlowProblemExample("Пример 5", "Пример 5", 0, 9,
                            new[,] {
                                {0, 1, 0, 1, 0, 1, 0, 1, 0, 0},
                                {0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
                                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                {0, 0, 1, 0, 1, 0, 0, 0, 1, 0},
                                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                {0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                {0, 0, 0, 0, 0, 0, 1, 0, 1, 0},
                                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}},
                            new PointF[] {
                                new PointF(100, 250), new PointF(250, 50), new PointF(450, 50), new PointF(250, 175), new PointF(450, 175),
                                new PointF(250, 290), new PointF(450, 290), new PointF(250, 450), new PointF(450, 450), new PointF(600, 250)
                            }
                        )
        };

        public bool IsRandomExampleAvailable => true;
    }
}