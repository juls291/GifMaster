namespace GifMaster
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            label1 = new Label();
            tbxApiKey = new TextBox();
            nudMaxResults = new NumericUpDown();
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)nudMaxResults).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "API Key";
            // 
            // tbxApiKey
            // 
            tbxApiKey.Location = new Point(85, 6);
            tbxApiKey.Name = "tbxApiKey";
            tbxApiKey.Size = new Size(241, 23);
            tbxApiKey.TabIndex = 1;
            // 
            // nudMaxResults
            // 
            nudMaxResults.Location = new Point(85, 37);
            nudMaxResults.Name = "nudMaxResults";
            nudMaxResults.Size = new Size(51, 23);
            nudMaxResults.TabIndex = 2;
            nudMaxResults.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 39);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 3;
            label2.Text = "MaxResults";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(152, 39);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(68, 15);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Get API Key";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(251, 37);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(345, 73);
            Controls.Add(btnSave);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(nudMaxResults);
            Controls.Add(tbxApiKey);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            Text = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize)nudMaxResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbxApiKey;
        private NumericUpDown nudMaxResults;
        private Label label2;
        private LinkLabel linkLabel1;
        private Button btnSave;
    }
}