using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    public interface ICalculator<T>
    {
        T Sum(T a, T b);
        T Difference(T a, T b);
        int Compare(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
    }
}
