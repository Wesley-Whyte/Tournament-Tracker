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
                                                  prizeAmountTextBox.Text, prizePercentageTextBox.Text);

                
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

            if (!isPrizeAmountValid || !isPrizePercentageValid)
            {
                output = false;
            }
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }


            return output;
        }
    }
}
