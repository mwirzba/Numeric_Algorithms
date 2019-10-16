using System;
using System.Collections.Generic;

namespace SummationOfPowerSeries
{
    static class Algorithm
    {
        public static List<double> CountUsingFormula(double x,int numberOfElements)
        {
            var resultsList = new List<double>();
            double firstElement = x - 1;
            double xToThePower = firstElement;
            resultsList.Clear();
            resultsList.Add(firstElement);
            for (int i = 2; i <= numberOfElements; i++)
            {
                xToThePower = xToThePower * firstElement * (-1);
                resultsList.Add(xToThePower / i);
            }

            return resultsList;
        }

        public static int CountUsingFormula(double x,List<double> resultsList)
        {
            resultsList.Clear();
            double firstElement = x - 1;
            double xToThePower = firstElement;
            int counter = 1;
            do
            {
                resultsList.Add(xToThePower / counter);
                xToThePower = xToThePower * firstElement * (-1);
                counter++;

            } while (!IsNextElementAccurateEnough(xToThePower / counter));

            return counter;
        }

        public static List<double> CountUsingPreviousElement(double x, int numberOfElements)
        {
            
            var resultsList = new List<double>();
            double element = x - 1;
            resultsList.Add(element);
            for (int i = 1; i < numberOfElements; i++)
            {
                element = element * ((-1) * i * (x - 1)) / (i + 1);
                resultsList.Add(element);
            }

            return resultsList;
        }
                
        public static int CountUsingPreviousElement(double x,List<double> resultsList)
        {
            resultsList.Clear();
            double element = x - 1;
            resultsList.Add(element);
            int counter = 1;

            do
            {
                element = element * ((-1) * counter * (x - 1))/(counter + 1);
                resultsList.Add(element);
                counter++;

            } while (!IsNextElementAccurateEnough(element));

             return counter;
        }

        private static bool IsNextElementAccurateEnough(double element)
        {
            return (element < 0 && element > -0.000001 || element > 0 && element < 0.000001);
        }

        public static double GetSumFromBegin(List<double> resultsList)
        {
            double result=0;
            for (int i = 0; i<resultsList.Count; i++)
            {
                result += resultsList[i];
            }

            return result;
        }

        public static double GetSumFromEnd(List<double> resultsList)
        {
            double result = 0;
            for (int i = resultsList.Count-1; i >= 0; i--)
            {

                result += resultsList[i];
            }

            return result;
        }

        public static List<double> GetArgumentsList(double amount, bool getRandomArguments,double numberToIncrease=0)
        {
            var rtnList = new List<double>();
            var random = new Random();
            double number = 0;
            if(!getRandomArguments)

                for (int i = 0; i < amount; i++)
                { 
                    number += numberToIncrease;
                    rtnList.Add(number);
                }
            else
                for (int i = 0; i < amount; i++)
                {
                    double randomNumber = random.NextDouble() * 2;
                    rtnList.Add(randomNumber);
                }


            return rtnList;

        }
    }
}
