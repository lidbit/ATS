
namespace core
{
    public class Box
    {
        private Renderer _renderer;

        public Box(Renderer renderer, int width, int height)
        {
            _renderer = renderer;
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
        }

        public void draw()
        {
            _renderer.drawSquare(X, Y, Width);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
