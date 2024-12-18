﻿namespace GifMaster
{
    public partial class SettingsForm : Form
    {
        AppConfig Config;

        #region Constructor

        public SettingsForm(AppConfig config)
        {
            InitializeComponent();
            Config = config;
            initializeValues();
        }

        #endregion

        #region Methods

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://developers.google.com/tenor/guides/quickstart");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.ApiKey = tbxApiKey.Text;
            Config.MaxResults = (int)Math.Floor(nudMaxResults.Value);
            Config.Save();
            this.Close();
        }

        private void initializeValues()
        {
            tbxApiKey.Text = Config.ApiKey;
            nudMaxResults.Value = Config.MaxResults;
        }

        #endregion
    }
}