using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GOES.Controls {
    /// <summary>
    /// Расширение стандартного элемента DataGridView для удобного отображения на форме матриц.
    /// Содердит также настройки по умолчанию, такие как: отключение сортировки по столбцам, 
    /// отключение перемещения столбцов, отключение добавления/удаления строк, 
    /// настройки размера столбцов и выравнивания в них текста
    /// </summary>
    class MatrixDataGridView : DataGridView {
        // Переопределение метода Sort(dataGridViewColumn, direction) на пустой полностью отключает сортировку
        // по столбцам в элементе управления
        public override void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction) { }

        // Метод, который используется для нейтрализации выделения ячеек
        private void OnSelectionChanged(object sender, EventArgs e) {
            ClearSelection();
        }

        private bool isCellsSelectable;
        /// <summary>
        /// Флаг, указывающий, работает ли выделение ячеек
        /// </summary>
        public bool IsCellsSelectable {
            get {
                return isCellsSelectable;
            }
            set {
                isCellsSelectable = value;
                if (!value)
                    SelectionChanged += OnSelectionChanged;
                else
                    SelectionChanged -= OnSelectionChanged;
            }
        }

        public MatrixDataGridView() : base() {
            // Запрещаем пользователю действия по изменению матрицы
            AllowUserToAddRows =
                AllowUserToDeleteRows =
                AllowUserToOrderColumns = false;
            // Запрещаем пользователю действия по изменению размера матрицы
            AllowUserToResizeRows =
                AllowUserToResizeColumns = false;
            // Устанавливаем выравнивание заголовков строк/столбцов по умолчанию
            RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Устанавливаем выравнивание данных в ячейках по умолчанию
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Устанавливаем авторазмер матрицы по данным
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // Для того, чтобы можно было использовать стили для ячеек-заголовков, выключаем использование для них стилей по умолчанию
            EnableHeadersVisualStyles = false;
            //RowHeadersDefaultCellStyle.BackColor = ColumnHeadersDefaultCellStyle.BackColor = Color.Empty;
        }

        /// <summary>
        /// Отобразить в элементе управления заданную целочисленную матрицу, использовав заданные
        /// заголовки для строк и столбцов
        /// </summary>
        /// <param name="matrix">Матрица, которую необходимо отобразить</param>
        /// <param name="rowsHeaders">Заголовки для строк</param>
        /// <param name="columnsHeaders">Заголовки для столбцов</param>
        public void FillMatrix(int[,] matrix, string[] rowsHeaders, string[] columnsHeaders) {
            SuspendLayout();
            int rowsCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);
            ColumnCount = columnsCount;
            RowCount = rowsCount;
            int i = 0;
            foreach (DataGridViewColumn col in Columns) {
                // Задаём настройки столбца
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                // Задаём заголовок столбца
                col.HeaderCell.Value = columnsHeaders[i++];
            }
            i = 0;
            foreach (DataGridViewRow row in Rows) {
                // Задаём настройки строки
                row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Задаём заголовок строки
                row.HeaderCell.Value = rowsHeaders[i++];
            }
            // Заполняем матрицу
            for (int row = 0; row < rowsCount; row++)
                for (int col = 0; col < columnsCount; col++)
                    this[col, row].Value = matrix[row, col].ToString();
            // Если у нас остаётся пустое место для того, чтобы растянуть столбцы - растягиваем их поровну
            //if (Columns.GetColumnsWidth(DataGridViewElementStates.None) <= ClientSize.Width) // <= Width - RowHeadersWidth
            //    foreach (DataGridViewColumn col in Columns)
            //        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ResumeLayout();
        }

        /// <summary>
        /// Отобразить в элементе управления заданную целочисленную матрицу, 
        /// использовав нумерацию в качестве заголовков для строк и столбцов
        /// </summary>
        /// <param name="matrix">Матрица, которую необходимо отобразить</param>
        public void FillMatrix(int[,] matrix) {
            int rowsCount = matrix.GetLength(0);
            string[] rowsHeaders = new string[rowsCount];
            for (int i = 0; i < rowsCount; i++)
                rowsHeaders[i] = (i + 1).ToString();
            int columnsCount = matrix.GetLength(1);
            string[] columnsHeaders = new string[columnsCount];
            for (int i = 0; i < columnsCount; i++)
                columnsHeaders[i] = (i + 1).ToString();
            FillMatrix(matrix, rowsHeaders, columnsHeaders);
        }

        /// <summary>
        /// Раскрасить фон заголовка заданной строки в заданный цвет
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="color">Цвет</param>
        public void SetRowHeaderColor(int rowIndex, Color color) {
            Rows[rowIndex].HeaderCell.Style.BackColor = color;
        }

        /// <summary>
        /// Раскрасить фон заголовка заданного столбца в заданный цвет
        /// </summary>
        /// <param name="colIndex">Индекс столбца</param>
        /// <param name="color">Цвет</param>
        public void SetColumnHeaderColor(int colIndex, Color color) {
            Columns[colIndex].HeaderCell.Style.BackColor = color;
        }

        /// <summary>
        /// Раскрасить фон заданной ячейки в заданный цвет
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="colIndex">Индекс столбца</param>
        /// <param name="color">Цвет</param>
        public void SetCellColor(int rowIndex, int colIndex, Color color) {
            this[colIndex, rowIndex].Style.BackColor = color;
        }

        /// <summary>
        /// Задать цвет шрифта для заданной ячейки
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="colIndex">Индекс столбца</param>
        /// <param name="color">Цвет</param>
        public void SetCellFontColor(int rowIndex, int colIndex, Color color) {
            this[colIndex, rowIndex].Style.ForeColor = color;
        }

        /// <summary>
        /// Сбросить цвет шрифта для заданной ячейки
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="colIndex">Индекс столбца</param>
        public void SetCellFontColorToDefault(int rowIndex, int colIndex) {
            this[colIndex, rowIndex].Style.ForeColor = Color.Empty;
        }

        /// <summary>
        /// Сбросить цвет фона заголовка заданной строки
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        public void SetRowHeaderColorToDefault(int rowIndex) {
            Rows[rowIndex].HeaderCell.Style.BackColor = Color.Empty;
        }

        /// <summary>
        /// Сбросить цвет фона заголовка заданного столбца
        /// </summary>
        /// <param name="colIndex">Индекс столбца</param>
        public void SetColumnHeaderColorToDefault(int colIndex) {
            Columns[colIndex].HeaderCell.Style.BackColor = Color.Empty; ;
        }

        /// <summary>
        /// Сбросить цвет фона заданной ячейки таблицы
        /// </summary>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="colIndex">Индекс столбца</param>
        public void SetCellColorToDefault(int rowIndex, int colIndex) {
            this[colIndex, rowIndex].Style.BackColor = Color.Empty;
        }

        /// <summary>
        /// Сбросить цвет всех ячеек таблицы, включая ячейки заголовков
        /// </summary>
        public void SetCellsColorsToDefault() {
            SuspendLayout();
            foreach (DataGridViewRow row in Rows) {
                row.HeaderCell.Style.BackColor = Color.Empty;
                foreach (DataGridViewCell cell in row.Cells)
                    cell.Style.BackColor = Color.Empty;
            }
            foreach (DataGridViewColumn col in Columns)
                col.HeaderCell.Style.BackColor = Color.Empty;
            ResumeLayout();
        }

        /// <summary>
        /// Сбросить цвет шрифта всех ячеек таблицы, включая ячейки заголовков
        /// </summary>
        public void SetCellsFontColorToDefault() {
            SuspendLayout();
            foreach (DataGridViewRow row in Rows) {
                row.HeaderCell.Style.ForeColor = Color.Empty;
                foreach (DataGridViewCell cell in row.Cells)
                    cell.Style.ForeColor = Color.Empty;
            }
            foreach (DataGridViewColumn col in Columns)
                col.HeaderCell.Style.ForeColor = Color.Empty;
            ResumeLayout();
        }
    }
}
