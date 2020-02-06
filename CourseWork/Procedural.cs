using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    static class Procedural
    {
        enum LineConstraintType
        {
            underBd,
            aboveBe
        };

        const int firstRandomSeed = 13;
        const int secondRandomSeed = 178;
        static PointD b, d, circleCenter, areaStart, areaEnd;
        static double radius, bdKoef, bdAddition, beKoef, beAddition, areaWidth, areaHeight, realSquare;
        static Random randomX, randomY;

        public static void Init(PointD b, PointD d)
        {
            Procedural.b = b;
            Procedural.d = d;

            
            bdKoef = (d.Y - b.Y) / (d.X - b.X);
            bdAddition = b.Y - bdKoef * b.X;

            radius = d.Y - b.Y;
            circleCenter = new PointD
            {
                X = d.X,
                Y = d.Y - radius
            };

            PointD e = new PointD
            {
                X = d.X,
                Y = d.Y - 2 * radius
            };
            beKoef = (e.Y - b.Y) / (e.X - b.X);
            beAddition = b.Y - beKoef * b.X;
            
            areaStart = new PointD
            {
                X = b.X,
                Y = b.Y - radius
            };

            areaEnd = new PointD
            {
                X = e.X + radius,
                Y = e.Y
            };

            areaWidth = e.X + radius - b.X;
            areaHeight = 2 * radius;

            realSquare = GetRealSquare();
        }


        public static double GetSquareMonteKarlo(int pointsCount)
        {
            randomX = new Random(firstRandomSeed);
            randomY = new Random(secondRandomSeed);
            int hits = 0;

            for (int i = 0; i < pointsCount; i++)
            {
                PointD point = GetRandomPoint();

                if (CheckPoint(point))
                {
                    hits++;
                }
            }

            return areaWidth * areaHeight * hits / pointsCount;
        }

        public static TestResult Test(int pointsCount)
        {
            TestResult result = new TestResult
            {
                PointsCount = pointsCount
            };

            result.MonteCarloResult = GetSquareMonteKarlo(pointsCount);
            result.RealResult = realSquare;

            double error = Math.Abs(result.RealResult - result.MonteCarloResult);
            result.RelativeError = error / result.RealResult;

            return result;
        }

        private static PointD GetRandomPoint()
        {
            double randX = randomX.NextDouble();
            double randY = randomY.NextDouble();

            return new PointD
            {
                X = areaStart.X + randX * areaWidth,
                Y = areaStart.Y + randY * areaHeight
            };
        }


        private static double GetRealSquare()
        {
            return radius * (d.X - b.X + Math.PI * radius / 2);
        }

        private static bool CheckPoint(PointD point)
        {
            return CheckLineConstraint(point, LineConstraintType.aboveBe) &&
                CheckLineConstraint(point, LineConstraintType.underBd) &&
                CheckCircleConstraint(point);
        }

        private static bool CheckLineConstraint(PointD toCheck, LineConstraintType constraintType)
        {
            if (constraintType == LineConstraintType.underBd)
            {
                double y = bdKoef * toCheck.X + bdAddition;
                return y >= toCheck.Y;
            }
            else
            {
                double y = beKoef * toCheck.X + beAddition;
                return y <= toCheck.Y;
            }
        }

        private static bool CheckCircleConstraint(PointD toCheck)
        {
            double root = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(toCheck.Y - circleCenter.Y, 2));
            double maxX = circleCenter.X + root;

            return toCheck.X <= maxX;
        }
    }
}
