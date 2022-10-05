using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Matrice
    {
        public int[,] contenu { get; set; }
        public int longueurLigneColonne { get; set; }

        public Matrice(int[,] contenu, int longueurLigneColonne)
        {
            this.contenu = contenu;
            this.longueurLigneColonne = longueurLigneColonne;
        }

        public void AfficherMatrice()
        {
            const char ESPACE = ' ';
            const char DEBUT_CASE = '[';
            const char FIN_CASE = ']';
            
            for (int iterateurLigne = 0; iterateurLigne < this.longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < this.longueurLigneColonne; iterateurColonne++)
                {
                    Console.Write(DEBUT_CASE + this.contenu[iterateurLigne, iterateurColonne].ToString() + FIN_CASE + ESPACE);
                }
                Console.WriteLine();
            }
        }

        public Matrice Multiplier(Matrice matrice)
        {
            Matrice matriceProduit = new Matrice(new int[this.longueurLigneColonne, this.longueurLigneColonne], this.longueurLigneColonne);

            for (int iterateurLigne = 0; iterateurLigne < this.longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < this.longueurLigneColonne; iterateurColonne++)
                {
                    for (int k = 0; k < this.longueurLigneColonne; k++)
                    {
                        matriceProduit.contenu[iterateurLigne, iterateurColonne] += this.contenu[iterateurLigne, k] * matrice.contenu[k, iterateurColonne];
                    }
                }
            }

            return matriceProduit;
        }
        
        public Matrice Puissance(int puissance)
        {
            if(puissance == 0)
            {
                return this.faireMatriceIdentiteDeMemeTaille();
            }

            if (puissance == 1)
            {
                return this;
            }

            Matrice matriceProduit = this;

            for (int iterateur = 0; iterateur < puissance - 1; iterateur++)
            {
                matriceProduit = matriceProduit.Multiplier(this);
            }

            return matriceProduit;
        }

        private Matrice faireMatriceIdentiteDeMemeTaille()
        {
            var contenuMatriceIdentiteDeMemeTaille = new int[this.longueurLigneColonne, this.longueurLigneColonne];
            
            for(int iterateurLigne = 0; iterateurLigne < this.longueurLigneColonne; iterateurLigne++)
            {
                for(int iterateurColonne = 0; iterateurColonne < this.longueurLigneColonne; iterateurColonne++)
                {
                    if(iterateurLigne != iterateurColonne)
                    {
                        contenuMatriceIdentiteDeMemeTaille[iterateurColonne, iterateurLigne] = 0;
                    }
                    else
                    {
                        contenuMatriceIdentiteDeMemeTaille[iterateurColonne, iterateurLigne] = 1;
                    }
                }
            }

            return new Matrice(contenuMatriceIdentiteDeMemeTaille, this.longueurLigneColonne);
        }
    }
}
