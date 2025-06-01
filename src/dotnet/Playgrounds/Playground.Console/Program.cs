using Microsoft.Extensions.DependencyInjection;

namespace Playground.Consolas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sc = new ServiceCollection();
            var t1 = new Type1();
            var t2 = new Type2();
            sc.AddSingleton<Inteface1>(t1);
            sc.AddSingleton<Inteface1>(t2);
            sc.AddSingleton<Inteface1>(t2);
            var pr = sc.BuildServiceProvider();
            var srs = pr.GetServices<Inteface1>().ToArray();
            foreach (var item in srs)
            {
                Console.WriteLine(item.GetType().AssemblyQualifiedName);
            }

            var q1 = sc.Where(x => x.ServiceType == typeof(Inteface1)).ToArray();
            foreach (var item in q1)
            {
                Console.WriteLine(item.ImplementationType);
            }
            sc.RemoveAt(0);
        }
    }

    public interface Inteface1
    {

    }

    public class Type1 : Inteface1
    {

    }
    public class Type2 : Inteface1
    {

    }
}
