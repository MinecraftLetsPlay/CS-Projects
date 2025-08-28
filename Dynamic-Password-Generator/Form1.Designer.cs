namespace Dynamic_Password_Generator
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDownPWLegnth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBoxLettersCap = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CustomPool = new System.Windows.Forms.TextBox();
            this.btnUseCustomPool = new System.Windows.Forms.Button();
            this.CheckBoxCharacters = new System.Windows.Forms.CheckBox();
            this.CheckBoxNumbers = new System.Windows.Forms.CheckBox();
            this.CheckBoxLetters = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PasswordOut = new System.Windows.Forms.TextBox();
            this.btnGeneratePassword = new System.Windows.Forms.Button();
            this.labelNoteMultiline = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPWLegnth)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownPWLegnth
            // 
            this.numericUpDownPWLegnth.Font = new System.Drawing.Font("Trebuchet MS", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownPWLegnth.Location = new System.Drawing.Point(463, 12);
            this.numericUpDownPWLegnth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPWLegnth.Name = "numericUpDownPWLegnth";
            this.numericUpDownPWLegnth.Size = new System.Drawing.Size(120, 68);
            this.numericUpDownPWLegnth.TabIndex = 1;
            this.numericUpDownPWLegnth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownPWLegnth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(682, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(535, 70);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password Generator";
            // 
            // CheckBoxLettersCap
            // 
            this.CheckBoxLettersCap.AutoSize = true;
            this.CheckBoxLettersCap.Font = new System.Drawing.Font("Trebuchet MS", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxLettersCap.Location = new System.Drawing.Point(74, 116);
            this.CheckBoxLettersCap.Name = "CheckBoxLettersCap";
            this.CheckBoxLettersCap.Size = new System.Drawing.Size(457, 59);
            this.CheckBoxLettersCap.TabIndex = 3;
            this.CheckBoxLettersCap.Text = "Letters (Capitalized)";
            this.CheckBoxLettersCap.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CustomPool);
            this.panel1.Controls.Add(this.btnUseCustomPool);
            this.panel1.Controls.Add(this.CheckBoxCharacters);
            this.panel1.Controls.Add(this.CheckBoxNumbers);
            this.panel1.Controls.Add(this.CheckBoxLetters);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.CheckBoxLettersCap);
            this.panel1.Location = new System.Drawing.Point(223, 263);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 585);
            this.panel1.TabIndex = 4;
            // 
            // CustomPool
            // 
            this.CustomPool.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomPool.Location = new System.Drawing.Point(29, 475);
            this.CustomPool.Multiline = true;
            this.CustomPool.Name = "CustomPool";
            this.CustomPool.Size = new System.Drawing.Size(567, 83);
            this.CustomPool.TabIndex = 9;
            // 
            // btnUseCustomPool
            // 
            this.btnUseCustomPool.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUseCustomPool.Location = new System.Drawing.Point(74, 395);
            this.btnUseCustomPool.Name = "btnUseCustomPool";
            this.btnUseCustomPool.Size = new System.Drawing.Size(341, 63);
            this.btnUseCustomPool.TabIndex = 8;
            this.btnUseCustomPool.Text = "Use Custom Pool";
            this.btnUseCustomPool.UseVisualStyleBackColor = true;
            this.btnUseCustomPool.Click += new System.EventHandler(this.btnUseCustomPool_Click);
            // 
            // CheckBoxCharacters
            // 
            this.CheckBoxCharacters.AutoSize = true;
            this.CheckBoxCharacters.Font = new System.Drawing.Font("Trebuchet MS", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxCharacters.Location = new System.Drawing.Point(74, 311);
            this.CheckBoxCharacters.Name = "CheckBoxCharacters";
            this.CheckBoxCharacters.Size = new System.Drawing.Size(261, 59);
            this.CheckBoxCharacters.TabIndex = 7;
            this.CheckBoxCharacters.Text = "Characters";
            this.CheckBoxCharacters.UseVisualStyleBackColor = true;
            // 
            // CheckBoxNumbers
            // 
            this.CheckBoxNumbers.AutoSize = true;
            this.CheckBoxNumbers.Font = new System.Drawing.Font("Trebuchet MS", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxNumbers.Location = new System.Drawing.Point(74, 246);
            this.CheckBoxNumbers.Name = "CheckBoxNumbers";
            this.CheckBoxNumbers.Size = new System.Drawing.Size(223, 59);
            this.CheckBoxNumbers.TabIndex = 6;
            this.CheckBoxNumbers.Text = "Numbers";
            this.CheckBoxNumbers.UseVisualStyleBackColor = true;
            // 
            // CheckBoxLetters
            // 
            this.CheckBoxLetters.AutoSize = true;
            this.CheckBoxLetters.Font = new System.Drawing.Font("Trebuchet MS", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxLetters.Location = new System.Drawing.Point(74, 181);
            this.CheckBoxLetters.Name = "CheckBoxLetters";
            this.CheckBoxLetters.Size = new System.Drawing.Size(189, 59);
            this.CheckBoxLetters.TabIndex = 5;
            this.CheckBoxLetters.Text = "Letters";
            this.CheckBoxLetters.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(578, 66);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password Building pools";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(402, 66);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password length";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.PasswordOut);
            this.panel2.Controls.Add(this.btnGeneratePassword);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.numericUpDownPWLegnth);
            this.panel2.Location = new System.Drawing.Point(1023, 263);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(649, 499);
            this.panel2.TabIndex = 6;
            // 
            // PasswordOut
            // 
            this.PasswordOut.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordOut.Location = new System.Drawing.Point(3, 304);
            this.PasswordOut.Multiline = true;
            this.PasswordOut.Name = "PasswordOut";
            this.PasswordOut.Size = new System.Drawing.Size(643, 190);
            this.PasswordOut.TabIndex = 7;
            // 
            // btnGeneratePassword
            // 
            this.btnGeneratePassword.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratePassword.Location = new System.Drawing.Point(66, 116);
            this.btnGeneratePassword.Name = "btnGeneratePassword";
            this.btnGeneratePassword.Size = new System.Drawing.Size(510, 60);
            this.btnGeneratePassword.TabIndex = 6;
            this.btnGeneratePassword.Text = "Generate Password";
            this.btnGeneratePassword.UseVisualStyleBackColor = true;
            this.btnGeneratePassword.Click += new System.EventHandler(this.btnGeneratePassword_Click);
            // 
            // labelNoteMultiline
            // 
            this.labelNoteMultiline.AutoSize = true;
            this.labelNoteMultiline.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoteMultiline.Location = new System.Drawing.Point(50, 934);
            this.labelNoteMultiline.Name = "labelNoteMultiline";
            this.labelNoteMultiline.Size = new System.Drawing.Size(1799, 40);
            this.labelNoteMultiline.TabIndex = 7;
            this.labelNoteMultiline.Text = "Note: When using Characters, there can be unecpexted line breaks because of the w" +
    "ay textboxes handle certain characters.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1898, 1024);
            this.Controls.Add(this.labelNoteMultiline);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Dynamic Password Genetator";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPWLegnth)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numericUpDownPWLegnth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CheckBoxLettersCap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox CheckBoxCharacters;
        private System.Windows.Forms.CheckBox CheckBoxNumbers;
        private System.Windows.Forms.CheckBox CheckBoxLetters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUseCustomPool;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox CustomPool;
        private System.Windows.Forms.Button btnGeneratePassword;
        private System.Windows.Forms.TextBox PasswordOut;
        private System.Windows.Forms.Label labelNoteMultiline;
    }
}

