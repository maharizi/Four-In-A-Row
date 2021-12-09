using System;
using System.Collections.Generic;
using System.Text;
using Ex02.ConsoleUtils;

namespace C21_Ex02_Liron_315783852_Maor_315900795
{
    public class ConnectFour
    {
        public const int k_MinRowsAndColumnsSize = 4;

        public const int k_MaxRowsAndColumnsSize = 8;

        public const int k_NumOfConsecutiveTokensForWinning = 4;

        private static Dictionary<Player, int> s_PlayersAndWins = new Dictionary<Player, int>();

        private static int s_MatrixRows;

        private static int s_MatrixColumns;

        private static eGameOption s_GameOption;

        private static eCurrentGameStatus s_CurrentGameStatus;

        public static void HandleWrongInput()
        {
            string userInput = string.Empty;
            Console.WriteLine("Seems that your input is incorrect, do you want to play ?");
            Console.WriteLine("if so, click y, otherwise type anything else");
            userInput = Console.ReadLine();
            if (userInput == "y")
            {
                GetGameSettingsFromUser();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public static eGameOption GetPlayMode()
        {
            eGameOption currentGameMode = eGameOption.TwoPlayers;
            Player firstPlayer = null;
            string inputValue = string.Empty;
            string userInput = string.Empty;
            Console.WriteLine("If you want game with 2 players, type P");
            Console.WriteLine("if you want to play against the copmuter, type C");
            Console.WriteLine("please notice, if you will type anything else, play mode will be set to default - 2 players");
            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "P":

                    Console.WriteLine("Name of first player");
                    inputValue = Console.ReadLine();
                    firstPlayer = new Player((char)ePlayerSign.First, inputValue);
                    s_PlayersAndWins.Add(firstPlayer, 0);
                    Console.WriteLine("Name of second player");
                    inputValue = Console.ReadLine();
                    Player secondPlayer = new Player((char)ePlayerSign.Second, inputValue);
                    s_PlayersAndWins.Add(secondPlayer, 0);

                    break;
                case "C":
                    currentGameMode = eGameOption.OnePlayerWithTheComputer;
                    Console.WriteLine("Name of first player");
                    inputValue = Console.ReadLine();
                    firstPlayer = new Player((char)ePlayerSign.First, inputValue);
                    s_PlayersAndWins.Add(firstPlayer, 0);
                    Player computerPlayer = new Player((char)ePlayerSign.Second, "computer", true);
                    s_PlayersAndWins.Add(computerPlayer, 0);
                    break;
                default:
                    break;
            }

            return currentGameMode;
        }

        private static void handleEndOfCurrentRound(Player i_CurrentPlayer = null)
        {
            try
            {
                switch (s_CurrentGameStatus)
                {
                    case eCurrentGameStatus.Win:
                        Console.WriteLine(string.Format("the winner for this round is {0}", i_CurrentPlayer));
                        s_PlayersAndWins[i_CurrentPlayer]++;
                        break;
                    case eCurrentGameStatus.Teko:
                        foreach (var player in s_PlayersAndWins.Keys)
                        {
                            s_PlayersAndWins[player]++;
                        }

                        break;
                    case eCurrentGameStatus.Quit:
                        foreach (var player in s_PlayersAndWins.Keys)
                        {
                            if (!(i_CurrentPlayer == player))
                            {
                                s_PlayersAndWins[player]++;
                                break;
                            }
                        }

                        break;
                    default:
                        break;
                }

                Console.WriteLine();
                askForAnotherRound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Oops, seems there was an exception : {0}", ex));
            }
        }

        private static void printCurrentSummery()
        {
            foreach (var player in s_PlayersAndWins)
            {
                Console.WriteLine(string.Format("Winning Summery : {0} has {1} points", player.Key.m_PlayerName, player.Value));
            }
        }

        private static void askForAnotherRound()
        {
            printCurrentSummery();
            string inputValue = string.Empty;
            Console.WriteLine("if you want to start another game, enter y, enter anything else for no");
            inputValue = Console.ReadLine();
            switch (inputValue)
            {
                case "y":
                    Game();
                    break;
                default:
                    Console.WriteLine("Bye Bye :)");
                    Environment.Exit(0);
                    break;
            }
        }

        public static void GetGameSettingsFromUser()
        {
            bool isValidInput = false;
            string userInput = string.Empty;
            Console.WriteLine("Welcome to the best game everrr- Connect 4");
            Console.WriteLine("Please enter matrix size (allowed size is between 4-8");
            Console.WriteLine("Number of rows is :");
            userInput = Console.ReadLine();
            isValidInput = int.TryParse(userInput, out s_MatrixRows);
            if (!isValidInput || s_MatrixRows >= k_MaxRowsAndColumnsSize || s_MatrixRows <= k_MinRowsAndColumnsSize)
            {
                HandleWrongInput();
            }

            Console.WriteLine("Number of columns is :");
            userInput = Console.ReadLine();
            isValidInput = int.TryParse(userInput, out s_MatrixColumns);
            if (!isValidInput || s_MatrixRows >= k_MaxRowsAndColumnsSize || s_MatrixRows <= k_MinRowsAndColumnsSize)
            {
                HandleWrongInput();
            }

            s_GameOption = GetPlayMode();
            Game();
        }

        public static void GetDesiredColumnFromCurrentPlayer(eGameOption i_s_GameOption, Matrix i_Matrix, Player i_CurrentPlayer, out int io_CurrentDesiredColumn)
        {
            io_CurrentDesiredColumn = -1;

            try
            {
                string userInput = string.Empty;
                bool validInputNumber;

                Console.WriteLine(string.Format("current player turn : {0}", i_CurrentPlayer.m_PlayerName));
                Board.PrintMatrix(i_Matrix.m_GameMatrix);
                if (i_s_GameOption == eGameOption.OnePlayerWithTheComputer && i_CurrentPlayer.m_IsComputerPlayer)
                {
                    Console.WriteLine("Computer turn - current desired column will be choose automatically ");
                    System.Threading.Thread.Sleep(7000);
                    i_CurrentPlayer.ComputerPlayerTurn(i_Matrix.m_ColumnsCapacity, i_Matrix.m_Columns, out io_CurrentDesiredColumn);
                }
                else
                {
                    Console.WriteLine("Player - please enter which column you want to insert the token ");
                    Console.WriteLine("of you want to quit the game , enter Q");
                    userInput = Console.ReadLine();
                    validInputNumber = int.TryParse(userInput, out io_CurrentDesiredColumn);

                    if (!(userInput == "Q") && !validInputNumber)
                    {
                        Console.WriteLine("Wrong input");
                        Environment.Exit(0);
                    }

                    if (userInput == "Q")
                    {
                        s_CurrentGameStatus = eCurrentGameStatus.Quit;
                        handleEndOfCurrentRound(i_CurrentPlayer);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("The board is filled and the game is a draw...Sorry no winner.");
            }
        }

        public static void Game()
        {
            int totalMoves = s_MatrixRows * s_MatrixColumns;
            int currentDesiredColumn;
            Player currentPlayer;
            MatrixNode currentmatrixNode;
            Matrix matrix = new Matrix(s_MatrixRows, s_MatrixColumns, k_NumOfConsecutiveTokensForWinning);
            bool currentRoundWin;
            bool is_inserted;

            for (int i = 1; i <= totalMoves; i++)
            {
                Screen.Clear();
                currentPlayer = Player.GetCurrentPlayer(i, s_PlayersAndWins);
                GetDesiredColumnFromCurrentPlayer(s_GameOption, matrix, currentPlayer, out currentDesiredColumn);
                is_inserted = matrix.AddNewNode(currentPlayer, currentDesiredColumn - 1, out currentmatrixNode);

                if (!is_inserted)
                {
                    Console.WriteLine("the column you chose is full , you have another turn");
                    System.Threading.Thread.Sleep(7000);

                    // so the same player will get another turn
                    i--;
                    continue;
                }

                // new matrix node was created
                else
                {
                    Screen.Clear();
                    Board.PrintMatrix(matrix.m_GameMatrix);
                    currentRoundWin = matrix.SearchForWin(currentPlayer, currentmatrixNode);
                    if (currentRoundWin)
                    {
                        s_CurrentGameStatus = eCurrentGameStatus.Win;
                        handleEndOfCurrentRound(currentPlayer);
                        Console.WriteLine(string.Format("{0} is the winner for this game!!!!, congrats", currentPlayer.m_PlayerName));
                        break;
                    }
                }

                if (i == totalMoves)
                {
                    s_CurrentGameStatus = eCurrentGameStatus.Teko;
                    handleEndOfCurrentRound();
                    Console.WriteLine("The board is filled and the game is a draw...Sorry no winner.");
                    break;
                }
            }
        }
    }

    public enum eGameOption
    {
        TwoPlayers,
        OnePlayerWithTheComputer,
    }

    public enum eCurrentGameStatus
    {
        Win,
        Teko,
        Quit,
    }
}
