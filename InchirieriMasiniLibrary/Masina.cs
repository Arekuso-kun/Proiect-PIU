﻿using System;
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
        public int Culoare;
        public bool AerConditionat;
        public int AnFabricare;
        public float PretPerZi;
        public bool Status;
        public enum Culori
        {
            Necunoscut = 0,
            Rosu = 1,
            Albastru = 2,
            Verde = 3,
            Galben = 4,
            Negru = 5,
            Alb = 6,
            Argintiu = 7,
            Gri = 8,
            Alta = 9
        }

        private const int ID_MASINA = 0;
        private const int MARCA = 1;
        private const int MODEL = 2;
        private const int CULOARE = 3;
        private const int AER_CONDITIONAT = 4;
        private const int AN_FABRICARE = 5;
        private const int PRET_PER_ZI = 6;
        private const int STATUS = 7;

        public Masina(int idMasina = 0, string marca = "NECUNOSCUTA", string model = "NECUNOSCUT", int culoare = 0,
            bool aerConditionat = false, int anFabricare = 0, float pretPerZi = 0.0f, bool status = true)
        {
            IdMasina = idMasina;
            Marca = marca;
            Model = model;
            Culoare = culoare;
            AerConditionat = aerConditionat;
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
            Culoare = int.Parse(dateFisier[CULOARE]);
            AerConditionat = bool.Parse(dateFisier[AER_CONDITIONAT]);
            AnFabricare = int.Parse(dateFisier[AN_FABRICARE]);
            PretPerZi = float.Parse(dateFisier[PRET_PER_ZI]);
            Status = bool.Parse(dateFisier[STATUS]);
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectMasinaPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdMasina.ToString(),
                Marca,
                Model,
                Culoare,
                AerConditionat.ToString(),
                AnFabricare.ToString(),
                PretPerZi.ToString(),
                Status.ToString());

            return obiectMasinaPentruFisier;
        }

        public string Info()
        {
            string infoMasina = string.Format("#{0}: {1} {2} {5}, {3}, AC: {4}, pret pe zi: {6}, status: {7}",
                IdMasina.ToString(),
                Marca,
                Model,
                (Culori)Culoare,
                AerConditionat.ToString(),
                AnFabricare.ToString(),
                PretPerZi.ToString(),
                Status ? "disponibila" : "indisponibila");

            return infoMasina;
        }
    }
}
