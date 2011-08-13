using System.Drawing;

namespace AsteroidsGdiApp
{
    public interface IBoardDrawingMachine
    {
        void DrawBoard(Graphics formGraphics, Size boardSize);
    }
}