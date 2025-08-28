using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dynamic_Password_Generator
{
    public partial class Form1 : Form
    {

        // Variables

        private string customPWPool = String.Empty;

        private bool useLettersUppercase = false;
        private bool useLettersLowercase = false;
        private bool useNumbers = false;
        private bool useCharacters = false;

        private Random random = new Random();

        // Main initialization
        public Form1()
        {
            InitializeComponent();
            InitializeEventHandlers();
            CustomPool.Visible = false;
            labelNoteMultiline.Visible = false;
            btnUseCustomPool.Enabled = false;
        }

        private void InitializeEventHandlers() // Set up event handlers for checkboxes and buttons
        {
            CheckBoxLettersCap.CheckedChanged += (s, e) => CheckBoxStateChanged();
            CheckBoxLetters.CheckedChanged += (s, e) => CheckBoxStateChanged();
            CheckBoxNumbers.CheckedChanged += (s, e) => CheckBoxStateChanged();
            CheckBoxCharacters.CheckedChanged += (s, e) => CheckBoxStateChanged();

            btnGeneratePassword.Click += btnGeneratePassword_Click;
            btnUseCustomPool.Click += btnUseCustomPool_Click;
        }

        private void CheckBoxStateChanged() // Update state based on checkbox changes
        {
            useLettersUppercase = CheckBoxLettersCap.Checked;
            useLettersLowercase = CheckBoxLetters.Checked;
            useNumbers = CheckBoxNumbers.Checked;
            useCharacters = CheckBoxCharacters.Checked;

            bool anyChecked = useLettersUppercase || useLettersLowercase || useNumbers || useCharacters;

            btnUseCustomPool.Enabled = !anyChecked;
            CustomPool.Enabled = !anyChecked;
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e) // Generate password based on selected criteria
        {
            int passwordLength = (int)numericUpDownPWLegnth.Value;


            // Define character pools

            const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?";

            // Build the character pool based on user selections

            StringBuilder charPool = new StringBuilder();

            if (useLettersUppercase) charPool.Append(uppercaseLetters);
            if (useLettersLowercase) charPool.Append(lowercaseLetters);
            if (useNumbers) charPool.Append(numbers);
            if (useCharacters)
            {
                charPool.Append(specialChars);
                labelNoteMultiline.Visible = true;
            }
            else
            {
                labelNoteMultiline.Visible = false;
            }

            if (charPool.Length == 0 && !string.IsNullOrEmpty(customPWPool))
            {
                charPool.Append(customPWPool);
            }

            if (charPool.Length == 0)
            {
                MessageBox.Show("Please select at least one character pool.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = GeneratePassword(passwordLength, charPool.ToString());

            PasswordOut.Text = password.Replace(Environment.NewLine, string.Empty);
        }

        // Event handler for using custom pool
        private void btnUseCustomPool_Click(object sender, EventArgs e)
        {
            CustomPool.Visible = true;
            customPWPool = CustomPool.Text;
        }

        // Password generation logic
        private string GeneratePassword(int passwordLength, string charPool)
        {
            char[] password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = charPool[random.Next(charPool.Length)];
            }

            return new string(password);
        }
    }
}
