using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms_DG
{
    public partial class Form1 : Form
    {
        Point PlayerPosition = new Point(0,0);
        Point GhostPosition = new Point(0,0);

        int RoomWidth = 0;
        int RoomHeight = 0;

        List<PictureBox> CellSpaces = new List<PictureBox>();
        public void GetWindowDetails()
        {
            int WindowTop = this.Top + 8;
            int WindowLeft = this.Left + 8;
            Debug.WriteLine("The window is located at X = {0}, and Y = {1}", WindowLeft, WindowTop);
        }
        int GridSize = 8;
        public void SwitchToSmall()
        {
            pictureBox2.Visible = true;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Size = new Size(360, 360);
            pictureBox2.Image = Forms_DG.Properties.Resources.SMALLROOMGRIDEDITED1;
            pictureBox3.Visible = true;
            pictureBox3.Location = new Point(-7, -6);
            pictureBox3.Size = new Size(390, 390);
            GridSize = 8;
        }
        public void SwitchToMedium()
        {
            pictureBox2.Image = Forms_DG.Properties.Resources.MEDIUMROOMGRIDEDITED;
            pictureBox2.Visible = true;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Size = new Size(539, 539);
            pictureBox3.Visible = true;
            pictureBox3.Location = new Point(-7, -6);
            pictureBox3.Size = new Size(570, 570);
            GridSize = 12;
        }

        public void SwitchToLarge()
        {
            pictureBox2.Image = Forms_DG.Properties.Resources.LARGEROOMGRIDEDITED;
            pictureBox2.Visible = true;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Size = new Size(718, 718);
            pictureBox3.Visible = true;
            pictureBox3.Location = new Point(-7, -6);
            pictureBox3.Size = new Size(749, 749);
            GridSize = 16;
        }

        public void SwitchToRandom()
        {
            Random r = new Random();
            int RandomColumnMultiplier = r.Next(0,13);
            int RandomRowMultiplier = r.Next(0, 13);
            pictureBox2.Image = Forms_DG.Properties.Resources.LARGEROOMGRIDEDITED;
            pictureBox2.Visible = true;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Size = new Size((45*RandomColumnMultiplier)+135, (45*RandomRowMultiplier)+135);
            pictureBox3.Visible = true;
            pictureBox3.Location = new Point(-7, -6);
            pictureBox3.Size = new Size((45 * RandomColumnMultiplier) + 165, (45 * RandomRowMultiplier) + 165);
        }

        public void GenerateRoom() //T1 - 50%, T2 = 30%, T3 = 10%, Random = 10%
        {
            SwitchToRandom();
            RoomWidth = pictureBox2.Size.Width / 45;
            RoomHeight = pictureBox2.Size.Height / 45;
            //Random r = new Random();
            //int RoomPicker = r.Next(1,101);
            //if (RoomPicker < 51)
            //{
            //    SwitchToSmall();
            //    RoomWidth = 8;
            //    RoomHeight = 8;
            //}
            //else if (RoomPicker > 50 && RoomPicker < 81)
            //{
            //    SwitchToMedium();
            //    RoomWidth = 12;
            //    RoomHeight = 12;
            //}
            //else if (RoomPicker > 80 && RoomPicker < 91)
            //{
            //    SwitchToLarge();
            //    RoomWidth = 16;
            //    RoomHeight = 16;
            //}
            //else
            //{
            //    SwitchToRandom();
            //    RoomWidth = pictureBox2.Size.Width / 45;
            //    RoomHeight = pictureBox2.Size.Height / 45;
            //}
        }

        public void ShowInitiativeTable()
        {
            pictureBox4.Location = new Point(742, 0);
            pictureBox4.Size = new Size(360, 695);
            pictureBox4.Visible = true;
        }

        bool IsNormalDifficulty = true;
        public Form1()
        {
            //CreateButton();
            InitializeComponent();
            //this.BackColor = Color.Green;
            //TransparencyKey = Color.Green;
            this.BackColor = Color.Gray;

            button1.Size = new Size(506, 112);
            button1.Location = new Point(382, 322);
            button1.Image = Forms_DG.Properties.Resources.DIFFICULTYNORMALBUTTON;
            button2.Size = new Size(250, 112);
            button2.Location = new Point(509, 205);
            pictureBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            pictureBox1.Location = new Point(-1, -3);
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Location = new Point(742, 0);
            pictureBox4.Size = new Size(360, 695);
            pictureBox4.Visible = false;
            
        }

        public void PlacingPictureBoxes(int RoomWidth, int RoomHeight)
        {
            Random r = new Random();
            int NumberOfCells = RoomWidth * RoomHeight;
            int k = 0;
            int BringBackH = 0;
            int BringBackW = 0;
            string RoomRarity = "";

            double CommonRatio = 0;
            int CommonPercent = 0;
            int RarePercent = 0;
            int LegendaryPercent = 0;
            int ForsakenPercent = 0;
            int RandomCommonNumber = 0;

            foreach (PictureBox p in CellSpaces)
            {
                p.Visible = false;
            }
            CellSpaces.Clear();

            for (int i = 0; i < NumberOfCells; i++)
            {
                PictureBox Cell = new PictureBox();
                this.Controls.Add(Cell);
                Cell.Name = "Cell" + i;
                Cell.Size = new Size(40, 40);
                Cell.Visible = false;
                Cell.SendToBack();
                if (NumberOfCells <= 49)
                {
                    RoomRarity = "Common";
                }
                else if (49 < NumberOfCells && NumberOfCells <= 121)
                {
                    RoomRarity = "Rare";
                }
                else if (121 < NumberOfCells && NumberOfCells <= 196)
                {
                    RoomRarity = "Legendary";
                }
                else
                {
                    RoomRarity = "Forsaken";
                }
                int ChoosingObject = r.Next(1, 101);
                if (ChoosingObject < 4)
                {
                    switch (RoomRarity)
                    {
                        case "Common":
                            CommonRatio = NumberOfCells / 25;
                            CommonPercent = Convert.ToInt32((95 - (2.5 * CommonRatio)));
                            RarePercent = Convert.ToInt32((3 + (2 * CommonRatio)));
                            LegendaryPercent = Convert.ToInt32((1 + (0.5 * CommonRatio)));
                            ForsakenPercent = 1;
                            RandomCommonNumber = r.Next(1, 101);
                            if (RandomCommonNumber <= CommonPercent)
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T1CommonFinal;
                            }
                            else if (RandomCommonNumber > CommonPercent && RandomCommonNumber <= (CommonPercent + RarePercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T1RareFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T1LegendaryFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent + LegendaryPercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent + ForsakenPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T1Forsaken;
                            }
                            else
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T1CommonFinal;
                            }
                            break;

                        case "Rare":
                            double RareRatio = NumberOfCells / 85;
                            CommonPercent = Convert.ToInt32((80 - (15 * RareRatio)));
                            RarePercent = Convert.ToInt32((17 + (13.5 * RareRatio)));
                            LegendaryPercent = Convert.ToInt32((2 + (1 * RareRatio)));
                            ForsakenPercent = Convert.ToInt32((1 + (1 * RareRatio))); ;
                            RandomCommonNumber = r.Next(1, 101);
                            if (RandomCommonNumber <= CommonPercent)
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T2CommonFinal;
                            }
                            else if (RandomCommonNumber > CommonPercent && RandomCommonNumber <= (CommonPercent + RarePercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T2RareFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T2LegendaryFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent + LegendaryPercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent + ForsakenPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T2Forsaken;
                            }
                            else
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T2CommonFinal;
                            }
                            break;

                        case "Legendary":
                            double LegendaryRatio = NumberOfCells / 159;
                            CommonPercent = Convert.ToInt32((40 - (15 * LegendaryRatio)));
                            RarePercent = Convert.ToInt32((54 + (13 * LegendaryRatio)));
                            LegendaryPercent = Convert.ToInt32((4 + (1.5 * LegendaryRatio)));
                            ForsakenPercent = Convert.ToInt32((2 + (0.5 * LegendaryRatio))); ;
                            RandomCommonNumber = r.Next(1, 101);
                            if (RandomCommonNumber <= CommonPercent)
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T3CommonFinal;
                            }
                            else if (RandomCommonNumber > CommonPercent && RandomCommonNumber <= (CommonPercent + RarePercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T3RareFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T3LegendaryFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent + LegendaryPercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent + ForsakenPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T3Forsaken;
                            }
                            else
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T3CommonFinal;
                            }
                            break;

                        case "Forsaken":
                            double ForsakenRatio = NumberOfCells / 226;
                            CommonPercent = 0;
                            RarePercent = Convert.ToInt32((87 - (0.5 * ForsakenRatio)));
                            LegendaryPercent = Convert.ToInt32((8 + (0.5 * ForsakenRatio)));
                            ForsakenPercent = 5;
                            RandomCommonNumber = r.Next(1, 101);
                            if (RandomCommonNumber <= CommonPercent)
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T4CommonFinal;
                            }
                            else if (RandomCommonNumber > CommonPercent && RandomCommonNumber <= (CommonPercent + RarePercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T4RareFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T4LegendaryFinal;
                            }
                            else if (RandomCommonNumber > (CommonPercent + RarePercent + LegendaryPercent) && RandomCommonNumber <= (CommonPercent + RarePercent + LegendaryPercent + ForsakenPercent))
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T4Forsaken;
                            }
                            else
                            {
                                Cell.Image = Forms_DG.Properties.Resources.T4CommonFinal;
                            }
                            break;
                    }
                }
                CellSpaces.Add(Cell);
            }

            for (int i = 0; i < RoomHeight; i++)
            {
                if (i >= 6 && i < 12)
                {
                    BringBackH = 1;
                }else if (i >= 12)
                {
                    BringBackH = 3;
                }

                for (int j = 0; j < RoomWidth; j++)
                {
                    if (j >= 5 && j <10)
                    {
                        BringBackW = 1;
                    }else if (j >= 10 && j < 12)
                    {
                        BringBackW = 3;
                    }
                    else if (j >= 12 && j < 15)
                    {
                        BringBackW = 3;
                    } else if (j >=15)
                    {
                        BringBackW = 4;
                    }

                    CellSpaces[k].Location = new Point(((14 - BringBackW) + 45 * j), ((14 - BringBackH) + 45 * i));
                    CellSpaces[k].Visible = true;
                    CellSpaces[k].BringToFront();
                    k++;
                    BringBackW = 0;
                }
                BringBackH = 0;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //private void CreateButton()
        //{
        //    button1 = new Button();
        //    button1.FlatAppearance.BorderSize = 0;
        //    button1.FlatAppearance.MouseDownBackColor = Color.Green;
        //    button1.FlatAppearance.MouseOverBackColor = Color.Green;
        //    button1.FlatStyle = FlatStyle.Flat;
        //    button1.ForeColor = Color.Transparent;
        //    button1.Location = new Point(500, 200); //Give your own location as needed
        //    button1.Name = "button1";
        //    button1.Size = new Size(75, 23);
        //    button1.TabIndex = 0;
        //    button1.Text = "";
        //    button1.UseVisualStyleBackColor = true;
        //    button1.Click += this.button1_Click;
        //    Controls.Add(button1);
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            if (IsNormalDifficulty) //If the game is in normal difficulty, and the button is pressed, it should shift to forsaken.
                //If the game is in forsaken difficulty, and the button is pressed, it should shift to normal.
            {
                button1.Size = new Size(537, 112);
                button1.Location = new Point(360, 326);
                button1.Image = Forms_DG.Properties.Resources.DIFFICULTYFORSAKENBUTTON;
                IsNormalDifficulty = false;

            } else
            {
                button1.Size = new Size(506, 112);
                button1.Location = new Point(382, 322);
                button1.Image = Forms_DG.Properties.Resources.DIFFICULTYNORMALBUTTON;
                IsNormalDifficulty = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) //When START is clicked, the main UI screen shows which is done here.
        {
            pictureBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            pictureBox3.Visible = true;
            pictureBox3.Location = new Point(-7, -6);
            pictureBox3.Size = new Size(390, 390);
            pictureBox2.Visible = true;
            pictureBox2.Location = new Point(12, 12);
            pictureBox2.Size = new Size(360, 360);

            ShowInitiativeTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
         
        }

        public Point GetRowColumn(int MousePositionX, int MousePositionY)
        {
            double GridWidth = pictureBox2.Width;
            double SquareWidth = GridWidth / GridSize;
            int Column = (int)((MousePositionX-14-this.Left) / SquareWidth);
            int Row = (int)(((MousePositionY-this.Top) / SquareWidth)-1);
            return new Point(Column, Row);
            //MousePositionX = pictureBox2.Location.X;
        }

        public double GetAPToMove() //Big work in progress.
        {
            int APCalculated = 0;
            Point MoveTo = GetRowColumn(MousePosition.X,MousePosition.Y);
            double[] adjacentCells = new double[8];
            bool Moving = true;
            double bestCellDistance = 10000;
            int bestCell = 1;
            while (Moving)
            {
                //Debug.WriteLine("Player Position: {0}.", GhostPosition);
                //Debug.WriteLine("MoveTo Position:{0}.", MoveTo);
                if (GhostPosition == MoveTo)
                {
                    Moving = false;
                    APCalculated = 0;
                    GhostPosition = PlayerPosition;
                    break;
                }
                GhostPosition.Y -= 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[0] = 10000;
                }
                else
                {
                    adjacentCells[0] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    bestCellDistance = adjacentCells[0];
                }
                GhostPosition.X += 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[1] = 10000;
                }
                else
                {
                    adjacentCells[1] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    if (bestCellDistance > adjacentCells[1])
                    {
                        bestCell = 2;
                        bestCellDistance = adjacentCells[1];
                        //previousCell = adjacentCells[1];
                    }
                }
                GhostPosition.Y += 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[2] = 10000;
                }
                else
                {
                    adjacentCells[2] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    if (bestCellDistance > adjacentCells[2])
                    {
                        bestCell = 3;
                        bestCellDistance = adjacentCells[2];
                    }
                }
                GhostPosition.Y += 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[3] = 10000;
                }
                else
                {
                    adjacentCells[3] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    if (bestCellDistance > adjacentCells[3])
                    {
                        bestCell = 4;
                        bestCellDistance = adjacentCells[3];
                    }
                }
                GhostPosition.X -= 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[4] = 10000;
                }
                else
                {
                    adjacentCells[4] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    if (bestCellDistance > adjacentCells[4])
                    {
                        bestCell = 5;
                        bestCellDistance = adjacentCells[4];
                    }
                }
                GhostPosition.X -= 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[5] = 10000;
                }
                else
                {
                    adjacentCells[5] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    if (bestCellDistance > adjacentCells[5])
                    {
                        bestCell = 6;
                        bestCellDistance = adjacentCells[5];
                    }
                }
                GhostPosition.Y -= 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[6] = 10000;
                }
                else
                {
                    adjacentCells[6] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    if (bestCellDistance > adjacentCells[6])
                    {
                        bestCell = 7;
                        bestCellDistance = adjacentCells[6];
                    }
                }
                GhostPosition.Y -= 1;
                if (GhostPosition.Y < 0 || GhostPosition.Y > 16 || GhostPosition.X < 0 || GhostPosition.X > 16)
                {
                    adjacentCells[7] = 10000;
                }
                else
                {
                    adjacentCells[7] = Math.Pow(Convert.ToDouble(MoveTo.X - GhostPosition.X), 2) + Math.Pow(Convert.ToDouble(MoveTo.Y - GhostPosition.Y), 2);
                    if (bestCellDistance > adjacentCells[7])
                    {
                        bestCell = 8;
                        bestCellDistance = adjacentCells[7];
                    }
                }
                GhostPosition.X += 1;
                GhostPosition.Y += 1;
                switch (bestCell)
                {
                    case 1: 
                        GhostPosition.Y -= 1;
                        break;
                    case 2:
                        GhostPosition.Y -= 1;
                        GhostPosition.X += 1;
                        break;
                    case 3:
                        GhostPosition.X += 1;
                        break;
                    case 4:
                        GhostPosition.X += 1;
                        GhostPosition.Y += 1;
                        break;
                    case 5:
                        GhostPosition.Y += 1;
                        break;
                    case 6:
                        GhostPosition.Y += 1;
                        GhostPosition.X -= 1;
                        break;
                    case 7:
                        GhostPosition.X -= 1;
                        break;
                    case 8:
                        GhostPosition.X -= 1;
                        GhostPosition.Y -= 1;
                        break;

                }
                if (Moving)
                {
                    APCalculated += 1;
                }
                if (GhostPosition == MoveTo)
                {
                    Moving = false;
                    GhostPosition = PlayerPosition;
                }
            }//while
            //Debug.WriteLine("Best cell is currently {0}.", bestCell);
            return APCalculated;
        }

        public Point GetYX(int Row, int Column)
        {
            double GridWidth = pictureBox2.Width;
            double SquareWidth = GridWidth / GridSize;
            int Y = (int)(Row * SquareWidth);
            int X = (int)(Column * SquareWidth);
            return new Point(X, Y);
        }

        public System.Drawing.Point PointToClient(System.Drawing.Point p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            GenerateRoom();
            PlacingPictureBoxes(RoomWidth, RoomHeight);
            Debug.WriteLine("The width is {0} and height is {1}.",RoomWidth,RoomHeight);
            //SwitchToRandom();
            //Enemy.GenerateT1RoomEnemy("Common", pictureBox5);
            //Debug.WriteLine(pictureBox5.Visible);
            //Debug.WriteLine(pictureBox5.Image);
            //Enemy E1 = Enemy.GenerateT1RoomEnemy("Common",pictureBox5);
            //Debug.WriteLine(E1.Coins);
            //GenerateRoom();
            //GetWindowDetails();
            Point MousePositionUser = new Point(MousePosition.X,MousePosition.Y);
            Debug.WriteLine(PointToClient(MousePositionUser));
            Debug.WriteLine(GetRowColumn(MousePosition.X, MousePosition.Y));
            //Debug.WriteLine("The AP needed is {0}.",GetAPToMove());
            //PictureBoxLocation();
        }

        private void pictureBox4_Click(object sender, EventArgs e)//Initiative List
        {

        }

       
    }// Form1 Class

    class Hero
    {
        string HeroName = "";
        int MaxHP = 0;
        int CurrentHP = 0;
        int MaxAP = 0;
        int CurrentAP = 0;
        int PhysicalBP = 0;
        int PhysicalResist = 0;
        int MagicBP = 0;
        int MagicResist = 0;
        int Constitution = 0;
        int Strength = 0;
        int Dexterity = 0;
        int Intelligence = 0;
        int Faith = 0;
        int Luck = 0;
        int FireResist = 0;
        int FireBuildup = 0;
        int LightningResist = 0;
        int LightningBuildup = 0;
        int FrostResist = 0;
        int FrostBuildup = 0;
        int AcidResist = 0;
        int AcidBuildup = 0;
        int PetrifyResist = 0;
        int PetrifyBuildup = 0;
        int StunResist = 0;
        int StunBuildup = 0;
        int BleedResist = 0;
        int BleedBuildup = 0;
        int IllusionResist = 0;
        int IllusionBuildup = 0;
        int ToxicResist = 0;
        int ToxicBuildup = 0;
        int Coins = 0;

        string CurrentLocation = "";
        int Initiative = 0;
        int Range = 0;

        string RightHand = "";
        string LeftHand = "";
        string Helmet = "";
        string Chestpiece = "";
        string Leggings = "";
        string Boots = "";
        string Equipment1 = "";
        string Equipment2 = "";
        string Artifact = "";
        string Ammo1 = "";
        string Ammo2 = "";

        Hero()
        {

        }

    }// Hero Class

    class Item
    {
        string ItemName = "";
        string ItemType = "";
        string ItemPrefix = "";
        string ItemSuffix = "";
        string ItemDescription = "";
        int ItemTier = 0;
        int APCost = 0;
        int Damage1 = 0;
        // Needs a list of damage types
        int Range = 0;
        int BuyPrice = 0;
        int SellPrice = 0;
        int StrengthRequirement = 0;
        int DexterityRequirement = 0;
        int FaithRequirement = 0;
        int IntelligenceRequirement = 0;

        Item ()
        {

        }
    } //Item Class

    class Room
    {
        Room()
        {
            int NumberofEnemies = 0;
            int NumberofObstructions = 0;
            int CellSpaces = 0;
            List<string> Enemies = new List<string>(NumberofEnemies);
            List<string> Obstructions = new List<string>(NumberofObstructions);
        }
    }
    public class Enemy
    {
        string EnemyName = "";
        string EnemyType = "";
        string Prefix = "";
        string Suffix = "";
        int EnemyTier = 0;
        int MaxHP = 0;
        int CurrentHP = 0;
        int MaxAP = 0;
        int CurrentAP = 0;
        int PhysicalBP = 0;
        int PhysicalResist = 0;
        int PhysicalDMG = 0;
        int MagicBP = 0;
        int MagicResist = 0;
        int MagicDMG = 0;
        int FireResist = 0;
        int FireBuildup = 0;
        int FireDMG = 0;
        int LightningResist = 0;
        int LightningBuildup = 0;
        int LightningDMG = 0;
        int FrostResist = 0;
        int FrostBuildup = 0;
        int FrostDMG = 0;
        int AcidResist = 0;
        int AcidBuildup = 0;
        int AcidDMG = 0;
        int PetrifyResist = 0;
        int PetrifyBuildup = 0;
        int PetrifyDMG = 0;
        int StunResist = 0;
        int StunBuildup = 0;
        int StunDMG = 0;
        int BleedResist = 0;
        int BleedBuildup = 0;
        int BleedDMG = 0;
        int IllusionResist = 0;
        int IllusionBuildup = 0;
        int IllusionDMG = 0;
        int ToxicResist = 0;
        int ToxicBuildup = 0;
        int ToxicDMG = 0;
        public int Coins = 0;

        Point CurrentLocation = new Point(0,0);
        int Initiative = 0;
        int Range = 0;
        public PictureBox PictureBox = new PictureBox();

        public void GenerateEnemyName()
        {
            int NormalPrefixNumber = 4;
            int MagicPrefixNumber = 1;
            int AcidPrefixNumber = 1;
            int FrostPrefixNumber = 1;
            int FirePrefixNumber = 1;
            int LightningPrefixNumber = 1;
            int IllusionPrefixNumber = 1;
            int ToxicPrefixNumber = 1;
            int PetrifyPrefixNumber = 1;
            int StunPrefixNumber = 1;
            string[] NormalPrefixes = new string[NormalPrefixNumber];
            string[] MagicPrefixes = new string[MagicPrefixNumber];
            string[] AcidPrefixes = new string[AcidPrefixNumber];
            string[] FrostPrefixes = new string[FrostPrefixNumber];
            string[] FirePrefixes = new string[FirePrefixNumber];
            string[] LightningPrefixes = new string[LightningPrefixNumber];
            string[] IllusionPrefixes = new string[IllusionPrefixNumber];
            string[] ToxicPrefixes = new string[ToxicPrefixNumber];
            string[] PetrifyPrefixes = new string[PetrifyPrefixNumber];
            string[] StunPrefixes = new string[StunPrefixNumber];

            //Normal Prefix List
            NormalPrefixes[0] = "Tough ";
            NormalPrefixes[1] = "Weak ";
            NormalPrefixes[2] = "Combative ";
            NormalPrefixes[3] = "Leeching ";
            //Magic Prefix List
            MagicPrefixes[0] = "Enchanted ";
            //Acid Prefix List
            AcidPrefixes[0] = "Acidic ";
            //Frost Prefix List
            FrostPrefixes[0] = "Frosted ";
            //Fire Prefix List
            FirePrefixes[0] = "Blazing ";
            //Lightning Prefixes
            LightningPrefixes[0] = "Electric ";
            //Toxic Prefixes
            ToxicPrefixes[0] = "Toxic ";
            //Illusion Prefixes
            IllusionPrefixes[0] = "Tricky ";
            IllusionPrefixes[1] = "Illusion of ";
            //Petrify Prefixes
            PetrifyPrefixes[0] = "Stoner ";
            //Stun Prefixes
            StunPrefixes[0] = "Hard-Hitting ";

            //Type List
            //Common Types
            int CommonTypeNumber = 2;
            string[] CommonTypes = new string[CommonTypeNumber];
            CommonTypes[0] = "Skeleton ";
            CommonTypes[1] = "Walking Corpse ";
            //Uncommon Types
            int RareTypeNumber = 1;
            string[] RareTypes = new string[RareTypeNumber];
            RareTypes[0] = "Draugr ";
            //Legendary Types
            int LegendaryTypeNumber = 2;
            string[] LegendaryTypes = new string[LegendaryTypeNumber];
            LegendaryTypes[0] = "Skeleton Lord ";
            LegendaryTypes[1] = "Draugr Overlord ";
            //Forsaken Types
            int ForsakenTypeNumber = 1;
            string[] ForsakenTypes = new string[ForsakenTypeNumber];
            ForsakenTypes[0] = "Lich King ";

            //Suffix List

        }

        public static Enemy GenerateT1RoomEnemy(string Rarity, PictureBox enemyPictureBox)
        {
            Random r = new Random(); // RE = ReturnedEnemy
            Enemy RE = new Enemy();
            switch (Rarity)
            {
                case "Common": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Rare": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Legendary":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Forsaken":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;
            }
            return RE;
        }

        public static Enemy GenerateT2RoomEnemy(string Rarity, PictureBox enemyPictureBox)
        {
            Random r = new Random(); // RE = ReturnedEnemy
            Enemy RE = new Enemy();
            switch (Rarity)
            {
                case "Common": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Rare": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Legendary":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Forsaken":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;
            }
            return RE;
        }

        public static Enemy GenerateT3RoomEnemy(string Rarity, PictureBox enemyPictureBox)
        {
            Random r = new Random(); // RE = ReturnedEnemy
            Enemy RE = new Enemy();
            switch (Rarity)
            {
                case "Common": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Rare": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Legendary":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Forsaken":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;
            }
            return RE;
        }

        public static Enemy GenerateT4RoomEnemy(string Rarity, PictureBox enemyPictureBox)
        {
            Random r = new Random(); // RE = ReturnedEnemy
            Enemy RE = new Enemy();
            switch (Rarity)
            {
                case "Common": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Rare": // Done
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Legendary":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;

                case "Forsaken":
                    RE.MaxHP = r.Next(12, 27);
                    RE.MaxAP = 1;
                    RE.MagicBP = 0;
                    RE.PhysicalBP = 0;
                    RE.PhysicalDMG = r.Next(1, 3);
                    RE.Range = 1;
                    RE.Coins = r.Next(0, 3);
                    enemyPictureBox.Visible = true;
                    enemyPictureBox.Image = Forms_DG.Properties.Resources.T1Common;
                    RE.PictureBox.Image = enemyPictureBox.Image;
                    break;
            }
            return RE;
        }
    }
}
