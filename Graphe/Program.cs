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
}

static void AfficherMatrice(Graphe graphe, int[,] matriceAdjacence)
{
    for (int i = 0; i < graphe.nombreArette; i++)
    {
        for (int j = 0; j < graphe.nombreArette; j++)
        {
            Console.Write(matriceAdjacence[i, j]);
        }
        Console.WriteLine();
    }
}

Graphe graphe = CreerGraphe();

Console.WriteLine(graphe.VerdString());
graphe.TrierParAretesCout();
Console.Write(graphe.VerdString());
int[,] matriceAdjacence = graphe.AvoirMatriceAdjacence();

AfficherMatrice(graphe, matriceAdjacence);
*/
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

