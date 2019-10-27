using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    class Algorithm
    {
        public Algorithm(int numberOfElements)
        {

            NumberOfElements = numberOfElements;
            Matrix = new []
            {
                //new float[] { 2, 1, 1, 7 } , new float[] { 0, 2, 1, 4 } , new float[] { 1, 1, 2, 6 }
              // new float[] { 1, 1/2f , 1/3f , 32 } , new float[] { 1/2f, 1/3f, 1/4f, 22 } , new float[] { 1/3f, 1/4f, 1/5f, 17 }
                 new float[] { 1.00f, 0.5f , 0.33f , 32 } , new float[] { 0.5f, 0.33f, 0.25f, 22 } , new float[] { 0.33f, 0.25f, 0.20f, 17 }

               //new float[] { 4,-2,4,-2,8 }, new float[] {3,1,4,2,7 },new float[] {2,4,2,1,10},new float[] {2,-2,4,2,2}
            };          
            //new float[,] { { 4,-2,4,-2,8 },{3,1,4,2,7 }, {2,4,2,1,10}, {2,-2,4,2,2} };                
            //new float[NumberOfElements,NumberOfElements+1];
            VectorX = new float[NumberOfElements];

            MatrixCopy =  new float[NumberOfElements][];
            for (int i = 0; i < Matrix.Length; i++)
            {
                MatrixCopy[i] = new float[Matrix[i].Length];
                Array.Copy(Matrix[i],MatrixCopy[i],Matrix[i].Length);
            }
          //  Array.Copy(Matrix,MatrixCopy,Matrix.Length);

            PrintMatrix(MatrixCopy);
           /* for (int i = 0; i < numberOfElements; i++)
            {
                Console.WriteLine(Matrix[][0]);
             


            }*/

        }

        private void PrintMatrix(float[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(" "+ matrix[i][j]+" ");
                }

                Console.WriteLine();
            }
        }

        
        public int NumberOfElements { get; set; }

        public float[] VectorX { get; set; }
        public float[][] Matrix { get; set; }

        public float[][] MatrixCopy { get; set; }

        
        private bool SwapRowWithZero(int i,int j,float accuracy)
        {
            for (int k = i+1; k < NumberOfElements; k++)
            {
                if (Math.Abs(Matrix[k][j]) > accuracy)
                {
                    Console.WriteLine("Before");
                    PrintMatrix(Matrix);
                    float[] tmp = Matrix[i];
                    Matrix[i] = Matrix[k];
                    Matrix[k] = tmp;
                    Console.WriteLine("After");
                    PrintMatrix(Matrix);
                    return true;
                }
            }

            return false;
        }
        
        
        public bool GaussElimination(float accuracy)
        {
            float multiplier = 0;
            float sumOfMultipliers = 0;
            //bool r = false;
            for (int i = 0; i < NumberOfElements-1; i++)
            {
                Console.WriteLine(i);
                for (int j = i+1; j < NumberOfElements; j++)
                {
                    
                    if (Math.Abs(Matrix[i][i]) < accuracy)
                    {
                        bool success = SwapRowWithZero(i, i, accuracy);
                        if (success==false)
                        {
                            VectorX[i] = 0;
                            break;
                        }

                    }

                    multiplier = (-1) * Matrix[j][i] / Matrix[i][ i];

                    for (int k = 0; k <= NumberOfElements; k++)
                    {
                        Matrix[j][k] += multiplier * Matrix[i][k];
                    }

                }
            }

            for (int i = NumberOfElements -1; i >= 0; i--)
            {
               // PrintMatrix();

                sumOfMultipliers = Matrix[i][NumberOfElements];

                for (int j = NumberOfElements -1; j >= i+1; j--)
                {
                    sumOfMultipliers -= Matrix[i][j] * VectorX[j];
                }

                if ( Math.Abs(Matrix[i][i]) > accuracy)
                {
                    VectorX[i] = sumOfMultipliers / Matrix[i][i];
                }
            }

            return true;
        }

        public void PrintResult(float[] result)
        {
            for (int i = 0; i < NumberOfElements ; i++)
            {
                int tmp = i+1;
                Console.WriteLine("x"+tmp+"=   "+result[i]);


            }

        }



        public float[] CalculateError()
        {
            float[] diff =  new float[Matrix.Length];
            float result = 0;
            for (int i = 0; i < Matrix.Length; i++)
            {
                
                for (int j = 0; j < Matrix[i].Length-1; j++)
                {
                    result += VectorX[j] * MatrixCopy[i][j];
                }

                diff[i] = MatrixCopy[i][Matrix.Length] - result;
                result = 0;
            }

            return diff;

        }



        /*
         public bool GaussElimination(int accuracy)
        {
            float multiplier = 0;
            float sumOfMultipliers = 0;
            //bool r = false;
            for (int i = 1; i < NumberOfElements-1; i++)
            {
                for (int j = i+1; j < NumberOfElements; j++)
                {
                    if (Matrix[i,i] < accuracy)
                    {
                        return false;
                    }

                    multiplier = (-1) * Matrix[j, i] / Matrix[i, i];

                    for (int k = i+1; k < NumberOfElements+1; k++)
                    {
                        Matrix[j, k] = Matrix[j, k] + multiplier * Matrix[i, k];
                    }

                }
            }

            for (int i = NumberOfElements; i > 1; i--)
            {
                sumOfMultipliers = Matrix[i, NumberOfElements + 1];

                for (int j = NumberOfElements; j > i+1; j--)
                {
                    sumOfMultipliers = sumOfMultipliers - Matrix[i, j] * VectorX[j];
                }

                if (Matrix[i,i] < accuracy)
                {
                    VectorX[i] = sumOfMultipliers / Matrix[i, i];
                }
            }

            return true;
        }
         
         */
    }
}
