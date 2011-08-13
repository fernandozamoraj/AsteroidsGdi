using System.Drawing;
using AsteroidsGdiApp;
using AsteroidsGdiApp.Core;

namespace CheckersApp3
{
    public class BoardDrawingMachine : IBoardDrawingMachine
    {
        public void DrawBoard(Graphics formGraphics, Size boardSize)
        {
            SetImages();
            
            Image backBuffer = DrawToBuffer(boardSize);

            TransferDrawingToScreen(backBuffer, formGraphics);
        }

        private Image DrawToBuffer(Size boardSize)
        {
            //To allow doublebuffering
            var backbuffer = new Bitmap(boardSize.Width, boardSize.Height);

            using(Graphics graphicsBuffer = Graphics.FromImage(backbuffer))
            {
                DrawBoard(graphicsBuffer, new Size(Constants.CanvasWidth, Constants.CanvasWidth));

                graphicsBuffer.Dispose();
            }

            return backbuffer;
        }

        private void SetImages()
        {
            //if (_blackCheckerImage == null)
            //{
            //    _blackCheckerImage = (Bitmap) Image.FromFile("./blackchecker.png");
            //    _redCheckerImage = (Bitmap) Image.FromFile("./redchecker.png");
            //}
        }

        private static void TransferDrawingToScreen(Image backbuffer, Graphics formGraphics)
        {
            formGraphics.DrawImageUnscaled(backbuffer, 0, 0);
        }
    }
}