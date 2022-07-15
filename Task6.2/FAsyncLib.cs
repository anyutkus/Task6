using System.Numerics;
namespace Task6._2
{
    public static class FAsyncLib
    {
        public static Task<List<BigInteger>> FactorizationAsync(BigInteger number)
        {
            var task = new TaskCompletionSource<List<BigInteger>>();
            new Thread(GetPrime).Start();
            return task.Task;

            void GetPrime()
            {
                try
                {
                    var result = FLib.Factorization(number);
                    task.SetResult(result);
                }
                catch (Exception ex)
                {
                    task.SetException(ex);
                }
            }
        }
    }
}

