using System;

namespace CalculatorLibrary
{
    public class Calculator
    {
        double num1, num2;

        public Calculator(double num1, double num2)
        {
            this.num1 = num1;
            this.num2 = num2;
        }

        public double Add() { return num1 + num2; }

        public double Sub() { return num1 - num2; }

        public double Mul() { return num1 * num2; }

        public double Div() { return num1 / num2; }

        public double Mod() { return num1 % num2; }



    }
}
