using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedGameClasses{

    /// <summary>
    /// Bad Investment Square causes the player to lose at least $25
    /// </summary>
    public class BadInvestmentSquare : Square {

        private const int AMOUNT_TO_BE_DEBITED = 25;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="number">number of the square</param>
        /// <param name="name">name of the square</param>
        public BadInvestmentSquare(Board board, int number, string name)
            : base(board, number, name) {

            // nothing extra to be done here

        } //end BadInvestmentSquare

        /// <summary>
        /// Implements the action when a player lands on this square.
        /// Debits the bad investment.
        /// </summary>
        /// <param name="player">who landed on the square</param>
        public override void LandOn(Player player) {
            
            base.LandOn(player);  // keep in case of future change 

            player.Debit(AMOUNT_TO_BE_DEBITED);
        } //end LandOn
    }
}
