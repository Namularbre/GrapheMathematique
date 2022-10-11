using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Matrice
    {
        //Un tableau de deux dimensions qui représente le contenu d'une matrice
        public int[,] contenu { get; set; }
        //Les matrices étant toutes carrées, on stoque seulement la longueur de la ligne.
        public int longueurLigneColonne { get; set; }

        public Matrice(int[,] contenu)
        {
            this.contenu = contenu;
            //On récupère le nombre de lignes de la matrice.
            this.longueurLigneColonne = contenu.GetLength(0);
        }
        //Cette fonction affiche la matrice
        public void AfficherMatrice()
        {
            const char ESPACE = ' ';
            const char DEBUT_CASE = '[';
            const char FIN_CASE = ']';
            //On itère sur chaque élément présent dans le contenu et on l'affiche
            for (int iterateurLigne = 0; iterateurLigne < this.longueurLigneColonne; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < this.longueurLigneColonne; iterateurColonne++)
                {
                    Console.Write(DEBUT_CASE + this.contenu[iterateurLigne, iterateurColonne].ToString() + FIN_CASE + ESPACE);
                }
                Console.WriteLine();
            }
        }
        
        //Cette fonction multiplie deux matrices (celle en argument et la matrice this)
        public Matrice Multiplier(Matrice matrice)
        {
            //On créé une matrice vide de même dimension que les deux matrices carrées du produit
            Matrice matriceProduit = new Matrice(new int[this.longueurLigneColonne, this.longueurLigneColonne]);
            //On fait le calcul du produit pour chaque quotient.
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
            //On retourne le résultat du calcul
            return matriceProduit;
        }
        //Cette méthode calcul une matrice à la puissance donné en paramètre
        public Matrice Puissance(int puissance)
        {
            //Si la puissance est 0, alors on retourne une matrice identité de même taille.
            if(puissance == 0)
            {
                return this.FaireMatriceIdentiteDeMemeTaille();
            }
            //Si la puissance vaut 1, on retourne la matrice
            if (puissance == 1)
            {
                return this;
            }
            //La matrice produit est la matrice qui contiendra le résultat. Par défaut, elle vaut la matrice sur laquelle on travaille.
            Matrice matriceProduit = this;
            //On fait une multiplication succésive pour avoir la puissance de la matrice
            for (int iterateur = 0; iterateur < puissance - 1; iterateur++)
            {
                matriceProduit = matriceProduit.Multiplier(this);
            }
            //On retourne la matrice
            return matriceProduit;
        }

        //Cette fonction génère une matrice identidé de dimension égale à la matrice actuelle (this, self en python je crois)
        private Matrice FaireMatriceIdentiteDeMemeTaille()
        {
            //On crée un tableau de deux dimension ayant la même taille que la matrice actuel
            var contenuMatriceIdentiteDeMemeTaille = new int[this.longueurLigneColonne, this.longueurLigneColonne];
            
            //Pour chaque ligne, pour chaque colonne
            for(int iterateurLigne = 0; iterateurLigne < this.longueurLigneColonne; iterateurLigne++)
            {
                for(int iterateurColonne = 0; iterateurColonne < this.longueurLigneColonne; iterateurColonne++)
                {
                    //Si les itérateurs sont égaux, alors on est sur la diagonale de la matrice, on place 1
                    if(iterateurLigne != iterateurColonne)
                    {
                        contenuMatriceIdentiteDeMemeTaille[iterateurColonne, iterateurLigne] = 0;
                    }
                    //Sinon 0.
                    else
                    {
                        contenuMatriceIdentiteDeMemeTaille[iterateurColonne, iterateurLigne] = 1;
                    }
                }
            }
            //On retourne la matrice
            return new Matrice(contenuMatriceIdentiteDeMemeTaille);
        }
    }
}
