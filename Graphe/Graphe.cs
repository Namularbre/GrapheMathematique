using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Graphe
    {
        //Le HashSet contient une liste d'objet d'un seul et unique type. Il n'a JAMAIS de doublons.
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
            var aretesDansOrdrePoid = this.aretes.OrderBy(arete => arete.poidArete);
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

        public void AfficherNombreChemin(Matrice matriceAdjacence, int longueurChemin, int sommetDepart, int sommetFin)
        {
            var matrice = matriceAdjacence.Puissance(longueurChemin);

            int nombreChemins1 = matrice.contenu[AvoirIndexSommet(sommetDepart), AvoirIndexSommet(sommetFin)];

            Console.WriteLine(nombreChemins1);
        }
        //Kruskal
        public void AvoirArbreCouvrantMaximal()
        {
            if (!this.EstConnexe())
            {
                Console.WriteLine("Le graphe n'est pas connexe.");
                return;
            }

            Console.WriteLine("---- Arbre couvrant maximal ----");
            //To dictionary ?
            HashSet<Arete> aretesTrierParPoid = this.aretes.OrderByDescending(poidArete => this.aretes.ElementAt(0).poidArete).ToHashSet();
            HashSet<int> sommetMarquees = new HashSet<int>();
            
            foreach (var arete in aretesTrierParPoid)
            {
                Console.WriteLine(arete.VersString());
                if (!sommetMarquees.Contains(arete.sommetDepart) && !sommetMarquees.Contains(arete.sommetArrive))
                {
                    sommetMarquees.Add(arete.sommetDepart);
                    sommetMarquees.Add(arete.sommetArrive);
                }
            }

            foreach (var sommet in sommetMarquees)
            {
                Console.WriteLine(sommet);
            }
        }
        //Parcour en profondeur du graphe
        private bool EstConnexe()
        {
            const bool PAS_MARQUER = false;
            const bool MARQUER = true;

            Dictionary<int, bool> sommetsDuGrapheAvecMarqueur = new Dictionary<int, bool>();

            foreach(int sommet in this.sommets)
            {
                sommetsDuGrapheAvecMarqueur.Add(sommet, PAS_MARQUER);
            }



            return true;
        }

        public Matrice AvoirMatriceTransitive()
        {
            /*int[,] matrice = new int[,]
            {
               {0, 1, 0, 1, 0, 0},
               {0, 0, 0, 0, 0, 0},
               {0, 1, 0, 0, 1, 0},
               {0, 0, 0, 0, 0, 1},
               {0, 0, 1, 1, 0, 0},
               {0, 0, 0, 0, 0, 0},
            };*/

            int[,] matrice = this.AvoirMatriceAdjacence().contenu;


            for (int k = 0; k < matrice.GetLength(0); k++)
            {
                for (int iterateurLigne = 0; iterateurLigne < matrice.GetLength(0); iterateurLigne++)
                {
                    for (int iterateurColonne = 0; iterateurColonne < matrice.GetLength(0); iterateurColonne++)
                    {
                        matrice[iterateurLigne, iterateurColonne] = (matrice[iterateurLigne, iterateurColonne] != 0) ||
                            ((matrice[iterateurLigne, k] != 0) && (matrice[k, iterateurColonne] != 0)) ? 1 : 0;
                    }
                }
            }

            return new Matrice(matrice, this.sommets.Count);
        }
    }
}