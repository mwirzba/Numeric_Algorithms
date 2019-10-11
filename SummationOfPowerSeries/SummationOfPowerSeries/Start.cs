using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummationOfPowerSeries
{
    class Start
    {

        List<Result> formulaResultsFromBegin;
        List<Result> formulaResultsFromEnd;
        List<Result> countUsingPreviousWordResultsFromBegin;
        List<Result> countUsingPreviousWordResultsFromEnd;
        Algorithm alg;
        double numberOfArgs;

        public Start()
        {
            formulaResultsFromBegin = new List<Result>();
            formulaResultsFromEnd = new List<Result>();

            countUsingPreviousWordResultsFromBegin = new List<Result>();
            countUsingPreviousWordResultsFromEnd = new List<Result>();
            alg = new Algorithm();
            numberOfArgs = 1000000;
            List<double> argumentsList = alg.GetArgumentsList(numberOfArgs);


            StartExecutingAlgorithms(argumentsList, alg);


            double cmpResult1 = Compare(formulaResultsFromBegin, formulaResultsFromEnd, argumentsList) / numberOfArgs;
            double cmpResult2 = Compare(countUsingPreviousWordResultsFromBegin,countUsingPreviousWordResultsFromEnd, argumentsList) / numberOfArgs;


            double cmpResult3 = Compare(formulaResultsFromBegin, countUsingPreviousWordResultsFromBegin, argumentsList) / numberOfArgs;
            double cmpResult4 = Compare(formulaResultsFromEnd, countUsingPreviousWordResultsFromEnd, argumentsList) / numberOfArgs;

            GraphsFunctions graphs = new GraphsFunctions();
            graphs.AddColumnChartToPDF(new List<double> { cmpResult1, cmpResult2, cmpResult3, cmpResult4 });
            Console.WriteLine(cmpResult1);
            Console.WriteLine(cmpResult2);

            Console.WriteLine(cmpResult3);
            Console.WriteLine(cmpResult4);

            Console.WriteLine("Done");
            Console.ReadKey();

        }

        int Compare(List<Result> resultsList, List<Result> resultsListToCompare, List<double> args)
        {
            int numberOfMoreAccurateResults = 0;
            for (int i = 0; i < resultsList.Count; i++)
            {
                double tmp = Math.Log(args[i]);
                if (tmp - resultsListToCompare[i].AlgorithmResult < tmp - resultsList[i].AlgorithmResult)
                {
                    numberOfMoreAccurateResults++;
                }
            }

            return numberOfMoreAccurateResults;

        }

        void StartExecutingAlgorithms(List<double> argList, Algorithm alg)
        {
            try
            {


                foreach (var arg in argList)
                {

                    ExecuteAlgorithmAndAddResultToList(arg, alg.ResultsList,10, alg.CountUsingFormula, alg.SumFromBeginning, formulaResultsFromBegin);
                    ExecuteAlgorithmAndAddResultToList(arg, alg.ResultsList,10, alg.CountUsingFormula, alg.SumFromEnd, formulaResultsFromEnd);

                    ExecuteAlgorithmAndAddResultToList(arg, alg.ResultsList2,10,alg.CountUsingPreviousResult,alg.SumFromBeginning, countUsingPreviousWordResultsFromBegin);
                    ExecuteAlgorithmAndAddResultToList(arg, alg.ResultsList2,10,alg.CountUsingPreviousResult,alg.SumFromEnd, countUsingPreviousWordResultsFromEnd);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
           

        }

        private void ExecuteAlgorithmAndAddResultToList(double arg,List<double> resultList,int numberOfElementsToIncrease, Func<double,int,int> algFunction, Func<List<double>, double> sumFunction,List<Result> sumList)
        {      
            double result = 0;
            int counter = algFunction(arg,numberOfElementsToIncrease);
            result = sumFunction(resultList);
            sumList.Add(new Result() { AlgorithmResult = result, NumberOfAddedWords = counter });
        }

        // ReSharper disable once InconsistentNaming


    }
}
