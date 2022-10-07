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

            return new Matrice(matriceAdjacence);
        }

        public void AfficherNombreChemin(Matrice matriceAdjacence, int longueurChemin, int sommetDepart, int sommetFin)
        {
            var matrice = matriceAdjacence.Puissance(longueurChemin);

            int nombreChemins1 = matrice.contenu[AvoirIndexSommet(sommetDepart), AvoirIndexSommet(sommetFin)];

            Console.WriteLine(nombreChemins1);
        }

        //Kruskal
        public void AvoirArbreCouvrantMinimal()
        {
            if (!this.EstConnexe())
            {
                Console.WriteLine("Le graphe n'est pas connexe.");
                return;
            }

            Console.WriteLine("---- Arbre couvrant minimal ----");
            
            HashSet<Arete> aretesTrierParPoid = this.aretes.OrderByDescending(poidArete => this.aretes.ElementAt(0).poidArete).ToHashSet();
            var aretesArbreCouvrantPoidMinimal = new HashSet<Arete>();
            var sommetsAvecFlag = FlaggerSommet();

            foreach (var arete in aretesTrierParPoid)
            {
                HashSet<Arete> aretesReliees = AvoirAreteRelierArete(arete);
                sommetsAvecFlag = ChangerFlag(arete, sommetsAvecFlag);
                
                
            }

            foreach(var arete in aretesArbreCouvrantPoidMinimal)
            {
                Console.WriteLine(arete.VersString());
            }
        }

        private Dictionary<int, int> ChangerFlag(Arete arete, Dictionary<int, int> sommetsAvecFlag)
        {
            if (arete.sommetDepart < arete.sommetArrive)
            {
                sommetsAvecFlag[arete.sommetArrive] = arete.sommetDepart;
            }
            else
            {
                sommetsAvecFlag[arete.sommetDepart] = arete.sommetArrive;
            }

            return sommetsAvecFlag;
        }

        private Dictionary<int, int> FlaggerSommet()
        {
            var sommetsAvecFlag = new Dictionary<int, int>();

            foreach (var sommet in this.sommets)
            {
                sommetsAvecFlag.Add(sommet, 0);
            }

            return sommetsAvecFlag;
        }

        private HashSet<Arete> AvoirAreteRelierArete(Arete arete)
        {
            HashSet<Arete> aretesReliees = AvoirAreteRelierAuSommet(arete.sommetDepart);
            aretesReliees.Concat(AvoirAreteRelierAuSommet(arete.sommetArrive));

            return aretesReliees;
        }

        private HashSet<Arete> AvoirAreteRelierAuSommet(int sommet)
        {
            HashSet<Arete> aretesRelierAuSommet = new HashSet<Arete>();

            foreach (var arete in aretesRelierAuSommet)
            {
                if (arete.sommetDepart == sommet || arete.sommetArrive == sommet)
                {
                    aretesRelierAuSommet.Add(arete);
                }
            }

            return aretesRelierAuSommet;
        }

        //Parcour en profondeur du graphe à partir d'un sommet
        private bool EstConnexe()
        {
            const bool PAS_MARQUER = false;
            const bool MARQUER = true;

            int sommet = this.sommets.ElementAt(0);

            var aretesReliees = new HashSet<Arete>();

            foreach (var arete in this.aretes)
            {
                if (arete.sommetDepart == sommet || arete.sommetArrive == sommet)
                {
                    aretesReliees.Add(arete);
                }
            }
            return true;
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