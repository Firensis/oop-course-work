using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseWork.Models;
using CourseWork.Testers;
using CourseWork.Exceptions;
using System.Diagnostics;

namespace CourseWork
{
    public partial class Form1 : Form
    {
        PointD b, d;
        string resultStr = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!TryLoadPoints() || !ValidatePoints())
            {
                return;
            }

            ActivateOOP();
            //ActivateProcedural();

            MessageBox.Show(resultStr, "Результат работы");
        }

        protected void ActivateOOP()
        {
            MainTester tester = new MainTester(b, d);
            resultStr = "";
            int points = 1000;
            
            for (int i = 0; i < 5; i++)
            {
                TestResult result = tester.Test(points);
                resultStr += result.ToString() + "\n";
                points *= 10;
            }
        }

        protected void ActivateProcedural()
        {
            Procedural.Init(b, d);

            resultStr = "";
            int points = 1000;
            
            for (int i = 0; i < 5; i++)
            {
                TestResult result = Procedural.Test(points);
                resultStr += result.ToString() + "\n";
                points *= 10;
            }
        }

        protected bool TryLoadPoints()
        {
            bool result = true;

            try
            {
                b = new PointD
                {
                    X = GetTextBoxValue(tb_bx),
                    Y = GetTextBoxValue(tb_by),
                };

                d = new PointD
                {
                    X = GetTextBoxValue(tb_dx),
                    Y = GetTextBoxValue(tb_dy)
                };
            }
            catch (WrongInputException)
            {
                MessageBox.Show("Необходимо ввести корректные значения координат точек! (допустимы десятичные дроби, в которой дробная часть отделена точкой или запятой)");
                result = false;
            }

            return result;
        }

        protected bool ValidatePoints()
        {
            bool result = b.X < d.X && b.Y < d.Y;

            if (!result)
            {
                MessageBox.Show("Абсцисса и ордината точки b должны быть соответственно меньше абсциссы и ординаты точки d!");
            }

            return result;
        }

        protected double GetTextBoxValue(TextBox textbox)
        {
            string value = textbox.Text.Replace('.', ',');

            if (double.TryParse(value, out double result))
            {
                return result;
            }
            else
            {
                throw new WrongInputException();
            }
        }
    }
}
