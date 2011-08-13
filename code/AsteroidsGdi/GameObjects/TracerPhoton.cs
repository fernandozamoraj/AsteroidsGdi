using System.Drawing;
using AsteroidsGdiApp.Core;

namespace AsteroidsGdiApp.GameObjects
{
    public class TracerPhoton : Photon
    {
        public override void Draw(Graphics graphics)
        {
            if (_bulletSprite != null && IsActive)
            {
                _bulletSprite.Polygon = new Point[] { new Point(-1 + _location.X, -1 + _location.Y), new Point(-1 + _location.X, 1 + _location.Y), new Point(1 + _location.X, _location.Y), new Point(-1 + _location.X, -1 + _location.Y) };

                Point[] clone = _bulletSprite.Polygon.ClonePolygon();

                graphics.DrawPolygon(Pens.Red, clone);
            }
        }
    }
}