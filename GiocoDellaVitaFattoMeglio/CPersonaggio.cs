using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace GiocoDellaVitaFattoMeglio
{

    public abstract class CPersonaggio
    {
        new ToolTip tooltip = new();
        public string Nome { get; set; } = string.Empty;
        public int Colonne { get; set; }
        public int Righe { get; set; }
        public int Energia { get; set; }
        public Image? Immagine { get; set; }

        public event Action<CPersonaggio, CPersonaggio>? OnMangiatoAnimale; 
        public event Action<CPersonaggio, CMangiabile>? OnMangiatoCibo;       
        public event Action<CPersonaggio>? OnMorte;

        public int TurniSenzaCaccia { get; set; } = 0; // per il leone

        public abstract void PossoMuovermi();

     
        public void MangiaAnimale(CPersonaggio preda)
        {
            OnMangiatoAnimale?.Invoke(this, preda);
        }

        
        public void MangiaCibo(CMangiabile cibo)
        {
            OnMangiatoCibo?.Invoke(this, cibo);
        }

        public void Muori()
        {
            OnMorte?.Invoke(this);
        }
    }



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

 
    public abstract class CMangiabile
    {
        protected Random rnd = new Random();

        public int Colonne { get;  set; }
        public int Righe { get;  set; }
        public int PuntiEnergia { get;  set; }
        public Image? Immagine { get;  set; }

    }

    

    public class CCarota : CMangiabile
    {
        
        public CCarota()
        {
            PuntiEnergia = 3;
            Immagine = ImmagineHelper.CaricaImmagine("carota.png");
        }
    }

   

    public class CFogliame : CMangiabile
    {
        public CFogliame()
        {
            PuntiEnergia = 5;
            Immagine = ImmagineHelper.CaricaImmagine("fogliame.png");
        }
    }
    
    internal static class ImmagineHelper
    {
        public static Image? CaricaImmagine(string nomeFile)
        {
            DirectoryInfo dirinf = new DirectoryInfo(Directory.GetCurrentDirectory());
            string path = dirinf.Parent.Parent.FullName + @"\images\" + nomeFile.ToLower();
          

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
            return null; 
        }
    }
}