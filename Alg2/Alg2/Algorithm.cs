using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    class Algorithm
    {
        public int NumberOfElements { get; set; }
      //  public float[] VectorX { get; set; }
       // public float[][] Matrix { get; set; }

       // public float[][] MatrixCopy { get; set; }

        public Algorithm(int numberOfElements,EnumsContainer.DataType type)
        {

            switch (type)
            {
                case EnumsContainer.DataType.Double:
                    break;
                case EnumsContainer.DataType.Float:
                    break;

            }

            NumberOfElements = numberOfElements;
            //VectorX = new float[NumberOfElements];

        }

        /*
        private float[][] CopyArray(float[][] matrix)
        {
            MatrixCopy = new float[NumberOfElements][];
            for (var i = 0; i < matrix.Length; i++)
            {
                MatrixCopy[i] = new float[matrix[i].Length];
                Array.Copy(matrix[i], MatrixCopy[i], matrix[i].Length);
            }

            return  
        }
        */
        private void PrintMatrix(float[][] matrix)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++) Console.Write(" "+ matrix[i][j]+" ");

                Console.WriteLine();
            }
        }

        private bool SwapRowWithZero(int i,float accuracy,float[][] Matrix)
        {
            for (var k = i+1; k < NumberOfElements; k++)
                if (Math.Abs(Matrix[k][i]) > accuracy)
                {
                    //Console.WriteLine("Before");
                    //PrintMatrix(Matrix);
                    var tmp = Matrix[i];
                    Matrix[i] = Matrix[k];
                    Matrix[k] = tmp;
                   // Console.WriteLine("After");
                    //PrintMatrix(Matrix);
                    return true;
                }

            return false;
        }

        private void SwapRowWithRowWithMaxValue(int i,float[][] Matrix)
        {
            float maxValue = Matrix[i][i];
            int position = i;
            for (var k = i + 1; k < NumberOfElements; k++)
            {
                if (maxValue < Matrix[k][i])
                {
                    maxValue = Matrix[k][i];
                    position = k;
                }
           
            }
            //PrintMatrix(Matrix);
            var tmp = Matrix[i];
            Matrix[i] = Matrix[position];
            Matrix[position] = tmp;
            //Console.WriteLine();
            //PrintMatrix(Matrix);
            //Console.WriteLine();
        }


        private void SwapWithhMaxValue(int i,float[][] Matrix,ref int[] indexTable)
        {
            float maxValue = Matrix[i][i];
            int rowPosition = i;
            int columnPosition = i;

            for (var k = i ; k < NumberOfElements; k++)
            {
                for (int l = i; l < NumberOfElements; l++)
                {
                    if (maxValue < Matrix[k][l])
                    {
                        maxValue = Matrix[k][l];
                        rowPosition = k;
                        columnPosition = l; 
                    }

                }
               

            }
            //swap rows

           // PrintMatrix(Matrix);
            var tmp = Matrix[i];
            Matrix[i] = Matrix[rowPosition];
            Matrix[rowPosition] = tmp;

            int index = indexTable[i];
            indexTable[i] = indexTable[columnPosition];
            indexTable[columnPosition] = index;

            for (int k = 0; k < NumberOfElements; k++)
            {
                var cellValue = Matrix[k][i];
                Matrix[k][i] = Matrix[k][columnPosition];
                Matrix[k][columnPosition] = cellValue;
            }
            Console.WriteLine();
           // PrintMatrix(Matrix);
            Console.WriteLine();

            Console.WriteLine(indexTable[0]);
            Console.WriteLine(indexTable[1]);
            Console.WriteLine(indexTable[2]);
            Console.WriteLine(indexTable[3]);
        }

        public float[] GaussElimination(float[][] Matrix, float accuracy,EnumsContainer.GaussType gaussType,int[] indexArray = null)
        {
            float multiplier = 0;
            float sumOfMultipliers = 0;
            float[] VectorX =  new float[NumberOfElements];
            //bool r = false;
            for (var i = 0; i < NumberOfElements-1; i++)
            {
                switch (gaussType)
                {
                    case EnumsContainer.GaussType.Normal:
                    {
                        if (Math.Abs(Matrix[i][i]) < accuracy)
                        {
                            var success = SwapRowWithZero(i,accuracy,Matrix);
                            if (success == false)
                            {
                                VectorX[i] = 0;
                                break;
                            }

                        }
                        break;
                    }
                    case EnumsContainer.GaussType.PartialPivot:
                    {
                        SwapRowWithRowWithMaxValue(i, Matrix);
                        break;
                    }
                    case EnumsContainer.GaussType.FullPivot:
                    {
                        SwapWithhMaxValue(i,Matrix,ref indexArray);
                        break;
                    }
                }

                for (var j = i+1; j < NumberOfElements; j++)
                {

                    multiplier = -1 * Matrix[j][i] / Matrix[i][ i];

                    for (var k = 0; k <= NumberOfElements; k++)
                        Matrix[j][k] += multiplier * Matrix[i][k];
                    
                }

                //Console.WriteLine("MATRIX");
                //PrintMatrix(Matrix);
                //Console.WriteLine("==========");
            }

            var dupaaa = 3;
            if (gaussType == EnumsContainer.GaussType.FullPivot)
            {
                for (var i = NumberOfElements - 1; i >= 0; i--)
                {

                    sumOfMultipliers = Matrix[i][NumberOfElements];
                    Console.WriteLine(sumOfMultipliers);
                    for (var j = NumberOfElements - 1; j >= i + 1; j--)
                    {
                        sumOfMultipliers -= Matrix[i][j] * VectorX[indexArray[j]];
                        Console.WriteLine(sumOfMultipliers);
                    }

                    if (Math.Abs(Matrix[i][i]) > accuracy)
                    {
                        VectorX[indexArray[dupaaa]] = sumOfMultipliers / Matrix[i][i];
                        dupaaa--;
                    }

                    //Console.WriteLine("RESULT:");
                    //PrintResult(VectorX);
                    //Console.WriteLine("============");
                }
            }
            else
            {
                for (var i = NumberOfElements - 1; i >= 0; i--)
                {
                    sumOfMultipliers = Matrix[i][NumberOfElements];

                    for (var j = NumberOfElements - 1; j >= i + 1; j--)
                        sumOfMultipliers -= Matrix[i][j] * VectorX[j];

                    if (Math.Abs(Matrix[i][i]) > accuracy)
                        VectorX[i] = sumOfMultipliers / Matrix[i][i];
                }
            }
            
            return VectorX;
        }

        public void PrintResult(float[] result)
        {
            for (var i = 0; i < NumberOfElements ; i++)
            {
                var tmp = i+1;
                Console.WriteLine("x"+tmp+"=   "+result[i]);
            }

        }

        public float[] CalculateError(float[][] MatrixCopy,float[] VectorX)
        {
            var diff =  new float[MatrixCopy.Length];
            float result = 0;
            for (var i = 0; i < MatrixCopy.Length; i++)
            {
                
                for (int j = 0; j < MatrixCopy[i].Length-1; j++)
                {
                    result += VectorX[j] * MatrixCopy[i][j];
                }

                diff[i] = MatrixCopy[i][MatrixCopy.Length] - result;
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
