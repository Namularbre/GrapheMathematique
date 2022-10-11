using ApplicationGraphe;
//Cette fonction permet de saisir un graphe arêtes par arêtes.
static Graphe CreerGraphe()
{
    //On récupère les nouvelles arêtes dans un HASHSET, qui est une liste ne comportant aucun doublons.
    HashSet<Arete> nouvelleAretes = new HashSet<Arete>();
    const int REPONSE_CONTINUER = 1;
    const int REPONSE_ARRETER_SAISI = 0;

    int reponse = REPONSE_CONTINUER;
    //Tant que l'utilisateur n'a pas saisi le nombre qui arête la boucle while, il peut saisir des arêtes 
    while (reponse != REPONSE_ARRETER_SAISI)
    {
        try
        {
            //Note : int.Parse() convertie une chaine en entier. Si l'utilisateur ne saisi pas un entier, il arive dans la partie catch, lui
            //fesant reprendre la saisi.

            //Saisi du "Sommet de départ". Evidemment nos graphe ne sont pas orientés, mais nous avons gardez cette notation.
            Console.WriteLine("Entrez le sommet d'où part votre arête");
            int sommetDepart = int.Parse(Console.ReadLine());
            //Saisi du "Sommet d'arrivé" de l'arête
            Console.WriteLine("Entre le sommet d'où arrive votre arête");
            int sommetArrive = int.Parse(Console.ReadLine());
            //Saisi du poid de l'arête, sa valeur.
            Console.WriteLine("Entrez le poid de votre arête");
            int poidArete = int.Parse(Console.ReadLine());
            //On récupère la réponse de l'utilisateur afin de savoir si il veut continuer la saisi.
            Console.WriteLine("Continuer? 0=non 1=oui");
            reponse = int.Parse(Console.ReadLine());

            //On ajoute l'arête saisi dans notre liste d'arêtes.
            nouvelleAretes.Add(new Arete(sommetDepart, sommetArrive, poidArete));
        }
        //Si l'utilisateur fait une saisie frauduleuse, on lui affiche un message d'erreur, et il recommence la saisi.
        catch (Exception)
        {
            Console.WriteLine("Vous n'avez pas fait une saisi valide !");
        }
    }
    //On retourne un nouveau graphe, en lui donnant les arêtes saisi et leur nombre (avec Count)
    return new Graphe(nouvelleAretes);
}
//Ici, on récupère le nouveau graphe généré par la saisi de l'utilisateur.
Graphe graphe = CreerGraphe();

Console.WriteLine("---- Graphe saisi ----");
Console.WriteLine(graphe.VerdString());

var matriceAdjacence = graphe.AvoirMatriceAdjacence();

Console.WriteLine("---- Matrice d'adjacence ----");
matriceAdjacence.AfficherMatrice();

