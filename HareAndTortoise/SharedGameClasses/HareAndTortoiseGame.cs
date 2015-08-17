/*
 * This class puts the whole game together. 
 * It contains the game board, two dice, and the players.
 * 
 * Author:  HAO PAN
 * Date:    2nd June, 2013
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

using System.ComponentModel;  // for BindingList.

namespace SharedGameClasses {
    /// <summary>
    /// Plays a game called Hare and the Tortoise
    /// </summary>
    public class HareAndTortoiseGame {

        private Board board;
        public Board Board {
            get {
                return board;
            }
        }

        private Die die1, die2;

        // A BindingList is like an array that can grow and shrink. 
        // 
        // Using a BindingList will make it easier to implement the GUI with a DataGridView
        private BindingList<Player> players = new BindingList<Player>();
        public BindingList<Player> Players {
            get {
                return players;
            }
        }

        // Minimum and maximum players.
        private const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;

        private int numberOfPlayers = 2;  // The value 2 is purely to avoid compiler errors.

        public int NumberOfPlayers {
            get {
                return numberOfPlayers;
            }
            set {
                numberOfPlayers = value;
            }
        }

        // Is the current game finished?
        private bool finished = false;
        public bool Finished {
            get {
                return finished;
            }
        }

        // Some player names.  In the Console game, these are purely for testing purposes.
        // These values are intended to be read-only.  I.e. the program code should never update this array.
        private string[] defaultNames = { "One", "Two", "Three", "Four", "Five", "Six" };

        // Some colours for the players' tokens (or "pieces"). 
        // These are not used in the Console version of the game, but will be important in the GUI version.
        Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Black, Brushes.Red, 
                                                              Brushes.Gold, Brushes.GreenYellow, 
                                                              Brushes.Fuchsia, Brushes.White };

        /// <summary>
        /// Parameterless Constructor.
        /// Initialise the board, and the two dice.
        /// Pre:  none
        /// Post: the board and the objects it conatins are initialised.
        /// </summary>
        public HareAndTortoiseGame() {
            board = new Board();
            die1 = new Die();
            die2 = new Die();
        } //end Game

        
        /// <summary>
        /// Resets the game, including putting all the players on the Start square.
        /// Pre:  none.
        /// Post: the game is reset as though it is being played for the first time.
    
        /// </summary>
        public void SetPlayersAtTheStart() {
            foreach (Player player in players)
            {
                //set all the players back to start square
                player.Location = board.StartSquare;

                //all of the players have $100 again 
                player.Money = 100;

                //game start again
                finished = false;
            }
        } // end SetPlayersAtTheStart

        /// <summary>
        /// Initialises each of the players and adds them to the players BindingList.
        /// This method is to be called only once, when the game first starts.
        /// 
        /// Pre:  none.
        /// Post: the game's players are initialised.
        /// </summary>
        public void InitialiseAllThePlayers() {

            //clear all the players
            Players.Clear();
            for (int playerNumber = 0; playerNumber < NumberOfPlayers; playerNumber++)
            {

                //change all the player's name back to the default name, and back to start square
                Player player = new Player(defaultNames[playerNumber], board.StartSquare);
                player.PlayerTokenColour = playerTokenColours[playerNumber];
                Players.Add(player);
            }
        } // end InitialiseAllThePlayers

        //################## Game Play Methods above this line

        /// <summary>
        /// This method demonstrate how the WriteLine of ListBoxTraceListener can be used
        ///  to output to the ListBox for debugging purposes.
        ///  
        /// Outputs a player's current location and amount of money
        ///  to 
        /// pre:  player's object to display
        /// post: displayed the player's location and amount
        /// </summary>
        /// <param name="who">the player to display</param>
        public static void OutputIndividualDetails(Player who) {
            Trace.WriteLine(String.Format("Player {0} is on square {1} with {2:c}",
                             who.Name, who.Location.Number, who.Money));          
        } //end OutputIndividualDetails

            
        /// <summary>
        /// This method demonstrate how the WriteLine of ListBox1 show 
        /// individual player's location and money
        /// 
        /// pre: none
        /// post:display the game information in listbox
        /// </summary>
        public void displayGameInformation()
        {
            //during the game, show the information below
            Trace.WriteLine("-----------Around Start-----------");
            foreach (Player player in players)
            {
                player.Play(die1, die2);
                OutputIndividualDetails(player);
                if (player.Location.IsFinish())
                {
                    finished = true;
                }//end if
            }//end foreach
            Trace.WriteLine("-----------Around Over-----------");
            
            //When game over, show the information below
            if(finished)
            {
                Trace.WriteLine("=====GAME OVER=====\n");
                displayWinner();
            }//end if
         }//end displayGameInformation


        /// <summary>
        /// This method display the winner and the most money winner has.
        /// 
        /// pre: winner is decided
        /// post:show the winner
        /// </summary>
        public void displayWinner()
        {
            int theMostMoney = 0;
            foreach (Player player in players)
            {
                if (player.Money > theMostMoney)
                {
                    theMostMoney = player.Money;
                }//end if
            }//end foreach

            Trace.WriteLine(" The WINNER is: \n");
            foreach (Player player in players)
            {
                if (player.Money == theMostMoney)
                {
                    Trace.WriteLine(String.Format(player.Name + " has {0:c}", player.Money));
                    player.Winner = true;
                }//end if
            } //end foreach  
        } //end displayWinner       
    } //end class HareAndTortoiseGame
}
