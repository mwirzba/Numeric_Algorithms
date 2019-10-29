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

            
            int numberOfElements = 4;
            Algorithm algorithm =  new Algorithm(numberOfElements, EnumsContainer.DataType.Float);
            float accuracy = 0.0001f;

            int numberdefrbdfvghefvgghjk = 0;

            for (int p = 0; p < 1; p++)
            {
                DataSource d = new DataSource(numberOfElements);

                float[][] Matrix = d.GetMatrixWithFloatData(numberOfElements);

                float[] VectorX = new float[numberOfElements];

                float[][] MatrixCopy = new float[numberOfElements][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    MatrixCopy[i] = new float[Matrix[i].Length];
                    Array.Copy(Matrix[i], MatrixCopy[i], Matrix[i].Length);
                }

                float[][] Matrix2 = new float[numberOfElements][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix2[i] = new float[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix2[i], Matrix[i].Length);
                }

                float[][] Matrix3 =  new float[numberOfElements][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix3[i] = new float[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix3[i], Matrix[i].Length);
                }

                /*Matrix3 = new[]
                {
                    new float[] {4, -2, 4, -2, 8}, new float[] {3, 1, 4, 2, 7}, new float[] {2, 4, 2, 1, 10},
                    new float[] {2, -2, 4, 2, 2}
                };*/

                #region GaussTypeNormal


                float res1 = 0;
                var vectorResult = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
                if (vectorResult != null)
                {
                    // algorithm.PrintResult(algorithm.VectorX);
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResult);
                    Console.WriteLine();
                    //algorithm.PrintResult(rtn);


                    foreach (var variable in rtn)
                    {
                        res1 += variable;
                    }
                    Console.WriteLine("Result:" + Math.Abs(res1));
                }

                #endregion
    

                #region GaussPartialPivot

                float res2 = 0;
                var vectorResult2 = algorithm.GaussElimination(Matrix2, 0, EnumsContainer.GaussType.PartialPivot);
                if (vectorResult2 != null)
                {
                    // algorithm.PrintResult(algorithm.VectorX);
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResult2);
                    algorithm.PrintResult(vectorResult2);

                    foreach (var variable in rtn)
                    {
                        res2 += variable;
                    }
                    Console.WriteLine("Result:" + Math.Abs(res2));
                }

                #endregion
                
                
                #region GaussTypeFullPivot
                int[] indexArray = new int[numberOfElements];
                for (int i = 0; i < numberOfElements; i++)
                {
                    indexArray[i] = i;
                }

                float res3 = 0;
                var vectorResult3 = algorithm.GaussElimination(Matrix3, 0, EnumsContainer.GaussType.FullPivot, indexArray);
                if (vectorResult3 != null)
                {
                    // algorithm.PrintResult(algorithm.VectorX);
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResult3);
                    Console.WriteLine();
                    algorithm.PrintResult(vectorResult3);

                    foreach (var variable in rtn)
                    {
                        res3 += variable;
                    }
                    Console.WriteLine("Result:" + Math.Abs(res3));
                }

                #endregion

    
            }






            Console.ReadKey();
        }
    }
}
