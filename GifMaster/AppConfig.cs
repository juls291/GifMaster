using Newtonsoft.Json;

namespace GifMaster
{
    public class AppConfig
    {
        #region Fields

        private const string ConfigFileName = "appconfig.json";

        public string ApiKey { get; set; }
        public int MaxResults { get; set; }

        #endregion

        #region Constructor

        private AppConfig()
        {
            ApiKey = string.Empty;
            MaxResults = 24;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the existing configuration or creates a new one if it doesn't exist.
        /// </summary>
        public static AppConfig GetOrCreate()
        {
            if (File.Exists(ConfigFileName))
            {
                try
                {
                    string configJson = File.ReadAllText(ConfigFileName);
                    return JsonConvert.DeserializeObject<AppConfig>(configJson) ?? new AppConfig();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading configuration: {ex.Message}");
                }
            }
            AppConfig _config = new AppConfig();
            _config.Save();
            return _config;
        }

        /// <summary>
        /// Saves the current configuration to the configuration file.
        /// </summary>
        public void Save()
        {
            try
            {
                string configJson = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(ConfigFileName, configJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving configuration: {ex.Message}");
            }
        }

        #endregion
    }
}