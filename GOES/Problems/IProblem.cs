namespace GOES.Problems {
    /// <summary>
    /// Интерфейс задачи
    /// </summary>
    public interface IProblem {
        /// <summary>
        /// Дескриптор задачи, предоставляющий общую информацию о ней
        /// </summary>
        IProblemDescriptor ProblemDescriptor { get; }
        /// <summary>
        /// Инициализировать задачу, т.е. подготовить её к решению (демонстрации)
        /// </summary>
        /// <param name="example">Пример задачи, которым будет проинициализирована задача, 
        /// или null, если необходимо сгенерировать случайный пример</param>
        /// <param name="mode">Режим, в котором будет работать задача</param>
        void InitializeProblem(ProblemExample example, ProblemMode mode);
    }
}
