using System.Numerics;
namespace Task6._2
{
    public static class Gcd
    {
        public static Task<BigInteger> GcdAsync(BigInteger num1, BigInteger num2)
        {
            var task = new TaskCompletionSource<BigInteger>();
            new Thread(GetGcd).Start();
            return task.Task;

            void GetGcd()
            {
                var res1 = Task<BigInteger[]>.Run(() => FAsyncLib.FactorizationAsync(num1).Result.ToArray());
                var res2 = Task<BigInteger[]>.Run(() => FAsyncLib.FactorizationAsync(num2).Result.ToArray());
                BigInteger result = 1;
                int i = 0, j = 0;
                Task.WaitAll(res1, res2);
                var list1 = res1.Result;
                var list2 = res2.Result;
                var l1 = list1.Length;
                var l2 = list2.Length;
                while (i < l1 && j < l2)
                {
                    if (list1[i] < list2[j])
                    {
                        i++;
                    }
                    else if (list1[i] > list2[j])
                    {
                        j++;
                    }
                    else
                    {
                        result *= list1[i];
                        i++;
                        j++;
                    }
                    }
                task.SetResult(result);
            }
        }
    }
}

