using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
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

        private void populateListBox(IEnumerable<MediaEntry> mediaEntries,ItemsControl box)
        {
            foreach (MediaEntry entry in mediaEntries)
            {
                box.Items.Add(entry.ToString());
                _mediaDict.Add(entry.ToString(),entry);
            }
        }

        private static IList<MediaEntry> searchForFilesAndGetInfo()
        {
            //Jess' code starts here
            ArrayList files = searchForFiles();

            //Jess' code ends here

            //Chelsea's code begins here
            IList<MediaEntry> mediaEntries = getMediaInfo(files);

            return mediaEntries;
        }

        private static IList<MediaEntry> getMediaInfo(IEnumerable files)
        {
            IList<MediaEntry> mediaEntries = new List<MediaEntry>();

            foreach (string filePath in from object filePathObject in files select filePathObject.ToString())
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    if (fs.Length < 128)
                    {
                        continue;
                    }
                    MusicID3Tag tag = new MusicID3Tag();
                    fs.Seek(-128, SeekOrigin.End);
                    fs.Read(tag.tagid, 0, tag.tagid.Length);
                    fs.Read(tag.title, 0, tag.title.Length);
                    fs.Read(tag.artist, 0, tag.artist.Length);
                    fs.Read(tag.album, 0, tag.album.Length);
                    fs.Read(tag.year, 0, tag.year.Length);
                    fs.Read(tag.comment, 0, tag.comment.Length);
                    fs.Read(tag.genre, 0, tag.genre.Length);
                    string theTagid = Encoding.Default.GetString(tag.tagid);

                    if (!theTagid.Equals("TAG"))
                    {
                        continue;
                    }
                    string title = Encoding.Default.GetString(tag.title).removeNullTerminater().removeControlCharacters();
                    string artist = Encoding.Default.GetString(tag.artist).removeNullTerminater().removeControlCharacters();
                    string album = Encoding.Default.GetString(tag.album).removeNullTerminater().removeControlCharacters();
                    string year = Encoding.Default.GetString(tag.year).removeNullTerminater().removeControlCharacters();
                    string comment = Encoding.Default.GetString(tag.comment).removeNullTerminater().removeControlCharacters();
                    string genre = Encoding.Default.GetString(tag.genre).removeNullTerminater().removeControlCharacters();
                    //long Length = Encoding.Default.GetString(tag.)

                    if (string.IsNullOrEmpty(title))
                    {
                        title = Path.GetFileNameWithoutExtension(filePath);
                    }
                    if (string.IsNullOrEmpty(artist))
                    {
                        artist = "N/A";
                    }
                    if (string.IsNullOrEmpty(album))
                    {
                        album = "N/A";
                    }
                    if (string.IsNullOrEmpty(year))
                    {
                        year = "N/A";
                    }
                    if (string.IsNullOrEmpty(comment))
                    {
                        comment = "N/A";
                    }
                    if (string.IsNullOrEmpty(genre))
                    {
                        genre = "N/A";
                    }

                    Console.WriteLine();
                    Console.WriteLine(@"Title: " + title);
                    Console.WriteLine(@"Artist: " + artist);
                    Console.WriteLine(@"Album: " + album);
                    Console.WriteLine(@"Year: " + year);
                    Console.WriteLine(@"Comment: " + comment);
                    Console.WriteLine(@"Genre: " + genre);
                    Console.WriteLine();

                    MusicEntry m = new MusicEntry(genre, title, 0, artist, filePath);
                    mediaEntries.Add(m);
                }
            }
            return mediaEntries;
        }

        private static ArrayList searchForFiles()
        {
            string[] mp3Files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.mp3");
            string[] aviFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.avi");
            Console.WriteLine(@"Current Working Directory: " + Directory.GetCurrentDirectory());
            ArrayList files = new ArrayList(mp3Files.Length + aviFiles.Length);
            if (mp3Files.Length == 0)
            {
                Console.WriteLine(@"Error: No_MP3_Doge"); // (╯°□°）╯︵ ┻━┻
            }
            else
            {
                foreach (string t in mp3Files)
                {
                    Console.WriteLine(t);
                    files.Add(t);
                }
            }
            if (aviFiles.Length == 0)
            {
                Console.WriteLine(@"Error: No_AVI_Doge"); // (╯°□°）╯︵ ┻━┻
            }
            else
            {
                foreach (string t in aviFiles)
                {
                    Console.WriteLine(t);
                    files.Add(t);
                }
            }

            Console.WriteLine(@"ArrayList: ");
            foreach (object curLine in files)
            {
                Console.WriteLine(curLine);
            }
            Console.ReadLine();
            return files;
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
                //MessageBox.Show(selectedEntry+" selected.");
            }
            else
            {
                throw new TypeAccessException("selectedValue is not a string");
            }
        }

        private readonly IToggle _pausePlayToggle;
        private readonly VolumeHandler _volumeHandler;
        private QuickSearchWindow _quickSearchWindow;
        private AdvancedSearchWindow _advancedSearchWindow;
        private readonly DatabaseHandler _databaseHandler;
        private readonly Dictionary<string, MediaEntry> _mediaDict;

    }
}
