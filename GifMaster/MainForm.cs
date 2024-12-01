using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace GifMaster
{
    public partial class MainForm : Form
    {
        #region Fields
        private const string VersionNo = "v1.0.0";

        private const string TenorApiUrl = "https://g.tenor.com/v2/search";
        private const string GitHubApiUrl = "http://api.github.com/repos/juls291/GifMaster/releases/latest";
        private System.Timers.Timer searchDelayTimer;
        private AppConfig config;
        private bool Debug = false;

        private const int WM_HOTKEY = 0x0312;
        private const int HotKeyId = 1;
        private const int MOD_ALT = 0x1;
        private const int MOD_CONTROL = 0x2;
        private const int MOD_SHIFT = 0x4;
        private const int MOD_WIN = 0x8;

        #endregion

        #region DLL Import

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            config = AppConfig.GetOrCreate();

            bool hotkeyRegistrationSuccessfull = RegisterHotKey(this.Handle, HotKeyId, MOD_CONTROL, (int)Keys.D1);

            if (Debug)
            {
                if (!hotkeyRegistrationSuccessfull)
                {
                    MessageBox.Show("Hotkey registration failed! Attempting to unregister and re-register the hotkey.");
                    UnregisterHotKey(this.Handle, HotKeyId);

                    hotkeyRegistrationSuccessfull = RegisterHotKey(this.Handle, HotKeyId, MOD_CONTROL, (int)Keys.D1);
                }

                if (hotkeyRegistrationSuccessfull)
                {
                    MessageBox.Show("Hotkey registration successful!");
                }
                else
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    MessageBox.Show($"Hotkey registration failed! Error Code: {errorCode}");
                }

            }

            searchDelayTimer = new System.Timers.Timer(500);
            searchDelayTimer.AutoReset = false;
            searchDelayTimer.Elapsed += SearchDelayTimer_Elapsed;
            lblVersion.Text += VersionNo;
        }

        #endregion

        #region Controls

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            searchDelayTimer.Stop();
            searchDelayTimer.Start();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(config);
            settingsForm.ShowDialog();
        }

        private void AddGifToPanel(string gifUrl)
        {
            PictureBox pictureBox = new PictureBox
            {
                Width = 150,
                Height = 150,
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = gifUrl,
                Cursor = Cursors.Hand,
                Tag = gifUrl
            };

            pictureBox.Click += GifPictureBox_Click;

            flpGifPanel.Controls.Add(pictureBox);
        }

        private async void GifPictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox && pictureBox.Tag != null)
            {
                string? gifUrl = pictureBox.Tag.ToString();

                if (string.IsNullOrEmpty(gifUrl))
                {
                    return;
                }

                try
                {
                    byte[] gifBytes = await DownloadGifBytesAsync(gifUrl);

                    string tempFilePath = Path.Combine(Path.GetTempPath(), $"temp_{Guid.NewGuid()}.gif");
                    await File.WriteAllBytesAsync(tempFilePath, gifBytes);

                    var fileDropList = new System.Collections.Specialized.StringCollection();
                    fileDropList.Add(tempFilePath);
                    Clipboard.SetFileDropList(fileDropList);

                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();

                }
                catch (Exception ex)
                {
                    if (Debug)
                    {
                        MessageBox.Show($"Failed to copy GIF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.WindowState == FormWindowState.Minimized)
            {
                OnMinimize();
            }
        }

        private void OnMinimize()
        {
            tbxSearch.Text = string.Empty;
        }

        #endregion

        #region Hotkey

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HotKeyId)
            {
                if (Debug)
                {
                    MessageBox.Show("Hotkey Pressed!");
                }

                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                }
                else
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HotKeyId);
            base.OnFormClosing(e);
        }

        #endregion

        #region Helper Methods

        private void SearchDelayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => PerformSearch()));
            }
            else
            {
                PerformSearch();
            }
        }

        private async void PerformSearch()
        {
            string searchQuery = tbxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                await SearchAndDisplayGifs(searchQuery);
            }
            else
            {
                flpGifPanel.Controls.Clear();
            }
        }

        private async Task SearchAndDisplayGifs(string query)
        {
            flpGifPanel.Controls.Clear();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string requestUrl = $"{TenorApiUrl}?q={query}&key={config.ApiKey}&limit={config.MaxResults}";
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {jsonResponse}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }   

                    JObject parsedResponse = JObject.Parse(jsonResponse);
                    var gifResults = parsedResponse["results"];

                    if (gifResults != null)
                    {
                        foreach (var result in gifResults)
                        {
                            var gifUrl = result["media_formats"]?["gif"]?["url"]?.ToString();
                            if (!string.IsNullOrEmpty(gifUrl))
                            {
                                AddGifToPanel(gifUrl);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching GIFs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task<byte[]> DownloadGifBytesAsync(string gifUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetByteArrayAsync(gifUrl);
            }
        }

        #endregion
    }
}