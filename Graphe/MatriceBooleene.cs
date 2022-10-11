using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class MatriceBooleene
    {
        //Le conteny de notre matrice
        public int[,] contenu { get; set; }

        public MatriceBooleene(int[,] contenu)
        {
            //On vérifie que le contenu que l'on veut donné à la matrice et bien constituer de 0 et de 1
            if (ContenuEstBooleen(contenu))
            {
                this.contenu = contenu;
            }
            else
            {
                //Si ce n'est pas le cas, on fait planté le programe en indiquant le problème
                throw new ArgumentException("Le contenu de la matrice booléene ne peut être composé que de 0 ou de 1");
            }
        }

        private bool ContenuEstBooleen(int[,] contenu)
        {
            //On parcour le contenu donné en paramètre et on regarde si chaque élément et égale à 0 ou 1
            for (int indexLigne = 0; indexLigne < contenu.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < contenu.GetLength(1); indexColonne++)
                {
                    //Si un des éléments n'est pas 1 ou 0, alors on retourne faux.
                    if (NEstPasBooleen(contenu, indexLigne, indexColonne))
                    {
                        return false;
                    }
                }
            }
            //Si on arrive jusqu'ici, c'est que tout les éléments sont égaux soit à 1 soit à 0, on retourne vrai.
            return true;
        }
        //Cette fonction regarde un élément du contenu en particulier, et regarde si il est différent de 1 et de 0.
        private bool NEstPasBooleen(int[,] contenu, int indexLigne, int indexColonne)
        {
            return contenu[indexLigne, indexColonne] != 1 && contenu[indexLigne, indexColonne] != 0;
        }

        public MatriceBooleene Multiplier(MatriceBooleene matrice)
        {
            //On récupère le nombre de ligne de la matrice carré booléene.
            int longueurLigneColonne = matrice.contenu.GetLength(0);
            //On crée une matrice de même dimensions
            MatriceBooleene matriceProduit = new MatriceBooleene(new int[longueurLigneColonne, longueurLigneColonne]);

            //On multiplie les matrices. Il s'agit du même algorithme que celui des matrices de base.
            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    for (int k = 0; k < longueurLigneColonne; k++)
                    {
                        matriceProduit.contenu[iterateurLigne, iterateurColonne] += this.contenu[iterateurLigne, k] * matrice.contenu[k, iterateurColonne];
                    }
                    //Ici, si on a un résultat différent de 0, alors on le met à un, pour rester dans un contexte booléen.
                    if (matriceProduit.contenu[iterateurLigne, iterateurColonne] != 0)
                    {
                        matriceProduit.contenu[iterateurLigne, iterateurColonne] = 1;
                    }
                }
            }
            //On retourne une matrice booléene contenant le résultat.
            return matriceProduit;
        }

        public void AfficherMatrice()
        {
            //On récupère le nombre de lignes de la matrice.
            int longueurLigneColonne = this.contenu.GetLength(0);
            //Ici, je défini des constantes pour rendre le code plus visible (cf. CLEAN CODE de C.Robert Martin) 
            const char ESPACE = ' ';
            const char DEBUT_CASE = '[';
            const char FIN_CASE = ']';

            //On parcours la matrice booléene est on affiche chacun de ses éléments.
            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    Console.Write(DEBUT_CASE + this.contenu[iterateurLigne, iterateurColonne].ToString() + FIN_CASE + ESPACE);
                }
                Console.WriteLine();
            }
        }

        public MatriceBooleene Additionner(MatriceBooleene matriceBooleene)
        {
            //On récupère le nombre de lignes de la matrice.
            int longueurLigneColonne = this.contenu.GetLength(0);

            //On créé un tableau en deux dimensions de taille égale à notre matrice. Il contiendera le résultat.
            int[,] contenuResultat = new int[longueurLigneColonne, longueurLigneColonne];

            //On remplis la matrice de 0.
            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    contenuResultat[iterateurLigne, iterateurColonne] = 0;
                }
            }
            //Pour chaque quotient de la matrice actuelle (this) on l'additionne avec celui de la matrice passée en paramètre.
            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    //Si le contenu est différent de 0, alors on l'attribut à 1.
                    if (this.contenu[iterateurLigne, iterateurColonne] + matriceBooleene.contenu[iterateurLigne, iterateurColonne] != 0)
                    {
                        contenuResultat[iterateurLigne, iterateurColonne] = 1;
                    }
                }
            }
            //On retourne une nouvelle matrice contenant le résultat.
            return new MatriceBooleene(contenuResultat);
        }

        public bool EstIdentique(MatriceBooleene matriceBooleene)
        {
            //On récupère le nombre de lignes de la matrice.
            int longueurLigneColonne = this.contenu.GetLength(0);

            //On parcour le contenu des deux matrices et pour chaque quotient on regarde si ils sont différent.
            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    //Si l'un des quotients des deux matrices et différent, alors on retourne faux
                    if(this.contenu[iterateurLigne, iterateurColonne] != matriceBooleene.contenu[iterateurLigne, iterateurColonne])
                    {
                        return false;
                    }
                }
            }
            //Si on arrive ici, c'est que les quotients des deux matrices sont identique. On retourne vrai.
            return true;
        }
    }
}
