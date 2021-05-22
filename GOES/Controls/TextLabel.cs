using System.Windows.Forms;

namespace GOES.Controls {
    /// <summary>
    /// Класс, представляющий собой изменённый стандартный элемент управления TextBox,
    /// работающий как Label: без выделения текста, возможности ввода и т.д., и при этом
    /// дающий использовать встренные панели прокрутки ScroolBars
    /// </summary>
    public class TextLabel : TextBox {
        // See: http://wiki.winehq.org/List_Of_Windows_Messages

        private const int WM_SETFOCUS = 0x07;
        private const int WM_ENABLE = 0x0A;
        private const int WM_SETCURSOR = 0x20;

        protected override void WndProc(ref System.Windows.Forms.Message m) {
            if (!(m.Msg == WM_SETFOCUS || m.Msg == WM_ENABLE || m.Msg == WM_SETCURSOR))
                base.WndProc(ref m);
        }
    }
}
