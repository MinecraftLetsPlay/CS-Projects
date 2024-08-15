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
        private string customPWPool = String.Empty;

        private bool useLettersUppercase = false;
        private bool useLettersLowercase = false;
        private bool useNumbers = false;
        private bool useCharacters = false;

        public Form1()
        {
            InitializeComponent();
            InitializeEventHandlers();
            CustomPool.Visible = false;
            labelNoteMultiline.Visible = false;
            btnUseCustomPool.Enabled = false;
        }

        private void InitializeEventHandlers()
        {
            CheckBoxLettersCap.CheckedChanged += (s, e) => CheckBoxStateChanged();
            CheckBoxLetters.CheckedChanged += (s, e) => CheckBoxStateChanged();
            CheckBoxNumbers.CheckedChanged += (s, e) => CheckBoxStateChanged();
            CheckBoxCharacters.CheckedChanged += (s, e) => CheckBoxStateChanged();

            btnGeneratePassword.Click += btnGeneratePassword_Click;
            btnUseCustomPool.Click += btnUseCustomPool_Click;
        }

        private void CheckBoxStateChanged()
        {
            useLettersUppercase = CheckBoxLettersCap.Checked;
            useLettersLowercase = CheckBoxLetters.Checked;
            useNumbers = CheckBoxNumbers.Checked;
            useCharacters = CheckBoxCharacters.Checked;

            bool anyChecked = useLettersUppercase || useLettersLowercase || useNumbers || useCharacters;

            btnUseCustomPool.Enabled = !anyChecked;
            CustomPool.Enabled = !anyChecked;
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            int passwordLength = (int)numericUpDownPWLegnth.Value;

            const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?";

            string charPool = "";

            if (useLettersUppercase) charPool += uppercaseLetters;
            if (useLettersLowercase) charPool += lowercaseLetters;
            if (useNumbers) charPool += numbers;
            if (useCharacters)
            {
                charPool += specialChars;
                labelNoteMultiline.Visible = true;
            }
            else
            {
                labelNoteMultiline.Visible = false;
            }
            if (!useLettersUppercase && !useLettersLowercase && !useNumbers && !useCharacters && !string.IsNullOrEmpty(customPWPool))
            {
                charPool += customPWPool;
            }

            if (string.IsNullOrEmpty(charPool))
            {
                MessageBox.Show("Please select at least one character pool.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = GeneratePassword(passwordLength, charPool);

            PasswordOut.Text = password.Replace(Environment.NewLine, string.Empty);
        }

        private void btnUseCustomPool_Click(object sender, EventArgs e)
        {
            CustomPool.Visible = true;
            customPWPool = CustomPool.Text;
        }

        private string GeneratePassword(int passwordLength, string charPool)
        {
            Random random = new Random();
            char[] password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = charPool[random.Next(charPool.Length)];
            }

            return new string(password);
        }
    }
}
