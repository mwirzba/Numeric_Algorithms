using Alg2;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ALS
{
    class Program
    {

       
        static void Main(string[] args)
        {
            //800//1500/8000
            DataSource dataSource = new DataSource(4, 3);
            var rtn = dataSource.GetDataFromFile(0,800);
            var matrix = dataSource.PutResultsToMatrix(rtn);
            var matrixU = dataSource.GetRandomNumbers(20, matrix.Length);
            var matrixP = dataSource.GetRandomNumbers(20, matrix[0].Length);

            Console.WriteLine("UZ" + matrix.Length + "PROD" + matrix[0].Length);

            Stopwatch sw;

            ALSAlgorithm aLSAlgorithm;
            for (int d = 3; d < 20 ; d++)
            {
                var matrixArgU = new double[d][];
                var matrixArgP = new double[d][];
     
                //var results = new List<int>(); 
               //for (double sigma = 0.1; sigma <= 1; sigma +=0.1)
               //{
                    dataSource.CopyMatrix(matrixArgU, matrixU);
                    dataSource.CopyMatrix(matrixArgP, matrixP);
                Console.WriteLine(d);
                    aLSAlgorithm = new ALSAlgorithm(matrix.Length, matrix[0].Length,d,0.1, matrix,matrixArgP,matrixArgU);
                    int numberOfLoops;
                    sw = Stopwatch.StartNew();
                    double[][] result = aLSAlgorithm.Start(out numberOfLoops);
                    sw.Stop();
                    var numbers = aLSAlgorithm.GetNumberOfBadResults(result);
                  //  Console.WriteLine("=================================");
                    dataSource.WriteResultErrosToCsv(d,0.1, numbers[0], numbers[1],numberOfLoops,   sw.ElapsedMilliseconds);
                //}
            }
            Console.WriteLine("DONE");




            //DataSource dataSource = new DataSource(4,3);
            //var rtn = dataSource.GetDataFromFile(0,800);
            //var matrix = dataSource.PutResultsToMatrix(rtn);
            //ALSAlgorithm aLSAlgorithm = new ALSAlgorithm(matrix.Length, matrix[0].Length,3, 0.5, matrix);


            // var cmp  = dataSource.GetDataFromFile(0, 300);
            // var cmpToMatrix = dataSource.PutResultsToMatrix(cmp);
            // Console.WriteLine("Start");
            // aLSAlgorithm.PrintMatrix(cmpToMatrix);


            //aLSAlgorithm.Start();      
            Console.Read();



        }
    }
}
  