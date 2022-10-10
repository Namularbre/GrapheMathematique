using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Sockets;
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
            //Ici, on utile les fonctions de C# pour trier les arêtes par poid (coût)
            var aretesDansOrdrePoid = this.aretes.OrderBy(arete => arete.poidArete);
            //On créer une liste d'arête de type hashset pour que l'on remplis avec le résultat. Cette opération sert juste à avoir une
            //structure compatible avec la pluspart des listes.
            HashSet<Arete> aretesTrierParPoid = new HashSet<Arete>();
            foreach (var arete in aretesDansOrdrePoid)
            {
                aretesTrierParPoid.Add(arete);
            }
            //On donne au graphe  
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
            //On créer un tableau de 2 dimensions de taille nombreArette*nombreArette
            int[,] matriceAdjacence = new int[this.nombreArette, this.nombreArette];

            //On le remplis de 0
            for(int iterateurLigne = 0; iterateurLigne < this.nombreArette; iterateurLigne++)
            {
                for(int iterateurColonne = 0; iterateurColonne < this.nombreArette; iterateurColonne++)
                {
                    matriceAdjacence[iterateurLigne, iterateurColonne] = 0;
                }
            }

            //Pour chaque lien entre deux sommets (représenter avec les arêtes) on place à 1
            foreach (Arete arete in this.aretes)
            {
                matriceAdjacence[AvoirIndexSommet(arete.sommetDepart), AvoirIndexSommet(arete.sommetArrive)] = 1;
                matriceAdjacence[AvoirIndexSommet(arete.sommetArrive), AvoirIndexSommet(arete.sommetDepart)] = 1;
            }
            //On retourne un objet matrice contenant le résultat.
            return new Matrice(matriceAdjacence);
        }

        public void AfficherNombreChemin(Matrice matriceAdjacence, int longueurChemin, int sommetDepart, int sommetFin)
        {
            //On utilise la matrice d'adjacence du graphe, que l'on monte à la puissance = à la longueur du chemin.
            //Cela nous donne le nombre de chemin de longueur donnée dans cette matrice, à la place :
            //M(index du sommet de départ dans la liste des sommets, index du sommet d'arrivé dans la liste des sommets)
            var matrice = matriceAdjacence.Puissance(longueurChemin);
            //On récupère donc le nombre de chemin
            int nombreChemins = matrice.contenu[AvoirIndexSommet(sommetDepart), AvoirIndexSommet(sommetFin)];
            //On l'affiche
            Console.WriteLine("Le nombre de chemin entre " + sommetDepart + " et " + sommetFin + " de longueur " +
                longueurChemin + " est : " + nombreChemins);
        }

        public class Famille
        {
            public int parent, rang;

            public Famille(int parent, int rang)
            {
                this.parent = parent; 
                this.rang = rang;
            }
        };

        private int AvoirParent(List<Famille> familles, int sommet)
        {
            if (familles[AvoirIndexSommet(sommet)].parent != sommet)
            {
                familles[AvoirIndexSommet(sommet)].parent = AvoirParent(familles, familles[AvoirIndexSommet(sommet)].parent);
            }

            return familles[AvoirIndexSommet(sommet)].parent;
        }

        private void Union(List<Famille> familles, int sommetDepart, int sommetArrive)
        {
            int plusVieuxParentSommetDepart = AvoirParent(familles, sommetDepart);
            int plusVieuxParentSommetArrive = AvoirParent(familles, sommetArrive);

            if (familles[AvoirIndexSommet(plusVieuxParentSommetDepart)].rang < familles[AvoirIndexSommet(plusVieuxParentSommetArrive)].rang)
            {
                familles[AvoirIndexSommet(plusVieuxParentSommetDepart)].parent = plusVieuxParentSommetArrive;
            }
            else if (familles[AvoirIndexSommet(plusVieuxParentSommetDepart)].rang > familles[AvoirIndexSommet(plusVieuxParentSommetArrive)].rang)
            {
                familles[AvoirIndexSommet(plusVieuxParentSommetArrive)].parent = plusVieuxParentSommetDepart;
            }
            else
            {
                familles[AvoirIndexSommet(plusVieuxParentSommetArrive)].parent = plusVieuxParentSommetDepart;
                familles[AvoirIndexSommet(plusVieuxParentSommetDepart)].rang++;
            }
        }

        public void Kruskal()
        {
            TrierParAretesCout();

            List<Arete> resultat = new List<Arete>();

            List<Famille> familles = new List<Famille>();

            foreach (var sommet in this.sommets)
            {
                familles.Add(new Famille(sommet, 0));
            }

            foreach (Arete arete in this.aretes)
            {
                int plusVieuxParentSommetDepart = AvoirParent(familles, arete.sommetDepart);
                int plusVieuxParentSommetArrive = AvoirParent(familles, arete.sommetArrive);

                if (plusVieuxParentSommetDepart != plusVieuxParentSommetArrive)
                {
                    resultat.Add(arete);
                    Union(familles, plusVieuxParentSommetDepart, plusVieuxParentSommetArrive);
                }
            }

            Console.WriteLine("---- Arbre couvrant minimal ----");

            int coutMinimal = 0;

            foreach (var areteResult in resultat)
            {
                Console.WriteLine(areteResult.VersString());
                coutMinimal += areteResult.poidArete;
            }

            Console.WriteLine("Poid de l'arbre : " + coutMinimal);
        }

        private HashSet<Arete> TrouverAreteSommet(int sommet)
        {
            var aretesReliees = new HashSet<Arete>();

            foreach (var arete in this.aretes)
            {
                if (arete.sommetDepart == sommet || arete.sommetArrive == sommet)
                {
                    aretesReliees.Add(arete);
                    TrouverAreteSommet(arete.sommetDepart);
                    TrouverAreteSommet(arete.sommetArrive);
                }
            }
            return aretesReliees;
        }

        public Matrice AvoirMatriceTransitive()
        {
            int[,] matriceAdjacence = this.AvoirMatriceAdjacence().contenu;

            for (int k = 0; k < matriceAdjacence.GetLength(0); k++)
            {
                for (int iterateurLigne = 0; iterateurLigne < matriceAdjacence.GetLength(0); iterateurLigne++)
                {
                    for (int iterateurColonne = 0; iterateurColonne < matriceAdjacence.GetLength(0); iterateurColonne++)
                    {
                        if ((matriceAdjacence[iterateurLigne, iterateurColonne] != 0) ||
                            ((matriceAdjacence[iterateurLigne, k] != 0) && (matriceAdjacence[k, iterateurColonne] != 0)))
                        {
                            matriceAdjacence[iterateurLigne, iterateurColonne] = 1;
                        }
                        else
                        {
                            matriceAdjacence[iterateurLigne, iterateurColonne] = 0;
                        }
                        /*
                        matriceAdjacence[iterateurLigne, iterateurColonne] = (matriceAdjacence[iterateurLigne, iterateurColonne] != 0) ||
                            ((matriceAdjacence[iterateurLigne, k] != 0) && (matriceAdjacence[k, iterateurColonne] != 0)) ? 1 : 0;
                        */
                    }
                }
            }

            return new Matrice(matriceAdjacence);
        }
    }
}