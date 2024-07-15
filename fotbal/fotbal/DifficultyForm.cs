using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fotbal
{
    public partial class DifficultyForm : Form
    {
        public DifficultyForm()
        {
            InitializeComponent();
            ShowDifficultyButtons();
        }

        private void ShowDifficultyButtons()
        {
            Button easy = new Button();
            easy.Tag = "easyBTN";
            easy.Text = "EASY";
            easy.Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            easy.Width = 250;
            easy.Height = 100;
            easy.Location = new Point(this.Width / 2 - easy.Width / 2, 300);
            this.Controls.Add(easy);
            easy.BackColor = Color.Orange;
            easy.Click += new EventHandler(Easy_Clicked);

            Button medium = new Button();
            medium.Tag = "mediumBTN";
            medium.Text = "Medium";
            medium.Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            medium.Width = 250;
            medium.Height = 100;
            medium.Bounds = new Rectangle(this.Width / 2 - medium.Width / 2, 450, medium.Width, medium.Height);
            medium.Visible = true;
            this.Controls.Add(medium);
            medium.BackColor = Color.Orange;
            medium.Click += new EventHandler(Medium_Clicked);

            Button hard = new Button();
            hard.Tag = "hardBTN";
            hard.Text = "HARD";
            hard.Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            hard.Width = 250;
            hard.Height = 100;
            hard.Bounds = new Rectangle(this.Width / 2 - hard.Width / 2, 600, hard.Width, hard.Height);
            hard.Visible = true;
            this.Controls.Add(hard);
            hard.BackColor = Color.Orange;
            hard.Click += new EventHandler(Hard_Clicked);

        }

        public void ChangeToEasy()
        {
            Computer computer = new Computer("easy");
            computer.TopLevel = false;
            computer.Dock = DockStyle.Fill;
            this.Controls.Add(computer);
            computer.Show();
        }
        private void Easy_Clicked(object sender, EventArgs e)
        {
            RemoveButtons();
            ChangeToEasy();

        }
        public void ChangeToMedium()
        {
            Computer computer = new Computer("medium");
            computer.TopLevel = false;
            computer.Dock = DockStyle.Fill;
            this.Controls.Add(computer);
            computer.Show();
        }
        private void Medium_Clicked(object sender, EventArgs e)
        {
            RemoveButtons();
            ChangeToMedium();

        }
        public void ChangeToHard()
        {
            //Computer computer = new Computer("hard");
            //computer.TopLevel = false;
            //computer.Dock = DockStyle.Fill;
            //this.Controls.Add(computer);
            //computer.Show();
        }
        private void Hard_Clicked(object sender, EventArgs e)
        {
            RemoveButtons();
            ChangeToHard();

        }




        public void RemoveButtons()
        {
            this.Controls.Clear();
        }
    }
}
