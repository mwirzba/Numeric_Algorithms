using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    class Program
    {
        static void Main(string[] args)
        {
           Algorithm algorithm =  new Algorithm(4);
            float accuracy = 0.0001f;
            if (algorithm.GaussElimination(0))
            {
                algorithm.PrintResult();
            }

            Console.ReadKey();
        }
    }
}
