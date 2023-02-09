using System.Media;
using VolScore;
using static VolScore.IVolscoreDB;


//déclaration des Variables 
int intNuméroEquipe;
int intChoixEquipe = 1;
char chrAnswer;
char chrAnswerAction;

VolscoreDB vdb = new VolscoreDB();

//Déclaration des list sur les équipe et les matchs 
List<IVolscoreDB.Game> games = vdb.GetGames();
List<IVolscoreDB.Team> teams = vdb.GetTeams();

// CODE POUR L'ÉCRAN D'ACCEUIL

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\n  _   __     ______                \r\n | | / /__  / / __/______  _______ \r\n | |/ / _ \\/ /\\ \\/ __/ _ \\/ __/ -_)\r\n |___/\\___/_/___/\\__/\\___/_/  \\__/ \r\n                                   ");
Console.ForegroundColor = ConsoleColor.White;

Console.WriteLine("\n");

do
{
    Console.WriteLine("Pour voir la liste des match appuyez sur [A] \nPour créer un match appuyer sur [B]");
    chrAnswerAction = Console.ReadKey().KeyChar;

    if (chrAnswerAction != 'a' && chrAnswerAction != 'b' && chrAnswerAction != 'A' && chrAnswerAction != 'B')
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nVous avez rentré une valeur qui est fausse !\n");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
while (chrAnswerAction != 'a' && chrAnswerAction != 'b' && chrAnswerAction != 'A' && chrAnswerAction != 'B');

if (chrAnswerAction == 'a' || chrAnswerAction == 'A')
{

    //CODE POUR LA LISTE DES MATCH 
    do
    {

        Console.Clear();
        Console.WriteLine("---------------------------------------------------------------------------------");
        Console.WriteLine("-                               Liste de matchs                                 -");
        Console.WriteLine("---------------------------------------------------------------------------------\n");

        // Boucle for que permet d'afficher la liste des match 
        for (int i = 0; i < games.Count; i++)
        {
            // Va cherche dans la base de donnée l'équipe à domicil à l'index I, pareil pour les visiteur et les écrire 
            Console.Write(intChoixEquipe + ". " + games[i].ReceivingTeamName + "    VS     " + games[i].VisitingTeamName + "    ¦   " + games[i].Moment + "\n");
            intChoixEquipe++;
        }

        do
        {
            // Demande à l'utilisateur quel équipe il veut les détails 
            Console.Write("\nCliquez sur le chiffre assigné à l'équipe pour voir les détails : ");
            intNuméroEquipe = Convert.ToInt32(Console.ReadLine());
            if (intNuméroEquipe > games.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nVous avez rentrer un chiffre qui ne correspond pas à un match !");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        while (intNuméroEquipe > games.Count);

        Console.WriteLine("\n");
        Console.Clear();

        // la variables mygame contient la valeur du match de l'index choisis par lûtilisateur 
        Game mygame = games[intNuméroEquipe - 1];

        Console.Write($"{mygame.ReceivingTeamName}    VS    {games[intNuméroEquipe - 1].VisitingTeamName}\n\n");
        Console.WriteLine($"Lieu : {games[intNuméroEquipe - 1].Place}");
        Console.Write($"Date du match : {games[intNuméroEquipe - 1].Moment}\n");
        Console.Write("État : ");

        // Condition qui permet d'afficher l'état du match si la date du match est plus petites que la date d'aujourd'hui
        if (DateTime.Now > games[intNuméroEquipe - 1].Moment)
            Console.Write("TERMINÉ\n");
        else
            Console.Write("A VENIR\n");

        Console.WriteLine("Catégorie : " + mygame.Category);
        Console.WriteLine("Ligue : " + games[intNuméroEquipe - 1].League);

        Console.WriteLine("\n");

        Team receiving = vdb.GetTeam(games[intNuméroEquipe - 1].ReceivingTeamId);
        List<IVolscoreDB.Member> test = vdb.GetPlayers(receiving);

        Console.WriteLine("Membre de : " + mygame.ReceivingTeamName + "\n");
        foreach (IVolscoreDB.Member member in test)
        {
            if (member.Number > 9)
                Console.WriteLine($"    {member.Number}  |  {member.FirstName}  {member.LastName}   /   {member.Role}");
            else
                Console.WriteLine($"    {member.Number}   |  {member.FirstName}  {member.LastName}   /   {member.Role}");
        }

        Console.WriteLine("\n");

        Team visiting = vdb.GetTeam(games[intNuméroEquipe - 1].VisitingTeamId);
        List<IVolscoreDB.Member> test2 = vdb.GetPlayers(visiting);

        Console.WriteLine("Membre de : " + mygame.VisitingTeamName + "\n");
        foreach (IVolscoreDB.Member member in test2)
        {
            if (member.Number > 9)
                Console.WriteLine($"    {member.Number}  |  {member.FirstName}  {member.LastName}   /   {member.Role}");
            else
                Console.WriteLine($"    {member.Number}   |  {member.FirstName}  {member.LastName}   /   {member.Role}");
        }

        Console.Write("\nVoulez retourner sur la liste des match ? <o/n> : ");
        chrAnswer = Convert.ToChar(Console.ReadKey().KeyChar);

        intChoixEquipe = 1;

    }
    while (chrAnswer == 'o' || chrAnswer == 'O');
}

else if (chrAnswerAction == 'b' || chrAnswerAction == 'B')
{
    Console.Clear();
    Console.WriteLine("test");
    Console.ReadLine();
}