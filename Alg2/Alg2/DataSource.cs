using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Alg2
{

    public struct FractionType
    {
        public BigInteger Numerator { get; set; }
        public BigInteger Denominator { get; set; }

        public static FractionType operator +(FractionType type1, FractionType type2)
        {
            return new FractionType
            {
                Numerator = type1.Denominator == type2.Denominator ?  type1.Numerator + type2.Numerator  
                    : type1.Numerator * type2.Denominator + type1.Denominator * type2.Numerator,
                Denominator = type1.Denominator == type2.Denominator ? type2.Denominator 
                    : type1.Denominator * type2.Denominator

            };
        }

        public static FractionType operator -(FractionType type1, FractionType type2)
        {
            return new FractionType
            {
                Numerator = type1.Denominator == type2.Denominator ? type1.Numerator - type2.Numerator
                    : type1.Numerator * type2.Denominator - type1.Denominator * type2.Numerator,
                Denominator = type1.Denominator == type2.Denominator ? type2.Denominator 
                    : type1.Denominator * type2.Denominator

            };
        }

        public static FractionType operator *(FractionType type1, FractionType type2)
        {
            return new FractionType
            {
                Numerator = type1.Numerator * type2.Numerator,
    
                Denominator = type1.Denominator * type2.Denominator

            };
        }
        public static FractionType operator /(FractionType type1, FractionType type2)
        {
            if (type2.Numerator < 0)
            {
                return new FractionType
                {
                    Numerator = type1.Numerator * type2.Denominator *(-1),
                    Denominator = type1.Denominator * type2.Numerator *(-1)
                };
            }
            return new FractionType
            {
                Numerator = type1.Numerator * type2.Denominator,
                Denominator = type1.Denominator * type2.Numerator
            };
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

        public  float[][] GetMatrixWithFloatData(int numberOfRows)
        {
            var floatRandomNumbers = new float[numberOfRows][];

            for (int i = 0; i < numberOfRows; i++)
            {
                floatRandomNumbers[i] = new float[numberOfRows + 1];
                for (int j = 0; j < numberOfRows + 1; j++)
                {
                    floatRandomNumbers[i][j] = (float)RandomNumbers[i][j]/65536;
                }
            }

            return floatRandomNumbers;
        }

        public  double[][] GetMatrixWithDoubleData(int numberOfRows)
        {
            var doubleRandomNumbers = new double[numberOfRows][];

            for (int i = 0; i < numberOfRows; i++)
            {
                doubleRandomNumbers[i] = new double[numberOfRows + 1];
                for (int j = 0; j < numberOfRows + 1; j++)
                {
                    doubleRandomNumbers[i][j] = (double)RandomNumbers[i][j] / 65536;
                }
            }

            return doubleRandomNumbers;
        }


        public FractionType[][] GetMatrixWithSpecialTypeData(int numberOfRows)
        {
            var specialTypeRandomNumbers = new FractionType[numberOfRows][];

            for (int i = 0; i < numberOfRows; i++)
            {
                specialTypeRandomNumbers[i] = new FractionType[numberOfRows + 1];
                for (int j = 0; j < numberOfRows + 1; j++)
                {
                    specialTypeRandomNumbers[i][j].Numerator =RandomNumbers[i][j];
                    specialTypeRandomNumbers[i][j].Denominator = 65536;
                }
            }

            return specialTypeRandomNumbers;
        }



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
}
