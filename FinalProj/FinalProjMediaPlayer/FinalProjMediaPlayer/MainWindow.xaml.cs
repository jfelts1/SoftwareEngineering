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
            _pausePlayToggle = new ImageToggle(new BitmapImage(new Uri("pack://application:,,,/Icons/Symbols_Play_16xLG.png")),
                                               ref ImageMainWindowPausePlayButton);
            _volumeOnOffToggle = new ImageToggle(new BitmapImage(new Uri("pack://application:,,,/Icons/SoundfileNoSound_461.png")), 
                                                 ref ImageMainWindowVolumePic);
            SliderMainWindowSoundSlider.Value = 10;
            MediaElementMainWindow.Volume = 1;
        }

        public void closeQuickSearchWindow()
        {
            _quickSearchWindow.Close();
        }

        public void closeAdvancedSearchWindow()
        {
            _advancedSearchWindow.Close();
        }

        private void exitProgram(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void pausePlayToggle(object sender, MouseButtonEventArgs e)
        {
            bool t = _pausePlayToggle.toggle();
            //TODO: do function logic
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
            bool t = _volumeOnOffToggle.toggle();
            if (t)
            {
                SliderMainWindowSoundSlider.Value = 0;
                MediaElementMainWindow.Volume = 0;
            }
            else
            {
                SliderMainWindowSoundSlider.Value = 10;
                MediaElementMainWindow.Volume = 1;
            }
        }

        private void SliderMainWindowSoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_volumeOnOffToggle == null) return;
            if (Math.Abs(SliderMainWindowSoundSlider.Value) < .0001)
            {
                _volumeOnOffToggle.forceOn();
            }
            else
            {
                _volumeOnOffToggle.forceOff();
            }
            //TODO: do function logic
        }

        private void openQuickSearchWindow(object sender, RoutedEventArgs e)
        {
            _quickSearchWindow = new QuickSearchWindow(this);
            _quickSearchWindow.Show();
        }

        private void openAdvancedSearchWindow(object sender, RoutedEventArgs e)
        {
            _advancedSearchWindow = new AdvancedSearchWindow(this);
            _advancedSearchWindow.Show();
        }

        private readonly IToggle _pausePlayToggle;
        private readonly IToggle _volumeOnOffToggle;
        private QuickSearchWindow _quickSearchWindow;
        private AdvancedSearchWindow _advancedSearchWindow;

    }
}
