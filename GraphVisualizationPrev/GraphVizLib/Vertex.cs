using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GraphVisualization.GraphVizLib {
    /// <summary>
    /// Класс вершины графа
    /// </summary>
    class Vertex {
        /// <summary>
        /// Идентификатор вершины
        /// </summary>
        public int ID { get; private set; }


        private PointF leftUpperPointPosition;
        /// <summary>
        /// Координаты верхнего угла вершины при отображении.
        /// К координатам вершины также прикреплены координаты надписей.
        /// </summary>
        public PointF LeftUpperPointPosition {
            get => leftUpperPointPosition;
            private set {
                leftUpperPointPosition = value;
                InsideLabelPosition = new PointF(value.X + radius / 4, value.Y + radius / 4);
                OutsideLabelPosition = new PointF(value.X, value.Y + 2 * radius);
                CenterPos = new PointF(value.X + radius, value.Y + radius);
            }
        }

        /// <summary>
        /// Координаты центра вершины
        /// </summary>
        public PointF CenterPos { get; private set; }


        private float radius;
        /// <summary>
        /// Радиус вершины.
        /// Радиус вершины также влияет на расположение меток вершины
        /// </summary>
        public float Radius {
            get => radius; 
            set {
                radius = value;
                InsideLabelPosition = new PointF(leftUpperPointPosition.X + value / 4, leftUpperPointPosition.Y + value / 4);
                OutsideLabelPosition = new PointF(leftUpperPointPosition.X, leftUpperPointPosition.Y + 2 * value);
                CenterPos = new PointF(leftUpperPointPosition.X + value, leftUpperPointPosition.Y + value);
            }
        }


        /// <summary>
        /// Цвет вершины
        /// </summary>
        public Color Color { get; set; }



        /// <summary>
        /// Метка вершины, отображающаяся внутри неё
        /// </summary>
        public Label InsideLabel { get; set; }

        /// <summary>
        /// Позиция внутренней метки
        /// </summary>
        public PointF InsideLabelPosition { get; private set; }


        /// <summary>
        /// Метка вершины, отображаемая снаружи
        /// </summary>
        public Label OutsideLabel { get; set; }

        /// <summary>
        /// Позиция внешней метки
        /// </summary>
        public PointF OutsideLabelPosition { get; private set; }


        

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="insideLabel"></param>
        /// <param name="outsideLabel"></param>
        /// <param name="color"></param>
        /// <param name="leftUpperPointPosition"></param>
        /// <param name="radius"></param>
        public Vertex(int iD, string insideLabelText, string outsideLabelText, Color color, PointF leftUpperPointPosition, int radius) {
            ID = iD;
            Color = color;
            this.leftUpperPointPosition = leftUpperPointPosition;
            this.radius = radius;
            InsideLabel = new Label(insideLabelText);
            InsideLabelPosition = new PointF(LeftUpperPointPosition.X + Radius / 4, LeftUpperPointPosition.Y + Radius / 4);
            OutsideLabel = new Label(outsideLabelText);
            OutsideLabelPosition = new PointF(LeftUpperPointPosition.X, LeftUpperPointPosition.Y + 2 * Radius);
            CenterPos = new PointF(leftUpperPointPosition.X + radius, leftUpperPointPosition.Y + radius);
        }



        /// <summary>
        /// Метод отрисовки вершины
        /// </summary>
        /// <param name="canvas"></param>
        public void Draw(Graphics canvas) {
            canvas.FillEllipse(new SolidBrush(Color.White), LeftUpperPointPosition.X, LeftUpperPointPosition.Y, Radius * 2, Radius * 2);
            canvas.DrawEllipse(new Pen(Color), LeftUpperPointPosition.X, LeftUpperPointPosition.Y, Radius * 2, Radius * 2);
            canvas.DrawString(InsideLabel.Text, new Font(InsideLabel.FontFamilyName, InsideLabel.FontSize), new SolidBrush(InsideLabel.Color), InsideLabelPosition.X, InsideLabelPosition.Y);
            canvas.DrawString(OutsideLabel.Text, new Font(OutsideLabel.FontFamilyName, OutsideLabel.FontSize), new SolidBrush(OutsideLabel.Color), OutsideLabelPosition.X, OutsideLabelPosition.Y);
        }
    }
}
