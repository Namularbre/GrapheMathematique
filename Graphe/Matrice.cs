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

        public Matrice(int[,] contenu)
        {
            this.contenu = contenu;
        }

        public void AfficherMatrice()
        {
            const char ESPACE = ' ';
            for (int i = 0; i < contenu.Length; i++)
            {
                for (int j = 0; j < contenu.Length; j++)
                {
                    Console.Write(this.contenu[i, j] + ESPACE);
                }
                Console.WriteLine();
            }
        }

        public Matrice Multiplier(Matrice matrice)
        {
            Matrice matriceProduit = new Matrice(new int[contenu.Length, contenu.Length]);

            for (int iterateurLigne = 0; iterateurLigne < contenu.Length; iterateurLigne++)
            {
                for (int iterateurColonne = 0; iterateurColonne < contenu.Length; iterateurColonne++)
                {
                    for (int k = 0; k < contenu.Length; k++)
                    {
                        matriceProduit.contenu[iterateurLigne, iterateurColonne] += this.contenu[iterateurLigne, k] * matrice.contenu[k, iterateurColonne];
                    }
                }
            }

            return matriceProduit;
        }

        public Matrice Exponentiel(int exponentiel)
        {
            Matrice matriceProduit = this;

            for (int iterateur = 0; iterateur < contenu.Length; iterateur++)
            {
                matriceProduit = this.Multiplier(this);
            }

            return matriceProduit;
        }
    }
}
