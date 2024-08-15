using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        public int spieler = 2;
        public int zug = 0;
        public int s1 = 0; // Score Spieler1
        public int s2 = 0; // Score Spieler2
        public int sd = 0; // Score Draw

        bool IsDraw()
        {
            if ((zug == 9) && (IsWinner() == false))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        bool IsWinner()
        {
            // Horizontal

            if ((ObenLinks.Text == ObenMitte.Text) && (ObenMitte.Text == ObenRechts.Text) && ObenLinks.Text !="")
            {
                return true;
            }

            if ((MitteLinks.Text == MitteMitte.Text) && (MitteMitte.Text == MitteRechts.Text) && MitteLinks.Text != "")
            {
                return true;
            }

            if ((UntenLinks.Text == UntenMitte.Text) && (UntenMitte.Text == UntenRechts.Text) && UntenLinks.Text != "")
            {
                return true;
            }

            // Vertikal

            if ((ObenLinks.Text == MitteLinks.Text) && (MitteLinks.Text == UntenLinks.Text) && ObenLinks.Text != "")
            {
                return true;
            }

            if ((ObenMitte.Text == MitteMitte.Text) && (MitteMitte.Text == UntenMitte.Text) && ObenMitte.Text != "")
            {
                return true;
            }

            if ((ObenRechts.Text == MitteRechts.Text) && (MitteRechts.Text == UntenRechts.Text) && ObenRechts.Text != "")
            {
                return true;
            }

            // Diagonal

            if ((ObenLinks.Text == MitteMitte.Text) && (MitteMitte.Text == UntenRechts.Text) && ObenLinks.Text != "")
            {
                return true;
            }

            if ((UntenLinks.Text == MitteMitte.Text) && (MitteMitte.Text == ObenRechts.Text) && UntenLinks.Text != "")
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_scoreX.Text = "Player 1 (X): " + s1;
            lbl_scoreY.Text = "Player 2 (O): " + s2;
            lbl_scoreDraw.Text = "Draw: " + sd;
        }

        private void ButtonKlick(object sender, EventArgs e)
        {
            if (IsGameOver()) // Check if the game is already over
            {
                MessageBox.Show("The game is already over. Start a new game!");
                return;
            }

            var button = (Button)sender;

            if (!button.Enabled)
            {
                // Button is disabled, do nothing
                return;
            }

            if (button.Text == "")
            {
                if (spieler %2 == 0)
                {
                    button.Text = "X";
                    spieler++;
                    zug++;
                }

                else
                {
                    button.Text = "O";
                    spieler++;
                    zug++;
                }

                button.Enabled = false;

                if (IsDraw() == true)
                {
                    MessageBox.Show("Its a draw!");
                    sd++;
                    NewGame();
                }

                if (IsWinner() == true)
                {
                    if (button.Text == "X")
                    {
                        MessageBox.Show("Player 1 (X) won the game!");
                        s1++;
                        NewGame();
                    }

                    else
                    {
                        MessageBox.Show("Player 2 (O) won the game!");
                        s2++;
                        NewGame();
                    }
                }
            }
        }

        private void bttn_newgame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        public void NewGame()
        {
            spieler = 2;
            zug = 0;
            ObenLinks.Text = ObenMitte.Text = ObenRechts.Text = MitteLinks.Text = MitteMitte.Text = MitteRechts.Text = UntenLinks.Text = UntenMitte.Text = UntenRechts.Text = "";

            // Re-Enable Buttons

            ObenLinks.Enabled = ObenMitte.Enabled = ObenRechts.Enabled = MitteLinks.Enabled = MitteMitte.Enabled = MitteRechts.Enabled = UntenLinks.Enabled = UntenMitte.Enabled = UntenRechts.Enabled = true;

            lbl_scoreX.Text = "Player 1 (X): " + s1;
            lbl_scoreY.Text = "Player 2 (O): " + s2;
            lbl_scoreDraw.Text = "Draw: " + sd;
        }

        private void bttn_reset_Click(object sender, EventArgs e)
        {
            s1 = s2 = sd = 0;
            NewGame();
        }

        private void bttn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsGameOver()
        {
            return IsWinner() || IsDraw();
        }
    }
}
