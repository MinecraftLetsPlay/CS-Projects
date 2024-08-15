namespace Tic_Tac_Toe
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
            this.MitteMitte = new System.Windows.Forms.Button();
            this.MitteLinks = new System.Windows.Forms.Button();
            this.ObenRechts = new System.Windows.Forms.Button();
            this.ObenMitte = new System.Windows.Forms.Button();
            this.MitteRechts = new System.Windows.Forms.Button();
            this.UntenLinks = new System.Windows.Forms.Button();
            this.UntenMitte = new System.Windows.Forms.Button();
            this.UntenRechts = new System.Windows.Forms.Button();
            this.lbl_scoreX = new System.Windows.Forms.Label();
            this.lbl_scoreY = new System.Windows.Forms.Label();
            this.lbl_scoreDraw = new System.Windows.Forms.Label();
            this.bttn_close = new System.Windows.Forms.Button();
            this.bttn_reset = new System.Windows.Forms.Button();
            this.bttn_newgame = new System.Windows.Forms.Button();
            this.ObenLinks = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MitteMitte
            // 
            this.MitteMitte.Location = new System.Drawing.Point(299, 276);
            this.MitteMitte.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MitteMitte.Name = "MitteMitte";
            this.MitteMitte.Size = new System.Drawing.Size(257, 240);
            this.MitteMitte.TabIndex = 1;
            this.MitteMitte.UseVisualStyleBackColor = true;
            this.MitteMitte.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // MitteLinks
            // 
            this.MitteLinks.Location = new System.Drawing.Point(28, 276);
            this.MitteLinks.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MitteLinks.Name = "MitteLinks";
            this.MitteLinks.Size = new System.Drawing.Size(257, 240);
            this.MitteLinks.TabIndex = 2;
            this.MitteLinks.UseVisualStyleBackColor = true;
            this.MitteLinks.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // ObenRechts
            // 
            this.ObenRechts.Location = new System.Drawing.Point(570, 24);
            this.ObenRechts.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ObenRechts.Name = "ObenRechts";
            this.ObenRechts.Size = new System.Drawing.Size(257, 240);
            this.ObenRechts.TabIndex = 3;
            this.ObenRechts.UseVisualStyleBackColor = true;
            this.ObenRechts.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // ObenMitte
            // 
            this.ObenMitte.Location = new System.Drawing.Point(299, 24);
            this.ObenMitte.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ObenMitte.Name = "ObenMitte";
            this.ObenMitte.Size = new System.Drawing.Size(257, 240);
            this.ObenMitte.TabIndex = 4;
            this.ObenMitte.UseVisualStyleBackColor = true;
            this.ObenMitte.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // MitteRechts
            // 
            this.MitteRechts.Location = new System.Drawing.Point(570, 276);
            this.MitteRechts.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MitteRechts.Name = "MitteRechts";
            this.MitteRechts.Size = new System.Drawing.Size(257, 240);
            this.MitteRechts.TabIndex = 5;
            this.MitteRechts.UseVisualStyleBackColor = true;
            this.MitteRechts.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // UntenLinks
            // 
            this.UntenLinks.Location = new System.Drawing.Point(28, 528);
            this.UntenLinks.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.UntenLinks.Name = "UntenLinks";
            this.UntenLinks.Size = new System.Drawing.Size(257, 240);
            this.UntenLinks.TabIndex = 6;
            this.UntenLinks.UseVisualStyleBackColor = true;
            this.UntenLinks.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // UntenMitte
            // 
            this.UntenMitte.Location = new System.Drawing.Point(299, 528);
            this.UntenMitte.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.UntenMitte.Name = "UntenMitte";
            this.UntenMitte.Size = new System.Drawing.Size(257, 240);
            this.UntenMitte.TabIndex = 7;
            this.UntenMitte.UseVisualStyleBackColor = true;
            this.UntenMitte.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // UntenRechts
            // 
            this.UntenRechts.Location = new System.Drawing.Point(570, 528);
            this.UntenRechts.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.UntenRechts.Name = "UntenRechts";
            this.UntenRechts.Size = new System.Drawing.Size(257, 240);
            this.UntenRechts.TabIndex = 8;
            this.UntenRechts.UseVisualStyleBackColor = true;
            this.UntenRechts.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // lbl_scoreX
            // 
            this.lbl_scoreX.AutoSize = true;
            this.lbl_scoreX.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lbl_scoreX.Location = new System.Drawing.Point(968, 24);
            this.lbl_scoreX.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbl_scoreX.Name = "lbl_scoreX";
            this.lbl_scoreX.Size = new System.Drawing.Size(53, 40);
            this.lbl_scoreX.TabIndex = 9;
            this.lbl_scoreX.Text = "X:";
            // 
            // lbl_scoreY
            // 
            this.lbl_scoreY.AutoSize = true;
            this.lbl_scoreY.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lbl_scoreY.Location = new System.Drawing.Point(971, 104);
            this.lbl_scoreY.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbl_scoreY.Name = "lbl_scoreY";
            this.lbl_scoreY.Size = new System.Drawing.Size(52, 40);
            this.lbl_scoreY.TabIndex = 10;
            this.lbl_scoreY.Text = "Y:";
            // 
            // lbl_scoreDraw
            // 
            this.lbl_scoreDraw.AutoSize = true;
            this.lbl_scoreDraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lbl_scoreDraw.Location = new System.Drawing.Point(968, 180);
            this.lbl_scoreDraw.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbl_scoreDraw.Name = "lbl_scoreDraw";
            this.lbl_scoreDraw.Size = new System.Drawing.Size(116, 40);
            this.lbl_scoreDraw.TabIndex = 11;
            this.lbl_scoreDraw.Text = "Draw:";
            // 
            // bttn_close
            // 
            this.bttn_close.Location = new System.Drawing.Point(978, 696);
            this.bttn_close.Name = "bttn_close";
            this.bttn_close.Size = new System.Drawing.Size(222, 72);
            this.bttn_close.TabIndex = 12;
            this.bttn_close.Text = "Close";
            this.bttn_close.UseVisualStyleBackColor = true;
            this.bttn_close.Click += new System.EventHandler(this.bttn_close_Click);
            // 
            // bttn_reset
            // 
            this.bttn_reset.Location = new System.Drawing.Point(978, 612);
            this.bttn_reset.Name = "bttn_reset";
            this.bttn_reset.Size = new System.Drawing.Size(222, 72);
            this.bttn_reset.TabIndex = 13;
            this.bttn_reset.Text = "Reset";
            this.bttn_reset.UseVisualStyleBackColor = true;
            this.bttn_reset.Click += new System.EventHandler(this.bttn_reset_Click);
            // 
            // bttn_newgame
            // 
            this.bttn_newgame.Location = new System.Drawing.Point(978, 528);
            this.bttn_newgame.Name = "bttn_newgame";
            this.bttn_newgame.Size = new System.Drawing.Size(222, 72);
            this.bttn_newgame.TabIndex = 14;
            this.bttn_newgame.Text = "New Game";
            this.bttn_newgame.UseVisualStyleBackColor = true;
            this.bttn_newgame.Click += new System.EventHandler(this.bttn_newgame_Click);
            // 
            // ObenLinks
            // 
            this.ObenLinks.Location = new System.Drawing.Point(28, 24);
            this.ObenLinks.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ObenLinks.Name = "ObenLinks";
            this.ObenLinks.Size = new System.Drawing.Size(257, 240);
            this.ObenLinks.TabIndex = 15;
            this.ObenLinks.UseVisualStyleBackColor = true;
            this.ObenLinks.Click += new System.EventHandler(this.ButtonKlick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(21F, 40F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 900);
            this.Controls.Add(this.ObenLinks);
            this.Controls.Add(this.bttn_newgame);
            this.Controls.Add(this.bttn_reset);
            this.Controls.Add(this.bttn_close);
            this.Controls.Add(this.lbl_scoreDraw);
            this.Controls.Add(this.lbl_scoreY);
            this.Controls.Add(this.lbl_scoreX);
            this.Controls.Add(this.UntenRechts);
            this.Controls.Add(this.UntenMitte);
            this.Controls.Add(this.UntenLinks);
            this.Controls.Add(this.MitteRechts);
            this.Controls.Add(this.ObenMitte);
            this.Controls.Add(this.ObenRechts);
            this.Controls.Add(this.MitteLinks);
            this.Controls.Add(this.MitteMitte);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.ButtonKlick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button MitteMitte;
        private System.Windows.Forms.Button MitteLinks;
        private System.Windows.Forms.Button ObenRechts;
        private System.Windows.Forms.Button ObenMitte;
        private System.Windows.Forms.Button MitteRechts;
        private System.Windows.Forms.Button UntenLinks;
        private System.Windows.Forms.Button UntenMitte;
        private System.Windows.Forms.Button UntenRechts;
        private System.Windows.Forms.Label lbl_scoreX;
        private System.Windows.Forms.Label lbl_scoreY;
        private System.Windows.Forms.Label lbl_scoreDraw;
        private System.Windows.Forms.Button bttn_close;
        private System.Windows.Forms.Button bttn_reset;
        private System.Windows.Forms.Button bttn_newgame;
        private System.Windows.Forms.Button ObenLinks;
    }
}

