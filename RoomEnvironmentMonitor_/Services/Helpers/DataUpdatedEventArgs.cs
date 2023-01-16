
namespace RoomEnvironmentMonitor_.Helpers
{
    public class DataUpdatedEventArgs
    {
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string Light { get; set; }

        public DataUpdatedEventArgs(string temperature, string humidity, string light)
        {
            Temperature = temperature;
            Humidity = humidity;
            Light = light;
        }
    }
}
