using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GraphVisualization.GraphVizLib {
    /// <summary>
    /// Класс надписи
    /// </summary>
    class Label {
        /// <summary>
        /// Текст надписи
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Цвет надписи
        /// </summary>
        public Color Color { get; set; } 

        /// <summary>
        /// Название шрифта
        /// </summary>
        public string FontFamilyName { get; private set; }

        /// <summary>
        /// Стиль шрифта (не используем)
        /// </summary>
        public FontStyle FontStyle { get; private set; }

        /// <summary>
        /// Размер шрифта
        /// </summary>
        public float FontSize { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="text"></param>
        public Label(string text) {
            Text = text;
            Color = Color.Black;
            FontFamilyName = "Calibri";
            FontSize = 10f;
        }
    }
}
