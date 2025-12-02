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
        Coniglio,
        Carota,
        Fogliame
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
                Personaggio.Fogliame => new CFogliame(),
                Personaggio.Carota => new CCarota(),
                _ => throw new ArgumentException("Tipo non valido", nameof(tipo))
            };
        }

    }
}
   
