using System;
using System.Collections.Generic;
using System.Text;

namespace C21_Ex02_Liron_315783852_Maor_315900795
{
    public class MatrixNode
    {
        internal Player m_Player { get; set; }

        internal int m_Column { get; set; }

        internal int m_Row { get; set; }

        public MatrixNode(Player i_Player, int i_Row, int i_Column)
        {
            m_Player = i_Player;
            m_Row = i_Row;
            m_Column = i_Column;
        }
    }
}
