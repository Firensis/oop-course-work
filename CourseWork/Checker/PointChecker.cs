using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CourseWork.Models;
using CourseWork.AreaConstraints;

namespace CourseWork.Checker
{
    class PointChecker
    {
        protected List<IAreaConstraint> constraints;

        public PointChecker(List<IAreaConstraint> constraints)
        {
            this.constraints = constraints;
        }

        public bool CheckPoint(PointD point)
        {
            foreach (IAreaConstraint constraint in constraints)
            {
                if (!constraint.IsPointInArea(point))
                {
                    return false;
                }
            }

            return true;
        }       
    }
}
