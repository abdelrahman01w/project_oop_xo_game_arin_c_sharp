using System;
using System.Drawing;
using System.Windows.Forms;

namespace XOGUI
{
    public class Program : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private Label scoreLabel;
        private int player1Score = 0;
        private int player2Score = 0;
        private bool isPlayer1Turn = true;

        public Program()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "XO Game";
            this.Size = new Size(400, 500);

            // Create buttons for the grid
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Font = new Font("Arial", 24),
                        Size = new Size(100, 100),
                        Location = new Point(100 * j + 30, 100 * i + 30)
                    };
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }

            // Score label
            scoreLabel = new Label
            {
                Text = "Player 1: 0 | Player 2: 0",
                Font = new Font("Arial", 14),
                Location = new Point(30, 350),
                AutoSize = true
            };
            this.Controls.Add(scoreLabel);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Text != "")
                return;

            clickedButton.Text = isPlayer1Turn ? "X" : "O";
            clickedButton.Enabled = false;

            if (CheckWinner())
            {
                if (isPlayer1Turn)
                {
                    player1Score++;
                    MessageBox.Show("Player 1 wins!");
                }
                else
                {
                    player2Score++;
                    MessageBox.Show("Player 2 wins!");
                }

                UpdateScores();
                ResetBoard();
            }
            else if (IsDraw())
            {
                MessageBox.Show("It's a draw!");
                ResetBoard();
            }

            isPlayer1Turn = !isPlayer1Turn;
        }

        private bool CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" && buttons[i, 0].Text == buttons[i, 1].Text && buttons[i, 1].Text == buttons[i, 2].Text)
                    return true;

                if (buttons[0, i].Text != "" && buttons[0, i].Text == buttons[1, i].Text && buttons[1, i].Text == buttons[2, i].Text)
                    return true;
            }

            if (buttons[0, 0].Text != "" && buttons[0, 0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 2].Text)
                return true;

            if (buttons[0, 2].Text != "" && buttons[0, 2].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 0].Text)
                return true;

            return false;
        }

        private bool IsDraw()
        {
            foreach (var button in buttons)
            {
                if (button.Text == "")
                    return false;
            }
            return true;
        }

        private void UpdateScores()
        {
            scoreLabel.Text = $"Player 1: {player1Score} | Player 2: {player2Score}";
        }

        private void ResetBoard()
        {
            foreach (var button in buttons)
            {
                button.Text = "";
                button.Enabled = true;
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Program());
        }
    }
}
