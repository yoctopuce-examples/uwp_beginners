using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        private NonReentrantDispatcherTimer timer;
        private int hardwaredetect = 0;
        private YSensor sensor;


        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            HwIdTextBox.Text = "No sensor detected";
            TempTextBox.Text = "N/A";
            try {
                await YAPI.RegisterHub("usb");
            } catch (YAPI_Exception ex) {
                var dialog = new MessageDialog("Init error:" + ex.Message);
                await dialog.ShowAsync();
                return;
            }

            timer = new NonReentrantDispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.TickTask = async () => { await dispatcherTimer_Tick(); };
            timer.Start();
        }

        async Task dispatcherTimer_Tick()
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


    public class NonReentrantDispatcherTimer : DispatcherTimer
    {
        private bool IsRunning;

        public NonReentrantDispatcherTimer()
        {
            base.Tick += SmartDispatcherTimer_Tick;
        }

        async void SmartDispatcherTimer_Tick(object sender, object e)
        {
            if (TickTask == null || IsRunning) {
                return;
            }

            try {
                IsRunning = true;
                await TickTask.Invoke();
            } finally {
                IsRunning = false;
            }
        }
        public Func<Task> TickTask { get; set; }
    }
}