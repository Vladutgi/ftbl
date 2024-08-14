
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace fotbal
{
    public partial class Form1 : Form
    {
        private bool intersects = false;
        private bool available = true;
        Servicii servicii;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            timer1.Start();
            foreach (Control Btn in this.Controls)
            {
                if (Btn is Button && (String)Btn.Tag == "buton")
                {
                    Btn.Click += new EventHandler(BTNClick);
                }
            }
            BallPB.SetBounds(button5.Location.X - button5.Width / 4, button5.Location.Y - button5.Height / 4, BallPB.Width, BallPB.Height);
            Rand.Text = $"Red's Turn";
            servicii = new Servicii(this, BallPB.Width, BallPB.Height, button5.Width, button5.Height, button5.Left - button4.Left, button5.Top - button14.Top, BallPB);
            servicii.CallTheBorder();
        }

        private void BTNClick(object sender, EventArgs e)
        {
            Button cb = (Button)sender;
            int x, y;
            x = cb.Location.X;
            y = cb.Location.Y;
            int a, b;
            a = BallPB.Location.X;
            b = BallPB.Location.Y;

            BallPB.SetBounds(cb.Location.X - button5.Width / 4, cb.Location.Y - button5.Height / 4, BallPB.Width, BallPB.Height);
            intersects = false;

            foreach (Control pb in this.Controls)
            {
                if (pb is PictureBox && (string)pb.Tag == "intersectie")
                {
                    if (pb.Bounds.IntersectsWith(BallPB.Bounds))
                    {
                        intersects = true;
                        break;
                    }
                }
            }
            if (x < a - button5.Width / 4 && y - button5.Height / 4 == b)
            {
                //stanga
                servicii.LinieStanga(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y == b + button5.Height / 4 && x > a + button5.Width / 4)
            {
                //dreapta
                servicii.LinieDreapta(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y - button5.Height / 4 > b && x == a + button5.Width / 4)
            {
                //jos
                servicii.LinieJos(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y - button5.Height / 4 < b && x - button5.Width / 4 == a)
            {
                //sus
                servicii.LinieSus(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - button5.Width / 4 > a && y < b + button5.Height / 4)
            {
                //dreapta sus
                servicii.LinieDreaptaSus(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - button5.Width / 4 > a && y > b + button5.Height / 4)
            {
                //dreapta jos
                servicii.LinieDreaptaJos(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - button5.Width / 4 < a && y < b + button5.Height / 4)
            {
                //stanga sus
                servicii.LinieStangaSus(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - button5.Width / 4 < a && y > b + button5.Height / 4)
            {
                //stanga jos
                servicii.LinieStangaJos(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled == true && (BallPB.Bounds.Y == 698 - button5.Height / 4 || BallPB.Bounds.Y == 26 - button5.Height / 4))
            {
                GolLabel.Visible = true;
                if (BallPB.Bounds.Y < poartaBlue.Bounds.Y)
                {
                    GolLabel.ForeColor = Color.Red;
                }
                else
                {
                    GolLabel.ForeColor = Color.Blue;
                }
                Rand.Visible = false;
                AllBallsCB.Visible = false;
                foreach (Control Btn in this.Controls)
                {
                    if (Btn is Button && (String)Btn.Tag == "buton")
                    {
                        Btn.Visible = false;
                    }
                }
            }


            if (AllBallsCB.Checked == true && GolLabel.Visible == false)
            {
                servicii.ToateButoanele();
                foreach (Control Btn in this.Controls)
                {
                    if (Btn is Button && (String)Btn.Tag == "buton")
                    {
                        Btn.Enabled = false;
                    }
                }
            }
            else
            {
                if (GolLabel.Visible == false)
                {
                    servicii.Mutare();
                    foreach (Control Btn in this.Controls)
                    {
                        if (Btn is Button && (String)Btn.Tag == "buton")
                        {
                            Btn.Enabled = true;
                        }
                    }
                }
            }

            if (timer1.Enabled == true && AllBallsCB.Checked == false && GolLabel.Visible == false)
            {
                available = false;
                foreach (Control Btn in this.Controls)
                {
                    if (Btn is Button && (String)Btn.Tag == "buton")
                    {
                        if (Btn.Visible)
                        {
                            available = true;
                            break;
                        }
                    }
                }
                if (available == false)
                {
                    //Turn();
                    if (Rand.Text.ToLower() == "red's turn")
                    {
                        GolLabel.Text = "Blue Won";
                        GolLabel.BackColor = Color.Blue;
                    }
                    else
                    {
                        GolLabel.Text = "Red Won";
                        GolLabel.BackColor = Color.Red;
                    }
                    Rand.Visible = false;
                    GolLabel.ForeColor = Color.White;
                    GolLabel.Visible = true;
                    timer1.Stop();
                }
            }
        }

        public void Restart()
        {
            List<Control> deSters = new List<Control>();
            foreach (Control label in this.Controls)
            {
                if (label is Label && (string)label.Tag == "linieOrizontala")
                {
                    deSters.Add(label);
                }
                if (label is Label && (string)label.Tag == "linieVerticala")
                {
                    deSters.Add(label);
                }
                if (label is PictureBox && (string)label.Tag == "centruJS")
                {
                    deSters.Add(label);
                }
                if (label is PictureBox && (string)label.Tag == "centruSJ")
                {
                    deSters.Add(label);
                }
                if (label is PictureBox && (string)label.Tag == "intersectie")
                {
                    deSters.Add(label);
                }
                this.Invalidate();
            }
            foreach (Control desters in deSters)
            {
                this.Controls.Remove(desters);
                desters.Dispose();
            }
            servicii.CallTheBorder();
            AllBallsCB.Visible = true;
            GolLabel.BackColor = Color.Transparent;
            GolLabel.Text = "GOOOOOOOOOOOOL!";
            Rand.Visible = true;
            BallPB.SetBounds(button5.Location.X - button5.Width / 4, button5.Location.Y - button5.Height / 4, BallPB.Width, BallPB.Height);
            timer1.Start();
        }

        private void Restart_btn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            GolLabel.Visible = false;
            Restart();
        }



    }
}