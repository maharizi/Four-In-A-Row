using System;
using System.Collections.Generic;
using System.Text;

namespace C21_Ex02_Liron_315783852_Maor_315900795
{
    public class Player
    {
        public static int s_Winnings { get; internal set; }

        public char m_PlayerSign { get; set; }

        public string m_PlayerName { get; set; }

        public bool m_IsComputerPlayer { get; set; }

        public Player(char sign, string i_PlayerName, bool i_isComputerPlayer = false)
        {
            this.m_PlayerSign = sign;
            m_PlayerName = i_PlayerName;
            s_Winnings = 0;
            m_IsComputerPlayer = i_isComputerPlayer;
        }

        public static Player GetCurrentPlayer(int i_MoveCount, Dictionary<Player, int> i_Players)
        {
            Player currentPlayer = null;

            try
            {
                if (i_MoveCount % 2 == 0)
                {
                    foreach (var player in i_Players)
                    {
                        if (player.Key.m_PlayerSign == (char)ePlayerSign.Second)
                        {
                            currentPlayer = player.Key;
                        }
                    }
                }
                else
                {
                    foreach (var player in i_Players)
                    {
                        if (player.Key.m_PlayerSign == (char)ePlayerSign.First)
                        {
                            currentPlayer = player.Key;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Oops, seems there was an exception : {0}", ex));
            }

            return currentPlayer;
        }

        public void ComputerPlayerTurn(int[] i_CcolumnsCapacity, int i_MaxCapacity, out int io_ChosenColumn)
        {
            io_ChosenColumn = 0;
            try
            {
                for (int i = 0; i < i_CcolumnsCapacity.Length; i++)
                {
                    if (i_CcolumnsCapacity[i] < i_MaxCapacity - 1)
                    {
                        io_ChosenColumn = i + 1;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Oops, seems there was an exception : {0}", ex));
            }
        }
    }

    public enum ePlayerSign
    {
        First = 'X',
        Second = 'O',
    }
}
