using System;
using System.Collections.Generic;
using System.Text;

namespace C21_Ex02_Liron_315783852_Maor_315900795
{
  public static class MatrixUtils
    {
        public static bool FindHorizontalWin(Matrix i_Matrix, MatrixNode i_CurrentMove)
        {
            int count = 0;
            int leftBound = Math.Max(0, i_CurrentMove.m_Column - 3);
            int rightBound = Math.Min(i_CurrentMove.m_Column + 3, i_Matrix.m_Columns - 1);
            bool winFound = false;

            for (int i = leftBound; i <= rightBound; i++)
            {
                if (i_Matrix.m_GameMatrix[i_CurrentMove.m_Row, i] != null && i_Matrix.m_GameMatrix[i_CurrentMove.m_Row, i].m_Player.m_PlayerSign == i_CurrentMove.m_Player.m_PlayerSign)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                winFound = count == i_Matrix.m_NumOfConsecutiveForWinning;
                if (winFound)
                {
                    break;
                }
            }

            return winFound;
        }

        public static bool FindVerticalWin(Matrix i_Matrix, MatrixNode i_CurrentMove)
        {
            int count = 0;
            int upperBound = Math.Min(i_CurrentMove.m_Row + 3, i_Matrix.m_Rows - 1);
            int lowerBound = i_CurrentMove.m_Row;
            bool winFound = false;

            for (int i = lowerBound; i <= upperBound; i++)
            {
                if (i_Matrix.m_GameMatrix[i, i_CurrentMove.m_Column] != null &&
                    i_Matrix.m_GameMatrix[i, i_CurrentMove.m_Column].m_Player.m_PlayerSign == i_CurrentMove.m_Player.m_PlayerSign)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                winFound = count == i_Matrix.m_NumOfConsecutiveForWinning;
                if (winFound)
                {
                    break;
                }
            }

            return winFound;
        }

