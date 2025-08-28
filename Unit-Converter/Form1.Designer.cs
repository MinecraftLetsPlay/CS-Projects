using System.Drawing;
using System.Windows.Forms;

namespace Unit_Converter
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
        /// 

        private TextBox InputTextBox2;

        private Label Info_Input2;

        private void InitializeComponent()
        {
            this.LargerValueTextBox = new System.Windows.Forms.TextBox();
            this.InputTextBox2 = new System.Windows.Forms.TextBox();
            this.Info_Input2 = new System.Windows.Forms.Label();
            this.InputUnitComboBox = new System.Windows.Forms.ComboBox();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.Label_InputValue = new System.Windows.Forms.Label();
            this.Label_InputUnit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MethodComboBox = new System.Windows.Forms.ComboBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.Label_Mode = new System.Windows.Forms.Label();
            this.Label_OutputValue = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Button_Run = new System.Windows.Forms.Button();
            this.OutputUnitComboBox = new System.Windows.Forms.ComboBox();
            this.Label_OutputUnit = new System.Windows.Forms.Label();
            this.InputUnitComboBox2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // LargerValueTextBox
            // 
            this.LargerValueTextBox.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LargerValueTextBox.Location = new System.Drawing.Point(892, 452);
            this.LargerValueTextBox.Name = "LargerValueTextBox";
            this.LargerValueTextBox.Size = new System.Drawing.Size(260, 40);
            this.LargerValueTextBox.TabIndex = 17;
            // 
            // InputTextBox2
            // 
            this.InputTextBox2.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTextBox2.Location = new System.Drawing.Point(44, 505);
            this.InputTextBox2.Name = "InputTextBox2";
            this.InputTextBox2.Size = new System.Drawing.Size(260, 40);
            this.InputTextBox2.TabIndex = 14;
            // 
            // Info_Input2
            // 
            this.Info_Input2.AutoSize = true;
            this.Info_Input2.Font = new System.Drawing.Font("Trebuchet MS", 16F);
            this.Info_Input2.Location = new System.Drawing.Point(37, 455);
            this.Info_Input2.Name = "Info_Input2";
            this.Info_Input2.Size = new System.Drawing.Size(204, 40);
            this.Info_Input2.TabIndex = 15;
            this.Info_Input2.Text = "Input value 2";
            // 
            // InputUnitComboBox
            // 
            this.InputUnitComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.InputUnitComboBox.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputUnitComboBox.FormattingEnabled = true;
            this.InputUnitComboBox.Location = new System.Drawing.Point(310, 400);
            this.InputUnitComboBox.Name = "InputUnitComboBox";
            this.InputUnitComboBox.Size = new System.Drawing.Size(210, 44);
            this.InputUnitComboBox.TabIndex = 0;
            this.InputUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.InputUnitComboBox_SelectedIndexChanged);
            // 
            // InputTextBox
            // 
            this.InputTextBox.Font = new System.Drawing.Font("Trebuchet MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTextBox.Location = new System.Drawing.Point(44, 400);
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(260, 42);
            this.InputTextBox.TabIndex = 1;
            // 
            // Label_InputValue
            // 
            this.Label_InputValue.AutoSize = true;
            this.Label_InputValue.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_InputValue.Location = new System.Drawing.Point(37, 350);
            this.Label_InputValue.Name = "Label_InputValue";
            this.Label_InputValue.Size = new System.Drawing.Size(177, 40);
            this.Label_InputValue.TabIndex = 2;
            this.Label_InputValue.Text = "Input value";
            // 
            // Label_InputUnit
            // 
            this.Label_InputUnit.AutoSize = true;
            this.Label_InputUnit.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_InputUnit.Location = new System.Drawing.Point(303, 350);
            this.Label_InputUnit.Name = "Label_InputUnit";
            this.Label_InputUnit.Size = new System.Drawing.Size(157, 40);
            this.Label_InputUnit.TabIndex = 3;
            this.Label_InputUnit.Text = "Input unit";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(510, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(345, 61);
            this.label2.TabIndex = 4;
            this.label2.Text = "Unit Converter";
            // 
            // MethodComboBox
            // 
            this.MethodComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MethodComboBox.Font = new System.Drawing.Font("Trebuchet MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MethodComboBox.FormattingEnabled = true;
            this.MethodComboBox.Location = new System.Drawing.Point(618, 400);
            this.MethodComboBox.Name = "MethodComboBox";
            this.MethodComboBox.Size = new System.Drawing.Size(190, 46);
            this.MethodComboBox.TabIndex = 5;
            this.MethodComboBox.SelectedIndexChanged += new System.EventHandler(this.MethodComboBox_SelectedIndexChanged);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Font = new System.Drawing.Font("Trebuchet MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputTextBox.Location = new System.Drawing.Point(892, 400);
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(260, 42);
            this.OutputTextBox.TabIndex = 6;
            // 
            // Label_Mode
            // 
            this.Label_Mode.AutoSize = true;
            this.Label_Mode.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Mode.Location = new System.Drawing.Point(629, 350);
            this.Label_Mode.Name = "Label_Mode";
            this.Label_Mode.Size = new System.Drawing.Size(92, 40);
            this.Label_Mode.TabIndex = 8;
            this.Label_Mode.Text = "Mode";
            // 
            // Label_OutputValue
            // 
            this.Label_OutputValue.AutoSize = true;
            this.Label_OutputValue.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_OutputValue.Location = new System.Drawing.Point(892, 350);
            this.Label_OutputValue.Name = "Label_OutputValue";
            this.Label_OutputValue.Size = new System.Drawing.Size(203, 40);
            this.Label_OutputValue.TabIndex = 9;
            this.Label_OutputValue.Text = "Output value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1095, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 40);
            this.label5.TabIndex = 10;
            // 
            // Button_Run
            // 
            this.Button_Run.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Run.Location = new System.Drawing.Point(445, 578);
            this.Button_Run.Name = "Button_Run";
            this.Button_Run.Size = new System.Drawing.Size(496, 79);
            this.Button_Run.TabIndex = 11;
            this.Button_Run.UseVisualStyleBackColor = true;
            this.Button_Run.Click += new System.EventHandler(this.Button_Run_Click);
            // 
            // OutputUnitComboBox
            // 
            this.OutputUnitComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OutputUnitComboBox.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputUnitComboBox.FormattingEnabled = true;
            this.OutputUnitComboBox.Location = new System.Drawing.Point(1160, 400);
            this.OutputUnitComboBox.Name = "OutputUnitComboBox";
            this.OutputUnitComboBox.Size = new System.Drawing.Size(230, 44);
            this.OutputUnitComboBox.TabIndex = 12;
            this.OutputUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.OutputUnitComboBox_SelectedIndexChanged);
            // 
            // Label_OutputUnit
            // 
            this.Label_OutputUnit.AutoSize = true;
            this.Label_OutputUnit.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_OutputUnit.Location = new System.Drawing.Point(1151, 350);
            this.Label_OutputUnit.Name = "Label_OutputUnit";
            this.Label_OutputUnit.Size = new System.Drawing.Size(183, 40);
            this.Label_OutputUnit.TabIndex = 13;
            this.Label_OutputUnit.Text = "Output unit";
            // 
            // InputUnitComboBox2
            // 
            this.InputUnitComboBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.InputUnitComboBox2.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputUnitComboBox2.FormattingEnabled = true;
            this.InputUnitComboBox2.Location = new System.Drawing.Point(310, 501);
            this.InputUnitComboBox2.Name = "InputUnitComboBox2";
            this.InputUnitComboBox2.Size = new System.Drawing.Size(210, 44);
            this.InputUnitComboBox2.TabIndex = 18;
            this.InputUnitComboBox2.SelectedIndexChanged += new System.EventHandler(this.InputUnitComboBox2_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1448, 794);
            this.Controls.Add(this.InputUnitComboBox2);
            this.Controls.Add(this.Label_OutputUnit);
            this.Controls.Add(this.OutputUnitComboBox);
            this.Controls.Add(this.Button_Run);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Label_OutputValue);
            this.Controls.Add(this.Label_Mode);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.MethodComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Label_InputUnit);
            this.Controls.Add(this.Label_InputValue);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.InputUnitComboBox);
            this.Controls.Add(this.InputTextBox2);
            this.Controls.Add(this.Info_Input2);
            this.Controls.Add(this.LargerValueTextBox);
            this.Name = "Form1";
            this.Text = "Unit Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox InputUnitComboBox;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.Label Label_InputValue;
        private System.Windows.Forms.Label Label_InputUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox MethodComboBox;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Label Label_Mode;
        private System.Windows.Forms.Label Label_OutputValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Button_Run;
        private System.Windows.Forms.ComboBox OutputUnitComboBox;
        private System.Windows.Forms.Label Label_OutputUnit;
        private System.Windows.Forms.TextBox LargerValueTextBox;
        private ComboBox InputUnitComboBox2;
    }
}

