using System.Drawing;

namespace SGVL.Visualizers.SimpleGraphVisualizer {
    /// <summary>
    /// Класс, предоставляющий настройки для визуализатора SimpleGraphVisualizer 
    /// </summary>
    public class DrawingSettings {
        // ----Атрибуты
        private Color backgroundColor;
        /// <summary>
        /// Цвет фона холста рисования
        /// </summary>
        public Color BackgroundColor {
            get => backgroundColor;
            set {
                backgroundColor = value;
                SettingsChanged?.Invoke();
            } 
        }

        private Color vertexBorderColor;
        /// <summary>
        /// Цвет границы вершины
        /// </summary>
        public Color VertexBorderColor { 
            get =>vertexBorderColor;
            set {
                vertexBorderColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color vertexSelectingColor;
        /// <summary>
        /// Цвет подсвечивания вершины при наведении на неё мыши
        /// </summary>
        public Color VertexSelectingColor {
            get => vertexSelectingColor; 
            set {
                vertexSelectingColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color vertexFillingColor;
        /// <summary>
        /// Цвет заливки вершины
        /// </summary>
        public Color VertexFillingColor {
            get => vertexFillingColor; 
            set {
                vertexFillingColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private float vertexRadius;
        /// <summary>
        /// Радиус вершины
        /// </summary>
        public float VertexRadius { 
            get => vertexRadius; 
            set {
                vertexRadius = value;
                SettingsChanged?.Invoke();
            } 
        }

        private Color edgeColor;
        /// <summary>
        /// Цвет ребра
        /// </summary>
        public Color EdgeColor { 
            get => edgeColor;
            set {
                edgeColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color edgeSelectingColor;
        /// <summary>
        /// Цвет подсвечивания ребра при наведении на него мыши
        /// </summary>
        public Color EdgeSelectingColor { 
            get => edgeSelectingColor;
            set {
                edgeSelectingColor = value;
                SettingsChanged?.Invoke();
            } 
        }

        private string fontName;
        /// <summary>
        /// Название шрифта меток
        /// </summary>
        public string FontName {
            get => fontName;
            set {
                fontName = value;
                SettingsChanged?.Invoke();
            }
        }

        private float fontSize;
        /// <summary>
        /// Размер шрифта меток
        /// </summary>
        public float FontSize  { 
            get => fontSize;
            set {
                fontSize = value;
                SettingsChanged?.Invoke();
            } 
        }

        private bool isEdgeLabelVisible;
        /// <summary>
        /// Признак включения отображения меток рёбер
        /// </summary>
        public bool IsEdgeLabelVisible {
            get => isEdgeLabelVisible;
            set {
                isEdgeLabelVisible = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color edgeLabelColor;
        /// <summary>
        /// Цвет метки ребра
        /// </summary>
        public Color EdgeLabelColor { 
            get => edgeLabelColor;
            set {
                edgeLabelColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color edgeLabelSelectingColor;
        /// <summary>
        /// Цвет метки ребра при наведении на ребро мыши
        /// </summary>
        public Color EdgeLabelSelectingColor { 
            get => edgeLabelSelectingColor; 
            set {
                edgeLabelSelectingColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color vertexNumberColor;
        /// <summary>
        /// Цвет отображения номера вершины
        /// </summary>
        public Color VertexNumberColor {
            get => vertexNumberColor;
            set {
                vertexNumberColor = value;
                SettingsChanged?.Invoke();
            } 
        }

        private Color vertexNumberSelectingColor;
        /// <summary>
        /// Цвет отображения номера вершины при наведении на вершину мыши
        /// </summary>
        public Color VertexNumberSelectingColor { 
            get => vertexNumberSelectingColor;
            set {
                vertexNumberSelectingColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private bool isVertexLabelVisible;
        /// <summary>
        /// Признак включения отображения меток вершин
        /// </summary>
        public bool IsVertexLabelVisible {
            get => isVertexLabelVisible;
            set {
                isVertexLabelVisible = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color vertexLabelColor;
        /// <summary>
        /// Цвет метки вершины
        /// </summary>
        public Color VertexLabelColor {
            get => vertexLabelColor;
            set {
                vertexLabelColor = value;
                SettingsChanged?.Invoke();
            }
        }

        private Color vertexLabelSelectingColor;
        /// <summary>
        /// Цвет метки вершины при наведении на вершину мыши
        /// </summary>
        public Color VertexLabelSelectingColor {
            get => vertexLabelSelectingColor; 
            set {
                vertexLabelSelectingColor = value;
                SettingsChanged?.Invoke();
            }
        }


        // ----Конструктор
        /// <summary>
        /// Конструктор, инициализирующий объект настроек с настройками по умолчанию
        /// </summary>
        public DrawingSettings() {
            backgroundColor = SystemColors.Control;
            vertexBorderColor = Color.Black;
            vertexSelectingColor = Color.Red;
            vertexFillingColor = Color.White;
            vertexRadius = 27; // 20 - неплохо
            edgeColor = Color.Black;
            edgeSelectingColor = Color.Blue;
            fontName = "Calibri";
            fontSize = 10f;
            isEdgeLabelVisible = true;
            edgeLabelColor = Color.Green;
            edgeLabelSelectingColor = Color.DarkGreen;
            vertexNumberColor = Color.Green;
            vertexNumberSelectingColor = Color.DarkGreen;
            isVertexLabelVisible = true;
            vertexLabelColor = Color.Blue;
            vertexLabelSelectingColor = Color.DarkBlue;
        }


        // ----События
        public event DrawingSettingsChangedEventHandler SettingsChanged;
    }
}
