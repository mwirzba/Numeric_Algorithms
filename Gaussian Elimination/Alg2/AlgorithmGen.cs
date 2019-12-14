using System;
using System.Linq.Expressions;
using static Alg2.DynamicArithmetic;

namespace Alg2
{

    class AlgorithmGen<T> where T : struct 
    {

        public int NumberOfElements { get; set; }
        public AlgorithmGen(int numberOfElements)
        {
            var x = Expression.Parameter(typeof(byte));
            this.NumberOfElements = numberOfElements;
        }

        public EnumsContainer.DataType GetTypeOfData<T>()
        {
            var z = typeof(T).Name;
            switch (typeof(T).Name)
            {
                case "Single":
                    return EnumsContainer.DataType.Float;
                case "Double":
                    return EnumsContainer.DataType.Double;
                default:
                    return EnumsContainer.DataType.FractionType;
            }
        }
        private void PrintMatrix(T[][] matrix)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                    Console.Write(" " + matrix[i][j] + " ");

                Console.WriteLine();
            }
        }
        public void PrintResult(T[] result)
        {
            for (var i = 0; i < NumberOfElements; i++)
            {
                var tmp = i + 1;
                Console.WriteLine("x" + tmp + "=   " + result[i]);
            }
        }

        public T[] GaussElimination(T[][] Matrix, T accuracy, EnumsContainer.GaussType gaussType, int[] indexArray = null )
        {
            T multiplier = default(T);
            T sumOfMultipliers = default(T);
            T[] VectorX =new T[NumberOfElements];
            switch (this.GetTypeOfData<T>())
            {
                case EnumsContainer.DataType.Float:
                    multiplier = (T)Convert.ChangeType(multiplier, typeof(float));
                    break;
                case EnumsContainer.DataType.Double:
                    multiplier = (T)Convert.ChangeType(multiplier, typeof(double));
                    break;
                case EnumsContainer.DataType.FractionType:
                    multiplier = (T)Convert.ChangeType(multiplier, typeof(FractionType));
                    break;
            }
           
            //bool r = false;
            for (var i = 0; i < NumberOfElements - 1; i++)
            {
                switch (gaussType)
                {
                    case EnumsContainer.GaussType.Normal:
                    {
                            if (Matrix[i][i].Equals(0))
                            {
                                var success = SwapRowWithZero(i, accuracy, Matrix);
                                if (success == false)
                                {
                                    //VectorX[i] = 0;
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
                            SwapWithMaxValue(i, Matrix, ref indexArray);
                            break;
                        }
                }

                dynamic minusOne;
                if (typeof(T) == typeof(FractionType))
                    minusOne = new FractionType {Denominator = 1, Numerator = -1};
                else
                    minusOne = -1;
                for (var j = i + 1; j < NumberOfElements; j++)
                {
                    multiplier = Divide<T>(Multiply<T>(minusOne, Matrix[j][i]), Matrix[i][i]);
                    for (var k = 0; k <= NumberOfElements; k++)
                    {
                        Matrix[j][k] = Add<T>(Matrix[j][k], Multiply<T>(multiplier, Matrix[i][k]));
                    }
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

                        sumOfMultipliers = Subtract<T>(sumOfMultipliers, Multiply<T>(Matrix[i][j], VectorX[indexArray[j]]));
                    }

                    if (CompareBigger(Abs(Matrix[i][i]), accuracy))
                    {
                        VectorX[indexArray[index0]] = Divide<T>(sumOfMultipliers, Matrix[i][i]);
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
                        sumOfMultipliers = Subtract<T>(sumOfMultipliers, Multiply<T>(Matrix[i][j], VectorX[j]));        
            
                    if (CompareBigger<T>(Abs(Matrix[i][i]),accuracy))
                        VectorX[i] = Divide<T>(sumOfMultipliers,Matrix[i][i]);
                }
            }

            return VectorX;
        }

        public T[] CalculateError(T[][] MatrixCopy, T[] VectorX)
        {
            switch (typeof(T).Name)
            {
                case "FractionType":
                {
                    var diff = new T[MatrixCopy.Length];
                    T result = new T();
                    for (var i = 0; i < MatrixCopy.Length; i++)
                    {

                        for (int j = 0; j < MatrixCopy[i].Length - 1; j++)
                        {
                            result = Add(result, Multiply(VectorX[j], MatrixCopy[i][j]));

                        }

                        diff[i] = Subtract(MatrixCopy[i][MatrixCopy.Length], result);
                        result =  new T();
                    }

                    return diff;
                }
                default:
                {
                    var diff = new T[MatrixCopy.Length];
                    T result = default(T);
                    for (var i = 0; i < MatrixCopy.Length; i++)
                    {

                        for (int j = 0; j < MatrixCopy[i].Length - 1; j++)
                        {
                            result = Add(result, Multiply(VectorX[j], MatrixCopy[i][j]));
                        }

                        diff[i] = Subtract(MatrixCopy[i][MatrixCopy.Length], result);
                        result = default(T);
                    }

                    return diff;
                }

            }


           
        }


        #region SwapMethods

        private bool SwapRowWithZero(int i, T accuracy, T[][] Matrix)
        {
            for (var k = i + 1; k < NumberOfElements; k++)
                if (CompareBigger(Abs(Matrix[k][i]),accuracy))
                {
                    var tmp = Matrix[i];
                    Matrix[i] = Matrix[k];
                    Matrix[k] = tmp;
                    return true;
                }

            return false;
        }

        private void SwapRowWithRowWithMaxValue(int i, T[][] Matrix)
        {
            T maxValue = Matrix[i][i];
            int position = i;
            for (var k = i + 1; k < NumberOfElements; k++)
            {
                if (CompareBigger(Matrix[k][i], maxValue))
                {
                    maxValue = Matrix[k][i];
                    position = k;
                }

            }
            var tmp = Matrix[i];
            Matrix[i] = Matrix[position];
            Matrix[position] = tmp;
        }
        private void SwapWithMaxValue(int i, T[][] Matrix, ref int[] indexTable)
        {
            T maxValue = Matrix[i][i];
            int rowPosition = i;
            int columnPosition = i;

            for (var k = i; k < NumberOfElements; k++)
            {
                for (int l = i; l < NumberOfElements; l++)
                {
                    if (CompareBigger(Matrix[k][i], maxValue))
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

        #endregion


        #region DynamicArithmetic

       


        #endregion




    }
}
