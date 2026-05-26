namespace Game
{
    public partial class Form1 : Form
    {
        private Button[,] buttons;
        private char currentPlayer = 'X';
        private bool gameOver = false;
        public Form1()
        {
            InitializeComponent();
            InitBoard();
        }
        private void InitBoard()
        {
            buttons = new Button[3, 3]
            {
                { button1, button2, button3 },
                { button4, button5, button6 },
                { button7, button8, button9 }
            };
            foreach (var btn in buttons)
            {
                btn.Click += Button_Click;
                btn.Font = new System.Drawing.Font("Arial", 24);
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            if (gameOver)
            {
                return;
            }
            Button btn = sender as Button;
            if (btn.Text != "")
            {
                return;
            } 
            btn.Text = currentPlayer.ToString();
            if (CheckWin())
            {
                MessageBox.Show($"Игрок {currentPlayer} победил!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gameOver = true;
                return;
            }
            if (IsDraw())
            {
                MessageBox.Show("Ничья!", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gameOver = true;
                return;
            }
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }
        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text == currentPlayer.ToString() &&
                    buttons[i, 1].Text == currentPlayer.ToString() &&
                    buttons[i, 2].Text == currentPlayer.ToString())
                {
                    return true;
                }
                if (buttons[0, i].Text == currentPlayer.ToString() &&
                    buttons[1, i].Text == currentPlayer.ToString() &&
                    buttons[2, i].Text == currentPlayer.ToString())
                {
                    return true;
                }
            }
            if (buttons[0, 0].Text == currentPlayer.ToString() &&
                buttons[1, 1].Text == currentPlayer.ToString() &&
                buttons[2, 2].Text == currentPlayer.ToString())
            {
                return true;
            }
            if (buttons[0, 2].Text == currentPlayer.ToString() &&
                buttons[1, 1].Text == currentPlayer.ToString() &&
                buttons[2, 0].Text == currentPlayer.ToString())
            {
                return true;
            }
            return false;
        }
        private bool IsDraw()
        {
            foreach (var btn in buttons)
            {
                if (btn.Text == "")
                {
                    return false;
                } 
            }
            return true;
        }
        private void ResetGame()
        {
            foreach (var btn in buttons)
            {
                btn.Text = "";
            }
            currentPlayer = 'X';
            gameOver = false;
        }
        private void SaveGame(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(currentPlayer);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        sw.Write(buttons[i, j].Text == "" ? "-" : buttons[i, j].Text);
                    }
                    sw.WriteLine();
                }
            }
        }
        private void LoadGame(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                currentPlayer = sr.ReadLine()[0];

                for (int i = 0; i < 3; i++)
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < 3; j++)
                    {
                        buttons[i, j].Text = line[j] == '-' ? "" : line[j].ToString();
                    }
                }
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGame("save.txt");
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadGame("save.txt");
        }
        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}
