using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TTTForm : Form
    {
        public TTTForm()
        {
            InitializeComponent();
        }

        const string USER_SYMBOL = "X";
        const string COMPUTER_SYMBOL = "O";
        const string EMPTY = "";

        const int SIZE = 5;

        // constants for the 2 diagonals
        const int TOP_LEFT_TO_BOTTOM_RIGHT = 1;
        const int TOP_RIGHT_TO_BOTTOM_LEFT = 2;

        // constants for IsWinner
        const int NONE = -1;
        const int ROW = 1;
        const int COLUMN = 2;
        const int DIAGONAL = 3;

        

        //int userWins;
        //int cpuWins;

        // This method takes a row and column as parameters and 
        // returns a reference to a label on the form in that position
        private Label GetSquare(int row, int column)
        {
            int labelNumber = row * SIZE + column + 1;
            return (Label)(this.Controls["label" + labelNumber.ToString()]);
        }

        // This method does the "reverse" process of GetSquare
        // It takes a label on the form as it's parameter and
        // returns the row and column of that square as output parameters
        private void GetRowAndColumn(Label l, out int row, out int column)
        {
            int position = int.Parse(l.Name.Substring(5));
            row = (position - 1) / SIZE;
            column = (position - 1) % SIZE;
        }

        // This method takes a row (in the range of 0 - 4) and returns true if 
        // the row on the form contains 5 Xs or 5 Os.
        // Use it as a model for writing IsColumnWinner
        private bool IsRowWinner(int row)
        {
            Label square = GetSquare(row, 0);
            string symbol = square.Text;
            for (int col = 1; col < SIZE; col++)
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                    return false;
            }
            return true;
        }

        //* TODO:  finish all of these that return true

        //check to see if any Row is a winner
        private bool IsAnyRowWinner()
        {
            //bool winner = false;

            for (int row = 0; row < SIZE; row++)
            {
                if (IsRowWinner(row))
                    return true;
                    //winner = true;
            }
            return false;
            //return winner;

            
        }
        //Check to see if a single Column is a winner
        private bool IsColumnWinner(int col)
        {
            Label square = GetSquare(0, col);
            string symbol = square.Text;

            for (int row = 1; row < SIZE; row++)
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                {
                    return false;
                }
            }
            return true;


        }

        //check to see if any column is a winner
        private bool IsAnyColumnWinner()
        {
            //bool winner = false;
            
            for (int col = 0; col < SIZE; col++)
            {
                if (IsColumnWinner(col))
                    return true;
                    //winner = true;
            }
            //return winner;
            return false;
        }


        private bool IsDiagonal1Winner()
        {
            //set first square to position 0,0
            Label square = GetSquare(0, 0);
            string symbol = square.Text;
            
            // set loop to count from 1,1 to 4,4
            for (int row = 1, col = 1; row < SIZE; row++, col++ )
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                {
                    return false;
                }
            }
            return true;

        }
        //check to see if bottom-left to upper-right diagonal is a winner
        private bool IsDiagonal2Winner()
        {
            Label square = GetSquare(0, (SIZE - 1));
            string symbol = square.Text;
            for (int row = 1, col = SIZE - 2; row < SIZE; row++, col--)
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                    return false;
            }
            return true;
        }

        //check to see if any diagnal is a winner
        private bool IsAnyDiagonalWinner()
        {
            if (IsDiagonal1Winner() == false)
                if (IsDiagonal2Winner() == false)
                    return false;

            return true;
        }


        //check to see if the board is full
        private bool IsFull()
        {
            for (int square = 1; square < 26; square++)
            {
                Label templbl = (Label)(this.Controls["label" + square.ToString()]);
                if (templbl.Text == EMPTY)
                    return false;
                
            }

            return true;
        }

        // This method determines if any row, column or diagonal on the board is a winner.
        // It returns true or false and the output parameters will contain appropriate values
        // when the method returns true.  See constant definitions at top of form.
        private bool IsWinner(out int whichDimension, out int whichOne)
        {
            // rows
            for (int row = 0; row < SIZE; row++)
            {
                if (IsRowWinner(row))
                {
                    whichDimension = ROW;
                    whichOne = row;
                    return true;
                }
            }
            // columns
            for (int column = 0; column < SIZE; column++)
            {
                if (IsColumnWinner(column))
                {
                    whichDimension = COLUMN;
                    whichOne = column;
                    return true;
                }
            }
            // diagonals
            if (IsDiagonal1Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_LEFT_TO_BOTTOM_RIGHT;
                return true;
            }
            if (IsDiagonal2Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_RIGHT_TO_BOTTOM_LEFT;
                return true;
            }
            whichDimension = NONE;
            whichOne = NONE;
            return false;
        }

        // I wrote this method to show you how to call IsWinner and check for a Tie
        private bool IsTie()
        {
            int winningDimension, winningValue;
            return (IsFull() && !IsWinner(out winningDimension, out winningValue));
        }

        // This method takes an integer in the range 0 - 4 that represents a column
        // as it's parameter and changes the font color of that cell to red.
        private void HighlightColumn(int col)
        {
            for (int row = 0; row < SIZE; row++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        // This method changes the font color of the top right to bottom left diagonal to red
        // I did this diagonal because it's harder than the other one
        private void HighlightDiagonal2()
        {
            for (int row = 0, col = SIZE - 1; row < SIZE; row++, col--)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        // This method will highlight either diagonal, depending on the parameter that you pass
        private void HighlightDiagonal(int whichDiagonal)
        {
            if (whichDiagonal == TOP_LEFT_TO_BOTTOM_RIGHT)
                HighlightDiagonal1();
            else
                HighlightDiagonal2();

        }

        //* TODO:  finish these 2

        //if winning row found, it will call this method to highlight
        private void HighlightRow(int row)
        {
            for (int col = 0; col < 5; col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        //if a winner is found the diagonal from Upper-Left to bottom right will highlight
        private void HighlightDiagonal1()
        {
            for (int row = 0, col = 0; col < 5; row++, col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        //* TODO:  finish this
        
        //Used to call individual highlight methods to highlight winning path
        private void HighlightWinner(string player, int winningDimension, int winningValue)
        {
            switch (winningDimension)
            {
                case ROW:
                    HighlightRow(winningValue);
                    resultLabel.Text = (player + " Wins!");
                    break;
                case COLUMN:
                    HighlightColumn(winningValue);
                    resultLabel.Text = (player + " Wins!");
                    break;
                case DIAGONAL:
                    HighlightDiagonal(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
            }
        }

        //* TODO:  finish these 2
        //remove all data for all squares
        private void ResetSquares()
        {
            for (int square = 1; square < 26; square++)
            {
                //(Label)(this.Controls["label" + square.ToString()]);
                Label templbl = (Label)(this.Controls["label" + square.ToString()]);
                templbl.Text = "";
                templbl.Enabled = true;
                templbl.ForeColor = Color.Black;

            }

        }
        //Create a random based on the usable fields use it to mimic a CPU turn
        //Check for winners and if none found make it the User's turn
        private void MakeComputerMove()
        {
            int row, col;
            int winningDimension = NONE;
            int winningValue = NONE;
            string user = "CPU ";

            Random rand = new Random();
            Label selectedLabel;

            do
            {
                row = rand.Next(0, 5);
                col = rand.Next(0, 5);
                selectedLabel = GetSquare(row, col);
            }
            while (selectedLabel.Text != EMPTY);
            {
                selectedLabel.Text = COMPUTER_SYMBOL;
                selectedLabel.Enabled = false;

                if (IsWinner(out winningDimension, out winningValue))
                {
                    DisableAllSquares();
                    HighlightWinner(user, winningDimension, winningValue);

                }
                else if (IsTie())
                    resultLabel.Text = "It's a tie!";
                //else
                //    MessageBox.Show("User's turn.");
            }

        }

        // Setting the enabled property changes the look and feel of the cell.
        // Instead, this code removes the event handler from each square.
        // Use it when someone wins or the board is full to prevent clicking a square.
        private void DisableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    DisableSquare(square);
                }
            }
        }

        // Inside the click event handler you have a reference to the label that was clicked
        // Use this method (and pass that label as a parameter) to disable just that one square
        private void DisableSquare(Label square)
        {
            square.Click -= new System.EventHandler(this.label_Click);
        }

        // You'll need this method to allow the user to start a new game
        private void EnableAllSquares()
        {
            //use for loops to enable every square and column
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Click += new System.EventHandler(this.label_Click);
                }
            }
        }

        //* TODO:  finish the event handlers
        private void label_Click(object sender, EventArgs e)
        {
            int winningDimension = NONE;
            int winningValue = NONE;
            string user = "User ";

            Label clickedLabel = (Label)sender;

            //mark the clicked square for user if it's empty
            if (clickedLabel.Text == EMPTY)
            {
                int row, column;
                GetRowAndColumn(clickedLabel, out row, out column);

                clickedLabel.Text = USER_SYMBOL;
                clickedLabel.Enabled = false;

                //check for a winner after changing column for User
                if (IsWinner(out winningDimension, out winningValue))
                {
                    DisableAllSquares();
                    HighlightWinner(user, winningDimension, winningValue);
                    DisableAllSquares();

                }
                //give a result of a tie
                else if (IsTie())
                    resultLabel.Text = "It's a tie!";
                else
                    //make the CPU move
                    MakeComputerMove();
            }

        }


        private void newGameButton_Click(object sender, EventArgs e)
        {
            ResetSquares();  //wipes data in all squares to default
            resultLabel.Text = "";  //blank out result label
            EnableAllSquares(); //Enables all squares

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();  //close the form
        }

        private void TTTForm_Load(object sender, EventArgs e)
        {
            EnableAllSquares();  //enables on squares on load
        }
    }
}
