using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GraphVisualization.GraphVizLib {
    // Класс ребра графа
    class Edge {
        /// <summary>
        /// Идентификатор дуги
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Идентификатор вершины-начала ребра
        /// </summary>
        public int BeginVertexID { get; private set; }

        /// <summary>
        /// Идентификатор вершины-конца ребра
        /// </summary>
        public int EndVertexID { get; private set; }

        /// <summary>
        /// Метка дуги
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// Цвет дуги
        /// </summary>
        public Color Color { get; set; }

        public Edge(int iD, string label, Color color) {
            ID = iD;
            Label = label;
            Color = color;
        }
    }
}
