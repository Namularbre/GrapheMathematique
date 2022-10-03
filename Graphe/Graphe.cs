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
        public HashSet<int> sommets { get; set; }

        public Graphe(HashSet<Arete> aretes, int nombreArette)
        {
            this.aretes = aretes;
            this.nombreArette = nombreArette;
            DefinirSommetAPartirDesAretes();
        }

        private void DefinirSommetAPartirDesAretes()
        {
            this.sommets = new HashSet<int>();

            foreach (var arete in this.aretes)
            {
                this.sommets.Add(arete.sommetDepart);
                this.sommets.Add(arete.sommetArrive);
            }

            IEnumerable<int> sommetsEnOrdre = this.sommets.OrderBy(sommet => sommet);
            HashSet<int> sommetsTrie = new HashSet<int>();

            foreach (var sommet in sommetsEnOrdre)
            {
                sommetsTrie.Add(sommet);
            }
            this.sommets = sommetsTrie;
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

        private int AvoirIndexSommet(int sommetCherche)
        {
            int iterateur = 0;
            foreach (int sommet in this.sommets)
            {
                if (sommet == sommetCherche)
                {
                    return iterateur;
                }
                iterateur++;
            }
            return -1;
        }

        public Matrice AvoirMatriceAdjacence()
        {
            int[,] matriceAdjacence = new int[this.nombreArette, this.nombreArette];

            for(int iterateurLigne = 0; iterateurLigne < this.nombreArette; iterateurLigne++)
            {
                for(int iterateurColonne = 0; iterateurColonne < this.nombreArette; iterateurColonne++)
                {
                    matriceAdjacence[iterateurLigne, iterateurColonne] = 0;
                }
            }

            foreach (Arete arete in this.aretes)
            {
                matriceAdjacence[AvoirIndexSommet(arete.sommetDepart), AvoirIndexSommet(arete.sommetArrive)] = 1;
                matriceAdjacence[AvoirIndexSommet(arete.sommetArrive), AvoirIndexSommet(arete.sommetDepart)] = 1;
            }

            return new Matrice(matriceAdjacence, this.sommets.Count);
        }

        public void AfficherChemin(Matrice matriceAdjacence)
        {

        }
    }
}
