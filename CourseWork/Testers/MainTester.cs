using CourseWork.Algorithms;
using CourseWork.AreaConstraints;
using CourseWork.Models;
using CourseWork.Checker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Testers
{
    class MainTester
    {
        protected PointD b;
        protected PointD d;
        protected PointD center;
        protected double radius;
        protected double realResult;

        protected MonteCarlo monteCarlo;

        public MainTester(PointD b, PointD d)
        {
            this.b = b;
            this.d = d;

            radius = d.Y - b.Y;
            center = new PointD
            {
                X = d.X,
                Y = d.Y - radius
            };
            monteCarlo = GetMonteCarlo();
            TrueCalculator trueCalculator = new TrueCalculator(b.X, center.X, radius);
            realResult = trueCalculator.GetSquare();
        }

        protected MonteCarlo GetMonteCarlo()
        {
            PointD leftBottom = new PointD
            {
                X = b.X,
                Y = b.Y - radius
            };

            PointD rightTop = new PointD
            {
                X = d.X + radius,
                Y = d.Y
            };

            PointChecker checker = GetChecker();
            return new MonteCarlo(checker, leftBottom, rightTop);
        }

        protected PointChecker GetChecker()
        {
            PointD e = new PointD
            {
                X = d.X,
                Y = d.Y - 2 * radius
            };

            return new PointChecker(new List<IAreaConstraint>
            {
                new LineConstraint(b, d, LineConstraint.ConstraintType.TOP_CONSTRAINT),
                new LineConstraint(b, e, LineConstraint.ConstraintType.BOTTOM_CONSTRAINT),
                new HalfCircleConstraint(center, radius, HalfCircleConstraint.CircleSide.RIGHT)
            });
        }
        
        public TestResult Test(int pointsCount)
        {
            TestResult result = new TestResult
            {
                PointsCount = pointsCount
            };

            monteCarlo.PointsCount = pointsCount;
            result.MonteCarloResult = monteCarlo.GetSquare();
            result.RealResult = realResult;

            double error = Math.Abs(result.RealResult - result.MonteCarloResult);
            result.RelativeError = error / result.RealResult;

            return result;
        }
    }
}
