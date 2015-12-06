using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using FinalProjMediaPlayer.Extensions;
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

            _volumeHandler = new VolumeHandler(ref MediaElementMainWindow,
                ref SliderMainWindowSoundSlider,
                ref ImageMainWindowVolumePic,
                new BitmapImage(new Uri("pack://application:,,,/Icons/SoundfileNoSound_461.png")));

            IList<MediaEntry> mediaEntries = searchForFilesAndGetInfo();

            _databaseHandler = new DatabaseHandler(mediaEntries);
            _mediaDict = new Dictionary<string, MediaEntry>();
            populateListBox(mediaEntries,ListBoxMainWindowRecentlyPlayed);
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
        }

        private void ListBoxMainWindowRecentlyPlayed_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IList selected =  ListBoxMainWindowRecentlyPlayed.SelectedItems;
            var s = selected[0] as string;
            if (s != null)
            {          
                MediaEntry selectedEntry = _mediaDict[s];
                MediaElementMainWindow.loadMediaEntry(selectedEntry);
                MediaElementMainWindow.Play();
            }
            else
            {
                throw new TypeAccessException("selectedValue is not a string");
            }
        }

        private void TextBoxMainWindowQuickSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_databaseHandler != null)
            {              
                IEnumerable<string> tmp = _databaseHandler.searchByArtist(e.ToString());
            }
        }

        private readonly IToggle _pausePlayToggle;
        private readonly VolumeHandler _volumeHandler;
        private QuickSearchWindow _quickSearchWindow;
        private AdvancedSearchWindow _advancedSearchWindow;
        private readonly DatabaseHandler _databaseHandler;
        private readonly Dictionary<string, MediaEntry> _mediaDict;
        private IPlayList _currentPlayList;
    }
}
