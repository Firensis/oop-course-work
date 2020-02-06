using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.AreaConstraints
{
    interface IAreaConstraint
    {
        bool IsPointInArea(PointD point);
    }
}
