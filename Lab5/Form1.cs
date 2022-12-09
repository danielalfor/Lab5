using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Lab 5, By Daniel Alfonso
        /*This program contain different functions that send and receive data
         Generate random number, use and display lists*/

        //Global variables
        const string PROGRAMMER = "DANIEL";
        //Global variable for attemps allowed
        int attemps = 0;

        //Function receives two integers and returns a random number on that range of values
        private int GetRandom(int min, int max)
        {
            //create random object x, and assign random value to z on the input range
            Random x = new Random();
            int z = x.Next(min, max);
            return z;
        }
        //When form loads, sets up properties, hide groups and generate random code
        private void Form1_Load(object sender, EventArgs e)
        {
            //Update form name
            this.Text += " " + PROGRAMMER;
            //Hide groups
            grpChoose.Hide();
            grpText.Hide();
            grpStats.Hide();
            //Put Cursor in code textbox
            txtCode.Focus();
            int min = 100000;
            int max = 200000;
            //Generate random number and display it to lblCode
            lblCode.Text = GetRandom(min, max).ToString();
        }

            //validates code entered by user
            private void btnLogin_Click(object sender, EventArgs e)
            {
                //validates input code, breaks if code matches or there is a wrong attemp
                //closes when 3rd wrong attemp is entered
                while (attemps != 3)
                {
                    attemps++;
                    if (txtCode.Text == lblCode.Text)
                    {
                        grpChoose.Show();
                        grpLogin.Enabled = false;
                        break;
                    }
                    else if (attemps == 3)
                    {
                        MessageBox.Show(attemps.ToString() + " Attemps to login.\n Account locked - Closing program.", PROGRAMMER);
                        Close();
                    }
                    else
                    {
                        //MessageBox.Show(attemps.ToString() + " Incorrect code(s) entered.\n Try again " + (3 - attemps).ToString() + " left.", PROGRAMMER);
                        MessageBox.Show(attemps.ToString() + " Incorrect code(s) entered.\n Try again - only 3 attemps allowed.", PROGRAMMER);
                        txtCode.SelectAll();
                        break;
                    }
                }
            }
            //Clears Text Group
            private void ResetTextGrp()
            {
                txtString1.Clear();
                txtString2.Clear();
                chkSwap.Checked = false;
                lblResults.Text = "";
                AcceptButton = btnJoin;
                CancelButton = btnReset;
                txtString1.Focus();
            }
            //Clears Stats Group
            private void ResetStatsGrp()
            {
                nudHowMany.Value = 10;
                lblSum.Text = "";
                lblMean.Text = "";
                lblOdd.Text = "";
                lstNumbers.Items.Clear();
                nudHowMany.Focus();
                AcceptButton = btnGenerate;
                CancelButton = btnClear;
            }
            //Evaluates which rad button is pressed and displays options
            private void SetupOption()
            {
                if (radStats.Checked)
                {
                    grpStats.Visible = true;
                    grpText.Visible = false;
                }
                else
                {
                    grpText.Visible = true;
                    grpStats.Visible = false;
                    txtString1.Focus();
                }
            }
            //Calls function to setup Text group if radText checked
            private void radText_CheckedChanged(object sender, EventArgs e)
            {
                SetupOption();
                ResetStatsGrp();
            }
            //Calls function to setup Stats group if radStats checked
            private void radStats_CheckedChanged(object sender, EventArgs e)
            {
                SetupOption();
                ResetTextGrp();
            }
            //Calls function to clear Text Group
            private void btnReset_Click(object sender, EventArgs e)
            {
                ResetTextGrp();
            }
            //Calls function to clear Stats Group
            private void btnClear_Click(object sender, EventArgs e)
            {
                ResetStatsGrp();
            }

            //Swaps to input referenced strings
            private void SwapString(ref string text1, ref string text2)
            {
                string temporal = text1;
                text1 = text2;
                text2 = temporal;
            }
            //Checks if both strings have content
            private bool CheckInput()
            {
                if ((txtString1.Text != "") && (txtString2.Text != ""))
                    return true;
                else
                    return false;
            }
            //If Checked property changed to true, go to swap strings
            private void chkSwap_CheckedChanged(object sender, EventArgs e)
            {
                if (CheckInput() && chkSwap.Checked == true)
                {
                    string string1 = txtString1.Text;
                    string string2 = txtString2.Text;
                    SwapString(ref string1, ref string2);
                    txtString1.Text = string1;
                    txtString2.Text = string2;
                    lblResults.Text = "Strings have been swapped";
                }
            }
            //Joins the two strings and display result
            private void btnJoin_Click(object sender, EventArgs e)
            {
                if (CheckInput())
                {
                    lblResults.Text = "First string = " + txtString1.Text
                        + "\nSecond string = " + txtString2.Text
                        + "\nJoined = " + txtString1.Text + "-->" + txtString2.Text;

                }
            }
            //Counts characters in strings and display results
            private void btnAnalyze_Click(object sender, EventArgs e)
            {
                if (CheckInput())
                {
                    lblResults.Text = "First string = " + txtString1.Text
                        + "\n Characters = " + txtString1.TextLength
                        + "\nSecond string = " + txtString2.Text
                        + "\n Characters = " + txtString2.TextLength;
                }
            }
            //Generate list, calculate addition and mean by calling functions and display results
            private void btnGenerate_Click(object sender, EventArgs e)
            {
                //create random object x, and clear list
                Random x = new Random(733);
                lstNumbers.Items.Clear();

                //run for loop filling listbox with random doubles            
                for (int i = 0; i < nudHowMany.Value; i++)
                {
                    int randomNumber = x.Next(1000, 5000 + 1);
                    lstNumbers.Items.Add(randomNumber.ToString());
                }
                //Call funcion AddList to add and display result
                lblSum.Text = AddList().ToString("n0");

                //calculate and display mean
                double mean = AddList() / (double)lstNumbers.Items.Count;
                lblMean.Text = mean.ToString("n2");

                //call function Odd and display result
                lblOdd.Text = CountOdd().ToString();
            }

            //Function runs a while loop to add up items in the list
            private int AddList()
            {
                int suma = 0;
                int j = 0;
                while (j < lstNumbers.Items.Count)
                {
                    suma += Convert.ToInt32(lstNumbers.Items[j]);
                    j++;
                }
                return suma;
            }
            //Function runs a for loop to identify and count odd numbers in list
            private int CountOdd() {
                int oddNumbers = 0;
                int j = 0;
                do
                {
                    if (Convert.ToInt32(lstNumbers.Items[j]) % 2 != 0)
                        oddNumbers++;
                    j++;
                } while (j < lstNumbers.Items.Count);
                return oddNumbers;

            }
        }
    }


