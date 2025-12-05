using System;
using System.Drawing;
using System.IO;
using System.Threading;
using GiocoDellaVitaFattoMeglio;

namespace GiocoDellaVitaFattoMeglio
{
    public abstract class CPersonaggio
    {
        public string Nome { get; set; } = string.Empty;
        public int Colonne { get; set; }
        public int Righe { get; set; }
        public int Energia { get; set; }
        public bool State { get; set; }
        public Image? Immagine { get; set; }

        public event Action<CPersonaggio, CPersonaggio>? OnMangiatoAnimale;
        public event Action<CPersonaggio, CPersonaggio>? OnMangiatoCibo;
        public event Action<CPersonaggio>? OnMorte;

        public int TurniSenzaCaccia { get; set; } = 0;

        public abstract int EnergiaMinimaPerMuoversi { get; }

        public void PossoMuovermi()
        {
            State = Energia >= EnergiaMinimaPerMuoversi;
        }

        public void MangiaAnimale(CPersonaggio preda) => OnMangiatoAnimale?.Invoke(this, preda);
        public void MangiaCibo(CPersonaggio cibo) => OnMangiatoCibo?.Invoke(this, cibo);
        public void Muori() => OnMorte?.Invoke(this);
    }

    public class CLeone : CPersonaggio
    {
        private static int conteggio = 0;
        public CLeone()
        {
            Nome = $"Leone{Interlocked.Increment(ref conteggio)}";
            Immagine = ImmagineHelper.CaricaImmagine("leone.png");
            Energia = 10;
        }
        public override int EnergiaMinimaPerMuoversi => 5;
    }

    public class CGazzella : CPersonaggio
    {
        private static int conteggio = 0;
        public CGazzella()
        {
            Nome = $"Gazzella{Interlocked.Increment(ref conteggio)}";
            Immagine = ImmagineHelper.CaricaImmagine("gazzella.png");
            Energia = 6;
        }
        public override int EnergiaMinimaPerMuoversi => 3;
    }

    public class CConiglio : CPersonaggio
    {
        private static int conteggio = 0;
        public CConiglio()
        {
            Nome = $"Coniglio{Interlocked.Increment(ref conteggio)}";
            Immagine = ImmagineHelper.CaricaImmagine("coniglio.png");
            Energia = 4;
        }
        public override int EnergiaMinimaPerMuoversi => 2;
    }

    public class CCarota : CPersonaggio
    {
        public CCarota()
        {
            Energia = 3;
            Immagine = ImmagineHelper.CaricaImmagine("carota.png");
            State = false;
        }

        public override int EnergiaMinimaPerMuoversi => 3;
        }

       
    }

    public class CFogliame : CPersonaggio
    {
        public CFogliame()
        {
            Energia = 5;
            Immagine = ImmagineHelper.CaricaImmagine("fogliame.png");
            State = false;
        }

        public override int EnergiaMinimaPerMuoversi => 3;
        }

       
    

    internal static class ImmagineHelper
    { public static Image? CaricaImmagine(string nomeFile) 
        { DirectoryInfo dirinf = new DirectoryInfo(Directory.GetCurrentDirectory()); 
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
            } Console.WriteLine($"Immagine NON trovata: {nomeFile}"); 
            
            return null;
        }
    }
