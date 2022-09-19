using ApplicationGraphe;

Graphe CreerGraphe()
{
    HashSet<Arete> nouvelleAretes = new HashSet<Arete>();
    int reponse = 1;
    while (reponse != 0)
    {
        try
        {
            Console.WriteLine("Entrez le sommet d'où part votre arête");
            int sommetDepart = int.Parse(Console.ReadLine());
            Console.WriteLine("Entre le sommet d'où arrive votre arête");
            int sommetArrive = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le poid de votre arête");
            int poidArete = int.Parse(Console.ReadLine());
            Console.WriteLine("Continuer? n=non autre=oui");
            reponse = int.Parse(Console.ReadLine());

            nouvelleAretes.Add(new Arete(sommetDepart, sommetArrive, poidArete));
        }
        catch (Exception)
        {
            Console.WriteLine("Vous n'avez pas fait une saisi valide !");
        }
    }

    return new Graphe(nouvelleAretes, nouvelleAretes.Count);
}

Graphe graphe = CreerGraphe();

Console.WriteLine(graphe.VerdString());
graphe.TrierParAretesCout();
Console.Write(graphe.VerdString());