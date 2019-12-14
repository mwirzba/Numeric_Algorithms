using System;

namespace Alg2
{
    static class DynamicArithmetic
    {
        public static T Abs<T>(T number)
        {
            if (typeof(T) == typeof(FractionType))
            {
                FractionType tmp = (FractionType)Convert.ChangeType(number, typeof(FractionType));
                if (tmp.Numerator > 0)
                    return number;
                tmp.Numerator *= (-1);
                return (T)Convert.ChangeType(tmp, typeof(T));
            }

            dynamic dynNumber = number;
            if (dynNumber > 0)
                return number;
            return dynNumber * (-1);
        }

        public static T Add<T>(T number1, T number2)
        {
            if (typeof(T) == typeof(FractionType))
            {
                FractionType fracNumb1 = (FractionType)Convert.ChangeType(number1, typeof(FractionType));
                FractionType fracNumb2 = (FractionType)Convert.ChangeType(number2, typeof(FractionType));

                var tmp = fracNumb1 + fracNumb2;
                return (T)Convert.ChangeType(tmp, typeof(T));
            }
            dynamic dynNumber1 = number1;
            dynamic dynNumber2 = number2;
            return dynNumber1 + dynNumber2;
        }


        public static T Subtract<T>(T number1, T number2)
        {
            if (typeof(T) == typeof(FractionType))
            {
                FractionType fracNumb1 = (FractionType)Convert.ChangeType(number1, typeof(FractionType));
                FractionType fracNumb2 = (FractionType)Convert.ChangeType(number2, typeof(FractionType));

                var tmp = fracNumb1 - fracNumb2;
                return (T)Convert.ChangeType(tmp, typeof(T));
            }

            dynamic dynNumber1 = number1;
            dynamic dynNumber2 = number2;
            return dynNumber1 - dynNumber2;
        }

        public static T Divide<T>(T number1, T number2)
        {
            if (typeof(T) == typeof(FractionType))
            {
                FractionType fracNumb1 = (FractionType)Convert.ChangeType(number1, typeof(FractionType));
                FractionType fracNumb2 = (FractionType)Convert.ChangeType(number2, typeof(FractionType));

                var tmp = fracNumb1 / fracNumb2;
                return (T)Convert.ChangeType(tmp, typeof(T));
            }
            dynamic dynNumber1 = number1;
            dynamic dynNumber2 = number2;
            return dynNumber1 / dynNumber2;
        }


        public static T Multiply<T>(T number1, T number2)
        {
            if (typeof(T) == typeof(FractionType))
            {
                FractionType fracNumb1 = (FractionType)Convert.ChangeType(number1, typeof(FractionType));
                FractionType fracNumb2 = (FractionType)Convert.ChangeType(number2, typeof(FractionType));

                var tmp = fracNumb1 * fracNumb2;
                return (T)Convert.ChangeType(tmp, typeof(T));
            }
            dynamic dynNumber1 = number1;
            dynamic dynNumber2 = number2;
            return dynNumber1 * dynNumber2;
        }

        public static bool CompareBigger<T>(T number1, T number2)
        {
            if (typeof(T) == typeof(FractionType))
            {
                FractionType fracNumb1 = (FractionType)Convert.ChangeType(number1, typeof(FractionType));
                FractionType fracNumb2 = (FractionType)Convert.ChangeType(number2, typeof(FractionType));

                return fracNumb1 > fracNumb2;
            }
            dynamic dynNumber1 = number1;
            dynamic dynNumber2 = number2;
            return dynNumber1 > dynNumber2;
        }
    }
}
