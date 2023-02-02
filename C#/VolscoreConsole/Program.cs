using VolScore;

//COnstantes
const byte BYT_NBR_MATCH = 250;

//Tableau 
string[,] tab_match = new string[BYT_NBR_MATCH, 2];

//Variables 
char chrNuméroEquipe;
char chrChoixEquipe = 'a';
byte bytNumCaseTab = 0;
int chrTemp;

VolscoreDB vdb = new VolscoreDB();
List<IVolscoreDB.Game> games = vdb.GetGames();
List<IVolscoreDB.Team> teams = vdb.GetTeams();

Console.WriteLine("---------------------------------------------------------------------------------");
Console.WriteLine("-                               Liste de matchs                                 -");
Console.WriteLine("---------------------------------------------------------------------------------\n");

foreach (IVolscoreDB.Game game in games)
{
    Console.Write((char)chrChoixEquipe + ". " + $"{game.ReceivingTeamName}    VS    {game.VisitingTeamName}\n");
    chrTemp = chrChoixEquipe;
    chrTemp = chrTemp - (chrTemp - 1);
    tab_match[chrTemp, 0] = game.ReceivingTeamName;
    tab_match[chrTemp, 1] = game.VisitingTeamName;
    
    chrChoixEquipe++;
}

Console.Write("\nCliquez sur le chiffre assigné à l'équipe pour voir les détails : ");
chrNuméroEquipe = Convert.ToChar(Console.ReadKey().KeyChar);
Console.WriteLine("\n");

if (chrNuméroEquipe == 'a')
{
    Console.Clear();
    Console.Write($"{games.Count}");
    Console.WriteLine(games[0].VisitingTeamName);
}


Console.ReadKey();