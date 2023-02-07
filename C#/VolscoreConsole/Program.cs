using VolScore;
using static VolScore.IVolscoreDB;

//Variables 
int intNuméroEquipe;
int intChoixEquipe = 0;
char chrAnswer;

VolscoreDB vdb = new VolscoreDB();
List<IVolscoreDB.Game> games = vdb.GetGames();
List<IVolscoreDB.Team> teams = vdb.GetTeams();

do
{


    Console.WriteLine("---------------------------------------------------------------------------------");
    Console.WriteLine("-                               Liste de matchs                                 -");
    Console.WriteLine("---------------------------------------------------------------------------------\n");
    /*
    for (int i = 0; i < games.count; i++)
    {
        Console.Write(game[i + 1].RecievingTeamName);
        
        Console.Write(             " VS ");
        Console.SetCursorPosition(, i + 5);
        Console.Write(game.
    
    
    
    }
    
    */
    
    
    foreach (IVolscoreDB.Game game in games)
    {
        Console.Write(intChoixEquipe + ". " + game.ReceivingTeamName + "    VS     " + game.VisitingTeamName + "    ¦   " + game.Moment + "\n");
        intChoixEquipe++;
    }

    Console.Write("\nCliquez sur le chiffre assigné à l'équipe pour voir les détails : ");
    intNuméroEquipe = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("\n");
    Console.Clear();

    Game mygame = games[intNuméroEquipe];

    Console.Write($"{mygame.ReceivingTeamName}    VS    {games[intNuméroEquipe].VisitingTeamName}\n\n");
    Console.WriteLine($"Lieu : {games[intNuméroEquipe].Place}");
    Console.Write($"Date du match : {games[intNuméroEquipe].Moment}\n");
    Console.Write("État : ");

    if (DateTime.Now > games[intNuméroEquipe].Moment)
        Console.Write("TERMINÉ\n");
    else
        Console.Write("A VENIR\n");

    Console.WriteLine("Catégorie : " + mygame.Category);
    Console.WriteLine("Ligue : " + games[intNuméroEquipe].League);

    Console.WriteLine("\n");

    Team receiving = vdb.GetTeam(games[intNuméroEquipe].ReceivingTeamId);
    List<IVolscoreDB.Member> test = vdb.GetPlayers(receiving);

    Console.WriteLine("Membre de : " + mygame.ReceivingTeamName);
    foreach (IVolscoreDB.Member member in test)
    {
        if (member.Number > 9)
            Console.WriteLine($"    {member.Number}  |  {member.FirstName}  {member.LastName}   /   {member.Role}");
        else
            Console.WriteLine($"    {member.Number}   |  {member.FirstName}  {member.LastName}   /   {member.Role}");
    }

    Console.WriteLine("\n");

    Team receiving2 = vdb.GetTeam(games[intNuméroEquipe].VisitingTeamId);
    List<IVolscoreDB.Member> tes2 = vdb.GetPlayers(receiving);

    Console.WriteLine("Membre de : " + mygame.VisitingTeamName);
    foreach (IVolscoreDB.Member member in test)
    {
        if (member.Number > 9)
            Console.WriteLine($"    {member.Number}  |  {member.FirstName}  {member.LastName}   /   {member.Role}");
        else
            Console.WriteLine($"    {member.Number}   |  {member.FirstName}  {member.LastName}   /   {member.Role}");
    }

    Console.Write("Voulez retourner sur la liste des match ? <o/n> : ");
    chrAnswer = Convert.ToChar(Console.ReadKey().KeyChar);

}
while (chrAnswer == 'o' || chrAnswer == 'O');
