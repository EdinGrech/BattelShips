﻿using static DataAccess.Structures.StructHelpers;
using static FrontEnd.FrontEndController;
using static FrontEnd.BoardGridEngin;
using DataAccess.Structures.DBStructures;
using static DataAccess.Structures.Positioners;

namespace LogicController
{
    internal class ConfigurePlayerShips
    {
        internal BoardData playerBoard1 {  get; set; }
        internal BoardData playerBoard2 { get; set; }

        public void CreatePlayerBoards(Player player1, Player player2, int gameId)
        {
            Console.WriteLine("Player Ship Configuration");
            Grid grid = new();
            int localHitPoitns = 0;
            playerBoard1 = new( player1.id, player1.username, gameId); //board id, player id, game id
            playerBoard2 = new( player2.id, player2.username, gameId);

            List<(ShipInfo, int)> shipData = PlaceShips(playerBoard1, grid);
            foreach ((ShipInfo ship, int shipId) in shipData)
            {
                localHitPoitns += ship.Size;
                SaveTranslationController.ShipOnBoardPlacementSaver(ship, playerBoard1, shipId);
            }
            playerBoard1.hitPoints = localHitPoitns;

            localHitPoitns = 0;
            List<(ShipInfo, int)> shipData1 = PlaceShips(playerBoard2, grid);
            foreach ((ShipInfo ship, int shipId) in shipData1)
            {
                localHitPoitns += ship.Size;
                SaveTranslationController.ShipOnBoardPlacementSaver(ship, playerBoard2, shipId);
            }
            playerBoard2.hitPoints = localHitPoitns;

            Console.WriteLine("Ship Configuration complete");
        }

        public ConfigurePlayerShips(Player player1, Player player2, int gameId)
        {
            CreatePlayerBoards(player1, player2, gameId);
        }
    }
}