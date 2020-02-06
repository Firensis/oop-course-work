using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Algorithms
{
    class TrueCalculator : ISquareCalculator
    {
        protected double square;

        public TrueCalculator(double bX, double centerX, double radius)
        {
            square = radius * (centerX - bX + Math.PI * radius / 2);
        }

        public double GetSquare()
        {
            return square;
        }
    }
}
