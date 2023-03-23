using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InchirieriMasiniLibrary;

namespace InchirieriMasiniDataManagement
{
    public class AdministrareInchirieri
    {
        private const int NR_MAX_INCHIRIERI = 100;
        private string numeFisier;

        public AdministrareInchirieri(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddInchiriere(Inchiriere inchiriere)
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(inchiriere.ConversieLaSir_PentruFisier());
            }
        }

        public Inchiriere[] GetInchirieri(out int nrInchirieri)
        {
            Inchiriere[] inchirieri = new Inchiriere[NR_MAX_INCHIRIERI];

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrInchirieri = 0;

                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    inchirieri[nrInchirieri++] = new Inchiriere(linieFisier);
                }
            }

            return inchirieri;
        }
    }
}
