using System;
using System.Collections.Generic;
using System.Text;

namespace IziHardGames.IziLiquiMap.Contracts.Enums
{
    [Flags]
    public enum ESpreadingType
    {
        None,
        /// <summary>
        /// Если движение вперед, то только северная ячейка
        /// </summary>
        Forward,
        /// <summary>
        /// Если движение вперед то WN, N, NE ячейки
        /// </summary>
        Cone,
        /// <summary>
        /// Если движение вперед то W, WN, N, NE, E ячейки
        /// </summary>
        Hemisphere,
        /// <summary>
        /// Только по основным 
        /// </summary>
        Crest,
        /// <summary>
        /// Во все стороны
        /// </summary>
        Radial,
    }
}
