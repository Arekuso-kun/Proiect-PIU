﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InchirieriMasiniLibrary
{
    public class Inchiriere
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';

        private int idInchiriere;
        private int idMasina;
        private string nume;
        private string prenume;
        private float pret;
        private DateTime dataPreluare;
        private DateTime dataReturnare;

        private const int ID_INCHIRIERE = 0;
        private const int ID_MASINA = 1;
        private const int NUME = 2;
        private const int PRENUME = 3;
        private const int PRET = 4;
        private const int DATA_PRELUARE = 5;
        private const int DATA_RETURNARE = 6;
        public Inchiriere(int idInchiriere, int idMasina, string nume, string prenume, float pret, DateTime dataPreluare, DateTime dataReturnare)
        {
            IdInchiriere = idInchiriere;
            IdMasina = idMasina;
            Nume = nume;
            Prenume = prenume;
            Pret = pret;
            DataPreluare = dataPreluare;
            DataReturnare = dataReturnare;
        }
        public Inchiriere(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            idInchiriere = int.Parse(dateFisier[ID_INCHIRIERE]);
            idMasina = int.Parse(dateFisier[ID_MASINA]);
            nume = dateFisier[NUME];
            prenume = dateFisier[PRENUME];
            pret = float.Parse(dateFisier[PRET]);
            dataPreluare = DateTime.Parse(dateFisier[DATA_PRELUARE]);
            dataReturnare = DateTime.Parse(dateFisier[DATA_RETURNARE]);
            // TO DO 
            // verificari date intrare
        }
        public string ConversieLaSir_PentruFisier()
        {
            string obiectStudentPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                idInchiriere.ToString(),
                idMasina.ToString(),
                (nume ?? " NECUNOSCUT "),
                (prenume ?? " NECUNOSCUT "),
                (pret.ToString() ?? " NECUNOSCUT "),
                (dataPreluare.ToString() ?? " NECUNOSCUT "),
                (dataReturnare.ToString() ?? " NECUNOSCUT "));

            return obiectStudentPentruFisier;
        }
        public int IdInchiriere
        {
            get { return idInchiriere; }
            set { idInchiriere = value; }
        }
        public int IdMasina
        {
            get { return idMasina; }
            set { idMasina = value; }
        }
        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }
        public string Prenume
        {
            get { return prenume; }
            set { prenume = value; }
        }
        public float Pret
        {
            get { return pret; }
            set { pret = value; }
        }
        public DateTime DataPreluare
        {
            get { return dataPreluare; }
            set { dataPreluare = value; }
        }
        public DateTime DataReturnare
        {
            get { return dataReturnare; }
            set { dataReturnare = value; }
        }
    }
}
