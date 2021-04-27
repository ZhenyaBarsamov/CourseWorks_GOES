using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOES.Forms {
    /// <summary>
    /// Форма, использующаяся для отображения лекций, представленных в формате HTML
    /// </summary>
    public partial class FormLectureViewer : Form {
        // ----Конструкторы
        public FormLectureViewer() {
            InitializeComponent();
        }

        /// <summary>
        /// Создать объект просмотрщика лекции.
        /// Для того, чтобы открыть окно с лекцией, используйте методы формы Show() / ShowDialog()
        /// </summary>
        /// <param name="docPath">Путь до html-документа, содержащего лекцию</param>
        /// <param name="docPage">Страница, на которой нужно открыть документ (по умолчанию первая)</param>
        public FormLectureViewer(string docPath, int docPage = 0) {
            InitializeComponent();
            // Нам нужен абсолютный путь до файла с лекцией. Формируем его, если нам дали относительный
            docPath = Path.GetFullPath(docPath);
            // Проверяем, существует ли этот файл. Если нет - говорим об этом и просим вернуть его
            if (!File.Exists(docPath))
                throw new ArgumentException(
                    "В каталоге приложения файл с лекцией отсутствует. Он должен называться \"Теория.html\". " +
                    "Пожалуйста, верните файл, содержащий лекцию, в каталог приложения и повторите попытку снова.",
                    nameof(docPath));
            // Если нужно открыть на конкретной странице, добавляем это в адрес: 
            // <адрес>#pf<номер страницы в 16-ричной системе счисления>
            if (docPage > 0)
                docPath += $"#pf{docPage:x}";
            // По абсолютному пути открываем файл в веб-браузере
            webBrowserLecture.Navigate(docPath);
        }
    }
}
