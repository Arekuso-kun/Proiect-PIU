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

        private int idMasina;
        private string marca;
        private string model;
        private int nrTrepteViteza;
        private int anFabricare;
        private float pretPerZi;
        private bool status;

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
            Pret = pretPerZi;
            Status = status;
        }

        public Masina(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            idMasina = int.Parse(dateFisier[ID_MASINA]);
            marca = dateFisier[MARCA];
            model = dateFisier[MODEL];
            nrTrepteViteza = int.Parse(dateFisier[NR_TREPTE_VITEZA]);
            anFabricare = int.Parse(dateFisier[AN_FABRICARE]);
            pretPerZi = float.Parse(dateFisier[PRET_PER_ZI]);
            status = bool.Parse(dateFisier[STATUS]);
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectMasinaPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                idMasina.ToString(),
                marca,
                model,
                nrTrepteViteza.ToString(),
                anFabricare.ToString(),
                pretPerZi.ToString(),
                status.ToString());

            return obiectMasinaPentruFisier;
        }

        public string Info()
        {
            string infoMasina = string.Format("#{0}: {1} {2} - {3} viteze, fabricata in anul {4}, pret per zi: {5}, status: {6}",
                idMasina.ToString(),
                marca,
                model,
                nrTrepteViteza.ToString(),
                anFabricare.ToString(),
                pretPerZi.ToString(),
                status ? "disponibila" : "indisponibila");

            return infoMasina;
        }

        public int IdMasina
        {
            get { return idMasina; }
            set { idMasina = value; }
        }

        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public int NrTrepteViteza
        {
            get { return nrTrepteViteza; }
            set { nrTrepteViteza = value; }
        }

        public int AnFabricare
        {
            get { return anFabricare; }
            set { anFabricare = value; }
        }

        public float Pret
        {
            get { return pretPerZi; }
            set { pretPerZi = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
