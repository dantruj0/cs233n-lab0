﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    public partial class boardForm : Form
    {
        public boardForm()
        {
            InitializeComponent();
        }

        #region Instance Variables
        const int NOT_PICKED_YET = -1;

        int firstCardNumber = NOT_PICKED_YET;
        int secondCardNumber = NOT_PICKED_YET;
        int matches = 0;

        //Total cards to call upon
        const int CARDS = 21;

        //PLaying board locations
        string[] chosenCards = new string[CARDS]; 
        #endregion

        #region Methods

        // This method finds a picture box on the board based on it's number (1 - 20)
        // It takes an integer as it's parameter and returns the picture box controls
        // that's name contains that number
        private PictureBox GetCard(int i)
        {
            PictureBox card = (PictureBox)this.Controls["card" + i];
            return card;
        }

        // This method gets the filename for the image displayed in a picture box given it's number
        // It takes an integer as it's parameter and returns a string containing the 
        // filename for the image in the corresponding picture box
        private string GetCardFilename(int i)
        {
            return GetCard(i).Tag.ToString();
        }

        // This method changes the filename for a given picture box
        // It takes an integer and a string that represents a filename as it's parameters
        // It doesn't return a value but stores the filename for the image to be displayed
        // in the picture box.  It doesn't actually display the new image
        private void SetCardFilename(int i, string filename)
        {
            GetCard(i).Tag = filename;
        }

        // These 2 methods get the value (and suit) of the card in a given picturebox
        // Both methods take an integer as the parameter and return a string
        private string GetCardValue(int index)
        {
            return GetCardFilename(index).Substring(4, 1);
        }

        private string GetCardSuit(int index)
        {
            return GetCardFilename(index).Substring(5, 1);
        }

        // TODO:  students should write this one

        //This will check two picked cards for a match
        private bool IsMatch(int index1, int index2)
        {
            if (GetCardValue(index1) == GetCardValue(index2) )
                return true;
            else
                return false;

        }
      
        
        // This method fills each picture box with a filename
        private void FillCardFilenames()
        {
            string[] values = { "a", "2", "j", "q", "k" };
            string[] suits = { "c", "d", "h", "s" };
            int i = 1;

            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 0; value <= 4; value++)
                {
                    //LOOK AT THIS LATER


                    //chosenCards[i] = values[value] + suits[suit];
                    SetCardFilename(i, "card" + values[value] + suits[suit] + ".jpg");
                    i++;
                }
            }
        }

        // TODO:  students should write this one
        
        //ShuffleCards and Swap are Used to shuffle the deck by 
        //moving .Tag info from PictureBox to another randomly generated picturebox
        private void ShuffleCards()
        {
            Random rand = new Random();


            for (int card = 1; card < CARDS;  card++)
            {
                
                //choose a card beween 1 and 20 and assign to card2
                int card2 = rand.Next(1, CARDS);
                //Swap(ref SetCardFilename(card), ref chosenCards[card2]);
                Swap(GetCard(card),GetCard(card2));

            }
            
        }
        private void Swap(PictureBox card1, PictureBox card2)
        {
            cardTemp.Tag = card1.Tag;
            card1.Tag = card2.Tag;
            card2.Tag = cardTemp.Tag;
            /*
             * PictureBox tempPB;
             * tempPB = card1;
             * tempPB.Tag = card1.Tag;
             * card1.Tag = card2.Tag;
             * card2.Tag = tempPB.Tag;
             * WHY WOULDNT THIS WORK.  HAD TO CREATE ACTUAL PICTUREBOX AND MAKE IT INVISIBLE */

        }

        // This method loads (shows) an image in a picture box.  Assumes that filenames
        // have been filled in an earlier call to FillCardFilenames
        private void LoadCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\" + GetCardFilename(i));
        }

        // This method loads the image for the back of a card in a picture box
        private void LoadCardBack(int i)
        {
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\black_back.jpg");
        }

        // TODO:  students should write all of these
        // shows (loads) the backs of all of the cards
        private void LoadAllCardBacks()
        {
            for (int card = 1; card <= 20; card++)
            {
                //PictureBox cardPic = GetCard(card);
                LoadCardBack(card);

                
            }
        }

        // Hides a picture box
        private void HideCard(int i)
        {
            GetCard(i).Visible = false;
        }

        //hides all pictureboxes
        private void HideAllCards()
        {
            for(int card = 1; card < CARDS; card++)
            {
                HideCard(card);
            }
        }

        //Shows all dealt cards
        private void ShowAllCards()
        {
            for (int card =1; card < CARDS; card++)
            {
                LoadCard(card);
            }
        }

        // disables a picture box
        private void DisableCard(int i)
        {
            GetCard(i).Enabled = false;
        }

        //Disable cards from being used 
        private void DisableAllCards()
        {
            for (int card = 1; card < CARDS; card++)
            {
                DisableCard(card);
            }
        }

        //Enable a picturebox if a match is not found
        private void EnableCard(int i)
        {
            GetCard(i).Enabled = true;
        }

        //Enable all picture boxes
        private void EnableAllCards()
        {
            for (int card = 1; card < CARDS; card++)
            {
                EnableCard(card);
            }
        }
        

        //Any card that is visible will Reenabled for picks
        private void EnableAllVisibleCards()
        {
            for (int card = 1; card < CARDS; card++)
            {
                if (GetCard(card).Visible == true)
                {
                    GetCard(card).Enabled = true;
                }
            }
        }

        #endregion

        #region EventHandlers
        private void boardForm_Load(object sender, EventArgs e)
        {
            /* 
             * Fill the picture boxes with filenames
             * Shuffle the cards
             * Load all of the card backs - 
             *      While you're testing you might want to load all of card faces
             *      to make sure that the cards are loaded successfully and that
             *      they're shuffled.  If you get all 2s, something is wrong.
            */
            
            
            FillCardFilenames();
            ShuffleCards();
      
            LoadAllCardBacks();

        }

        private void card_Click(object sender, EventArgs e)
        {
            PictureBox card = (PictureBox)sender;
            //MessageBox.Show(card.Tag.ToString());
            int cardNumber = int.Parse(card.Name.Substring(4));

            if (firstCardNumber == NOT_PICKED_YET)
            {
                LoadCard(cardNumber);
                DisableCard(cardNumber);
                firstCardNumber = cardNumber;
                
            }
            else
            {
                secondCardNumber = cardNumber;
                LoadCard(cardNumber);
                DisableAllCards();
                flipTimer.Enabled = true;

            }

            /* 
             * if the first card isn't picked yet
             *      save the first card index
             *      load the card
             *      disable the card
             *  else (the user just picked the second card)
             *      save the second card index
             *      load the card
             *      disable all of the cards
             *      start the flip timer
             *  end if
            */
        }

        private void flipTimer_Tick(object sender, EventArgs e)
        {
            flipTimer.Enabled = false;
            
            if (IsMatch(firstCardNumber, secondCardNumber))
            {
                matches += 1;
                HideCard(firstCardNumber);
                HideCard(secondCardNumber);
                firstCardNumber = NOT_PICKED_YET;
                secondCardNumber = NOT_PICKED_YET;

                if (matches == 10)
                {
                    MessageBox.Show("You have matched every card.  Real nice there pal");
                }
                else
                {
                    EnableAllVisibleCards();
                }

            }
            else
            {
                LoadCardBack(firstCardNumber);
                LoadCardBack(secondCardNumber);
                firstCardNumber = NOT_PICKED_YET;
                secondCardNumber = NOT_PICKED_YET;
                EnableAllVisibleCards();
            }
             /*
             * stop the flip timer
             * if the first card and second card are a match
             *      increment the number of matches
             *      hide the first card
             *      hide the second card
             *      reset the first card number
             *      reset the second card number
             *      if the number of matches is 10
             *          show a message box
             *      else
             *          enable all of the cards left on the board
             *      end if
             * else
             *      flip the first card back over
             *      flip the second card back over
             *      reset the first card number
             *      reset the second card number
             *      enable all of the cards left on the board
             * end if
             */
        }
        #endregion


    }
}
