using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GiocoDellaVitaFattoMeglio
{
    public partial class Form1 : Form
    {
        List<CPersonaggio> Personaggi = new List<CPersonaggio>();
        private PictureBox[,] mappa = new PictureBox[10, 10];
        private Random rnd = new Random();
        private HashSet<CLeone> leoniCheHannoMangiatoQuestoTurno = new();
        private int turniDaUltimoMangiabile = 0;
        private const int TURNI_PER_NUOVO_CIBO = 2;
        private ToolTip tooltipEnergia = new ToolTip();


        public Form1()
        {
            InitializeComponent();
            CreaGriglia();
            inizializzaEventiPersonaggi();
            InizializzaChat();
        }

        private void HandlerMorte(CPersonaggio morto)
        {
            if (IsPositionValid(morto.Righe, morto.Colonne))
                mappa[morto.Righe, morto.Colonne].Image = null;

            if (Personaggi.Contains(morto))
                Personaggi.Remove(morto);

            Log($"{morto.Nome} è morto.");

            // Controlla chi ha vinto
            if (!Personaggi.OfType<CLeone>().Any())
                Log("Tutti i leoni sono morti! Vincono gli erbivori!");
            else if (!Personaggi.OfType<CGazzella>().Any() && !Personaggi.OfType<CConiglio>().Any())
                Log("Tutti gli erbivori sono morti! Vincono i leoni!");
            VerificaVittoria();
        }


        private void HandlerMangiatoCibo(CPersonaggio erbivoro, CPersonaggio cibo)
        {
            if (IsPositionValid(cibo.Righe, cibo.Colonne))
                mappa[cibo.Righe, cibo.Colonne].Image = null;

            if (Personaggi.Contains(cibo))
                Personaggi.Remove(cibo);

            erbivoro.Energia += 2;

            // Log dell'azione
            Log($"{erbivoro.Nome} ha mangiato {cibo.Nome}!");

            turniDaUltimoMangiabile++;

            if (turniDaUltimoMangiabile >= TURNI_PER_NUOVO_CIBO)
            {
                CPersonaggio nuovo = CFactory.Crea(Personaggio.Carota);
                CPersonaggio nuovo2 = CFactory.Crea(Personaggio.Fogliame);

                Personaggi.Add(nuovo);
                Personaggi.Add(nuovo2);

                nuovo.OnMangiatoAnimale += HandlerMangiatoAnimale;
                nuovo.OnMangiatoCibo += HandlerMangiatoCibo;
                nuovo.OnMorte += HandlerMorte;

                nuovo2.OnMangiatoAnimale += HandlerMangiatoAnimale;
                nuovo2.OnMangiatoCibo += HandlerMangiatoCibo;
                nuovo2.OnMorte += HandlerMorte;

                PosizionaElementoCasuale(nuovo);
                PosizionaElementoCasuale(nuovo2);

                Log("Sono comparsi nuovi cibi: Carota e Fogliame");

                turniDaUltimoMangiabile = 0;
            }

            AggiornaGriglia();
        }




        private void PosizionaElementoCasuale(CPersonaggio p)
        {
            List<(int r, int c)> libere = new();

            for (int r = 0; r < mappa.GetLength(0); r++)
            {
                for (int c = 0; c < mappa.GetLength(1); c++)
                {
                    if (!Personaggi.Any(x => x.Righe == r && x.Colonne == c))
                    {
                        libere.Add((r, c));
                    }
                }
            }

            if (libere.Count == 0) return;

            var pos = libere[rnd.Next(libere.Count)];
            p.Righe = pos.r;
            p.Colonne = pos.c;

            if (p.Immagine != null)
                mappa[pos.r, pos.c].Image = p.Immagine;
        }


        private void VerificaVittoria()
        {
            int leoniVivi = Personaggi.OfType<CLeone>().Count();
            int erbivoriVivi = Personaggi.OfType<CGazzella>().Count() + Personaggi.OfType<CConiglio>().Count();

            Log($"Animali in vita: Leoni={leoniVivi}, Erbivori={erbivoriVivi}");

            if (leoniVivi == 0)
            {
                Log("Tutti i leoni sono morti. Gli erbivori vincono!");
                MessageBox.Show("I leoni sono morti, vincono le prede");

                DisabilitaMovimento();
            }
            else if (erbivoriVivi == 0)
            {
                Log("Tutti gli erbivori sono morti. I leoni vincono!");
                MessageBox.Show("Le prede sono morte, vincono i leoni");
                DisabilitaMovimento();
            }
        }


        // Disabilita il pulsante di movimento quando il gioco finisce
        private void DisabilitaMovimento()
        {
            MuoviGioco.Enabled = false;
        }


        private void HandlerMangiatoAnimale(CPersonaggio predatore, CPersonaggio preda)
        {
            if (IsPositionValid(preda.Righe, preda.Colonne))
                mappa[preda.Righe, preda.Colonne].Image = null;

            if (Personaggi.Contains(preda))
                Personaggi.Remove(preda);

            // Log dell'azione
            Log($"{predatore.Nome} ha mangiato {preda.Nome}!");

            if (predatore is CLeone leone)
            {
                predatore.Energia += 3;
                leone.TurniSenzaCaccia = 0;
                leoniCheHannoMangiatoQuestoTurno.Add(leone);
            }
            else if (predatore is CGazzella || predatore is CConiglio)
            {
                predatore.Energia += 1;
            }
        }



        private void inizializzaEventiPersonaggi()
        {
            foreach (var p in Personaggi)
            {
                p.OnMangiatoAnimale -= HandlerMangiatoAnimale;
                p.OnMangiatoCibo -= HandlerMangiatoCibo;
                p.OnMorte -= HandlerMorte;
            }

            foreach (var p in Personaggi)
            {
                p.OnMangiatoAnimale += HandlerMangiatoAnimale;
                p.OnMangiatoCibo += HandlerMangiatoCibo;
                p.OnMorte += HandlerMorte;
            }
        }

        private void CreaGriglia()
        {
            int righe = mappa.GetLength(0);
            int colonne = mappa.GetLength(1);
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
                    pb.Location = new Point(c * (cellSize + padding), r * (cellSize + padding));
                    mappa[r, c] = pb;
                    pb.MouseHover += PictureBox_MouseHover;
                    MappaDiGioco.Controls.Add(pb);
                }
            }
        }
        private void PictureBox_MouseHover(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb == null) return;

            // Recupera posizione nella griglia
            for (int r = 0; r < mappa.GetLength(0); r++)
            {
                for (int c = 0; c < mappa.GetLength(1); c++)
                {
                    if (mappa[r, c] == pb)
                    {
                        // Cerca se c’è un personaggio in questa cella
                        var p = Personaggi.FirstOrDefault(x => x.Righe == r && x.Colonne == c);
                        if (p != null)
                        {
                            string text = $"{p.Nome}\nEnergia: {p.Energia}";
                            tooltipEnergia.SetToolTip(pb, text);
                        }
                        else
                        {
                            tooltipEnergia.SetToolTip(pb, "");
                        }
                        return;
                    }
                }
            }
        }

        private bool IsPositionValid(int r, int c) => r >= 0 && r < mappa.GetLength(0) && c >= 0 && c < mappa.GetLength(1);

        public void PosizionaLeCose()
        {
            var tutti = new List<CPersonaggio>(Personaggi);
            List<(int r, int c)> posizioni = new List<(int, int)>();

            for (int r = 0; r < mappa.GetLength(0); r++)
                for (int c = 0; c < mappa.GetLength(1); c++)
                    posizioni.Add((r, c));

            posizioni = posizioni.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < tutti.Count && i < posizioni.Count; i++)
            {
                var elem = tutti[i];
                var (r, c) = posizioni[i];
                elem.Righe = r;
                elem.Colonne = c;
            }

            AggiornaGriglia();
        }

        private void VerificaNuovePosizioni()
        {
            var azioni = new List<(bool animaleMangiaAnimale, CPersonaggio attore, CPersonaggio bersaglio)>();
            for (int i = 0; i < Personaggi.Count; i++)
            {
                for (int j = i + 1; j < Personaggi.Count; j++)
                {
                    if (Personaggi[i].Righe == Personaggi[j].Righe && Personaggi[i].Colonne == Personaggi[j].Colonne)
                    {
                        CPersonaggio a = Personaggi[i];
                        CPersonaggio b = Personaggi[j];
                        if (a is CLeone && (b is CGazzella || b is CConiglio)) azioni.Add((true, a, b));
                        else if (b is CLeone && (a is CGazzella || a is CConiglio)) azioni.Add((true, b, a));
                        else if ((a is CGazzella || a is CConiglio) && (b is CCarota || b is CFogliame)) azioni.Add((false, a, b));
                        else if ((b is CGazzella || b is CConiglio) && (a is CCarota || a is CFogliame)) azioni.Add((false, b, a));
                    }
                }
            }

            foreach (var (animaleMangiaAnimale, attore, bersaglio) in azioni)
            {
                if (animaleMangiaAnimale) attore.MangiaAnimale(bersaglio);
                else attore.MangiaCibo(bersaglio);
            }

            AggiornaGriglia();
        }

        private void GestisciFameLeoni()
        {
            const int MAX_TURNI_SENZA_CACCIA = 10;
            var leoni = Personaggi.OfType<CLeone>().ToList();
            foreach (var leone in leoni)
            {
                if (leoniCheHannoMangiatoQuestoTurno.Contains(leone))
                {
                    leoniCheHannoMangiatoQuestoTurno.Remove(leone);
                    continue;
                }
                leone.TurniSenzaCaccia++;
                if (leone.TurniSenzaCaccia > MAX_TURNI_SENZA_CACCIA) leone.Muori();
            }
        }

        private void MovimentoCasuale()
        {
            List<CPersonaggio> daRimuovere = new List<CPersonaggio>();

            foreach (var p in Personaggi.ToList())
            {
                p.Energia += 1; 
                p.PossoMuovermi();
                if (!p.State) continue;

                int nuovaR = rnd.Next(mappa.GetLength(0));
                int nuovaC = rnd.Next(mappa.GetLength(1));

                p.Righe = nuovaR;
                p.Colonne = nuovaC;
                p.Energia -= 1;
                if (p.Energia <= 0) daRimuovere.Add(p);

            }

            VerificaNuovePosizioni(); 
            GestisciFameLeoni();

            foreach (var morto in daRimuovere)
                morto.Muori();

            AggiornaGriglia();
        }


        private void AggiornaGriglia()
        {
            for (int r = 0; r < mappa.GetLength(0); r++)
                for (int c = 0; c < mappa.GetLength(1); c++)
                    mappa[r, c].Image = null;

            foreach (var p in Personaggi)
                if (IsPositionValid(p.Righe, p.Colonne) && p.Immagine != null)
                    mappa[p.Righe, p.Colonne].Image = p.Immagine;
        }

        public int CreaMangiabili(int quante)
        {
            if (quante <= 0) return 0;

            for (int i = 0; i < quante; i++)
            {
                CPersonaggio nuova = CFactory.Crea(Personaggio.Carota);
                CPersonaggio nuova1 = CFactory.Crea(Personaggio.Fogliame);

                Personaggi.Add(nuova);
                Personaggi.Add(nuova1);

                nuova.OnMangiatoAnimale += HandlerMangiatoAnimale;
                nuova.OnMangiatoCibo += HandlerMangiatoCibo;
                nuova.OnMorte += HandlerMorte;

                nuova1.OnMangiatoAnimale += HandlerMangiatoAnimale;
                nuova1.OnMangiatoCibo += HandlerMangiatoCibo;
                nuova1.OnMorte += HandlerMorte;
            }

            return quante;
        }
        private const int MaxRighe = 8; 

        private void InizializzaChat()
        {
            ChatDiAggiornamento.RowCount = MaxRighe;
            ChatDiAggiornamento.ColumnCount = 1;
            ChatDiAggiornamento.Controls.Clear();
            ChatDiAggiornamento.RowStyles.Clear();

            for (int r = 0; r < MaxRighe; r++)
            {
                ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / MaxRighe));

                Label lbl = new Label
                {
                    Text = string.Empty,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Fill,
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent 
                };

                ChatDiAggiornamento.Controls.Add(lbl, 0, r);
            }
        }


        private void Log(string messaggio)
        {
           
            for (int r = 0; r < MaxRighe - 1; r++)
            {
                if (ChatDiAggiornamento.GetControlFromPosition(0, r) is Label lbl &&
                    ChatDiAggiornamento.GetControlFromPosition(0, r + 1) is Label lblSucc)
                {
                    lbl.Text = lblSucc.Text;
                }
            }

            if (ChatDiAggiornamento.GetControlFromPosition(0, MaxRighe - 1) is Label ultima)
            {
                ultima.Text = messaggio;
            }
        }

        public void CreaGiocatori()
        {
            Personaggi.Add(CFactory.Crea(Personaggio.Leone));
            Personaggi.Add(CFactory.Crea(Personaggio.Leone));
            Personaggi.Add(CFactory.Crea(Personaggio.Leone));
            Personaggi.Add(CFactory.Crea(Personaggio.Leone));
            Personaggi.Add(CFactory.Crea(Personaggio.Gazzella));
            Personaggi.Add(CFactory.Crea(Personaggio.Gazzella));
            Personaggi.Add(CFactory.Crea(Personaggio.Coniglio));
            Personaggi.Add(CFactory.Crea(Personaggio.Coniglio));
            Personaggi.Add(CFactory.Crea(Personaggio.Coniglio));
        }

        private void IniziaGioco_Click(object sender, EventArgs e)
        {
            Log("Partita iniziata!");
            Personaggi.Clear();
            for (int r = 0; r < mappa.GetLength(0); r++)
                for (int c = 0; c < mappa.GetLength(1); c++)
                    mappa[r, c].Image = null;

            foreach (Control ctrl in ChatDiAggiornamento.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.Text = string.Empty;
            }

            CreaGiocatori();
            CreaMangiabili(6);
            inizializzaEventiPersonaggi();
            PosizionaLeCose();
            MuoviGioco.Enabled = true;

        }

        private void MuoviGioco_Click(object sender, EventArgs e)
        {
            MovimentoCasuale();
            if (Personaggi.Count == 0)
            {
                MessageBox.Show("Inizia prima una partita");
                
            }
            
        }
        private void label1_Click(object sender, EventArgs e) { }
        private void MappaDiGioco_Paint(object sender, PaintEventArgs e) { }
        private void ChatDiAggiornamento_Paint(object sender, PaintEventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
    }
}
