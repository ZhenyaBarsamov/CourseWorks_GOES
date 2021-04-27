using System.Drawing;

namespace GOES.Problems {
    /// <summary>
    /// Класс, предназначенный для хранения постановки некоторой конкретной оптимизационной задачи на графе
    /// </summary>
    public abstract class ProblemStatement {
        // ----Атрибуты
        /// <summary>
        /// Название постановки (идентификатор, номер, или часть описания)
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Описание постановки (о чём она, особенности, и т.д.)
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Признак ориентированности графа
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
        public ProblemStatement(string name, string description, bool isGraphDirected, PointF[] defaultGraphLayout) {
            Name = name;
            Description = description;
            IsGraphDirected = isGraphDirected;
            DefaultGraphLayout = defaultGraphLayout;
        }
    }
}
