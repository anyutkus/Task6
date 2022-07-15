using System.Numerics;

namespace Task6._2
{
    public class FLib
    {
        public static List<BigInteger> Factorization(BigInteger number)
        {
            List<BigInteger> list = new();
            if (number < 2)
            {
                throw new ArgumentOutOfRangeException();
            }
            PrimeNumbers(number);
            return list;

            void PrimeNumbers(BigInteger num)
            {
                var num1 = num;
                var isPrime = true;
                for (BigInteger i = 2; i*i <= num; i++)
                {
                    while (num1 % i == 0)
                    {
                        isPrime = false;
                        list.Add(i);
                        num1 /= i;
                    }
                }

                if (num1 > 1)
                {
                    list.Add(num1);
                }

                if (isPrime)
                {
                    list.Add(num);
                }
            }
        }
    }
}

