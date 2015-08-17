/*
 * This class will display the whole game, 
 * which is a the Hare and the Tortoise of board game
 * 
 * Author:  HAO PAN
 * Date:    2nd June, 2013
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

using SharedGameClasses;

namespace GuiGame {

    /// <summary>
    /// The form that displays the GUI version of the game.
    /// </summary>
    public partial class HareAndTortoiseForm : Form {

        // The GUI game uses the HareAndTortoiseGame, from the SharedGameClasses.
        HareAndTortoiseGame hareAndTortoiseGame = new HareAndTortoiseGame();

        // Specify the numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 6;

        // When we update what's on the screen, we show the movement of players 
        // by removing them from their old squares and adding them to their new squares.
        // This enum makes it clearer that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };
    
        /// <summary>
        /// Constructor with initialising parameters.
        /// Pre:  none.
        /// Post: the form is initialised, ready for the game to start.
        /// </summary>
        public HareAndTortoiseForm() {
            InitializeComponent();
            hareAndTortoiseGame.NumberOfPlayers = HareAndTortoiseGame.MAX_PLAYERS; // Max players, by default.
            hareAndTortoiseGame.InitialiseAllThePlayers();  
            SetupTheGui();
            ResetGame();
        }

        /// <summary>
        /// Set up the GUI when the game is first displayed on the screen.
        /// This method is almost complete. It should only be changed by adding one line:
        ///     to set the initial ComboBox selection to "6"; and
        ///     remove the comment from the line which adds the ListBox as a Trace Listener.
        /// These changes are covered in Part 2 of the assignment spec.
        /// Pre:  the form contains the controls needed for the game.
        /// Post: the game is ready for the user(s) to play.
        /// </summary>
        private void SetupTheGui() {
            CancelButton = exitButton;  // Allow the Esc key to close the form.
            ResizeGameBoard();
            SetupGameBoard();

            //set intitial ComboBos Seletion to 6 here
            
            SetupPlayersDataGridView();

            //remove this comment and the two end of the line comment symbol on the next line
            Trace.Listeners.Add(new ListBoxTraceListener(listBox1));
            comboBox1.SelectedIndex = comboBox1.FindString("6");//default show the number 6 in combobox.
        }// end SetupTheGui


        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGameBoard() {
            // Uncomment all the lines in this method, once you've added the boardTableLayoutPanel to your form.
            // Do not add any extra code.
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = boardTableLayoutPanel.Size.Height;
            int currentWidth = boardTableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            boardTableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);
        }

        /// <summary>
        /// Creates each SquareControl and adds it to the boardTableLayoutPanel that displays the board.
        /// Pre:  none.
        /// Post: the boardTableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetupGameBoard() {
            SquareControl squareControl;
            Square squareGui;
 
            //the row of 0,2,4,6
            for (int row = 0; row < boardTableLayoutPanel.RowCount; row+=2)
            {
                for (int column = 0; column < boardTableLayoutPanel.ColumnCount; column++)
                {
                    squareGui = hareAndTortoiseGame.Board.Squares[36 - row * 6 + column];
                    squareControl = new SquareControl(squareGui, hareAndTortoiseGame.Players);

                    // Add the PictureBox object to boardTableLayoutPanel.
                    boardTableLayoutPanel.Controls.Add(squareControl, column, row);                   
                }//end colume
            }//end row
  
            //the row of 1,3,5,7
            for (int row = 1; row < boardTableLayoutPanel.RowCount - 1; row += 2)
            {
                for (int column = 0; column < boardTableLayoutPanel.ColumnCount; column++)
                {

                    squareGui = hareAndTortoiseGame.Board.Squares[41 - row * 6 - column];
                    squareControl = new SquareControl(squareGui, hareAndTortoiseGame.Players);

                    // Add the PictureBox object to boardTableLayoutPanel.
                    boardTableLayoutPanel.Controls.Add(squareControl, column, row);
                } // end column
            } // end row  
        }

        /// <summary>
        /// Tells the players DataGridView to get its data from the hareAndTortoiseGame.Players BindingList.
        /// Pre:  players DataGridView exists on the form.
        ///       hareAndTortoiseGame.Players BindingList is not null.
        /// Post: players DataGridView displays the correct rows and columns.
        /// </summary>
        private void SetupPlayersDataGridView() {

            //get its data of correct rows and columns from the hareAndTortoiseGame.Players BindingList.
            playerBindingSource.DataSource = hareAndTortoiseGame.Players;
        }

        /// <summary>
        /// Resets the game, including putting all the players on the Start square.
        /// This requires updating what is displayed in the GUI, 
        /// as well as resetting the hareAndTortoiseGame object.
        /// This method is used by both the Reset button and 
        /// when a new value is chosen in the Number of Players ComboBox.
        /// Pre:  none.
        /// Post: the form displays the game in the same state as when the program first starts 
        ///       (except that any user names that the player has entered are not reset).
        /// </summary>
        private void ResetGame() {
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            hareAndTortoiseGame.InitialiseAllThePlayers();
            hareAndTortoiseGame.SetPlayersAtTheStart();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            listBox1.Items.Clear();
            button1.Enabled = true;
        }// end reset game



        /// <summary>
        /// implement the animation that shows each player moving one square at a time.
        /// 
        /// Pre:  players' location are match in board
        /// Post: each player moving one square at a time
        /// </summary>
        private void animationMovement()
        {
            //get original square number
            int[] originalSquare = new int[6];
            for (int number = 0; number < hareAndTortoiseGame.Players.Count; number++)
            {
                originalSquare[number] = hareAndTortoiseGame.Players.ElementAt(number).Location.Number;
                Console.WriteLine(originalSquare[number]);
            }
            hareAndTortoiseGame.displayGameInformation();
            button1.Enabled = false;

            //show players move to a new square
            for (int number = 0; number < hareAndTortoiseGame.Players.Count; number++)
            {
                for (int newNumber = originalSquare[number];
                    newNumber < hareAndTortoiseGame.Players.ElementAt(number).Location.Number; 
                    newNumber++)
                {
                    SquareControlAt(newNumber).ContainsPlayers[number] = false;
                    SquareControlAt(newNumber + 1).ContainsPlayers[number] = true;
                    
                    //refresh display
                    RefreshBoardTablePanelLayout();
                    Application.DoEvents();
                    Thread.Sleep(100);  // Sleep for 100 milliseconds.
                }// end for
            }// end for
            button1.Enabled = true;
        }// end animationMovement

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens (or "pieces") are removed from their old squares
        /// or added to their new squares. E.g. when all players are moved back to the Start.
        /// 
        /// For each of the players, this method is to use the GetSquareNumberOfPlayer method to find out 
        /// which square number the player is on currently, then use the SquareControlAt method
        /// to find the corresponding SquareControl, and then update that SquareControl so that it
        /// knows whether the player is on that square or not.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed by using one or more methods 
        /// in the hareAndTortoiseGame class. Otherwise, you won't see any change on the screen.
        /// 
        /// Because this method moves ALL players, it should NOT be used when animating a SINGLE player's
        /// movements from square to square.
        /// 
        /// Pre:  the Players objects in the hareAndTortoiseGame give the players' current locations.
        /// Post: the GUI board is updated to match the locations in the Players objects.
        /// </summary>
        /// <param name="typeOfGuiUpdate">Specifies whether all the players are being removed 
        /// from their old squares or added to their new squares</param>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate) {

            // remove players from their old squares.
            if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer)
            {
                for (int squareNumber = 0; squareNumber < hareAndTortoiseGame.Players.Count; squareNumber++)
                {
                    int playerSquare = GetSquareNumberOfPlayer(squareNumber);
                    SquareControlAt(playerSquare).ContainsPlayers[squareNumber] = false;
                }//end for
            }
                
            // add players to their new squares.
            else if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer)
            {
                for (int squareNumber = 0; squareNumber < hareAndTortoiseGame.NumberOfPlayers; squareNumber++)
                {
                    int playerSquare = GetSquareNumberOfPlayer(squareNumber);
                    SquareControlAt(playerSquare).ContainsPlayers[squareNumber] = true;
                }//end for
            }// end if
            RefreshBoardTablePanelLayout(); // Should be the last line in this method. Do not put inside a loop.
        }// end UpdatePlayersGuiLocations



        /*** START OF LOW-LEVEL METHODS ***
         * 
         *   The methods in this section are "helper" methods that can be called to do basic things. 
         *   That makes coding easier in other methods of this class.
         *   You should NOT CHANGE these methods, except where otherwise specified in the assignment. 
         *   ***/

        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout() {
            // Uncomment the following line once you've added the boardTableLayoutPanel to your form.
            boardTableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, money, etc.),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the hareAndTortoiseGame.
        /// </summary>
        private void RefreshPlayersInfoInDataGridView() {
            hareAndTortoiseGame.Players.ResetBindings();
        }

        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the playerNumber.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber) {
            Square playerSquare = hareAndTortoiseGame.Players[playerNumber].Location;
            return playerSquare.Number;
        }

        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the boardTableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNumber) {
            int rowNumber;
            int columnNumber;
            MapSquareNumToScreenRowAndColumn(squareNumber, out rowNumber, out columnNumber);

            // Uncomment the following line once you've added the boardTableLayoutPanel to your form.
            return (SquareControl) boardTableLayoutPanel.GetControlFromPosition(columnNumber, rowNumber);

            // Delete the following line once you've added the boardTableLayoutPanel to your form.
            
        }

        /// <summary>
        /// For a given square number, tells you the corresponding row and column numbers.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNumber, out int rowNumber, out int columnNumber) {

            // Add more code to this method and replace the next two lines by something more sensible.
            rowNumber = 0;      // Use 0 to make the compiler happy for now.
            columnNumber = 0;   // Use 0 to make the compiler happy for now.
            
            // map square in the row 0
            if (squareNumber >= 36 && squareNumber <= 41)
            {
                rowNumber = 0;
                columnNumber = (squareNumber - 36);
            }

            // map square in the row 1
            if (squareNumber >= 30 && squareNumber <= 35)
            {
                rowNumber = 1;
                columnNumber = (35 - squareNumber);
            }

            // map square in the row 2
            if (squareNumber >= 24 && squareNumber <= 29)
            {
                rowNumber = 2;
                columnNumber = (squareNumber - 24);
            }

            // map square in the row 3
            if (squareNumber >= 18 && squareNumber <= 23)
            {
                rowNumber = 3;
                columnNumber = (23 - squareNumber);
            }

            // map square in the row 4
            if (squareNumber >= 12 && squareNumber <= 17)
            {
                rowNumber = 4;
                columnNumber = (squareNumber - 12);
            }

            // map square in the row 5
            if (squareNumber >= 6 && squareNumber <= 11)
            {
                rowNumber = 5;
                columnNumber = (11 - squareNumber);
            }

            // map square in the row 6
            if (squareNumber <= 5)
            {
                rowNumber = 6;
                columnNumber = squareNumber;
            }
                
        }// end Map Square Number To Screen Row And Column

        /*** END OF LOW-LEVEL METHODS ***/

        /*** START OF EVENT-HANDLING METHODS ***/

        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is closed.
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e) {
            // Terminate immediately, rather than calling Close(), 
            // so that we don't have problems with any animations that are running at the same time.
            Environment.Exit(0);  
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Handle the Roll Dice button being clicked
        /// 
        /// pre: the Roll Dice button is clicked
        /// post:give the result of rolling dice, and players will move base on the result
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void button1_Click(object sender, EventArgs e)
        {
            animationMovement();

            //refresh the information shows in DataGridView
            RefreshPlayersInfoInDataGridView();

            //when game finish, roll dice button is not allow to be used
            if (hareAndTortoiseGame.Finished)
            {
                button1.Enabled = false;
            }
        }// end roll rice

        /// <summary>
        /// reset the game
        /// pre: the Reset button is clicked
        /// post:the game is reset
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void button2_Click(object sender, EventArgs e)
        {
            ResetGame();
        }


        /// <summary>
        /// select the number of player join the game,
        /// and also reset the game
        /// 
        /// pre: the combobox is used
        /// post:the number of player is selected
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hareAndTortoiseGame.NumberOfPlayers = int.Parse(comboBox1.Text);

            //also reset the game
            ResetGame();
        }


        /*** END OF EVENT-HANDLING METHODS ***/
    }
}

