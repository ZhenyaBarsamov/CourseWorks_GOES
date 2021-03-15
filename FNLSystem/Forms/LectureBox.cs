using System.IO;
using System.Windows.Forms;

namespace FNLSystem.Forms {
    /// <summary>
    /// Форма, предоставляющая текст лекции (или любого другого html-документа из папки с приложением)
    /// </summary>
    public partial class LectureBox : Form {
        public LectureBox() {
            InitializeComponent();
        }

        public void ShowLecture(string docName = "Теория.html", int docPage = 0) {
            // Получаем путь до директории приложения
            string directoryPath = Directory.GetCurrentDirectory();
            // Формируем абсолютный путь до лекции
            string docPath = Path.Combine(directoryPath, docName);
            // Открываем окно
            Show();
            // Проверяем, существует ли этот файл. Если нет - говорим об этом и просим вернуть его
            if (!File.Exists(docPath))
                MessageBox.Show(
                    "В каталоге приложения файл с лекцией отсутствует. Он должен называться \"Теория.html\". " +
                    "Пожалуйста, верните файл, содержащий лекцию, в каталог приложения и повторите попытку снова.",
                    "Лекция отсутствует", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            // Если нужно открыть на конкретной странице, добавляем это в адрес: 
            // <адрес>#pf<номер страницы в 16-ричной системе счисления>
            if (docPage > 0)
                docPath += $"#pf{docPage:x}";
            // По абсолютному пути открываем файл в веб-браузере
            wbLecture.Navigate(docPath);
        }
    }
}
