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

            Inchiriere inchiriere = new Inchiriere();

            int nrInchirieri;
            int nextID = 1;
            Inchiriere[] inchirieri_temp = fisierTest.GetInchirieri(out nrInchirieri);
            if (nrInchirieri != 0)
                nextID = inchirieri_temp[nrInchirieri - 1].IdInchiriere;

            string optiune;
            do
            {
                Console.Clear();
                Console.WriteLine("I. Introducere informatii inchiriere");
                Console.WriteLine("A. Afisare inchiriere");
                Console.WriteLine("F. Afisare inchirieri din fisier");
                Console.WriteLine("S. Salvare inchiriere in fisier");
                Console.WriteLine("K. Cauta inchiriere dupa ID");
                Console.WriteLine("L. Cauta inchiriere dupa nume");
                Console.WriteLine("X. Inchidere program");
                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine();
                Console.WriteLine();
                switch (optiune.ToUpper())
                {
                    case "I":
                        bool valid;

                        Console.Write("Introdu nume : ");
                        string nume = Console.ReadLine();
                        Console.Write("Introdu prenume : ");
                        string prenume = Console.ReadLine();

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
                        
                        inchirieri_temp = fisierTest.GetInchirieri(out nrInchirieri);
                        if (nrInchirieri != 0)
                            nextID = inchirieri_temp[nrInchirieri - 1].IdInchiriere;

                        inchiriere = new Inchiriere(nextID, idMasina, nume, prenume, pret, dataPreluare, dataReturnare);
                        Console.WriteLine("\nInchirierea a fost salvata temporar.");
                        Console.ReadKey();

                        break;
                    case "A":
                        string infoInchiriere = inchiriere.Info();
                        Console.WriteLine("Inchirierea {0}", infoInchiriere);
                        Console.ReadKey();

                        break;
                    case "F":
                        Inchiriere[] inchirieri = fisierTest.GetInchirieri(out nrInchirieri);
                        AfisareInchirieri(inchirieri, nrInchirieri);
                        Console.ReadKey();

                        break;
                    case "S":
                        inchirieri_temp = fisierTest.GetInchirieri(out nrInchirieri);
                        if (nrInchirieri != 0)
                            nextID = inchirieri_temp[nrInchirieri - 1].IdInchiriere;

                        inchiriere.IdInchiriere = nrInchirieri + 1;

                        //adaugare student in fisier
                        fisierTest.AddInchiriere(inchiriere);
                        Console.WriteLine("\nInchirierea a fost salvata in fisier.");
                        Console.ReadKey();

                        break;
                    case "K":
                        Console.Write("Introdu ID : ");
                        int id_cautat = int.Parse(Console.ReadLine());

                        Console.WriteLine();

                        Inchiriere output_caurate_id = fisierTest.GetInchiriere(id_cautat);
                        if (output_caurate_id.IdInchiriere != 0)
                            Console.WriteLine(fisierTest.GetInchiriere(id_cautat).Info());
                        else
                            Console.WriteLine("Nu s-a gasit nicio inchiriere.");
                        Console.ReadKey();

                        break;
                    case "L":
                        Console.Write("Introdu nume : ");
                        string nume_cautat = Console.ReadLine();
                        Console.Write("Introdu prenume : ");
                        string prenume_cautat = Console.ReadLine();

                        Console.WriteLine();

                        Inchiriere output_cautare_nume = fisierTest.GetInchiriere(nume_cautat, prenume_cautat);
                        if (output_cautare_nume.IdInchiriere != 0)
                            Console.WriteLine(fisierTest.GetInchiriere(nume_cautat, prenume_cautat).Info());
                        else
                            Console.WriteLine("Nu s-a gasit nicio inchiriere.");
                        Console.ReadKey();

                        break;
                    case "X":

                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");

                        break;
                }
            } while (optiune.ToUpper() != "X");

            Console.ReadKey();
        }
        public static void AfisareInchirieri(Inchiriere[] inchirieri, int nrInchirieri)
        {
            Console.WriteLine("Inchirierile sunt:");
            for (int contor = 0; contor < nrInchirieri; contor++)
                Console.WriteLine(inchirieri[contor].Info());
        }
    }
}
