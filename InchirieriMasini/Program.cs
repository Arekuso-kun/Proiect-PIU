using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InchirieriMasiniLibrary;
using InchirieriMasiniDataManagement;

namespace InchirieriMasini
{
    class Program
    {
        static void Main(string[] args)
        {
            AdministrareInchirieri fisierTest = new AdministrareInchirieri("data.txt");
            Inchiriere inchiriere = new Inchiriere(65243, 12312, "Vasile", "Popescu");
            fisierTest.AddInchiriere(inchiriere);
            int nrInchirieri = 1;
            Console.Write("Introdu nume : ");
            string nume = Console.ReadLine();
            Console.Write("Introdu prenume : ");
            string prenume = Console.ReadLine();
            Console.Write("Introdu ID inchiriere : ");
            int idInchiriere = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introdu ID masina : ");
            int idMasina = Convert.ToInt32(Console.ReadLine());
            inchiriere = new Inchiriere(idInchiriere, idMasina, nume, prenume);
            fisierTest.AddInchiriere(inchiriere);
            nrInchirieri++;
            Inchiriere[] inchirieri = fisierTest.GetInchirieri(out nrInchirieri);
            AfisareInchirieri(inchirieri, nrInchirieri);
            Console.ReadKey();
        }
        public static void AfisareInchirieri(Inchiriere[] inchirieri, int nrInchirieri)
        {
            Console.WriteLine("Inchirierile sunt:");
            for (int contor = 0; contor < nrInchirieri; contor++)
            {
                string infoStudent = string.Format("#{0}: {2} {3} a inchiriat masina #{1}",
                   inchirieri[contor].GetIDinchiriere(),
                   inchirieri[contor].GetIDmasina(),
                   inchirieri[contor].GetNume(),
                   inchirieri[contor].GetPrenume());

            Console.WriteLine(infoStudent);
            }
        }
    }
}
