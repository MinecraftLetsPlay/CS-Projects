using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Unit_Converter
{
    public partial class Form1 : Form
    {
        private Dictionary<(string, string), double> conversionFactors = new Dictionary<(string, string), double>();

        // Main initialization
        public Form1()
        {
            InitializeComponent();
            InitializeConversionFactors();
            PopulateUnitComboBoxes();

            InputUnitComboBox.SelectedIndex = 0;
            InputUnitComboBox2.SelectedIndex = 1;
            OutputUnitComboBox.SelectedIndex = 1;

            MethodComboBox.Items.AddRange(new string[] { "Calculate", "Compare" });
            MethodComboBox.SelectedIndex = 0;

            InputUnitComboBox.DropDownHeight = 200;
            InputUnitComboBox2.DropDownHeight = 200;
            OutputUnitComboBox.DropDownHeight = 200;

            Button_Run.Text = "Calculate";

            InputTextBox.Text = "1";
            InputTextBox2.Text = "1";
            OutputTextBox.Text = "0";
            LargerValueTextBox.Text = "";
        }

        // Populate ComboBoxes with unit options
        private void PopulateUnitComboBoxes()
        {
            var weightUnits = new string[] { "Kilogram", "Gram", "Pound", "Ounce" };
            var lengthUnits = new string[] { "Meter", "Decimeter", "Centimeter", "Millimeter", "Inch", "Foot" };
            var areaUnits = new string[] { "M²", "Dm²", "Cm²", "Inch²", "Foot²" };
            var volumeUnits = new string[] { "M³", "Dm³", "Cm³", "Centiliter", "Milliliter", "Liter" };
            var timeUnits = new string[] { "Seconds", "Minutes", "Hours", "Days" };
            var speedUnits = new string[] { "Km/h", "Miles/h", "M/s" };
            var temperatureUnits = new string[] { "Celsius", "Fahrenheit", "Kelvin" };
            var pressureUnits = new string[] { "Pascal", "Bar", "Psi" };
            var energyUnits = new string[] { "Joule", "Kilojoule", "Calorie" };

            AddUnitsToComboBox(InputUnitComboBox, weightUnits, lengthUnits, areaUnits, volumeUnits, timeUnits, speedUnits, temperatureUnits, pressureUnits, energyUnits);
            AddUnitsToComboBox(InputUnitComboBox2, weightUnits, lengthUnits, areaUnits, volumeUnits, timeUnits, speedUnits, temperatureUnits, pressureUnits, energyUnits);
            AddUnitsToComboBox(OutputUnitComboBox, weightUnits, lengthUnits, areaUnits, volumeUnits, timeUnits, speedUnits, temperatureUnits, pressureUnits, energyUnits);
        }

        private void AddUnitsToComboBox(ComboBox comboBox, params string[][] unitGroups)
        {
            foreach (var group in unitGroups)
            {
                foreach (var unit in group)
                {
                    comboBox.Items.Add(unit);
                }
                comboBox.Items.Add("---"); // Separator between groups
            }
        }

        private void InputUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Removes the focus from the ComboBox
            this.ActiveControl = null;
        }

        private void InputUnitComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Removes the focus from the ComboBox
            this.ActiveControl = null;
        }

        private void OutputUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Removes the focus from the ComboBox
            this.ActiveControl = null;
        }

        private void MethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;

            if (MethodComboBox.SelectedItem.ToString() == "Compare")
            {
                Label_OutputValue.Text = "Difference";
                Label_OutputUnit.Visible = false;
                OutputUnitComboBox.Visible = false;
                InputTextBox2.Visible = true;
                Info_Input2.Visible = true;
                InputUnitComboBox2.Visible = true;
                LargerValueTextBox.Visible = true;
                Button_Run.Text = "Compare";
            }
            else
            {
                Label_OutputValue.Text = "Output value";
                InputTextBox2.Visible = false;
                Info_Input2.Visible = false;
                InputUnitComboBox2.Visible = false;
                LargerValueTextBox.Visible = false;
                InputUnitComboBox.Visible = true;
                OutputUnitComboBox.Visible = true;
                OutputTextBox.Width = 175;
                Button_Run.Text = "Calculate";
            }
        }

        private void Button_Run_Click(object sender, EventArgs e)
        {
            try
            {
                if (MethodComboBox.SelectedItem.ToString() == "Calculate")
                {
                    if (double.TryParse(InputTextBox.Text, out double inputValue))
                    {
                        string inputUnit = InputUnitComboBox.SelectedItem.ToString();
                        string outputUnit = OutputUnitComboBox.SelectedItem.ToString();
                        double outputValue = ConvertUnits(inputValue, inputUnit, outputUnit);

                        OutputTextBox.Text = outputValue.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number.");
                    }
                }
                else if (MethodComboBox.SelectedItem.ToString() == "Compare")
                {
                    if (double.TryParse(InputTextBox.Text, out double value1) &&
                        double.TryParse(InputTextBox2.Text, out double value2))
                    {
                        string unit1 = InputUnitComboBox.SelectedItem.ToString();
                        string unit2 = InputUnitComboBox2.SelectedItem.ToString();

                        // Determine the common unit for comparison
                        string commonUnit = GetCommonUnit(unit1, unit2);

                        // Dynamically set the OutputUnit to the common unit for comparison
                        OutputUnitComboBox.SelectedItem = commonUnit;

                        var (percentDifference, largerValue) = CompareValues(value1, unit1, value2, unit2, commonUnit);

                        OutputTextBox.Width = 200;
                        OutputTextBox.Text = $"Difference: {percentDifference:F2}%";
                        LargerValueTextBox.Text = largerValue;
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid numbers for both values.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeConversionFactors()
        {
            // Base conversion factors
            conversionFactors = new Dictionary<(string, string), double>
    {
        // Weight
        { ("Kilogram", "Gram"), 1000 },
        { ("Kilogram", "Pound"), 2.20462 },
        { ("Pound", "Kilogram"), 0.453592 },
        { ("Pound", "Gram"), 453.592 },
        { ("Ounce", "Gram"), 28.3495 },
        { ("Gram", "Ounce"), 0.035274 },
        { ("Ounce", "Pound"), 0.0625 },
        { ("Pound", "Ounce"), 16 },
        { ("Ounce", "Kilogram"), 0.0283495 },

        // Length
        { ("Meter", "Centimeter"), 100 },
        { ("Meter", "Millimeter"), 1000 },
        { ("Meter", "Inch"), 39.3701 },
        { ("Meter", "Foot"), 3.28084 },
        { ("Centimeter", "Meter"), 0.01 },
        { ("Millimeter", "Meter"), 0.001 },
        { ("Inch", "Meter"), 0.0254 },
        { ("Foot", "Meter"), 0.3048 },
        { ("Meter", "Decimeter"), 10 },
        { ("Decimeter", "Meter"), 0.1 },
        { ("Inch", "Foot"), 0.0833333 },
        { ("Foot", "Inch"), 12 },
        { ("Inch", "Millimeter"), 25.4 },
        { ("Inch", "Centimeter"), 2.54 },
        { ("Inch", "Decimeter"), 0.254 },

        // Area
        { ("M²", "Cm²"), 10000 },
        { ("M²", "Inch²"), 1550.0031 },
        { ("M²", "Foot²"), 10.7639 },
        { ("Cm²", "M²"), 0.0001 },
        { ("Inch²", "M²"), 0.00064516 },
        { ("Foot²", "M²"), 0.092903 },
        { ("M²", "Dm²"), 100 },
        { ("Dm²", "M²"), 0.01 },
        { ("Inch²", "Foot²"), 0.00694 },
        { ("Foot²", "Inch²"), 144 },

        // Volume
        { ("M³", "Dm³"), 1000 },
        { ("Dm³", "M³"), 0.001 },
        { ("M³", "Cm³"), 1000000 },
        { ("Cm³", "M³"), 0.000001 },
        { ("Dm³", "Cm³"), 1000 },
        { ("Cm³", "Dm³"), 0.001 },
        { ("Liter", "Milliliter"), 1000 },
        { ("Milliliter", "Liter"), 0.001 },
        { ("Liter", "Centiliter"), 100 },
        { ("Centiliter", "Liter"), 0.01 },
        { ("M³", "Liter"), 1000 },
        { ("Liter", "M³"), 0.001 },
        { ("Cm³", "Liter"), 0.001 },
        { ("Liter", "Cm³"), 1000 },
        { ("Dm³", "Liter"), 1 },
        { ("Liter", "Dm³"), 1 },
        { ("Milliliter", "Cm³"), 1 },
        { ("Cm³", "Milliliter"), 1 },

        // Speed
        { ("Km/h", "Miles/h"), 0.621371 },
        { ("Miles/h", "Km/h"), 1.60934 },
        { ("M/s", "Km/h"), 3.6 },
        { ("Km/h", "M/s"), 0.277778 },

        // Time
        { ("Seconds", "Minutes"), 1.0 / 60 },
        { ("Minutes", "Seconds"), 60 },
        { ("Minutes", "Hours"), 1.0 / 60 },
        { ("Hours", "Minutes"), 60 },
        { ("Hours", "Days"), 1.0 / 24 },
        { ("Days", "Hours"), 24 },

        // Pressure
        { ("Pascal", "Bar"), 0.00001 },
        { ("Bar", "Pascal"), 100000 },
        { ("Pascal", "Psi"), 0.000145038 },
        { ("Psi", "Pascal"), 6894.76 },
        { ("Bar", "Psi"), 14.5038 },
        { ("Psi", "Bar"), 0.0689476 },

        // Energy
        { ("Joule", "Kilojoule"), 0.001 },
        { ("Kilojoule", "Joule"), 1000 },
        { ("Joule", "Calorie"), 0.239006 },
        { ("Calorie", "Joule"), 4.184 },
        { ("Kilojoule", "Calorie"), 239.006 },
        { ("Calorie", "Kilojoule"), 0.004184 },
    };

            // Dynamically add all possible conversion combinations
            AddAllCombinations();
        }

        private void AddAllCombinations()
        {
            var additionalConversions = new Dictionary<(string, string), double>();

            foreach (var conversion in conversionFactors)
            {
                foreach (var intermediate in conversionFactors.Keys)
                {
                    if (conversion.Key.Item2 == intermediate.Item1 && !conversionFactors.ContainsKey((conversion.Key.Item1, intermediate.Item2)))
                    {
                        double newFactor = conversion.Value * conversionFactors[intermediate];
                        additionalConversions[(conversion.Key.Item1, intermediate.Item2)] = newFactor;
                    }
                }
            }

            foreach (var conversion in additionalConversions)
            {
                conversionFactors[conversion.Key] = conversion.Value;
            }
        }

        private string GetCommonUnit(string unit1, string unit2)
        {
            // Handle time-based units (Seconds, Minutes, Hours, etc.)
            if (unit1.Contains("Second") || unit2.Contains("Second"))
            {
                return "Seconds";
            }
            else if (unit1.Contains("Minute") || unit2.Contains("Minute"))
            {
                return "Minutes";
            }
            else if (unit1.Contains("Hour") || unit2.Contains("Hour"))
            {
                return "Hours";
            }

            // Handle length-based units (Meter, Centimeter, Millimeter, etc.)
            if (unit1.Contains("Meter") || unit2.Contains("Meter"))
            {
                return "Meter";
            }
            else if (unit1.Contains("Centimeter") || unit2.Contains("Centimeter"))
            {
                return "Centimeter"; 
            }
            else if (unit1.Contains("Millimeter") || unit2.Contains("Millimeter"))
            {
                return "Millimeter"; 
            }
            else if (unit1.Contains("Decimeter") || unit2.Contains("Decimeter"))
            {
                return "Decimeter"; 
            }

            // Handle volume-based units (Liter, Milliliter, Centiliter, etc.)
            if (unit1.Contains("Liter") || unit2.Contains("Liter"))
            {
                return "Liter";
            }
            else if (unit1.Contains("Milliliter") || unit2.Contains("Milliliter"))
            {
                return "Milliliter";
            }
            else if (unit1.Contains("Centiliter") || unit2.Contains("Centiliter"))
            {
                return "Centiliter";
            }

            // Handle weight-based units (Kilogram, Gram, Pound, etc.)
            if (unit1.Contains("Kilogram") || unit2.Contains("Kilogram"))
            {
                return "Kilogram";
            }
            else if (unit1.Contains("Gram") || unit2.Contains("Gram"))
            {
                return "Gram";
            }
            else if (unit1.Contains("Pound") || unit2.Contains("Pound"))
            {
                return "Pound";
            }

            // Handle speed-based units (Km/h, Miles/h, M/s, etc.)
            if (unit1.Contains("Km/h") || unit2.Contains("Km/h"))
            {
                return "Km/h";
            }
            else if (unit1.Contains("Miles/h") || unit2.Contains("Miles/h"))
            {
                return "Miles/h";
            }
            else if (unit1.Contains("M/s") || unit2.Contains("M/s"))
            {
                return "M/s"; 
            }

            // Default case: return the first unit if no specific category found
            return unit1;
        }

        private double ConvertUnits(double inputValue, string inputUnit, string outputUnit)
        {
            if (inputUnit == outputUnit)
            {
                return inputValue;
            }

            // Special handling for temperature conversions
            if ((inputUnit == "Celsius" && outputUnit == "Kelvin") ||
                (inputUnit == "Kelvin" && outputUnit == "Celsius") ||
                (inputUnit == "Celsius" && outputUnit == "Fahrenheit") ||
                (inputUnit == "Fahrenheit" && outputUnit == "Celsius") ||
                (inputUnit == "Fahrenheit" && outputUnit == "Kelvin") ||
                (inputUnit == "Kelvin" && outputUnit == "Fahrenheit"))
            {
                return ConvertTemperature(inputValue, inputUnit, outputUnit);
            }

            // Generic conversion using predefined factors
            if (conversionFactors.TryGetValue((inputUnit, outputUnit), out double factor))
            {
                return inputValue * factor;
            }

            throw new Exception("Conversion not available.");
        }

        // Temperature conversion logic
        private double ConvertTemperature(double value, string inputUnit, string outputUnit)
        {
            if (inputUnit == "Celsius" && outputUnit == "Kelvin")
            {
                return value + 273.15;
            }
            else if (inputUnit == "Kelvin" && outputUnit == "Celsius")
            {
                return value - 273.15;
            }
            else if (inputUnit == "Celsius" && outputUnit == "Fahrenheit")
            {
                return (value * 1.8) + 32;
            }
            else if (inputUnit == "Fahrenheit" && outputUnit == "Celsius")
            {
                return (value - 32) / 1.8;
            }
            else if (inputUnit == "Fahrenheit" && outputUnit == "Kelvin")
            {
                return ((value - 32) / 1.8) + 273.15;
            }
            else if (inputUnit == "Kelvin" && outputUnit == "Fahrenheit")
            {
                return ((value - 273.15) * 1.8) + 32;
            }

            throw new Exception("Temperature conversion not available.");
        }

        // Comparison logic
        private (double, string) CompareValues(double value1, string unit1, double value2, string unit2, string outputUnit)
        {
            double value1Converted = ConvertUnits(value1, unit1, outputUnit);
            double value2Converted = ConvertUnits(value2, unit2, outputUnit);

            double difference = Math.Abs(value1Converted - value2Converted);
            double percentDifference = (difference / ((value1Converted + value2Converted) / 2)) * 100;

            string largerValue;
            if (value1Converted > value2Converted)
            {
                largerValue = "Value 1 is larger";
            }
            else if (value2Converted > value1Converted)
            {
                largerValue = "Value 2 is larger";
            }
            else
            {
                largerValue = "Values are equal";
            }

            return (percentDifference, largerValue);
        }
    }
}