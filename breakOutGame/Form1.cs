using System;
using System.Drawing;
using System.Windows.Forms;

namespace breakOutGame
{
    public partial class Form1 : Form
    {
        //movimentação
        private bool _moverDireita;

        private bool _moverEsquerda;
        private int _velocidade = 10;

        //posicao bola
        private int _ballX = 5;

        private int _ballY = 5;

        private int _score = 0;
        private Random rndCores = new Random();

        public Form1()
        {
            InitializeComponent();
            ColorirBlocos();
        }

        private void ColorirBlocos()
        {
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && item.Tag == "block")
                {
                    Color cores = Color.FromArgb(rndCores.Next(256), rndCores.Next(256), rndCores.Next(256));
                    item.BackColor = cores;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && player.Left > 0)
            {
                _moverEsquerda = true;
            }

            if (e.KeyCode == Keys.Right && player.Left + player.Width < 920)
            {
                _moverDireita = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                _moverEsquerda = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                _moverDireita = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Left += _ballX;
            ball.Top += _ballY;
            label1.Text = "Score : " + _score;

            if (_moverEsquerda) { player.Left -= _velocidade; }

            if (_moverDireita) { player.Left += _velocidade; }

            if (player.Left < 1)
            {
                _moverEsquerda = false;
            }
            else if (player.Left + player.Width > 920)
            {
                _moverDireita = true;
            }

            if (ball.Right + ball.Width > ClientSize.Width || ball.Left < 0)
            {
                _ballX = -_ballX;
            }

            if (ball.Top < 0 || ball.Bounds.IntersectsWith(player.Bounds))
            {
                _ballY = -_ballY;
            }

            if (ball.Top + ball.Height > ClientSize.Height)
            {
                gameOver();
            }

            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && item.Tag == "block")
                {
                    if (ball.Bounds.IntersectsWith(item.Bounds))
                    {
                        this.Controls.Remove(item);
                        _ballY = -_ballY;
                        _score++;
                    }
                }
            }

            if (_score > 34)
            {
                gameOver();
                MessageBox.Show("Vitoria");
            }
        }

        private void gameOver()
        {
            timer1.Stop();
        }

        private bool limiteTelaEsquerda()
        {
            return player.Left < 1;
        }
    }
}