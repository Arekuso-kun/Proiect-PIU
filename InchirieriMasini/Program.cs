using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InchirieriMasiniLibrary;

namespace InchirieriMasini
{
    class Program
    {
        static void Main(string[] args)
        {
            Inchiriere test = new Inchiriere();
            Console.WriteLine(test.GetTest());
            Console.ReadKey();
        }
    }
}
