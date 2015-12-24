namespace Chess
{
    partial class SelectСontenderPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxContenders = new System.Windows.Forms.ListBox();
            this.buttonWait = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxContenders
            // 
            this.listBoxContenders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxContenders.FormattingEnabled = true;
            this.listBoxContenders.ItemHeight = 21;
            this.listBoxContenders.Location = new System.Drawing.Point(13, 13);
            this.listBoxContenders.Name = "listBoxContenders";
            this.listBoxContenders.Size = new System.Drawing.Size(274, 235);
            this.listBoxContenders.TabIndex = 0;
            // 
            // buttonWait
            // 
            this.buttonWait.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonWait.Location = new System.Drawing.Point(203, 254);
            this.buttonWait.Name = "buttonWait";
            this.buttonWait.Size = new System.Drawing.Size(84, 33);
            this.buttonWait.TabIndex = 2;
            this.buttonWait.Text = "Wait";
            this.buttonWait.UseVisualStyleBackColor = true;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonUpdate.Location = new System.Drawing.Point(13, 254);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(93, 33);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonConnect.Location = new System.Drawing.Point(112, 254);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(85, 33);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            // 
            // SelectСontenderPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonWait);
            this.Controls.Add(this.listBoxContenders);
            this.Name = "SelectСontenderPage";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(300, 300);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listBoxContenders;
        public System.Windows.Forms.Button buttonWait;
        public System.Windows.Forms.Button buttonUpdate;
        public System.Windows.Forms.Button buttonConnect;
    }
}