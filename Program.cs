using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_PIU
{
    class Program
    {
        static void Main(string[] args)
        {
            CMasina Honda = new CMasina();
            Console.WriteLine(Honda.GetTest());
            Console.ReadKey();
        }
    }
}
