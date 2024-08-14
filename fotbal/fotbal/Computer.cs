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
        private int btnCounter = 0;
        private System.Windows.Forms.Timer botTimer;
        Random random = new Random();
        private string selectedDifficulty = String.Empty;

        public Computer(string difficulty)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            timer1.Start();
            AddButtons();
            BallPB.SetBounds(247, 357, BallPB.Width, BallPB.Height);
            Rand.Text = $"Blue's turn";
            Rand.TextChanged += new EventHandler(Rand_TextChanged);
            servicii = new Servicii(this, BallPB.Width, BallPB.Height, 20, 20, 58, 56, BallPB);
            servicii.CallTheBorder();

            botTimer = new System.Windows.Forms.Timer();
            botTimer.Interval = 1000;
            botTimer.Tick += BotTimer_Tick;
            selectedDifficulty = difficulty;
            AllBallsCB.CheckedChanged += SchimbaVizibilitatea;
        }

        private void AddButtons()
        {
            // Coordinates as provided
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
                Button button = new Button();
                button.Size = new Size(20, 20);
                button.Left = coordinates[i, 0];
                button.Top = coordinates[i, 1];
                button.Tag = "buton";
                button.Click += new EventHandler(BTNClick);
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
                    if (Btn is Button && (String)Btn.Tag == "buton")
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
                    if (Btn is Button && (String)Btn.Tag == "buton")
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
                    servicii.Mutare();
                    if (Rand.Text.ToLower() == "red's turn")
                    {
                        EnableButtons();
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

            Button cb = (Button)sender;
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


        }
        private void BotMove()
        {

            List<Button> closestBTNs = new List<Button>();
            double distantaMinima = double.MaxValue;
            int goalX = poartaRed.Location.X;
            int goalY = poartaRed.Location.Y;
            int ballXLocation = BallPB.Location.X;
            int ballYLocation = BallPB.Location.Y;



            foreach (Control control in this.Controls)
            {
                if (control is Button button && (string)control.Tag == "buton" && control.Visible == true)
                {
                    int btnXLocation = button.Location.X;
                    int btnYLocation = button.Location.Y;

                    double distanta = Math.Sqrt(Math.Pow(btnXLocation - goalX, 2) + Math.Pow(btnYLocation - goalY, 2));

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
            if (closestBTNs.Count > 0)
            {
                Button selectedButton = closestBTNs[random.Next(closestBTNs.Count)];
                BTNClick(selectedButton, EventArgs.Empty);
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

            List<Button> bestMoves = new List<Button>();
            double bestScore = double.MinValue;

            int goalX = poartaRed.Location.X;
            int goalY = poartaRed.Location.Y;
            int ballX = BallPB.Location.X;
            int ballY = BallPB.Location.Y;

            foreach (Control control in this.Controls)
            {
                if (control is Button button && control.Tag == "buton" && control.Visible)
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
                Button selectedButton = bestMoves[random.Next(bestMoves.Count)];
                BTNClick(selectedButton, EventArgs.Empty);
            }
        }

        private bool IsBlockingMove(Button button)
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Bounds.IntersectsWith(button.Bounds))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsScoringMove(Button button)
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

            double distanceWeight = 1.0;
            double blockingMoveWeight = 0.5;
            double scoringMoveWeight = 0.7;
            double score = distanceWeight * (1.0 / (distanceToGoal + 1));

            if (isBlockingMove)
            {
                score += blockingMoveWeight;
            }
            if (isScoringMove)
            {
                score += scoringMoveWeight;
            }

            return score;
        }







        private void DisableButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Tag == "buton")
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
                if (control is Button button && (string)button.Tag == "buton" && button.Bounds.IntersectsWith(new Rectangle(BallPB.Location.X - BallPB.Width / 2 - 100, BallPB.Location.Y - BallPB.Height / 2 - 100, 200, 200)))
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


        // Use tuples for representing coordinates
        private Dictionary<Point, Point> bfsParents = new Dictionary<Point, Point>();
        private Dictionary<Point, int> bfsDistances = new Dictionary<Point, int>();

        private IEnumerable<Point> GetNeighbors(Point p)
        {
            List<Point> neighbors = new List<Point>();

            // Define movement directions (up, down, left, right)
            var directions = new (int dx, int dy)[]
            {
        (0, -1), // Up
        (0, 1),  // Down
        (-1, 0), // Left
        (1, 0)   // Right
            };

            foreach (var (dx, dy) in directions)
            {
                Point newPoint = new Point(p.X + dx, p.Y + dy);
                if (IsValidPoint(newPoint))
                {
                    neighbors.Add(newPoint);
                    //MessageBox.Show($"Neighbor found: {newPoint}");
                }
            }

            return neighbors;
        }



        private bool IsValidPoint(Point p)
        {
            // Example validation: ensure the point is within bounds
            return true;
        }


        private void BotMoveBFS()
        {
            Button ballButton = ButtonBehindTheBall();
            Button goalButton = FindClosestButton(new Point(poartaRed.Location.X, poartaRed.Location.Y+15));

            if (ballButton == null || goalButton == null)
            {
                MessageBox.Show("No suitable buttons found for the ball or goal.");
                return;
            }

            var ballPosition = new Point(ballButton.Location.X, ballButton.Location.Y - 1); // Adjust if needed
            var goalPosition = new Point(goalButton.Location.X, goalButton.Location.Y);

            List<Point> path;
            BFS(ballPosition, goalPosition, out path);

            if (path.Count > 1)
            {
                // Process path steps, skipping the initial ball position
                for (int i = 1; i < path.Count; i++)
                {
                    var nextMove = path[i];
                    Button targetButton = this.Controls.OfType<Button>()
                                                      .FirstOrDefault(b => new Point(b.Location.X, b.Location.Y) == nextMove);

                    if (targetButton != null)
                    {
                        Debug.WriteLine($"Moving to button at {nextMove}");
                        BTNClick(targetButton, EventArgs.Empty);
                        // Consider adding a delay to make sure each move is processed
                        //Task.Delay(1000).Wait(); // Adjust delay as needed
                    }
                    else
                    {
                       // MessageBox.Show($"Target button not found for the next move. Next move: {nextMove.ToString()}");
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
            queue.Enqueue(new Point(start.X, start.Y - 1));
            bfsParents[new Point(start.X,start.Y-1)] = new Point(start.X, start.Y-1);
            bfsDistances[new Point(start.X, start.Y - 1)] = 0;

            bool found = false;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                //MessageBox.Show($"Processing point: {current}");

                if (current.Equals(goal))
                {
                    found = true;
                    break; // Goal reached
                }

                foreach (var neighbor in GetNeighbors(current))
                {
                    //MessageBox.Show($"Checking neighbor: {neighbor}");

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
                path.Add(new Point(start.X, start.Y - 2));
                path.Reverse();
                //MessageBox.Show($"Path found: {string.Join(" -> ", path)}");
            }
            else
            {
                MessageBox.Show("No path found.");
            }
        }










        private Button FindClosestButton(Point point)
        {
            Button closestButton = null;
            double minDistance = double.MaxValue;

            foreach (Control control in this.Controls)
            {
                if (control is Button button && (string)control.Tag == "buton")
                {
                    var buttonPosition = new Point(button.Location.X + button.Width / 2, button.Location.Y + button.Height / 2);
                    double distance = Math.Sqrt(Math.Pow(buttonPosition.X - point.X, 2) + Math.Pow(buttonPosition.Y - point.Y, 2));

                    //MessageBox.Show($"Button {button} at {buttonPosition} distance to target: {distance}");

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestButton = button;
                    }
                }
            }

            return closestButton;
        }


        public Button ButtonBehindTheBall()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button && (string)button.Tag == "buton" && BallPB.Bounds.IntersectsWith(button.Bounds))
                {
                    return button;
                }
            }
            return null; // Return null if no button is found
        }





    }
}
