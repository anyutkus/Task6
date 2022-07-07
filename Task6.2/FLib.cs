using System.Numerics;

namespace Task6._2
{
    public class FLib
    {
        public static List<BigInteger> Factorization(BigInteger N)
        {
            List<BigInteger> list = new();
            if (N < 2)
            {
                throw new ArgumentOutOfRangeException();
            }
            PrimeNumbers(N);
            return list;

            void PrimeNumbers(BigInteger number)
            {
                var num = Math.Ceiling(Math.Exp(BigInteger.Log(number) / 2));
                var isPrime = true;

                for (var i = 2; i <= num; i++)
                {
                    while (number % i == 0)
                    {
                        isPrime = false;
                        list.Add(i);
                        number /= i;
                    }
                    if (i == num && number > 1)
                    {
                        list.Add(number);
                    }
                }

                if (isPrime)
                {
                    list.Add(number);
                }
            }
        }
    }
}

