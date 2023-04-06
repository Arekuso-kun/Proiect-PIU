using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace InchirieriMasiniLibrary
{
    public class Inchiriere
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';

        public int IdInchiriere { get; set; }
        public int IdMasina { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime DataPreluare { get; set; }
        public DateTime DataReturnare { get; set; }

        private const int ID_INCHIRIERE = 0;
        private const int ID_MASINA = 1;
        private const int NUME = 2;
        private const int PRENUME = 3;
        private const int DATA_PRELUARE = 4;
        private const int DATA_RETURNARE = 5;
        public Inchiriere(int idInchiriere = 0, int idMasina = 0, string nume = "NECUNOSCUT", string prenume = "NECUNOSCUT", DateTime dataPreluare = default, DateTime dataReturnare = default)
        {
            IdInchiriere = idInchiriere;
            IdMasina = idMasina;
            Nume = nume;
            Prenume = prenume;
            DataPreluare = dataPreluare;
            DataReturnare = dataReturnare;
        }
        public Inchiriere(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            IdInchiriere = int.Parse(dateFisier[ID_INCHIRIERE]);
            IdMasina = int.Parse(dateFisier[ID_MASINA]);
            Nume = dateFisier[NUME];
            Prenume = dateFisier[PRENUME];
            DataPreluare = DateTime.Parse(dateFisier[DATA_PRELUARE]);
            DataReturnare = DateTime.Parse(dateFisier[DATA_RETURNARE]);
            // TO DO 
            // verificari date intrare
        }
        public string ConversieLaSir_PentruFisier()
        {
            string obiectInchirierePentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdInchiriere.ToString(),
                IdMasina.ToString(),
                (Nume ?? " NECUNOSCUT "),
                (Prenume ?? " NECUNOSCUT "),
                (DataPreluare.ToString() ?? " NECUNOSCUT "),
                (DataReturnare.ToString() ?? " NECUNOSCUT "));

            return obiectInchirierePentruFisier;
        }
        public string Info()
        {
            string infoInchiriere = string.Format("#{0}: {2} {3} a inchiriat masina #{1} la data de {4} pana la data de {5}",
                IdInchiriere.ToString(),
                IdMasina.ToString(),
                (Nume ?? " NECUNOSCUT "),
                (Prenume ?? " NECUNOSCUT "),
                (DataPreluare.ToString() ?? " NECUNOSCUT "),
                (DataReturnare.ToString() ?? " NECUNOSCUT "));

            return infoInchiriere;
        }
    }
}
