using System;
using System.Drawing;
using AsteroidsGdiApp.Core;

namespace AsteroidsGdiApp.GameObjects
{
    public class Photon : IGameObject
    {
        private float _maxDistance;
        private float _distanceTraveled = Constants.MaxEnemyBulletDistance + 1;
        protected Sprite _bulletSprite = new Sprite();
        protected Point _location = new Point(0, 0);
        private PhotonTimeManager _photonTimeManager;

        public void Fire(Point startLocation, double direction, float maxDistance, PhotonTimeManager photoTimeManager)
        {
            _photonTimeManager = photoTimeManager;

            if(!EnoughTimeHasPassed())
                return;

            _maxDistance = maxDistance;
            _distanceTraveled = 0;
            _bulletSprite = new Sprite();
            _bulletSprite.Polygon = new[]
                                        {
                                            new Point(startLocation.X, startLocation.Y),
                                            new Point(startLocation.X+1, startLocation.Y+1),
                                            new Point(startLocation.X, startLocation.Y)
                                        };

            _bulletSprite.Speed = Constants.BulletSpeed;
            _bulletSprite.TravelDirectionInDegrees = direction;
            _location = startLocation;
            _photonTimeManager.SetFired();
            
        }

        private bool EnoughTimeHasPassed()
        {
            return _photonTimeManager.EnoughTimeHasPassed();
        }

        public Point Location
        {
            get { return _location; }
        }

        public bool IsActive
        {
            get
            {
                return _distanceTraveled < _maxDistance;
            }
        }

        public void Update()
        {
            if (_bulletSprite != null && IsActive)
            {
                double radians = _bulletSprite.TravelDirectionInDegrees.DegreesToRadians();

                _location.X += (int)(_bulletSprite.Speed * Math.Cos(radians));
                _location.Y += (int)(_bulletSprite.Speed * Math.Sin(radians));

                if (_location.X < 0)
                    _location.X = Constants.CanvasWidth;
                if (_location.X > Constants.CanvasWidth)
                    _location.X = 0;
                if (_location.Y < 0)
                    _location.Y = Constants.CanvasWidth;
                if (_location.Y > Constants.CanvasWidth)
                    _location.Y = 0;

                _distanceTraveled += _bulletSprite.Speed;
            }
        }
        
        public virtual void Draw(Graphics graphics)
        {
            if(_bulletSprite != null && IsActive)
            {
                _bulletSprite.Polygon = new Point[] { new Point(-1 + _location.X, -1 + _location.Y), new Point(-1 + _location.X, 1 + _location.Y), new Point(1 + _location.X, _location.Y), new Point(-1 + _location.X, -1 + _location.Y) };

                Point[] clone = _bulletSprite.Polygon.ClonePolygon();

                graphics.DrawPolygon(Pens.White, clone);
            }
        }

        public void SetInactive()
        {
            _distanceTraveled = _maxDistance + 1;
        }
    }
}