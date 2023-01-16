using RoomEnvironmentMonitor_.Stores;
using System;
using System.Globalization;
using System.IO;
using System.Windows;

namespace RoomEnvironmentMonitor_.Services
{
    public class ReadWriteSettingsService
    {
        private string FilePath { get; }
        private string[] UserSettings { get; set; }

        UserSettingsStore settingsStore;

        public ReadWriteSettingsService(string filePath, UserSettingsStore settingsStore)
        {
            this.FilePath = filePath;
            this.settingsStore = settingsStore;
        }

        public void ReadSettings() 
        {
            try
            {
                UserSettings = File.ReadAllLines(FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "StartUpSettings file error", MessageBoxButton.OK);
                throw;
            }

            try
            {
                settingsStore.Opacity = double.Parse(UserSettings[0], NumberStyles.Any, CultureInfo.InvariantCulture);
                settingsStore.Left = double.Parse(UserSettings[1]);
                settingsStore.Top = double.Parse(UserSettings[2]);
                settingsStore.ComName = UserSettings[3];
                settingsStore.BaudRate = int.Parse(UserSettings[4]);
                settingsStore.RefreshRate = int.Parse(UserSettings[5]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Invalid Settings", MessageBoxButton.OK);
                throw;
            }
        }


        public void WriteSettings()
        {
            try
            {
                UserSettings[0] = settingsStore.Opacity.ToString().Replace(",", ".");
                UserSettings[1] = settingsStore.Left.ToString();
                UserSettings[2] = settingsStore.Top.ToString();
                UserSettings[3] = settingsStore.ComName;
                UserSettings[4] = settingsStore.BaudRate.ToString();
                UserSettings[5] = settingsStore.RefreshRate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Invalid Settings", MessageBoxButton.OK);
                throw;
            }

            File.WriteAllLines(FilePath, UserSettings);
        }
    }
}
