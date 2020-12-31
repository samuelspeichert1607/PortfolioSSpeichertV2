namespace TP3ProfGame
{
  partial class FormJeuPrincipal
  {
    /// <summary>
    /// Variable nécessaire au concepteur.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Nettoyage des ressources utilisées.
    /// </summary>
    /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose( );
      }
      base.Dispose( disposing );
    }

    #region Code généré par le Concepteur Windows Form

    /// <summary>
    /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
    /// le contenu de cette méthode avec l'éditeur de code.
    /// </summary>
    private void InitializeComponent( )
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJeuPrincipal));
            this.tableauJeuOrdi = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.partieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recommencerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changerLesOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButtonBoulet = new System.Windows.Forms.RadioButton();
            this.radioButtonGrenade = new System.Windows.Forms.RadioButton();
            this.radioButtonLaser = new System.Windows.Forms.RadioButton();
            this.radioButtonBombe = new System.Windows.Forms.RadioButton();
            this.lblQuantiteBoulet = new System.Windows.Forms.Label();
            this.lblQuantiteGrenades = new System.Windows.Forms.Label();
            this.lblQuantiteLasers = new System.Windows.Forms.Label();
            this.lblQuantiteBombes = new System.Windows.Forms.Label();
            this.progressBarPetit = new System.Windows.Forms.ProgressBar();
            this.progressBarMoyen = new System.Windows.Forms.ProgressBar();
            this.progressBarGrand = new System.Windows.Forms.ProgressBar();
            this.lblPetit = new System.Windows.Forms.Label();
            this.lblMoyen = new System.Windows.Forms.Label();
            this.lblGrand = new System.Windows.Forms.Label();
            this.radioButtonCocktail = new System.Windows.Forms.RadioButton();
            this.lblQuantiteCocktails = new System.Windows.Forms.Label();
            this.progressBarCumulatif = new System.Windows.Forms.ProgressBar();
            this.lblCumulatif = new System.Windows.Forms.Label();
            this.progressBarPrecision = new System.Windows.Forms.ProgressBar();
            this.lblPrecision = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableauJeuOrdi
            // 
            this.tableauJeuOrdi.BackColor = System.Drawing.Color.Transparent;
            this.tableauJeuOrdi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableauJeuOrdi.BackgroundImage")));
            this.tableauJeuOrdi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableauJeuOrdi.ColumnCount = 1;
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableauJeuOrdi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableauJeuOrdi.Location = new System.Drawing.Point(295, 70);
            this.tableauJeuOrdi.Name = "tableauJeuOrdi";
            this.tableauJeuOrdi.RowCount = 1;
            this.tableauJeuOrdi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableauJeuOrdi.Size = new System.Drawing.Size(500, 500);
            this.tableauJeuOrdi.TabIndex = 3;
            this.tableauJeuOrdi.MouseEnter += new System.EventHandler(this.tableauJeuOrdi_MouseEnter);
            this.tableauJeuOrdi.MouseLeave += new System.EventHandler(this.tableauJeuOrdi_MouseLeave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Sienna;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.partieToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1167, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // partieToolStripMenuItem
            // 
            this.partieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recommencerToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.partieToolStripMenuItem.ForeColor = System.Drawing.Color.Yellow;
            this.partieToolStripMenuItem.Name = "partieToolStripMenuItem";
            this.partieToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.partieToolStripMenuItem.Text = "Partie";
            // 
            // recommencerToolStripMenuItem
            // 
            this.recommencerToolStripMenuItem.Name = "recommencerToolStripMenuItem";
            this.recommencerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.recommencerToolStripMenuItem.Text = "Recommencer";
            this.recommencerToolStripMenuItem.Click += new System.EventHandler(this.recommencerToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changerLesOptionsToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.Yellow;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // changerLesOptionsToolStripMenuItem
            // 
            this.changerLesOptionsToolStripMenuItem.Name = "changerLesOptionsToolStripMenuItem";
            this.changerLesOptionsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.changerLesOptionsToolStripMenuItem.Text = "Changer les options";
            this.changerLesOptionsToolStripMenuItem.Click += new System.EventHandler(this.changerLesOptionsToolStripMenuItem_Click);
            // 
            // radioButtonBoulet
            // 
            this.radioButtonBoulet.AutoSize = true;
            this.radioButtonBoulet.Checked = true;
            this.radioButtonBoulet.Location = new System.Drawing.Point(43, 70);
            this.radioButtonBoulet.Name = "radioButtonBoulet";
            this.radioButtonBoulet.Size = new System.Drawing.Size(103, 17);
            this.radioButtonBoulet.TabIndex = 5;
            this.radioButtonBoulet.TabStop = true;
            this.radioButtonBoulet.Text = "Boulet de canon";
            this.radioButtonBoulet.UseVisualStyleBackColor = true;
            // 
            // radioButtonGrenade
            // 
            this.radioButtonGrenade.AutoSize = true;
            this.radioButtonGrenade.Location = new System.Drawing.Point(43, 115);
            this.radioButtonGrenade.Name = "radioButtonGrenade";
            this.radioButtonGrenade.Size = new System.Drawing.Size(66, 17);
            this.radioButtonGrenade.TabIndex = 6;
            this.radioButtonGrenade.Text = "Grenade";
            this.radioButtonGrenade.UseVisualStyleBackColor = true;
            // 
            // radioButtonLaser
            // 
            this.radioButtonLaser.AutoSize = true;
            this.radioButtonLaser.Location = new System.Drawing.Point(43, 159);
            this.radioButtonLaser.Name = "radioButtonLaser";
            this.radioButtonLaser.Size = new System.Drawing.Size(51, 17);
            this.radioButtonLaser.TabIndex = 7;
            this.radioButtonLaser.Text = "Laser";
            this.radioButtonLaser.UseVisualStyleBackColor = true;
            // 
            // radioButtonBombe
            // 
            this.radioButtonBombe.AutoSize = true;
            this.radioButtonBombe.Location = new System.Drawing.Point(43, 199);
            this.radioButtonBombe.Name = "radioButtonBombe";
            this.radioButtonBombe.Size = new System.Drawing.Size(105, 17);
            this.radioButtonBombe.TabIndex = 8;
            this.radioButtonBombe.Text = "Bombe Atomique";
            this.radioButtonBombe.UseVisualStyleBackColor = true;
            // 
            // lblQuantiteBoulet
            // 
            this.lblQuantiteBoulet.AutoSize = true;
            this.lblQuantiteBoulet.Location = new System.Drawing.Point(152, 72);
            this.lblQuantiteBoulet.Name = "lblQuantiteBoulet";
            this.lblQuantiteBoulet.Size = new System.Drawing.Size(91, 13);
            this.lblQuantiteBoulet.TabIndex = 9;
            this.lblQuantiteBoulet.Text = "Boulets lancés : 0";
            // 
            // lblQuantiteGrenades
            // 
            this.lblQuantiteGrenades.AutoSize = true;
            this.lblQuantiteGrenades.Location = new System.Drawing.Point(152, 117);
            this.lblQuantiteGrenades.Name = "lblQuantiteGrenades";
            this.lblQuantiteGrenades.Size = new System.Drawing.Size(108, 13);
            this.lblQuantiteGrenades.TabIndex = 10;
            this.lblQuantiteGrenades.Text = "Grenades lancées : 0";
            // 
            // lblQuantiteLasers
            // 
            this.lblQuantiteLasers.AutoSize = true;
            this.lblQuantiteLasers.Location = new System.Drawing.Point(152, 161);
            this.lblQuantiteLasers.Name = "lblQuantiteLasers";
            this.lblQuantiteLasers.Size = new System.Drawing.Size(87, 13);
            this.lblQuantiteLasers.TabIndex = 11;
            this.lblQuantiteLasers.Text = "Lasers lancés : 0";
            // 
            // lblQuantiteBombes
            // 
            this.lblQuantiteBombes.AutoSize = true;
            this.lblQuantiteBombes.Location = new System.Drawing.Point(152, 203);
            this.lblQuantiteBombes.Name = "lblQuantiteBombes";
            this.lblQuantiteBombes.Size = new System.Drawing.Size(100, 13);
            this.lblQuantiteBombes.TabIndex = 12;
            this.lblQuantiteBombes.Text = "Bombes lancées : 0";
            // 
            // progressBarPetit
            // 
            this.progressBarPetit.Location = new System.Drawing.Point(996, 70);
            this.progressBarPetit.Name = "progressBarPetit";
            this.progressBarPetit.Size = new System.Drawing.Size(100, 23);
            this.progressBarPetit.TabIndex = 13;
            this.progressBarPetit.Value = 100;
            // 
            // progressBarMoyen
            // 
            this.progressBarMoyen.Location = new System.Drawing.Point(996, 99);
            this.progressBarMoyen.Name = "progressBarMoyen";
            this.progressBarMoyen.Size = new System.Drawing.Size(100, 23);
            this.progressBarMoyen.TabIndex = 14;
            this.progressBarMoyen.Value = 100;
            // 
            // progressBarGrand
            // 
            this.progressBarGrand.Location = new System.Drawing.Point(996, 128);
            this.progressBarGrand.Name = "progressBarGrand";
            this.progressBarGrand.Size = new System.Drawing.Size(100, 23);
            this.progressBarGrand.TabIndex = 15;
            this.progressBarGrand.Value = 100;
            // 
            // lblPetit
            // 
            this.lblPetit.AutoSize = true;
            this.lblPetit.Location = new System.Drawing.Point(880, 70);
            this.lblPetit.Name = "lblPetit";
            this.lblPetit.Size = new System.Drawing.Size(99, 13);
            this.lblPetit.TabIndex = 16;
            this.lblPetit.Text = "Vie du petit bateau:";
            // 
            // lblMoyen
            // 
            this.lblMoyen.AutoSize = true;
            this.lblMoyen.Location = new System.Drawing.Point(880, 99);
            this.lblMoyen.Name = "lblMoyen";
            this.lblMoyen.Size = new System.Drawing.Size(110, 13);
            this.lblMoyen.TabIndex = 17;
            this.lblMoyen.Text = "Vie du moyen bateau:";
            // 
            // lblGrand
            // 
            this.lblGrand.AutoSize = true;
            this.lblGrand.Location = new System.Drawing.Point(880, 128);
            this.lblGrand.Name = "lblGrand";
            this.lblGrand.Size = new System.Drawing.Size(106, 13);
            this.lblGrand.TabIndex = 18;
            this.lblGrand.Text = "Vie du grand bateau:";
            // 
            // radioButtonCocktail
            // 
            this.radioButtonCocktail.AutoSize = true;
            this.radioButtonCocktail.Location = new System.Drawing.Point(43, 239);
            this.radioButtonCocktail.Name = "radioButtonCocktail";
            this.radioButtonCocktail.Size = new System.Drawing.Size(104, 17);
            this.radioButtonCocktail.TabIndex = 19;
            this.radioButtonCocktail.Text = "Cocktail Molotov";
            this.radioButtonCocktail.UseVisualStyleBackColor = true;
            // 
            // lblQuantiteCocktails
            // 
            this.lblQuantiteCocktails.AutoSize = true;
            this.lblQuantiteCocktails.Location = new System.Drawing.Point(152, 243);
            this.lblQuantiteCocktails.Name = "lblQuantiteCocktails";
            this.lblQuantiteCocktails.Size = new System.Drawing.Size(99, 13);
            this.lblQuantiteCocktails.TabIndex = 20;
            this.lblQuantiteCocktails.Text = "Cocktails lancés : 0";
            // 
            // progressBarCumulatif
            // 
            this.progressBarCumulatif.BackColor = System.Drawing.Color.Blue;
            this.progressBarCumulatif.ForeColor = System.Drawing.Color.Blue;
            this.progressBarCumulatif.Location = new System.Drawing.Point(996, 161);
            this.progressBarCumulatif.Name = "progressBarCumulatif";
            this.progressBarCumulatif.Size = new System.Drawing.Size(100, 23);
            this.progressBarCumulatif.TabIndex = 21;
            this.progressBarCumulatif.Value = 100;
            // 
            // lblCumulatif
            // 
            this.lblCumulatif.AutoSize = true;
            this.lblCumulatif.Location = new System.Drawing.Point(880, 163);
            this.lblCumulatif.Name = "lblCumulatif";
            this.lblCumulatif.Size = new System.Drawing.Size(56, 13);
            this.lblCumulatif.TabIndex = 22;
            this.lblCumulatif.Text = "Cumulatif :";
            // 
            // progressBarPrecision
            // 
            this.progressBarPrecision.Location = new System.Drawing.Point(996, 222);
            this.progressBarPrecision.Name = "progressBarPrecision";
            this.progressBarPrecision.Size = new System.Drawing.Size(100, 23);
            this.progressBarPrecision.TabIndex = 23;
            // 
            // lblPrecision
            // 
            this.lblPrecision.AutoSize = true;
            this.lblPrecision.Location = new System.Drawing.Point(880, 222);
            this.lblPrecision.Name = "lblPrecision";
            this.lblPrecision.Size = new System.Drawing.Size(56, 13);
            this.lblPrecision.TabIndex = 24;
            this.lblPrecision.Text = "Précision :";
            // 
            // FormJeuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.BackgroundImage = global::TP3ProfGame.Properties.Resources.sfu025_hardwood_flooring_laminate_floors_floor_ca_california_for_new_laminated_wooden_floor_1024x1024;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1167, 590);
            this.Controls.Add(this.lblPrecision);
            this.Controls.Add(this.progressBarPrecision);
            this.Controls.Add(this.lblCumulatif);
            this.Controls.Add(this.progressBarCumulatif);
            this.Controls.Add(this.lblQuantiteCocktails);
            this.Controls.Add(this.radioButtonCocktail);
            this.Controls.Add(this.lblGrand);
            this.Controls.Add(this.lblMoyen);
            this.Controls.Add(this.lblPetit);
            this.Controls.Add(this.progressBarGrand);
            this.Controls.Add(this.progressBarMoyen);
            this.Controls.Add(this.progressBarPetit);
            this.Controls.Add(this.lblQuantiteBombes);
            this.Controls.Add(this.lblQuantiteLasers);
            this.Controls.Add(this.lblQuantiteGrenades);
            this.Controls.Add(this.lblQuantiteBoulet);
            this.Controls.Add(this.radioButtonBombe);
            this.Controls.Add(this.radioButtonLaser);
            this.Controls.Add(this.radioButtonGrenade);
            this.Controls.Add(this.radioButtonBoulet);
            this.Controls.Add(this.tableauJeuOrdi);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormJeuPrincipal";
            this.Text = "Battleship";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableauJeuOrdi;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem partieToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem recommencerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem changerLesOptionsToolStripMenuItem;
    private System.Windows.Forms.RadioButton radioButtonBoulet;
    private System.Windows.Forms.RadioButton radioButtonGrenade;
    private System.Windows.Forms.RadioButton radioButtonLaser;
    private System.Windows.Forms.RadioButton radioButtonBombe;
    private System.Windows.Forms.Label lblQuantiteBoulet;
    private System.Windows.Forms.Label lblQuantiteGrenades;
    private System.Windows.Forms.Label lblQuantiteLasers;
    private System.Windows.Forms.Label lblQuantiteBombes;
    private System.Windows.Forms.ProgressBar progressBarPetit;
    private System.Windows.Forms.ProgressBar progressBarMoyen;
    private System.Windows.Forms.ProgressBar progressBarGrand;
    private System.Windows.Forms.Label lblPetit;
    private System.Windows.Forms.Label lblMoyen;
    private System.Windows.Forms.Label lblGrand;
    private System.Windows.Forms.RadioButton radioButtonCocktail;
    private System.Windows.Forms.Label lblQuantiteCocktails;
    private System.Windows.Forms.ProgressBar progressBarCumulatif;
    private System.Windows.Forms.Label lblCumulatif;
    private System.Windows.Forms.ProgressBar progressBarPrecision;
    private System.Windows.Forms.Label lblPrecision;

  }
}

