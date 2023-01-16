
namespace RoomEnvironmentMonitor_.Stores
{
    public class UserSettingsStore
    {
        private double opacity;
        public double Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        private double left;
        public double Left
        {
            get { return left; }
            set { left = value; }
        }

        private double top;
        public double Top
        {
            get { return top; }
            set { top = value; }
        }

        private string comName;
        public string ComName
        {
            get { return comName; }
            set { comName = value; }
        }

        private int baudRate;
        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        private int refreshRate;
        public int RefreshRate
        {
            get { return refreshRate; }
            set { refreshRate = value; }
        }
    }
}
