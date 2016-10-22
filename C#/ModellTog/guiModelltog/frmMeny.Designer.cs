namespace guiModellTog {
    partial class frmMeny {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.cmdSokModell = new System.Windows.Forms.Button();
            this.cmdAvslutt = new System.Windows.Forms.Button();
            this.cmbRegAlternativ = new System.Windows.Forms.ComboBox();
            this.cmdRegModell = new System.Windows.Forms.Button();
            this.cmdVisValgte = new System.Windows.Forms.Button();
            this.cmbVisAlle = new System.Windows.Forms.ComboBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDetaljer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLeggTilLand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLeggTilProdusent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLeggTilSalgssted = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSkrivTilFil = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAvslutt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRegistrering = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRegLokomotiv = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRegVogn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLeggTilVogn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRegTilbehor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRegOnske = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisning = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisAlleLokomotiv = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisAlleVogner = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisAltTilbehor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisAlleOnsker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSokMeny = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSoking = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHjelpMeny = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHjelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTittel = new System.Windows.Forms.Label();
            this.tipMeny = new System.Windows.Forms.ToolTip(this.components);
            this.hpHjelp = new System.Windows.Forms.HelpProvider();
            this.tsmRedigerSlett = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSokModell
            // 
            this.cmdSokModell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSokModell.Location = new System.Drawing.Point(44, 256);
            this.cmdSokModell.Name = "cmdSokModell";
            this.cmdSokModell.Size = new System.Drawing.Size(346, 32);
            this.cmdSokModell.TabIndex = 5;
            this.cmdSokModell.Text = "Søk etter modell";
            this.tipMeny.SetToolTip(this.cmdSokModell, "Søk etter en modell basert på \r\nmodellnr, navn eller type");
            this.cmdSokModell.UseVisualStyleBackColor = true;
            this.cmdSokModell.Click += new System.EventHandler(this.cmdSokModell_Click);
            // 
            // cmdAvslutt
            // 
            this.cmdAvslutt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAvslutt.Location = new System.Drawing.Point(44, 321);
            this.cmdAvslutt.Name = "cmdAvslutt";
            this.cmdAvslutt.Size = new System.Drawing.Size(346, 32);
            this.cmdAvslutt.TabIndex = 6;
            this.cmdAvslutt.Text = "Avslutt programmet";
            this.tipMeny.SetToolTip(this.cmdAvslutt, "Avslutter programmet");
            this.cmdAvslutt.UseVisualStyleBackColor = true;
            this.cmdAvslutt.Click += new System.EventHandler(this.cmdAvslutt_Click);
            // 
            // cmbRegAlternativ
            // 
            this.cmbRegAlternativ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegAlternativ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegAlternativ.FormattingEnabled = true;
            this.cmbRegAlternativ.Location = new System.Drawing.Point(44, 134);
            this.cmbRegAlternativ.Name = "cmbRegAlternativ";
            this.cmbRegAlternativ.Size = new System.Drawing.Size(231, 24);
            this.cmbRegAlternativ.TabIndex = 1;
            // 
            // cmdRegModell
            // 
            this.cmdRegModell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRegModell.Location = new System.Drawing.Point(281, 134);
            this.cmdRegModell.Name = "cmdRegModell";
            this.cmdRegModell.Size = new System.Drawing.Size(109, 24);
            this.cmdRegModell.TabIndex = 2;
            this.cmdRegModell.Text = "Registrer";
            this.tipMeny.SetToolTip(this.cmdRegModell, "Legg til en ny modell basert på valg fra comboboxen");
            this.cmdRegModell.UseVisualStyleBackColor = true;
            this.cmdRegModell.Click += new System.EventHandler(this.cmdRegModell_Click);
            // 
            // cmdVisValgte
            // 
            this.cmdVisValgte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdVisValgte.Location = new System.Drawing.Point(281, 193);
            this.cmdVisValgte.Name = "cmdVisValgte";
            this.cmdVisValgte.Size = new System.Drawing.Size(109, 24);
            this.cmdVisValgte.TabIndex = 4;
            this.cmdVisValgte.Text = "Vis resultater";
            this.tipMeny.SetToolTip(this.cmdVisValgte, "Vis registrerte modeller basert på valg fra comboboxen");
            this.cmdVisValgte.UseVisualStyleBackColor = true;
            this.cmdVisValgte.Click += new System.EventHandler(this.cmdVisValgte_Click);
            // 
            // cmbVisAlle
            // 
            this.cmbVisAlle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVisAlle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVisAlle.FormattingEnabled = true;
            this.cmbVisAlle.Location = new System.Drawing.Point(44, 193);
            this.cmbVisAlle.Name = "cmbVisAlle";
            this.cmbVisAlle.Size = new System.Drawing.Size(231, 24);
            this.cmbVisAlle.TabIndex = 3;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmRegistrering,
            this.tsmVisning,
            this.tsmSokMeny,
            this.tsmHjelpMeny});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(429, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDetaljer,
            this.tsmSkrivTilFil,
            this.tsmAvslutt});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "File";
            // 
            // tsmDetaljer
            // 
            this.tsmDetaljer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLeggTilLand,
            this.tsmLeggTilProdusent,
            this.tsmLeggTilSalgssted,
            this.tsmRedigerSlett});
            this.tsmDetaljer.Name = "tsmDetaljer";
            this.tsmDetaljer.Size = new System.Drawing.Size(180, 22);
            this.tsmDetaljer.Text = "Detaljer";
            // 
            // tsmLeggTilLand
            // 
            this.tsmLeggTilLand.Name = "tsmLeggTilLand";
            this.tsmLeggTilLand.Size = new System.Drawing.Size(184, 22);
            this.tsmLeggTilLand.Text = "Legg til land";
            this.tsmLeggTilLand.Click += new System.EventHandler(this.tsmLeggTilLand_Click);
            // 
            // tsmLeggTilProdusent
            // 
            this.tsmLeggTilProdusent.Name = "tsmLeggTilProdusent";
            this.tsmLeggTilProdusent.Size = new System.Drawing.Size(184, 22);
            this.tsmLeggTilProdusent.Text = "Legg til produsent";
            this.tsmLeggTilProdusent.Click += new System.EventHandler(this.tsmLeggTilProdusent_Click);
            // 
            // tsmLeggTilSalgssted
            // 
            this.tsmLeggTilSalgssted.Name = "tsmLeggTilSalgssted";
            this.tsmLeggTilSalgssted.Size = new System.Drawing.Size(184, 22);
            this.tsmLeggTilSalgssted.Text = "Legg til salgssted";
            this.tsmLeggTilSalgssted.Click += new System.EventHandler(this.tsmLeggTilSalgssted_Click);
            // 
            // tsmSkrivTilFil
            // 
            this.tsmSkrivTilFil.Name = "tsmSkrivTilFil";
            this.tsmSkrivTilFil.Size = new System.Drawing.Size(180, 22);
            this.tsmSkrivTilFil.Text = "Skriv ønskeliste til fil";
            this.tsmSkrivTilFil.Click += new System.EventHandler(this.tsmSkrivTilFil_Click);
            // 
            // tsmAvslutt
            // 
            this.tsmAvslutt.Name = "tsmAvslutt";
            this.tsmAvslutt.Size = new System.Drawing.Size(180, 22);
            this.tsmAvslutt.Text = "Avslutt";
            this.tsmAvslutt.Click += new System.EventHandler(this.tsmAvslutt_Click);
            // 
            // tsmRegistrering
            // 
            this.tsmRegistrering.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRegLokomotiv,
            this.tsmRegVogn,
            this.tsmLeggTilVogn,
            this.tsmRegTilbehor,
            this.tsmRegOnske});
            this.tsmRegistrering.Name = "tsmRegistrering";
            this.tsmRegistrering.Size = new System.Drawing.Size(82, 20);
            this.tsmRegistrering.Text = "Registrering";
            // 
            // tsmRegLokomotiv
            // 
            this.tsmRegLokomotiv.Name = "tsmRegLokomotiv";
            this.tsmRegLokomotiv.Size = new System.Drawing.Size(211, 22);
            this.tsmRegLokomotiv.Text = "Registrer nytt lokomotiv";
            this.tsmRegLokomotiv.Click += new System.EventHandler(this.tsmRegLokomotiv_Click);
            // 
            // tsmRegVogn
            // 
            this.tsmRegVogn.Name = "tsmRegVogn";
            this.tsmRegVogn.Size = new System.Drawing.Size(211, 22);
            this.tsmRegVogn.Text = "Registrer ny vogn";
            this.tsmRegVogn.Click += new System.EventHandler(this.tsmRegVogn_Click);
            // 
            // tsmLeggTilVogn
            // 
            this.tsmLeggTilVogn.Name = "tsmLeggTilVogn";
            this.tsmLeggTilVogn.Size = new System.Drawing.Size(211, 22);
            this.tsmLeggTilVogn.Text = "Legg til eksisterende vogn";
            this.tsmLeggTilVogn.Click += new System.EventHandler(this.tsmLeggTilVogn_Click);
            // 
            // tsmRegTilbehor
            // 
            this.tsmRegTilbehor.Name = "tsmRegTilbehor";
            this.tsmRegTilbehor.Size = new System.Drawing.Size(211, 22);
            this.tsmRegTilbehor.Text = "Registrer nytt tilbehør";
            this.tsmRegTilbehor.Click += new System.EventHandler(this.tsmRegTilbehor_Click);
            // 
            // tsmRegOnske
            // 
            this.tsmRegOnske.Name = "tsmRegOnske";
            this.tsmRegOnske.Size = new System.Drawing.Size(211, 22);
            this.tsmRegOnske.Text = "Registrer nytt ønske";
            this.tsmRegOnske.Click += new System.EventHandler(this.tsmRegOnske_Click);
            // 
            // tsmVisning
            // 
            this.tsmVisning.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmVisAlleLokomotiv,
            this.tsmVisAlleVogner,
            this.tsmVisAltTilbehor,
            this.tsmVisAlleOnsker});
            this.tsmVisning.Name = "tsmVisning";
            this.tsmVisning.Size = new System.Drawing.Size(58, 20);
            this.tsmVisning.Text = "Visning";
            // 
            // tsmVisAlleLokomotiv
            // 
            this.tsmVisAlleLokomotiv.Name = "tsmVisAlleLokomotiv";
            this.tsmVisAlleLokomotiv.Size = new System.Drawing.Size(167, 22);
            this.tsmVisAlleLokomotiv.Text = "Vis alle lokomotiv";
            this.tsmVisAlleLokomotiv.Click += new System.EventHandler(this.tsmVisAlleLokomotiv_Click);
            // 
            // tsmVisAlleVogner
            // 
            this.tsmVisAlleVogner.Name = "tsmVisAlleVogner";
            this.tsmVisAlleVogner.Size = new System.Drawing.Size(167, 22);
            this.tsmVisAlleVogner.Text = "Vis alle vogner";
            this.tsmVisAlleVogner.Click += new System.EventHandler(this.tsmVisAlleVogner_Click);
            // 
            // tsmVisAltTilbehor
            // 
            this.tsmVisAltTilbehor.Name = "tsmVisAltTilbehor";
            this.tsmVisAltTilbehor.Size = new System.Drawing.Size(167, 22);
            this.tsmVisAltTilbehor.Text = "Vis alt tilbehør";
            this.tsmVisAltTilbehor.Click += new System.EventHandler(this.tsmVisAltTilbehor_Click);
            // 
            // tsmVisAlleOnsker
            // 
            this.tsmVisAlleOnsker.Name = "tsmVisAlleOnsker";
            this.tsmVisAlleOnsker.Size = new System.Drawing.Size(167, 22);
            this.tsmVisAlleOnsker.Text = "Vis alle ønsker";
            this.tsmVisAlleOnsker.Click += new System.EventHandler(this.tsmVisAlleOnsker_Click);
            // 
            // tsmSokMeny
            // 
            this.tsmSokMeny.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSoking});
            this.tsmSokMeny.Name = "tsmSokMeny";
            this.tsmSokMeny.Size = new System.Drawing.Size(55, 20);
            this.tsmSokMeny.Text = "Søking";
            // 
            // tsmSoking
            // 
            this.tsmSoking.Name = "tsmSoking";
            this.tsmSoking.Size = new System.Drawing.Size(160, 22);
            this.tsmSoking.Text = "Søk etter modell";
            this.tsmSoking.Click += new System.EventHandler(this.tsmSoking_Click);
            // 
            // tsmHjelpMeny
            // 
            this.tsmHjelpMeny.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHjelp,
            this.tsmAbout});
            this.tsmHjelpMeny.Name = "tsmHjelpMeny";
            this.tsmHjelpMeny.Size = new System.Drawing.Size(47, 20);
            this.tsmHjelpMeny.Text = "Hjelp";
            // 
            // tsmHjelp
            // 
            this.tsmHjelp.Name = "tsmHjelp";
            this.tsmHjelp.Size = new System.Drawing.Size(107, 22);
            this.tsmHjelp.Text = "Hjelp";
            this.tsmHjelp.Click += new System.EventHandler(this.tsmHjelp_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(107, 22);
            this.tsmAbout.Text = "About";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAbout_Click);
            // 
            // lblTittel
            // 
            this.lblTittel.AutoSize = true;
            this.lblTittel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTittel.Location = new System.Drawing.Point(86, 56);
            this.lblTittel.Name = "lblTittel";
            this.lblTittel.Size = new System.Drawing.Size(252, 20);
            this.lblTittel.TabIndex = 7;
            this.lblTittel.Text = "Hovedmeny for modelltog program";
            // 
            // tsmRedigerSlett
            // 
            this.tsmRedigerSlett.Name = "tsmRedigerSlett";
            this.tsmRedigerSlett.Size = new System.Drawing.Size(184, 22);
            this.tsmRedigerSlett.Text = "Rediger/Slett detaljer";
            this.tsmRedigerSlett.Click += new System.EventHandler(this.tsmRedigerSlett_Click);
            // 
            // frmMeny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(429, 381);
            this.Controls.Add(this.lblTittel);
            this.Controls.Add(this.cmdVisValgte);
            this.Controls.Add(this.cmbVisAlle);
            this.Controls.Add(this.cmdRegModell);
            this.Controls.Add(this.cmbRegAlternativ);
            this.Controls.Add(this.cmdAvslutt);
            this.Controls.Add(this.cmdSokModell);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.hpHjelp.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TableOfContents);
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "frmMeny";
            this.hpHjelp.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hovedmeny";
            this.Load += new System.EventHandler(this.frmMeny_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSokModell;
        private System.Windows.Forms.Button cmdAvslutt;
        private System.Windows.Forms.ComboBox cmbRegAlternativ;
        private System.Windows.Forms.Button cmdRegModell;
        private System.Windows.Forms.Button cmdVisValgte;
        private System.Windows.Forms.ComboBox cmbVisAlle;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmAvslutt;
        private System.Windows.Forms.ToolStripMenuItem tsmHjelpMeny;
        private System.Windows.Forms.ToolStripMenuItem tsmHjelp;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmDetaljer;
        private System.Windows.Forms.ToolStripMenuItem tsmLeggTilLand;
        private System.Windows.Forms.ToolStripMenuItem tsmLeggTilProdusent;
        private System.Windows.Forms.ToolStripMenuItem tsmLeggTilSalgssted;
        private System.Windows.Forms.ToolStripMenuItem tsmRegistrering;
        private System.Windows.Forms.ToolStripMenuItem tsmRegLokomotiv;
        private System.Windows.Forms.ToolStripMenuItem tsmRegVogn;
        private System.Windows.Forms.ToolStripMenuItem tsmLeggTilVogn;
        private System.Windows.Forms.ToolStripMenuItem tsmRegTilbehor;
        private System.Windows.Forms.ToolStripMenuItem tsmRegOnske;
        private System.Windows.Forms.ToolStripMenuItem tsmVisning;
        private System.Windows.Forms.ToolStripMenuItem tsmVisAlleLokomotiv;
        private System.Windows.Forms.ToolStripMenuItem tsmVisAlleVogner;
        private System.Windows.Forms.ToolStripMenuItem tsmVisAltTilbehor;
        private System.Windows.Forms.ToolStripMenuItem tsmVisAlleOnsker;
        private System.Windows.Forms.ToolStripMenuItem tsmSokMeny;
        private System.Windows.Forms.Label lblTittel;
        private System.Windows.Forms.ToolStripMenuItem tsmSoking;
        private System.Windows.Forms.ToolStripMenuItem tsmSkrivTilFil;
        private System.Windows.Forms.ToolTip tipMeny;
        private System.Windows.Forms.HelpProvider hpHjelp;
        private System.Windows.Forms.ToolStripMenuItem tsmRedigerSlett;
    }
}

