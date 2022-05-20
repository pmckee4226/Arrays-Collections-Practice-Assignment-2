using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Patrick McKee                    *
 * CIS123 - OOP                     *
 * May 19, 2022                     *
 * Ex 8-2: Use a rectangular array  */


// Patrick McKee - CIS123 - Ex8.2 
// Step 
namespace FutureValue
{
    public partial class frmFutureValue : Form
    {
        public frmFutureValue()
        {
            InitializeComponent();
        }

        // TODO: Declare the rectangular array and the row index here
        // Patrick McKee - CIS123 - Ex8.2 
        // Step 2 - Declare class variables for a row counter and a 
        // rectangular array of strings that provides for 10 rows & 4 columns

        string[,] calculationsTable = new string[10, 4];
        int row = 0;

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {

                    // Patrick McKee - CIS123 - Ex8.2 
                    // Step 3 - Add code that stores the values for each calculation
                    //in the next row of the array.

                    //INPUT
                    decimal monthlyInvestment =
                        Convert.ToDecimal(txtMonthlyInvestment.Text);
                    decimal yearlyInterestRate =
                        Convert.ToDecimal(txtInterestRate.Text);
                    int years = Convert.ToInt32(txtYears.Text);

                    //PROCESSING
                    int months = years * 12;
                    decimal interestRateMonthly = yearlyInterestRate / 12 / 100;

                    decimal futureValue = CalculateFutureValue(
                        monthlyInvestment, interestRateMonthly, months);
                    calculationsTable[row, 0] = monthlyInvestment.ToString("c");
                    calculationsTable[row, 1] = yearlyInterestRate.ToString("p1");
                    calculationsTable[row, 2] = years.ToString();
                    calculationsTable[row, 3] = futureValue.ToString("c");
                    row++;

                    //OUTPUT
                    txtFutureValue.Text = futureValue.ToString("c");
                    txtMonthlyInvestment.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }

        public bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";

            // Validate the Monthly Investment text box
            errorMessage += IsDecimal(txtMonthlyInvestment.Text, txtMonthlyInvestment.Tag.ToString());
            errorMessage += IsWithinRange(txtMonthlyInvestment.Text, txtMonthlyInvestment.Tag.ToString(), 1, 1000);

            // Validate the Yearly Interest Rate text box
            errorMessage += IsDecimal(txtInterestRate.Text, txtInterestRate.Tag.ToString());
            errorMessage += IsWithinRange(txtInterestRate.Text, txtInterestRate.Tag.ToString(), 1, 20);

            // Validate the Number of Years text box
            errorMessage += IsInt32(txtYears.Text, txtYears.Tag.ToString());
            errorMessage += IsWithinRange(txtYears.Text, txtYears.Tag.ToString(), 1, 40);

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }

        public static string IsPresent(string value, string name)
        {
            string msg = "";
            if (value == "")
            {
                msg += name + " is a required field.\n";
            }
            return msg;
        }

        public static string IsDecimal(string value, string name)
        {
            string msg = "";
            if (!Decimal.TryParse(value, out _))
            {
                msg += name + " must be a valid decimal value.\n";
            }
            return msg;
        }

        public static string IsInt32(string value, string name)
        {
            string msg = "";
            if (!Int32.TryParse(value, out _))
            {
                msg += name + " must be a valid integer value.\n";
            }
            return msg;
        }

        public static string IsWithinRange(string value, string name, decimal min, decimal max)
        {
            string msg = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    msg += name + " must be between " + min + " and " + max + ".\n";
                }
            }
            return msg;
        }
        private decimal CalculateFutureValue(decimal monthlyInvestment,
           decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                    * (1 + monthlyInterestRate);
            }

            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Patrick McKee - CIS123 - Ex8.2 
            // Step 5 - Test the program by making up to 10 future value calculations

            string message = "Inv/Mo.\tRate\tYears\tFuture Value\n";
            for (int i = 0; i < calculationsTable.GetLength(0); i++)
            {
                for (int j = 0; j < calculationsTable.GetLength(1); j++)
                {
                    message += calculationsTable[i, j] + "\t";
                }
                message += "\n";
            }
            MessageBox.Show(message, "Future Value Calculations");

            this.Close();
        }
 
    }
}