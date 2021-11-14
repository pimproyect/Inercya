using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRandom
{
    class RandomInt32
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            Console.WriteLine("Generating 10 random numbers:");

            List<int> numerosSalidos = new List<int>();
            for (int ctr = 0; ctr < 100000; ctr++)
            {
                int numero = rnd.Next();
                if (numerosSalidos.Count > 0)
                {
                    if (numerosSalidos.Contains(numero))
                    {
                        ctr--;
                    }
                    else
                    {
                        numerosSalidos.Add(numero);
                    }
                }
                else
                {
                    numerosSalidos.Add(numero);
                }
            }
            numerosSalidos.Sort((x, y) => x.CompareTo(y));
            numerosSalidos.ForEach(item => { Console.WriteLine(item + " "); });
            Console.ReadLine();
        }
    }
}
