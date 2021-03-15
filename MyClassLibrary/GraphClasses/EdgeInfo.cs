using System;

namespace MyClassLibrary.GraphClasses {
    /// <summary>
    /// Класс, содержащий информацию о дуге графа EduGraph
    /// </summary>
    public class EdgeInfo {
        /// <summary>
        /// Номер начальной вершины
        /// </summary>
        public int StartVertexID { get; private set; }

        /// <summary>
        /// Номер конечной вершины
        /// </summary>
        public int FinishVertexID { get; private set; }

        /// <summary>
        /// Текущая величина потока для данной дуги
        /// </summary>
        private int flow;
        /// <summary>
        /// Текущая величина потока для данной дуги
        /// </summary>
        public int Flow {
            get => flow;
            set {
                if (value < 0 || value > Capacity)
                    throw new Exception("Было присвоено неверное значение потока!");
                else
                    flow = value;
            }
        }

        /// <summary>
        /// Величина пропускной способности данной дуги
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Метка дуги, содержащая текущий поток и общую пропускную способность
        /// </summary>
        public string Label => $"{Flow}/{Capacity}";

        /// <summary>
        /// Возвращает метку вершины "поток/вместимость" (для отладки)
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Label;
        }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="flow">Величина начального потока</param>
        /// <param name="capacity">Величина пропускной способности</param>
        public EdgeInfo(int startVertexID, int finishVertexID, int flow, int capacity) {
            StartVertexID = startVertexID;
            FinishVertexID = finishVertexID;
            Flow = flow;
            Capacity = capacity;
        }

        /// <summary>
        /// Конструктор, задающий пропускную способность и обнуляющий поток
        /// </summary>
        /// <param name="capacity"Величина пропускной способности</param>
        public EdgeInfo(int startVertexID, int finishVertexID, int capacity) {
            StartVertexID = startVertexID;
            FinishVertexID = finishVertexID;
            Capacity = capacity;
            Flow = 0;
        }


        /// <summary>
        /// Получить дополнительный поток, который можно
        /// пустить по данному ребру
        /// </summary>
        /// <returns></returns>
        public int GetAugmentalFlow() {
            return Capacity - Flow;
        }
    }
}
