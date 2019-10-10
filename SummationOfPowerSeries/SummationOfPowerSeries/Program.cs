using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SummationOfPowerSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            Algorithm alg = new Algorithm();
            double numberOfArgs = 2000000;
            List<double> argumentsList = alg.GetArgumentsList(numberOfArgs);
            List<double> formulaResultsFromBegin =  new List<double>();
            List<double> formulaResultsFromEnd = new List<double>();

            List<double> countUsingPreviousWordResultsFromBegin = new List<double>();
            List<double> countUsingPreviousWordResultsFromEnd = new List<double>();

            foreach (var arg in argumentsList)
            {
                alg.CountUsingFormula(arg,30);
                alg.CountUsingPreviousResult(arg,30);

                formulaResultsFromBegin.Add(alg.SumFromBeginning(resultsList: alg.ResultsList));
                formulaResultsFromEnd.Add(alg.SumFromEnd(resultsList: alg.ResultsList));

                countUsingPreviousWordResultsFromBegin.Add(alg.SumFromBeginning(resultsList: alg.ResultsList2));
                countUsingPreviousWordResultsFromEnd.Add(alg.SumFromEnd(resultsList: alg.ResultsList2));
            }


            Console.WriteLine(Program.Compare(formulaResultsFromBegin, formulaResultsFromEnd,argumentsList)/numberOfArgs);
            Console.WriteLine(Program.Compare(countUsingPreviousWordResultsFromBegin, countUsingPreviousWordResultsFromEnd, argumentsList)/numberOfArgs);


            Console.WriteLine();

            Console.WriteLine(Program.Compare(formulaResultsFromBegin, countUsingPreviousWordResultsFromBegin, argumentsList) / numberOfArgs);
            Console.WriteLine(Program.Compare(formulaResultsFromEnd, countUsingPreviousWordResultsFromEnd, argumentsList) / numberOfArgs);

       

            /* alg.CountUsingFormula(1.55,8);
             double rtn = alg.SumFromBeginning(resultsList: alg.ResultsList);
             double rtn2 = alg.SumFromEnd(resultsList: alg.ResultsList);
             alg.CountUsingPreviousResult(1.55,8);


             double rtn1 = alg.SumFromBeginning(resultsList: alg.ResultsList2);
             double rtn12 = alg.SumFromEnd(resultsList: alg.ResultsList2);

             Console.WriteLine("1.1: "+rtn);
             Console.WriteLine("2.1: "+ rtn2);
             Console.WriteLine("Log: "+ Math.Log(1.55));


             Console.WriteLine("1.2: " + rtn1);
             Console.WriteLine("2.2: " + rtn12);
             Console.WriteLine("Log: " +Math.Log(1.55));
             */

            Console.ReadKey();

        }

        static int Compare(List<double> formulaResults, List<double> countUsingPreviousWordResults, List<double> args)
        {
            int numberOfMoreAccurateResults=0;
            for (int i = 0; i < formulaResults.Count; i++)
            {
                double tmp = Math.Log(args[i]);
                if ( tmp - countUsingPreviousWordResults[i] < tmp - formulaResults[i])
                {
                    numberOfMoreAccurateResults++;
                }
            }

            return numberOfMoreAccurateResults;

        }

        /*static int CheckIfAlgorithmUsingPreviousWordIsMoreAccurate(List<double> resultsFromBeginning, List<double> resultsFromEnd, List<double> args)
        {
            int numberOfMoreAccurateResults = 0;
            for (int i = 0; i < resultsFromBeginning.Count; i++)
            {
                double tmp = Math.Log(args[i]);
                if (tmp - resultsFromEnd[i] < tmp - resultsFromBeginning[i])
                {
                    numberOfMoreAccurateResults++;
                }
            }

            return numberOfMoreAccurateResults;

        }*/

    }
}
