
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace fotbal
{
    public partial class Form1 : Form
    {
        private bool intersects = false;
        private bool available = true;
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
            CallTheBorder();
        }

        public void Mutare()
        {

            foreach (Control Btn in this.Controls)
            {
                ///////stanga-dreapta
                if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.X > BallPB.Bounds.X - 70) && (Btn.Bounds.X < BallPB.Bounds.X) &&
                    Btn.Bounds.Y == BallPB.Bounds.Y + button5.Height / 4 &&
                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {


                    if ((Btn.Bounds.X < 194 || Btn.Bounds.X > 252) && Btn.Bounds.Y == 82)
                    {
                        Btn.Visible = false;
                    }
                    else if ((Btn.Bounds.X < 194 || Btn.Bounds.X > 252) && Btn.Bounds.Y == 642)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        Btn.Visible = !SeIntersecteazaOrizontal(BallPB, Btn);

                    }





                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.X < BallPB.Bounds.X + 70) && (Btn.Bounds.X > BallPB.Bounds.X) &&
                    Btn.Bounds.Y == BallPB.Bounds.Y + button5.Height / 4 &&
                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {
                    if ((Btn.Bounds.X < 252 || Btn.Bounds.X > 310) && Btn.Bounds.Y == 82)
                    {
                        Btn.Visible = false;
                    }
                    else if ((Btn.Bounds.X < 252 || Btn.Bounds.X > 310) && Btn.Bounds.Y == 642)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        Btn.Visible = !SeIntersecteazaOrizontal(BallPB, Btn);

                    }
                }
                //////////sus-jos
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y < BallPB.Bounds.Y + 70) && (Btn.Bounds.Y > BallPB.Bounds.Y) &&
                    Btn.Bounds.X == BallPB.Bounds.X + button5.Width / 4 &&
                    (BallPB.Bounds.X != 20 - button5.Width / 4) &&
                    (BallPB.Bounds.X != 484 - button5.Width / 4) &&
                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {
                    if ((Btn.Bounds.X == 310 || Btn.Bounds.X == 194) && BallPB.Bounds.Y == 642 - button5.Height / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        Btn.Visible = !SeIntersecteazaVertical(BallPB, Btn);
                    }
                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y > BallPB.Bounds.Y - 70) && (Btn.Bounds.Y < BallPB.Bounds.Y) &&
                    Btn.Bounds.X == BallPB.Bounds.X + button5.Width / 4 &&
                    (BallPB.Bounds.X != 20 - button5.Width / 4) &&
                    (BallPB.Bounds.X != 484 - button5.Width / 4) &&
                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {
                    if ((Btn.Bounds.X == 310 || Btn.Bounds.X == 194) && BallPB.Bounds.Y == 82 - button5.Height / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        Btn.Visible = !SeIntersecteazaVertical(BallPB, Btn);
                    }
                }
                ///////diagonala stanga sus-jos
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y > BallPB.Bounds.Y - 70) && (Btn.Bounds.Y < BallPB.Bounds.Y - 20) &&
                    (Btn.Bounds.X > BallPB.Bounds.X - 70) && (Btn.Bounds.X < BallPB.Bounds.X - 20) &&
                    (BallPB.Bounds.X != 20 - button5.Width / 4) &&
                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {
                    if (Btn.Bounds.X == 310 && BallPB.Bounds.X == 368 - button5.Width / 4 && BallPB.Bounds.Y == 82 - button5.Height / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {

                        if (SeIntersecteazaOblicSJ(BallPB, Btn) == "stanga")
                        {
                            Btn.Visible = false;
                        }
                        else { Btn.Visible = true; }
                    }
                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y < BallPB.Bounds.Y + 70) && (Btn.Bounds.Y > BallPB.Bounds.Y + 20) &&
                    (Btn.Bounds.X > BallPB.Bounds.X - 70) && (Btn.Bounds.X < BallPB.Bounds.X - 20) &&
                    (BallPB.Bounds.X != 20 - button5.Width / 4) &&
                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {
                    if (Btn.Bounds.X == 310 && BallPB.Bounds.X == 368 - button5.Width / 4 && BallPB.Bounds.Y == 642 - button5.Height / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        if (SeIntersecteazaOblicJS(BallPB, Btn) == "stanga")
                        {
                            Btn.Visible = false;
                        }
                        else { Btn.Visible = true; }


                    }
                }
                /////////diagonala dreapta sus-jos
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y > BallPB.Bounds.Y - 70) && (Btn.Bounds.Y < BallPB.Bounds.Y - 20) &&
                    (Btn.Bounds.X < BallPB.Bounds.X + 70) && (Btn.Bounds.X > BallPB.Bounds.X + 20) &&
                    (BallPB.Bounds.X != 484 - button5.Width / 4) &&
                    (BallPB.Location.X != Btn.Location.X) && (BallPB.Location.Y != Btn.Location.Y) &&
                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {
                    if (Btn.Bounds.X == 194 && BallPB.Bounds.X == 136 - button5.Width / 4 && BallPB.Bounds.Y == 82 - button5.Height / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {

                        if (SeIntersecteazaOblicJS(BallPB, Btn) == "dreapta")
                        {
                            Btn.Visible = false;
                        }
                        else { Btn.Visible = true; }

                    }
                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y < BallPB.Bounds.Y + 70) && (Btn.Bounds.Y > BallPB.Bounds.Y + 20) &&
                    (Btn.Bounds.X < BallPB.Bounds.X + 70) && (Btn.Bounds.X > BallPB.Bounds.X + 20) &&
                    (BallPB.Bounds.X != 484 - button5.Width / 4) &&

                    Btn.Bounds.IntersectsWith(BallPB.Bounds) == false)
                {
                    if (Btn.Bounds.X == 194 && BallPB.Bounds.X == 136 - button5.Width / 4 && BallPB.Bounds.Y == 642 - button5.Height / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {

                        if (SeIntersecteazaOblicSJ(BallPB, Btn) == "dreapta")
                        {
                            Btn.Visible = false;
                        }
                        else { Btn.Visible = true; }

                    }
                }
                else if (Btn is Button && (String)Btn.Tag == "buton")
                {
                    Btn.Visible = false;
                }
            }
        }
        public void ToateButoanele()
        {
            foreach (Control Btn in this.Controls)
            {
                if (Btn is Button && (String)Btn.Tag == "buton")
                {
                    Btn.Visible = true;
                }
            }
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
            intersects = false; // Reset intersects to false before checking for intersections



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
                LinieStanga(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }


            }
            else if (y == b + button5.Height / 4 && x > a + button5.Width / 4)
            {
                //dreapta
                LinieDreapta(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }

            }
            else if (y - button5.Height / 4 > b && x == a + button5.Width / 4)
            {
                //jos
                LinieJos(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }
            }
            else if (y - button5.Height / 4 < b && x - button5.Width / 4 == a)
            {
                //sus
                LinieSus(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }
            }
            //////
            ///
            else if (x - button5.Width / 4 > a && y < b + button5.Height / 4)
            {
                //dreapta sus
                LinieDreaptaSus(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }
            }
            else if (x - button5.Width / 4 > a && y > b + button5.Height / 4)
            {
                //dreapta jos
                LinieDreaptaJos(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }
            }
            else if (x - button5.Width / 4 < a && y < b + button5.Height / 4)
            {
                //stanga sus
                LinieStangaSus(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }
            }
            else if (x - button5.Width / 4 < a && y > b + button5.Height / 4)
            {
                //stanga jos
                LinieStangaJos(x, y, a, b);
                if (!intersects)
                {
                    Turn();
                }
                else { }
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
                ToateButoanele();
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
                    Mutare();
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
                    if (Rand.Text.ToLower()=="red's turn")
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
                    GolLabel.ForeColor= Color.White;
                    GolLabel.Visible = true;
                    timer1.Stop();
                }

            }
        }

        public void LinieStanga(int x, int y, int a, int b)
        {
            if (x > 50 && x < 400)
            {
                Label linie = new();
                linie.Height = button5.Height / 2;
                linie.Width = button5.Left - button4.Left + button5.Width / 4;
                linie.SetBounds(x + button5.Width / 2 - linie.Height / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                this.Controls.Add(linie);
            }
            else if (x < 40)
            {
                Label linie = new();
                linie.Height = button5.Height / 2;
                linie.Width = button5.Left - button4.Left + button5.Width / 4;
                linie.SetBounds(x + button5.Width / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                this.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Height = button5.Height / 2;
                linie.Width = button5.Left - button4.Left + button5.Width / 4 - linie.Height / 4;
                linie.SetBounds(x + button5.Width / 2 - linie.Height * 2 / 4, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                this.Controls.Add(linie);
            }
            IsThereALine(a + BallPB.Width, b + BallPB.Height);


        }
        public void LinieDreapta(int x, int y, int a, int b)
        {
            if (x > 100 && x < 450)
            {
                Label linie = new();
                linie.Height = button5.Height / 2;
                linie.Width = button5.Left - button4.Left + button5.Width / 4;
                linie.SetBounds(x - linie.Width / 2 - button5.Width - linie.Height / 4 + linie.Height / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                this.Controls.Add(linie);
            }
            else if (x < 100)
            {
                Label linie = new();
                linie.Height = button5.Height / 2;
                linie.Width = button5.Left - button4.Left + button5.Width / 4 - linie.Height / 8;
                linie.SetBounds(x - linie.Width / 2 - button5.Width - linie.Height / 8 + linie.Height / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                this.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Height = button5.Height / 2;
                linie.Width = button5.Left - button4.Left + button5.Width / 4 - linie.Height / 2;
                linie.SetBounds(x - linie.Width / 2 - button5.Width - linie.Height / 8, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                this.Controls.Add(linie);
            }
            IsThereALine(a + BallPB.Width, b + BallPB.Height);

        }
        public void LinieSus(int x, int y, int a, int b)
        {
            if (y > 115 && y < 550)
            {
                Label linie = new();
                linie.Width = button5.Height / 2;
                linie.Height = button5.Top - button14.Top + button5.Height / 4 + linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y + button5.Height / 2 - linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                this.Controls.Add(linie);
            }
            else if (y < 105)
            {
                Label linie = new();
                linie.Width = button5.Height / 2;
                linie.Height = button5.Top - button14.Top + button5.Height / 4 - linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y + button5.Height / 2 + linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                this.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Width = button5.Height / 2;
                linie.Height = button5.Top - button14.Top + button5.Height / 4 - linie.Width / 2 + linie.Width / 8;
                linie.SetBounds(x + linie.Width / 2, y + button5.Height / 2 - linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                this.Controls.Add(linie);
            }
            IsThereALine(a + BallPB.Width, b + BallPB.Height);

        }
        public void LinieJos(int x, int y, int a, int b)
        {
            if (y > 160 && y < 620)
            {
                Label linie = new();
                linie.Width = button5.Height / 2;
                linie.Height = button5.Top - button14.Top + button5.Height / 4 + linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y - linie.Height + button5.Height / 2 + linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                this.Controls.Add(linie);
            }
            else if (y < 150)
            {
                Label linie = new();
                linie.Width = button5.Height / 2;
                linie.Height = button5.Top - button14.Top + button5.Height / 4 - linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y - linie.Height + button5.Height / 2 + linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                this.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Width = button5.Height / 2;
                linie.Height = button5.Top - button14.Top + button5.Height / 4 - linie.Width / 2 + linie.Width / 8;
                linie.SetBounds(x + linie.Width / 2, y - linie.Height + button5.Height / 2 - linie.Width / 2 + linie.Width / 8, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                this.Controls.Add(linie);
            }
            IsThereALine(a + BallPB.Width, b + BallPB.Height);


        }
        public void LinieStangaJos(int x, int y, int a, int b)
        {
            int width = 90;
            int height = 10;
            int angleDegrees = -44; // Change the angle to rotate from bottom to top

            double angleRadians = angleDegrees * Math.PI / 180;
            double rotatedWidth = Math.Abs(width * Math.Cos(angleRadians)) + Math.Abs(height * Math.Sin(angleRadians));
            double rotatedHeight = Math.Abs(width * Math.Sin(angleRadians)) + Math.Abs(height * Math.Cos(angleRadians));

            int bmpWidth = (int)Math.Ceiling(rotatedWidth) * 2; // Adjust resolution as needed
            int bmpHeight = (int)Math.Ceiling(rotatedHeight) * 2; // Adjust resolution as needed

            Bitmap bmp = new Bitmap(bmpWidth, bmpHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                g.Clear(Color.Transparent);

                g.TranslateTransform((float)bmpWidth / 2, (float)bmpHeight / 2);

                g.RotateTransform((float)angleDegrees);

                g.TranslateTransform(-(float)width / 2, -(float)height / 2);

                Rectangle rect = new Rectangle(0, 0, width, height);

                using (SolidBrush brush = new SolidBrush(Color.Black)) // Change the color as needed
                {
                    g.FillRectangle(brush, rect);
                }


            }

            bmp.MakeTransparent(Color.Transparent);

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + button5.Width / 4, (y + b) / 2 - bmp.Height / 2 + button5.Height, bmp.Width, bmp.Height);
            using (Graphics g = this.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + button5.Width / 4, (y + b) / 2 + button5.Height / 2);
            pictureBox.Tag = "centruJS";
            pictureBox.Visible = false;

            this.Controls.Add(pictureBox);
            IsThereALine(a + BallPB.Width, b + BallPB.Height);

        }





        public void LinieStangaSus(int x, int y, int a, int b)//
        {
            int width = 90;
            int height = 10;
            int angleDegrees = 44; // Change the angle to rotate from bottom to top

            double angleRadians = angleDegrees * Math.PI / 180;
            double rotatedWidth = Math.Abs(width * Math.Cos(angleRadians)) + Math.Abs(height * Math.Sin(angleRadians));
            double rotatedHeight = Math.Abs(width * Math.Sin(angleRadians)) + Math.Abs(height * Math.Cos(angleRadians));

            int bmpWidth = (int)Math.Ceiling(rotatedWidth) * 2; // Adjust resolution as needed
            int bmpHeight = (int)Math.Ceiling(rotatedHeight) * 2; // Adjust resolution as needed

            Bitmap bmp = new Bitmap(bmpWidth, bmpHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                g.Clear(Color.Transparent);

                g.TranslateTransform((float)bmpWidth / 2, (float)bmpHeight / 2);

                g.RotateTransform((float)angleDegrees);

                g.TranslateTransform(-(float)width / 2, -(float)height / 2);

                Rectangle rect = new Rectangle(0, 0, width, height);

                using (SolidBrush brush = new SolidBrush(Color.Black)) // Change the color as needed
                {
                    g.FillRectangle(brush, rect);
                }
            }

            bmp.MakeTransparent(Color.Transparent);

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + button5.Width, (y + b) / 2 - bmp.Height / 2 + button5.Height, bmp.Width, bmp.Height);
            using (Graphics g = this.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + button5.Width / 4, (y + b) / 2 + button5.Height / 2);
            pictureBox.Tag = "centruSJ";
            pictureBox.Visible = false;

            this.Controls.Add(pictureBox);
            IsThereALine(a + BallPB.Width, b + BallPB.Height);

        }
        public void LinieDreaptaJos(int x, int y, int a, int b)//
        {
            int width = 90;
            int height = 10;
            int angleDegrees = 44; // Change the angle to rotate from bottom to top

            double angleRadians = angleDegrees * Math.PI / 180;
            double rotatedWidth = Math.Abs(width * Math.Cos(angleRadians)) + Math.Abs(height * Math.Sin(angleRadians));
            double rotatedHeight = Math.Abs(width * Math.Sin(angleRadians)) + Math.Abs(height * Math.Cos(angleRadians));

            int bmpWidth = (int)Math.Ceiling(rotatedWidth) * 2; // Adjust resolution as needed
            int bmpHeight = (int)Math.Ceiling(rotatedHeight) * 2; // Adjust resolution as needed

            Bitmap bmp = new Bitmap(bmpWidth, bmpHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                g.Clear(Color.Transparent);

                g.TranslateTransform((float)bmpWidth / 2, (float)bmpHeight / 2);

                g.RotateTransform((float)angleDegrees);

                g.TranslateTransform(-(float)width / 2, -(float)height / 2);

                Rectangle rect = new Rectangle(0, 0, width, height);

                using (SolidBrush brush = new SolidBrush(Color.Black)) // Change the color as needed
                {
                    g.FillRectangle(brush, rect);
                }
            }

            bmp.MakeTransparent(Color.Transparent);

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + button5.Width, (y + b) / 2 - bmp.Height / 2 + button5.Height, bmp.Width, bmp.Height);
            using (Graphics g = this.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + button5.Width / 4, (y + b) / 2 + button5.Height / 2);
            pictureBox.Tag = "centruSJ";
            pictureBox.Visible = false;

            this.Controls.Add(pictureBox);
            IsThereALine(a + BallPB.Width, b + BallPB.Height);

        }
        public void LinieDreaptaSus(int x, int y, int a, int b)//
        {
            int width = 90;
            int height = 10;
            int angleDegrees = -44; // Change the angle to rotate from bottom to top

            double angleRadians = angleDegrees * Math.PI / 180;
            double rotatedWidth = Math.Abs(width * Math.Cos(angleRadians)) + Math.Abs(height * Math.Sin(angleRadians));
            double rotatedHeight = Math.Abs(width * Math.Sin(angleRadians)) + Math.Abs(height * Math.Cos(angleRadians));

            int bmpWidth = (int)Math.Ceiling(rotatedWidth) * 2; // Adjust resolution as needed
            int bmpHeight = (int)Math.Ceiling(rotatedHeight) * 2; // Adjust resolution as needed

            Bitmap bmp = new Bitmap(bmpWidth, bmpHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                g.Clear(Color.Transparent);

                g.TranslateTransform((float)bmpWidth / 2, (float)bmpHeight / 2);

                g.RotateTransform((float)angleDegrees);

                g.TranslateTransform(-(float)width / 2, -(float)height / 2);

                Rectangle rect = new Rectangle(0, 0, width, height);

                using (SolidBrush brush = new SolidBrush(Color.Black)) // Change the color as needed
                {
                    g.FillRectangle(brush, rect);
                }
            }

            bmp.MakeTransparent(Color.Transparent);

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + button5.Width / 4, (y + b) / 2 - bmp.Height / 2 + button5.Height, bmp.Width, bmp.Height);
            using (Graphics g = this.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + button5.Width / 4, (y + b) / 2 + button5.Height / 2);
            pictureBox.Tag = "centruJS";
            pictureBox.Visible = false;
            this.Controls.Add(pictureBox);

            IsThereALine(a + BallPB.Width, b + BallPB.Height);
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
            CallTheBorder();
            AllBallsCB.Visible = true;
            GolLabel.BackColor = Color.Transparent;
            GolLabel.Text = "GOOOOOOOOOOOOL!";
            Rand.Visible = true;
            BallPB.SetBounds(button5.Location.X - button5.Width / 4, button5.Location.Y - button5.Height / 4, BallPB.Width, BallPB.Height);
            timer1.Start();

        }

        private void restart_btn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            GolLabel.Visible = false;
            Restart();
        }
        public bool SeIntersecteazaOrizontal(Control player, Control button)
        {
            int midX = (player.Right + button.Left) / 2;
            int midY = (player.Top + player.Bottom) / 2;

            foreach (Control label in this.Controls)
            {
                if (label is Label && (string)label.Tag == "linieOrizontala")
                {
                    if (label.Bounds.Contains(midX, midY))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool SeIntersecteazaVertical(Control player, Control button)
        {
            int midX = (button.Right + button.Left) / 2;
            int midY = (player.Top + button.Bottom) / 2;

            foreach (Control label in this.Controls)
            {
                if (label is Label && (string)label.Tag == "linieVerticala")
                {
                    if (label.Bounds.Contains(midX, midY))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string SeIntersecteazaOblicJS(Control player, Control button)
        {
            foreach (Control pb in this.Controls)
            {
                if (pb is PictureBox && pb.Tag != null && pb.Tag.ToString() == "centruJS")
                {
                    // Calculate the center point of the PictureBox
                    int pbCenterX = pb.Left + pb.Width / 2;
                    int pbCenterY = pb.Top + pb.Height / 2;

                    // Check if the center point of the button lies between player and PictureBox
                    if (IsPointBetweenJS(pbCenterX, pbCenterY, player.Left, player.Top, button.Right, button.Bottom) == "dreapta")
                    {
                        return "dreapta";
                    }
                    else if (IsPointBetweenJS(pbCenterX, pbCenterY, player.Left, player.Top, button.Right, button.Bottom) == "stanga")
                    {
                        return "stanga";
                    }

                }
            }

            return "false";
        }

        private string IsPointBetweenJS(int x, int y, int x1, int y1, int x2, int y2)
        {
            if (x > x1 && x < x2 && y > y2 && y < y1) { return "dreapta"; }
            else if (x < x1 && x > x2 && y > y1 && y < y2) { return "stanga"; }
            else { return "false"; }
        }
        private string SeIntersecteazaOblicSJ(Control player, Control button)
        {
            foreach (Control pb in this.Controls)
            {
                if (pb is PictureBox && pb.Tag != null && pb.Tag.ToString() == "centruSJ")
                {
                    // Calculate the center point of the PictureBox
                    int pbCenterX = pb.Left + pb.Width / 2;
                    int pbCenterY = pb.Top + pb.Height / 2;

                    // Check if the center point of the button lies between player and PictureBox
                    if (IsPointBetweenSJ(pbCenterX, pbCenterY, player.Left, player.Top, button.Right, button.Bottom) == "dreapta")
                    {
                        return "dreapta";
                    }
                    else if (IsPointBetweenSJ(pbCenterX, pbCenterY, player.Left, player.Top, button.Right, button.Bottom) == "stanga")
                    {
                        return "stanga";
                    }
                }
            }

            return "false";
        }

        private string IsPointBetweenSJ(int x, int y, int x1, int y1, int x2, int y2)
        {
            if (x > x1 && x < x2 && y > y1 && y < y2) { return "dreapta"; }
            else if (x > x2 && x < x1 && y > y2 && y < y1) { return "stanga"; }
            else { return "false"; }
        }

        private void Turn()
        {
            if (Rand.Text.ToLower() == "red's turn")
            {
                Rand.Text = "Blue's Turn";
            }
            else if (Rand.Text.ToLower() == "blue's turn")
            {
                Rand.Text = "Red's Turn";
            }
            else { Rand.Text = "'s turn"; }
        }

        private void IsThereALine(int x, int y)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.SetBounds(x - BallPB.Width / 2 - 5, y - BallPB.Height / 2 - 5, 10, 10);
            pictureBox.BackColor = Color.Magenta;
            pictureBox.Tag = "intersectie";
            pictureBox.Visible = false;
            this.Controls.Add(pictureBox);
            pictureBox.BringToFront();
        }
        private void CallTheBorder()
        {
            IsThereALine(484 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 138 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 194 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 250 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 306 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 362 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 418 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 474 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 530 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 586 + BallPB.Height);
            IsThereALine(484 + BallPB.Width, 642 + BallPB.Height);

            IsThereALine(20 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 138 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 194 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 250 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 306 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 362 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 418 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 474 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 530 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 586 + BallPB.Height);
            IsThereALine(20 + BallPB.Width, 642 + BallPB.Height);
            IsThereALine(78 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(136 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(194 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(310 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(368 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(426 + BallPB.Width, 82 + BallPB.Height);
            IsThereALine(78 + BallPB.Width, 642 + BallPB.Height);
            IsThereALine(136 + BallPB.Width, 642 + BallPB.Height);
            IsThereALine(194 + BallPB.Width, 642 + BallPB.Height);
            IsThereALine(310 + BallPB.Width, 642 + BallPB.Height);
            IsThereALine(368 + BallPB.Width, 642 + BallPB.Height);
            IsThereALine(426 + BallPB.Width, 642 + BallPB.Height);
        }

    }
}