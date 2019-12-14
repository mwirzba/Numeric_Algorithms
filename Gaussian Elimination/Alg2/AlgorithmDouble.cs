using System;

namespace Alg2
{
    class AlgorithmDouble
    {
        public int NumberOfElements { get; set; }
        public AlgorithmDouble(int numberOfElements)
        {
            NumberOfElements = numberOfElements;
        }
        private void PrintMatrix(double[][] matrix)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++) Console.Write(" " + matrix[i][j] + " ");

                Console.WriteLine();
            }
        }

        private bool SwapRowWithZero(int i, double accuracy, double[][] Matrix)
        {
            for (var k = i + 1; k < NumberOfElements; k++)
                if (Math.Abs(Matrix[k][i]) > accuracy)
                {
                    var tmp = Matrix[i];
                    Matrix[i] = Matrix[k];
                    Matrix[k] = tmp;
                    return true;
                }

            return false;
        }

        private void SwapRowWithRowWithMaxValue(int i, double[][] Matrix)
        {
            double maxValue = Matrix[i][i];
            int position = i;
            for (var k = i + 1; k < NumberOfElements; k++)
            {
                if (maxValue < Matrix[k][i])
                {
                    maxValue = Matrix[k][i];
                    position = k;
                }

            }
            var tmp = Matrix[i];
            Matrix[i] = Matrix[position];
            Matrix[position] = tmp;
        }


        private void SwapWithhMaxValue(int i, double[][] Matrix, ref int[] indexTable)
        {
            double maxValue = Matrix[i][i];
            int rowPosition = i;
            int columnPosition = i;

            for (var k = i; k < NumberOfElements; k++)
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
        }

        public double[] GaussElimination(double[][] Matrix, double accuracy, EnumsContainer.GaussType gaussType, int[] indexArray = null)
        {
            double multiplier = 0;
            double sumOfMultipliers = 0;
            double[] VectorX = new double[NumberOfElements];
            //bool r = false;
            for (var i = 0; i < NumberOfElements - 1; i++)
            {
                switch (gaussType)
                {
                    case EnumsContainer.GaussType.Normal:
                        {
                            if (Matrix[i][i] == 0.0d)
                            {
                                var success = SwapRowWithZero(i, accuracy, Matrix);
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
                            SwapWithhMaxValue(i, Matrix, ref indexArray);
                            break;
                        }
                }

                for (var j = i + 1; j < NumberOfElements; j++)
                {

                    multiplier = -1 * Matrix[j][i] / Matrix[i][i];

                    for (var k = 0; k <= NumberOfElements; k++)
                        Matrix[j][k] += multiplier * Matrix[i][k];

                }
            }

            int index0 = VectorX.Length - 1;
            if (gaussType == EnumsContainer.GaussType.FullPivot)
            {
                for (var i = NumberOfElements - 1; i >= 0; i--)
                {

                    sumOfMultipliers = Matrix[i][NumberOfElements];
                    for (var j = NumberOfElements - 1; j >= i + 1; j--)
                    {
                        sumOfMultipliers -= Matrix[i][j] * VectorX[indexArray[j]];
                    }

                    if (Math.Abs(Matrix[i][i]) > accuracy)
                    {
                        VectorX[indexArray[index0]] = sumOfMultipliers / Matrix[i][i];
                        index0--;
                    }
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

        public void PrintResult(double[] result)
        {
            for (var i = 0; i < NumberOfElements; i++)
            {
                var tmp = i + 1;
                Console.WriteLine("x" + tmp + "=   " + result[i]);
            }

        }

        public double[] CalculateError(double[][] MatrixCopy, double[] VectorX)
        {
            var diff = new double[MatrixCopy.Length];
            double result = 0;
            for (var i = 0; i < MatrixCopy.Length; i++)
            {

                for (int j = 0; j < MatrixCopy[i].Length - 1; j++)
                {
                    result += VectorX[j] * MatrixCopy[i][j];
                }

                diff[i] = MatrixCopy[i][MatrixCopy.Length] - result;
                result = 0;
            }
            return diff;
        }

    }
}
