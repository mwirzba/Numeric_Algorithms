using System;
using System.Collections.Generic;

namespace SummationOfPowerSeries
{
    class Algorithm
    {
        public Algorithm()
        {
           ResultsList =  new List<double>();
           ResultsList2 = new List<double>();
        }
        public List<double> ResultsList { get; private set; }
        public List<double> ResultsList2 { get; private set; }
        public void CountUsingFormula(double x,int n)
        {

            double tmpX = x-1;
            double tmp = tmpX;
            ResultsList.Clear();
            ResultsList.Add(tmpX);
            for (int i = 2; i <= n; i++)
            {
                tmp = tmp * tmpX * (-1);
                ResultsList.Add(tmp/i);
            }
        }

        public void CountUsingPreviousResult(double x, int n)
        {
            ResultsList2.Clear();
            double tmpX = x - 1;
            ResultsList2.Add(tmpX);
            for (int i = 1; i < n; i++)
            {
                var tmp = tmpX * ((-1) * i * (x - 1)) / (i + 1);
                tmpX = tmp;
                ResultsList2.Add(tmp);
            }

        }

        public double SumFromBeginning(List<double> resultsList)
        {
            double result=0;
            for (int i = 0; i<resultsList.Count; i++)
            {

                result += resultsList[i];
            }

            return result;
        }

        public double SumFromEnd(List<double> resultsList)
        {
            double result = 0;
            for (int i = resultsList.Count-1; i >= 0; i--)
            {

                result += resultsList[i];
            }

            return result;
        }

        public List<double> GetArgumentsList(double amount)
        {
            var rtnList = new List<double>();
            var random = new Random();
            for (int i = 0; i < amount; i++)
            {
                double randomNumber = random.NextDouble() * 2;
                rtnList.Add(randomNumber);
            }

            return rtnList;

        }
    }
}
