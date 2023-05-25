using InchirieriMasiniDataManagement;
using InchirieriMasiniLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InchirieriMasini_UI
{
    public partial class Form1 : Form
    {
        AdministrareInchirieri adminInchirieri;
        AdministrareMasini adminMasini;

        private Label lblHeaderNume;
        private Label lblHeaderPrenume;
        private Label lblHeaderIdMasina;
        private Label lblHeaderDataPreluare;
        private Label lblHeaderDataReturnare;

        private Label[] lblsNume;
        private Label[] lblsPrenume;
        private Label[] lblsIdMasina;
        private Label[] lblsDataPreluare;
        private Label[] lblsDataReturnare;

        private Label lblHeaderID;
        private Label lblHeaderMarca;
        private Label lblHeaderModel;
        private Label lblHeaderCuloare;
        private Label lblHeaderAerConditionat;
        private Label lblHeaderAnFabricare;
        private Label lblHeaderPretPeZi;
        private Label lblHeaderStatus;

        private Label[] lblsID;
        private Label[] lblsMarca;
        private Label[] lblsModel;
        private Label[] lblsCuloare;
        private Label[] lblsAerConditionat;
        private Label[] lblsAnFabricare;
        private Label[] lblsPretPeZi;
        private Label[] lblsStatus;

        private const int LATIME_CONTROL = 130;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 130;
        private const int OFFSET_Y = 20;
        private const int OFFSET_X = 600;

        private int nrInchirieri = 0;
        private int nrMasini = 0;
        private int nextIdInchiriere = 1;
        private int nextIdMasina = 1;

        public Form1()
        {
            string NumeFisierInchirieri = ConfigurationManager.AppSettings["NumeFisierInchirieri"];
            string NumeFisierMasini = ConfigurationManager.AppSettings["NumeFisierMasini"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            // setare locatie fisier in directorul corespunzator solutiei
            // astfel incat datele din fisier sa poata fi utilizate si de alte proiecte
            string caleCompletaFisierInchirieri = locatieFisierSolutie + "\\" + NumeFisierInchirieri;
            string caleCompletaFisierMasini = locatieFisierSolutie + "\\" + NumeFisierMasini;

            adminInchirieri = new AdministrareInchirieri(caleCompletaFisierInchirieri);
            adminMasini = new AdministrareMasini(caleCompletaFisierMasini);

            InitializeComponent();

            //setare proprietati
            this.Size = new Size(520, 780);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.Blue;
            this.Text = "Informatii inchirieri si masini";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelBlack.Text = "";

            lblMesajCauta.Text = "";
            lblMesajCauta.ForeColor = Color.Red;

            lblMesajInchirieri.Text = "";
            lblMesajInchirieri.ForeColor = Color.Red;

            lblMesajMasini.Text = "";
            lblMesajMasini.ForeColor = Color.Red;

            pkrDataPreluare.Format = DateTimePickerFormat.Custom;
            pkrDataPreluare.CustomFormat = "yyyy-MM-dd HH:mm";

            pkrDataReturnare.Format = DateTimePickerFormat.Custom;
            pkrDataReturnare.CustomFormat = "yyyy-MM-dd HH:mm";

            update_nr_nextId_inchirieri(ref nrInchirieri, ref nextIdInchiriere, adminInchirieri);
            update_nr_nextId_masini(ref nrMasini, ref nextIdMasina, adminMasini);

            AfiseazaInchirieri();
            AfiseazaMasini();
            ClearTable();
        }

        private void AfiseazaInchirieri()
        {
            //adaugare control de tip Label pentru 'Nume';
            lblHeaderNume = new Label();
            lblHeaderNume.Width = LATIME_CONTROL;
            lblHeaderNume.Text = "Nume";
            lblHeaderNume.Left = OFFSET_X + 0;
            lblHeaderNume.Top = OFFSET_Y;
            lblHeaderNume.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderNume);

            //adaugare control de tip Label pentru 'Prenume';
            lblHeaderPrenume = new Label();
            lblHeaderPrenume.Width = LATIME_CONTROL;
            lblHeaderPrenume.Text = "Prenume";
            lblHeaderPrenume.Left = OFFSET_X + DIMENSIUNE_PAS_X;
            lblHeaderPrenume.Top = OFFSET_Y;
            lblHeaderPrenume.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderPrenume);

            //adaugare control de tip Label pentru 'ID Masina';
            lblHeaderIdMasina = new Label();
            lblHeaderIdMasina.Width = LATIME_CONTROL;
            lblHeaderIdMasina.Text = "ID Masina";
            lblHeaderIdMasina.Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
            lblHeaderIdMasina.Top = OFFSET_Y;
            lblHeaderIdMasina.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderIdMasina);

            //adaugare control de tip Label pentru 'Data Preluare';
            lblHeaderDataPreluare = new Label();
            lblHeaderDataPreluare.Width = LATIME_CONTROL;
            lblHeaderDataPreluare.Text = "Data Preluare";
            lblHeaderDataPreluare.Left = OFFSET_X + 3 * DIMENSIUNE_PAS_X;
            lblHeaderDataPreluare.Top = OFFSET_Y;
            lblHeaderDataPreluare.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderDataPreluare);

            //adaugare control de tip Label pentru 'Data Returnare';
            lblHeaderDataReturnare = new Label();
            lblHeaderDataReturnare.Width = LATIME_CONTROL;
            lblHeaderDataReturnare.Text = "Data Preluare";
            lblHeaderDataReturnare.Left = OFFSET_X + 4 * DIMENSIUNE_PAS_X;
            lblHeaderDataReturnare.Top = OFFSET_Y;
            lblHeaderDataReturnare.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderDataReturnare);

            int nrInchirieri = 0;
            Inchiriere[] inchirieri = adminInchirieri.GetInchirieri(out nrInchirieri);

            lblsNume = new Label[nrInchirieri];
            lblsPrenume = new Label[nrInchirieri];
            lblsIdMasina = new Label[nrInchirieri];
            lblsDataPreluare = new Label[nrInchirieri];
            lblsDataReturnare = new Label[nrInchirieri];

            int i = 0;
            foreach (Inchiriere inchiriere in inchirieri)
            {
                if (i >= nrInchirieri)
                    break;

                //adaugare control de tip Label pentru nume;
                lblsNume[i] = new Label();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = inchiriere.Nume;
                lblsNume[i].Left = OFFSET_X + 0;
                lblsNume[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsNume[i]);

                //adaugare control de tip Label pentru prenume
                lblsPrenume[i] = new Label();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = inchiriere.Prenume;
                lblsPrenume[i].Left = OFFSET_X + DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsPrenume[i]);

                //adaugare control de tip Label pentru id masina
                lblsIdMasina[i] = new Label();
                lblsIdMasina[i].Width = LATIME_CONTROL;
                lblsIdMasina[i].Text = inchiriere.IdMasina.ToString();
                lblsIdMasina[i].Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
                lblsIdMasina[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsIdMasina[i]);

                //adaugare control de tip Label pentru data preluare
                lblsDataPreluare[i] = new Label();
                lblsDataPreluare[i].Width = LATIME_CONTROL;
                lblsDataPreluare[i].Text = inchiriere.DataPreluare.ToString("yyyy-MM-dd HH:mm");
                lblsDataPreluare[i].Left = OFFSET_X + 3 * DIMENSIUNE_PAS_X;
                lblsDataPreluare[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsDataPreluare[i]);

                //adaugare control de tip Label pentru data returnare
                lblsDataReturnare[i] = new Label();
                lblsDataReturnare[i].Width = LATIME_CONTROL;
                lblsDataReturnare[i].Text = inchiriere.DataReturnare.ToString("yyyy-MM-dd HH:mm");
                lblsDataReturnare[i].Left = OFFSET_X + 4 * DIMENSIUNE_PAS_X;
                lblsDataReturnare[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsDataReturnare[i]);

                i++;
            }
        }

        private void AfiseazaMasini()
        {
            //adaugare control de tip Label pentru 'ID';
            lblHeaderID = new Label();
            lblHeaderID.Width = LATIME_CONTROL;
            lblHeaderID.Text = "ID";
            lblHeaderID.Left = OFFSET_X + 0 * DIMENSIUNE_PAS_X;
            lblHeaderID.Top = OFFSET_Y;
            lblHeaderID.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderID);

            //adaugare control de tip Label pentru 'Marca';
            lblHeaderMarca = new Label();
            lblHeaderMarca.Width = LATIME_CONTROL;
            lblHeaderMarca.Text = "Marca";
            lblHeaderMarca.Left = OFFSET_X + 1 * DIMENSIUNE_PAS_X;
            lblHeaderMarca.Top = OFFSET_Y;
            lblHeaderMarca.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderMarca);

            //adaugare control de tip Label pentru 'Model';
            lblHeaderModel = new Label();
            lblHeaderModel.Width = LATIME_CONTROL;
            lblHeaderModel.Text = "Model";
            lblHeaderModel.Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
            lblHeaderModel.Top = OFFSET_Y;
            lblHeaderModel.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderModel);

            //adaugare control de tip Label pentru 'Culoare';
            lblHeaderCuloare = new Label();
            lblHeaderCuloare.Width = LATIME_CONTROL;
            lblHeaderCuloare.Text = "Culoare";
            lblHeaderCuloare.Left = OFFSET_X + 3 * DIMENSIUNE_PAS_X;
            lblHeaderCuloare.Top = OFFSET_Y;
            lblHeaderCuloare.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderCuloare);

            //adaugare control de tip Label pentru 'Aer conditionat';
            lblHeaderAerConditionat = new Label();
            lblHeaderAerConditionat.Width = LATIME_CONTROL;
            lblHeaderAerConditionat.Text = "Aer conditionat";
            lblHeaderAerConditionat.Left = OFFSET_X + 4 * DIMENSIUNE_PAS_X;
            lblHeaderAerConditionat.Top = OFFSET_Y;
            lblHeaderAerConditionat.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderAerConditionat);

            //adaugare control de tip Label pentru 'An fabricare';
            lblHeaderAnFabricare = new Label();
            lblHeaderAnFabricare.Width = LATIME_CONTROL;
            lblHeaderAnFabricare.Text = "An fabricare";
            lblHeaderAnFabricare.Left = OFFSET_X + 5 * DIMENSIUNE_PAS_X;
            lblHeaderAnFabricare.Top = OFFSET_Y;
            lblHeaderAnFabricare.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderAnFabricare);

            //adaugare control de tip Label pentru 'Pret pe zi';
            lblHeaderPretPeZi = new Label();
            lblHeaderPretPeZi.Width = LATIME_CONTROL;
            lblHeaderPretPeZi.Text = "Pret pe zi";
            lblHeaderPretPeZi.Left = OFFSET_X + 6 * DIMENSIUNE_PAS_X;
            lblHeaderPretPeZi.Top = OFFSET_Y;
            lblHeaderPretPeZi.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderPretPeZi);

            //adaugare control de tip Label pentru 'Status';
            lblHeaderStatus = new Label();
            lblHeaderStatus.Width = LATIME_CONTROL;
            lblHeaderStatus.Text = "Disponibil";
            lblHeaderStatus.Left = OFFSET_X + 7 * DIMENSIUNE_PAS_X;
            lblHeaderStatus.Top = OFFSET_Y;
            lblHeaderStatus.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblHeaderStatus);

            int NrMasini = 0;
            Masina[] masini = adminMasini.GetMasini(out NrMasini);

            lblsID = new Label[NrMasini];
            lblsMarca = new Label[NrMasini];
            lblsModel = new Label[NrMasini];
            lblsCuloare = new Label[NrMasini];
            lblsAerConditionat = new Label[NrMasini];
            lblsAnFabricare = new Label[NrMasini];
            lblsPretPeZi = new Label[NrMasini];
            lblsStatus = new Label[NrMasini];

            int i = 0;
            foreach (Masina masina in masini)
            {
                if (i >= NrMasini)
                    break;

                //adaugare control de tip Label pentru ID;
                lblsID[i] = new Label();
                lblsID[i].Width = LATIME_CONTROL;
                lblsID[i].Text = masina.IdMasina.ToString();
                lblsID[i].Left = OFFSET_X + 0;
                lblsID[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsID[i]);

                //adaugare control de tip Label pentru marca;
                lblsMarca[i] = new Label();
                lblsMarca[i].Width = LATIME_CONTROL;
                lblsMarca[i].Text = masina.Marca;
                lblsMarca[i].Left = OFFSET_X + 1 * DIMENSIUNE_PAS_X;
                lblsMarca[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsMarca[i]);

                //adaugare control de tip Label pentru model
                lblsModel[i] = new Label();
                lblsModel[i].Width = LATIME_CONTROL;
                lblsModel[i].Text = masina.Model;
                lblsModel[i].Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
                lblsModel[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsModel[i]);

                //adaugare control de tip Label pentru culoare
                lblsCuloare[i] = new Label();
                lblsCuloare[i].Width = LATIME_CONTROL;
                lblsCuloare[i].Text = ((Masina.Culori)masina.Culoare).ToString();
                lblsCuloare[i].Left = OFFSET_X + 3 * DIMENSIUNE_PAS_X;
                lblsCuloare[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsCuloare[i]);

                //adaugare control de tip Label pentru AC
                lblsAerConditionat[i] = new Label();
                lblsAerConditionat[i].Width = LATIME_CONTROL;
                lblsAerConditionat[i].Text = (masina.AerConditionat ? "DA" : "NU");
                lblsAerConditionat[i].Left = OFFSET_X + 4 * DIMENSIUNE_PAS_X;
                lblsAerConditionat[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsAerConditionat[i]);

                //adaugare control de tip Label pentru an fabricare
                lblsAnFabricare[i] = new Label();
                lblsAnFabricare[i].Width = LATIME_CONTROL;
                lblsAnFabricare[i].Text = masina.AnFabricare.ToString();
                lblsAnFabricare[i].Left = OFFSET_X + 5 * DIMENSIUNE_PAS_X;
                lblsAnFabricare[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsAnFabricare[i]);

                //adaugare control de tip Label pentru pret
                lblsPretPeZi[i] = new Label();
                lblsPretPeZi[i].Width = LATIME_CONTROL;
                lblsPretPeZi[i].Text = masina.PretPerZi.ToString() + " RON";
                lblsPretPeZi[i].Left = OFFSET_X + 6 * DIMENSIUNE_PAS_X;
                lblsPretPeZi[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsPretPeZi[i]);

                //adaugare control de tip Label pentru stats
                lblsStatus[i] = new Label();
                lblsStatus[i].Width = LATIME_CONTROL;
                lblsStatus[i].Text = (masina.Status ? "DA" : "NU");
                lblsStatus[i].Left = OFFSET_X + 7 * DIMENSIUNE_PAS_X;
                lblsStatus[i].Top = OFFSET_Y + (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsStatus[i]);

                i++;
            }
        }

        private void BtnAdaugaInchiriere_Click(object sender, EventArgs e)
        {
            if (!DateIntrareValideInchirieri()) return;

            update_nr_nextId_inchirieri(ref nrInchirieri, ref nextIdInchiriere, adminInchirieri);

            Inchiriere inchiriere = new Inchiriere(nextIdInchiriere, int.Parse(txtIDmasina.Text), txtNume.Text, txtPrenume.Text, DateTime.Parse(pkrDataPreluare.Text), DateTime.Parse(pkrDataReturnare.Text));

            adminInchirieri.AddInchiriere(inchiriere);
            lblMesajInchirieri.Text = "Inchirierea a fost adaugata";
            lblMesajInchirieri.ForeColor = Color.Green;

            //resetarea controalelor pentru a introduce datele unei inchirieri noi
            ResetareControaleInchirieri();

            AfiseazaInchirieri();
        }

        private void btnAdaugaMasina_Click(object sender, EventArgs e)
        {
            if (!DateIntrareValideMasini()) return;

            update_nr_nextId_masini(ref nrMasini, ref nextIdMasina, adminMasini);

            Masina masina = new Masina(nextIdMasina, txtMarca.Text, txtModel.Text, CuloareSelectata(), 
                checkBoxAC.Checked, int.Parse(txtAnFab.Text), int.Parse(txtPret.Text), checkBoxStatus.Checked);

            adminMasini.AddMasina(masina);
            lblMesajMasini.Text = "Masina a fost adaugata";
            lblMesajMasini.ForeColor = Color.Green;

            //resetarea controalelor pentru a introduce datele unei inchirieri noi
            ResetareControaleMasini();

            AfiseazaMasini();
        }

        private bool DateIntrareValideInchirieri()
        {
            string mesaj_eroare = "";
            string nume = txtNume.Text;
            string prenume = txtPrenume.Text;

            bool ok = true;

            if (nume.Length < 1)
            {
                lblNume.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "Numele trebuie sa aiba cel putin un caracter!\n";
            }
            else
            {
                lblNume.ForeColor = Color.Blue;
            }

            if (prenume.Length < 1)
            {
                lblPrenume.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "Prenumele trebuie sa aiba cel putin un caracter!\n";
            }
            else
            {
                lblPrenume.ForeColor = Color.Blue;
            }

            if (!int.TryParse(txtIDmasina.Text, out int idMasina))
            {
                lblIDmasina.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "ID masina invalid!\n";
            }
            else
            {
                Masina masina = adminMasini.GetMasina(idMasina);
                if (masina.IdMasina != 0)
                    lblIDmasina.ForeColor = Color.Blue;
                else
                {
                    lblIDmasina.ForeColor = Color.Red;
                    ok = false;
                    mesaj_eroare += "Masina nu exista!\n";
                }
            }

            DateTime currentDate = DateTime.Now;
            DateTime dataPreluare = pkrDataPreluare.Value;
            DateTime dataReturnare = pkrDataReturnare.Value;

            TimeSpan difference = dataReturnare - dataPreluare;

            if (difference.TotalDays < 1)
            {
                lblDataPrel.ForeColor = Color.Red;
                lblDataRet.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "Inchirierea trebuie sa fie de cel putin o zi!\n";
            }
            else
            {
                lblDataPrel.ForeColor = Color.Blue;

                if (dataReturnare.Date < currentDate.Date)
                {
                    lblDataRet.ForeColor = Color.Red;
                    ok = false;
                    mesaj_eroare += "Data returnare invalida!\n";
                }
                else
                {
                    lblDataRet.ForeColor = Color.Blue;
                }

            }

            lblMesajInchirieri.Text = mesaj_eroare;

            return ok;
        }

        private bool DateIntrareValideMasini()
        {
            string mesaj_eroare = "";
            string marca = txtMarca.Text;
            string model = txtModel.Text;

            bool ok = true;

            if (marca.Length < 1)
            {
                lblMarca.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "Marca trebuie sa aiba cel putin un caracter!\n";
            }
            else
            {
                lblMarca.ForeColor = Color.Blue;
            }

            if (model.Length < 1)
            {
                lblModel.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "Modelul trebuie sa aiba cel putin un caracter!\n";
            }
            else
            {
                lblModel.ForeColor = Color.Blue;
            }

            if( CuloareSelectata() == 0)
            {
                lblCuloare.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "Culoarea nu a fost selectata!\n";
            }
            else
            {
                lblCuloare.ForeColor = Color.Blue;
            }

            if (!int.TryParse(txtAnFab.Text, out int AnFabricatieVal))
            {
                lblAnFab.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "An fabricatie invalid!\n";
            }
            else
            {
                int currentYear = DateTime.Now.Year;
                int minimumYear = 1900; 
                int maximumYear = currentYear + 1; 

                if (AnFabricatieVal >= minimumYear && AnFabricatieVal <= maximumYear)
                {
                    lblAnFab.ForeColor = Color.Blue;
                }
                else
                {
                    lblAnFab.ForeColor = Color.Red;
                    ok = false;
                    mesaj_eroare += "An fabricatie invalid!\n";
                }
                
            }

            if (!int.TryParse(txtPret.Text, out int PretVal))
            {
                lblPret.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "Pret invalid!\n";
            }
            else
            {
                if( PretVal > 0 )
                    lblPret.ForeColor = Color.Blue;
                else
                {
                    lblPret.ForeColor = Color.Red;
                    ok = false;
                    mesaj_eroare += "Pret invalid!\n";
                }
            }

            lblMesajMasini.Text = mesaj_eroare;

            return ok;
        }

        private void ResetareControaleInchirieri()
        {
            txtNume.Text = txtPrenume.Text = txtIDmasina.Text = string.Empty;

            /*lblMesajInchirieri.Text = string.Empty;*/

            pkrDataPreluare.ResetText();
            pkrDataReturnare.ResetText();
        }

        private void ResetareControaleMasini()
        {
            txtMarca.Text = txtModel.Text = txtAnFab.Text = txtPret.Text = string.Empty;

            /*lblMesajMasini.Text = string.Empty;*/

            rdb1.Checked = false;
            rdb2.Checked = false;
            rdb3.Checked = false;
            rdb4.Checked = false;
            rdb5.Checked = false;
            rdb6.Checked = false;
            rdb7.Checked = false;
            rdb8.Checked = false;

            checkBoxStatus.Checked = false; 
            checkBoxAC.Checked = false;
        }

        private int CuloareSelectata()
        {
            if (rdb1.Checked)
                return 1;
            if (rdb2.Checked)
                return 2;
            if (rdb3.Checked)
                return 3;
            if (rdb4.Checked)
                return 4;
            if (rdb5.Checked)
                return 5;
            if (rdb6.Checked)
                return 6;
            if (rdb7.Checked)
                return 7;
            if (rdb8.Checked)
                return 8;

            return 0;
        }

        private void BtnAfiseazaInchirieri_Click(object sender, EventArgs e)
        {
            ClearTable();
            AfiseazaInchirieri();
            this.Width = 1300;
        }

        private void BtnAfiseazaMasini_Click(object sender, EventArgs e)
        {
            ClearTable();
            AfiseazaMasini();
            this.Width = 1660;
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

        private void ClearTable()
        {
            bool isVisible = false; // Get the current state of the checkbox

            // Toggle the visibility of the controls for 'AfiseazaInchirieri'
            lblHeaderNume.Visible = isVisible;
            lblHeaderPrenume.Visible = isVisible;
            lblHeaderIdMasina.Visible = isVisible;
            lblHeaderDataPreluare.Visible = isVisible;
            lblHeaderDataReturnare.Visible = isVisible;

            foreach (Label lbl in lblsNume)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsPrenume)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsIdMasina)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsDataPreluare)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsDataReturnare)
                lbl.Visible = isVisible;

            // Toggle the visibility of the controls for 'AfiseazaMasini'
            lblHeaderID.Visible = isVisible;
            lblHeaderMarca.Visible = isVisible;
            lblHeaderModel.Visible = isVisible;
            lblHeaderCuloare.Visible = isVisible;
            lblHeaderAerConditionat.Visible = isVisible;
            lblHeaderAnFabricare.Visible = isVisible;
            lblHeaderPretPeZi.Visible = isVisible;
            lblHeaderStatus.Visible = isVisible;

            foreach (Label lbl in lblsID)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsMarca)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsModel)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsCuloare)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsAerConditionat)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsAnFabricare)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsPretPeZi)
                lbl.Visible = isVisible;

            foreach (Label lbl in lblsStatus)
                lbl.Visible = isVisible;
        }

        private void btnCauta_Click(object sender, EventArgs e)
        {
            bool ok = true;
            string mesaj_eroare = "";
            Masina masina = new Masina();

            if (!int.TryParse(txtIdMasinaCauta.Text, out int idMasina))
            {
                lblIdMasinaCauta.ForeColor = Color.Red;
                ok = false;
                mesaj_eroare += "ID masina invalid!\n";
            }
            else
            {
                masina = adminMasini.GetMasina(idMasina);
                if (masina.IdMasina != 0)
                {
                    lblIdMasinaCauta.ForeColor = Color.Blue;
                }
                else
                {
                    lblIdMasinaCauta.ForeColor = Color.Red;
                    ok = false;
                    mesaj_eroare += "Masina nu exista!\n";
                }
            }

            lblMesajCauta.Text = mesaj_eroare;

            if (!ok) return;

            lblMesajCauta.Text = "Masina a fost gasita!\n\n" + masina.Info();
            lblMesajCauta.ForeColor = Color.Green;
        }
    }
}
