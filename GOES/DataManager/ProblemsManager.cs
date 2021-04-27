using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOES.Problems;
using GOES.Problems.MaximalFlow;
using GOES.Problems.MaximalBipartiteMatching;

namespace GOES.DataManager {
    /// <summary>
    /// Класс, который организовывает работу с доступными задачами и примерами их постановок
    /// </summary>
    public class ProblemsManager {
        /// <summary>
        /// Получить название оптимизационной задачи по её коду (значению перечислителя)
        /// </summary>
        /// <param name="problem">Значение перечислителя, соответствующее нужной задаче</param>
        public string GetProblemName(Problem problem) {
            switch (problem) {
                case Problem.MaximalFlow:
                    return "Задача о максимальном потоке и минимальном разрезе в сети";
                default:
                    return "Неизвестная задача";
            }
        }

        /// <summary>
        /// Получить список кодов задач, доступных для решения
        /// </summary>
        public Problem[] GetAvailableProblemsList() {
            /* Если задачи и примеры их постановок будут храниться в файлах, то у файлов будет
             * определённое имя. Все имена файлов будут известны менеджеру.
             * Он будет проходить по файлам и смотреть, файлы каких задач существуют. В итоге он будет
             * выдавать не список всех задач, о которых что-то сказано в коде системы, а список тех, которые
             * действительно доступны для решения
             */
            return new Problem[] {
                Problem.MaximalFlow
            };
        }

        public ProblemStatement[] GetAvailableProblemStatements(Problem problem) {
            switch (problem) {
                case Problem.MaximalFlow:
                    return new ProblemStatement[] {
                        new MaximalFlowProblemStatement("Задача 1", "", 0, 6,  
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
                        new MaximalFlowProblemStatement("Задача 2", "", 0, 3, 
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
                        new MaximalFlowProblemStatement("Задача 3", "Парк Кирова", 0, 6, 
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
                        new MaximalFlowProblemStatement("Задача 4", "", 0, 8, 
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
                        new MaximalFlowProblemStatement("Задача 5", "", 0, 9, 
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
                case Problem.MaximalBipartiteMatching:
                    // графы двудольные, идёт чередование: вершина доли 1, вершина доли 2, вершина доли 1, ...
                    return new ProblemStatement[] {
                        new MaximalBipartiteMatchingStatement("Задача 1", "", 
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
                default:
                    return null;
            }
        }
    }
}
