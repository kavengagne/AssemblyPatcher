using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace CalculatorLib
{
    public class Calculator
    {
        [ComVisible(false)]
        public double Add(double op1, double op2)
        {
            var result = 0;
            foreach (var d in new[] { 1,2,3 })
            {
                result += d;
            }
            return result;
        }

        public static double Add2(string op1)
        {
            return 3 + 2;
        }

        public object Add3(double op1, double op2)
        {
            var result = op1 + op2;
            return result;
        }

        public Tuple<int, int> Add4(double op1, double op2)
        {
            var result = op1 + op2;
            return new Tuple<int, int>(5, 6);
        }

        public void Add5(double op1, double op2)
        {
            var result = op1 + op2;
        }

        public double Add_L(double op1, double op2)
        {
            var result = op1 + op2;
            Debug.WriteLine("Result: {0}", result);
            return result;
        }

        public double Sub(double op1, double op2)
        {
            return op1 - op2;
        }
    }
}
