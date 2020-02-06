using CourseWork.Models;
using CourseWork.Checker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Algorithms
{
    class MonteCarlo : ISquareCalculator
    {
        public int PointsCount { get; set; } = 1000000;

        protected const int firstRandomSeed = 13;
        protected const int secondRandomSeed = 178;
        protected PointChecker checker;
        protected PointD areaStart;
        protected Random randomX;
        protected Random randomY;
        protected double width;
        protected double height;

        public MonteCarlo(PointChecker checker, PointD leftBottom, PointD rightTop)
        {
            this.checker = checker;
            areaStart = leftBottom;
            width = rightTop.X - leftBottom.X;
            height = rightTop.Y - leftBottom.Y;
        }

        public double GetSquare()
        {
            randomX = new Random(firstRandomSeed);
            randomY = new Random(secondRandomSeed);
            int hits = 0;

            for (int i = 0; i < PointsCount; i++)
            {
                PointD point = GetRandomPoint();

                if (checker.CheckPoint(point))
                {
                    hits++;
                }
            }

            return width * height * hits / PointsCount;
        }

        protected PointD GetRandomPoint()
        {
            double randX = randomX.NextDouble();
            double randY = randomY.NextDouble();

            return new PointD
            {
                X = areaStart.X + randX * width,
                Y = areaStart.Y + randY * height
            };
        }
    }
}
