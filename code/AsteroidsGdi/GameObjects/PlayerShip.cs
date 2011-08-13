using System;
using System.Drawing;
using AsteroidsGdiApp.Core;

namespace AsteroidsGdiApp.GameObjects
{
    public class PlayerShip : IGameObject
    {
        private Point _location = new Point(40, 40);
        private Sprite _shipSprite;
        private bool _isActive = true;
        private int _shieldCountDown = Constants.ShieldCountDown;
        private int _shields = 3;
        private bool _thrusterIsOn;
        private DateTime _shipBlewUp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        ShipBlowUpScene _blowupScene = new ShipBlowUpScene();

        public PlayerShip()
        {
            _isActive = true;
            _shipSprite = new Sprite();
            _shipSprite.TravelDirectionInDegrees = 30f;
        }

        public void TurnOnThruster()
        {
            _thrusterIsOn = true;
        }

        public void TurnOffThruster()
        {
            _thrusterIsOn = false;
        }

        public void TurnOnShield()
        {
            if (_shields > 0 && _shieldCountDown <= 0)
            {
                _shields--;
                _shieldCountDown = Constants.ShieldCountDown;
            }
        }

        public Sprite Sprite 
        { 
            get 
            { 
                return _shipSprite; 
            } 
        }

        public void SetInactive()
        {
            if (_shieldCountDown < 1)
            {
                _isActive = false;
            }
        }

        public void Activate()
        {
            _isActive = true;
            _shieldCountDown = Constants.ShieldCountDown;
            _location = new Point(Constants.CanvasWidth / 2, Constants.CanvasWidth / 2);

            _shipSprite.DeltaX = 0;
            _shipSprite.DeltaY = 0;
        }

        public void SetShield()
        {
            if (_shieldCountDown < 1)
            {
                _shieldCountDown = Constants.ShieldCountDown;
            }
        }

        public bool ShieldIsOn
        {
            get { return _shieldCountDown > 0; }
        }

        public bool IsActive
        {
            get { return _isActive; }
        }

        public void Draw(Graphics graphics)
        {
            if(DateTime.Now.Subtract(_shipBlewUp).TotalMilliseconds < 1000)
            {
                DrawShipBlowingup(graphics);
            }

            if (!_isActive)
                return;

            double rotation = _shipSprite.DirectionOfSprite.DegreesToRadians();
            
            
            if(DateTime.Now.Subtract(_shipBlewUp).TotalMilliseconds > 1000)
            {
                CreateShip();
                DrawShip(rotation, graphics);
                DrawThruster(rotation, graphics);
            }
        }

        private void DrawShipBlowingup(Graphics graphics)
        {
            _blowupScene.Draw(graphics);
        }

        private void CreateShip()
        {
            _shipSprite.Polygon = new[] { 
                                            new Point(-12 + _location.X, -8 + _location.Y), 
                                            new Point(-12 + _location.X, 8 + _location.Y), 
                                            new Point(12 + _location.X, _location.Y), 
                                            new Point(-12 + _location.X, -8 + _location.Y) };
        }

        public void BlowUpShip()
        {
            _shipBlewUp = DateTime.Now;
            _blowupScene.Start(this);
        }

        private void DrawShip(double rotation, Graphics graphics)
        {
            Point[] clone = _shipSprite.Polygon.ClonePolygon().Rotate(_location, rotation);

            if (_shieldCountDown > 0)
            {
                graphics.DrawPolygon(Pens.DarkGray, clone);
                graphics.DrawEllipse(Pens.DarkGray, _location.X-20, _location.Y-20, 40, 40);
            }
            else
                graphics.FillPolygon(Brushes.White, clone);
        }

        private void DrawThruster(double rotation, Graphics graphics)
        {
            if (_thrusterIsOn)
            {
                var thruster = new[]{
                                        new Point(-18 + _location.X, _location.Y),
                                        new Point(-10 + _location.X, -6 + _location.Y), 
                                        new Point(-10 + _location.X, 6 + _location.Y), 
                                        new Point(-18 + _location.X, _location.Y),
                                    };

               
                Point[] thrusterClone = thruster.ClonePolygon().Rotate(_location, rotation);

                graphics.DrawPolygon(Pens.DarkGray, thrusterClone);
            }
        }

        public void Update()
        {
            _shieldCountDown--;

            if (!_isActive)
                return;

            _location.X += (int)_shipSprite.DeltaX;
            _location.Y += (int)_shipSprite.DeltaY;

            if (_location.X < 0)
                _location.X = Constants.CanvasWidth;
            if (_location.X > Constants.CanvasWidth)
                _location.X = 0;
            if (_location.Y < 0)
                _location.Y = Constants.CanvasWidth;
            if (_location.Y > Constants.CanvasWidth)
                _location.Y = 0;
        }

        public void RotateRight()
        {
            _shipSprite.DirectionOfSprite = (_shipSprite.DirectionOfSprite + 10) % 360;
        }


        public void RotateLeft()
        {
            _shipSprite.DirectionOfSprite = (_shipSprite.DirectionOfSprite - 10);

            if (_shipSprite.DirectionOfSprite < 0)
                _shipSprite.DirectionOfSprite += 360;
        }

        public void SlowDown()
        {
            double dx = Math.Cos(_shipSprite.DirectionOfSprite.DegreesToRadians());
            double dy = Math.Sin(_shipSprite.DirectionOfSprite.DegreesToRadians());
            double limit = 0.8 * 10;

            if (_shipSprite.DeltaX - dx > -limit && _shipSprite.DeltaX - dx < limit)
                _shipSprite.DeltaX -= dx;
            if (_shipSprite.DeltaY - dy > -limit && _shipSprite.DeltaY - dy < limit)
                _shipSprite.DeltaY -= dy;

        }

        public void Thrust()
        {
            double dx = Math.Cos(_shipSprite.DirectionOfSprite.DegreesToRadians());
            double dy = Math.Sin(_shipSprite.DirectionOfSprite.DegreesToRadians());
            double limit = 0.8 * 10;

            if (_shipSprite.DeltaX + dx > -limit && _shipSprite.DeltaX + dx < limit)
                _shipSprite.DeltaX += dx;
            if (_shipSprite.DeltaY + dy > -limit && _shipSprite.DeltaY + dy < limit)
                _shipSprite.DeltaY += dy;

        }

        public Point Location
        {
            get { return _location; }
        }

        public double ShipDirection
        {
            get { return _shipSprite.DirectionOfSprite; }
        }

        internal bool IsPointWithin(Point point)
        {
            return _shipSprite.IsPointWithin(point);
        }
    }
}