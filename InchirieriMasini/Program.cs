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
            string[][] words = new string[26][];

            if(args.Length != 0) 
            { 
                for (int i = 0; i < 26; i++)
                    words[i] = new string[0];

                for (int i = 1; i < args.Length; i++)
                {
                    char firstLetter = Char.ToLower(args[i][0]);
                    int index = firstLetter - 'a';

                    if (index >= 0 && index < 26)
                    {
                        Array.Resize(ref words[index], words[index].Length + 1);
                        words[index][words[index].Length - 1] = args[i];
                    }
                }

                for (int i = 0; i < 26; i++)
                {
                    Console.Write((char)(i + 'A') + ": ");
                    Console.WriteLine(string.Join(", ", words[i]));
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

            AdministrareInchirieri adminInchirieri = new AdministrareInchirieri("Inchirieri.txt");
            AdministrareMasini adminMasini = new AdministrareMasini("Masini.txt");

            Inchiriere inchiriere = new Inchiriere();
            Masina masina = new Masina();

            int nrInchirieri = 0;
            int nrMasini = 0;
            int nextIdInchiriere = 1;
            int nextIdMasina = 1;

            update_nr_nextId_inchirieri(ref nrInchirieri, ref nextIdInchiriere, adminInchirieri);
            update_nr_nextId_masini(ref nrMasini, ref nextIdMasina, adminMasini);

            string optiune;
            do
            {
                Console.Clear();
                Console.WriteLine(" ---- Inchirieri ---- ");
                Console.WriteLine("I. Introducere informatii inchiriere");
                Console.WriteLine("A. Afisare inchiriere");
                Console.WriteLine("F. Afisare inchirieri din fisier");
                Console.WriteLine("S. Salvare inchiriere in fisier");
                Console.WriteLine("K. Cauta inchiriere dupa ID");
                Console.WriteLine("L. Cauta inchiriere dupa nume");
                Console.WriteLine("\n ---- Masini ---- ");
                Console.WriteLine("1. Introducere informatii masina");
                Console.WriteLine("2. Afisare masina");
                Console.WriteLine("3. Afisare masini din fisier");
                Console.WriteLine("4. Salvare masina in fisier");
                Console.WriteLine("5. Cauta masina dupa ID");
                Console.WriteLine("6. Cauta masina dupa marca si model");
                Console.WriteLine("\nX. Inchidere program");
                Console.WriteLine("\nAlegeti o optiune...");
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

                        update_nr_nextId_inchirieri(ref nrInchirieri, ref nextIdInchiriere, adminInchirieri);

                        inchiriere = new Inchiriere(nextIdInchiriere, idMasina, nume, prenume, dataPreluare, dataReturnare);
                        Console.WriteLine("\nInchirierea a fost salvata temporar.");
                        Console.ReadKey();

                        break;
                    case "A":
                        string infoInchiriere = inchiriere.Info();
                        if (inchiriere.IdInchiriere != 0)
                            Console.WriteLine("Inchirierea {0}", infoInchiriere);
                        else
                            Console.WriteLine("Nu a fost introdusa nicio inchiriere.");
                        Console.ReadKey();

                        break;
                    case "F":
                        Inchiriere[] inchirieri = adminInchirieri.GetInchirieri(out nrInchirieri);
                        AfisareInchirieri(inchirieri, nrInchirieri);
                        Console.ReadKey();

                        break;
                    case "S":
                        update_nr_nextId_inchirieri(ref nrInchirieri, ref nextIdInchiriere, adminInchirieri);

                        inchiriere.IdInchiriere = nextIdInchiriere;

                        //adaugare student in fisier
                        adminInchirieri.AddInchiriere(inchiriere);
                        Console.WriteLine("\nInchirierea a fost salvata in fisier.");
                        Console.ReadKey();

                        break;
                    case "K":
                        Console.Write("Introdu ID : ");
                        int id_cautat = int.Parse(Console.ReadLine());

                        Console.WriteLine();

                        Inchiriere output_caurate_id = adminInchirieri.GetInchiriere(id_cautat);
                        if (output_caurate_id.IdInchiriere != 0)
                            Console.WriteLine(adminInchirieri.GetInchiriere(id_cautat).Info());
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

                        Inchiriere output_cautare_nume = adminInchirieri.GetInchiriere(nume_cautat, prenume_cautat);
                        if (output_cautare_nume.IdInchiriere != 0)
                            Console.WriteLine(adminInchirieri.GetInchiriere(nume_cautat, prenume_cautat).Info());
                        else
                            Console.WriteLine("Nu s-a gasit nicio inchiriere.");
                        Console.ReadKey();

                        break;
                    case "1":
                        Console.Write("Introdu marca : ");
                        string marca = Console.ReadLine();
                        Console.Write("Introdu model : ");
                        string model = Console.ReadLine();

                        int nrTrepteViteza = 0;
                        valid = false;
                        while (!valid)
                        {
                            Console.Write("Introdu numar trepte viteza : ");
                            string inputString = Console.ReadLine();
                            valid = int.TryParse(inputString, out nrTrepteViteza);

                            if (!valid)
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                            }
                        }

                        int anFabricare = 0;
                        valid = false;
                        while (!valid)
                        {
                            Console.Write("Introdu an fabricare : ");
                            string inputString = Console.ReadLine();
                            valid = int.TryParse(inputString, out anFabricare);

                            if (!valid)
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                            }
                        }

                        float pretPerZi = 0.0f;
                        valid = false;
                        while (!valid)
                        {
                            Console.Write("Introdu pret per zi : ");
                            string inputString = Console.ReadLine();
                            valid = float.TryParse(inputString, out pretPerZi);

                            if (!valid)
                            {
                                Console.WriteLine("Invalid input. Please try again.");
                            }
                        }

                        update_nr_nextId_masini(ref nrMasini, ref nextIdMasina, adminMasini);

                        masina = new Masina(nextIdMasina, marca, model, nrTrepteViteza, anFabricare, pretPerZi, true);
                        Console.WriteLine("\nMasina a fost salvata temporar.");
                        Console.ReadKey();

                        break;
                    case "2":
                        string infoMasina = masina.Info();
                        if (masina.IdMasina != 0)
                            Console.WriteLine("Masina {0}", infoMasina);
                        else
                            Console.WriteLine("Nu a fost introdusa nicio masina.");
                        Console.ReadKey();

                        break;
                    case "3":
                        Masina[] masini = adminMasini.GetMasini(out nrMasini);
                        AfisareMasini(masini, nrMasini);
                        Console.ReadKey();

                        break;
                    case "4":
                        update_nr_nextId_masini(ref nrMasini, ref nextIdMasina, adminMasini);

                        masina.IdMasina = nextIdMasina;

                        //adaugare student in fisier
                        adminMasini.AddMasina(masina);
                        Console.WriteLine("\nMasina a fost salvata in fisier.");
                        Console.ReadKey();

                        break;
                    case "5":
                        Console.Write("Introdu ID : ");
                        id_cautat = int.Parse(Console.ReadLine());

                        Console.WriteLine();

                        Masina output_caurate_id_masina = adminMasini.GetMasina(id_cautat);
                        if (output_caurate_id_masina.IdMasina != 0)
                            Console.WriteLine(adminMasini.GetMasina(id_cautat).Info());
                        else
                            Console.WriteLine("Nu s-a gasit nicio masina.");
                        Console.ReadKey();

                        break;
                    case "6":
                        Console.Write("Introdu marca : ");
                        string marca_cautata = Console.ReadLine();
                        Console.Write("Introdu model : ");
                        string model_cautat = Console.ReadLine();

                        Console.WriteLine();

                        Masina output_cautare_marca_model = adminMasini.GetMasina(marca_cautata, model_cautat);
                        if (output_cautare_marca_model.IdMasina != 0)
                            Console.WriteLine(adminMasini.GetMasina(marca_cautata, model_cautat).Info());
                        else
                            Console.WriteLine("Nu s-a gasit nicio masina.");
                        Console.ReadKey();

                        break;
                    case "X":

                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");
                        Console.ReadKey();

                        break;
                }
            } while (optiune.ToUpper() != "X");

            Console.ReadKey();
        }
        public static void update_nr_nextId_inchirieri(ref int nrInchirieri, ref int nextIdInchiriere, AdministrareInchirieri adminInchirieri)
        {
            Inchiriere[] inchirieri = adminInchirieri.GetInchirieri(out nrInchirieri);
            if (nrInchirieri != 0)
                nextIdInchiriere = inchirieri[nrInchirieri - 1].IdInchiriere + 1;
        }

        public static void update_nr_nextId_masini(ref int nrMasini, ref int nextIdMasina, AdministrareMasini adminMasini)
        {
            Masina[] masini = adminMasini.GetMasini(out nrMasini);
            if (nrMasini != 0)
                nextIdMasina = masini[nrMasini - 1].IdMasina + 1;
        }
        public static void AfisareInchirieri(Inchiriere[] inchirieri, int nrInchirieri)
        {
            if(inchirieri.Length == 0)
            {
                Console.WriteLine("Nu sunt inchirieri in fisier. :(");
                return;
            }
            Console.WriteLine("Inchirierile sunt:");
            for (int contor = 0; contor < nrInchirieri; contor++)
                Console.WriteLine(inchirieri[contor].Info());
        }

        public static void AfisareMasini(Masina[] masini, int nrMasini)
        {
            if (masini.Length == 0)
            {
                Console.WriteLine("Nu sunt masini in fisier. :(");
                return;
            }
            Console.WriteLine("Masinile sunt:");
            for (int contor = 0; contor < nrMasini; contor++)
                Console.WriteLine(masini[contor].Info());
        }
    }
}
