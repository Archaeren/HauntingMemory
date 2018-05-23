using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchGame
{
    public partial class HauntingMemory : Form
    {

        // firstClicked points to the first Label control 
        // that the player clicks, but it will be null 
        // if the player hasn't clicked a label yet
        Label firstClicked = null;

        // secondClicked points to the second Label control 
        // that the player clicks
        Label secondClicked = null;

        // Use this Random object to choose random icons for the squares
        Random random = new Random();

        bool victoryTrue = false;

        //This integer variable keeps track of pairs clicked
        int clickCount;

        //This integer variable keeps track of how many matches have been made
        int matchMade;

        //This integer variable keeps track of how many total matches are possible
        int matchTotal;

        // This integer variable keeps track of the time.
        int timeCount;

        /* Each of these letters is an spooky icon
        in the Webdings font,
        and each icon appears twice in this list
        Had to create a variable to define one of them
        because it's an invisible character
        */
        private static readonly string shy = ((char)173).ToString();
        List<string> icons = new List<string>()
        {
            "!", "!", "\"", "\"", "N", "N", "Z", "Z",
            "•", "•", "˜", "˜", "¡", "¡", "Ñ", "Ñ",
            "ó", "ó", "ö", "ö", "«", "«", shy, shy
        };
        private void AssignIconsToSquares()
        {

            //This foreach loop selects an icon randomly from the icon list
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                //checks to see if the instance of 'control' is a label
                //if it is, tells the program it's okay to use as a label
                //if not a label, returns null
                Label iconLabel = control as Label;
                if (iconLabel != null && iconLabel.Text == "c")
                {
                    //creates a variable, randomNumber, by selecting a number
                    //between 0 and the end of the icons list
                    int randomNumber = random.Next(icons.Count);
                    //chooses the text string from the list based on the
                    //number chosen in the previous line
                    iconLabel.Text = icons[randomNumber];
                    //changes the icon's color so the player can't see them
                    iconLabel.ForeColor = iconLabel.BackColor;
                    //removes the text string for the icon we just used
                    //so it doesn't get repeated
                    icons.RemoveAt(randomNumber);
                }

            }
        }

        public HauntingMemory()
        {
            InitializeComponent();
            
            timer2.Start();

            matchTotal = (icons.Count / 2);

            AssignIconsToSquares();

            
        }


        private void label_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching 
            // icons have been shown to the player, 
            // so ignore any clicks if the timer is running
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // If firstClicked is null, this is the first icon 
                // in the pair that the player clicked,
                // so set firstClicked to the label that the player 
                // clicked, change its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    clickCount += 1;

                    return;
                }

                // If the player gets this far, the timer isn't
                // running and firstClicked isn't null,
                // so this must be the second icon the player clicked
                // Set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;



                // If the player clicked two matching icons, keep them 
                // black and reset firstClicked and secondClicked 
                // so the player can click another icon
                if (firstClicked.Text == secondClicked.Text)
                {
                    matchMade += 1;
                    firstClicked = null;
                    secondClicked = null;
                    // Check to see if the player won
                    CheckForWinner();

                    return;
                }

                

                // If the player gets this far, the player 
                // clicked two different icons, so start the 
                // timer (which will wait three quarters of 
                // a second, and then hide the icons)
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked 
            // so the next time a label is
            // clicked, the program knows it's the first click
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            if (matchMade == matchTotal)
            {
                timer2.Stop();

                MessageBox.Show("You are the spookiest!!", "Your soul is dark as night");
                Close();
            }
            else
            {
                return;
            }
            

            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            /*foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }*/

            // If the loop didn't return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timeCount = timeCount + 1;
            timeLabel.Text = timeCount.ToString();
            //if the player hasn't won yet, add 1 to the counter every second
            //if (victoryTrue == false)
            {
                //adds 1 to the counter
                //timeCount = timeCount + 1;
                //Should display the time in the label. Commented out because it's causing a refresh issue.
                //timeLabel.Text = timeCount.ToString();
            }
            //else
                //Theoretically stops the timer upon winning
                //can't test because form area currently too small to display
                //timer2.Stop();
        }
    }
}
