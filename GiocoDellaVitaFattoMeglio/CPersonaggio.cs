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
            Colonne = r.Next(0, 7);     // griglia 7x7 → indici 0..6
            Righe = r.Next(0, 7);
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
    public class CCarota
    {
        private static readonly Random rnd = new Random();

        public int PuntiEnergia { get; }
        public Image? Immagine { get; }
        public int Righe { get; }
        public int Colonne { get; }

        public CCarota()
        {
            Colonne = rnd.Next(0, 7);
            Righe = rnd.Next(0, 7);
            PuntiEnergia = rnd.Next(1, 4); // 1, 2 o 3 punti energia
            Immagine = ImmagineHelper.CaricaImmagine("carota.png");
        }
    }

    // ===========================================================
    // HELPER PER CARICARE LE IMMAGINI IN MODO SICURO
    // ===========================================================
    internal static class ImmagineHelper
    {
        public static Image? CaricaImmagine(string nomeFile)
        {
            // Prova questi percorsi (funziona sia in debug che dopo il build)
            string[] percorsi =
            {
                nomeFile,
                Path.Combine("images", nomeFile),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeFile),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", nomeFile)
            };

            foreach (var percorso in percorsi)
            {
                if (File.Exists(percorso))
                {
                    try
                    {
                        return Image.FromFile(percorso);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Errore caricamento {percorso}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"Immagine NON trovata: {nomeFile}");
            return null; // puoi sostituire con un'immagine placeholder se vuoi
        }
    }
}