using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GiocoDellaVitaFattoMeglio
{
    public partial class Form1 : Form
    {
        List<CCarota> Carote = new List<CCarota>();
        List<CPersonaggio> Personaggi = new List<CPersonaggio>();
        private PictureBox[,] mappa = new PictureBox[7, 7];

        public Form1()
        {
            InitializeComponent();
            InizializzaMappa();
            CreaAnimali();
            CreaCarote(6);
        }
        private void CreaCarote(int quante)
        {
            for (int i = 0; i < quante; i++)
            {
                CCarota c = new CCarota();

                // trova una cella libera
                while (mappa[c.Righe, c.Colonne].Image != null)
                {
                    c = new CCarota(); // genera una nuova posizione
                }

                Carote.Add(c);
                mappa[c.Righe, c.Colonne].Image = c.Immagine;
            }
        }

        private void InizializzaMappa()
        {
            int cellSize = 80;

            MappaDiGioco.ColumnCount = 6;
            MappaDiGioco.RowCount = 6;

            for (int i = 0; i < 6; i++)
            {
                MappaDiGioco.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 6));
                MappaDiGioco.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6));
            }

            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 6; c++)
                {
                    PictureBox pb = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.FromArgb(240, 255, 240)
                    };

                    MappaDiGioco.Controls.Add(pb, c, r);
                    mappa[r, c] = pb;
                }
            }
        }


        private void CreaAnimali()
        {
            // Crea 6 animali in posizioni casuali senza sovrapposizioni
            var tipi = new[]
            {
                Personaggio.Leone,
                Personaggio.Leone,
                Personaggio.Gazzella,
                Personaggio.Gazzella,
                Personaggio.Coniglio,
                Personaggio.Coniglio
            };

            foreach (var tipo in tipi)
            {
                CPersonaggio p = CFactory.Crea(tipo);

                int r, c;
                do
                {
                    r = new Random().Next(0, 6);
                    c = new Random().Next(0, 6);
                } while (mappa[r, c].Image != null);

                p.Righe = r;
                p.Colonne = c;

                Personaggi.Add(p);
                mappa[r, c].Image = p.Immagine; // ECCO LA MAGIA
            }
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
    }
}