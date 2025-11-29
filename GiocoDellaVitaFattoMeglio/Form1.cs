using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GiocoDellaVitaFattoMeglio
{
    public partial class Form1 : Form
    {
        List<CMangiabile> Mangiabili = new List<CMangiabile>();
        List<CPersonaggio> Personaggi = new List<CPersonaggio>();
        private PictureBox[,] mappa = new PictureBox[6, 6];
        private static readonly Random rnd = new Random();
        private int caroteCounter => Mangiabili.Count;
        public Form1()
        {
            InitializeComponent();
            CreaMangiabili(6);
            CreaGriglia();
        }
        private void CreaGriglia()
        {
            int righe = 6;
            int colonne = 6;
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
            foreach (var personaggio in Personaggi)
            {
                if (personaggio.Immagine != null)
                    mappa[personaggio.Righe, personaggio.Colonne].Image = personaggio.Immagine;
            }

            foreach (var mangiabile in Mangiabili)
            {
                if (mangiabile.Immagine != null)
                    mappa[mangiabile.Righe, mangiabile.Colonne].Image = mangiabile.Immagine;
            }


        }

        public void GeneraMangiabili()
        {
            //Genera mangiabili se sono 0
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
            CreaGiocatori();
            PosizionaLeCose();
            
        }

        private void MuoviGioco_Click(object sender, EventArgs e)
        {
            
        }
    }
}