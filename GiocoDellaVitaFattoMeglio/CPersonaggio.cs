using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace GiocoDellaVitaFattoMeglio
{
    // ===========================================================
    // CLASSE BASE ASTRATTA PER TUTTI I PERSONAGGI
    // ===========================================================
    public abstract class CPersonaggio
    {
        protected static readonly Random r = new Random();

        public string Nome { get; set; } = string.Empty;
        public int Colonne { get; set; }
        public int Righe { get; set; }
        public int Energia { get; set; }
        public Image? Immagine { get; set; }

        // Costruttore base: posizione ed energia casuali
        protected CPersonaggio()
        {
            Colonne = r.Next(0, 6);     // griglia 7x7 → indici 0..6
            Righe = r.Next(0, 6);
            Energia = r.Next(1, 11);
        }

        public abstract void PossoMuovermi();
    }

    // ===========================================================
    // LEONE
    // ===========================================================
    public class CLeone : CPersonaggio
    {
        private static int conteggio = 0;

        public CLeone()
        {
            Nome = $"Leone{Interlocked.Increment(ref conteggio)}";
            Immagine = ImmagineHelper.CaricaImmagine("leone.png");
        }

        public override void PossoMuovermi()
        {
            if (Energia < 5)
                Console.WriteLine($"{Nome}: Energia troppo bassa ({Energia}) – non mi muovo!");
        }
    }

    // ===========================================================
    // GAZZELLA
    // ===========================================================
    public class CGazzella : CPersonaggio
    {
        private static int conteggio = 0;

        public CGazzella()
        {
            Nome = $"Gazzella{Interlocked.Increment(ref conteggio)}";
            Immagine = ImmagineHelper.CaricaImmagine("gazzella.png");
        }

        public override void PossoMuovermi()
        {
            if (Energia < 4)
                Console.WriteLine($"{Nome}: Energia troppo bassa ({Energia}) – non mi muovo!");
        }
    }

    // ===========================================================
    // CONIGLIO
    // ===========================================================
    public class CConiglio : CPersonaggio
    {
        private static int conteggio = 0;

        public CConiglio()
        {
            Nome = $"Coniglio{Interlocked.Increment(ref conteggio)}";
            Immagine = ImmagineHelper.CaricaImmagine("coniglio.png");
        }

        public override void PossoMuovermi()
        {
            if (Energia < 2)
                Console.WriteLine($"{Nome}: Energia troppo bassa ({Energia}) – non mi muovo!");
        }
    }

    // ===========================================================
    // CAROTA (cibo)
    // ===========================================================

    // ===========================================================
    // MANGIABILI (oggetti cibo)
    // ===========================================================

    public abstract class CMangiabile
    {
        protected static readonly Random rnd = new Random();

        public int Colonne { get; protected set; }
        public int Righe { get; protected set; }
        public int PuntiEnergia { get; protected set; }
        public Image? Immagine { get; protected set; }

        protected CMangiabile()
        {
            Colonne = rnd.Next(0, 6);   // posizionamento casuale 0..6
            Righe = rnd.Next(0, 6);
        }
    }

    // ===========================================================
    // CAROTA
    // ===========================================================

    public class CCarota : CMangiabile
    {
        public CCarota()
        {
            PuntiEnergia = rnd.Next(1, 4); // 1–3 energia
            Immagine = ImmagineHelper.CaricaImmagine("carota.png");
        }
    }

    // ===========================================================
    // FOGLIAME
    // ===========================================================

    public class CFogliame : CMangiabile
    {
        public CFogliame()
        {
            PuntiEnergia = rnd.Next(1, 3); // fogliame = 1–2 energia
            Immagine = ImmagineHelper.CaricaImmagine("fogliame.png");
        }
    }
    // ===========================================================
    // HELPER PER CARICARE LE IMMAGINI IN MODO SICURO
    // ===========================================================
    internal static class ImmagineHelper
    {
        public static Image? CaricaImmagine(string nomeFile)
        {
            DirectoryInfo dirinf = new DirectoryInfo(Directory.GetCurrentDirectory());
            string path = dirinf.Parent.Parent.FullName + @"\images\" + nomeFile.ToLower();
            //MessageBox.Show(path);

            if (File.Exists(path))
            {
                try
                {
                    return Image.FromFile(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore caricamento {path}: {ex.Message}");
                }
            }

            Console.WriteLine($"Immagine NON trovata: {nomeFile}");
            return null; // puoi sostituire con un'immagine placeholder se vuoi
        }
    }
}