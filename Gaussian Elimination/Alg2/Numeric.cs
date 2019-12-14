using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg2
{
    /*public struct Number<T>
    {

        public T value { get; set; }

        public Number(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Big IF chain to decide exactly which ICalculator needs to be created
        /// Since the ICalculator is cached, this if chain is executed only once per type
        /// </summary>
        /// <returns>The type of the calculator that needs to be created</returns>

        public static Type GetCalculatorType()
        {
            Type tType = typeof(T);
            Type calculatorType = null;
            if (tType == typeof(double))
            {
                calculatorType = typeof(DoubleCalculator);
            }
            else if (tType == typeof(float))
            {
                calculatorType = typeof(FloatCalculator);
            }
            else
            {
                throw new InvalidCastException(String.Format("Unsupported Type- Type {0}" +
                      " does not have a partner implementation of interface " +
                      "ICalculator<T> and cannot be used in generic " +
                      "arithmetic using type Number<T>", tType.Name));
            }
            return calculatorType;
        }

        /// <summary>

        /// a static field to store the calculator after it is created
        /// this is the caching that is refered to above
        /// </summary>
        private static ICalculator<T> fCalculator = null;

        /// <summary>

        /// Singleton pattern- only one calculator created per type
        /// 
        /// </summary>
        public static ICalculator<T> Calculator
        {
            get
            {
                if (fCalculator == null)
                {
                    MakeCalculator();
                }
                return fCalculator;
            }
        }

        /// <summary>

        /// Here the actual calculator is created using the system activator
        /// </summary>

        public static void MakeCalculator()
        {
            Type calculatorType = GetCalculatorType();
            fCalculator = Activator.CreateInstance(calculatorType) as ICalculator<T>;
        }

        /// These methods can be called by the applications
        /// programmer if no operator overload is defined
        /// If an operator overload is defined these methods are not needed
        #region operation methods

        public static T Sum(T a, T b)
        {
            return Calculator.Sum(a, b);
        }

        public static T Difference(T a, T b)
        {
            return Calculator.Difference(a, b);
        }

        public static int Compare(T a, T b)
        {
            return Calculator.Compare(a, b);
        }

        public static T Multiply(T a, T b)
        {
            return Calculator.Multiply(a, b);
        }

        public static T Divide(T a, T b)
        {
            return Calculator.Divide(a, b);
        }


        #endregion

        /// These operator overloads make doing the arithmetic easy.
        /// For custom operations, an operation method
        /// may be the only way to perform the operation
        #region Operators

        //IMPORTANT:  The implicit operators
        //allows an object of type Number<T> to be
        //easily and seamlessly wrap an object of type T. 
        public static implicit operator Number<T>(T a)
        {
            return new Number<T>(a);
        }

        //IMPORTANT:  The implicit operators allows 
        //an object of type Number<T> to be
        //easily and seamlessly wrap an object of type T. 
        public static implicit operator T(Number<T> a)
        {
            return a.value;
        }

        public static Number<T>
               operator +(Number<T> a, Number<T> b)
        {
            return Calculator.Sum(a.value, b.value);
        }

        public static Number<T>
               operator -(Number<T> a, Number<T> b)
        {
            return Calculator.Difference(a, b);
        }

        public static bool operator >(Number<T> a, Number<T> b)
        {
            return Calculator.Compare(a, b) > 0;
        }

        public static bool operator <(Number<T> a, Number<T> b)
        {
            return Calculator.Compare(a, b) < 0;
        }

        public static Number<T>
               operator *(Number<T> a, Number<T> b)
        {
            return Calculator.Multiply(a, b);
        }

        public static Number<T>
               operator /(Number<T> a, Number<T> b)
        {
            return Calculator.Divide(a, b);
        }
        #endregion
    }()*/
}
