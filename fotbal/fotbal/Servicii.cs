using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace fotbal
{
    public class Servicii
    {
        private Control form;
        private Control ball;
        private int ballWidth;
        private int ballHeight;
        private int btnWidth;
        private int btnHeight;
        private int btnXdistance;
        private int btnYdistance;

        public Servicii(Control form, int ballWidth, int ballHeight, int btnWidth, int btnHeight, int btnXdistance, int btnYdistance, Control ball)
        {
            this.form = form;
            this.ballWidth = ballWidth;
            this.ballHeight = ballHeight;
            this.btnWidth = btnWidth;
            this.btnHeight = btnHeight;
            this.btnXdistance = btnXdistance;
            this.btnYdistance = btnYdistance;
            this.ball = ball;
        }

        public void CallTheBorder()
        {
            IsThereALine(484 + ballWidth, 82 + ballHeight);
            IsThereALine(484 + ballWidth, 138 + ballHeight);
            IsThereALine(484 + ballWidth, 194 + ballHeight);
            IsThereALine(484 + ballWidth, 250 + ballHeight);
            IsThereALine(484 + ballWidth, 306 + ballHeight);
            IsThereALine(484 + ballWidth, 362 + ballHeight);
            IsThereALine(484 + ballWidth, 418 + ballHeight);
            IsThereALine(484 + ballWidth, 474 + ballHeight);
            IsThereALine(484 + ballWidth, 530 + ballHeight);
            IsThereALine(484 + ballWidth, 586 + ballHeight);
            IsThereALine(484 + ballWidth, 642 + ballHeight);

            IsThereALine(20 + ballWidth, 82 + ballHeight);
            IsThereALine(20 + ballWidth, 138 + ballHeight);
            IsThereALine(20 + ballWidth, 194 + ballHeight);
            IsThereALine(20 + ballWidth, 250 + ballHeight);
            IsThereALine(20 + ballWidth, 306 + ballHeight);
            IsThereALine(20 + ballWidth, 362 + ballHeight);
            IsThereALine(20 + ballWidth, 418 + ballHeight);
            IsThereALine(20 + ballWidth, 474 + ballHeight);
            IsThereALine(20 + ballWidth, 530 + ballHeight);
            IsThereALine(20 + ballWidth, 586 + ballHeight);
            IsThereALine(20 + ballWidth, 642 + ballHeight);
            IsThereALine(78 + ballWidth, 82 + ballHeight);
            IsThereALine(136 + ballWidth, 82 + ballHeight);
            IsThereALine(194 + ballWidth, 82 + ballHeight);
            IsThereALine(310 + ballWidth, 82 + ballHeight);
            IsThereALine(368 + ballWidth, 82 + ballHeight);
            IsThereALine(426 + ballWidth, 82 + ballHeight);
            IsThereALine(78 + ballWidth, 642 + ballHeight);
            IsThereALine(136 + ballWidth, 642 + ballHeight);
            IsThereALine(194 + ballWidth, 642 + ballHeight);
            IsThereALine(310 + ballWidth, 642 + ballHeight);
            IsThereALine(368 + ballWidth, 642 + ballHeight);
            IsThereALine(426 + ballWidth, 642 + ballHeight);
        }
        public void IsThereALine(int x, int y)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.SetBounds(x - ballWidth / 2 - 5, y - ballHeight / 2 - 5, 10, 10);
            pictureBox.BackColor = Color.Magenta;
            pictureBox.Tag = "intersectie";
            pictureBox.Visible = false;
            form.Controls.Add(pictureBox);
            pictureBox.BringToFront();
        }
        public string IsPointBetweenSJ(int x, int y, int x1, int y1, int x2, int y2)
        {
            if (x > x1 && x < x2 && y > y1 && y < y2) { return "dreapta"; }
            else if (x > x2 && x < x1 && y > y2 && y < y1) { return "stanga"; }
            else { return "false"; }
        }
        public string IsPointBetweenJS(int x, int y, int x1, int y1, int x2, int y2)
        {
            if (x > x1 && x < x2 && y > y2 && y < y1) { return "dreapta"; }
            else if (x < x1 && x > x2 && y > y1 && y < y2) { return "stanga"; }
            else { return "false"; }
        }
        public bool SeIntersecteazaVertical(Control player, Control button)
        {
            int midX = (button.Right + button.Left) / 2;
            int midY = (player.Top + button.Bottom) / 2;

            foreach (Control label in form.Controls)
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

        public bool SeIntersecteazaOrizontal(Control player, Control button)
        {
            int midX = (player.Right + button.Left) / 2;
            int midY = (player.Top + player.Bottom) / 2;

            foreach (Control label in form.Controls)
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



        public string SeIntersecteazaOblicJS(Control player, Control button)
        {
            foreach (Control pb in form.Controls)
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


        public string SeIntersecteazaOblicSJ(Control player, Control button)
        {
            foreach (Control pb in form.Controls)
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





        public void LinieStanga(int x, int y, int a, int b)
        {
            if (x >= 180 && x <= 215 && (y >= 620 || y <= 90))
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4 ;
                linie.SetBounds(x + btnWidth / 2 - linie.Height / 2+linie.Height/4 +linie.Height/4 + linie.Height / 8, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else if (x >= 237 && x <= 265 && (y >= 620 || y <= 90))
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4-linie.Height/4;
                linie.SetBounds(x + btnWidth / 2 -linie.Height/2 , y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else if (x > 50 && x < 400)
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4;
                linie.SetBounds(x + btnWidth / 2 - linie.Height / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else if (x < 40)
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4;
                linie.SetBounds(x + btnWidth / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4 - linie.Height / 4;
                linie.SetBounds(x + btnWidth / 2 - linie.Height * 2 / 4, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            IsThereALine(a + ballWidth, b + ballHeight);


        }
        public void LinieDreapta(int x, int y, int a, int b)
        {
            if (x >= 237 && x <= 265 && (y >= 620 || y <= 90))
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 8*2;
                linie.SetBounds(x - linie.Width / 2 - btnWidth - linie.Height / 4 *2-linie.Height/8*2+ linie.Height, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else if (x >= 262 && x <= 320 && (y >= 620 || y <= 90 ))
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance +btnWidth/8;
                linie.SetBounds(x - linie.Width / 2 - btnWidth - linie.Height / 4-linie.Height/8  , y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else if (x > 100 && x < 450)
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4;
                linie.SetBounds(x - linie.Width / 2 - btnWidth - linie.Height / 4 + linie.Height / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else if (x < 100)
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4 - linie.Height / 8;
                linie.SetBounds(x - linie.Width / 2 - btnWidth - linie.Height / 8 + linie.Height / 2, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Height = btnHeight / 2;
                linie.Width = btnXdistance + btnWidth / 4 - linie.Height / 2;
                linie.SetBounds(x - linie.Width / 2 - btnWidth - linie.Height / 8, y + linie.Height / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieOrizontala";
                form.Controls.Add(linie);
            }
            IsThereALine(a + ballWidth, b + ballHeight);

        }
        public void LinieSus(int x, int y, int a, int b)
        {
            if (y > 115 && y < 550)
            {
                Label linie = new();
                linie.Width = btnHeight / 2;
                linie.Height = btnYdistance + btnHeight / 4 + linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y + btnHeight / 2 - linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                form.Controls.Add(linie);
            }
            else if (y < 105)
            {
                Label linie = new();
                linie.Width = btnHeight / 2;
                linie.Height = btnYdistance + btnHeight / 4 - linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y + btnHeight / 2 + linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                form.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Width = btnHeight / 2;
                linie.Height = btnYdistance + btnHeight / 4 - linie.Width / 2 + linie.Width / 8;
                linie.SetBounds(x + linie.Width / 2, y + btnHeight / 2 - linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                form.Controls.Add(linie);
            }
            IsThereALine(a + ballWidth, b + ballHeight);

        }
        public void LinieJos(int x, int y, int a, int b)
        {
            if (y > 160 && y < 620)
            {
                Label linie = new();
                linie.Width = btnHeight / 2;
                linie.Height = btnYdistance + btnHeight / 4 + linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y - linie.Height + btnHeight / 2 + linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                form.Controls.Add(linie);
            }
            else if (y < 150)
            {
                Label linie = new();
                linie.Width = btnHeight / 2;
                linie.Height = btnYdistance + btnHeight / 4 - linie.Width / 2;
                linie.SetBounds(x + linie.Width / 2, y - linie.Height + btnHeight / 2 + linie.Width / 2, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                form.Controls.Add(linie);
            }
            else
            {
                Label linie = new();
                linie.Width = btnHeight / 2;
                linie.Height = btnYdistance + btnHeight / 4 - linie.Width / 2 + linie.Width / 8;
                linie.SetBounds(x + linie.Width / 2, y - linie.Height + btnHeight / 2 - linie.Width / 2 + linie.Width / 8, linie.Width, linie.Height);
                linie.BackColor = Color.Black;
                linie.Visible = true;
                linie.Tag = "linieVerticala";
                form.Controls.Add(linie);
            }
            IsThereALine(a + ballWidth, b + ballHeight);


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

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + btnWidth / 4, (y + b) / 2 - bmp.Height / 2 + btnHeight, bmp.Width, bmp.Height);
            using (Graphics g = form.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + btnWidth / 4, (y + b) / 2 + btnHeight / 2);
            pictureBox.Tag = "centruJS";
            pictureBox.Visible = false;

            form.Controls.Add(pictureBox);
            IsThereALine(a + ballWidth, b + ballHeight);

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

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + btnWidth, (y + b) / 2 - bmp.Height / 2 + btnHeight, bmp.Width, bmp.Height);
            using (Graphics g = form.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + btnWidth / 4, (y + b) / 2 + btnHeight / 2);
            pictureBox.Tag = "centruSJ";
            pictureBox.Visible = false;

            form.Controls.Add(pictureBox);
            IsThereALine(a + ballWidth, b + ballHeight);

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

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + btnWidth, (y + b) / 2 - bmp.Height / 2 + btnHeight, bmp.Width, bmp.Height);
            using (Graphics g = form.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + btnWidth / 4, (y + b) / 2 + btnHeight / 2);
            pictureBox.Tag = "centruSJ";
            pictureBox.Visible = false;

            form.Controls.Add(pictureBox);
            IsThereALine(a + ballWidth, b + ballHeight);

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

            Rectangle targetRect = new Rectangle((x + a) / 2 - bmp.Width / 2 + btnWidth / 4, (y + b) / 2 - bmp.Height / 2 + btnHeight, bmp.Width, bmp.Height);
            using (Graphics g = form.CreateGraphics())
            {
                g.DrawImage(bmp, targetRect);
            }

            bmp.Dispose();
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Blue;
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = new Point((x + a) / 2 + btnWidth / 4, (y + b) / 2 + btnHeight / 2);
            pictureBox.Tag = "centruJS";
            pictureBox.Visible = false;
            form.Controls.Add(pictureBox);

            IsThereALine(a + ballWidth, b + ballHeight);
        }

        public void ToateButoanele()
        {
            foreach (Control Btn in form.Controls)
            {
                if (Btn is Button && (String)Btn.Tag == "buton")
                {
                    Btn.Visible = true;
                }
            }
        }

        public void Mutare()
        {

            foreach (Control Btn in form.Controls)
            {
                ///////stanga-dreapta
                if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.X > ball.Bounds.X - 70) && (Btn.Bounds.X < ball.Bounds.X) &&
                    Btn.Bounds.Y == ball.Bounds.Y + btnHeight / 4 &&
                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
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
                        Btn.Visible = !SeIntersecteazaOrizontal(ball, Btn);

                    }





                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.X < ball.Bounds.X + 70) && (Btn.Bounds.X > ball.Bounds.X) &&
                    Btn.Bounds.Y == ball.Bounds.Y + btnHeight / 4 &&
                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
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
                        Btn.Visible = !SeIntersecteazaOrizontal(ball, Btn);

                    }
                }
                //////////sus-jos
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y < ball.Bounds.Y + 70) && (Btn.Bounds.Y > ball.Bounds.Y) &&
                    Btn.Bounds.X == ball.Bounds.X + btnWidth / 4 &&
                    (ball.Bounds.X != 20 - btnWidth / 4) &&
                    (ball.Bounds.X != 484 - btnWidth / 4) &&
                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
                {
                    if ((Btn.Bounds.X == 310 || Btn.Bounds.X == 194) && ball.Bounds.Y == 642 - btnHeight / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        Btn.Visible = !SeIntersecteazaVertical(ball, Btn);
                    }
                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y > ball.Bounds.Y - 70) && (Btn.Bounds.Y < ball.Bounds.Y) &&
                    Btn.Bounds.X == ball.Bounds.X + btnWidth / 4 &&
                    (ball.Bounds.X != 20 - btnWidth / 4) &&
                    (ball.Bounds.X != 484 - btnWidth / 4) &&
                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
                {
                    if ((Btn.Bounds.X == 310 || Btn.Bounds.X == 194) && ball.Bounds.Y == 82 - btnHeight / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        Btn.Visible = !SeIntersecteazaVertical(ball, Btn);
                    }
                }
                ///////diagonala stanga sus-jos
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y > ball.Bounds.Y - 70) && (Btn.Bounds.Y < ball.Bounds.Y - 20) &&
                    (Btn.Bounds.X > ball.Bounds.X - 70) && (Btn.Bounds.X < ball.Bounds.X - 20) &&
                    (ball.Bounds.X != 20 - btnWidth / 4) &&
                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
                {
                    if (Btn.Bounds.X == 310 && ball.Bounds.X == 368 - btnWidth / 4 && ball.Bounds.Y == 82 - btnHeight / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {

                        if (SeIntersecteazaOblicSJ(ball, Btn) == "stanga")
                        {
                            Btn.Visible = false;
                        }
                        else { Btn.Visible = true; }
                    }
                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y < ball.Bounds.Y + 70) && (Btn.Bounds.Y > ball.Bounds.Y + 20) &&
                    (Btn.Bounds.X > ball.Bounds.X - 70) && (Btn.Bounds.X < ball.Bounds.X - 20) &&
                    (ball.Bounds.X != 20 - btnWidth / 4) &&
                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
                {
                    if (Btn.Bounds.X == 310 && ball.Bounds.X == 368 - btnWidth / 4 && ball.Bounds.Y == 642 - btnHeight / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {
                        if (SeIntersecteazaOblicJS(ball, Btn) == "stanga")
                        {
                            Btn.Visible = false;
                        }
                        else { Btn.Visible = true; }


                    }
                }
                /////////diagonala dreapta sus-jos
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y > ball.Bounds.Y - 70) && (Btn.Bounds.Y < ball.Bounds.Y - 20) &&
                    (Btn.Bounds.X < ball.Bounds.X + 70) && (Btn.Bounds.X > ball.Bounds.X + 20) &&
                    (ball.Bounds.X != 484 - btnWidth / 4) &&
                    (ball.Location.X != Btn.Location.X) && (ball.Location.Y != Btn.Location.Y) &&
                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
                {
                    if (Btn.Bounds.X == 194 && ball.Bounds.X == 136 - btnWidth / 4 && ball.Bounds.Y == 82 - btnHeight / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {

                        if (SeIntersecteazaOblicJS(ball, Btn) == "dreapta")
                        {
                            Btn.Visible = false;
                        }
                        else { Btn.Visible = true; }

                    }
                }
                else if (Btn is Button && (String)Btn.Tag == "buton" &&
                    (Btn.Bounds.Y < ball.Bounds.Y + 70) && (Btn.Bounds.Y > ball.Bounds.Y + 20) &&
                    (Btn.Bounds.X < ball.Bounds.X + 70) && (Btn.Bounds.X > ball.Bounds.X + 20) &&
                    (ball.Bounds.X != 484 - btnWidth / 4) &&

                    Btn.Bounds.IntersectsWith(ball.Bounds) == false)
                {
                    if (Btn.Bounds.X == 194 && ball.Bounds.X == 136 - btnWidth / 4 && ball.Bounds.Y == 642 - btnHeight / 4)
                    {
                        Btn.Visible = false;
                    }
                    else
                    {

                        if (SeIntersecteazaOblicSJ(ball, Btn) == "dreapta")
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

        public void Turn(Control Rand)
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













    }



}
