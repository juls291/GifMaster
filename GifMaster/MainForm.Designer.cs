namespace GifMaster
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tbxSearch = new TextBox();
            flpGifPanel = new FlowLayoutPanel();
            btnSettings = new Button();
            label1 = new Label();
            lblVersion = new Label();
            SuspendLayout();
            // 
            // tbxSearch
            // 
            tbxSearch.Location = new Point(12, 12);
            tbxSearch.Name = "tbxSearch";
            tbxSearch.Size = new Size(458, 23);
            tbxSearch.TabIndex = 0;
            tbxSearch.TextChanged += tbxSearch_TextChanged;
            // 
            // flpGifPanel
            // 
            flpGifPanel.AutoScroll = true;
            flpGifPanel.Location = new Point(12, 41);
            flpGifPanel.Name = "flpGifPanel";
            flpGifPanel.Size = new Size(492, 339);
            flpGifPanel.TabIndex = 1;
            // 
            // btnSettings
            // 
            btnSettings.Image = Properties.Resources.settingsIcon;
            btnSettings.Location = new Point(476, 12);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(28, 23);
            btnSettings.TabIndex = 2;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(435, 384);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 3;
            label1.Text = "made by juha";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(1, 384);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(51, 15);
            lblVersion.TabIndex = 4;
            lblVersion.Text = "Version: ";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 400);
            Controls.Add(lblVersion);
            Controls.Add(label1);
            Controls.Add(btnSettings);
            Controls.Add(flpGifPanel);
            Controls.Add(tbxSearch);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "GifMaster";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbxSearch;
        private FlowLayoutPanel flpGifPanel;
        private Button btnSettings;
        private Label label1;
        private Label lblVersion;
    }
}
