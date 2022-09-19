using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Graphe
    {
        public HashSet<Arete> aretes { get; set; }
        public int nombreArette { get; set; }

        public Graphe(HashSet<Arete> aretes, int nombreArette)
        {
            this.aretes = aretes;
            this.nombreArette = nombreArette;
        }

        public string VerdString()
        {
            const char RETOUR_A_LA_LIGNE = '\n';

            string affichage = "Nombre de sommet : " + this.nombreArette + RETOUR_A_LA_LIGNE;

            foreach (Arete arete in this.aretes)
            {
                affichage += arete.VersString() + RETOUR_A_LA_LIGNE;
            }

            return affichage;
        }

        public HashSet<int> AvoirArete()
        {
            HashSet<int> sommetsGraphe = new HashSet<int>();

            foreach(Arete arete in this.aretes)
            {
                sommetsGraphe.Add(arete.sommetDepart);
                sommetsGraphe.Add(arete.sommetArrive);
            }

            return sommetsGraphe;
        }

        public void TrierParAretesCout()
        {
            var aretesDansOrdrePoid = this.aretes.OrderBy(arete => arete.poidArette);
            HashSet<Arete> aretesTrierParPoid = new HashSet<Arete>();
            foreach (var arete in aretesDansOrdrePoid)
            {
                aretesTrierParPoid.Add(arete);
            }

            this.aretes = aretesTrierParPoid;
        }
    }
}
