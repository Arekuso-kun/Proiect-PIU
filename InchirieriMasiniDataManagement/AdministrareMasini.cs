using InchirieriMasiniLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InchirieriMasiniDataManagement
{
    public class AdministrareMasini
    {
        private const int NR_MAX_MASINI = 100;
        private string numeFisier;

        public AdministrareMasini(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddMasina(Masina masina)
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(masina.ConversieLaSir_PentruFisier());
            }
        }

        public Masina[] GetMasini(out int nrMasini)
        {
            Masina[] masini = new Masina[NR_MAX_MASINI];

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrMasini = 0;

                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    masini[nrMasini++] = new Masina(linieFisier);
                }
            }

            return masini;
        }

        public Masina GetMasina(string marca, string model)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Masina masina = new Masina(linieFisier);
                    if (masina.Marca == marca && masina.Model == model)
                    {
                        return masina;
                    }
                }
            }

            return new Masina();
        }

        public Masina GetMasina(int id)
        {
            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                // citeste cate o linie si creaza un obiect de tip Student
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Masina masina = new Masina(linieFisier);
                    if (masina.IdMasina == id)
                    {
                        return masina;
                    }
                }
            }

            return new Masina();
        }
    }
}
