using DataAccess.Structures.DBStructures;

namespace LogicController
{
    public class PlayerGameInit
    {
        internal int gameId {  get; set; }
        internal Game game { get; set; }
        internal Player player1 { get; set; }
        internal Player player2 { get; set; }

        private string HidePassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        private Player PlayerRegister2Game()
        {
            Player player;
            Console.WriteLine("Enter Username:");
            string username = Console.ReadLine();

            if (PlayerAccess.DoesPlayerExist(username))
            {
                Console.WriteLine("Enter Password:");
                string password = HidePassword();

                while (!PlayerAccess.IsPasswordMatch(username, password))
                {
                    Console.WriteLine("Incorrect Password. Please try again:");
                    password = HidePassword();
                }

                Console.WriteLine("Login Successful!");
                player = PlayerAccess.getPlayerByUsername(username)!;  
            }
            else
            {
                Console.WriteLine("New User detected enter a Password:");
                string password = HidePassword();

                Player tempPlayer = new Player
                {
                    username = username,
                    password = password
                };

                PlayerAccess.AddNewPlayer(tempPlayer);
                Console.WriteLine($"New Player {username} cerated");
                player = PlayerAccess.getPlayerByUsername(username)!;
            }
            player.password = "";
            return player;
        }

        public void InitGame()
        { 
            player1 = PlayerRegister2Game();
            player2 = PlayerRegister2Game();

            game = new Game
            {
                creatorFK = player1.id,
                opponentFK = player2.id,
                complete = false,

                title = $"player {player1.username} VS player {player2.username}"
            };

            gameId = GameAccess.AddGame(game);
        }

        public PlayerGameInit()
        {
            InitGame();
        }
    }
}
