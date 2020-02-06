using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.AreaConstraints
{
    class HalfCircleConstraint : IAreaConstraint
    {
        public enum CircleSide
        {
            LEFT, RIGHT
        }

        protected PointD center;
        protected double radius;
        protected CircleSide side;

        public HalfCircleConstraint(PointD center, double radius, CircleSide side)
        {
            this.center = center;
            this.radius = radius;
            this.side = side;
        }

        public bool IsPointInArea(PointD point)
        {
            double root = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(point.Y - center.Y, 2));
            double borderLine = center.X + (side == CircleSide.RIGHT ? root : -root );

            return side == CircleSide.RIGHT ? point.X <= borderLine : point.X >= borderLine;
        }
    }
}
