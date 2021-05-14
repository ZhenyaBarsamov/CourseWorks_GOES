using System.Drawing;

namespace GOES.Problems {
    /// <summary>
    /// Класс, предназначенный для хранения примера конкретной оптимизационной задачи на графе
    /// (например, задача о максимальном потоке для заданной конкретной сети)
    /// </summary>
    public abstract class ProblemExample {
        // ----Атрибуты
        /// <summary>
        /// Название примера (название, номер, часть описания, и т.д.)
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Описание примера (о чём он, особенности, и т.д.)
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Признак использования в данной примере (задаче) ориентированного графа
        /// </summary>
        public bool IsGraphDirected { get; private set; }
        /// <summary>
        /// Массив точек, задающих расположение вершин графа по умолчанию
        /// </summary>
        public PointF[] DefaultGraphLayout { get; private set; }


        // ----Конструктор
        /// <summary>
        /// Создаёт объект постановки некоторой задачи
        /// </summary>
        /// <param name="name">Название постановки задачи</param>
        /// <param name="description">Описание постановки задачи</param>
        /// <param name="isGraphDirected">Признак ориентированности графа</param>
        /// <param name="defaultGraphLayout">Массив точек, задающих расположение вершин графа по умолчанию</param>
        public ProblemExample(string name, string description, bool isGraphDirected, PointF[] defaultGraphLayout) {
            Name = name;
            Description = description;
            IsGraphDirected = isGraphDirected;
            DefaultGraphLayout = defaultGraphLayout;
        }
    }
}
