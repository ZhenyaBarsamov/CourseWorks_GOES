using System.Drawing;

namespace SGVL.Visualizers {
    /// <summary>
    /// Класс, предоставляющий настройки для визуализатора SimpleGraphVisualizer
    /// *Для MsaglVisualizer работает с немедленной перерисовкой только BackgroundColor;
    /// BackgroundColor, VertexBorderColor, VertexFillColor, EdgeColor - при вызове методов ResetColor...
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
                BackgroundColorChanged?.Invoke(this);
                SettingsChanged?.Invoke(this);
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
                VertexBorderColorChanged?.Invoke(this);
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
            }
        }

        private Color vertexFillColor;
        /// <summary>
        /// Цвет заливки вершины
        /// </summary>
        public Color VertexFillColor {
            get => vertexFillColor; 
            set {
                vertexFillColor = value;
                VertexFillColorChanged?.Invoke(this);
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                EdgeColorChanged?.Invoke(this);
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
                SettingsChanged?.Invoke(this);
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
            vertexFillColor = Color.White;
            vertexRadius = 27; // 20 - неплохо
            edgeColor = Color.Black;
            edgeSelectingColor = Color.Blue;
            fontName = "Calibri";
            fontSize = 10f;
            isEdgeLabelVisible = true;
            edgeLabelColor = Color.Green;
            edgeLabelSelectingColor = Color.LightSeaGreen;
            vertexNumberColor = Color.Green;
            vertexNumberSelectingColor = Color.Red;
            isVertexLabelVisible = true;
            vertexLabelColor = Color.Blue;
            vertexLabelSelectingColor = Color.Green;
        }


        // ----События
        /// <summary>
        /// Событие, происходящее при изменении объекта DrawingSettings
        /// </summary>
        public event DrawingSettingsChangedEventHandler SettingsChanged;
        /// <summary>
        /// Событие, происходящее при изменении свойства BackgroundColorChanged
        /// </summary>
        public event DrawingSettingsChangedEventHandler BackgroundColorChanged;
        /// <summary>
        /// Событие, происходящее при изменении свойства VertexBorderColorChanged
        /// </summary>
        public event DrawingSettingsChangedEventHandler VertexBorderColorChanged;
        /// <summary>
        /// Событие, происходящее при изменении свойства VertexFillColorChanged
        /// </summary>
        public event DrawingSettingsChangedEventHandler VertexFillColorChanged;
        /// <summary>
        /// Событие, происходящее при изменении свойства EdgeColorChanged
        /// </summary>
        public event DrawingSettingsChangedEventHandler EdgeColorChanged;
        
    }
}
