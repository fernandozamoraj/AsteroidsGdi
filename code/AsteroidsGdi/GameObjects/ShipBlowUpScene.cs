using System;
using System.Collections.Generic;
using System.Drawing;

namespace AsteroidsGdiApp.GameObjects
{
    public class ShipBlowUpScene
    {
        private List<Line> Lines;
        private List<Point> _deltas;
        Random _random = new Random((int)DateTime.Now.Ticks);

 
        public void Start(PlayerShip ship)
        {
            Lines = ship.Sprite.ToLineList();

            CreateDeltas();
        }

        private void CreateDeltas()
        {
            _deltas = new List<Point>();

            for(int i = 0; i < Lines.Count; i++)
            {
                Point point = new Point();

                point.X = _random.Next(6) - 3;
                point.Y = _random.Next(6) - 3;
                _deltas.Add(point);
            }
        }

        public void End()
        {
            Lines = null;
        }

        public void Draw(Graphics graphics)
        {
            if(Lines == null)
                return;

            UpdateLines();
            graphics.DrawLine(Pens.White, Lines[0].StartPoint, Lines[0].EndPoint);
            graphics.DrawLine(Pens.White, Lines[1].StartPoint, Lines[1].EndPoint);
            graphics.DrawLine(Pens.White, Lines[2].StartPoint, Lines[2].EndPoint);
        }

        private void UpdateLines()
        {
            if(Lines == null)
                return;


            for(int i = 0; i < Lines.Count; i++)
            {
                Lines[i].StartPoint.X += _deltas[i].X;
                Lines[i].StartPoint.Y += _deltas[i].Y;
                Lines[i].EndPoint.X += _deltas[i].X;
                Lines[i].EndPoint.Y += _deltas[i].Y;
            }
        }
    }

    public class ScoredPointsDisplay
    {
        Point _location;
        Point _startLocation;
        int _points;
        bool _active;
        
        public void Display(Point location, int points)
        {
            _active = true;
            _location = location;
            _points = points;
            _startLocation = _location;
        }

        public void Draw(Graphics graphics)
        {
            if (IsActive())
            {
                string points = string.Format("{0}", _points);
                Font font = new Font("Courier New", 12);
                graphics.DrawString(points, font, Brushes.White, _location.X, _location.Y);
            }
        }

        public void Update()
        {
            _location = new Point(_location.X, _location.Y - 2);
            if (_startLocation.Y - _location.Y > 40)
                _active = false;
        }

        private bool IsActive()
        {
            return _active;
        }
    }
}