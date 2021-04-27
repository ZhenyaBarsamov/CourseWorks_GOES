using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOES.Problems.MaximalBipartiteMatching {
    /// <summary>
    /// Класс, предназначенный для хранения постановки задачи о максимальном паросочетании в двудольном графе
    /// </summary>
    public class MaximalBipartiteMatchingStatement : ProblemStatement {
        // ----Атрибуты
        /// <summary>
        /// Матрица смежности графа
        /// </summary>
        public int[,] GraphMatrix { get; private set; }


        // ----Конструктор
        /// <summary>
        /// Конструктор, создающий экземпляр постановки задачи о максимальном потоке и минимальном разрезе
        /// </summary>
        /// <param name="name">Название постановки задачи</param>
        /// <param name="description">Описание постановки задачи</param>
        /// <param name="graphMatrix">Матрица смежности графа</param>
        /// <param name="defaultGraphLayout">Массив точек, задающих расположение вершин графа по умолчанию</param>
        public MaximalBipartiteMatchingStatement(string name, string description, int[,] graphMatrix,
            PointF[] defaultGraphLayout) : base(name, description, false, defaultGraphLayout) {
            GraphMatrix = graphMatrix;
        }
    }
}
