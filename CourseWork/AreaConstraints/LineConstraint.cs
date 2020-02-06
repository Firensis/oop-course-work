using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.AreaConstraints
{
    class LineConstraint : IAreaConstraint
    {
        protected double koef;
        protected double addition;
        protected ConstraintType type;

        public enum ConstraintType
        {
            TOP_CONSTRAINT, BOTTOM_CONSTRAINT
        }

        public LineConstraint(PointD left, PointD right, ConstraintType type)
        {
            koef = (right.Y - left.Y) / (right.X - left.X);
            addition = left.Y - koef * left.X;
            this.type = type;
        }

        public bool IsPointInArea(PointD point)
        {
            double calculatedY = koef * point.X + addition;

            if (type == ConstraintType.TOP_CONSTRAINT)
            {
                return point.Y <= calculatedY;
            }
            else
            {
                return point.Y >= calculatedY;
            }
        }
    }
}
