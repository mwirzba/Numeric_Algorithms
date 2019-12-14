using System;
using System.Numerics;

namespace Alg2
{

    public struct FractionType
    {
        static FractionType Euklides(BigInteger num1, BigInteger num2)
        {

            FractionType rtn = new FractionType { Numerator = num1, Denominator = num2 };
            BigInteger d = BigInteger.GreatestCommonDivisor(num1, num2);
            if (d != 0)
            {
                rtn.Numerator = rtn.Numerator / d;
                rtn.Denominator = rtn.Denominator / d;

            }
            return rtn;
        }
        public BigInteger Numerator { get; set; }
        public BigInteger Denominator { get; set; }

        

        public static FractionType operator +(FractionType type1, FractionType type2)
        {
            var rtn =  new FractionType
            {
                Numerator = type1.Denominator == type2.Denominator ?  type1.Numerator + type2.Numerator  
                    : type1.Numerator * type2.Denominator + type1.Denominator * type2.Numerator,
                Denominator = type1.Denominator == type2.Denominator ? type2.Denominator 
                    : type1.Denominator * type2.Denominator
            
            };
            return Euklides(rtn.Numerator, rtn.Denominator);

        }

        public static FractionType operator -(FractionType type1, FractionType type2)
        {
            var rtn = new FractionType
            {
                Numerator = type1.Denominator == type2.Denominator ? type1.Numerator - type2.Numerator
                    : type1.Numerator * type2.Denominator - type1.Denominator * type2.Numerator,
                Denominator = type1.Denominator == type2.Denominator ? type2.Denominator 
                    : type1.Denominator * type2.Denominator

            };
            return Euklides(rtn.Numerator, rtn.Denominator);
        }

        public static FractionType operator *(FractionType type1, FractionType type2)
        {
            var rtn = new FractionType
            {
                Numerator = type1.Numerator * type2.Numerator,
    
                Denominator = type1.Denominator * type2.Denominator

            };
            return Euklides(rtn.Numerator, rtn.Denominator);
        }
        public static FractionType operator /(FractionType type1, FractionType type2)
        {
            if (type2.Numerator < 0)
            {
                var rtn1 =  new FractionType
                {
                    Numerator = type1.Numerator * type2.Denominator *(-1),
                    Denominator = type1.Denominator * type2.Numerator *(-1)
                };
                return Euklides(rtn1.Numerator, rtn1.Denominator);
            }

            var rtn = new FractionType
            {
                Numerator = type1.Numerator * type2.Denominator,
                Denominator = type1.Denominator * type2.Numerator
            };
            return Euklides(rtn.Numerator, rtn.Denominator);
        }
        public static bool operator >(FractionType type1, FractionType type2)
        {
            return (type1.CompareTo(type2) > 0);
        }

        public static bool operator <(FractionType type1, FractionType type2)
        {
            return (type1.CompareTo(type2) < 0);
        }

        public static bool operator >=(FractionType type1, FractionType type2)
        {
            return (type1.CompareTo(type2) >= 0);
        }

        public static bool operator <=(FractionType type1, FractionType type2)
        {
            return (type1.CompareTo(type2) <= 0);
        }

        private int CompareTo(FractionType other)
        {
            if (other.Numerator == 0)
                return 1;
            if( (other.Numerator ==0 && this.Numerator == 0))
            {
                return 0;
            }
            if (this.Denominator == other.Denominator)
            {
                if (this.Numerator > other.Numerator)
                    return 1;
                else if(this.Numerator < other.Numerator)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (this.Numerator * other.Denominator > this.Denominator * this.Numerator)
                    return 1;
                else if (this.Numerator * other.Denominator > this.Denominator * this.Numerator)
                    return -1;
                else
                    return 0;
            }
        }
        public override string ToString()
        {
            return "["+Numerator + "/" + Denominator+"]";
        }
    }
    public class DataSource
    {
        public int[][] RandomNumbers { get; set; }

        public DataSource(int numberOfRows)
        {
            RandomNumbers = GetRandomNumbers(numberOfRows);
        }

