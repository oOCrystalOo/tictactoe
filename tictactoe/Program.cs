using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class Program
    {
        public string[,] gameGrid;
        public Player player1;
        public Player player2;

        public static void Main(string[] args)
        {
            Program p = new Program();
            p.InitializePlayers();
            p.StartGame();
        }

        private void InitializePlayers ()
        {
            player1 = new Player("x", "Player 1");
            player2 = new Player("o", "Player 2");
        }

        private void StartGame()
        {
            gameGrid = new string[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    gameGrid[x, y] = "";
                }
            }

            Random rnd = new Random();
            int randomNum = rnd.Next(1, 100);
            if (randomNum <= 50)
            {
                //Player 1 starts first
                Console.WriteLine("{0} begins first.", player1.name);
                startTurn(player1);
            } else
            {
                //Player 1 starts first
                Console.WriteLine("{0} begins first.", player2.name);
                startTurn(player2);
            }
        }

        //Check if grid is filled
        private bool IsGridFilled ()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (gameGrid[x,y].Length == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void startTurn (Player player)
        {
            DrawGrid();
            int x = PromptInput(player, "x");
            int y = PromptInput(player, "y");
            inputCoords(x - 1, y - 1, player);
        }

        private int PromptInput (Player player, string type)
        {
            Console.WriteLine("{0}, please choose the {1} coordinate of your input (1/2/3).", player.name, type);
            string stringInput = Console.ReadLine();
            int input;
            bool tryParse = Int32.TryParse(stringInput, out input);
            if (tryParse)
            {
                if (input < 1 || input > 3)
                {
                    Console.WriteLine("Input is not valid. Please try again.");
                    return input = PromptInput(player, type);
                }
                else
                {
                    return input;
                }
            } else
            {
                return PromptInput(player, type);
            }
        }

        private void inputCoords (int x, int y, Player player)
        {
            if (gameGrid[x, y] != "")
            {
                Console.WriteLine("This space is filled. Please choose a different space.");
                startTurn(player);
            } else
            {
                gameGrid[x, y] = player.mark;
                DrawGrid();
                CheckGrid(player);
            }
        }

        private void DrawGrid ()
        {
            List<string> s = new List<string>();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    s.Add(gameGrid[x, y]);
                }
            }
            
            for (int f = 0; f < s.Count; f++)
            {
                if (s[f].Length == 0)
                {
                    s[f] = "_";
                }
            }
            Console.WriteLine("{0}|{1}|{2}", s[0], s[1], s[2]);
            Console.WriteLine("{0}|{1}|{2}", s[3], s[4], s[5]);
            Console.WriteLine("{0}|{1}|{2}", s[6], s[7], s[8]);
        }

        private void CheckGrid(Player player)
        {
            List<string> s = new List<string>();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    s.Add(gameGrid[x, y]);
                }
            }
            string m = player.mark;
            if (
                //Check horizontal
                s[0] == m && s[1] == m && s[2] == m ||
                s[3] == m && s[4] == m && s[5] == m ||
                s[6] == m && s[7] == m && s[8] == m ||
                //Check vertical
                s[0] == m && s[3] == m && s[6] == m ||
                s[1] == m && s[4] == m && s[7] == m ||
                s[2] == m && s[5] == m && s[8] == m ||
                //Check diagonal
                s[0] == m && s[4] == m && s[8] == m ||
                s[6] == m && s[4] == m && s[2] == m )
            {
                DeclareWinner(player);
                return;
            }
            //Check if Grid is filled
            bool gridFilled = IsGridFilled();
            if (IsGridFilled())
            {
                Console.WriteLine("Grid is filled. No Winner is determined.");
                PromptNewGame();
                return;
            }

            //No 3-in-a-row. Start next player's turn.
            if (player == player1)
            {
                Console.WriteLine("{0}'s turn.", player2.name);
                startTurn(player2);
            } else
            {
                Console.WriteLine("{0}'s turn.", player1.name);
                startTurn(player1);
            }
        }

        private void DeclareWinner (Player player)
        {
            Console.WriteLine("{0} wins!", player.name);
            PromptNewGame();
        }
        
        private void PromptNewGame()
        {
            Console.WriteLine("Start another game? Y/N");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                //Start another game. We can skip the player initialization part.
                StartGame();
            }
        }
    }
}
