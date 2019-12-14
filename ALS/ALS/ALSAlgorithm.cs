using Alg2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALS
{
    class ALSAlgorithm
    {
        private DataSource _dataSource;
        public ALSAlgorithm(long numberOfUsers, long numberOfProducts, int userParamD,double sigma,double?[][] matrixR, double[][] matrixP, double[][] matrixU)
        {
            _userParamD = userParamD;
            _numberOfProducts = numberOfProducts;
            _numberOfUsers = numberOfUsers;
            _dataSource = new DataSource(userParamD, numberOfUsers);
            _sigma = sigma;
            MatrixR = matrixR;
            //MatrixP = _dataSource.GetRandomNumbers(userParamD, numberOfProducts);
            //MatrixU = _dataSource.GetRandomNumbers(userParamD, numberOfUsers);
            MatrixP = matrixP;
            MatrixU = matrixU;
            //PrintMatrix(MatrixR);
            //Console.WriteLine();
            //Console.WriteLine();
            //dataSource.PrintMatrix(MatrixU);
        }


        public double[][] Start(out int numberOfLoops)
        {
            double oldFuncRes = double.MaxValue;
            double newFuncRes = double.MaxValue;
            numberOfLoops = 0;
            var calcObjList = new List<double>();
            do
            {
                oldFuncRes = newFuncRes;
                for (int z = 0; z < _numberOfUsers; z++)
                {
                    var indexList = GetIndexListForUser(z);
                    double[][] P = new double[_userParamD][];
                    CopyColumns(indexList, P, MatrixP);
                    //_dataSource.PrintMatrix(P);
                    //Console.WriteLine("xdddd");
                    double[][] Pt = GetTransposedArray(P);
                    //_dataSource.PrintMatrix(Pt);


                    double[][] multiplyResult = new double[P.Length][];
                    InitMatrix(multiplyResult, P.Length);
                    var source = Enumerable.Range(0, P.Length);
                    var pquery = from num in source.AsParallel()
                                 select num;
                    pquery.ForAll((e) => MultiplyMatrix(P, Pt, multiplyResult, e));
                    //Console.WriteLine("RESULT");
                    //_dataSource.PrintMatrix(multiplyResult);
                    AddSigma(multiplyResult);
                    //Console.WriteLine();
                    //_dataSource.PrintMatrix(multiplyResult);
                    CalculateVectorVu(indexList, P, multiplyResult, z);
                    //Console.WriteLine();
                    //_dataSource.PrintMatrix(multiplyResult);

                    AlgorithmDouble algorithmDouble = new AlgorithmDouble(multiplyResult.Length);
                    var vectorResult = algorithmDouble.GaussElimination(multiplyResult, 0, EnumsContainer.GaussType.PartialPivot);
                    //for (int i = 0; i < vectorResult.Length; i++)
                    //{
                    //    Console.WriteLine(vectorResult[i]);
                    //}
                    for (int i = 0; i < MatrixU.Length; i++)
                    {
                        MatrixU[i][z] = vectorResult[i];
                    }
                }

                for (int z = 0; z < _numberOfProducts; z++)
                {
                    var indexList = GetIndexListForProduct(z);
                    double[][] P = new double[_userParamD][];
                    CopyColumns(indexList, P, MatrixU);
                    double[][] Pt = GetTransposedArray(P);

                    double[][] multiplyResult = new double[P.Length][];
                    InitMatrix(multiplyResult, P.Length);
                    var source = Enumerable.Range(0, P.Length);
                    var pquery = from num in source.AsParallel()
                                 select num;

                    pquery.ForAll((e) => MultiplyMatrix(P, Pt, multiplyResult, e));

                    AddSigma(multiplyResult);

                    CalculateVectorVp(indexList, P, multiplyResult, z);


                    AlgorithmDouble algorithmDouble = new AlgorithmDouble(multiplyResult.Length);
                    var vectorResult = algorithmDouble.GaussElimination(multiplyResult, 0, EnumsContainer.GaussType.PartialPivot);

                    for (int i = 0; i < MatrixU.Length; i++)
                    {
                        MatrixP[i][z] = vectorResult[i];
                    }
                }
                double endSum = (CalculateVectorSum(MatrixU) + CalculateVectorSum(MatrixP)) * _sigma;
                double rtn = CalculateObjectiveFunc(MatrixU, MatrixP) + endSum;

                newFuncRes = rtn;

                calcObjList.Add(rtn);

          //      Console.WriteLine(" OLD: " + oldFuncRes + " NEW " + newFuncRes);

                //   if (newFuncRes > oldFuncRes)
                //break;
                numberOfLoops++;
            } while (newFuncRes < oldFuncRes);


                //_dataSource.PrintMatrix(MatrixU);
                //_dataSource.PrintMatrix(MatrixP);

            var transpU = GetTransposedArray(MatrixU);

            var resultMatrix = new double[_numberOfUsers][];
            for (int i = 0; i < _numberOfUsers; i++)
            {
                resultMatrix[i] = new double[_numberOfProducts];
            }
            var s = Enumerable.Range(0, transpU.Length);
            var p = from num in s.AsParallel()
                         select num;


            p.ForAll((e) => { MultiplyMatrix(transpU, MatrixP, resultMatrix, e); });
            //PrintMatrix(MatrixR);
            //Console.WriteLine("Original");
            //PrintMatrix(MatrixR);
            //Console.WriteLine("WYNIKI");
            //Console.WriteLine();
            //_dataSource.PrintMatrix(resultMatrix);
            //Console.WriteLine("NUMBER OF BAD RESULTS");

            return resultMatrix;
            //Console.WriteLine(GetNumberOfBadResults(resultMatrix) + "/" +resultMatrix[0].Length * resultMatrix.Length);
            //CalculateError(MatrixR, resultMatrix);
            //Console.WriteLine("funkcja");
            //foreach (var item in calcObjList)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("BLEDE");

            //var res = CalculateError(MatrixR, resultMatrix).ToList();

            //_dataSource.WriteToCsv(calcObjList);
        }

        public List<double> CalculateError(double?[][] originalMatrix, double[][] resultMatrix)
        {
            List<double> errorsList = new List<double>();

            for (int i = 0; i < originalMatrix.Length; i++)
            {
                double errorForUser = 0;
                int notNullCounter = 0;
                for (int j = 0; j < originalMatrix[i].Length; j++)
                {
                    if (originalMatrix[i][j] != null)
                    {
                        errorForUser += Math.Abs((double)originalMatrix[i][j] - resultMatrix[i][j]);
                        notNullCounter++;
                    }
                }
                errorForUser /= notNullCounter;
                errorsList.Add(errorForUser);
            }
            return errorsList;
        }


        private double CalculateVectorSum(double[][] matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix[0].Length; i++)
            {
                double mul = 0;
                for (int j = 0; j < matrix.Length; j++)
                {
                    mul += matrix[j][i] * matrix[j][i];
                }
                mul = Math.Sqrt(mul);
                mul *= mul;
                mul = Math.Pow(mul, -1);
                sum += mul;
            }

            return sum;
        }

        public List<int> GetNumberOfBadResults(double[][] matrixU)
        {
            int diffNumber = 0;
            int bigDiffNumber = 0;
            var rtn = new List<int>();
            for (int i = 0; i < matrixU.Length; i++)
            {
                for (int j = 0; j < matrixU[i].Length; j++)
                {
                    if (matrixU[i][j] > 5.10 || matrixU[i][j] < 0.9)
                        diffNumber++;

                    if (matrixU[i][j] > 6.00 || matrixU[i][j] < 0.0)
                        bigDiffNumber++;

                }
            }
            rtn.Add(diffNumber);
            rtn.Add(bigDiffNumber);
            return rtn;
        }
        public double CalculateObjectiveFunc(double[][] matrixU,double[][] matrixP)
        {
            double sum=0;
            for (int i = 0; i < MatrixR.Length; i++)
            {
                for (int j = 0; j < MatrixR[i].Length; j++)
                {
                    double x = 0;
                    if (MatrixR[i][j]!=null)
                    {
                        
                        for (int k = 0; k < _userParamD; k++)
                        {
                            x += matrixU[k][i] * matrixP[k][j];
                        }

                        sum += Math.Pow((double)MatrixR[i][j] - x, 2);
                    }        
                }
            }
            return sum;
        }

        
        public void PrintMatrix(double?[][] matrix)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    if(matrix[i][j]==null)
                        Console.Write("[null]");
                    else
                    Console.Write("[ " + Math.Round((double)matrix[i][j], 2) + "] ");
                }
                Console.WriteLine("NEW");
            }
        }
        private void CalculateVectorVu(List<int> indexList, double[][] P,double[][] multiplyResult,int z)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                for (int j = 0; j < P.Length; j++)
                {
                    multiplyResult[j][multiplyResult.Length] += (double)MatrixR[z][indexList[i]] * P[j][i];
                }
            }
        }

        private void CalculateVectorVp(List<int> indexList, double[][] P, double[][] multiplyResult, int z)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                for (int j = 0; j < P.Length; j++)
                {
                    multiplyResult[j][multiplyResult.Length] += (double)MatrixR[indexList[i]][z] * P[j][i];
                }
            }
        }

        public void AddSigma(double[][] multiplyResult)
        {
            for (int i = 0; i < multiplyResult.Length; i++)
            {
                multiplyResult[i][i] += _sigma;
            }
        }

        public List<int> GetIndexListForUser(int userNumber)
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < MatrixR[0].Length; i++)
            {
                if (MatrixR[userNumber][i] != null)
                {
                    indexList.Add(i);
                }
            }
            return indexList;
        }
        public List<int> GetIndexListForProduct(int prodnumber)
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < MatrixR.Length; i++)
            {
                if (MatrixR[i][prodnumber] != null)
                {
                    indexList.Add(i);
                }
            }
            return indexList;
        }

        private void CopyColumns(List<int> indexList, double[][] P,double[][] Matrix)
        {
            for (int i = 0; i < _userParamD; i++)
            {
                P[i] = new double[indexList.Count];
                for (int j = 0; j < indexList.Count; j++)
                {
                    P[i][j] = Matrix[i][indexList[j]];
                }
            }
        }
        
        private double[][] GetTransposedArray(double[][] vs)
        {
            var transponsedArray = new double[vs[0].Length][];
            for (int i = 0; i < transponsedArray.Length; i++)
            {
                transponsedArray[i] = new double[vs.Length];
                for (int j = 0; j < transponsedArray[i].Length; j++)
                {
                    transponsedArray[i][j] = vs[j][i];
                }
            }
            return transponsedArray;
        }

        private void MultiplyMatrix(double[][] A,double[][] B,double[][] Result,int indexer)
        {
            double[] iRowA = A[indexer];
            double[] iRowC = Result[indexer];
            for (int k = 0; k < B.Length; k++)
            {
                double[] kRowB = B[k];
                double ikA = iRowA[k];
                for (int j = 0; j < B[0].Length; j++)
                {
                    iRowC[j] += ikA * kRowB[j];
                }
            }
        }

        private void InitMatrix(double[][] matrix,int length)
        {
            for (int i = 0; i < length; i++)
            {
                matrix[i] = new double[length+1];
            }
        }
    


        private readonly long _numberOfUsers;
        private readonly long _numberOfProducts;
        public double?[][] MatrixR { get; set; }
        public double[][] MatrixP { get; set; }
        public double[][] MatrixU { get; set; }

        private readonly int _userParamD;
        private double _sigma;

    }
}
