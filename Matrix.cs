using System;
using System.Collections.Generic;
using System.Text;

namespace C21_Ex02_Liron_315783852_Maor_315900795
{
    public class Matrix
    {
        internal int m_Rows { get; set; }

        internal int m_Columns { get; set; }

        internal int[] m_ColumnsCapacity;

        internal int m_NumOfConsecutiveForWinning { get; set; }

        public MatrixNode[,] m_GameMatrix;

        public Matrix(int i_InputRows, int i_InputColumns, int i_NumOfConsecutiveForWinning)
        {
            m_Rows = i_InputRows;
            m_Columns = i_InputColumns;
            m_ColumnsCapacity = new int[i_InputColumns];
            m_GameMatrix = new MatrixNode[i_InputRows, i_InputColumns];
            m_NumOfConsecutiveForWinning = i_NumOfConsecutiveForWinning;
        }

        public int GetNextFreeNodeInColumn(int i_Column)
        {
            if (m_GameMatrix != null)
            {
                return m_ColumnsCapacity[i_Column];
            }
            else
            {
                return -1;
            }
        }

        public bool AddNewNode(Player i_CurrentPlayer, int i_Column, out MatrixNode io_MatrixNode)
        {
            io_MatrixNode = null;
            try
            {
                int nextFreeNodeInColumn = GetNextFreeNodeInColumn(i_Column);
                if (nextFreeNodeInColumn != -1)
                {
                    io_MatrixNode = new MatrixNode(i_CurrentPlayer, m_Rows - nextFreeNodeInColumn - 1, i_Column);
                    m_GameMatrix[m_Rows - nextFreeNodeInColumn - 1, i_Column] = io_MatrixNode;
                    m_ColumnsCapacity[i_Column]++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Oops, seems there was an exception : {0}", ex));
                return false;
            }
        }

        public bool SearchForWin(Player i_CurrentPlayer, MatrixNode i_CurrentMove)
        {
            try
            {
                if (MatrixUtils.FindHorizontalWin(this, i_CurrentMove) || MatrixUtils.FindVerticalWin(this, i_CurrentMove)
                    || MatrixUtils.FindUpDiagonalWin(this, i_CurrentMove) || MatrixUtils.FindUpDiagonalWin(this, i_CurrentMove))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Oops, seems there was an exception : {0}", ex));
                return false;
            }
        }
    }
}