        public static bool FindUpDiagonalWin(Matrix i_Matrix, MatrixNode i_CurrentMove)
        {
            int count = 0;
            int leftLowerRowIndex, leftLowerColumnIndex;
            int rightUpperRowIndex, rightUpperColumnIndex;
            bool winFound = false;
            bool noNeedToSearch = false;

            LeftLowerCoordinateForUpDiagonal(i_Matrix, i_CurrentMove, out leftLowerRowIndex, out leftLowerColumnIndex);

            RightUpperCoordinateForUpDiagonal(i_Matrix, i_CurrentMove, out rightUpperRowIndex, out rightUpperColumnIndex);

            noNeedToSearch = rightUpperColumnIndex - leftLowerColumnIndex < 3;
            if (noNeedToSearch)
            {
                return winFound;
            }

            for (int i = leftLowerRowIndex, j = leftLowerColumnIndex; i >= rightUpperRowIndex && j <= rightUpperColumnIndex; i--, j++)
            {
                if (i_Matrix.m_GameMatrix[i, j] != null && i_Matrix.m_GameMatrix[i, j].m_Player.m_PlayerSign == i_CurrentMove.m_Player.m_PlayerSign)
                {
                    count++;

                    if (count == i_Matrix.m_NumOfConsecutiveForWinning)
                    {
                        winFound = true;
                        break;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            return winFound;
        }

        public static bool FindDownDiagonalWin(Matrix i_Matrix, MatrixNode i_CurrentMove)
        {
            bool winFound = false;
            bool thereIsnoNeedToSearch = false;
            int leftUpperRowIndex, leftUpperColumnIndex;
            int rightLowerRowIndex, rightLowerColumnIndex;
            int count = 0;

            LeftUpperCoordinateForDownDiagonal(i_Matrix, i_CurrentMove, out leftUpperRowIndex, out leftUpperColumnIndex);

            RightLowerCoordinateForDownDiagonal(i_Matrix, i_CurrentMove, out rightLowerRowIndex, out rightLowerColumnIndex);

            thereIsnoNeedToSearch = rightLowerRowIndex - leftUpperRowIndex < 3;
            if (thereIsnoNeedToSearch)
            {
                return winFound;
            }

            for (int i = leftUpperRowIndex, j = leftUpperColumnIndex; i <= rightLowerRowIndex && j <= rightLowerColumnIndex; i++, j++)
            {
                if (i_Matrix.m_GameMatrix[i, j] != null && i_Matrix.m_GameMatrix[i, j].m_Player.m_PlayerSign == i_CurrentMove.m_Player.m_PlayerSign)
                {
                    count++;

                    winFound = count == i_Matrix.m_NumOfConsecutiveForWinning;
                    if (winFound)
                    {
                        break;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            return winFound;
        }

        private static void RightLowerCoordinateForDownDiagonal(Matrix i_Matrix, MatrixNode i_CurrentMove, out int io_RightLowerRowIndex, out int io_RightLowerColumnIndex)
        {
            io_RightLowerRowIndex = i_CurrentMove.m_Row;
            io_RightLowerColumnIndex = i_CurrentMove.m_Column;

            int rowUpperBound = i_Matrix.m_Rows - 1;
            int columnUpperBound = i_Matrix.m_Columns - 1;

            if (i_CurrentMove.m_Row == rowUpperBound || i_CurrentMove.m_Column == columnUpperBound)
            {
                return;
            }

            for (int i = 1; i < 4; i++)
            {
                if (i_CurrentMove.m_Row == rowUpperBound - i || i_CurrentMove.m_Column == columnUpperBound - i)
                {
                    io_RightLowerRowIndex = i_CurrentMove.m_Row + i;
                    io_RightLowerColumnIndex = i_CurrentMove.m_Column + i;
                }
            }
        }

        private static void LeftUpperCoordinateForDownDiagonal(Matrix i_Matrix, MatrixNode i_CurrentMove, out int io_LeftUpperRowIndex, out int io_LeftUpperColumnIndex)
        {
            io_LeftUpperRowIndex = i_CurrentMove.m_Row;
            io_LeftUpperColumnIndex = i_CurrentMove.m_Column;

            if (i_CurrentMove.m_Row == 0 || i_CurrentMove.m_Column == 0)
            {
                return;
            }

            for (int i = 1; i < 4; i++)
            {
                if (i_CurrentMove.m_Row == i || i_CurrentMove.m_Column == i)
                {
                    io_LeftUpperRowIndex = i_CurrentMove.m_Row - i;
                    io_LeftUpperColumnIndex = i_CurrentMove.m_Column - i;
                    break;
                }
            }
        }

        private static void RightUpperCoordinateForUpDiagonal(Matrix i_Matrix, MatrixNode i_CurrentMove, out int io_RightUpperRowIndex, out int io_RightUpperColumnIndex)
        {
            io_RightUpperRowIndex = i_CurrentMove.m_Row;
            io_RightUpperColumnIndex = i_CurrentMove.m_Column;

            int columnUpperBound = i_Matrix.m_Columns - 1;

            if (i_CurrentMove.m_Row == 0 || i_CurrentMove.m_Column == columnUpperBound)
            {
                return;
            }

            for (int i = 1; i < 4; i++)
            {
                if (i_CurrentMove.m_Row == i || i_CurrentMove.m_Column == i)
                {
                    io_RightUpperRowIndex = i_CurrentMove.m_Row - i;
                    io_RightUpperColumnIndex = i_CurrentMove.m_Column + i;
                    break;
                }
            }
        }

        private static void LeftLowerCoordinateForUpDiagonal(Matrix i_Matrix, MatrixNode i_CurrentMove, out int io_LeftLowerRowIndex, out int io_LeftLowerColumnIndex)
        {
            io_LeftLowerRowIndex = i_CurrentMove.m_Row;
            io_LeftLowerColumnIndex = i_CurrentMove.m_Column;
            int rowUpperBound = i_Matrix.m_Rows - 1;

            if (i_CurrentMove.m_Row == rowUpperBound || i_CurrentMove.m_Column == 0)
            {
                return;
            }

            for (int i = 1; i < 3; i++)
            {
                if (i_CurrentMove.m_Row == rowUpperBound - i || i_CurrentMove.m_Column == i)
                {
                    io_LeftLowerRowIndex = i_CurrentMove.m_Row + i;
                    io_LeftLowerColumnIndex = i_CurrentMove.m_Column - i;
                    break;
                }
            }

            if (i_CurrentMove.m_Row <= rowUpperBound - 3 || i_CurrentMove.m_Column >= 3)
            {
                io_LeftLowerRowIndex = i_CurrentMove.m_Row + 3;
                io_LeftLowerColumnIndex = i_CurrentMove.m_Column - 3;
            }
        }
    }
}
