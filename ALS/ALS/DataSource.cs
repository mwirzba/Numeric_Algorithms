using ALS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

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
        public double[][] RandomNumbers { get; set; }
        private readonly long _numberOfRows;
        private readonly long  _numberOfColumns;
        private List<string> _customersList;
        public DataSource(long numberOfRows,long numberOfColumns)
        {
            _numberOfRows = numberOfRows;
            _numberOfColumns = numberOfColumns;
            _customersList = new List<string>();
            //RandomNumbers = GetRandomNumbers(numberOfRows,numberOfColumns);
        }

        #region FileFuncion
        public List<ProductWithRatings> GetDataFromFile(long startId,long endId)
        {
            List<ProductWithRatings> products = new List<ProductWithRatings>();
            long numberCounter = startId;
            bool start = false;
            bool correctCat = false;
            int id = 0;
            string file = "";
            using (StreamReader sr = new StreamReader("amazon-meta.txt"))
            {
                while (numberCounter < endId)
                {                  
                    if (numberCounter == startId)
                        start = true;
                    var line = sr.ReadLine();            
                    string[] idLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (idLine.Length > 1  &&  idLine[0] == "Id:")
                    {
                        id = int.Parse(idLine[1]);
                        numberCounter++;
                    }
                    if (idLine.Length > 1 && idLine[0]=="group:")
                    {
                        if (idLine[1] == "Book")
                            correctCat = true;
                        else
                            correctCat = false;
                    }
                    
                    if (start && correctCat)
                    {

                        if (line.Contains("reviews:"))
                        {
                            string[] ratingHeader = line.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                            int numberOfRatings = int.Parse(ratingHeader[4]);
                            if (numberOfRatings > 5)
                            {
                                var ratings = GetCustomersRatings(sr, numberOfRatings);
                                if (ratings.Any())
                                    products.Add(new ProductWithRatings { ProductId = id, customersRatings = ratings });
                            }
                        }
                    }
                }
            }
            
            return products;
        }

        public double?[][] PutResultsToMatrix(List<ProductWithRatings> products)
        {
           
            List<CustomerWithRatings> ratings = new List<CustomerWithRatings>();
            CustomerRating customerRating = null;
            for (int i = 0; i < _customersList.Count; i++)
            {
                for (int j = 0; j < products.Count; j++)
                {
                    customerRating = GetCustomerRatingForProduct(products[j], _customersList[i]);
                    if (customerRating != null)
                    {
                        if (ratings.FirstOrDefault(c => c.Customer == customerRating.Customer) == null)
                        {
                            ratings.Add(new CustomerWithRatings { Customer = customerRating.Customer, ProductsRatings = new Dictionary<long, double>() });
                            ratings.Where(r => r.Customer == customerRating.Customer).Select(s => { s.ProductsRatings.Add(products[j].ProductId, customerRating.Rating); return s; }).ToList();
                        }
                        else
                        {
                            ratings.Where(r => r.Customer == customerRating.Customer).Select(s => { s.ProductsRatings.Add(products[j].ProductId, customerRating.Rating); return s; }).ToList();
                        }
                    }
                }   
            }

            var customersWithMoreReviews = ratings.Where(c => c.ProductsRatings.Count > 2 && c.Customer != "ATVPDKIKX0DER").ToList();
            var productsId = GetProductIdList(customersWithMoreReviews);


            double?[][] dataMatix = new double?[customersWithMoreReviews.Count][];
            for (int i = 0; i < customersWithMoreReviews.Count; i++)
            {
                dataMatix[i] = new double?[productsId.Count];
                for (int j = 0; j < productsId.Count; j++)
                {
                    dataMatix[i][j] = null;
                    
                    if (customersWithMoreReviews[i].ProductsRatings.ContainsKey(productsId[j]))
                    {
                        dataMatix[i][j] = customersWithMoreReviews[i].ProductsRatings.First(c => c.Key == productsId[j]).Value;
                    }
                }
            }

            return dataMatix;
        }
        private CustomerRating GetCustomerRatingForProduct(ProductWithRatings product, string customerId)
        {
            var rtn = product.customersRatings.Where(r => r.Customer == customerId).FirstOrDefault();
            return rtn;
        }

        #endregion

  

        private List<long> GetProductIdList(List<CustomerWithRatings>  customerWithRatings)
        {
            var productsId = new List<long>();
            foreach (var cust in customerWithRatings)
            {
                foreach (var rating in cust.ProductsRatings)
                {
                    if(!productsId.Contains(rating.Key))
                    {
                        productsId.Add(rating.Key);
                    }
                }    
            }
            return productsId.OrderBy(c=>c).ToList();
        }
        private List<CustomerRating> GetCustomersRatings(StreamReader strReader, int numberOfRatings)
        {
            List<CustomerRating> customersRatings = new List<CustomerRating>();
            for (int i = 0; i < numberOfRatings; i++)
            {
                string[] splitRatingLine = strReader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (splitRatingLine.Length > 2)
                    if (customersRatings.FirstOrDefault(c => c.Customer == splitRatingLine[2]) == null)
                    {
                        if (!_customersList.Contains(splitRatingLine[2]))
                            _customersList.Add(splitRatingLine[2]);
                        customersRatings.Add(new CustomerRating { Customer = splitRatingLine[2], Rating = double.Parse(splitRatingLine[4]) });
                    }
            }
            return customersRatings;
        }

        public void WriteToCsv(List<double> results)
        {
            using (StreamWriter file = File.AppendText(Directory.GetCurrentDirectory() + "//resultFunkcja.csv"))
            {
                file.WriteLine("Results");
                foreach (var result in results)
                {
                   file.WriteLine(result);
                }
            }
        }

        public void WriteResultErrosToCsv(int d,double sigma,int numberOfDiff,int numberOfBigDiff,int total,long elapsedMs)
        {
            using (StreamWriter file = File.AppendText(Directory.GetCurrentDirectory() + "//zczasemmalailosc.csv"))
            {
                file.WriteLine(d+";"+sigma+";"+ numberOfDiff + ";"+ numberOfBigDiff+";"+total+";"+elapsedMs);
            }
        }

        //public void WriteTFile(List<>)




        public double[][] GetMatrixWithDoubleData(int _numberOfRows)
        {
            var doubleRandomNumbers = new double[_numberOfRows][];

            for (int i = 0; i < _numberOfRows; i++)
            {
                doubleRandomNumbers[i] = new double[_numberOfRows + 1];
                for (int j = 0; j < _numberOfRows + 1; j++)
                {
                    doubleRandomNumbers[i][j] = (double)RandomNumbers[i][j];
                }
            }

            return doubleRandomNumbers;
        }

        public double[][] GetRandomNumbers(long numberOfRows, long numberOfColumns)
        {
            Random rnd = new Random();
            var randomNumbers = new double[numberOfRows][];
            for (long i = 0; i < numberOfRows; i++)
            {
                randomNumbers[i] = new double[numberOfColumns];
                for (long j = 0; j < numberOfColumns; j++)
                {
                    randomNumbers[i][j] = Math.Round(rnd.NextDouble()*5, 1);
                }
            }

            return randomNumbers;
        }

        public void CopyMatrix(double[][] dest, double[][] source)
        {
            for (int i = 0; i < dest.Length; i++)
            {
                dest[i] = new double[source[i].Length];
                Array.Copy(source[i], dest[i],dest[i].Length);
            }
        }

        public void PrintMatrix(double[][] matrix)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++) 
                    Console.Write("[ " + Math.Round(matrix[i][j], 2)  + "] ");

                Console.WriteLine("NEW");
            }
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
        /*
        public T[][] GetMatrixData<T>()
        {
            switch (typeof(T).Name)
            {
                case "FractionType":
                {
                    FractionType[][] randomNumbers = new FractionType[_numberOfRows][];
                    for (int i = 0; i < _numberOfRows; i++)
                    {
                        randomNumbers[i] = new FractionType[_numberOfRows + 1];
                        for (int j = 0; j < _numberOfRows + 1; j++)
                        {
                            randomNumbers[i][j].Numerator = RandomNumbers[i][j];
                            randomNumbers[i][j].Denominator = 65536;
                        }
                    }
                    return (T[][])Convert.ChangeType(randomNumbers, typeof(T[][]));
                }
                case "Double":
                {
                    double[][] randomNumbersDouble = new double[_numberOfRows][];
                    for (int i = 0; i < _numberOfRows; i++)
                    {
                        randomNumbersDouble[i] = new double[_numberOfRows + 1];
                        for (int j = 0; j < _numberOfRows + 1; j++)
                        {
                            randomNumbersDouble[i][j] = (double)RandomNumbers[i][j] / 65536;
                        }
                    }
                    return (T[][])Convert.ChangeType(randomNumbersDouble, typeof(T[][]));
                }
                default:
                {
                    float[][] randomNumbersFloat = new float[_numberOfRows][];
                    for (int i = 0; i < _numberOfRows; i++)
                    {
                        randomNumbersFloat[i] = new float[_numberOfRows + 1];
                        for (int j = 0; j < _numberOfRows + 1; j++)
                        {
                            randomNumbersFloat[i][j] = (float)RandomNumbers[i][j] / 65536;
                        }
                    }
                    return (T[][])Convert.ChangeType(randomNumbersFloat, typeof(T[][]));
                }
            }
        }
        */

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
