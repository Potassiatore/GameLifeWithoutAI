using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GiocoDellaVitaFattoMeglio
{
    public partial class Form1 : Form
    {
       
        List<CPersonaggio> Personaggi = new List<CPersonaggio>();
        private PictureBox[,] mappa = new PictureBox[10, 10];
        private Random rnd = new Random();


        public Form1()
        {
            CreaGiocatori();
            InitializeComponent();
            CreaMangiabili(6);
            CreaGriglia();
        }
        private void CreaGriglia()
        {
            int righe = 10;
            int colonne = 10;
            int cellSize = 60;
            int padding = 2;

            for (int r = 0; r < righe; r++)
            {
                for (int c = 0; c < colonne; c++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Width = cellSize;
                    pb.Height = cellSize;

                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    pb.Location = new Point(
                        c * (cellSize + padding),
                        r * (cellSize + padding)
                    );

                    mappa[r, c] = pb;
                    MappaDiGioco.Controls.Add(pb);
                }
            }
        }


        public void PosizionaLeCose()
        {
            int size = 10;

            // Unifica tutti gli elementi da posizionare
            var tutti = new List<dynamic>();
            tutti.AddRange(Personaggi);
           

            // Genera tutte le posizioni disponibili (100 celle)
            List<(int r, int c)> posizioni = new List<(int, int)>();
            for (int r = 0; r < size; r++)
            for (int c = 0; c < size; c++)
                posizioni.Add((r, c));

            // Mescola una volta sola usando il Random di classe
            posizioni = posizioni.OrderBy(x => rnd.Next()).ToList();

            // Svuota la griglia
            for (int r = 0; r < size; r++)
            for (int c = 0; c < size; c++)
                mappa[r, c].Image = null;

            // Assegna una posizione casuale diversa a ciascun animale/mangiabile
            for (int i = 0; i < tutti.Count && i < posizioni.Count; i++)
            {
                var elem = tutti[i];
                var (r, c) = posizioni[i];

                elem.Righe = r;
                elem.Colonne = c;

                if (elem.Immagine != null)
                    mappa[r, c].Image = elem.Immagine;
            }
            
        }

        private void VerificaNuovePosizioni(object sender, EventArgs e)
        {
            for (int i = 0; i < Personaggi.Count; i++)
            {
                for (int j = i + 1; j < Personaggi.Count; j++)
                {
                    // Stessa posizione
                    if ((Personaggi[i].Colonne == Personaggi[j].Colonne &&
                        Personaggi[i].Righe == Personaggi[j].Righe))
                    {
                        // Leone + (Gazzella o Coniglio)
                        bool coppiaLeonePreda =
                            (Personaggi[i] is CLeone && (Personaggi[j] is CGazzella || Personaggi[j] is CConiglio)) ||
                            (Personaggi[j] is CLeone && (Personaggi[i] is CGazzella || Personaggi[i] is CConiglio));

                        bool coppiaAnimaleErbivoroCibo =
                            ((Personaggi[i] is CGazzella || Personaggi[i] is CConiglio)&& (Personaggi[j] is CFogliame || Personaggi[j] is CCarota)) ||
                            ((Personaggi[j] is CGazzella || Personaggi[j] is CConiglio)&& (Personaggi[i] is CFogliame || Personaggi[i] is CCarota));
                        
                        if (coppiaLeonePreda)
                        {
                            // Azione leone–preda
                        }
                        else if (coppiaAnimaleErbivoroCibo)
                        {
                            // Azione Erbivoro-Cibo
                        }
                    }
                }
            }
        }


        private void MuoviCasuale()
        {
           foreach(var animale in Personaggi)
            {
                animale.PossoMuovermi();
            }
        }



        public void GeneraMangiabili()
        {
        }
        public int CreaMangiabili(int quante)
        {
            if (quante <= 0)
            {
                MessageBox.Show("Quantità di carote non valida.");
                return 0;
            }

            for (int i = 0; i < quante; i++)
            {
                // creo una nuova carota tramite factory
                CPersonaggio nuova = CFactory.Crea(Personaggio.Carota);
                CPersonaggio nuova1 = CFactory.Crea(Personaggio.Fogliame);

                // aggiungo alla lista
                Personaggi.Add(nuova);
                Personaggi.Add(nuova1);
            }

            return quante;
        }


        public void CreaGiocatori()
        {
            Personaggi.Add(CFactory.Crea(Personaggio.Leone));
            Personaggi.Add(CFactory.Crea(Personaggio.Leone));
            Personaggi.Add(CFactory.Crea(Personaggio.Gazzella));
            Personaggi.Add(CFactory.Crea(Personaggio.Gazzella));
            Personaggi.Add(CFactory.Crea(Personaggio.Coniglio));
            Personaggi.Add(CFactory.Crea(Personaggio.Coniglio));
            Personaggi.Add(CFactory.Crea(Personaggio.Coniglio));
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MappaDiGioco_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ChatDiAggiornamento_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void IniziaGioco_Click(object sender, EventArgs e)
        {
            
            PosizionaLeCose();
            
        }

        private void MuoviGioco_Click(object sender, EventArgs e)
        {
            MuoviCasuale();
        }
    }
}