using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GiocoDellaVitaFattoMeglio
{
    public partial class Form1 : Form
    {
        List<CMangiabile> Mangiabili = new List<CMangiabile>();
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
            tutti.AddRange(Mangiabili);

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

        private void MuoviCasuale()
        {
            int size = 10;

            // Salva le posizioni dei mangiabili
            var posMangiabili = new List<(int r, int c, Image img)>();
            foreach (var m in Mangiabili)
                if (m.Immagine != null)
                    posMangiabili.Add((m.Righe, m.Colonne, m.Immagine));

            // Svuota tutta la griglia
            for (int r = 0; r < size; r++)
            for (int c = 0; c < size; c++)
                mappa[r, c].Image = null;

            // Riposiziona i mangiabili
            foreach (var m in posMangiabili)
                mappa[m.r, m.c].Image = m.img;

            // Muove i personaggi
            foreach (var animale in Personaggi)
            {
                int nuovaRiga = rnd.Next(0, size);
                int nuovaColonna = rnd.Next(0, size);

                animale.Righe = nuovaRiga;
                animale.Colonne = nuovaColonna;

                if (animale.Immagine != null)
                    mappa[nuovaRiga, nuovaColonna].Image = animale.Immagine;
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
                CMangiabile nuova = CFactory2.Crea(Oggetto.Carota);
                CMangiabile nuova1 = CFactory2.Crea(Oggetto.Fogliame);

                // aggiungo alla lista
                Mangiabili.Add(nuova);
                Mangiabili.Add(nuova1);
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