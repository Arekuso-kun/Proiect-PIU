using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InchirieriMasiniLibrary
{
    public class Masina
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';

        public int IdMasina;
        public string Marca;
        public string Model;
        public int NrTrepteViteza;
        public int AnFabricare;
        public float PretPerZi;
        public bool Status;

        private const int ID_MASINA = 0;
        private const int MARCA = 1;
        private const int MODEL = 2;
        private const int NR_TREPTE_VITEZA = 3;
        private const int AN_FABRICARE = 4;
        private const int PRET_PER_ZI = 5;
        private const int STATUS = 6;

        public Masina(int idMasina = 0, string marca = "NECUNOSCUTA", string model = "NECUNOSCUT", int nrTrepteViteza = 0, int anFabricare = 0, float pretPerZi = 0.0f, bool status = true)
        {
            IdMasina = idMasina;
            Marca = marca;
            Model = model;
            NrTrepteViteza = nrTrepteViteza;
            AnFabricare = anFabricare;
            PretPerZi = pretPerZi;
            Status = status;
        }

        public Masina(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            IdMasina = int.Parse(dateFisier[ID_MASINA]);
            Marca = dateFisier[MARCA];
            Model = dateFisier[MODEL];
            NrTrepteViteza = int.Parse(dateFisier[NR_TREPTE_VITEZA]);
            AnFabricare = int.Parse(dateFisier[AN_FABRICARE]);
            PretPerZi = float.Parse(dateFisier[PRET_PER_ZI]);
            Status = bool.Parse(dateFisier[STATUS]);
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectMasinaPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdMasina.ToString(),
                Marca,
                Model,
                NrTrepteViteza.ToString(),
                AnFabricare.ToString(),
                PretPerZi.ToString(),
                Status.ToString());

            return obiectMasinaPentruFisier;
        }

        public string Info()
        {
            string infoMasina = string.Format("#{0}: {1} {2} - {3} viteze, fabricata in anul {4}, pret per zi: {5}, status: {6}",
                IdMasina.ToString(),
                Marca,
                Model,
                NrTrepteViteza.ToString(),
                AnFabricare.ToString(),
                PretPerZi.ToString(),
                Status ? "disponibila" : "indisponibila");

            return infoMasina;
        }
    }
}
