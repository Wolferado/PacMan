using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    class Enemy : PictureBox
    {
        public int Step { get; set; } = 2;
        public int HorizontalVelocity { get; set; } = 0;
        public int VerticalVelocity { get; set; } = 0;
        public Enemy()
        {
            InitializeEnemy();
        }

        private void InitializeEnemy()
        {
            this.BackColor = Color.Red;
            this.Size = new Size(55, 55);
            this.Tag = "ghost";
        }

        public void SetEnemyDirection(int directionCode)
        {
            switch (directionCode)
            {
                case 1: //Up
                    HorizontalVelocity = 0;
                    VerticalVelocity = -Step;
                    break;
                case 2: //Right
                    HorizontalVelocity = +Step;
                    VerticalVelocity = 0;
                    break;
                case 3: //Down
                    HorizontalVelocity = 0;
                    VerticalVelocity = +Step;
                    break;
                case 4: //Left
                    HorizontalVelocity = -Step;
                    VerticalVelocity = 0;
                    break;
            }
        }
    }
}
