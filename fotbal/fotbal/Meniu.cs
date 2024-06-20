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
    public partial class Meniu : Form
    {
        public Meniu()
        {
            InitializeComponent();
            MenuButtons();
        }


        public void MenuButtons()
        {
            Button singlePlayer = new Button();
            singlePlayer.Tag = "computerMenuBTN";
            singlePlayer.Text = "Computer";
            singlePlayer.Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            singlePlayer.Width = 250;
            singlePlayer.Height = 100;
            singlePlayer.Location = new Point(this.ClientSize.Width / 2 - singlePlayer.Width / 2, 300);
            this.Controls.Add(singlePlayer);
            singlePlayer.BackColor = Color.Orange;
            singlePlayer.Click += new EventHandler(Computer_Clicked);

            Button local = new Button();
            local.Tag = "singlePlayerMenuBTN";
            local.Text = "LOCAL";
            local.Font = new Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            local.Width = 250;
            local.Height = 100;
            local.Bounds = new Rectangle(this.ClientSize.Width / 2 - local.Width / 2, 450, local.Width, local.Height);
            local.Visible = true;
            this.Controls.Add(local);
            local.BackColor = Color.Orange;
            local.Click += new EventHandler(Local_Clicked);
        }
        public void RemoveButtons()
        {
            this.Controls.Clear();
        }

        public void ChangeToLocal()
        {
            Form1 localForm = new Form1();
            localForm.TopLevel = false;
            localForm.Dock = DockStyle.Fill;
            this.Controls.Add(localForm);
            localForm.Show();
        }
        public void ChangeToDifficultyForm()
        {
            DifficultyForm difficultyForm = new DifficultyForm();
            difficultyForm.TopLevel = false;
            difficultyForm.Dock = DockStyle.Fill;
            this.Controls.Add(difficultyForm);
            difficultyForm.Show();
        }
        private void Local_Clicked(object sender, EventArgs e)
        {
            RemoveButtons();
            ChangeToLocal();

        }
        private void Computer_Clicked(object sender, EventArgs e)
        {
            RemoveButtons();
            ChangeToDifficultyForm();

        }




    }
}
