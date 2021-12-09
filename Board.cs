using System;
using System.Collections.Generic;
using System.Text;

namespace C21_Ex02_Liron_315783852_Maor_315900795
{
    internal static class Board
    {
        public static void PrintMatrix(MatrixNode[,] i_Matrix)
        {
            StringBuilder sb = new StringBuilder();

            // Body
            for (int i = 0; i < i_Matrix.GetLength(1); ++i)
            {
                sb.Append("     ");
                sb.Append(i + 1);
            }

            // Body
            for (int i = 0; i <= i_Matrix.GetLength(0) - 1; ++i)
            {
                sb.AppendLine();
                sb.Append("  ");
                sb.Append(new string('=', i_Matrix.GetLength(1) * 6));
                sb.AppendLine();
                sb.Append("  ");

                for (int j = 0; j < i_Matrix.GetLength(1); ++j)
                {
                    sb.Append("|");

                    if (i_Matrix[i, j] != null)
                    {
                        sb.Append("  ");
                        sb.Append(i_Matrix[i, j].m_Player.m_PlayerSign);
                        sb.Append("  ");
                    }
                    else
                    {
                        sb.Append("     ");
                    }
                }

                sb.Append("|");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
