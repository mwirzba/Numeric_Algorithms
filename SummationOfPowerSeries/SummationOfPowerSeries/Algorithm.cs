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
        public int CountUsingFormula(double x,int numberOfElementsToAdd)
        {

            double tmpX = x - 1;
            double tmp = tmpX;
            int counter = 2;
            ResultsList.Clear();
            ResultsList.Add(tmpX);
            do
            {

                tmp = tmp * tmpX * (-1);
                ResultsList.Add(tmp / counter);
                counter += numberOfElementsToAdd;

            } while (!IsAccurateEnought(tmpX, tmp,counter));

            return counter;
        }

        private bool IsAccurateEnought(double tmpX, double tmp,int counter)
        {
            if((tmp * tmpX * (-1))/counter < 0)
                return (tmp * tmpX * (-1)) / counter > -0.000001;
            else
                return (tmp * tmpX * (-1))/counter < 0.000001;
        }

        public int CountUsingPreviousResult(double x, int numberOfElementsToAdd)
        {
            ResultsList2.Clear();
            double tmpX = x - 1;
            ResultsList2.Add(tmpX);
            int counter = 2;

            do
            {
                var tmp = tmpX * ((-1) * counter * (x - 1)) / (counter + 1);
                tmpX = tmp;
                ResultsList2.Add(tmp);
                counter += numberOfElementsToAdd;

            } while (!(tmpX<0 && tmpX > -0.000001 || tmpX >0 && tmpX < 0.000001));

             return counter;
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
            double number = 0;
            double numberToIncrease = 0.0000002;
            for (int i = 0; i < amount; i++)
            { 
                number += numberToIncrease‬;
                //double randomNumber = random.NextDouble() * 2;
                rtnList.Add(number);
            }

            return rtnList;

        }
    }
}
