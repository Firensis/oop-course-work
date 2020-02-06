using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    class TestResult
    {
        public int PointsCount { get; set; }
        public double RealResult { get; set; }
        public double MonteCarloResult { get; set; }
        public double RelativeError { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Точек: {0:e0}; Реальная площадь: {1:f3}; Монте-Карло: {2:f3}; относительная ошибка: {3:f4}%", 
                PointsCount, 
                RealResult, 
                MonteCarloResult, 
                RelativeError * 100
            );
        }
    }
}