        //public  float[][] GetMatrixWithFloatData(int numberOfRows)
        //{
        //    var floatRandomNumbers = new float[numberOfRows][];
        //
        //    for (int i = 0; i < numberOfRows; i++)
        //    {
        //        floatRandomNumbers[i] = new float[numberOfRows + 1];
        //        for (int j = 0; j < numberOfRows + 1; j++)
        //        {
        //            floatRandomNumbers[i][j] = (float)RandomNumbers[i][j]/65536;
        //        }
        //    }
        //
        //    return floatRandomNumbers;
        //}

        public T[][] GetMatrixData<T>(int numberOfRows)
        {
            switch (typeof(T).Name)
            {
                case "FractionType":
                {
                    FractionType[][] randomNumbers = new FractionType[numberOfRows][];
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        randomNumbers[i] = new FractionType[numberOfRows + 1];
                        for (int j = 0; j < numberOfRows + 1; j++)
                        {
                            randomNumbers[i][j].Numerator = RandomNumbers[i][j];
                            randomNumbers[i][j].Denominator = 65536;
                        }
                    }
                    return (T[][])Convert.ChangeType(randomNumbers, typeof(T[][]));
                }
                case "Double":
                {
                    double[][] randomNumbersDouble = new double[numberOfRows][];
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        randomNumbersDouble[i] = new double[numberOfRows + 1];
                        for (int j = 0; j < numberOfRows + 1; j++)
                        {
                            randomNumbersDouble[i][j] = (double)RandomNumbers[i][j] / 65536;
                        }
                    }
                    return (T[][])Convert.ChangeType(randomNumbersDouble, typeof(T[][]));
                }
                default:
                {
                    float[][] randomNumbersFloat = new float[numberOfRows][];
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        randomNumbersFloat[i] = new float[numberOfRows + 1];
                        for (int j = 0; j < numberOfRows + 1; j++)
                        {
                            randomNumbersFloat[i][j] = (float)RandomNumbers[i][j] / 65536;
                        }
                    }
                    return (T[][])Convert.ChangeType(randomNumbersFloat, typeof(T[][]));
                }
            }
        }

        //public  double[][] GetMatrixWithDoubleData(int numberOfRows)
        //{
        //    var doubleRandomNumbers = new double[numberOfRows][];
        //
        //    for (int i = 0; i < numberOfRows; i++)
        //    {
        //        doubleRandomNumbers[i] = new double[numberOfRows + 1];
        //        for (int j = 0; j < numberOfRows + 1; j++)
        //        {
        //            doubleRandomNumbers[i][j] = (double)RandomNumbers[i][j] / 65536;
        //        }
        //    }
        //
        //    return doubleRandomNumbers;
        //}
        //
        //
        //public FractionType[][] GetMatrixWithSpecialTypeData(int numberOfRows)
        //{
        //    var specialTypeRandomNumbers = new FractionType[numberOfRows][];
        //
        //    for (int i = 0; i < numberOfRows; i++)
        //    {
        //        specialTypeRandomNumbers[i] = new FractionType[numberOfRows + 1];
        //        for (int j = 0; j < numberOfRows + 1; j++)
        //        {
        //            specialTypeRandomNumbers[i][j].Numerator =RandomNumbers[i][j];
        //            specialTypeRandomNumbers[i][j].Denominator = 65536;
        //        }
        //    }
        //
        //    return specialTypeRandomNumbers;
        //}



        private  int[][] GetRandomNumbers(int numberOfRows)
        {
            Random rnd = new Random();
            var randomNumbers =  new int[numberOfRows][];
            for (int i = 0; i < numberOfRows; i++)
            {
                randomNumbers[i] = new int[numberOfRows+1];
                for (int j = 0; j < numberOfRows + 1; j++)
                {
                    randomNumbers[i][j] = rnd.Next(-65536, 65536);
                }
            }

            return randomNumbers;
        }
    }

    public struct ResultSet<T>
    {
        public T ResultNormal;
        public T ResultPartial;
    }

    public struct Q2TimesResults
    {
        public long floutTime;
        public long doubleTime;
    }
}
