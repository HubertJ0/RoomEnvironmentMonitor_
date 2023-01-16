using RoomEnvironmentMonitor_.Services;
using RoomEnvironmentMonitor_.Stores;
using RoomEnvironmentMonitor_.ViewModels;
using RoomEnvironmentMonitor_.Views;
using System;
using System.Drawing;
using System.Windows;
using Forms = System.Windows.Forms;

namespace RoomEnvironmentMonitor_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string IconPath = @"Icon.ico";
        private Forms.NotifyIcon notifyIcon;
        private UserSettingsStore settingsStore;
        private ReadWriteSettingsService settingsService;

        protected override void OnStartup(StartupEventArgs e)
        {
            notifyIcon = new Forms.NotifyIcon();
            settingsStore = new UserSettingsStore();
            settingsService = new ReadWriteSettingsService("StartUpSettings.txt", settingsStore);
            settingsService.ReadSettings();

            notifyIcon.Icon = new Icon(IconPath);
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = new();
            notifyIcon.ContextMenuStrip.Items.Add("Exit", Image.FromFile(IconPath), OnStartup);

            MainWindow = new MainView();
            MainWindow.DataContext = new MainViewModel(settingsStore);

            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            settingsService.WriteSettings();
            notifyIcon.Dispose();
            base.OnExit(e);
        }

        private void OnStartup(object? sender, EventArgs e) => Shutdown();
    }
}
