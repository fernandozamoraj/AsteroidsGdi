using System;
using System.Collections.Generic;
using System.Drawing;
using AsteroidsGdiApp.GameObjects;

namespace AsteroidsGdiApp.Core
{
    public class Sprite
    {
        public Sprite()
        {
            Polygon = new[]{new Point(0,0), new Point(0, 0), new Point(0, 0),  };
        }

        public Point[] Polygon { get; set; }
        public float Speed { get; set; }
        public double TravelDirectionInDegrees { get; set; }
        public double DirectionOfSprite { get; set; }
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }

        public bool IsPointWithin(Point point)
        {
            return Polygon.IsPointInPolygon(point);
        }

        internal bool CollidesWith(Sprite sprite)
        {
            return sprite.Polygon.CollidesWith(this.Polygon);
        }

        public List<Line> ToLineList()
        {
            List<Line> lines = new List<Line>();

            for(int i = 0; i < Polygon.Length-1; i++)
            {
                lines.Add(
                    new Line
                        {
                            StartPoint = new Point(Polygon[i].X, Polygon[i].Y),
                            EndPoint = new Point(Polygon[i + 1].X, Polygon[i + 1].Y)
                        });
                
            }

            return lines;
        }
    }
}