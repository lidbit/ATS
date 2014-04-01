using System;
using System.Drawing;
using System.Windows.Forms;

namespace core
{
    public class Renderer
    {
        private BufferedGraphics _grafx;
        private Brush _brush;
        private Font _font;
        private Pen _pen;
        public Renderer(BufferedGraphics bufferedGraphics)
        {
            _grafx = bufferedGraphics;
            _brush = Brushes.Black;
            _pen = new Pen(_brush);
            _font = new System.Drawing.Font("Arial", 8);
        }

        public void fillRect(Brush b, Form f)
        {
            _grafx.Graphics.FillRectangle(b, 0, 0, f.Width, f.Height);
        }

        public void drawText(String text, int x, int y)
        {
            _grafx.Graphics.DrawString(text, _font, _brush, x, y);
        }

        public void drawSquare(int x, int y, int width)
        {
            _grafx.Graphics.DrawRectangle(_pen, new Rectangle(x, y, width, width));
        }

        public void SetFont(Font font)
        {
            _font = font;
        }

        public void restoreFont()
        {
            _font = new System.Drawing.Font("Arial", 8);
        }
    }
}
