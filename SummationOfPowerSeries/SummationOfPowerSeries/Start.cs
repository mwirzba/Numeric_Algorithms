using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace SummationOfPowerSeries
{
    class Start
    {
        double numberOfArgs;

        public Start()
        {
            GraphsFunctions graphs = new GraphsFunctions();

            numberOfArgs = 1000000;
            int maxNumberOfAddedElements = 500;
            List<double> randomArgumentsList = Algorithm.GetArgumentsList(numberOfArgs, true);
            List<double> argumentsList = Algorithm.GetArgumentsList(numberOfArgs, false, 0.000002);



            #region H1 & H3
            
            var usingFormulaRandArgsResults = GetAlgResultsAccuracyComparison(maxNumberOfAddedElements,randomArgumentsList,Algorithm.CountUsingFormula,Algorithm.CountUsingFormula,Algorithm.GetSumFromEnd, Algorithm.GetSumFromBegin);  //CompareAlgByAddingOrder(maxNumberOfAddedElements, randomArgumentsList,true);
            var usingFormulaResults = GetAlgResultsAccuracyComparison(maxNumberOfAddedElements,argumentsList,Algorithm.CountUsingFormula, Algorithm.CountUsingFormula, Algorithm.GetSumFromEnd, Algorithm.GetSumFromBegin);
            var usingPreviousElementRandArgsResults = GetAlgResultsAccuracyComparison(maxNumberOfAddedElements,randomArgumentsList, Algorithm.CountUsingPreviousElement, Algorithm.CountUsingPreviousElement, Algorithm.GetSumFromEnd, Algorithm.GetSumFromBegin);
            var usingPreviousElementResults  = GetAlgResultsAccuracyComparison(maxNumberOfAddedElements,argumentsList, Algorithm.CountUsingPreviousElement, Algorithm.CountUsingPreviousElement, Algorithm.GetSumFromEnd, Algorithm.GetSumFromBegin);
            
            var usingDiffMethodsAndRandArgs = GetAlgResultsAccuracyComparison(maxNumberOfAddedElements, randomArgumentsList, Algorithm.CountUsingPreviousElement, Algorithm.CountUsingFormula, Algorithm.GetSumFromBegin, Algorithm.GetSumFromBegin);


            graphs.WriteResultsWithNumbersOfElementsToExelFile(usingFormulaRandArgsResults);
            graphs.WriteResultsWithNumbersOfElementsToExelFile(usingFormulaResults);

            graphs.WriteResultsWithNumbersOfElementsToExelFile(usingPreviousElementRandArgsResults);
            graphs.WriteResultsWithNumbersOfElementsToExelFile(usingPreviousElementResults);

            graphs.WriteResultsWithNumbersOfElementsToExelFile(usingDiffMethodsAndRandArgs);

            
            #endregion


            #region H2
            
            var errors = GetDiffInResultByNumberOfElementsAdded(argumentsList,maxNumberOfAddedElements);
            graphs.WriteResultsToExcelFile(errors);
            
            #endregion

            #region Q1

            var results = GetDifferencesCompToLog(argumentsList, maxNumberOfAddedElements);
            graphs.WriteResultsWithNumbersOfElementsToExelFile(results);

            #endregion

            #region Q2
            
            numberOfArgs = 1000000;
            argumentsList =Algorithm.GetArgumentsList(numberOfArgs, false, 0.000002);
            var rtns = new List<Result>();
            var avg = GetAvgOfElementsAdded(argumentsList, Algorithm.CountUsingFormula, rtns);
            graphs.WriteResultsToExcelFile(rtns);
            Console.WriteLine(avg);
            rtns.Clear();
            avg = GetAvgOfElementsAdded(argumentsList, Algorithm.CountUsingPreviousElement, rtns);
            graphs.WriteResultsToExcelFile(rtns);
            Console.WriteLine(avg);
            
            #endregion

            Console.WriteLine("Done");
            Console.ReadKey();

        }

        private List<Result> GetDifferencesCompToLog(List<double> argumentsList, int maxNumberOfAddedElements)
        {
            var comparedResultsList = new List<Result>();
            double diff = 0;
            for (int i = 0; i < maxNumberOfAddedElements ; i+=20)
            {
                foreach (var argument in argumentsList)
                {
                    var elements = Algorithm.CountUsingFormula(argument,i);
                    double resFromAlg = Algorithm.GetSumFromBegin(elements);
                    var res = Math.Log(argument) - resFromAlg;
                    if (res < 0)
                        res = res * (-1);
                    diff += res;

                }

                diff = diff / argumentsList.Count;
                comparedResultsList.Add(new Result{AlgorithmResult = diff,NumberOfAddedWords = i});
                diff = 0;
            }
            return comparedResultsList;
        }

        private List<Result> GetDiffInResultByNumberOfElementsAdded(List<double> argumentsList, int maxNumberOfAddedElements)
        {
            var res = new List<double>();
            var logRes = new List<double>();
            foreach (var argument in argumentsList)
            {
                res.Add(GetAlgorithmResult(argument, maxNumberOfAddedElements, Algorithm.CountUsingFormula,
                    Algorithm.GetSumFromBegin));
                logRes.Add(Math.Log(argument));
            }


            double n1 = 0;
            double n2 = 0;
            double arg = 0;

            var newli1 = new List<double>();
            var newli2 = new List<double>();
            var newargs = new List<double>();
            for (int i = 0; i < res.Count; i++)
            {
                n1 += res[i];
                n2 += logRes[i];
                arg += argumentsList[i];
                if ((i + 1) % 1000 == 0)
                {
                    newli1.Add(n1 / 1000);
                    newli2.Add(n2 / 1000);
                    newargs.Add(arg / 1000);
                    n1 = 0;
                    n2 = 0;
                    arg = 0;
                }
            }

            var errors = new List<Result>();
            double inaccuracy = 0;
            for (int i = 0; i < newli1.Count; i++)
            {
                if (newli2[i] > 0)
                {
                    if (newli1[i] < newli2[i])
                    {
                        inaccuracy = (newli2[i] - newli1[i]) / newli2[i];
                    }
                }
                else if (newli2[i] < 0)
                {
                    if (newli1[i] > newli2[i])
                    {
                        inaccuracy = (newli2[i] - newli1[i]) / newli2[i];
                    }
                }

                errors.Add(new Result {AlgorithmResult = inaccuracy, NumberOfAddedWords = newargs[i]});
            }

            return errors;
        }

        private double GetAvgOfElementsAdded(List<double> argumentsList, Func<double, List<double>, int> algFunction,List<Result> avgResultsSet)
        {
            var resList = new List<double>();
            var resultsSet = new List<Result>();
            foreach (var argument in argumentsList)
            {
                int counter = algFunction(argument, resList);
                resList.Clear();
                resultsSet.Add(new Result { AlgorithmResult = argument, NumberOfAddedWords = counter });
            }

            double counterSum = 0;
            double resultSum = 0;

            for (int i = 0; i < resultsSet.Count; i++)
            {
                counterSum += resultsSet[i].NumberOfAddedWords;
                resultSum += resultsSet[i].AlgorithmResult;
                if ((i + 1) % 1000 == 0)
                {
                    avgResultsSet.Add(new Result { AlgorithmResult = resultSum / 1000, NumberOfAddedWords = counterSum / 1000 });
                    counterSum = 0;
                    resultSum = 0;
                }
            }

            double average = 0;
            for (int i = 0; i < resultsSet.Count; i++)
            {
                average += resultsSet[i].NumberOfAddedWords;
            }

            average = average / resultsSet.Count;

            return average;
        }

        private List<Result> GetAlgResultsAccuracyComparison(int maxNumberOfAddedElements, List<double> argumentsList,Func<double, int, List<double>> algFunction1, Func<double, int, List<double>> algFunction2, Func<List<double>, double> sumFunction1, Func<List<double>, double> sumFunction2)
        {
            var resultsFromFirstMethod = new List<double>();
            var resultsFromSecondMethod = new List<double>();

            var cmpResults = new List<Result>();
            for (int i = 30; i < maxNumberOfAddedElements; i += 20)
            {
                foreach (var arg in argumentsList)
                {
                    resultsFromSecondMethod.Add(GetAlgorithmResult(arg,i,algFunction1,sumFunction1));
                    resultsFromFirstMethod.Add(GetAlgorithmResult(arg,i,algFunction2,sumFunction2));
                }

                double result = GetNumberOfMoreAccurateResults(resultsFromSecondMethod, resultsFromFirstMethod, argumentsList) /
                                (double) argumentsList.Count;
                resultsFromFirstMethod.Clear();
                resultsFromSecondMethod.Clear();
                cmpResults.Add(new Result() {AlgorithmResult = result, NumberOfAddedWords = i});
            }

            return cmpResults;
        }
        
        int GetNumberOfMoreAccurateResults(List<double> resultsList, List<double> resultsListToCompare, List<double> args)
        {
            int numberOfMoreAccurateResults = 0;
            for (int i = 0; i < resultsList.Count; i++)
            {
                double log = Math.Log(args[i]);
                if (log > 0)
                {
                    if (log - resultsList[i] < log - resultsListToCompare[i])
                    {
                        numberOfMoreAccurateResults++;
                    }
                    else if (log - resultsList[i] > log - resultsListToCompare[i])
                    {
                        numberOfMoreAccurateResults--;
                    }
                }
                else
                {
                    if (log - resultsList[i] > log - resultsListToCompare[i])
                    {
                        numberOfMoreAccurateResults++;
                    }
                    else if (log - resultsList[i] < log - resultsListToCompare[i])
                    {
                        numberOfMoreAccurateResults--;
                    }
                }
            }

            return numberOfMoreAccurateResults;
        }

        private double GetAlgorithmResult(double argument,int numberOfElements, Func<double,int, List<double>> algFunction, Func<List<double>, double> sumFunction)
        {         
            var resultsList = algFunction(argument,numberOfElements);
            var rtn = sumFunction(resultsList);
            return rtn;
        }


    }
}
