using System.Numerics;

namespace Task6._2
{
    class Program
    {
        static void Main()
        {
            var factorization = FLib.Factorization(274372696650);
            Console.WriteLine("Normal:");
            foreach (var f in factorization)
            {
                Console.WriteLine(f);
            }

            var tsk = FAsyncLib.FactorizationAsync(367952359391);
            Console.WriteLine($"Async:");
            foreach(var a in tsk.Result)
            {
                Console.WriteLine(a);
            }

            var t3 = Gcd.GcdAsync(400, 250);
            Console.WriteLine($"GCD: {t3.Result}");

            var t = Gcd.GcdAsync(23224233457368, 25232137423893);
            Console.WriteLine($"GCD: {t.Result}");

            var t2 = Gcd.GcdAsync(1234567890, 63018038201);
            Console.WriteLine($"GCD: {t2.Result}");
            
        }
    }
}