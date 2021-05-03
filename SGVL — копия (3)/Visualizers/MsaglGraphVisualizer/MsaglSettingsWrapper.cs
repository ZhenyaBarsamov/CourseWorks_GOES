using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using MsaglGraphs = Microsoft.Msagl.Drawing;
using SgvlGraphs = SGVL.Graphs;
using MsaglGViewer = Microsoft.Msagl.GraphViewerGdi;

namespace SGVL.Visualizers.MsaglGraphVisualizer {
    /// <summary>
    /// Класс-обёртка, связывающий настроечный объект класса SGVL.DrawingSettings
    /// с настройками визуализации MSAGL (с настройками графа MSAGL.Graph и
    /// с настройками визуализатора MSAGL.GViewer)
    /// </summary>
    class MsaglSettingsWrapper {
        // ----Свойства
        /// <summary>
        /// Граф MSAGL
        /// </summary>
        public MsaglGraphs.Graph MsaglGraph { get; private set; }
        public SgvlGraphs.Graph SgvlGraph { get; private set; }
        /// <summary>
        /// MSAGL-визуализатор
        /// </summary>
        public MsaglGViewer.GViewer GViewer { get; private set; }
        public DrawingSettings DrawingSettings { get; private set; }


        // ----Конструктор
        public MsaglSettingsWrapper(GViewer gViewer, MsaglGraphs.Graph msaglGraph, SgvlGraphs.Graph sgvlGraph, DrawingSettings drawingSettings) {
            MsaglGraph = msaglGraph;
            GViewer = gViewer;
            DrawingSettings = drawingSettings;
            // Подписываемся на событие изменения цвета фона
            drawingSettings.BackgroundColorChanged += UpdateBackgroundColor;
        }


        // ----Методы
        private void UpdateBackgroundColor(DrawingSettings drawingSettings) {
            MsaglGraph.Attr.BackgroundColor = new Color { 
                R = drawingSettings.BackgroundColor.R,
                G = drawingSettings.BackgroundColor.G,
                B = drawingSettings.BackgroundColor.B,
                A = drawingSettings.BackgroundColor.A
            };
        }
    }
}
