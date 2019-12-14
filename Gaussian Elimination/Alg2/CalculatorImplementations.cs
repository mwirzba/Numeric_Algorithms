using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    struct DoubleCalculator : ICalculator<double>
    {
        public int Compare(double a, double b)
        {
            throw new NotImplementedException();
        }

        public double Difference(double a, double b)
        {
            return a - b;
        }

        public double Divide(double a, double b)
        {
            return a / b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Sum(double a, double b)
        {
            return a + b;
        }
    }

    struct FloatCalculator : ICalculator<float>
    {
        public int Compare(float a, float b)
        {
            throw new NotImplementedException();
        }

        public float Difference(float a, float b)
        {
            return a - b;
        }

        public float Divide(float a, float b)
        {
            return a / b;
        }

        public float Multiply(float a, float b)
        {
            return a * b;
        }

        public float Sum(float a, float b)
        {
            return a + b;
        }
    }

}
