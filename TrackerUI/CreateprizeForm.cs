using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;
using System.Data.SqlClient;

namespace TrackerUI
{
    public partial class CreateprizeForm : Form
    {
        IPrizeRequester callingForm;
        PrizeType prizeType;
        string _prize;

        public CreateprizeForm()
        {
            InitializeComponent();
        }
        public CreateprizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                PrizeModel prize = new PrizeModel(placeNumberTextBox.Text, placeNameTextBox.Text,
                                                  _prize, prizeType);

                
                GlobalConfig.Connection.createPrize(prize);
                callingForm.prizeComplete(prize);

                this.Close();

                //placeNameTextBox.Text = "";
                //placeNumberTextBox.Text = "";
                //prizePercentageTextBox.Text = "0";
                //prizeAmountTextBox.Text = "0";

            }
            else
            {
                MessageBox.Show("Invalid form");
            }
            

        }

        private bool validateForm()
        {
            bool output = true;

            if (!validatePlaceNumber())
            {
                output = false;
            }

            if (!validatePlaceName())
            {
                output = false;
            }

            if (!validatePrize())
            {
                output = false;
            }
            return output;
        }

        private bool validatePlaceNumber()
        {
            bool output = true;
            int placeNumber = 0;
            bool isPlaceValid = Int32.TryParse(placeNumberTextBox.Text, out placeNumber);
            if(!isPlaceValid)
            {
                output = false;
            }
            if (placeNumber < 1)
            {
                output = false;
            }

            return output;
        }

        private bool validatePlaceName()
        {
            bool output = true;
            if (placeNameTextBox.Text.Length <= 0)
            {
                output = false;
            }
            return output;
        }

        private bool validatePrize()
        {
            bool output = true;
            decimal prizeAmount;
            double prizePercentage;
            bool isPrizeAmountValid = decimal.TryParse(prizeAmountTextBox.Text, out prizeAmount);
            bool isPrizePercentageValid = double.TryParse(prizePercentageTextBox.Text, out prizePercentage);

            if (prizeAmountTextBox.Text.Length > 0 && prizePercentageTextBox.Text.Length > 0)
            {
                output = false; 
            }
            else
            {

                if (isPrizeAmountValid)
                {
                    prizeType = PrizeType.Amount;
                }

                if (!isPrizePercentageValid)
                {
                    prizeType = PrizeType.Percentage;
                }

                switch (prizeType)
                {
                    case PrizeType.Amount:
                        if (prizeAmount <= 0)
                        {
                            output = false;
                        }
                        else
                        {
                            _prize = prizeAmountTextBox.Text;
                        }
                        break;
                    case PrizeType.Percentage:
                        if (prizePercentage <= 0)
                        {
                            output = false;
                        }
                        else
                        {
                            _prize = prizePercentageTextBox.Text;
                        }
                        break;
                    default:
                        output = false;
                        break;
                }
            }
            


            return output;
        }

        private void prizeAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (prizeAmountTextBox.Text.Length > 0)
            {
                prizePercentageTextBox.Enabled = false;
            }
            else
            {
                prizePercentageTextBox.Enabled = true;
            }
        }

        private void prizePercentageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (prizePercentageTextBox.Text.Length > 0)
            {
                prizeAmountTextBox.Enabled = false;
            }
            else
            {
                prizeAmountTextBox.Enabled = true;
            }
        }
    }
}
