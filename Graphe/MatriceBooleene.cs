using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class MatriceBooleene
    {
        public int[,] contenu { get; set; }

        public MatriceBooleene(int[,] contenu)
        {
            if (ContenuEstBooleen(contenu))
            {
                this.contenu = contenu;
            }
            else
            {
                throw new ArgumentException("Le contenu de la matrice booléene ne peut être composé que de 0 ou de 1");
            }
        }

        private bool ContenuEstBooleen(int[,] contenu)
        {
            for (int indexLigne = 0; indexLigne < contenu.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < contenu.GetLength(1); indexColonne++)
                {
                    if (EstBooleen(contenu, indexLigne, indexColonne))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool EstBooleen(int[,] contenu, int indexLigne, int indexColonne)
        {
            return contenu[indexLigne, indexColonne] != 1 && contenu[indexLigne, indexColonne] != 0;
        }

        public MatriceBooleene Multiplier(MatriceBooleene matrice)
        {
            int longueurLigneColonne = matrice.contenu.GetLength(0);

            MatriceBooleene matriceProduit = new MatriceBooleene(new int[longueurLigneColonne, longueurLigneColonne]);

            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    for (int k = 0; k < longueurLigneColonne; k++)
                    {
                        matriceProduit.contenu[iterateurLigne, iterateurColonne] += this.contenu[iterateurLigne, k] * matrice.contenu[k, iterateurColonne];
                    }

                    if (matriceProduit.contenu[iterateurLigne, iterateurColonne] != 0)
                    {
                        matriceProduit.contenu[iterateurLigne, iterateurColonne] = 1;
                    }
                }
            }

            return matriceProduit;
        }

        public void AfficherMatrice()
        {
            int longueurLigneColonne = this.contenu.GetLength(0);

            const char ESPACE = ' ';
            const char DEBUT_CASE = '[';
            const char FIN_CASE = ']';

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
            int longueurLigneColonne = this.contenu.GetLength(0);

            int[,] contenuResultat = new int[longueurLigneColonne, longueurLigneColonne];

            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    contenuResultat[iterateurLigne, iterateurColonne] = 0;
                }
            }

            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    if (this.contenu[iterateurLigne, iterateurColonne] + matriceBooleene.contenu[iterateurLigne, iterateurColonne] != 0)
                    {
                        contenuResultat[iterateurLigne, iterateurColonne] = 1;
                    }
                    else
                    {
                        contenuResultat[iterateurLigne, iterateurColonne] = 0;
                    }
                }
            }

            return new MatriceBooleene(contenuResultat);
        }

        public bool EstIdentique(MatriceBooleene matriceBooleene)
        {
            int longueurLigneColonne = this.contenu.GetLength(0);

            for (int iterateurLigne = 0; iterateurLigne < longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < longueurLigneColonne; iterateurColonne++)
                {
                    if(this.contenu[iterateurLigne, iterateurColonne] != matriceBooleene.contenu[iterateurLigne, iterateurColonne])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
