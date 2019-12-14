using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static Alg2.DynamicArithmetic;

namespace Alg2
{
    public class TestMethodsGen
    {
        public TestMethodsGen(int numberOfElements)
        {
            DataSource = new DataSource(numberOfElements);
            NumberOfElements = numberOfElements;
        }

        public DataSource DataSource { get; set; }
        public int NumberOfElements { get; set; }
        public void CheckQ1<T>(int numberOfElements) where  T : struct
        {
            List<ResultSet<T>> resultsSetdouble = new List<ResultSet<T>>();
            for (int matrixSize = 10; matrixSize < numberOfElements; matrixSize += 10)
            {
                T normalRes = default(T);
                T partialRes = default(T);

                var dataSource = new DataSource(matrixSize);


                var algorithm = new AlgorithmGen<T>(matrixSize);
                T[][] Matrix = dataSource.GetMatrixData<T>(matrixSize);
                T[][] MatrixCopy  = dataSource.GetMatrixData<T>(matrixSize);

                T[][] Matrix2 = dataSource.GetMatrixData<T>(matrixSize);


                double[][] Matrix3 = new double[matrixSize][];
                for (var i = 0; i < Matrix.Length; i++)
                {
                    Matrix3[i] = new double[Matrix[i].Length];
                    Array.Copy(Matrix[i], Matrix3[i], Matrix[i].Length);
                }

                #region GaussTypeNormal
                dynamic resNormalType = 0;
                var vectorResult = algorithm.GaussElimination(Matrix,resNormalType, EnumsContainer.GaussType.Normal);
                if (vectorResult != null)
                {
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResult);


                    foreach (var variable in rtn)
                    {
                        normalRes += Math.Abs(variable);
                    }

                    normalRes /= rtn.Length;
                }


                #endregion


                #region GaussPartialPivot


                resNormalType = 0;
                var vectorResultPartial = algorithm.GaussElimination(Matrix2,resNormalType, EnumsContainer.GaussType.PartialPivot);
                if (vectorResultPartial != null)
                {
                    var rtn = algorithm.CalculateError(MatrixCopy, vectorResultPartial);
                    foreach (var variable in rtn)
                    {
                        partialRes = Add(partialRes,Abs(variable));
                    }

                    dynamic rtnLength = rtn.Length;
                    partialRes = Divide<T>(partialRes,rtnLength);
                }

               

                resultsSetdouble.Add(new ResultSet<T> { ResultNormal = normalRes, ResultPartial = partialRes });
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
                */
            }

            WriteResultsToExcelFile(resultsSetdouble);
            Console.WriteLine("done");

        }

        public long[] CheckQ2(int numberOfElements)
        {
            long[] algTimes = new long[2];
            float[][] Matrix = DataSource.GetMatrixData<float>(numberOfElements);
            float resNormalType = 0;
            AlgorithmGen<float> algorithm = new AlgorithmGen<float>(numberOfElements);


            var watch = Stopwatch.StartNew();
            var vectorFloatNormal = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[0] = watch.ElapsedMilliseconds;


            AlgorithmGen<double> algorithmDouble = new AlgorithmGen<double>(numberOfElements);
            double[][] matrixDouble = DataSource.GetMatrixData<double>(numberOfElements);

            watch = Stopwatch.StartNew();
            var vectorDoubleNormal = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[1] = watch.ElapsedMilliseconds;

            return algTimes;
        }


        public long[] CheckH1(int numberOfElements)
        {
            long[] algTimes = new long[3];

            var Matrix = DataSource.GetMatrixData<float>(numberOfElements);
            var Matrix2 = DataSource.GetMatrixData<float>(numberOfElements);
            var Matrix3 = DataSource.GetMatrixData<float>(numberOfElements);


            float resNormalType = 0;
            var algorithm = new AlgorithmGen<float>(numberOfElements);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var vectorFloatNormal = algorithm.GaussElimination(Matrix, 0, EnumsContainer.GaussType.Normal);
            timer.Stop();
            algTimes[0] = timer.ElapsedMilliseconds;

            timer.Reset();
            timer.Start();
            var vectorFloatPartial = algorithm.GaussElimination(Matrix2, 0, EnumsContainer.GaussType.Normal);
            timer.Stop();
            algTimes[1] = timer.ElapsedMilliseconds;


            int[] indexArray = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            timer.Reset();
            timer.Start();
            var vectorFloatFull = algorithm.GaussElimination(Matrix3, 0, EnumsContainer.GaussType.Normal);
            timer.Stop();
            algTimes[2] = timer.ElapsedMilliseconds;

            return algTimes;
        }

