using PacMan.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    class Hero : PictureBox
    {
        public int Step { get; set; } = 2;
        public int HorizontalVelocity { get; set; } = 0;
        public int VerticalHelocity { get; set; } = 0;
        public string Direction { get; set; } = "right";

        private Timer animationTimer = null;
        private int frameCounter = 1;
        public Hero()
        {
            InitializeHero();
            InitializeAnimtationTimer();
        }

        private void InitializeAnimtationTimer()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 200;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object seder, EventArgs e)
        {
            Animate();
        }

        private void Animate()
        {
            this.Image = (Image)Resources.ResourceManager.GetObject("pacman_" + this.Direction + "_" + frameCounter.ToString());
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            frameCounter++;
            if (frameCounter > 4)
            {
                frameCounter = 1;
            }
        }

        private void InitializeHero()
        {
            this.BackColor = Color.Transparent;
            this.Size = new Size(60, 60);
            this.Location = new Point(100, 100);
            this.Name = "PacMan";
        }
    }
}
