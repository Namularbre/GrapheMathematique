using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Sommet
    {
        const int DRAPEAU_VIDE = 0;

        public int nom { get; set; }
        public int drapeau { get; set; }

        public Sommet(int nom)
        {
            this.nom = nom;
            this.drapeau = DRAPEAU_VIDE;
        }
    }
}
