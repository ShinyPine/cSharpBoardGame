using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedGameClasses {
    /// <summary>
    /// Models a game board consisting of different types of squares
    /// </summary>
    public class Board {

        public const int NUMBER_OF_SQUARES = 40;  // Value doesn't include start or finish squares.
        public const int START_SQUARE_NUMBER = 0;
        public const int FINISH_SQUARE_NUMBER = NUMBER_OF_SQUARES + 1;

        private Square[] squares = new Square[NUMBER_OF_SQUARES + 2];  // The array of squares.
        public Square[] Squares {
            get {
                return squares;
            }
        }

        public Square StartSquare {
            get {
                return squares[START_SQUARE_NUMBER];
            }
        }
     
        /// <summary>
        /// Parameterless Constructor
        /// Initialises a board consisting of a mix of Ordinary Squares,
        ///     Bad Investment Squares and Lottery Win Squares.
        /// The board has two 'non-board' squares:
        ///     a start square; and
        ///     a finish square.
        ///     This is to comply with the Hare and Tortoise requirements.
        /// The start square is to be used for initialisation, play is not yet on the board.
        /// The finish square is to be used for termination, players cannot move past this square.
        /// Pre:  none
        /// Post: board is constructed
        /// </summary>
        public Board() {
            for (int squareNumber = 1; squareNumber <= 40; squareNumber++)
            {
                if(squareNumber==5||squareNumber==15||squareNumber==25||squareNumber==35)
                {
                    this.squares[squareNumber] = new BadInvestmentSquare(this, squareNumber, "bad investment");
                } 
                else if (squareNumber==10||squareNumber==20||squareNumber==30||squareNumber==40)
                {
                    this.squares[squareNumber] = new LotteryWinSquare(this, squareNumber, "lottery win");
                }
                else {
                    this.squares[squareNumber] = new Square(this, squareNumber, "ordinary");
                }// end if
            }//end for

            //set the start square
            this.squares[START_SQUARE_NUMBER] = new Square(this, START_SQUARE_NUMBER, "Start");

            //set the finish square
            this.Squares[FINISH_SQUARE_NUMBER] = new Square(this, FINISH_SQUARE_NUMBER, "Finish");
        } // end Board
    } //end class Board
}