using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
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
#if DEBUG
            Environment.SetEnvironmentVariable("SQLite_ForceLogPrepare","True");
#endif
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

            _volumeHandler = new VolumeHandler(ref MediaElementMainWindow,
                ref SliderMainWindowSoundSlider,
                ref ImageMainWindowVolumePic,
                new BitmapImage(new Uri("pack://application:,,,/Icons/SoundfileNoSound_461.png")));

            IList<MediaEntry> mediaEntries = searchForFilesAndGetInfo();

            _databaseHandler = new DatabaseHandler(mediaEntries);
            _mediaDict = new Dictionary<string, MediaEntry>();
            initListBoxValues(mediaEntries,ListBoxMainWindowRecentlyPlayed);
            _defaultPlaylist = new Playlist(ListBoxMainWindowRecentlyPlayed.Items);
            _currentPlaylist = new Playlist(ListBoxMainWindowRecentlyPlayed.Items);
            _timeSliderHandler = new TimeSliderHandler(ref MediaElementMainWindow,
                ref SliderMainWindowTimeSlider,
                ref LabelMainWindowTimer);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += (source, e) => { _timeSliderHandler.updateSliderPosition(source,e);};
            _timer.Start();
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
                            Environment.Version +
                            (Environment.Is64BitOperatingSystem? " 64 bit" : " 32 bit"));
        }

        private void volumeOnOffToggle(object sender, MouseButtonEventArgs e)
        {
            _volumeHandler.toggle();
        }

        private void SliderMainWindowSoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_volumeHandler != null)
            {
                _volumeHandler.setVolume(sender,e);
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
            _timer.Stop();
        }

        private void ListBoxMainWindowRecentlyPlayed_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IList selected =  ListBoxMainWindowRecentlyPlayed.SelectedItems;
            _currentPlaylist.Current = selected[0];
            string s = selected[0] as string;
            playMedia(s);
        }

        private void TextBoxMainWindowQuickSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_databaseHandler == null)
            {
                return;
            }
            IEnumerable<string> tmp = _databaseHandler.searchByArtist(TextBoxMainWindowQuickSearch.Text);
            IEnumerable<string> enumerable = tmp as string[] ?? tmp.ToArray();
#if DEBUG
            Console.WriteLine(@"Found Items");
            foreach (var ele in enumerable)
            {
               Console.WriteLine(ele); 
            }
#endif
            populateListBox(enumerable,ListBoxMainWindowRecentlyPlayed);
        }

        private void MediaElementMainWindow_MediaEnded(object sender, RoutedEventArgs e)
        {
            string s;
            if (CheckBoxMainWindowRepeat.IsChecked == true)
            {
                s = _currentPlaylist.Current as string;
            }
            else
            {
                bool notEndOfList =_currentPlaylist.MoveNext();
                if (notEndOfList)
                {                
                    s = _currentPlaylist.Current as string;
                }
                else
                {
                    _currentPlaylist.Reset();
                    s = _currentPlaylist.Current as string;
                }              
            }
            playMedia(s);
        }

        private void SliderMainWindowTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _timeSliderHandler.updateMediaPosition();
        }

        private readonly IToggle _pausePlayToggle;
        private readonly VolumeHandler _volumeHandler;
        private QuickSearchWindow _quickSearchWindow;
        private AdvancedSearchWindow _advancedSearchWindow;
        private readonly DatabaseHandler _databaseHandler;
        private readonly Dictionary<string, MediaEntry> _mediaDict;
        private Playlist _currentPlaylist;
        private readonly Playlist _defaultPlaylist;
        private readonly TimeSliderHandler _timeSliderHandler;
        private MediaEntry _currentlyPlaying;
        private readonly DispatcherTimer _timer;
    }
}
