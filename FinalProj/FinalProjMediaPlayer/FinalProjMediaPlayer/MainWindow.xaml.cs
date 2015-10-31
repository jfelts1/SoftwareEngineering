using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProjMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _pausePlayImageToggle = new ImageToggle(new BitmapImage(new Uri("pack://application:,,,/Icons/Symbols_Play_16xLG.png")),
                                                    ref ImageMainWindowPausePlayButton);
            _volumeOnOffImageToggle = new ImageToggle(new BitmapImage(new Uri("pack://application:,,,/Icons/SoundfileNoSound_461.png")), 
                                                    ref ImageMainWindowVolumePic);
            SliderMainWindowSoundSlider.Value = 100;
        }

        private void exitProgram(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void pausePlayToggle(object sender, MouseButtonEventArgs e)
        {
            bool t = _pausePlayImageToggle.toggle();

        }

        private void openAboutWindow(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created by Team-Ctrl-Alt-Delete\n" +
                            "James Felts, Jess Albrecht, Chelsea Davis\n" +
                            ".NET Framework Version: " +
                            Environment.Version.ToString() +
                            (Environment.Is64BitOperatingSystem? " 64 bit" : " 32 bit"));
        }

        private void volumeOnOffToggle(object sender, MouseButtonEventArgs e)
        {
            bool t = _volumeOnOffImageToggle.toggle();
            if (t)
            {
                SliderMainWindowSoundSlider.Value = 0;
            }
            else
            {
                SliderMainWindowSoundSlider.Value = 100;
            }

        }

        private void SliderMainWindowSoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(SliderMainWindowSoundSlider.Value) < .0001)
            {
                _volumeOnOffImageToggle.forceOff();
            }
            else
            {
                _volumeOnOffImageToggle.forceOn();
            }

        }

        private readonly ImageToggle _pausePlayImageToggle;
        private readonly ImageToggle _volumeOnOffImageToggle;

    }
}
