using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpClickerGame
{
    public partial class Form1 : Form
    {
        //Created by DeeBeeR -- http://www.reddit.com/u/DeeBeeR
        //Modified by LeftBased
        int AutoSaveTime = 60; //Time for autosave
        public double Money = 0; //Amount of money
        public decimal MoneyPerSecond = 0; //Amount of money you get per second
        double MoneyPerClick = 1; //Amount of money you get per click
        double SelfClickedMoney = 0; //Amount of times you clicked
        int TotalTimesClicked = 0; //Total times you've clicked
        /*		a(n) = 3*2^n. 
(Formerly M2561)		150
3, 6, 12, 24, 48, 96, 192, 384, 768, 1536, 3072, 6144, 12288, 24576, 49152, 98304, 196608, 393216, 786432, 1572864, 3145728, 
6291456, 12582912, 25165824, 50331648, 100663296, 201326592, 402653184, 805306368, 1610612736, 3221225472, 6442450944, 12884901888
*/
        public int[] ClickEarner = new int[]  { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; //---------------------------current earner
        public double[] ClickPrice = new double[]    { 3, 48, 384, 6144, 98304, 1572864, 25165824, 402653184, 6442450944, 103079215104}; //----------current earner price
        double[] ClickPriceDef = new double[] { 3, 48, 384, 6144, 98304, 1572864, 25165824, 402653184, 6442450944, 103079215104}; //---current earner price (Defaults for Reset)
        public double[] ClickMPS = new double[]    { 0.6,  3,  24,   96,    384,    1536,     6144,     24576,      98304, 393216 }; //money per second
 
        private decimal TotalMPS()
        {
            return Convert.ToDecimal(MoneyPerClick * (ClickEarner[0] * ClickMPS[0]) + (ClickEarner[1] * ClickMPS[1]) + (ClickEarner[2] * ClickMPS[2]) + 
            (ClickEarner[3] * ClickMPS[3]) + (ClickEarner[4] * ClickMPS[4]) + (ClickEarner[5] * ClickMPS[5]) + (ClickEarner[6] * ClickMPS[6]) + 
            (ClickEarner[7] * ClickMPS[7]) + (ClickEarner[8] * ClickMPS[8]) + (ClickEarner[9] * ClickMPS[9]));
        }
        public double recalculateCosts(double BasePrice, int Number)
        {
            for (int i = 0; i < Number; i++)
            {
                BasePrice *= 1.15;
            }

            return BasePrice;
        }
        public void recalculateAll()
        {
            //ClickerPrice = recalculateCosts(ClickerPrice, Clicker);
            ClickPrice[0]  = recalculateCosts(ClickPrice[0], ClickEarner[0]);
            ClickPrice[1]  = recalculateCosts(ClickPrice[1], ClickEarner[1]);
            ClickPrice[2]  = recalculateCosts(ClickPrice[2], ClickEarner[2]);
            ClickPrice[3]  = recalculateCosts(ClickPrice[3], ClickEarner[3]);
            ClickPrice[4]  = recalculateCosts(ClickPrice[4], ClickEarner[4]);
            ClickPrice[5]  = recalculateCosts(ClickPrice[5], ClickEarner[5]);
            ClickPrice[6]  = recalculateCosts(ClickPrice[6], ClickEarner[6]);
            ClickPrice[7]  = recalculateCosts(ClickPrice[7], ClickEarner[7]);
            ClickPrice[8]  = recalculateCosts(ClickPrice[8], ClickEarner[8]);
            ClickPrice[9]  = recalculateCosts(ClickPrice[9], ClickEarner[9]);
        }
        public void saveGame()
        {
            Properties.Settings.Default.Money = this.Money; //money
            Properties.Settings.Default.MoneyPerSecond = this.MoneyPerSecond; //money per second
            Properties.Settings.Default.MoneyPerClick = this.MoneyPerClick; //money per click
            Properties.Settings.Default.SelfClickedMoney = this.SelfClickedMoney; //self clicked money

            Properties.Settings.Default.TotalTimesClicked = this.TotalTimesClicked;

            Properties.Settings.Default.Click1 = this.ClickEarner[0];
            Properties.Settings.Default.Click2 = this.ClickEarner[1];
            Properties.Settings.Default.Click3 = this.ClickEarner[2];
            Properties.Settings.Default.Click4 = this.ClickEarner[3];
            Properties.Settings.Default.Click5 = this.ClickEarner[4];
            Properties.Settings.Default.Click6 = this.ClickEarner[5];
            Properties.Settings.Default.Click7 = this.ClickEarner[6];
            Properties.Settings.Default.Click8 = this.ClickEarner[7];
            Properties.Settings.Default.Click9 = this.ClickEarner[8];
            Properties.Settings.Default.Click10 = this.ClickEarner[9];

            Properties.Settings.Default.Save();
        }
        public void loadGame()
        {
            this.Money = Properties.Settings.Default.Money;
            this.MoneyPerSecond = Properties.Settings.Default.MoneyPerSecond;
            this.MoneyPerClick = Properties.Settings.Default.MoneyPerClick;
            this.SelfClickedMoney = Properties.Settings.Default.SelfClickedMoney;
            this.TotalTimesClicked = Properties.Settings.Default.TotalTimesClicked;

            this.ClickEarner[0] = Properties.Settings.Default.Click1;
            this.ClickEarner[1] = Properties.Settings.Default.Click2;
            this.ClickEarner[2] = Properties.Settings.Default.Click3;
            this.ClickEarner[3] = Properties.Settings.Default.Click4;
            this.ClickEarner[4] = Properties.Settings.Default.Click5;
            this.ClickEarner[5] = Properties.Settings.Default.Click6;
            this.ClickEarner[6] = Properties.Settings.Default.Click7;
            this.ClickEarner[7] = Properties.Settings.Default.Click8;
            this.ClickEarner[8] = Properties.Settings.Default.Click9;
            this.ClickEarner[9] = Properties.Settings.Default.Click10;
        }
        public void resetGame()
        {
            Properties.Settings.Default.Money = 0;
            Properties.Settings.Default.MoneyPerSecond = 0;
            Properties.Settings.Default.MoneyPerClick = 1;
            Properties.Settings.Default.SelfClickedMoney = 0;
            Properties.Settings.Default.TotalTimesClicked = 0;

            Properties.Settings.Default.Click1 = 0;
            Properties.Settings.Default.Click2 = 0;
            Properties.Settings.Default.Click3 = 0;
            Properties.Settings.Default.Click4 = 0;
            Properties.Settings.Default.Click5 = 0;
            Properties.Settings.Default.Click6 = 0;
            Properties.Settings.Default.Click7 = 0;
            Properties.Settings.Default.Click8 = 0;
            Properties.Settings.Default.Click9 = 0;
            Properties.Settings.Default.Click10 = 0;

            Properties.Settings.Default.Save();
            loadGame();

            this.MoneyPerClick = 15;
            this.ClickPrice[0] = (double)ClickPriceDef[0];
            this.ClickPrice[1] = (double)ClickPriceDef[1];
            this.ClickPrice[2] = (double)ClickPriceDef[2];
            this.ClickPrice[3] = (double)ClickPriceDef[3];
            this.ClickPrice[4] = (double)ClickPriceDef[4];
            this.ClickPrice[5] = (double)ClickPriceDef[5];
            this.ClickPrice[6] = (double)ClickPriceDef[6];
            this.ClickPrice[7] = (double)ClickPriceDef[7];
            this.ClickPrice[8] = (double)ClickPriceDef[8];
            this.ClickPrice[9] = (double)ClickPriceDef[9];
            recalculateAll();
        }
        public Form1()
        {
            InitializeComponent();
            //Center game to the screen
            CenterToScreen();

            // Load up save, if exists
            loadGame();
            recalculateAll();
        }
        private void tmrLPS_Tick(object sender, EventArgs e)
        {
            Parallel.Invoke(() =>
            {
                MoneyPerSecond = TotalMPS();
                Money += Convert.ToDouble(MoneyPerSecond / 15);
                lblLogsInfo.Text = String.Format("Money: {0:n0}\nMoney Per Second: {1:#,###,##0.#}\nClicked: {2}", Money, MoneyPerSecond, SelfClickedMoney);
                //----[Buy/Sell Upgrade]----\\
                button5.Enabled = (Math.Round(Money) >= ClickPrice[0]);
                button6.Enabled = ClickEarner[0] != 0;
                lblClickerInfo.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[0], ClickPrice[0]);
                //----[Buy/Sell Upgrade]----\\
                button7.Enabled = (Math.Round(Money) >= ClickPrice[1]);
                button8.Enabled = ClickEarner[1] != 0;
                label1.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[1], ClickPrice[1]);
                //----[Buy/Sell Upgrade]----\\
                button9.Enabled = (Math.Round(Money) >= ClickPrice[2]);
                button10.Enabled = ClickEarner[2] != 0;
                label2.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[2], ClickPrice[2]);
                //----[Buy/Sell Upgrade]----\\
                button11.Enabled = (Math.Round(Money) >= ClickPrice[3]);
                button12.Enabled = ClickEarner[3] != 0;
                label3.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[3], ClickPrice[3]);
                //----[Buy/Sell Upgrade]----\\
                button13.Enabled = (Math.Round(Money) >= ClickPrice[4]);
                button14.Enabled = ClickEarner[4] != 0;
                label4.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[4], ClickPrice[4]);
                //----[Buy/Sell Upgrade]----\\
                button15.Enabled = (Math.Round(Money) >= ClickPrice[5]);
                button16.Enabled = ClickEarner[5] != 0;
                label5.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[5], ClickPrice[5]);
                //----[Buy/Sell Upgrade]----\\
                button17.Enabled = (Math.Round(Money) >= ClickPrice[6]);
                button18.Enabled = ClickEarner[6] != 0;
                label6.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[6], ClickPrice[6]);
                //----[Buy/Sell Upgrade]----\\
                button19.Enabled = (Math.Round(Money) >= ClickPrice[7]);
                button20.Enabled = ClickEarner[7] != 0;
                label7.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[7], ClickPrice[7]);
                //----[Buy/Sell Upgrade]----\\
                button21.Enabled = (Math.Round(Money) >= ClickPrice[8]);
                button22.Enabled = ClickEarner[8] != 0;
                label8.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[8], ClickPrice[8]);
                //----[Buy/Sell Upgrade]----\\
                button23.Enabled = (Math.Round(Money) >= ClickPrice[9]);
                button24.Enabled = ClickEarner[9] != 0;
                label9.Text = String.Format("Earners: {0:n0}\nCost per: {1:n0}", ClickEarner[9], ClickPrice[9]);
            });  // closed threading
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult QuitResult = MessageBox.Show("Are you sure you want to wipe your save? This is not a reversible option!",
   "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation); //Set message box text

            if (QuitResult == DialogResult.Yes)
            {
                //Reset save
                resetGame();

                //Reload save
                loadGame();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            saveGame();
            MessageBox.Show("Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Money += MoneyPerClick; //Add Logs from ClickLogs
            SelfClickedMoney += MoneyPerClick; //Adds ClickLogs to manually clicked logs
            TotalTimesClicked++; //Add 1 to total times clicked
            lblLogsInfo.Focus(); //Focus label to prevent holding enter/return abuse

        }
        public void AddBusiness(int objIndex)
        {
            ClickEarner[objIndex]++;
            Money -= ClickPrice[objIndex];
            ClickPrice[objIndex] *= 1.15;
            ClickPrice[objIndex] = Math.Round(ClickPrice[objIndex]);
            Money = Math.Round(Money);
            lblLogsInfo.Focus();
        }
        public void RemBusiness(int objIndex)
        {
            Money = Math.Floor(Money);
            Money += ClickPrice[objIndex] / 1.95;
            ClickPrice[objIndex] /= 1.15;
            ClickPrice[objIndex] = Math.Round(ClickPrice[objIndex]);
            Money = Math.Floor(Money);
            lblLogsInfo.Focus();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            AddBusiness(0);
            lblLogsInfo.Focus();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            RemBusiness(0);
            lblLogsInfo.Focus();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            AddBusiness(1);
            lblLogsInfo.Focus();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            RemBusiness(1);
            lblLogsInfo.Focus();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            AddBusiness(2);
            lblLogsInfo.Focus();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            RemBusiness(2);
            lblLogsInfo.Focus();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            AddBusiness(3);
            lblLogsInfo.Focus();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            RemBusiness(3);
            lblLogsInfo.Focus();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            AddBusiness(4);
            lblLogsInfo.Focus();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            RemBusiness(4);
            lblLogsInfo.Focus();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            AddBusiness(5);
            lblLogsInfo.Focus();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            RemBusiness(5);
            lblLogsInfo.Focus();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            AddBusiness(6);
            lblLogsInfo.Focus();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            RemBusiness(6);
            lblLogsInfo.Focus();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            AddBusiness(7);
            lblLogsInfo.Focus();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            RemBusiness(7);
            lblLogsInfo.Focus();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            AddBusiness(8);
            lblLogsInfo.Focus();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            RemBusiness(8);
            lblLogsInfo.Focus();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            AddBusiness(9);
            lblLogsInfo.Focus();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            RemBusiness(9);
            lblLogsInfo.Focus();
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tmrAutoSave_Tick_1(object sender, EventArgs e)
        {
            if (AutoSaveTime > 0) { AutoSaveTime--; lblSaved.Visible = false; }
            else
            {
                saveGame();
                lblSaved.Visible = true;
                AutoSaveTime = 60;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult QuitResult = MessageBox.Show("Thanks for playing my game! Are you sure you wish to quit?",
               "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question); //Sets quit messagebox text

            if (QuitResult == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            DialogResult SaveResult =
                MessageBox.Show("Do you wish to save your game?",
                "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question); //Sets save messagebox text


            if (SaveResult == DialogResult.Yes)
            {
                saveGame();
            }

        }
    }
}