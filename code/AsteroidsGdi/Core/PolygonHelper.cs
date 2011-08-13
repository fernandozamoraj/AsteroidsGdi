using System;
using System.Drawing;

namespace AsteroidsGdiApp.Core
{
    public static class PolygonHelper
    {
        public static Point[] Rotate(this Point[] polygon, Point centroid, double angle)
        {
            for (int i = 0; i < polygon.Length; i++)
            {
                polygon[i] = RotatePoint(polygon[i], centroid, angle);
            }

            return polygon;
        }

        private static Point RotatePoint(Point point, Point centroid, double angle)
        {
            int x = centroid.X + (int)((point.X - centroid.X) * Math.Cos(angle) - (point.Y - centroid.Y) * Math.Sin(angle));
            int y = centroid.Y + (int)((point.X - centroid.X) * Math.Sin(angle) + (point.Y - centroid.Y) * Math.Cos(angle));

            return new Point(x, y);
        }

        public static Point[] ClonePolygon(this Point[] points)
        {
            Point[] clone = new Point[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                clone[i] = points[i];
            }

            return clone;
        }

        //http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
        public static bool IsPointInPolygon(this Point[] polygon, Point testPoint)
        {
            int i, j;
            bool isInside = false;
            int numberOfPoints = polygon.Length;

            for (i = 0, j = numberOfPoints - 1; i < numberOfPoints; j = i++)
            {
                if (((polygon[i].Y > testPoint.Y) != (polygon[j].Y > testPoint.Y)) &&
                    (testPoint.X < (polygon[j].X - polygon[i].X) * (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    isInside = !isInside;
            }

            return isInside;
        }

        public static bool CollidesWith(this Point[] polygon, Point[] otherPolygon)
        {
            for (int i = 0; i < polygon.Length; i++)
            { 
                if(otherPolygon.IsPointInPolygon(polygon[i]))
                    return true;
            }

            return false;
        }
    }
}