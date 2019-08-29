using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace RehabMakerV2.Utils
{
    public static class Settings
    {
        private static ISettings AppSettings { get { return CrossSettings.Current; } }

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        public static string LastUsedDevices
        {
            get { return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(SettingsKey, value); }
        }
    }
}

