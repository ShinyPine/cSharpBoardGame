using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedGameClasses {

    /// <summary>
    /// Lottery Win Square rewards the player with $10 
    /// as well as advancing to the square following this square.
    /// </summary>
    public class LotteryWinSquare : Square {
        
        private const int AMOUNT_TO_BE_CREDITED = 10;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="number">number of the square</param>
        /// <param name="name">name of the square</param>
        public LotteryWinSquare(Board board, int number, string name)
            : base(board, number, name) {

            // nothing extra to be done here

        } //end LotteryWinSquare

        /// <summary>
        /// Implements the action when a player lands on this square.
        /// Credits the lottery win and advances a square.
        /// </summary>
        /// <param name="player">who landed on the square</param>
        public override void LandOn(Player player) {
            
            base.LandOn(player);  // leave in for possible future change

            player.Credit(AMOUNT_TO_BE_CREDITED);

          

        } //end LandOn
    }
}
