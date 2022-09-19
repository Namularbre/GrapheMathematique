using ApplicationGraphe;

Graphe CreerGraphe()
{
    HashSet<Arete> nouvelleArete = new HashSet<Arete>();
    char reponse = ' ';
    while (reponse != 'n')
    {
        Console.WriteLine("Entrez le sommet d'où part votre arête");
        int sommetDepart = int.Parse(Console.ReadLine());
        Console.WriteLine("Entre le sommet d'où arrive votre arête");
        int sommetArrive = int.Parse(Console.ReadLine());
        Console.WriteLine("Entrez le poid de votre arête");
        int poidArete = int.Parse(Console.ReadLine());
        Console.WriteLine("Continuer? n=non autre=oui");
        reponse = char.Parse(Console.ReadLine());

        nouvelleArete.Add(new Arete(sommetDepart, sommetArrive, poidArete));
    }

    return new Graphe(nouvelleArete, nouvelleArete.Count);
}

Graphe graphe = CreerGraphe();

Console.WriteLine(graphe.VerdString());
graphe.TrierParAretesCout();
Console.Write(graphe.VerdString());