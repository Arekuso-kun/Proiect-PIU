using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InchirieriMasiniLibrary;
using InchirieriMasiniDataManagement;
using System.Globalization;

namespace InchirieriMasini
{
    class Program
    {
        static void Main(string[] args)
        {
            AdministrareInchirieri fisierTest = new AdministrareInchirieri("data.txt");

            DateTime date1 = new DateTime(2023, 3, 31, 18, 0, 0); // March 31, 2023 6:00:00 PM
            DateTime date2 = new DateTime(2023, 4, 1, 10, 0, 0); // April 1, 2023 10:00:00 AM
            Inchiriere inchiriere = new Inchiriere(65243, 12312, "Vasile", "Popescu", 3000.00f, date1, date2);
            fisierTest.AddInchiriere(inchiriere);
            int nrInchirieri = 1;

            bool valid = false;

            Console.Write("Introdu nume : ");
            string nume = Console.ReadLine();
            Console.Write("Introdu prenume : ");
            string prenume = Console.ReadLine();
            Console.Write("Introdu ID inchiriere : ");
            int idInchiriere = int.Parse(Console.ReadLine());

            int idMasina = 0;
            valid = false;
            while (!valid)
            {
                Console.Write("Introdu ID masina : ");
                string inputString = Console.ReadLine();
                valid = int.TryParse(inputString, out idMasina);

                if (!valid)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }

            float pret = 0.0f;
            valid = false;
            while (!valid)
            {
                Console.Write("Introdu pret : ");
                string inputString = Console.ReadLine();
                valid = float.TryParse(inputString, out pret);

                if (!valid)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }

            DateTime dataPreluare = DateTime.MinValue;
            valid = false;
            string format = "dd/MM/yyyy HH:mm:ss";
            while (!valid)
            {
                Console.Write("Introdu data preluare ( dd/MM/yyyy HH:mm:ss ) : ");
                string inputString = Console.ReadLine();
                valid = DateTime.TryParseExact(inputString, format, null, System.Globalization.DateTimeStyles.None, out dataPreluare);
                if (!valid)
                {
                    Console.WriteLine("Invalid format. Please try again.");
                }
            }

            DateTime dataReturnare = DateTime.MinValue;
            valid = false;
            while (!valid)
            {
                Console.Write("Introdu data returnare ( dd/MM/yyyy HH:mm:ss ) : ");
                string inputString = Console.ReadLine();
                valid = DateTime.TryParseExact(inputString, format, null, System.Globalization.DateTimeStyles.None, out dataReturnare);
                if (!valid)
                {
                    Console.WriteLine("Invalid format. Please try again.");
                }
            }

            inchiriere = new Inchiriere(idInchiriere, idMasina, nume, prenume, pret, dataPreluare, dataReturnare);
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
                   inchirieri[contor].IdInchiriere,
                   inchirieri[contor].IdMasina,
                   inchirieri[contor].Nume,
                   inchirieri[contor].Prenume,
                   inchirieri[contor].Pret,
                   inchirieri[contor].DataPreluare,
                   inchirieri[contor].DataReturnare);

            Console.WriteLine(infoStudent);
            }
        }
    }
}
