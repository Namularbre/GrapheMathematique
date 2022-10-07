using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGraphe
{
    internal class Arbre
    {
        public HashSet<Arete> aretes;

        public Arbre()
        {
            this.aretes = new HashSet<Arete>();
        }

        public bool ContientArete(Arete arete)
        {
            return this.aretes.Contains(arete);
        }

        public void AjouterArete(Arete arete)
        {
            if (!FaitUneBoucle(arete))
            {
                this.aretes.Add(arete);
            }
        }

        public bool FaitUneBoucle(Arete arete)
        {
            if (this.aretes.Count <= 2)
            {
                return false;
            }

            var contenuArbrePlusNouvelleArete = this.aretes.ToList();
            contenuArbrePlusNouvelleArete.Add(arete);

            var areteParcourues = new List<Arete>();
            // Comment détecter une boucle ?
            var aretesVoisines = ParcourirAreteAdjacenteAuxSommetsDuneArete(arete, contenuArbrePlusNouvelleArete, areteParcourues);

            foreach (var areteSelectionneesDansAretesVoisines in aretesVoisines)
            {
                int nombreApparition = 0;

                foreach (var aretesComparereDansAretesVoisines in aretesVoisines)
                {
                    if (areteSelectionneesDansAretesVoisines.Equals(aretesComparereDansAretesVoisines))
                    {
                        nombreApparition++;
                    }
                }

                if (nombreApparition >= 2)
                {
                    return true;
                }
            }    

            return false;
        }

        private List<Arete> ParcourirAreteAdjacenteAuxSommetsDuneArete(Arete arete, List<Arete> arbreCouvrantPoindMinimal, List<Arete> areteParcourues)
        {
            areteParcourues.Add(arete);

            Console.WriteLine(arete.VersString());

            foreach (var areteArbre in arbreCouvrantPoindMinimal)
            {
                if (!areteArbre.Equals(arete) && (areteArbre.sommetArrive == arete.sommetArrive || areteArbre.sommetDepart == arete.sommetArrive) && !areteParcourues.Contains(areteArbre))
                {
                    return areteParcourues.Concat(ParcourirAreteAdjacenteAuxSommetsDuneArete(areteArbre, arbreCouvrantPoindMinimal, areteParcourues)).ToList();
                }
            }

            return areteParcourues;
        }

        //private List<Arete> ParcourirAreteAdjacenteAuxSommetsDuneArete(Arete arete, List<Arete> arbreCouvrantPoindMinimal)
        //{
        //    int sommetDebut = arete.sommetDepart;
        //    int sommetArrive = arete.sommetArrive;

        //    List<Arete> aretesReliantSommet = new List<Arete>();

        //    foreach(var areteArbre in arbreCouvrantPoindMinimal)
        //    {
        //        //Si l'arête n'est pas notre arête
        //        if (!areteArbre.Equals(arete))
        //        {
        //            int sommetDebutAreteArbre = areteArbre.sommetDepart;
        //            int sommetArriveAreteArbre = areteArbre.sommetArrive;
        //            //Si un des deux sommets relis notre, on ajoute l'arete, et on regarde ces aretes adjacentes
        //            if (sommetArriveAreteArbre == sommetArrive || sommetDebutAreteArbre == sommetDebut)
        //            {
        //                aretesReliantSommet.Add(areteArbre);
        //                aretesReliantSommet.Concat(ParcourirAreteAdjacenteAuxSommetsDuneArete(areteArbre, arbreCouvrantPoindMinimal));
        //            }
        //        }
        //    }

        //    return aretesReliantSommet;
        //}

        public void Afficher()
        {
            foreach (var arete in this.aretes)
            {
                Console.WriteLine(arete.VersString());
            }
        }
    }
}
