
namespace Task6._1
{
    class Program
    {
        static void Main()
        {
            Cache c1 = new(3);
            c1.Set("key1", "object1");

            Thread.Sleep(3000);

            c1.Set("key2", "object2");
            Thread.Sleep(2000);
            Console.WriteLine(c1.ToString());
            c1.Set("key2", "onj");
            c1.Set("key3", "object3");
            Thread.Sleep(2000);
            Console.WriteLine(c1.ToString());
            Console.WriteLine("get");
            Console.WriteLine(c1.Get("key1").ToString());
            Console.WriteLine(c1.Remove("key1"));
            Console.WriteLine(c1.ToString());
            Thread.Sleep(2000);
            c1.Set("key4", "object4");
            Console.WriteLine(c1.ToString());
            Thread.Sleep(1500);
            c1.Set("key5", "object5");
            Console.WriteLine(c1.ToString());
            Thread.Sleep(8000);
            Console.WriteLine(c1.ToString());
        }
    }
}