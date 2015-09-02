using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaspberryLearning
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Constants

        private const int RED_LED_PIN = 18;
        private const int BLUE_LED_PIN = 24;
        private const int BUZZER_PIN = 22;

        #endregion

        private bool _redIsOn;
        private bool _blueIsOn;
        private bool _buzzerIsOn;

        private GpioPin _redPin;
        private GpioPin _bluePin;
        private GpioPin _buzzerPin;

        private SolidColorBrush _redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush _greenBrush = new SolidColorBrush(Windows.UI.Colors.Green);

        public MainPage()
        {
            this.InitializeComponent();

            InitGpio();
        }

        private void InitGpio()
        {
            var gpio = GpioController.GetDefault();

            if(gpio == null)
            {
                _redPin = null;
                _bluePin = null;
                _buzzerPin = null;

                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            _redPin = gpio.OpenPin(RED_LED_PIN);
            _bluePin = gpio.OpenPin(BLUE_LED_PIN);
            _buzzerPin = gpio.OpenPin(BUZZER_PIN);

            _redPin.SetDriveMode(GpioPinDriveMode.Output);
            _bluePin.SetDriveMode(GpioPinDriveMode.Output);
            _buzzerPin.SetDriveMode(GpioPinDriveMode.Output);

            _redPin.Write(GpioPinValue.Low);
            _bluePin.Write(GpioPinValue.Low);
            _buzzerPin.Write(GpioPinValue.Low);

            GpioStatus.Text = "GPIO pin initialized correctly";
        }

        private void RedLedClick(object sender, RoutedEventArgs e)
        {
            if(_redPin != null)
            {
                if(_redIsOn)
                {
                    _redPin.Write(GpioPinValue.Low);
                    _redIsOn = false;
                    RedButton.Background = _redBrush;
                    RedButton.Content = "Red led on";
                }
                else
                {
                    _redPin.Write(GpioPinValue.High);
                    _redIsOn = true;
                    RedButton.Background = _greenBrush;
                    RedButton.Content = "Red led off";
                }
            }
        }

        private void BlueLedClick(object sender, RoutedEventArgs e)
        {
            if (_bluePin != null)
            {
                if (_blueIsOn)
                {
                    _bluePin.Write(GpioPinValue.Low);
                    _blueIsOn = false;
                    BlueButton.Background = _redBrush;
                    BlueButton.Content = "Blue led on";
                }
                else
                {
                    _bluePin.Write(GpioPinValue.High);
                    _blueIsOn = true;
                    BlueButton.Background = _greenBrush;
                    BlueButton.Content = "Blue led off";
                }
            }
        }

        private void BuzzerClick(object sender, RoutedEventArgs e)
        {
            if (_buzzerPin != null)
            {
                if (_buzzerIsOn)
                {
                    _buzzerPin.Write(GpioPinValue.Low);
                    _buzzerIsOn = false;
                    BuzzerButton.Background = _redBrush;
                    BuzzerButton.Content = "Buzzer on";
                }
                else
                {
                    _buzzerPin.Write(GpioPinValue.High);
                    _buzzerIsOn = true;
                    BuzzerButton.Background = _greenBrush;
                    BuzzerButton.Content = "Buzzer off";
                }
            }
        }
    }
}
