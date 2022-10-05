using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Arete
    {
        public int sommetDepart { get; set; }
        public int sommetArrive { get; set; }
        public int poidArete { get; set; }
        public bool estMarque { get; set; }

        public Arete(int sommetDepart, int sommetArrive, int poidArette)
        {
            this.sommetDepart = sommetDepart;
            this.sommetArrive = sommetArrive;
            this.poidArete = poidArette;
            this.estMarque = false;
        }

        public string VersString()
        {
            return this.sommetDepart + "<->" + this.sommetArrive + " coût : " + this.poidArete;
        }
    }
}
