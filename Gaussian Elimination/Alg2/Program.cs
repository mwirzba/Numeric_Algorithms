using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    class Program
    {


        static void Main(string[] args)
        {
           int matrixSize =100;
           TestMethodsGen methods = new TestMethodsGen(matrixSize);
           
           //CHECK Q1
            methods.CheckQ1<double>(100);
            Console.WriteLine("DONE");
           //Q2
           List<Q2TimesResults> list =  new List<Q2TimesResults>(); 
           for (int i = 10; i <= matrixSize; i+=10)
           {
               Console.WriteLine(i);
               var rtn = methods.CheckQ2(i);
               list.Add(new Q2TimesResults {floutTime = rtn[0],doubleTime = rtn[1]});
           }
           
           //TestsMethods.WriteResultsToExcelFileQ2Result(list);
           Console.WriteLine("DONE");
           
           //CHECK H1
           Console.WriteLine("=========H1============");
           var timesResults = new float[3];
           int numberOfTests = 10;
           for (int i = 0; i < numberOfTests; i++)
           {
              var timeRtn = methods.CheckH1(matrixSize);
              timesResults[0] += timeRtn[0];
              timesResults[1] += timeRtn[1];
              timesResults[2] += timeRtn[2];
           }
           
           timesResults[0] /=numberOfTests;  
           timesResults[1] /=numberOfTests;
           timesResults[2] /=numberOfTests;
           
           
           //H2
           var rtnCheckH2 = methods.CheckH2(matrixSize); 
           Console.WriteLine("TIMES");         
           
           for (var i = 0; i < 3; i++)
           {
               float sum = 0;
               Console.WriteLine("==========================");
               for (int j = 0; j < matrixSize; j++)
               {
                   var tmp = i + 1;
                   sum += Math.Abs(rtnCheckH2[i][j]);
               }
           
               Console.WriteLine(sum/ matrixSize);
               sum = 0;
           }
           
           //H3
           for (int i = 2; i <= matrixSize; i++)
           {
                methods.CheckH3(i,numberOfTests);
           }
           
           Console.ReadKey();
           
           
           
           //E1
           var E1Res = methods.CheckE1(100);
           foreach (var time in E1Res)
           {
               Console.WriteLine(time);
           }
           
           Console.WriteLine("DONE");

            Console.ReadKey();

        }

       


    }
    
}
