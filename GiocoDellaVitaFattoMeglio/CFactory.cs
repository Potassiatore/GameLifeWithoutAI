using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellaVitaFattoMeglio
{
    public enum Personaggio
    {
        Leone,
        Gazzella,
        Coniglio
    }
    public enum Oggetto
    {
        Carota
    }
    public class CFactory
    {
        public static CPersonaggio Crea(Personaggio tipo)
        {
            return tipo switch
            {
                Personaggio.Leone => new CLeone(),
                Personaggio.Gazzella => new CGazzella(),
                Personaggio.Coniglio => new CConiglio(),
                _ => throw new ArgumentException("Tipo non valido", nameof(tipo))
            };
        }

    }
    public class CFactory2
    {
        public CCarota Crea(Oggetto tipo)
        {
            return tipo switch
            {
                Oggetto.Carota => new CCarota(),
                _ => throw new ArgumentException("Tipo non valido", nameof(tipo))
            };
        }
    }
}
