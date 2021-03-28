using System.Drawing;

namespace SGVL.Visualization.AbstractTypes.Visualizer {
    /// <summary>
    /// Класс, содержащий настройки визуализации: стандартные цвета, шрифт и т.д. 
    /// </summary>
    public class VisualizationSettings {
        /// <summary>
        /// Цвет фона рисования
        /// </summary>
        public Color BackgroundColor { get; set; }
        /// <summary>
        /// Цвет границы вершины
        /// </summary>
        public Color VertexBorderColor { get; set; }
        /// <summary>
        /// Цвет подсвечивания вершины при выборе
        /// </summary>
        public Color VertexSelectingColor { get; set; }
        /// <summary>
        /// Цвет заливки вершины
        /// </summary>
        public Color VertexFillColor { get; set; }
        /// <summary>
        /// Радиус вершины
        /// </summary>
        public float VertexRadius { get; set; }
        /// <summary>
        /// Цвет дуги
        /// </summary>
        public Color EdgeColor { get; set; }
        /// <summary>
        /// Цвет подсвечивания дуги при выборе
        /// </summary>
        public Color EdgeSelectingColor { get; set; }
        /// <summary>
        /// Название шрифта меток
        /// </summary>
        public string FontName { get; set; }
        /// <summary>
        /// Размер шрифта меток
        /// </summary>
        public float FontSize { get; set; }
        /// <summary>
        /// Цвет меток дуги
        /// </summary>
        public Color EdgeLabelColor { get; set; }
        /// <summary>
        /// Цвет номерной метки вершины
        /// </summary>
        public Color VertexNumberLabelColor { get; set; }
        /// <summary>
        /// Цвет метки, привязанной к вершине
        /// </summary>
        public Color OutsideVertexLabelColor { get; set; }

        /// <summary>
        /// Конструктор, задающий стандартные параметры рисования
        /// </summary>
        public VisualizationSettings() {
            BackgroundColor = SystemColors.Control;
            VertexBorderColor = Color.Black;
            VertexSelectingColor = Color.Red;
            VertexFillColor = Color.White;
            VertexRadius = 27; // 20 - неплохо
            EdgeColor = Color.Black;
            EdgeSelectingColor = Color.Blue;
            FontName = "Calibri";
            FontSize = 10f;
            EdgeLabelColor = Color.Green;
            VertexNumberLabelColor = Color.Green;
            OutsideVertexLabelColor = Color.Blue;
        }
    }
}
