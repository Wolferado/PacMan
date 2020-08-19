using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Game : Form
    {
        private int initialEnemtCount = 4;

        private Random rand = new Random();
        private Label labelGameOver = new Label();
        private Level level = new Level();
        private Hero hero = new Hero();
        private Timer mainTimer = new Timer();
        private List<Enemy> enemies = new List<Enemy>();
        public Game()
        {
            InitializeComponent();
            InitializeGame();
            InitializeMainTimer();
        }

        private void InitializeGame()
        {
            this.Size = level.Size;
            this.KeyDown += Game_KeyDown;
            InitializeGameOverLabel();
            AddLevel();
            AddHero();
            AddEnemies();
        }

        private void InitializeGameOverLabel()
        {
            this.Controls.Add(labelGameOver);
            labelGameOver.Parent = level;
            labelGameOver.Visible = false;
            labelGameOver.Size = new Size(450, 200);
            labelGameOver.Location = new Point(150, 175);
            labelGameOver.Font = new Font("HelvLight", 50f);
            labelGameOver.BackColor = Color.Transparent;
            labelGameOver.ForeColor = Color.Yellow;
            labelGameOver.Text = "Game Over";
        }

        private void AddLevel()
        {
            this.Controls.Add(level);
        }

        private void AddHero()
        {
            this.Controls.Add(hero);
            hero.BringToFront();
            hero.Parent = level;
        }

        private void AddEnemies()
        {
            Enemy enemy;
            for (int i = 0; i < initialEnemtCount; i++)
            {
                enemy = new Enemy();
                enemies.Add(enemy);
                enemies[i].Location = new Point(rand.Next(0, 800), rand.Next(0, 500));
                enemy.SetEnemyDirection(rand.Next(1, 4));
                this.Controls.Add(enemy);
                enemy.BringToFront();
            }
        }

        private void InitializeMainTimer()
        {
            mainTimer = new Timer();
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Interval = 10;
            mainTimer.Start();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            MoveHero();
            HeroBorderCollision();
            HeroEnemyCollision();
            MoveEnemy();
            EnemyBorderCollision();
        }

        private void MoveHero()
        {
            hero.Left += hero.HorizontalVelocity;
            hero.Top += hero.VerticalHelocity;
        }

        private void MoveEnemy()
        {
            foreach (var enemy in enemies)
            {
                enemy.Left += enemy.HorizontalVelocity;
                enemy.Top += enemy.VerticalVelocity;
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    hero.HorizontalVelocity = hero.Step;
                    hero.VerticalHelocity = 0;
                    hero.Direction = "right";
                    break;
                case Keys.Down:
                    hero.HorizontalVelocity = 0;
                    hero.VerticalHelocity = hero.Step;
                    hero.Direction = "down";
                    break;
                case Keys.Left:
                    hero.HorizontalVelocity = -hero.Step;
                    hero.VerticalHelocity = 0;
                    hero.Direction = "left";
                    break;
                case Keys.Up:
                    hero.HorizontalVelocity = 0;
                    hero.VerticalHelocity = -hero.Step;
                    hero.Direction = "up";
                    break;
            }
        }

        private void HeroBorderCollision()
        {
            if(hero.Left > level.Width + hero.Width) //Collision with right side
            {
                hero.Left = level.Left - hero.Width;
            }
            else if(hero.Left + hero.Width < level.Left) //Collision with left side
            {
                hero.Left = level.Width + hero.Width;
            }
            else if(hero.Top > level.Height + hero.Height) //Collision with down side
            {
                hero.Top = level.Top - hero.Height;
            }
            else if(hero.Top + hero.Height < level.Top) //Collision with upper side
            {
                hero.Top = level.Top + level.Height;
            }
            SetRandomEnemyDirection();
        }

        private void HeroEnemyCollision()
        {
            foreach (var enemy in enemies)
            {
                if (hero.Bounds.IntersectsWith(enemy.Bounds))
                {
                    GameOver();
                }
            }
        }

        private void EnemyBorderCollision()
        {
            foreach (var enemy in enemies)
            {
                if(enemy.Top < level.Top) //From "up" to "down"
                {
                    enemy.HorizontalVelocity = 0;
                    enemy.VerticalVelocity = +enemy.Step;
                }
                if(enemy.Top > level.Height - enemy.Height) //From "down" to "up"
                {
                    enemy.HorizontalVelocity = 0;
                    enemy.VerticalVelocity = -enemy.Step;
                }
                if(enemy.Left < level.Left) //From "left" to "right"
                {
                    enemy.HorizontalVelocity = +enemy.Step;
                    enemy.VerticalVelocity = 0;
                }
                if(enemy.Left > level.Width - enemy.Width) //From "right" to "left"
                {
                    enemy.HorizontalVelocity = -enemy.Step;
                    enemy.VerticalVelocity = 0;
                }
            }
        }

        private void SetRandomEnemyDirection()
        {
            foreach(var enemy in enemies)
            {
                enemy.SetEnemyDirection(rand.Next(1, 4));
            }
        }

        private void GameOver()
        {
            mainTimer.Stop();
            labelGameOver.Visible = true;
            labelGameOver.BringToFront();
        }
    }
}
