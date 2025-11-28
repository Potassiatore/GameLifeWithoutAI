using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellaVitaFattoMeglio
{
    public enum AnimalType
    {
        Leone,
        Gazzella,
        Coniglio
    }
    public class CFactory
    {
        public CPersonaggio Crea(AnimalType tipo)
        {
            return tipo switch
            {
                AnimalType.Leone => new CLeone(),
                AnimalType.Gazzella => new CGazzella(),
                AnimalType.Coniglio => new CConiglio(),


            };
        }
    }
}
