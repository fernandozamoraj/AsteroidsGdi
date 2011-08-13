using System.Drawing;
using AsteroidsGdiApp.Core;

namespace AsteroidsGdiApp.GameObjects
{
    public class InsertCoinWarningView
    {
        public void Draw(Graphics graphics)
        {
            Draw(graphics, "PRESS S TO START GAME!", 0);
            Draw(graphics, "F to shoot and arrows to move ship", 20);
            Draw(graphics, "X to exit game", 40);
        }

        public void Draw(Graphics graphics, string message, int offset)
        {
            Font _font = new Font("Courier New", 12);

            var messageSize = graphics.MeasureString(message, _font);
            Point position = new Point((int)(Constants.CanvasWidth / 2 - messageSize.Width / 2),
                                       (int)(Constants.CanvasWidth / 2 - messageSize.Height / 2 + offset));

            graphics.DrawString(message, _font, Brushes.White, position);
        }


    }
}