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
               new float[] { 4,-2,4,-2,8 }, new float[] {3,1,4,2,7 },new float[] {2,4,2,1,10},new float[] {2,-2,4,2,2}
            };          
            //new float[,] { { 4,-2,4,-2,8 },{3,1,4,2,7 }, {2,4,2,1,10}, {2,-2,4,2,2} };                
            //new float[NumberOfElements,NumberOfElements+1];
            VectorX = new float[NumberOfElements];

           /* for (int i = 0; i < numberOfElements; i++)
            {
                Console.WriteLine(Matrix[][0]);
             


            }*/

        }

        private void PrintMatrix()
        {
            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[i].Length; j++)
                {
                    Console.Write(" "+Matrix[i][j]+" ");
                }

                Console.WriteLine();
            }
        }

        
        public int NumberOfElements { get; set; }

        public float[] VectorX { get; set; }
        public float[][] Matrix { get; set; }

        
        private bool SwapRowWithZero(int i,int j,float accuracy)
        {
            for (int k = i+1; k < NumberOfElements; k++)
            {
                if (Math.Abs(Matrix[k][j]) > accuracy)
                {
                    Console.WriteLine("Before");
                    PrintMatrix();
                    float[] tmp = Matrix[i];
                    Matrix[i] = Matrix[k];
                    Matrix[k] = tmp;
                    Console.WriteLine("After");
                    PrintMatrix();
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
                           // VectorX[i] = 0;
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
                PrintMatrix();

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

        public void PrintResult()
        {
            for (int i = 0; i < NumberOfElements ; i++)
            {
                Console.WriteLine("x"+i+1+"=   "+VectorX[i]);
            }

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
