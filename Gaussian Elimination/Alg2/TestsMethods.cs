using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Alg2
{

    struct ResultSetdouble
    {
        public double doubleResultNormal;
        public double doubleResultPartial;
    }/*
    class TestsMethods
    {
        private int numberOfElements;
        int numberOfTests = 1;
        private Algorithm algorithm;
        private float[][] MatrixCopy;
        public DataSource DataSource { get; set; }
        public TestsMethods(int numberOfElements)
        {     
            DataSource = new DataSource(numberOfElements);
            this.numberOfElements = numberOfElements;
        }

        public void CheckQ1(int numberOfElements)
        {
            #region doubleType
            List<ResultSetdouble> resultsSetdouble = new List<ResultSetdouble>();

            for (int matrixSize = 10; matrixSize < numberOfElements; matrixSize += 10)
            {
                double normalRes = 0;
                double partialRes = 0;

                var dataSource = new DataSource(matrixSize);

                
                AlgorithmDouble algorithm = new AlgorithmDouble(matrixSize);
                double[][] MatrixCopy = new double[matrixSize][];

                double[][] Matrix = dataSource.GetMatrixWithDoubleData(matrixSize);
                for (var i = 0; i < Matrix.Length; i++)
                {
                    MatrixCopy[i] = new double[Matrix[i].Length];
                    Array.Copy(Matrix[i], MatrixCopy[i], Matrix[i].Length);
                }

                double[][] Matrix2 = new double[matrixSize][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix2[i] = new double[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix2[i], Matrix[i].Length);
                }

                double[][] Matrix3 = new double[matrixSize][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix3[i] = new double[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix3[i], Matrix[i].Length);
                }

                #region GaussTypeNormal
                double resNormalType = 0;
                var vectorResult = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
                if (vectorResult != null)
                {
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResult);


                    foreach (var variable in rtn)
                    {
                        normalRes += Math.Abs(variable);
                    }

                    normalRes /= rtn.Length;
                    Console.WriteLine(matrixSize+" "+ normalRes);

                }


                #endregion


                #region GaussPartialPivot


                resNormalType = 0;
                var vectorResultPartial = algorithm.GaussElimination(Matrix2, 0, EnumsContainer.GaussType.PartialPivot);
                if (vectorResultPartial != null)
                {
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResultPartial);


                    foreach (var variable in rtn)
                    {
                        partialRes += Math.Abs(variable);
                    }

                    partialRes /= rtn.Length;
                    Console.WriteLine(matrixSize + " " + partialRes);
                }

                resultsSetdouble.Add(new ResultSetdouble { doubleResultNormal = normalRes, doubleResultPartial = partialRes });
                #endregion

                /*
                #region GaussTypeFullPivot
                int[] indexArray = new int[matrixSize];
                for (int i = 0; i < matrixSize; i++)
                {
                    indexArray[i] = i;
                }

                double res3 = 0;
                var vectorResult3 = algorithm.GaussElimination(Matrix3, 0, EnumsContainer.GaussType.FullPivot, indexArray);
                if (vectorResult3 != null)
                {
                    algorithm.PrintResult(vectorResult3);
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

            WriteResultsToExcelFiledouble(resultsSetdouble);
            Console.WriteLine("done");


            #endregion
        }
        public float[][] CheckH2()
        {
            float[][] resultsComp = new float[3][];

            float[][] Matrix = DataSource.GetMatrixWithFloatData(numberOfElements);
            float[][] matrixCopy = DataSource.GetMatrixWithFloatData(numberOfElements);
            float resNormalType = 0;
            algorithm = new Algorithm(numberOfElements);


            var vectorFloatNormal = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
            resultsComp[0] = algorithm.CalculateError(matrixCopy, vectorFloatNormal);

            Matrix = DataSource.GetMatrixWithFloatData(numberOfElements);
            var vectorFloatPartial = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.PartialPivot);
            resultsComp[1] = algorithm.CalculateError(matrixCopy, vectorFloatPartial);

            int[] indexArray = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            Matrix = DataSource.GetMatrixWithFloatData(numberOfElements);
            var vectorFloatFull = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.FullPivot, indexArray);
            resultsComp[2] = algorithm.CalculateError(matrixCopy, vectorFloatFull);

            return resultsComp;
        }

     


        public void CheckH3(int numberOfElements)
        {
            #region Fractiontype
            long[] algTimes = new long[3];

            AlghorithmWithSpecialType algorithmFracType = new AlghorithmWithSpecialType(numberOfElements);
            algorithm = new Algorithm(numberOfElements);

            for (int pf = 0; pf < numberOfTests; pf++)
            {
                FractionType[][] MatrixFracType = DataSource.GetMatrixWithSpecialTypeData(numberOfElements);
                FractionType[][] MatrixFracTypeCopy = DataSource.GetMatrixWithSpecialTypeData(numberOfElements);

                float[][] matrixCopy = DataSource.GetMatrixWithFloatData(numberOfElements);


                //FractionType[] VectorXFracType = new FractionType[numberOfElements];
                
                FractionType[][] MatrixCopyFracType = new FractionType[numberOfElements][];
                for (var i = 0; i < MatrixFracType.Length; i++)
                {
                    MatrixCopyFracType[i] = new FractionType[MatrixFracType[i].Length];
                    Array.Copy(MatrixFracType[i], MatrixCopyFracType[i], MatrixFracType[i].Length);
                }

                FractionType[][] Matrix2FracType = new FractionType[numberOfElements][];
                for (var i = 0; i < MatrixFracType.Length; i++)
                {
                    Matrix2FracType[i] = new FractionType[MatrixFracType[i].Length];
                    Array.Copy(MatrixFracType[i], Matrix2FracType[i], MatrixFracType[i].Length);
                }

                FractionType[][] Matrix3FracType = new FractionType[numberOfElements][];
                for (var i = 0; i < MatrixFracType.Length; i++)
                {
                    Matrix3FracType[i] = new FractionType[MatrixFracType[i].Length];
                    Array.Copy(MatrixFracType[i], Matrix3FracType[i], MatrixFracType[i].Length);
                }

                #region GaussTypeNormal
                FractionType res1 = new FractionType();

                //get alg time
                var vectorResultFracType =
                    algorithmFracType.GaussElimination(MatrixFracType, 0, EnumsContainer.GaussType.Normal);

                var varctorResultFracTypeToFloat = new float[numberOfElements];
                if (vectorResultFracType != null)
                {
                    for (int m = 0; m < vectorResultFracType.Length; m++)
                    {
                        while (vectorResultFracType[m].Numerator.ToString().Length > 9 || vectorResultFracType[m].Denominator.ToString().Length > 9)
                        {
                            vectorResultFracType[m].Numerator /= 10;
                            vectorResultFracType[m].Denominator /= 10;

                        }
                        varctorResultFracTypeToFloat[m] = (float)((float)vectorResultFracType[m].Numerator / (float)vectorResultFracType[m].Denominator);
                    }

                    var rtnFracTypeError = algorithmFracType.CalculateError(MatrixFracTypeCopy, vectorResultFracType);
                    algorithmFracType.PrintResult(rtnFracTypeError);
                }

                #endregion


                #region GaussPartialPivot

                var vectorResult2 = algorithmFracType.GaussElimination(Matrix2FracType, 0, EnumsContainer.GaussType.PartialPivot);
                var verctorResultFracTypePartialPivot = new float[numberOfElements];
                if (vectorResult2 != null)
                {
                    for (int m = 0; m < vectorResult2.Length; m++)
                    {
                        while (vectorResult2[m].Numerator.ToString().Length > 9 || vectorResult2[m].Denominator.ToString().Length > 9)
                        {
                            vectorResult2[m].Numerator /= 10;
                            vectorResult2[m].Denominator /= 10;

                        }
                        verctorResultFracTypePartialPivot[m] = (float)((float)vectorResult2[m].Numerator / (float)vectorResult2[m].Denominator);
                    }

                    var rtnFracTypeError = algorithmFracType.CalculateError(MatrixFracTypeCopy, vectorResultFracType);
                    algorithmFracType.PrintResult(rtnFracTypeError);
                }


                #endregion


                #region GaussTypeFullPivot


                int[] indexArray = new int[numberOfElements];
                for (int i = 0; i < numberOfElements; i++)
                {
                    indexArray[i] = i;
                }
                var vectorResult3 = algorithmFracType.GaussElimination(Matrix3FracType, 0, EnumsContainer.GaussType.FullPivot, indexArray);
                var verctorResultFracTypeFullpivot = new float[numberOfElements];
                if (vectorResult3 != null)
                {
                    for (int m = 0; m < vectorResult3.Length; m++)
                    {
                        while (vectorResult3[m].Numerator.ToString().Length > 9 || vectorResult3[m].Denominator.ToString().Length > 9)
                        {
                            vectorResult3[m].Numerator /= 10;
                            vectorResult3[m].Denominator /= 10;
                        }
                        verctorResultFracTypeFullpivot[m] = (float)((float)vectorResult3[m].Numerator / (float)vectorResult3[m].Denominator);
                    }

                    var rtnFracTypeError = algorithmFracType.CalculateError(MatrixFracTypeCopy, vectorResultFracType);
                    algorithmFracType.PrintResult(rtnFracTypeError);
                }


                #endregion


            }

            #endregion
        }

        public void FloatTypeTest(int numberOfElements)
        {
            #region FloatType
            List<ResultSetdouble> resultsSetFloat =  new List<ResultSetdouble>();

            for (int matrixSize = 10; matrixSize < numberOfElements; matrixSize += 10)
            {
                float normalRes = 0;
                float partialRes = 0;

                var dataSource = new DataSource(matrixSize);

                algorithm = new Algorithm(matrixSize);
                MatrixCopy = new float[matrixSize][];

                float[][] Matrix = dataSource.GetMatrixWithFloatData(matrixSize);
                for (var i = 0; i < Matrix.Length; i++)
                {
                    MatrixCopy[i] = new float[Matrix[i].Length];
                    Array.Copy(Matrix[i], MatrixCopy[i], Matrix[i].Length);
                }

                float[][] Matrix2 = new float[matrixSize][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix2[i] = new float[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix2[i], Matrix[i].Length);
                }

                float[][] Matrix3 = new float[matrixSize][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix3[i] = new float[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix3[i], Matrix[i].Length);
                }

                #region GaussTypeNormal
                float resNormalType = 0;
                var vectorResult = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
                if (vectorResult != null)
                {
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResult);


                    foreach (var variable in rtn)
                    {
                        normalRes += Math.Abs(variable);
                    }

                    normalRes /= rtn.Length;
                    Console.WriteLine(normalRes);

                }


                #endregion


                #region GaussPartialPivot


                resNormalType = 0;
                var vectorResultPartial = algorithm.GaussElimination(Matrix2, 0, EnumsContainer.GaussType.PartialPivot);
                if (vectorResultPartial != null)
                {
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResultPartial);


                    foreach (var variable in rtn)
                    {
                        partialRes += Math.Abs(variable);
                    }

                    partialRes /= rtn.Length;
                    Console.WriteLine(partialRes);
                }

                resultsSetFloat.Add(new ResultSetdouble { doubleResultNormal = normalRes ,doubleResultPartial = partialRes});
                #endregion

                /*
                #region GaussTypeFullPivot
                int[] indexArray = new int[matrixSize];
                for (int i = 0; i < matrixSize; i++)
                {
                    indexArray[i] = i;
                }

                float res3 = 0;
                var vectorResult3 = algorithm.GaussElimination(Matrix3, 0, EnumsContainer.GaussType.FullPivot, indexArray);
                if (vectorResult3 != null)
                {
                    algorithm.PrintResult(vectorResult3);
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

            WriteResultsToExcelFiledouble(resultsSetFloat);
            Console.WriteLine("done");


            #endregion
        }

        public void DoubleTypeTest()
        {
            #region DoubleType

            AlgorithmDouble algorithmDouble = new AlgorithmDouble(numberOfElements);
            var MatrixCopyDouble = new Double[numberOfElements][];

            for (int p = 0; p < numberOfTests; p++)
            {

                double[][] Matrix = DataSource.GetMatrixWithDoubleData(numberOfElements);
                for (var i = 0; i < Matrix.Length; i++)
                {
                    MatrixCopyDouble[i] = new double[Matrix[i].Length];
                    Array.Copy(Matrix[i], MatrixCopyDouble[i], Matrix[i].Length);
                }

                double[][] Matrix2 = new double[numberOfElements][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix2[i] = new double[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix2[i], Matrix[i].Length);
                }

                double[][] Matrix3 = new double[numberOfElements][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix3[i] = new double[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix3[i], Matrix[i].Length);
                }


                #region GaussTypeNormal


                double resNormalType = 0;
                var vectorResult = algorithmDouble.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
                if (vectorResult != null)
                {
                    Console.WriteLine("Res:");
                    algorithmDouble.PrintResult(vectorResult);
                    var rtn = algorithmDouble.CalculateError(MatrixCopyDouble, vectorResult);
                    Console.WriteLine();
                    algorithmDouble.PrintResult(rtn);


                    foreach (var variable in rtn)
                    {
                        Console.WriteLine();
                        resNormalType += variable;
                    }

                }

                #endregion

                /*
                #region GaussPartialPivot

                double res2 = 0;
                var vectorResult2 = algorithmDouble.GaussElimination(Matrix2, 0, EnumsContainer.GaussType.PartialPivot);
                if (vectorResult2 != null)
                {
                    // algorithmDouble.PrintResult(algorithmDouble.VectorX);
                    var rtn = algorithmDouble.CalculateError(MatrixCopyDouble, vectorResult2);
                    algorithmDouble.PrintResult(vectorResult2);

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

                double res3 = 0;
                var vectorResult3 = algorithmDouble.GaussElimination(Matrix3, 0, EnumsContainer.GaussType.FullPivot, indexArray);
                if (vectorResult3 != null)
                {
                   // algorithmDouble.PrintResult(algorithmDouble.VectorX);
                    var rtn = algorithmDouble.CalculateError(MatrixCopyDouble, vectorResult3);
                    Console.WriteLine();
                    algorithmDouble.PrintResult(vectorResult3);

                    foreach (var variable in rtn)
                    {
                        res3 += variable;
                    }
                    Console.WriteLine("Result:" + Math.Abs(res3));
                }

                #endregion
                
            }

            #endregion
        }

        public long[] CheckQ2(int numberOfElements)
        {
            long[] algTimes = new long[2];
            float[][] Matrix = DataSource.GetMatrixWithFloatData(numberOfElements);
            float resNormalType = 0;
            algorithm = new Algorithm(numberOfElements);

      
            var watch = Stopwatch.StartNew();
            var vectorFloatNormal = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[0] = watch.ElapsedMilliseconds;


            AlgorithmDouble algorithmDouble = new AlgorithmDouble(numberOfElements);
            double[][] matrixDouble = DataSource.GetMatrixWithDoubleData(numberOfElements);

            watch = Stopwatch.StartNew();
            var vectorDoubleNormal = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[1] = watch.ElapsedMilliseconds;

            return algTimes;
        }

        public long[] CheckE1(int numberOfElements)
        {
            long[] algTimes = new long[6];
            float[][] Matrix = DataSource.GetMatrixWithFloatData(numberOfElements);
            float resNormalType = 0;
            algorithm = new Algorithm(numberOfElements);

            var watch = Stopwatch.StartNew();
            var vectorFloatNormal = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[0] = watch.ElapsedMilliseconds;

            Matrix = DataSource.GetMatrixWithFloatData(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorFloatPartial = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.PartialPivot);
            watch.Stop();
            algTimes[1] = watch.ElapsedMilliseconds;


            int[] indexArray = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            Matrix = DataSource.GetMatrixWithFloatData(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorFloatFull = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.FullPivot,indexArray);
            watch.Stop();
            algTimes[2] = watch.ElapsedMilliseconds;



            AlgorithmDouble algorithmDouble = new AlgorithmDouble(numberOfElements);
            double[][] matrixDouble = DataSource.GetMatrixWithDoubleData(numberOfElements);

            watch = Stopwatch.StartNew();
            var vectorDoubleNormal = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[3] = watch.ElapsedMilliseconds;

            matrixDouble = DataSource.GetMatrixWithDoubleData(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorDoublePartial = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.PartialPivot);
            watch.Stop();
            algTimes[4] = watch.ElapsedMilliseconds;

            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            matrixDouble = DataSource.GetMatrixWithDoubleData(numberOfElements);
            watch = System.Diagnostics.Stopwatch.StartNew();
            var vectorDoubleFull = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.FullPivot,indexArray);
            watch.Stop();
            algTimes[5] = watch.ElapsedMilliseconds;

            /*
            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            MatrixFracType = DataSource.GetMatrixWithSpecialTypeData(numberOfElements);
            watch = System.Diagnostics.Stopwatch.StartNew();
            var vectorFracTypePartial = algorithmFracType.GaussElimination(MatrixFracType, 0, EnumsContainer.GaussType.PartialPivot);
            watch.Stop();
            algTimes[7] =  watch.ElapsedMilliseconds;

            MatrixFracType = DataSource.GetMatrixWithSpecialTypeData(numberOfElements);
            watch = System.Diagnostics.Stopwatch.StartNew();
            var vectorFracTypeFull = algorithmFracType.GaussElimination(MatrixFracType, 0, EnumsContainer.GaussType.FullPivot,indexArray);
            watch.Stop();
            algTimes[8] = watch.ElapsedMilliseconds;
            

            return algTimes;
        }


        public static FractionType GetFractionTypeAbs(FractionType type)
        {
            if (type.Numerator < 0)
                return new FractionType { Numerator = type.Numerator * -1, Denominator = type.Denominator };
            return type;

        }

        public static void WriteResultsToExcelFiledouble(List<ResultSetdouble> results)
        {
            using (StreamWriter file = File.AppendText(Directory.GetCurrentDirectory() + "//results1.csv"))
            {
                file.WriteLine("Normal;Partial");
                foreach (var result in results)
                {
                    file.WriteLine(result.doubleResultNormal + ";" + result.doubleResultPartial);
                }
                file.WriteLine();
            }

        }

        public static void WriteResultsToExcelFileQ2Result(List<Program.Q2TimesResults> results)
        {
            using (StreamWriter file = File.AppendText(Directory.GetCurrentDirectory() + "//resultsQ2.csv"))
            {
                file.WriteLine("Float;Double");
                foreach (var result in results)
                {
                    file.WriteLine(result.floutTime + ";" + result.doubleTime);
                }
                file.WriteLine();
            }

        }


    }*/
}
