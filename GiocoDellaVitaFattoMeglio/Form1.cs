namespace GiocoDellaVitaFattoMeglio
{
    public partial class Form1 : Form
    {
        List<CPersonaggio> Personaggi = new List<CPersonaggio>();
        private PictureBox[,] mappa = new PictureBox[6, 6];

        public Form1()
        {
            InitializeComponent();
            InizializzaMappa();
            CreaAnimali();
        }

        private void InizializzaMappa()
        {
            int cellSize = 80; // più grande = si vedono meglio le immagini
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 6; c++)
                {
                    PictureBox pb = new PictureBox
                    {
                        Width = cellSize,
                        Height = cellSize,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(c * cellSize + 10, r * cellSize + 10),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.FromArgb(240, 255, 240) // verdino prato
                    };
                    this.Controls.Add(pb); // oppure MappaDiGioco.Controls.Add(pb) se hai un Panel
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

    }
}