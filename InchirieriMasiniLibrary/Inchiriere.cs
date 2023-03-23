using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InchirieriMasiniLibrary
{
    public class Inchiriere
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ',';

        private int idInchiriere;
        private int idMasina;
        private string nume;
        private string prenume;
        // de adaugat: data inchiriere, durata, pret, ...

        private const int ID_INCHIRIERE = 0;
        private const int ID_MASINA = 1;
        private const int NUME = 2;
        private const int PRENUME = 3;
        public Inchiriere(int idInchiriere, int idMasina, string nume, string prenume)
        {
            this.idInchiriere = idInchiriere;
            this.idMasina = idMasina;
            this.nume = nume;
            this.prenume = prenume;
        }
        public Inchiriere(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            idInchiriere = Convert.ToInt32(dateFisier[ID_INCHIRIERE]);
            idMasina = Convert.ToInt32(dateFisier[ID_MASINA]);
            nume = dateFisier[NUME];
            prenume = dateFisier[PRENUME];
        }
        public string ConversieLaSir_PentruFisier()
        {
            string obiectStudentPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}",
                SEPARATOR_PRINCIPAL_FISIER,
                idInchiriere.ToString(),
                idMasina.ToString(),
                (nume ?? " NECUNOSCUT "),
                (prenume ?? " NECUNOSCUT "));

            return obiectStudentPentruFisier;
        }
        public int GetIDinchiriere()
        {
            return idInchiriere;
        }
        public int GetIDmasina()
        {
            return idMasina;
        }
        public string GetNume()
        {
            return nume;
        }
        public string GetPrenume()
        {
            return prenume;
        }
    }
}
