using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace SGVL.Graphs {
    /// <summary>
    /// Класс, представляющий точку рисования 
    /// в относительных координатах, измеряющихся в диапазоне от 0 до 1
    /// </summary>
    class RelativePointCoordinates {
        // ----Свойства
        private float x;
        /// <summary>
        /// Координата по оси абсцисс в диапазоне от 0 до 1
        /// </summary>
        public float X {
            get => x; 
            set {
                if (value < 0 | value > 1)
                    throw new ArgumentException("Неправильное значение.");
                x = value;
            }
        }

        public float y;
        /// <summary>
        /// Координата по оси ординат в диапазоне от 0 до 1
        /// </summary>
        public float Y {
            get => y;
            set {
                if (value < 0 | value > 1)
                    throw new ArgumentException("Неправильное значение.");
                y = value;
            }
        }

        // ----Конструкторы
        /// <summary>
        /// Конструктор, создающий экземпляр точки по готовым относительным координатам
        /// </summary>
        /// <param name="x">Координата X в диапазоне от 0 до 1</param>
        /// <param name="y">Координата Y в диапазоне от 0 до 1</param>
        public RelativePointCoordinates(float x, float y) {
            X = x;
            Y = y;
        }

        // ----Методы
        /// <summary>
        /// Построить относительные координаты для заданных координат в пикселях и
        /// заданных параметров области отображения
        /// </summary>
        /// <param name="point">Координаты точки в пикселях</param>
        /// <param name="width">Ширина области отображения</param>
        /// <param name="height">Высота области отображения</param>
        public RelativePointCoordinates(PointF point, float width, float height) {
            X = point.X / width;
            Y = point.Y / height;
        }

        /// <summary>
        /// Получить координаты в пикселях из данных относительных координат для 
        /// заданных параметров области отображения
        /// </summary>
        /// <param name="width">Ширина области отображения</param>
        /// <param name="height">Высота области отображения</param>
        /// <returns>Координаты точки в пикселях</returns>
        public PointF GetAbsoluteCoordinates (float width, float height) {
            return new PointF(X * width, Y * height);
        }
    }
}
