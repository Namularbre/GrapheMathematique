using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Arete
    {
        // Liste des attribues que possède chaque arête: origine, destination, coût de l'arête
        // Les "get/set" permette de "récupérer/modifier" ces attribue
        public int sommetDepart { get; set; }
        public int sommetArrive { get; set; }
        public int poidArete { get; set; }

        public Arete(int sommetDepart, int sommetArrive, int poidArette)
        {
            this.sommetDepart = sommetDepart;
            this.sommetArrive = sommetArrive;
            this.poidArete = poidArette;
        }
        //Transforme notre arête en chaine de caractère.
        public string VersString()
        {
            return this.sommetDepart + "<->" + this.sommetArrive + " coût : " + this.poidArete;
        }
    }
}
