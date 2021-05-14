namespace GOES.Problems {
    /// <summary>
    /// Интерфейс дескриптора задачи, позволяющий получить общую информацию о конкретной задаче 
    /// (не о примере задачи, а о самой задаче), такую как название, описание, список готовых примеров задачи, доступна ли
    /// случайная генерация задачи
    /// </summary>
    public interface IProblemDescriptor {
        /// <summary>
        /// Название задачи
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Описание задачи
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Список готовых примеров задачи
        /// </summary>
        ProblemExample[] ProblemExamples { get; }
        /// <summary>
        /// Флаг доступности случайной генерации примера задачи: 
        /// реализована ли в задаче логика случайной генерации примеров
        /// </summary>
        bool IsRandomExampleAvailable { get; }
    }
}
