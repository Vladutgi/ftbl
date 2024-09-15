using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace fotbal
{
    public partial class Computer : Form
    {
        Servicii servicii;
        private bool available = true;
        private bool intersects = false;
        private System.Windows.Forms.Timer botTimer;
        Random random = new Random();
        private string selectedDifficulty = String.Empty;

        public Computer(string difficulty)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            timer1.Start();
            //AddButtons();
            DrawCustomButton();
            BallPB.SetBounds(247, 357, BallPB.Width, BallPB.Height);
            Rand.Text = $"Red's turn";
            Rand.TextChanged += new EventHandler(Rand_TextChanged);
            servicii = new Servicii(this, BallPB.Width, BallPB.Height, 20, 20, 58, 56, BallPB);
            servicii.CallTheBorder();

            botTimer = new System.Windows.Forms.Timer();
            botTimer.Interval = 1000;
            botTimer.Tick += BotTimer_Tick;
            selectedDifficulty = difficulty;
            AllBallsCB.CheckedChanged += SchimbaVizibilitatea;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled == true && (BallPB.Bounds.Y == 698 - 20 / 4 || BallPB.Bounds.Y == 26 - 20 / 4))
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
                    if (Btn is CustomButton && (String)Btn.Tag == "buton")
                    {
                        Btn.Visible = false;
                    }
                }
            }


            if (AllBallsCB.Checked == true && GolLabel.Visible == false)
            {
                botTimer.Enabled = false;
                servicii.ToateButoanele();
                foreach (Control Btn in this.Controls)
                {
                    if (Btn is CustomButton && (String)Btn.Tag == "buton")
                    {
                        Btn.BackColor = Color.Green;
                        Btn.Enabled = false;
                    }
                }
            }
            else
            {
                if (GolLabel.Visible == false && AllBallsCB.Checked == false)
                {
                    ShowTheButtons();
                    //servicii.Mutare();
                    if (Rand.Text.ToLower() == "red's turn")
                    {
                        EnableButtons();
                    }
                }
            }

            if (timer1.Enabled == true && AllBallsCB.Checked == false && GolLabel.Visible == false)
            {
                available = false;



                if (DirectionsAvailable(ButtonBehindThePlayer()) == false)
                {
                    available = false;                           // MessageBox.Show("Test");

                }
                else
                {

                    ///MessageBox.Show("Testeeeeeeeee");
                    available = true;
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
                    botTimer.Stop();
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
                if (label is CustomButton && (string)label.Tag == "buton")
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
            DrawCustomButton();
            AllBallsCB.Visible = true;
            GolLabel.BackColor = Color.Transparent;
            GolLabel.Text = "GOOOOOOOOOOOOL!";
            Rand.Visible = true;
            BallPB.SetBounds(252 - 20 / 4, 362 - 20 / 4, BallPB.Width, BallPB.Height);
            timer1.Start();
            Rand.Text = "Red's Turn";
        }

        private void Restart_btn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            GolLabel.Visible = false;
            Restart();
        }
        private void BTNClick(object sender, EventArgs e)
        {
            CustomButton cb = (CustomButton)sender;
            int x, y;
            x = cb.Location.X;
            y = cb.Location.Y;
            int a, b;
            a = BallPB.Location.X;
            b = BallPB.Location.Y;

            BallPB.SetBounds(cb.Location.X - 20 / 4, cb.Location.Y - 20 / 4, BallPB.Width, BallPB.Height);
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
            if (x < a - 20 / 4 && y - 20 / 4 == b)
            {
                //stanga
                servicii.LinieStanga(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y == b + 20 / 4 && x > a + 20 / 4)
            {
                //dreapta
                servicii.LinieDreapta(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y - 20 / 4 > b && x == a + 20 / 4)
            {
                //jos
                servicii.LinieJos(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y - 20 / 4 < b && x - 20 / 4 == a)
            {
                //sus
                servicii.LinieSus(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 > a && y < b + 20 / 4)
            {
                //dreapta sus
                servicii.LinieDreaptaSus(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 > a && y > b + 20 / 4)
            {
                //dreapta jos
                servicii.LinieDreaptaJos(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 < a && y < b + 20 / 4)
            {
                //stanga sus
                servicii.LinieStangaSus(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 < a && y > b + 20 / 4)
            {
                //stanga jos
                servicii.LinieStangaJos(x, y, a, b);
                if (!intersects) { servicii.Turn(Rand); }
            }
            foreach (String item in servicii.Intersections(BallPB, cb))
            {
                if (item != String.Empty)
                {
                    MessageBox.Show(item.ToString());

                }
            }

        }
        private void BotMove()
        {

            List<CustomButton> closestBTNs = new List<CustomButton>();
            double distantaMinima = double.MaxValue;
            int goalX = poartaRed.Location.X;
            int goalY = poartaRed.Location.Y;

            CustomButton botButton = ButtonBehindThePlayer();
            int botX = botButton.Location.X;
            int botY = botButton.Location.Y;

            double botDistanceToGoal = Math.Sqrt(Math.Pow(botX - goalX, 2) + Math.Pow(botY - goalY, 2));

            List<CustomButton> validFallbackButtons = new List<CustomButton>();

            foreach (Control control in this.Controls)
            {
                if (control is CustomButton button && (string)button.Tag == "buton" && control.Visible == true)
                {
                    int btnXLocation = button.Location.X;
                    int btnYLocation = button.Location.Y;

                    double distanta = Math.Sqrt(Math.Pow(btnXLocation - goalX, 2) + Math.Pow(btnYLocation - goalY, 2));

                    validFallbackButtons.Add(button);

                    if (distanta < botDistanceToGoal)
                    {
                        if (distanta < distantaMinima)
                        {
                            distantaMinima = distanta;
                            closestBTNs.Clear();
                            closestBTNs.Add(button);
                        }
                        else if (distanta == distantaMinima)
                        {
                            closestBTNs.Add(button);
                        }
                    }
                }
            }
            if (closestBTNs.Count > 0)
            {
                CustomButton selectedButton = closestBTNs[random.Next(closestBTNs.Count)];
                CustomBTNClick(selectedButton, EventArgs.Empty);
            }
            else if (validFallbackButtons.Count > 0)
            {
                CustomButton fallbackButton = validFallbackButtons[random.Next(validFallbackButtons.Count)];
                CustomBTNClick(fallbackButton, EventArgs.Empty);
            }
        }

        private void Rand_TextChanged(object sender, EventArgs e)
        {
            if (Rand.Text.ToLower() == "blue's turn")
            {
                DisableButtons();
                botTimer.Start();
            }
            else
            {
                botTimer.Stop();
                EnableButtons();
            }
        }


        private void BotTimer_Tick(object sender, EventArgs e)
        {
            if (Rand.Text.ToLower() == "blue's turn")
            {

                if (selectedDifficulty == "easy")
                {
                    BotMove();

                }
                else if (selectedDifficulty == "medium")
                {
                    MediumBotMove();

                }
                else if (selectedDifficulty == "hard")
                {
                    BotMoveBFS();
                }
            }
            else
            {
                botTimer.Stop();
            }
        }

        private void MediumBotMove()
        {

            List<CustomButton> bestMoves = new List<CustomButton>();
            double bestScore = double.MinValue;

            int goalX = poartaRed.Location.X;
            int goalY = poartaRed.Location.Y;

            foreach (Control control in this.Controls)
            {
                if (control is CustomButton button && (string)button.Tag == "buton" && button.Visible && button != ButtonBehindThePlayer())
                {
                    int btnX = button.Location.X;
                    int btnY = button.Location.Y;
                    double distanceToGoal = Math.Sqrt(Math.Pow(btnX - goalX, 2) + Math.Pow(btnY - goalY, 2));
                    bool isBlockingMove = IsBlockingMove(button);
                    bool isScoringMove = IsScoringMove(button);
                    double score = EvaluateScore(distanceToGoal, isBlockingMove, isScoringMove);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMoves.Clear();
                        bestMoves.Add(button);
                    }
                    else if (score == bestScore)
                    {
                        bestMoves.Add(button);
                    }
                }
            }

            if (bestMoves.Count > 0)
            {
                CustomButton selectedButton = bestMoves[random.Next(bestMoves.Count)];
                CustomBTNClick(selectedButton, EventArgs.Empty);
            }
        }

        private string Direction(CustomButton current, CustomButton target)
        {
            int currentX = current.Location.X;
            int currentY = current.Location.Y;
            int targetX = target.Location.X;
            int targetY = target.Location.Y;
            if (currentX < targetX && currentY == targetY)//dreapta
            {
                return "dreapta";
            }
            else if (currentX > targetX && currentY == targetY)//stanga
            {
                return "stanga";
            }

            else if (currentY < targetY && currentX == targetX)//sus
            {
                return "sus";
            }
            else if (currentY > targetY && currentX == targetX)//jos
            {
                return "jos";
            }
            else if (currentX < targetX)//dreapta
            {
                if (currentY < targetY)//sus
                {
                    return "dreaptaSus";
                }
                else//jos
                {
                    return "dreaptaJos";
                }
            }
            else if (currentX > targetX)//stanga
            {
                if (currentY < targetY)//sus
                {
                    return "stangaSus";
                }
                else//jos
                {
                    return "stangaJos";
                }
            }

            return null;
        }
        private bool IsBlockingMove(CustomButton button)
        {
            string direction = Direction(ButtonBehindThePlayer(), button);
            int count = 0;
            if (button.StangaSus && direction != "stangaSus") count++;
            else if (button.Sus && direction != "sus") count++;
            else if (button.DreaptaSus && direction != "dreaptaSus") count++;

            else if (button.Stanga && direction != "stanga") count++;
            else if (button.Dreapta && direction != "dreapta") count++;

            else if (button.StangaJos && direction != "stangaJos") count++;
            else if (button.Jos && direction != "jos") count++;
            else if (button.DreaptaJos && direction != "dreaptaJos") count++;
            if (count > 0) { return true; } else { return false; }
        }


        private bool IsScoringMove(CustomButton button)
        {
            int btnX = button.Location.X;
            int btnY = button.Location.Y;
            int goalX = poartaRed.Location.X;
            int goalY = poartaRed.Location.Y;
            int ballX = BallPB.Location.X;
            int ballY = BallPB.Location.Y;

            double currentDistance = Math.Sqrt(Math.Pow(ballX - goalX, 2) + Math.Pow(ballY - goalY, 2));
            double newDistance = Math.Sqrt(Math.Pow(btnX - goalX, 2) + Math.Pow(btnY - goalY, 2));

            return newDistance < currentDistance;
        }

        private double EvaluateScore(double distanceToGoal, bool isBlockingMove, bool isScoringMove)
        {
            double distanceWeight = 0.8;
            double blockingMoveWeight = 1.0;
            double scoringMoveWeight = 1.0;

            double score = distanceWeight * (1.0 / (distanceToGoal + 1));

            if (isBlockingMove)
            {
                score += blockingMoveWeight ;  
            }
            else if (isScoringMove)
            {
                score += scoringMoveWeight;
            }

            return score;
        }








        private void DisableButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is CustomButton button && button.Tag == "buton")
                {
                    button.BackColor = Color.Green;
                    button.Enabled = false;
                }
            }
        }

        private void EnableButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is CustomButton button && (string)button.Tag == "buton" && button.Bounds.IntersectsWith(new Rectangle(BallPB.Location.X - BallPB.Width / 2 - 50, BallPB.Location.Y - BallPB.Height / 2 - 50, 150, 150)))
                {
                    button.BackColor = Color.White;
                    button.Enabled = true;
                }
            }
        }


        private void SchimbaVizibilitatea(object sender, EventArgs e)
        {
            if (Rand.Text.ToLower() == "blue's turn")
            {
                botTimer.Enabled = true;
            }
            else
            {
                EnableButtons();
            }
        }


        ///
        ///
        /// 
        /// 
        /// 
        private Dictionary<Point, Point> bfsParents = new Dictionary<Point, Point>();
        private Dictionary<Point, int> bfsDistances = new Dictionary<Point, int>();

        private IEnumerable<Point> GetNeighbors(Point p)
        {
            List<Point> neighbors = new List<Point>();

            var directions = new (int dx, int dy)[]
            {
            (0, -56), // Up
            (0, 56),  // Down
            (-58, 0), // Left
            (58, 0),   // Right
            
            (58, -56), // Up-Right
            (58, 56),  // Down-Right

            (-58, -56), // Up-Left
            (-58, 56),  // Down-Left
            };

            foreach (var (dx, dy) in directions)
            {
                Point newPoint = new Point(p.X + dx, p.Y + dy);
                if (IsValidPoint(newPoint))
                {
                    neighbors.Add(newPoint);
                }
            }

            return neighbors;
        }



        private bool IsValidPoint(Point p)
        {
            Control player = BallPB;
            CustomButton button = FindClosestButton(p);
            CustomButton closestToThePlayer = FindClosestButton(player.Location);
            if (p.X < 0 || p.Y < 0 || p.X >= this.ClientSize.Width || p.Y >= this.ClientSize.Height)
            {
                return false;
            }
            if ((servicii.PointSeIntersecteazaVertical((BallPB.Location), p) && BallPB.Location.Y == BallPB.Location.Y) || ((closestToThePlayer.Location.X == 20 || closestToThePlayer.Location.X == 484) && closestToThePlayer.Location.Y != button.Location.Y))
            {
                return false;
            }
            else if (servicii.PointSeIntersecteazaOrizontal((BallPB.Location), p) && BallPB.Location.X == button.Location.X)
            {
                return false;
            }



            else if (servicii.PointSeIntersecteazaOblicSJStanga(player, button).ToString() != "true" && player.Left > button.Right && player.Top > button.Bottom)
            {
                return false;
            }
            else if (servicii.PointSeIntersecteazaOblicSJDreapta(player, button).ToString() != "true" && player.Right < button.Left && player.Bottom > button.Top)
            {
                return false;
            }



            else if (servicii.PointSeIntersecteazaOblicJSStanga(player, button).ToString() != "false" && player.Left > button.Right && player.Bottom < button.Top)
            {
                return false;
            }

            else if (servicii.PointSeIntersecteazaOblicJSDreapta(player, button).ToString() != "false" && player.Right < button.Left && player.Top > button.Bottom)
            {
                return false;
            }

            //else if()
            return true;
        }


        private void BotMoveBFS()
        {
            CustomButton ballButton = ButtonBehindTheBall();
            CustomButton goalButton = FindClosestButton(new Point(262, 708));



            var ballPosition = new Point(ballButton.Location.X, ballButton.Location.Y);
            var goalPosition = new Point(goalButton.Location.X, goalButton.Location.Y);

            List<Point> path;
            BFS(ballPosition, goalPosition, out path);

            if (path.Count > 1)
            {
                for (int i = 1; i < path.Count; i++)
                {
                    var nextMove = path[i];
                    CustomButton targetButton = this.Controls.OfType<CustomButton>()
                                                      .FirstOrDefault(b => new Point(b.Location.X, b.Location.Y) == nextMove);

                    if (targetButton != null)
                    {
                        BTNClick(targetButton, EventArgs.Empty);
                        if (Rand.Text.ToLower() == "red's turn")
                        {
                            botTimer.Stop();
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Path is invalid or too short.");
            }
        }




        private void BFS(Point start, Point goal, out List<Point> path)
        {
            bfsParents.Clear();
            bfsDistances.Clear();

            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(start);
            bfsParents[start] = start;
            bfsDistances[start] = 0;

            bool found = false;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.Equals(goal))
                {
                    found = true;
                    break;
                }

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (!bfsDistances.ContainsKey(neighbor) && IsValidPoint(neighbor))
                    {
                        bfsDistances[neighbor] = bfsDistances[current] + 1;
                        bfsParents[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            path = new List<Point>();

            if (found)
            {
                for (var at = goal; !at.Equals(start); at = bfsParents[at])
                {
                    path.Add(at);
                }
                path.Add(start);
                path.Reverse();
            }
            else
            {
                path = new List<Point>();
                MessageBox.Show("No path found.");
            }
        }











        private CustomButton FindClosestButton(Point point)
        {
            CustomButton closestButton = null;
            double minDistance = double.MaxValue;

            foreach (Control control in this.Controls)
            {
                if (control is CustomButton button && (string)control.Tag == "buton")
                {
                    var buttonPosition = new Point(button.Location.X + button.Width / 2, button.Location.Y + button.Height / 2);
                    double distance = Math.Sqrt(Math.Pow(buttonPosition.X - point.X, 2) + Math.Pow(buttonPosition.Y - point.Y, 2));

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestButton = button;
                    }
                }
            }

            return closestButton;
        }


        public CustomButton ButtonBehindTheBall()
        {
            foreach (Control control in this.Controls)
            {
                if (control is CustomButton button && (string)button.Tag == "buton" && BallPB.Bounds.IntersectsWith(button.Bounds))
                {
                    return button;
                }
            }
            return null; // Return null if no button is found
        }



        public void DrawCustomButton()
        {



            int[,] coordinates = new int[,]
            {
            {310, 26}, {252, 26}, {194, 26},
            {310, 698}, {252, 698}, {194, 698},
            {310, 642}, {484, 642}, {426, 642}, {368, 642}, {252, 642}, {194, 642}, {136, 642}, {78, 642},
            {310, 586}, {484, 586}, {426, 586}, {368, 586}, {252, 586}, {194, 586}, {136, 586}, {78, 586},
            {310, 530}, {484, 530}, {426, 530}, {368, 530}, {252, 530}, {194, 530}, {136, 530}, {78, 530},
            {310, 474}, {484, 474}, {426, 474}, {368, 474}, {252, 474}, {194, 474}, {136, 474}, {78, 474},
            {310, 418}, {484, 418}, {426, 418}, {368, 418}, {252, 418}, {194, 418}, {136, 418}, {78, 418}, {20, 418},
            {20, 474}, {20, 530}, {20, 586}, {20, 642},
            {310, 82}, {484, 82}, {426, 82}, {368, 82}, {252, 82}, {194, 82}, {136, 82}, {78, 82},
            {310, 138}, {484, 138}, {426, 138}, {368, 138}, {252, 138}, {194, 138}, {136, 138}, {78, 138},
            {310, 194}, {484, 194}, {426, 194}, {368, 194}, {252, 194}, {194, 194}, {136, 194}, {78, 194},
            {310, 250}, {484, 250}, {426, 250}, {368, 250}, {252, 250}, {194, 250}, {136, 250}, {78, 250}, {20, 82},
            {20, 138}, {20, 194}, {20, 250},
            {310, 306}, {484, 306}, {426, 306}, {368, 306}, {252, 306}, {194, 306}, {136, 306}, {78, 306}, {20, 306},
            {310, 362}, {484, 362}, {426, 362}, {368, 362}, {252, 362}, {194, 362}, {136, 362}, {78, 362}, {20, 362}
            };

            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                CustomButton button = new CustomButton();
                button.Size = new Size(20, 20);
                button.BackColor = Color.FromArgb(255, GenerateRandom(), GenerateRandom(), GenerateRandom());
                button.Left = coordinates[i, 0];
                button.Top = coordinates[i, 1];
                button.Tag = "buton";
                button.Click += new EventHandler(CustomBTNClick);
                if (button.Bounds.X == 20)
                {
                    if (button.Bounds.Y == 82)
                    {
                        button.StangaSus = true;
                        button.Sus = true;
                        button.DreaptaSus = true;
                        button.Stanga = true;
                        button.Dreapta = true;
                        button.Jos = true;
                        button.StangaJos = true;
                    }
                    else if (button.Bounds.Y == 642)
                    {
                        button.StangaSus = true;
                        button.Sus = true;
                        button.Stanga = true;
                        button.Dreapta = true;
                        button.StangaJos = true;
                        button.Jos = true;
                        button.DreaptaJos = true;
                    }
                    else
                    {
                        button.StangaSus = true;
                        button.Stanga = true;
                        button.StangaJos = true;
                        button.Sus = true;
                        button.Jos = true;
                    }

                }
                else if (button.Bounds.X == 484)
                {
                    if (button.Bounds.Y == 82)
                    {
                        button.StangaSus = true;
                        button.Sus = true;
                        button.DreaptaSus = true;
                        button.Stanga = true;
                        button.Dreapta = true;
                        button.Jos = true;
                        button.DreaptaJos = true;
                    }
                    else if (button.Bounds.Y == 642)
                    {
                        button.Sus = true;
                        button.DreaptaSus = true;
                        button.Stanga = true;
                        button.Dreapta = true;
                        button.StangaJos = true;
                        button.Jos = true;
                        button.DreaptaJos = true;
                    }
                    else
                    {
                        button.DreaptaSus = true;
                        button.Dreapta = true;
                        button.DreaptaJos = true;
                        button.Sus = true;
                        button.Jos = true;
                    }
                }
                else
                {
                    if (button.Bounds.Y == 26)
                    {
                        if (button.Bounds.X == 194)
                        {
                            button.StangaSus = true;
                            button.Sus = true;
                            button.DreaptaSus = true;
                            button.Stanga = true;
                            button.Dreapta = true;
                            button.StangaJos = true;
                            button.Jos = true;
                        }
                        else if (button.Bounds.X == 310)
                        {
                            button.StangaSus = true;
                            button.Sus = true;
                            button.DreaptaSus = true;
                            button.Stanga = true;
                            button.Dreapta = true;
                            button.DreaptaJos = true;
                            button.Jos = true;
                        }

                    }

                    else if (button.Bounds.Y == 698)
                    {
                        if (button.Bounds.X == 194)
                        {
                            button.StangaSus = true;
                            button.Sus = true;
                            button.Stanga = true;
                            button.Dreapta = true;
                            button.StangaJos = true;
                            button.Jos = true;
                            button.DreaptaJos = true;
                        }
                        else if (button.Bounds.X == 310)
                        {
                            button.Sus = true;
                            button.DreaptaSus = true;
                            button.Stanga = true;
                            button.Dreapta = true;
                            button.StangaJos = true;
                            button.Jos = true;
                            button.DreaptaJos = true;
                        }

                    }

                    else if (button.Bounds.Y == 82)
                    {
                        if (button.Bounds.X == 194)
                        {
                            button.StangaSus = true;
                            button.Sus = true;
                            button.Stanga = true;
                        }
                        else if (button.Bounds.X == 310)
                        {
                            button.Sus = true;
                            button.DreaptaSus = true;
                            button.Dreapta = true;
                        }
                        else if (button.Bounds.X != 252)
                        {
                            button.StangaSus = true;
                            button.Sus = true;
                            button.DreaptaSus = true;
                            button.Stanga = true;
                            button.Dreapta = true;
                        }
                    }

                    else if (button.Bounds.Y == 642)
                    {
                        if (button.Bounds.X == 194)
                        {
                            button.Stanga = true;
                            button.StangaJos = true;
                            button.Jos = true;
                        }
                        else if (button.Bounds.X == 310)
                        {
                            button.Dreapta = true;
                            button.Jos = true;
                            button.DreaptaJos = true;
                        }
                        else if (button.Bounds.X != 252)
                        {
                            button.StangaJos = true;
                            button.Jos = true;
                            button.DreaptaJos = true;
                            button.Stanga = true;
                            button.Dreapta = true;
                        }
                    }

                }
                this.Controls.Add(button);
            }
            Button restartBTN = new Button();
            restartBTN.Size = new Size(96, 28);
            restartBTN.Left = 384;
            restartBTN.Top = 677;
            restartBTN.BackColor = Color.Green;
            restartBTN.Text = "Restart";
            restartBTN.FlatStyle = FlatStyle.Popup;
            restartBTN.Click += new EventHandler(Restart_btn_Click);


            this.Controls.Add(restartBTN);
        }

        public int GenerateRandom()
        {
            return random.Next(255);
        }









        private void CustomBTNClick(object sender, EventArgs e)
        {
            CustomButton cb = (CustomButton)sender;
            CustomButton playerButton = new CustomButton();
            playerButton = ButtonBehindThePlayer();
            int x, y;
            x = cb.Location.X;
            y = cb.Location.Y;
            int a, b;
            a = BallPB.Location.X;
            b = BallPB.Location.Y;

            BallPB.SetBounds(cb.Location.X - 20 / 4, cb.Location.Y - 20 / 4, BallPB.Width, BallPB.Height);
            intersects = ReturnIntesectationState(cb);





            if (x < a - 20 / 4 && y - 20 / 4 == b)
            {
                //stanga
                servicii.LinieStanga(x, y, a, b);
                playerButton.Stanga = true;
                cb.Dreapta = true;
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y == b + 20 / 4 && x > a + 20 / 4)
            {
                //dreapta
                servicii.LinieDreapta(x, y, a, b);
                playerButton.Dreapta = true;
                cb.Stanga = true;
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y - 20 / 4 > b && x == a + 20 / 4)
            {
                //jos
                servicii.LinieJos(x, y, a, b);
                playerButton.Jos = true;
                cb.Sus = true;
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (y - 20 / 4 < b && x - 20 / 4 == a)
            {
                //sus
                servicii.LinieSus(x, y, a, b);
                playerButton.Sus = true;
                cb.Jos = true;
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 > a && y < b + 20 / 4)
            {
                //dreapta sus
                servicii.LinieDreaptaSus(x, y, a, b);
                playerButton.DreaptaSus = true;
                cb.StangaJos = true;
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 > a && y > b + 20 / 4)
            {
                //dreapta jos
                servicii.LinieDreaptaJos(x, y, a, b);
                playerButton.DreaptaJos = true;
                cb.StangaSus = true;
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 < a && y < b + 20 / 4)
            {
                //stanga sus
                servicii.LinieStangaSus(x, y, a, b);
                playerButton.StangaSus = true;
                cb.DreaptaJos = true;
                if (!intersects) { servicii.Turn(Rand); }
            }
            else if (x - 20 / 4 < a && y > b + 20 / 4)
            {
                //stanga jos
                servicii.LinieStangaJos(x, y, a, b);
                playerButton.StangaJos = true;
                cb.DreaptaSus = true;
                if (!intersects) { servicii.Turn(Rand); }
            }

            //MessageBox.Show("stanga sus: " + cb.StangaSus + "\n sus: " + cb.Sus + "\n dreapta sus: " + cb.DreaptaSus + "\n stanga: " + cb.Stanga + "\n dreapta: " + cb.Dreapta + "\n stanga jos: " + cb.StangaJos + "\n jos: " + cb.Jos + "\n dreapta jos: " + cb.DreaptaJos);
        }

        public bool LeftIsValid(CustomButton current, CustomButton target)
        {
            if (current.Stanga == true || target.Dreapta == true)
            {
                return false;
            }
            return true;
        }

        public bool RightIsValid(CustomButton current, CustomButton target)
        {
            if (current.Dreapta == true || target.Stanga == true)
            {
                return false;
            }
            return true;
        }

        public bool UpIsValid(CustomButton current, CustomButton target)
        {
            if (current.Sus == true || target.Jos == true)
            {
                return false;
            }
            return true;
        }
        public bool DownIsValid(CustomButton current, CustomButton target)
        {
            if (current.Jos == true || target.Sus == true)
            {
                return false;
            }
            return true;
        }

        public bool UpLeftIsValid(CustomButton current, CustomButton target)
        {
            if (current.StangaSus == true || target.DreaptaJos == true)
            {
                return false;
            }
            return true;
        }

        public bool DownLeftIsValid(CustomButton current, CustomButton target)
        {
            if (current.StangaJos == true || target.DreaptaSus == true)
            {
                return false;
            }
            return true;
        }

        public bool UpRightIsValid(CustomButton current, CustomButton target)
        {
            if (current.DreaptaSus == true || target.StangaJos == true)
            {
                return false;
            }
            return true;
        }

        public bool DownRightIsValid(CustomButton current, CustomButton target)
        {
            if (current.DreaptaJos == true || target.StangaSus == true)
            {
                return false;
            }
            return true;
        }

        public CustomButton ButtonBehindThePlayer()
        {
            foreach (Control control in this.Controls)
            {
                if (control is CustomButton button && (string)button.Tag == "buton" && BallPB.Bounds.IntersectsWith(button.Bounds))
                {
                    return button;
                }
            }
            return null; //not found
        }
        public void ShowTheButtons()
        {
            int x = ButtonBehindThePlayer().Left;
            int y = ButtonBehindThePlayer().Top;

            foreach (Control control in this.Controls)
            {
                if (control is CustomButton customButton && (string)customButton.Tag == "buton" && BallPB.Bounds.IntersectsWith(customButton.Bounds) == false)
                {
                    if (customButton.Location.X == x)
                    {
                        if (customButton.Location.Y > y && customButton.Location.Y < y + 70)//jos
                        {
                            if (customButton.Sus == false)
                            {
                                customButton.Visible = true;
                            }
                            else customButton.Visible = false;


                            if ((customButton.Bounds.X == 310 || customButton.Bounds.X == 194) && BallPB.Bounds.Y == 642 - 20 / 4)
                            {
                                customButton.Visible = false;
                            }
                        }
                        else if (customButton.Location.Y < y && customButton.Location.Y > y - 70)//sus
                        {
                            if (customButton.Jos == false)
                            {
                                customButton.Visible = true;
                            }
                            else if ((customButton.Bounds.X == 310 || customButton.Bounds.X == 194) && BallPB.Bounds.Y == 82 - 20 / 4)
                            {
                                customButton.Visible = false;
                            }
                            else customButton.Visible = false;



                        }
                        else { customButton.Visible = false; }

                    }

                    else if (customButton.Location.Y == y)
                    {

                        if (customButton.Location.X > x && customButton.Location.X < x + 70)//dreapta
                        {
                            if (customButton.Stanga == false)
                            {
                                customButton.Visible = true;
                            }
                            else if ((customButton.Bounds.X < 252 || customButton.Bounds.X > 310) && customButton.Bounds.Y == 82)
                            {
                                customButton.Visible = false;
                            }
                            else if ((customButton.Bounds.X < 252 || customButton.Bounds.X > 310) && customButton.Bounds.Y == 642)
                            {
                                customButton.Visible = false;
                            }
                            else customButton.Visible = false;


                        }
                        else if (customButton.Location.X < x && customButton.Location.X > x - 70)//stanga
                        {
                            if (customButton.Dreapta == false)
                            {
                                customButton.Visible = true;
                            }
                            else if ((customButton.Bounds.X < 194 || customButton.Bounds.X > 252) && customButton.Bounds.Y == 82)
                            {
                                customButton.Visible = false;
                            }
                            else if ((customButton.Bounds.X < 194 || customButton.Bounds.X > 252) && customButton.Bounds.Y == 642)
                            {
                                customButton.Visible = false;
                            }
                            else customButton.Visible = false;



                        }
                        else { customButton.Visible = false; }

                    }


                    else if (customButton.Location.X > x && customButton.Location.X < x + 70)
                    {
                        if (customButton.Location.Y > y && customButton.Location.Y < y + 70)//dreapta jos
                        {
                            if (customButton.StangaSus == false)
                            {
                                customButton.Visible = true;
                            }
                            else customButton.Visible = false;

                        }
                        else if (customButton.Location.Y < y && customButton.Location.Y > y - 70)//dreapta sus
                        {
                            if (customButton.StangaJos == false)
                            {
                                customButton.Visible = true;
                            }
                            else customButton.Visible = false;

                        }
                        else { customButton.Visible = false; }


                    }
                    else if (customButton.Location.X < x && customButton.Location.X > x - 70)
                    {
                        if (customButton.Location.Y > y && customButton.Location.Y < y + 70)//stanga jos
                        {
                            if (customButton.DreaptaSus == false)
                            {
                                customButton.Visible = true;
                            }
                            else customButton.Visible = false;


                        }
                        else if (customButton.Location.Y < y && customButton.Location.Y > y - 70)//stanga sus
                        {
                            if (customButton.DreaptaJos == false)
                            {
                                customButton.Visible = true;
                            }
                            else customButton.Visible = false;

                        }
                        else { customButton.Visible = false; }

                    }
                    else { customButton.Visible = false; }
                }
            }
        }
        public bool ReturnIntesectationState(CustomButton button)
        {

            if (button.StangaSus == true || button.Sus == true || button.DreaptaSus == true || button.Stanga == true || button.Dreapta == true || button.StangaJos == true || button.Jos == true || button.DreaptaJos == true)
            {
                return true;
            }

            return false;
        }
        public bool DirectionsAvailable(CustomButton button)
        {

            if (button.StangaSus == true && button.Sus == true && button.DreaptaSus == true && button.Stanga == true && button.Dreapta == true && button.StangaJos == true && button.Jos == true && button.DreaptaJos == true)
            {
                return false;
            }

            return true;
        }
    }
}
