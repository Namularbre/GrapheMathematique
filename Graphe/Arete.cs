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
        public int poidArette { get; set; }

        public Arete(int sommetDepart, int sommetArrive, int poidArette)
        {
            this.sommetDepart = sommetDepart;
            this.sommetArrive = sommetArrive;
            this.poidArette = poidArette;
        }

        public string VersString()
        {
            return this.sommetDepart + "<->" + this.sommetArrive + " coût : " + this.poidArette;
        }
    }
}
