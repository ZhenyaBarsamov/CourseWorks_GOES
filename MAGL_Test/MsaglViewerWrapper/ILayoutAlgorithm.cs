using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAGL_Test.GraphWrapper;

namespace MAGL_Test.MsaglViewerWrapper {
    /// <summary>
    /// Интерфейс, представляющий алгоритм укладки графа
    /// </summary>
    public interface ILayoutAlgorithm {
        /// <summary>
        /// Построить укладку для заданного графа
        /// </summary>
        /// <param name="graph">Объект графа</param>
        void BuildGraphLayout(MsaglGraphWrapper graph);
    }
}
