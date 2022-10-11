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

int[,] contenuMatriceA = 
{
    { 1, 5 ,6},
    { 4, 12, 8},
    { 9, 7, 4}
};

int[,] contenuMatriceB = 
{
    { 7, 6 ,6},
    { 14, 1, 8},
    { 9, 6, 10}
};

var matriceA = new Matrice(contenuMatriceA);
var matriceB = new Matrice(contenuMatriceB);

var matriceC = matriceA.Multiplier(matriceB);

Console.WriteLine("---- Produit matriciel ----");

matriceA.AfficherMatrice();
Console.WriteLine("*");
matriceB.AfficherMatrice();
Console.WriteLine("=");
matriceC.AfficherMatrice();

Console.WriteLine("---- Puissance d'une matrice ----");

matriceA.AfficherMatrice();
Console.WriteLine("Puissance 3 :");
var matriceD = matriceA.Puissance(3);
matriceD.AfficherMatrice();

Console.Write("Avoir la nombre de chemin entre le sommet 1 et 4 de longueur 3");

graphe.AfficherNombreChemin(3, 1, 4);

Console.WriteLine("---- Produit de deux matrices booléenes ----");

int[,] contenuMatriceBoolA = {
    {1, 0, 1},
    {0, 0, 1},
    {1, 0, 0}
};

int[,] contenuMatriceBoolB = {
    {1, 0, 1},
    {0, 1, 1},
    {1, 0, 1}
};

var matriceBoolA = new MatriceBooleene(contenuMatriceBoolA);
var matriceBoolB = new MatriceBooleene(contenuMatriceBoolB);
var matriceBoolC = matriceBoolA.Multiplier(matriceBoolB);

matriceBoolA.AfficherMatrice();
Console.WriteLine("*");
matriceBoolB.AfficherMatrice();
Console.WriteLine("=");
matriceBoolC.AfficherMatrice();

Console.WriteLine("---- Somme de deux matrices booléenes ----");

var matriceBoolD = matriceBoolA.Additionner(matriceBoolB);

matriceBoolA.AfficherMatrice();
Console.WriteLine("+");
matriceBoolB.AfficherMatrice();
Console.WriteLine("=");
matriceBoolD.AfficherMatrice();

Console.WriteLine("---- Comparaison de deux matrices identique ----");

matriceBoolA.AfficherMatrice();

bool estIdentique = matriceBoolA.EstIdentique(matriceBoolA);
Console.WriteLine(estIdentique);

Console.WriteLine("---- Comparaison de deux matrices différente ----");

matriceBoolA.AfficherMatrice();

estIdentique = matriceBoolA.EstIdentique(matriceBoolB);
Console.WriteLine(estIdentique);

Console.WriteLine("---- Matrice transitive ----");

var matriceTransitive = graphe.AvoirMatriceTransitive();
matriceTransitive.AfficherMatrice();
