using System.Media;
using VolScore;
using static VolScore.IVolscoreDB;


//déclaration des Variables 
int intNuméroEquipe;
int intChoixEquipe = 1;
char chrAnswer;
char chrAnswerDetail;
char chrAnswerAction;
char chrAnswerAcceuil;

int intEquipe1;
int intEquipe2;
int intCmpteur = 1;
string strLieuMatch;
string strCategorie;
string strTypeMatch;
string strNiveaMatch;
string strLigue;
string strNomDeLaSalle;
char chrValidationMatch;


DateTime testdate;
Random testrandom = new Random();
testrandom.Next(10000);


SoundPlayer _Test = new SoundPlayer("Microsoft-Windows-XP-Error-Sound-Effect-HD.wav");

VolscoreDB vdb = new VolscoreDB();

//Déclaration des list sur les équipe et les matchs 
List<IVolscoreDB.Game> games = vdb.GetGames();
List<IVolscoreDB.Team> teams = vdb.GetTeams();

// CODE POUR L'ÉCRAN D'ACCEUIL

do
{

    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n  _   __     ______                \r\n | | / /__  / / __/______  _______ \r\n | |/ / _ \\/ /\\ \\/ __/ _ \\/ __/ -_)\r\n |___/\\___/_/___/\\__/\\___/_/  \\__/ \r\n                                   ");
    Console.ForegroundColor = ConsoleColor.White;

    Console.WriteLine("\n");

    //Boucle qui permet de recomencer si l'utilisateur à fais une fausse manipulation 
    do
    {
        //demande à l'utilisateur dquel action il faut faire 
        Console.WriteLine("Pour voir la liste des match appuyez sur [A] \nPour créer un match appuyer sur [B]");
        chrAnswerAction = Console.ReadKey().KeyChar;

        if (chrAnswerAction != 'a' && chrAnswerAction != 'b' && chrAnswerAction != 'A' && chrAnswerAction != 'B')
        {
            _Test.Play();
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ___        __      ________  ___________  _______      ___      ___       __  ___________  ______    __    __   \r\n|\"  |      |\" \\    /\"       )(\"     _   \")/\"     \"|    |\"  \\    /\"  |     /\"\"\\(\"     _   \")/\" _  \"\\  /\" |  | \"\\  \r\n||  |      ||  |  (:   \\___/  )__/  \\\\__/(: ______)     \\   \\  //   |    /    \\)__/  \\\\__/(: ( \\___)(:  (__)  :) \r\n|:  |      |:  |   \\___  \\       \\\\_ /    \\/    |       /\\\\  \\/.    |   /' /\\  \\  \\\\_ /    \\/ \\      \\/      \\/  \r\n \\  |___   |.  |    __/  \\\\      |.  |    // ___)_     |: \\.        |  //  __'  \\ |.  |    //  \\ _   //  __  \\\\  \r\n( \\_|:  \\  /\\  |\\  /\" \\   :)     \\:  |   (:      \"|    |.  \\    /:  | /   /  \\\\  \\\\:  |   (:   _) \\ (:  (  )  :) \r\n \\_______)(__\\_|_)(_______/       \\__|    \\_______)    |___|\\__/|___|(___/    \\___)\\__|    \\_______) \\__|  |__/  \r\n                                                                                                                 ");
            Console.ResetColor();

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
                    _Test.Play();
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

            // Affiche les catégorie des matchs avec leurs ligues
            Console.WriteLine("Catégorie : " + mygame.Category);

            Console.WriteLine("\n");

            // Déclare la liste contenent la liste des joueur de l'équipe a domicile
            Team receiving = vdb.GetTeam(games[intNuméroEquipe - 1].ReceivingTeamId);
            List<IVolscoreDB.Member> test = vdb.GetPlayers(receiving);

            Console.WriteLine("Membre de : " + mygame.ReceivingTeamName + "\n");
            // Boucle qui permet d'afficher les joueurs 
            foreach (IVolscoreDB.Member member in test)
            {
                // Condition pour aligner les chiffre 
                if (member.Number > 9)
                    Console.WriteLine($"    {member.Number}  |  {member.FirstName}  {member.LastName}   /   {member.Role}");
                else
                    Console.WriteLine($"    {member.Number}   |  {member.FirstName}  {member.LastName}   /   {member.Role}");
            }

            Console.WriteLine("\n");

            // Déclare la liste contenent la liste des joueur de l'équipe visiteuse 
            Team visiting = vdb.GetTeam(games[intNuméroEquipe - 1].VisitingTeamId);
            List<IVolscoreDB.Member> test2 = vdb.GetPlayers(visiting);

            Console.WriteLine("Membre de : " + mygame.VisitingTeamName + "\n");
            // Boucle qui permet d'afficher les joueurs
            foreach (IVolscoreDB.Member member in test2)
            {
                // Condition pour aligner les chiffre 
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

    // Condition qui permet d'afficher la page de création de match 
    else if (chrAnswerAction == 'b' || chrAnswerAction == 'B')
    {
        do
        {
            Game NewGamer = new Game();


            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ______    _______    _______   _______   _______       ___      ___       __  ___________  ______    __    __   \r\n /\" _  \"\\  /\"      \\  /\"     \"| /\"     \"| /\"      \\     |\"  \\    /\"  |     /\"\"\\(\"     _   \")/\" _  \"\\  /\" |  | \"\\  \r\n(: ( \\___)|:        |(: ______)(: ______)|:        |     \\   \\  //   |    /    \\)__/  \\\\__/(: ( \\___)(:  (__)  :) \r\n \\/ \\     |_____/   ) \\/    |   \\/    |  |_____/   )     /\\\\  \\/.    |   /' /\\  \\  \\\\_ /    \\/ \\      \\/      \\/  \r\n //  \\ _   //      /  // ___)_  // ___)_  //      /     |: \\.        |  //  __'  \\ |.  |    //  \\ _   //  __  \\\\  \r\n(:   _) \\ |:  __   \\ (:      \"|(:      \"||:  __   \\     |.  \\    /:  | /   /  \\\\  \\\\:  |   (:   _) \\ (:  (  )  :) \r\n \\_______)|__|  \\___) \\_______) \\_______)|__|  \\___)    |___|\\__/|___|(___/    \\___)\\__|    \\_______) \\__|  |__/  \r\n                                                                                                                  ");
            Console.ResetColor();
            Console.WriteLine("\n");

            do
            {
                do
                {

                    for (int i = 0; i < teams.Count; i++)
                    {
                        Console.WriteLine(intCmpteur + ". " + teams[i].Name);
                        intCmpteur++;
                    }

                    Console.Write("Veuillez choisir l'équipe 1 : ");
                    intEquipe1 = Convert.ToInt32(Console.ReadLine());

                    if (intEquipe1 > teams.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vous avez rentré une valeur qui est fausse !!");
                        Console.ResetColor();
                    }

                    intCmpteur = 1;

                }
                while (intEquipe1 > teams.Count);

                NewGamer.ReceivingTeamName = teams[intEquipe1 - 1].Name;
                NewGamer.ReceivingTeamId = teams[intEquipe1 - 1].Id;
                Console.Write("Équipe 1 : " + teams[intEquipe1 - 1].Name);
                Console.WriteLine("\n");

                do
                {
                    intCmpteur = 1;
                    for (int i = 0; i < teams.Count; i++)
                    {
                        Console.WriteLine(intCmpteur + ". " + teams[i].Name);
                        intCmpteur++;
                    }
                    Console.WriteLine("Veuillez choisir l'équipe 2 :");
                    intEquipe2 = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Équipe 2 : " + teams[intEquipe2 - 1].Name);

                    if (intEquipe2 == intEquipe1)
                    {
                        _Test.Play();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nVous avez déjà choisi cette équipe pour l'équipe 1 !!!\n");
                        Console.ResetColor();
                    }
                    if (intEquipe2 > teams.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vous avez rentré une valeur qui est fausse !!");
                        Console.ResetColor();
                    }
                }
                while (intEquipe1 == intEquipe2 || intEquipe2 > teams.Count);

                NewGamer.VisitingTeamName = teams[intEquipe2 - 1].Name;
                NewGamer.VisitingTeamId = teams[intEquipe2 - 1].Id;

                Console.WriteLine("\n");

                Console.Write("Veuillez entrer la date sur match (JJ.MM.AA hh:mm:ss) : ");
                testdate = Convert.ToDateTime(Console.ReadLine());
                NewGamer.Moment = testdate;
                Console.WriteLine();
                Console.Write("Veuillez entrer le lieu du match : ");
                strLieuMatch = Convert.ToString(Console.ReadLine());
                NewGamer.Place = strLieuMatch;
                Console.WriteLine();
                do
                {
                    Console.Write("Veuillez entrer la catégorie du match <M/F> : ");
                    strCategorie = Convert.ToString(Console.ReadLine());
                    if (strCategorie != "M" && strCategorie != "F")
                    {
                        _Test.Play();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVeuillez choisir parmis les proposition données !!\n");
                        Console.ResetColor();
                    }
                }
                while (strCategorie != "M" && strCategorie != "F");
                NewGamer.Category = strCategorie;
                Console.WriteLine();

                Console.Write("Quel est le type du match ? : ");
                strTypeMatch = Convert.ToString(Console.ReadLine());
                NewGamer.Type = strTypeMatch;
                Console.WriteLine();

                Console.Write("Quel est le niveau du match ? : ");
                strNiveaMatch = Convert.ToString(Console.ReadLine());
                NewGamer.Level = strNiveaMatch;
                Console.WriteLine();

                Console.Write("Quel est la ligue du match ? : ");
                strLigue = Convert.ToString(Console.ReadLine());
                NewGamer.League = strLigue;
                Console.WriteLine();

                Console.Write("Quel est le nom de la salle où aura lieu le match ? : ");
                strNomDeLaSalle = Convert.ToString(Console.ReadLine());
                NewGamer.Venue = strNomDeLaSalle;
                Console.WriteLine();

                Console.Write("Voulez Voir les détails du match ? <o/n> : ");
                chrAnswerDetail = Console.ReadKey().KeyChar;
                if (chrAnswerDetail == 'o' || chrAnswerDetail == 'O')
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ________    _______  ___________   __        __    ___           ___      ___       __  ___________  ______    __    __   \r\n|\"      \"\\  /\"     \"|(\"     _   \") /\"\"\\      |\" \\  |\"  |         |\"  \\    /\"  |     /\"\"\\(\"     _   \")/\" _  \"\\  /\" |  | \"\\  \r\n(.  ___  :)(: ______) )__/  \\\\__/ /    \\     ||  | ||  |          \\   \\  //   |    /    \\)__/  \\\\__/(: ( \\___)(:  (__)  :) \r\n|: \\   ) || \\/    |      \\\\_ /   /' /\\  \\    |:  | |:  |          /\\\\  \\/.    |   /' /\\  \\  \\\\_ /    \\/ \\      \\/      \\/  \r\n(| (___\\ || // ___)_     |.  |  //  __'  \\   |.  |  \\  |___      |: \\.        |  //  __'  \\ |.  |    //  \\ _   //  __  \\\\  \r\n|:       :)(:      \"|    \\:  | /   /  \\\\  \\  /\\  |\\( \\_|:  \\     |.  \\    /:  | /   /  \\\\  \\\\:  |   (:   _) \\ (:  (  )  :) \r\n(________/  \\_______)     \\__|(___/    \\___)(__\\_|_)\\_______)    |___|\\__/|___|(___/    \\___)\\__|    \\_______) \\__|  |__/  \r\n                                                                                                                           ");
                    Console.ResetColor();
                    Console.WriteLine("\n");

                    Console.WriteLine("Équipes : " + teams[intEquipe1 - 1].Name + "  VS  " + teams[intEquipe2 - 1].Name);
                    Console.WriteLine("date : " + testdate);
                    Console.WriteLine("Lieu : " + strLieuMatch);
                    Console.WriteLine("Catégorie : " + strCategorie);
                    Console.WriteLine("Type : " + strTypeMatch);
                    Console.WriteLine("Niveau : " + strNiveaMatch);
                    Console.WriteLine("Salle : " + strNomDeLaSalle);
                    Console.WriteLine();
                    Console.WriteLine("Appuyer sur une touche pour continuer");
                    Console.ReadLine();
                }

                Console.WriteLine("Etes-vous sûr des information que vous avez rentré ? <o/n>");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ATTENTION TOUTE ACTION SERA DEFINITIVE EN ETES VOUS SÛR ? <o/n> : ");
                Console.ResetColor();
                chrValidationMatch = Convert.ToChar(Console.ReadLine());
            }
            while (chrValidationMatch == 'n' || chrValidationMatch == 'N');
            vdb.CreateGame(NewGamer);

            Console.WriteLine();

            Console.Write("Voulez-vous refaire un match ? <o/n> : ");
            chrAnswer = Console.ReadKey().KeyChar;
        }
        while (chrAnswer == 'o' || chrAnswer == 'O');

    }

    Console.Write("\n\nVoulez-vous retourner à l'écran d'acceuil ? <o/n> : ");
    chrAnswerAcceuil = Console.ReadKey().KeyChar;
}
while (chrAnswerAcceuil == 'o' || chrAnswerAcceuil == 'O');