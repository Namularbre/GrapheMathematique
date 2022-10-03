using ApplicationGraphe;

/*static Graphe CreerGraphe()
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
            Console.WriteLine("Continuer? 0=non 1=oui");
            reponse = int.Parse(Console.ReadLine());

            nouvelleAretes.Add(new Arete(sommetDepart, sommetArrive, poidArete));
        }
        catch (Exception)
        {
            Console.WriteLine("Vous n'avez pas fait une saisi valide !");
        }
    }

    return new Graphe(nouvelleAretes, nouvelleAretes.Count);
}*/

//Graphe graphe = CreerGraphe();

Arete arete1 = new Arete(1, 2, 0);
Arete arete2 = new Arete(2, 3, 0);
Arete arete3 = new Arete(3, 1, 0);
Arete arete4 = new Arete(4, 2, 0);

HashSet<Arete> aretes = new HashSet<Arete>();

aretes.Add(arete1);
aretes.Add(arete2);
aretes.Add(arete3);
aretes.Add(arete4);

Graphe graphe = new Graphe(aretes, 4);

Console.WriteLine(graphe.VerdString());
graphe.TrierParAretesCout();
Console.Write(graphe.VerdString());
var matriceAdjacence = graphe.AvoirMatriceAdjacence();

matriceAdjacence.AfficherMatrice();

int[,] matriceA = new int[,] 
{
    {1, 2, 3},
    {4, 5, 6},
    {7, 8, 9}
};

int[,] matriceB = new int[,]
{
    {9, 8, 7},
    {6, 5, 4},
    {3, 2, 1}
};

var m1 = new Matrice(matriceA, 3);
var m2 = new Matrice(matriceB, 3);

var m12 = m1.Multiplier(m2);
var m21 = m2.Multiplier(m1);
var me2 = m1.Puissance(0);

graphe.AfficherChemin(matriceAdjacence, 4, 1, 4);

int[,] contenuMatriceBool1 = new int[3, 3]
{
    {1, 0, 0},
    {0, 1, 0},
    {1, 0, 1}
};

int[,] contenuMatriceBool2 = new int[3, 3]
{
    {1, 0, 1},
    {0, 1, 1},
    {0, 0, 1}
};

var matriceBooleene1 = new MatriceBooleene(contenuMatriceBool1);
var matriceBooleene2 = new MatriceBooleene(contenuMatriceBool2);

var mb12 = matriceBooleene1.Multiplier(matriceBooleene2);

mb12.AfficherMatrice();
Console.WriteLine("-----------");
var mb1plus2 = matriceBooleene1.Additionner(matriceBooleene2);

mb1plus2.AfficherMatrice();
