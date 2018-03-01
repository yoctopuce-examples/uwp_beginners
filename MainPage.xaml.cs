using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using com.yoctopuce.YoctoAPI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DemoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
     

        public MainPage()
        {
            this.InitializeComponent();
        }

        private DispatcherTimer dispatcherTimer;
        private int hardwaredetect = 0;
        private YSensor sensor;



        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            try {
                await YAPI.RegisterHub("usb");
            } catch (YAPI_Exception ex) {
                var dialog = new MessageDialog("Init error:" + ex.Message);
                await dialog.ShowAsync();
                return;
            }

            HwIdTextBox.Text = "No sensor detected";
            TempTextBox.Text = "N/A";

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        async void dispatcherTimer_Tick(object sender, object e)
        {
            try {
                if (hardwaredetect == 0) {
                    await YAPI.UpdateDeviceList();
                }
                hardwaredetect = (hardwaredetect + 1) % 20;
                await YAPI.HandleEvents();
                if (sensor == null)
                    sensor = YSensor.FirstSensor();
                if (sensor != null) {
                    HwIdTextBox.Text = await sensor.get_friendlyName();
                    TempTextBox.Text = await sensor.get_currentValue() + await sensor.get_unit();
                }
            } catch (YAPI_Exception ex) {
                HwIdTextBox.Text = "Sensor is offline";
                TempTextBox.Text = "OFFLINE";
                sensor = null;
            }
        }
    }
}