using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellaVitaFattoMeglio
{
    public abstract class CPersonaggio
    {
        public string Nome { get; set; }

        public int Colonne { get; set; }

        public int Righe { get; set; }

        public int Energia { get; set; }

        public Image Immagine { get; set; }

        public CPersonaggio()
        {
            Nome = string.Empty;
            Colonne = 0;
            Righe = 0;
            Energia = 0;
           
        }
        public CPersonaggio(string nome, int colonne, int righe, int energia)
        {
            Nome = nome;
            Colonne = colonne;
            Righe = righe;
            Energia = energia;
        }

        public abstract void PossoMuovermi();
    }

    public class CLeone : CPersonaggio
    {
        public override void PossoMuovermi()
        {
            if (Energia < 5)
            {
                Console.WriteLine("Energia insufficiente per muoversi");
            }
        }
    }

    public class CGazzella : CPersonaggio
    {
        public override void PossoMuovermi()
        {
            if (Energia < 4)
            {
                Console.WriteLine("Energia insufficiente per muoversi");
            }
        }
    }
    public class CConiglio : CPersonaggio
    {
        public override void PossoMuovermi()
        {
            if (Energia < 2)
            {
                Console.WriteLine("Energia insufficiente per muoversi");
            }
        }
    }

    public class CCarota
    {
        private static readonly Random _rnd = new Random();
        private int _puntiEnergia;

        public int PuntiEnergia
        {
            get => _puntiEnergia;
            set => _puntiEnergia = _rnd.Next(1, 4);
        }


    }
}
