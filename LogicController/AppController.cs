using DataAccess.Structures.DBStructures;
using Microsoft.Extensions.Options;

namespace LogicController
{
    public class AppController
    {
        bool playersSelected { get; set; } = false;
        bool shipsConfigured { get; set; } = false;

        Option[] options { get; set; }

        PlayerGameInit? playerGameInit = null;
        ConfigurePlayerShips? configurePlayerShips = null;

        public void LockinPlayers()
        {
            //Lock in players
            playerGameInit = new();
            playersSelected = true;
        }

        public void ShipsConfigured()
        {
            if (playersSelected != null)
            {
                configurePlayerShips = new(playerGameInit.player1, playerGameInit.player2, playerGameInit.gameId);
                shipsConfigured = true;
            }
        }

        public void AttackRound()
        {
            if (configurePlayerShips != null)
            {
                var data = AttackCycleController.AttackRound(playerGameInit.player1, playerGameInit.player2, configurePlayerShips.playerBoard1, configurePlayerShips.playerBoard2);

                configurePlayerShips.playerBoard1 = data.Item1;
                configurePlayerShips.playerBoard2 = data.Item2;

                if (data.Item3)
                {
                    //game end condition
                    GameOverWipe(configurePlayerShips.playerBoard1.GameID);
                }
            }
        }

        public void GameOverWipe(int gameId)
        {
            //update game in db
            GameAccess.UpdateGame(gameId, true);

            playersSelected=false;
            shipsConfigured=false;

            playerGameInit = null;
            configurePlayerShips=null;
        }
        public AppController() 
        {
            options = new Option[] {
                new Option($"Add Player details: 1 {(playersSelected ? "Disabled" : "")}", () => LockinPlayers()),
                new Option($"Configure Ships: 2 {(shipsConfigured || !playersSelected ? "Disabled" : "")}", () => ShipsConfigured()),
                new Option($"Launch Attack: 3 {(!shipsConfigured ? "Disabled" : "")}", () => AttackRound()),
                new Option("Quit: 4", () => {Console.WriteLine("Quitting"); Console.ReadKey(); Environment.Exit(0); })
            };

            do
            {
                MenuController.DisplayMenu(options, playersSelected, shipsConfigured);
            } while (true);
        }
    }
}
