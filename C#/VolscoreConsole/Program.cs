using VolScore;

string strTest;

VolscoreDB vdb = new VolscoreDB();
List<IVolscoreDB.Game> games = vdb.GetGames();

foreach (IVolscoreDB.Game game in games)
{
    Console.WriteLine($"{game.ReceivingTeamName}      VS      {game.VisitingTeamName}");
}

Console.ReadKey();


