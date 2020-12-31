namespace TP3ProfGame
{
    partial class FormOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelColonnes = new System.Windows.Forms.Label();
            this.lblSon = new System.Windows.Forms.Label();
            this.lblRayon = new System.Windows.Forms.Label();
            this.labelLignes = new System.Windows.Forms.Label();
            this.numericUpDownLignes = new System.Windows.Forms.NumericUpDown();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.numericUpDownColonnes = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSon = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxRayon = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLignes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColonnes)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackgroundImage = global::TP3ProfGame.Properties.Resources.gangplankgalleon_by_zacmariozero_d8ylnxu;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelColonnes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSon, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblRayon, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelLignes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownLignes, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownColonnes, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxSon, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonOK, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxRayon, 2, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(393, 227);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Red;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(151, 184);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 39);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // labelColonnes
            // 
            this.labelColonnes.AutoSize = true;
            this.labelColonnes.Location = new System.Drawing.Point(3, 42);
            this.labelColonnes.Name = "labelColonnes";
            this.labelColonnes.Size = new System.Drawing.Size(142, 26);
            this.labelColonnes.TabIndex = 4;
            this.labelColonnes.Text = "Nombre de colonnes dans la carte :";
            // 
            // lblSon
            // 
            this.lblSon.AutoSize = true;
            this.lblSon.Location = new System.Drawing.Point(3, 84);
            this.lblSon.Name = "lblSon";
            this.lblSon.Size = new System.Drawing.Size(131, 13);
            this.lblSon.TabIndex = 6;
            this.lblSon.Text = "Activer les effets sonores :";
            // 
            // lblRayon
            // 
            this.lblRayon.AutoSize = true;
            this.lblRayon.Location = new System.Drawing.Point(3, 148);
            this.lblRayon.Name = "lblRayon";
            this.lblRayon.Size = new System.Drawing.Size(130, 13);
            this.lblRayon.TabIndex = 7;
            this.lblRayon.Text = "Modifier le rayon d\'action :";
            // 
            // labelLignes
            // 
            this.labelLignes.AutoSize = true;
            this.labelLignes.Location = new System.Drawing.Point(3, 0);
            this.labelLignes.Name = "labelLignes";
            this.labelLignes.Size = new System.Drawing.Size(129, 26);
            this.labelLignes.TabIndex = 3;
            this.labelLignes.Text = "Nombre de lignes dans la carte :";
            // 
            // numericUpDownLignes
            // 
            this.numericUpDownLignes.Location = new System.Drawing.Point(151, 3);
            this.numericUpDownLignes.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownLignes.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownLignes.Name = "numericUpDownLignes";
            this.numericUpDownLignes.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownLignes.TabIndex = 1;
            this.numericUpDownLignes.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(151, 151);
            this.trackBar1.Minimum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(102, 27);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 2;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // numericUpDownColonnes
            // 
            this.numericUpDownColonnes.Location = new System.Drawing.Point(151, 45);
            this.numericUpDownColonnes.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownColonnes.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownColonnes.Name = "numericUpDownColonnes";
            this.numericUpDownColonnes.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownColonnes.TabIndex = 2;
            this.numericUpDownColonnes.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // checkBoxSon
            // 
            this.checkBoxSon.AutoSize = true;
            this.checkBoxSon.Checked = true;
            this.checkBoxSon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSon.Location = new System.Drawing.Point(151, 87);
            this.checkBoxSon.Name = "checkBoxSon";
            this.checkBoxSon.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSon.TabIndex = 5;
            this.checkBoxSon.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.Green;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(3, 184);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(101, 39);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            // 
            // textBoxRayon
            // 
            this.textBoxRayon.Location = new System.Drawing.Point(261, 151);
            this.textBoxRayon.Name = "textBoxRayon";
            this.textBoxRayon.Size = new System.Drawing.Size(100, 20);
            this.textBoxRayon.TabIndex = 10;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 227);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormOptions";
            this.Text = "FormOptions";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLignes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColonnes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.NumericUpDown numericUpDownLignes;
        private System.Windows.Forms.NumericUpDown numericUpDownColonnes;
        private System.Windows.Forms.Label labelLignes;
        private System.Windows.Forms.Label labelColonnes;
        private System.Windows.Forms.CheckBox checkBoxSon;
        private System.Windows.Forms.Label lblSon;
        private System.Windows.Forms.Label lblRayon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxRayon;
    }
}