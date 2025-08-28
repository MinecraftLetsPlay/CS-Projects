using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Digital_Storage_Units_Calculator
{
    public partial class Form1 : Form
    {
        private Dictionary<string, decimal> unitFactors; // Create Dictionary

        public Form1()
        {
            // Main Initialize
            InitializeComponent();
            InitializeUnitFactors();
            InitializeEventHandlers();
        }

        private void InitializeUnitFactors()
        {
            unitFactors = new Dictionary<string, decimal> // Data Units Math Registry
            {
                { "Kib", (decimal)Math.Pow(2, 10) },
                { "Mib", (decimal)Math.Pow(2, 20) },
                { "Gib", (decimal)Math.Pow(2, 30) },
                { "Tib", (decimal)Math.Pow(2, 40) },
                { "Pib", (decimal)Math.Pow(2, 50) },
                { "Eib", (decimal)Math.Pow(2, 60) },
                { "Zib", (decimal)Math.Pow(2, 70) },
                { "Yib", (decimal)Math.Pow(2, 80) },
                { "KB", (decimal)Math.Pow(10, 3) },
                { "MB", (decimal)Math.Pow(10, 6) },
                { "GB", (decimal)Math.Pow(10, 9) },
                { "TB", (decimal)Math.Pow(10, 12) },
                { "PB", (decimal)Math.Pow(10, 15) },
                { "EB", (decimal)Math.Pow(10, 18) },
                { "ZB", (decimal)Math.Pow(10, 21) },
                { "YB", (decimal)Math.Pow(10, 24) }
            };
        }

        private void InitializeEventHandlers() // Initialize individual event handler for each textbox
        {
            KiBTextbox.Leave += (s, e) => OnLeave("Kib");
            MiBTextbox.Leave += (s, e) => OnLeave("Mib");
            GiBTextbox.Leave += (s, e) => OnLeave("Gib");
            TiBTextbox.Leave += (s, e) => OnLeave("Tib");
            PiBTextbox.Leave += (s, e) => OnLeave("Pib");
            EiBTextbox.Leave += (s, e) => OnLeave("Eib");
            ZiBTextbox.Leave += (s, e) => OnLeave("Zib");
            YiBTextbox.Leave += (s, e) => OnLeave("Yib");
            KBTextbox.Leave += (s, e) => OnLeave("KB");
            MBTextbox.Leave += (s, e) => OnLeave("MB");
            GBTextbox.Leave += (s, e) => OnLeave("GB");
            TBTextbox.Leave += (s, e) => OnLeave("TB");
            PBTextbox.Leave += (s, e) => OnLeave("PB");
            EBTextbox.Leave += (s, e) => OnLeave("EB");
            ZBTextbox.Leave += (s, e) => OnLeave("ZB");
            YBTextbox.Leave += (s, e) => OnLeave("YB");
            BitTextbox.Leave += (s, e) => OnLeave("Bit");
            Bit2Textbox.Leave += (s, e) => OnLeave("Bit2");
            ByteTextbox.Leave += (s, e) => OnLeave("Byte");
            Byte2Textbox.Leave += (s, e) => OnLeave("Byte2");
        }

        private void OnLeave(string unit)
        {
            var textBox = GetTextBox(unit);

            if (textBox == null) // Error Debug
            {
                MessageBox.Show($"TextBox for {unit} not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If the text box is empty, do nothing
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                return;
            }

            // Test for Numerical input
            if (decimal.TryParse(textBox.Text, out decimal value))
            {
                if (value == 0)
                {
                    MessageBox.Show("Zero is not allowed", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Clear();
                }
                else
                {
                    UpdateValues(value, unit);
                }
            }
            else
            {
                MessageBox.Show("Only numbers are allowed", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Clear(); // Clears input
            }
        }

        private System.Windows.Forms.TextBox GetTextBox(string unit)
        {
            return Controls.Find(unit + "Textbox", true).FirstOrDefault() as System.Windows.Forms.TextBox; // Autoselect of the corrisponding Textbox
        }

        private void UpdateValues(decimal value, string unit)
        {
            decimal baseValue;
            if (unit == "Bit" || unit == "Bit2")
            {
                baseValue = value;
            }
            else if (unit == "Byte" || unit == "Byte2")
            {
                baseValue = value * 8; // 1 byte = 8 bits
            }
            else
            {
                baseValue = value * unitFactors[unit];
            }

            // Update the text boxes with the calculated values
            foreach (var kvp in unitFactors)
            {
                string targetUnit = kvp.Key;
                decimal targetValue = baseValue / kvp.Value;
                UpdateTextBox(targetUnit, targetValue);
                DisplayDifference(targetUnit, targetValue);
            }

            // Update both sets of bit and byte text boxes
            UpdateTextBox("Bit", baseValue);
            UpdateTextBox("Byte", baseValue / 8);
            UpdateTextBox("Bit2", baseValue);
            UpdateTextBox("Byte2", baseValue / 8);
        }

        private void UpdateTextBox(string unit, decimal value) // Testing for Text not Null
        {
            var textBox = GetTextBox(unit);
            if (textBox != null)
            {
                textBox.Text = FormatValue(value);
            }
        }

        private string FormatValue(decimal value)
        {
            return value.ToString("G29");
        }

        private void DisplayDifference(string unit, decimal binaryValue)
        {
            string decimalUnit = unit.Replace("i", "").Replace("b", "B");

            if (unitFactors.TryGetValue(unit, out decimal binaryFactor) && unitFactors.TryGetValue(decimalUnit, out decimal decimalFactor))
            {
                // Calculate the equivalent decimal value for the given binary value
                decimal equivalentDecimalValue = binaryValue * (decimalFactor / binaryFactor);

                // Calculate the percentage difference between the binary value and its decimal equivalent
                decimal difference = (equivalentDecimalValue - binaryValue) / binaryValue * 100;

                // Assume you have labels to display these differences
                var label = GetLabel("Dif" + unit);
                if (label != null)
                {
                    label.Text = $"{difference:F2} %";
                }
            }
        }

        private System.Windows.Forms.Label GetLabel(string name)
        {
            return Controls.Find(name, true).FirstOrDefault() as System.Windows.Forms.Label;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            var textBoxes = new List<System.Windows.Forms.TextBox>
            {
                KiBTextbox, MiBTextbox, GiBTextbox, TiBTextbox, PiBTextbox, EiBTextbox, ZiBTextbox, YiBTextbox,
                KBTextbox, MBTextbox, GBTextbox, TBTextbox, PBTextbox, EBTextbox, ZBTextbox, YBTextbox,
                BitTextbox, Bit2Textbox, ByteTextbox, Byte2Textbox
            };

            bool validInputFound = false;

            foreach (var textBox in textBoxes)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text) && decimal.TryParse(textBox.Text, out decimal value))
                {
                    
                }
            }

            if (!validInputFound)
            {
                MessageBox.Show("Please enter a valid number in one of the fields.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}