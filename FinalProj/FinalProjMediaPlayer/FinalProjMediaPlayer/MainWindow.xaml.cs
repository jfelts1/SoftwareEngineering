using System;
using System.Collections;
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
using FinalProjMediaPlayer.Interfaces;

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
            _pausePlayToggle = new FunctionImageToggle(new BitmapImage(new Uri("pack://application:,,,/Icons/Symbols_Play_16xLG.png")),
                                               ref ImageMainWindowPausePlayButton,
                () =>
                {
                    if (MediaElementMainWindow.IsLoaded &&
                        MediaElementMainWindow.LoadedBehavior == MediaState.Manual &&
                        MediaElementMainWindow.Clock == null)
                    {
                        MediaElementMainWindow.Play();
                        //MessageBox.Show("Play");
                    }
                },
                () =>
                {
                    if (MediaElementMainWindow.IsLoaded &&
                        MediaElementMainWindow.LoadedBehavior == MediaState.Manual &&
                        MediaElementMainWindow.Clock == null)
                    {
                        MediaElementMainWindow.Pause();
                        //MessageBox.Show("Pause");
                    }
                });
            _volumeOnOffToggle = new ImageToggle<double>(new BitmapImage(new Uri("pack://application:,,,/Icons/SoundfileNoSound_461.png")), 
                                                 ref ImageMainWindowVolumePic);
            SliderMainWindowSoundSlider.Value = Globals.MaxSliderValue;
            MediaElementMainWindow.Volume = Globals.MaxVolume;
            //TODO: fill this list
            IList<IMediaEntry> mediaEntries = new List<IMediaEntry>();
            //dummy data can be removed when real data is available
            mediaEntries.Add(new MusicEntry("qwerty","aaa",1000,"zxcvbnm","tehawetk/asdf.txt"));

            _databaseHandler = new DatabaseHandler(mediaEntries);
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
            _pausePlayToggle.toggle();
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
                SliderMainWindowSoundSlider.Value = Globals.MaxSliderValue;
                MediaElementMainWindow.Volume = Globals.MaxVolume;
            }
        }

        private void SliderMainWindowSoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_volumeOnOffToggle == null) return;

            double tmp = e.NewValue/Globals.MaxSliderValue;
            /*if (tmp <= 1 && tmp >= 0)
            {
                MediaElementMainWindow.Volume = tmp;
            }
            else
            {
                MessageBox.Show("Invalid Volume: "+tmp);
            }*/

            if (Math.Abs(SliderMainWindowSoundSlider.Value) < Globals.DoubleTolerance)
            {
                _volumeOnOffToggle.forceOn(0.0);
            }
            else
            {
                _volumeOnOffToggle.forceOff(tmp);
            }

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

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _databaseHandler.shutdownDatabaseConnection();
        }

        private readonly IToggle _pausePlayToggle;
        private readonly IToggle<double> _volumeOnOffToggle;
        private QuickSearchWindow _quickSearchWindow;
        private AdvancedSearchWindow _advancedSearchWindow;
        private DatabaseHandler _databaseHandler;

    }
}
