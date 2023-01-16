using RoomEnvironmentMonitor_.Helpers;
using System;
using System.IO.Ports;
using System.Timers;
using System.Windows;

namespace RoomEnvironmentMonitor_.Services
{
    public class ReadSensorService
    {
        #region Properties

        private string ComName { get; }
        private int BaudRate { get; }
        private int RefreshRate { get; }

        public string? Temperature { get; set; }
        public string? Humidity { get; set; }
        public string? Light { get; set; }

        #endregion

        public delegate void DataUpdated(object sender ,DataUpdatedEventArgs e);
        public event DataUpdated? DataUpdatedEventChandler;

        private readonly Timer refreshTimer;
        private readonly SerialPort serialPort;
        public ReadSensorService(string comName, int baudRate, int refreshRate)
        {
            ComName = comName;
            BaudRate = baudRate;
            RefreshRate = refreshRate;

            refreshTimer = new Timer(RefreshRate);
            serialPort = new SerialPort(ComName, BaudRate);

            refreshTimer.Elapsed += RefreshTimer_Elapsed;
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void RefreshTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                serialPort.WriteLine("SendData!");
            }
            catch (Exception)
            {
                RunService();
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Temperature = "";
            Humidity = "";
            Light = "";

            string receivedData = serialPort.ReadLine();
            Temperature += receivedData[0];
            Temperature += receivedData[1];
            Humidity += receivedData[2];
            Humidity += receivedData[3];

            // The light level value can vary in length
            for (int i = 4; i < receivedData.Length - 1; i++)
                Light += receivedData[i];

            DataUpdatedEventChandler?.Invoke(this, new DataUpdatedEventArgs(Temperature, Humidity, Light));
        }

        public void RunService()
        {
        TryAgain:
            try
            {
                serialPort.Open();
            }
            catch (Exception e)
            {
                refreshTimer?.Stop();
                if (MessageBox.Show(e.Message.ToString() + "\ntry again?", "COM error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    goto TryAgain;
                else throw;
            }
            refreshTimer?.Start();
        }
    }
}
