using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taschenrechner
{
    public partial class Form1 : Form
    {
        // Main Initialize

        private List<double> Numbers = new List<double>();  // Generate Numbers List
        private List<String> Operators = new List<String>();  // Generate Operators List
        private bool isDecimal = false;
        private string lastOperator = "";

        public Form1()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeEventHandlers()
        {
            // Iterate through all controls on the form
            foreach (Control control in Controls)
            {
                // Check if the control is a Button
                if (control is Button button)
                {
                    // Check if the button's Text is a number
                    int number;
                    if (int.TryParse(button.Text, out number))
                    {
                        // Assign the same event handler to all number buttons
                        button.Click += NumPadButton_Click;
                    }
                    else if (IsOperator(button.Text))
                    {
                        // Assign the same event handler to all operator buttons
                        button.Click += OperatorButton_Click;
                    }
                    else if (IsSpecialOperator(button))
                    {
                        button.Click += SpecialOperatorButton_Click;
                    }
                }
            }
        }

        private void NumPadButton_Click(object sender, EventArgs e)
        {
            // Common event handler for all number buttons
            Button button = (Button)sender;
            if (button.Text == ",")
            {
                // Handle comma button
                if (!isDecimal)
                {
                    isDecimal = true;
                    // You can also update the display to show the comma
                    UpdateDisplay();
                }
            }
            else
        {

            if (Operators.Count > 0 && Numbers.Count == Operators.Count)
            {
                // If the user started a new number, add a new entry to the Numbers list
                Numbers.Add(int.Parse(button.Text));
            }
            else
            {
                // If the user is adding digits to the current number, update the last entry in the Numbers list
                if (Numbers.Count > 0)
                {
                    Numbers[Numbers.Count - 1] = Numbers[Numbers.Count - 1] * 10 + int.Parse(button.Text);
                }
                else
                {
                    // If there are no numbers yet, start a new number
                    Numbers.Add(int.Parse(button.Text));
                }
            }
        }

            UpdateDisplay();
        }

        private void OperatorButton_Click(Object sender, EventArgs e)
        {
            // Common event handler for all operator buttons
            Button button = (Button)sender;

            Operators.Add(button.Text);

            UpdateDisplay();
        }

        private void SpecialOperatorButton_Click(Object sender, EventArgs e)
        {
            Button button = (Button)sender;
            double so_result = CalculateResult_Special(button.Name);

            Ergebnis_Anzeige.Text = so_result.ToString();
        }

        private bool IsOperator(string input)
        {
            return input == "+" || input == "-" || input == "X" || input == ":"; // Check for the specified Operators
        }

        private bool IsSpecialOperator(Button button)
        {
            return button.Name == "btnProzent" || button.Name == "btnQuadriert" || button.Name == "btnKubikx3" || button.Name == "btnQuadratwurzel" || button.Name == "btnKubikwurzel";
        }
        private double CalculateResult()
        {
            if (Numbers.Count == 0 || Operators.Count == 0 || Numbers.Count != Operators.Count + 1)
            {
                // Handle invalid input or incomplete expression
                return 0;
            }

            // Perform multiplication and division first
            for (int i = 0; i < Operators.Count; i++)
            {
                if (Operators[i] == "X" || Operators[i] == ":")
                {
                    double num1 = Numbers[i];
                    double num2 = Numbers[i + 1];
                    double result;

                    if (Operators[i] == "X")
                    {
                        result = num1 * num2;
                    }
                    else // ":"
                    {
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            // Handle division by zero
                            return 0;
                        }
                    }

                    // Replace the two numbers and the operator with the result
                    Numbers[i] = result;
                    Numbers.RemoveAt(i + 1);
                    Operators.RemoveAt(i);

                    // Adjust the index to account for the removed elements
                    i--;
                }
            }

            // Perform addition and subtraction second
            double finalResult = Numbers[0];
            for (int i = 0; i < Operators.Count; i++)
            {
                double nextNumber = Numbers[i + 1];

                switch (Operators[i])
                {
                    case "+":
                        finalResult += nextNumber;
                        break;
                    case "-":
                        finalResult -= nextNumber;
                        break;
                    default:
                        // Handle unexpected operators
                        return 0;
                }
            }

            return finalResult;
        }

        private double CalculateResult_Special(string buttonName)
        {
            double number = Numbers.Last();
            double SO_result = 0.0;

            switch (buttonName)
            {
                case "btnQuadriert": // Square (²)
                    lastOperator = "²";
                    SO_result = Math.Pow(number, 2);
                    UpdateDisplay();
                    break;

                case "btnKubikx3": // Cubic (³)
                    lastOperator = "³";
                    SO_result = Math.Pow(number, 3);
                    UpdateDisplay();
                    break;

                case "btnQuadratwurzel": // Square root (√)
                    if (number >= 0)
                    {
                        lastOperator = "√";
                        SO_result = Math.Sqrt(number);
                        UpdateDisplay();
                    }
                    else
                    {
                        // Handle invalid input for square root
                        MessageBox.Show("Invalid input for square root");
                    }
                    break;

                case "btnKubikwurzel": // Cubic root (³)
                    lastOperator = "∛";
                    SO_result = Math.Pow(number, 1.0 / 3.0);
                    UpdateDisplay();
                    break;

                case "btnProzent": // Percentage (%)
                                   
                    if (Numbers.Count >= 2) // Check if there are at least two numbers for percentage calculation
                    {
                        lastOperator = "%";
                        double percentage = Numbers.Last();
                        double baseNumber = Numbers[Numbers.Count - 2];
                        SO_result = baseNumber * (percentage / 100);
                        UpdateDisplay();
                        Numbers.Add(SO_result);
                       
                    }
                    else
                    {
                        // Handle invalid input for percentage
                        MessageBox.Show("Invalid input for percentage");
                    }
                    return SO_result;
            }
            Numbers.Clear(); // Clear the Numbers list
            Numbers.Add(SO_result); // Add the special operator result to the Numbers list

            Ergebnis_Anzeige.Text = SO_result.ToString();

            return SO_result;

        }

        private void UpdateDisplay()
        {
            string formulaText = string.Empty;

            if (lastOperator == "√" || lastOperator == "∛")
            {
                formulaText = lastOperator + Numbers.Last();
                lastOperator = string.Empty;
            }
            else
            {
                formulaText = string.Join(" ", Numbers.Zip(Operators, (n, op) => n + " " + op)) + " " + string.Join(" ", Numbers.Skip(Operators.Count)) + lastOperator;
                lastOperator = string.Empty;
            }

            // Display percentage separately
            if (Operators.Count > 0 && lastOperator == "%")
            {
                formulaText += " " + lastOperator;
            }

            Formel_Anzeige.Text = formulaText;

            if (isDecimal)
            {
                // Add a comma to the display
                Formel_Anzeige.Text += ",";
                isDecimal = false;
            }
        }

        private void btnErgebis_Click(object sender, EventArgs e)
        {
                double result = CalculateResult();
                Ergebnis_Anzeige.Text = result.ToString();
        }

        private void btnDEL_Click(object sender, EventArgs e)
        {
            // Handle the DEL button click
            if (Numbers.Count > 0 || Operators.Count > 0)
            {
                if (Operators.Count == 0 || Numbers.Count == Operators.Count)
                {
                    // If there are no operators or the user entered two operators in a row, delete the last number
                    if (Numbers.Count > 0)
                    {
                        Numbers.RemoveAt(Numbers.Count - 1);
                    }
                    else if (Operators.Count > 0)
                    {
                        // If there are no numbers but operators, delete the last operator
                        Operators.RemoveAt(Operators.Count - 1);
                    }
                }
                else
                {
                    // If there are operators, delete the last operator
                    Operators.RemoveAt(Operators.Count - 1);
                }
            }

            UpdateDisplay();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            // Handle the AC button click
            Numbers.Clear();
            Operators.Clear();
            Formel_Anzeige.Text = "";
            Ergebnis_Anzeige.Text = "";
            lastOperator = "";
        }
    }
}
