using DataAccess.Structures.DBStructures;
using static DataAccess.Structures.StructHelpers;
using static FrontEnd.BoardGridEngin;
using static FrontEnd.FrontEndController;

namespace LogicController
{
    internal class AttackCycleController
    {
        Grid grid1 = new();
        Grid grid2 = new();
        bool gameEnd = false;

        public Tuple<
            BoardData,
            BoardData,
            bool> AttackRound(Player player1, Player player2, BoardData playerBoard1, BoardData playerBoard2)
        {
            DataAccess.Structures.StructHelpers.Attack attack1;
            DataAccess.Structures.StructHelpers.Attack attack2;

            (playerBoard2, attack1) = AttackShip(playerBoard2, player1, grid1);
            (playerBoard1, attack2) = AttackShip(playerBoard1, player2, grid2);

            SaveTranslationController.Attack2BoardSaver(attack1, playerBoard2);
            SaveTranslationController.Attack2BoardSaver(attack2, playerBoard1);

            if (playerBoard1.hitPoints <= 0)
            {
                Console.WriteLine($"Player {player2.username} Has won!");
                Console.ReadKey();
                gameEnd = true;
            }
            if (playerBoard2.hitPoints <= 0)
            {
                Console.WriteLine($"Player {player1.username} Has won!");
                Console.ReadKey();
                gameEnd = true;
            }

            return Tuple.Create(playerBoard1, playerBoard2, gameEnd);
        }
    }
}
