using System;
using System.Drawing;
using AsteroidsGdiApp.Core;

namespace AsteroidsGdiApp.GameObjects
{
    public class EnemyShip : IGameObject
    {
        Point _location = new Point(-40, -40);
        Sprite _shipSprite;
        private DateTime _startTime;
        private float _deltaX = -3;
        private float _deltaY;
        readonly Random _random = new Random();

        public EnemyShip()
        {
            _location = new Point(-40, -40);
        }

        public Point Location
        {
            get
            {
                return _location;
            }
        }

        public void Activate()
        {
            _shipSprite = new Sprite();
            _shipSprite.Polygon = new[]
            {
                new Point(0, 0), 
                new Point(30, 0), 
                new Point(30, 20), 
                new Point(0, 20), 
                new Point(0, 0)};

            _startTime = DateTime.Now;
            _location = new Point(Constants.CanvasWidth, 60);
        }

        public bool IsActive
        {
            get
            {
                return _location.X > 0;
            }
        }

        public void Update()
        {

            if (DateTime.Now.Subtract(_startTime).TotalMilliseconds > 750)
            {
                int randomValue = _random.Next(3);

                if (randomValue % 2 == 0)
                {
                    _deltaY = 3;
                }
                else if(randomValue % 3 == 0)
                {
                    _deltaY = -3;
                }
                
                _startTime = DateTime.Now;
            }

            _location.X += (int)_deltaX;
            _location.Y += (int)_deltaY;

            if (_location.Y < 0)
                _deltaY = 3;
        }

        public void Draw(Graphics graphics)
        {
            if (!IsActive)
                return;

            CreateShip();
            DrawShip(graphics);
        }

        private void DrawShip(Graphics graphics)
        {
            graphics.DrawPolygon(Pens.White, _shipSprite.Polygon);
            graphics.DrawLine(Pens.White, _location.X - 20, _location.Y, _location.X + 20, _location.Y);
        }

        private void CreateShip()
        {
            _shipSprite.Polygon = new[] 
                                      { 
                                          new Point(_location.X - 5, _location.Y-10), 
                                          new Point(_location.X+5, _location.Y-10),
                                          new Point(_location.X+15, _location.Y),
                                          new Point(_location.X+5, _location.Y+10), 
                                          new Point(_location.X-5, _location.Y+10),
                                          new Point(_location.X-15, _location.Y),
                                          new Point(_location.X-5, _location.Y-10)
                                      };
        }

        internal bool IsPointWithin(Point point)
        {
            return _shipSprite.IsPointWithin(point);
        }

        internal void Inactivate()
        {
            _location.X = -100;
        }
    }
}
