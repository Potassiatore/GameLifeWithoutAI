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
        Carota,
        Fogliame
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
        public static CMangiabile Crea(Oggetto tipo)
        {
            return tipo switch
            {
                Oggetto.Carota => new CCarota(),
                Oggetto.Fogliame => new CFogliame(),
                _ => throw new ArgumentException("Tipo non valido", nameof(tipo))
            }; 
        }
    }
}