        public long[] CheckE1(int numberOfElements)
        {
            long[] algTimes = new long[9];
            AlgorithmGen<float> algorithm = new AlgorithmGen<float>(numberOfElements);
            float[][] matrixFloat = DataSource.GetMatrixData<float>(numberOfElements);
            var watch = Stopwatch.StartNew();
            var vectorFloatNormal = algorithm.GaussElimination(matrixFloat, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[0] = watch.ElapsedMilliseconds;

            matrixFloat = DataSource.GetMatrixData<float>(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorFloatPartial = algorithm.GaussElimination(matrixFloat, 0, EnumsContainer.GaussType.PartialPivot);
            watch.Stop();
            algTimes[1] = watch.ElapsedMilliseconds;


            int[] indexArray = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            matrixFloat = DataSource.GetMatrixData<float>(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorFloatFull = algorithm.GaussElimination(matrixFloat, 0, EnumsContainer.GaussType.FullPivot, indexArray);
            watch.Stop();
            algTimes[2] = watch.ElapsedMilliseconds;



            AlgorithmGen<double> algorithmDouble = new AlgorithmGen<double>(numberOfElements);
            double[][] matrixDouble = DataSource.GetMatrixData<double>(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorDoubleNormal = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[3] = watch.ElapsedMilliseconds;

            matrixDouble = DataSource.GetMatrixData<double>(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorDoublePartial = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.PartialPivot);
            watch.Stop();
            algTimes[4] = watch.ElapsedMilliseconds;

            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            matrixDouble = DataSource.GetMatrixData<double>(numberOfElements);
            watch = System.Diagnostics.Stopwatch.StartNew();
            var vectorDoubleFull = algorithmDouble.GaussElimination(matrixDouble, 0, EnumsContainer.GaussType.FullPivot, indexArray);
            watch.Stop();
            algTimes[5] = watch.ElapsedMilliseconds;

            
            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            AlgorithmGen<FractionType> algorithmFracType = new AlgorithmGen<FractionType>(numberOfElements);
            var matrixFracType = DataSource.GetMatrixData<FractionType>(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorFracTypeNormal = algorithmFracType.GaussElimination(matrixFracType, new FractionType(), EnumsContainer.GaussType.Normal);
            watch.Stop();
            algTimes[7] = watch.ElapsedMilliseconds;

            algorithmFracType = new AlgorithmGen<FractionType>(numberOfElements);
            matrixFracType = DataSource.GetMatrixData<FractionType>(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorFracTypePartial = algorithmFracType.GaussElimination(matrixFracType,new FractionType(),EnumsContainer.GaussType.PartialPivot);
            watch.Stop();
            algTimes[7] =  watch.ElapsedMilliseconds;

            matrixFracType = DataSource.GetMatrixData<FractionType>(numberOfElements);
            watch = Stopwatch.StartNew();
            var vectorFracTypeFull = algorithmFracType.GaussElimination(matrixFracType, new FractionType(),EnumsContainer.GaussType.FullPivot,indexArray);
            watch.Stop();
            algTimes[8] = watch.ElapsedMilliseconds;

            return algTimes;
        }

        public float[][] CheckH2(int numberOfElements)
        {
            float[][] resultsComp = new float[3][];
            float[][] matrix = DataSource.GetMatrixData<float>(numberOfElements);
            float[][] matrixCopy = DataSource.GetMatrixData<float>(numberOfElements);
            float resNormalType = 0;

            var algorithmFloat = new AlgorithmGen<float>(numberOfElements);

            var vectorFloatNormal = algorithmFloat.GaussElimination(matrix, 0, EnumsContainer.GaussType.Normal);
            resultsComp[0] = algorithmFloat.CalculateError(matrixCopy, vectorFloatNormal);

            matrix = DataSource.GetMatrixData<float>(numberOfElements);
            var vectorFloatPartial = algorithmFloat.GaussElimination(matrix, 0, EnumsContainer.GaussType.PartialPivot);
            resultsComp[1] = algorithmFloat.CalculateError(matrixCopy, vectorFloatPartial);

            int[] indexArray = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                indexArray[i] = i;
            }

            matrix = DataSource.GetMatrixData<float>(numberOfElements);
            var vectorFloatFull = algorithmFloat.GaussElimination(matrix, 0, EnumsContainer.GaussType.FullPivot, indexArray);
            resultsComp[2] = algorithmFloat.CalculateError(matrixCopy, vectorFloatFull);

            return resultsComp;
        }

        public void CheckH3(int numberOfElements,int numberOfTests)
        {
            #region Fractiontype
            long[] algTimes = new long[3];

            AlgorithmGen<FractionType> algorithmFracType = new AlgorithmGen<FractionType>(numberOfElements);
            AlgorithmGen<float> algorithm = new AlgorithmGen<float>(numberOfElements);

            for (int pf = 0; pf < numberOfTests; pf++)
            {
                FractionType[][] matrixFracCopyToCompare = DataSource.GetMatrixData<FractionType>(numberOfElements);
                FractionType[][] MatrixFracType = DataSource.GetMatrixData<FractionType>(numberOfElements);
                FractionType[][] Matrix2FracType = DataSource.GetMatrixData<FractionType>(numberOfElements); ;
                FractionType[][] Matrix3FracType = DataSource.GetMatrixData<FractionType>(numberOfElements); ;


                float[][] matrixCopy = DataSource.GetMatrixData<float>(numberOfElements);

              
                #region GaussTypeNormal
                FractionType res1 = new FractionType();

                //get alg time
                var vectorResultFracType =  algorithmFracType.GaussElimination(MatrixFracType,new FractionType(),EnumsContainer.GaussType.Normal);
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
                        varctorResultFracTypeToFloat[m] = ((float)vectorResultFracType[m].Numerator / (float)vectorResultFracType[m].Denominator);
                    }

                    var rtnFracTypeError = algorithmFracType.CalculateError(matrixFracCopyToCompare, vectorResultFracType);
                    algorithmFracType.PrintResult(rtnFracTypeError);
                }

                #endregion


                #region GaussPartialPivot

                var vectorResult2 = algorithmFracType.GaussElimination(Matrix2FracType, new FractionType(), EnumsContainer.GaussType.PartialPivot);
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

                    var rtnFracTypeError = algorithmFracType.CalculateError(matrixFracCopyToCompare, vectorResultFracType);
                    algorithmFracType.PrintResult(rtnFracTypeError);
                }


                #endregion


                #region GaussTypeFullPivot


                int[] indexArray = new int[numberOfElements];
                for (int i = 0; i < numberOfElements; i++)
                {
                    indexArray[i] = i;
                }
                var vectorResult3 = algorithmFracType.GaussElimination(Matrix3FracType,new FractionType(),EnumsContainer.GaussType.FullPivot, indexArray);
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
                        verctorResultFracTypeFullpivot[m] = ((float)vectorResult3[m].Numerator / (float)vectorResult3[m].Denominator);
                    }

                    var rtnFracTypeError = algorithmFracType.CalculateError(matrixFracCopyToCompare, vectorResultFracType);
                    algorithmFracType.PrintResult(rtnFracTypeError);
                }


                #endregion


            }

            #endregion
        }


        public static void WriteResultsToExcelFile<T>(List<ResultSet<T>> results)
        {
            using (StreamWriter file = File.AppendText(Directory.GetCurrentDirectory() + "//Q1Results.csv"))
            {
                file.WriteLine("Normal,Partial");
                foreach (var result in results)
                {
                    file.WriteLine(result.ResultNormal + "," + result.ResultPartial);
                }
                file.WriteLine();
            }

        }
        public static void WriteResultsToExcelFileQ2Result(List<Q2TimesResults> results)
        {
            using (StreamWriter file = File.AppendText(Directory.GetCurrentDirectory() + "//Q2Results.csv"))
            {
                file.WriteLine("Float;Double");
                foreach (var result in results)
                {
                    file.WriteLine(result.floutTime + ";" + result.doubleTime);
                }
                file.WriteLine();
            }

        }
    }
}
