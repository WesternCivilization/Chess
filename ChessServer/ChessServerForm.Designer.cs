namespace Chess
{
    partial class ChessServerForm
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
            this.listBoxFreeClients = new System.Windows.Forms.ListBox();
            this.labelFreeClients = new System.Windows.Forms.Label();
            this.listBoxInTheGame = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxIpBind = new System.Windows.Forms.ComboBox();
            this.buttonBind = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonViewDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxFreeClients
            // 
            this.listBoxFreeClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFreeClients.FormattingEnabled = true;
            this.listBoxFreeClients.Location = new System.Drawing.Point(12, 62);
            this.listBoxFreeClients.Name = "listBoxFreeClients";
            this.listBoxFreeClients.Size = new System.Drawing.Size(207, 238);
            this.listBoxFreeClients.TabIndex = 0;
            // 
            // labelFreeClients
            // 
            this.labelFreeClients.AutoSize = true;
            this.labelFreeClients.Location = new System.Drawing.Point(84, 46);
            this.labelFreeClients.Name = "labelFreeClients";
            this.labelFreeClients.Size = new System.Drawing.Size(61, 13);
            this.labelFreeClients.TabIndex = 1;
            this.labelFreeClients.Text = "Free clients";
            // 
            // listBoxInTheGame
            // 
            this.listBoxInTheGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxInTheGame.FormattingEnabled = true;
            this.listBoxInTheGame.Location = new System.Drawing.Point(225, 62);
            this.listBoxInTheGame.Name = "listBoxInTheGame";
            this.listBoxInTheGame.Size = new System.Drawing.Size(200, 238);
            this.listBoxInTheGame.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "In the game";
            // 
            // comboBoxIpBind
            // 
            this.comboBoxIpBind.FormattingEnabled = true;
            this.comboBoxIpBind.Location = new System.Drawing.Point(12, 14);
            this.comboBoxIpBind.Name = "comboBoxIpBind";
            this.comboBoxIpBind.Size = new System.Drawing.Size(159, 21);
            this.comboBoxIpBind.TabIndex = 4;
            // 
            // buttonBind
            // 
            this.buttonBind.Location = new System.Drawing.Point(177, 12);
            this.buttonBind.Name = "buttonBind";
            this.buttonBind.Size = new System.Drawing.Size(75, 23);
            this.buttonBind.TabIndex = 5;
            this.buttonBind.Text = "Bind";
            this.buttonBind.UseVisualStyleBackColor = true;
            this.buttonBind.Click += new System.EventHandler(this.buttonBind_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(258, 12);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(75, 23);
            this.buttonCopy.TabIndex = 6;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonViewDatabase
            // 
            this.buttonViewDatabase.Location = new System.Drawing.Point(339, 12);
            this.buttonViewDatabase.Name = "buttonViewDatabase";
            this.buttonViewDatabase.Size = new System.Drawing.Size(85, 23);
            this.buttonViewDatabase.TabIndex = 7;
            this.buttonViewDatabase.Text = "View database";
            this.buttonViewDatabase.UseVisualStyleBackColor = true;
            this.buttonViewDatabase.Click += new System.EventHandler(this.buttonViewDatabase_Click);
            // 
            // ChessServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 318);
            this.Controls.Add(this.buttonViewDatabase);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonBind);
            this.Controls.Add(this.comboBoxIpBind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxInTheGame);
            this.Controls.Add(this.labelFreeClients);
            this.Controls.Add(this.listBoxFreeClients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChessServerForm";
            this.Text = "ChessServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFreeClients;
        private System.Windows.Forms.Label labelFreeClients;
        private System.Windows.Forms.ListBox listBoxInTheGame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxIpBind;
        private System.Windows.Forms.Button buttonBind;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonViewDatabase;
    }
}

