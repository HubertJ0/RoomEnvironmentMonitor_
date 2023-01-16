using Navigation.WPF.ViewModels;
using RoomEnvironmentMonitor_.Services;
using RoomEnvironmentMonitor_.Stores;

namespace RoomEnvironmentMonitor_.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private string temperature = "???";
        public string Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }

        private string light = "???";
        public string Light
        {
            get { return light; }
            set
            {
                light = value;
                OnPropertyChanged(nameof(Light));
            }
        }

        private string humidity = "???";
        public string Humidity
        {
            get { return humidity; }
            set
            {
                humidity = value;
                OnPropertyChanged(nameof(Humidity));
            }
        }

        public double Left
        {
            get { return settingsStore.Left; }
            set { settingsStore.Left = value; }
        }

        public double Top 
        {
            get { return settingsStore.Top; }
            set { settingsStore.Top = value; }
        }

        public double Opacity => settingsStore.Opacity;
        public string ComName => settingsStore.ComName;
        public int BaudRate => settingsStore.BaudRate;
        public int RefreshRate => settingsStore.RefreshRate;

        #endregion

        private readonly ReadSensorService sensorService;
        private readonly UserSettingsStore settingsStore;

        public MainViewModel(UserSettingsStore settingsStore)
        {
            this.settingsStore = settingsStore;
            this.sensorService = new ReadSensorService(ComName, BaudRate, RefreshRate);
            sensorService.DataUpdatedEventChandler += SensorService_DataUpdatedEventChandler;

            sensorService.RunService();
        }

        private void SensorService_DataUpdatedEventChandler(object sender, Helpers.DataUpdatedEventArgs e)
        {
            Temperature = e.Temperature;
            Humidity = e.Humidity;
            Light= e.Light;
        }
    }
}
