namespace fotbal
{
    partial class Computer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Computer));
            timer1 = new System.Windows.Forms.Timer(components);
            BallPB = new PictureBox();
            GolLabel = new Label();
            Rand = new Label();
            poartaRed = new PictureBox();
            poartaBlue = new PictureBox();
            AllBallsCB = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)BallPB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)poartaRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)poartaBlue).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // BallPB
            // 
            BallPB.Anchor = AnchorStyles.None;
            BallPB.BackColor = Color.Transparent;
            BallPB.Image = (Image)resources.GetObject("BallPB.Image");
            BallPB.Location = new Point(247, 357);
            BallPB.Name = "BallPB";
            BallPB.Size = new Size(30, 30);
            BallPB.SizeMode = PictureBoxSizeMode.StretchImage;
            BallPB.TabIndex = 1;
            BallPB.TabStop = false;
            // 
            // GolLabel
            // 
            GolLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GolLabel.AutoSize = true;
            GolLabel.BackColor = Color.Transparent;
            GolLabel.Font = new Font("Playbill", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            GolLabel.ForeColor = Color.White;
            GolLabel.Location = new Point(20, 13);
            GolLabel.Name = "GolLabel";
            GolLabel.Size = new Size(144, 29);
            GolLabel.TabIndex = 108;
            GolLabel.Text = "GOOOOOOOOOOOOL!";
            GolLabel.Visible = false;
            // 
            // Rand
            // 
            Rand.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Rand.BackColor = Color.Transparent;
            Rand.Font = new Font("Playbill", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            Rand.ForeColor = Color.White;
            Rand.Location = new Point(20, 40);
            Rand.Name = "Rand";
            Rand.Size = new Size(136, 29);
            Rand.TabIndex = 112;
            // 
            // poartaRed
            // 
            poartaRed.BackColor = Color.Red;
            poartaRed.Location = new Point(257, 675);
            poartaRed.Name = "poartaRed";
            poartaRed.Size = new Size(10, 10);
            poartaRed.TabIndex = 114;
            poartaRed.TabStop = false;
            // 
            // poartaBlue
            // 
            poartaBlue.BackColor = Color.Blue;
            poartaBlue.Location = new Point(257, 59);
            poartaBlue.Name = "poartaBlue";
            poartaBlue.Size = new Size(10, 10);
            poartaBlue.TabIndex = 113;
            poartaBlue.TabStop = false;
            // 
            // AllBallsCB
            // 
            AllBallsCB.AutoSize = true;
            AllBallsCB.BackColor = Color.Transparent;
            AllBallsCB.Location = new Point(384, 13);
            AllBallsCB.Name = "AllBallsCB";
            AllBallsCB.Size = new Size(88, 19);
            AllBallsCB.TabIndex = 115;
            AllBallsCB.Text = "Teren intreg";
            AllBallsCB.UseVisualStyleBackColor = false;
            // 
            // Computer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.teren;
            ClientSize = new Size(534, 731);
            Controls.Add(AllBallsCB);
            Controls.Add(poartaRed);
            Controls.Add(poartaBlue);
            Controls.Add(Rand);
            Controls.Add(GolLabel);
            Controls.Add(BallPB);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Computer";
            Text = "Computer";
            ((System.ComponentModel.ISupportInitialize)BallPB).EndInit();
            ((System.ComponentModel.ISupportInitialize)poartaRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)poartaBlue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private PictureBox BallPB;
        private Label GolLabel;
        private Label Rand;
        private PictureBox poartaRed;
        private PictureBox poartaBlue;
        private CheckBox AllBallsCB;
    }
}