using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
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
            _volumeHandler = new VolumeHandler(ref MediaElementMainWindow,
                   ref SliderMainWindowSoundSlider,ref ImageMainWindowVolumePic);
            IList<IMediaEntry> mediaEntries = searchForFilesAndGetInfo();

            _databaseHandler = new DatabaseHandler(mediaEntries);
        }

        private static IList<IMediaEntry> searchForFilesAndGetInfo()
        {
            //Jess' code starts here
            ArrayList files = searchForFiles();

            //Jess' code ends here

            //Chelsea's code begins here
            IList<IMediaEntry> mediaEntries = getMediaInfo(files);

            return mediaEntries;
        }

        private static IList<IMediaEntry> getMediaInfo(IEnumerable files)
        {
            IList<IMediaEntry> mediaEntries = new List<IMediaEntry>();

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
                    fs.Read(tag.TAGID, 0, tag.TAGID.Length);
                    fs.Read(tag.Title, 0, tag.Title.Length);
                    fs.Read(tag.Artist, 0, tag.Artist.Length);
                    fs.Read(tag.Album, 0, tag.Album.Length);
                    fs.Read(tag.Year, 0, tag.Year.Length);
                    fs.Read(tag.Comment, 0, tag.Comment.Length);
                    fs.Read(tag.Genre, 0, tag.Genre.Length);
                    string theTAGID = Encoding.Default.GetString(tag.TAGID);

                    if (!theTAGID.Equals("TAG"))
                    {
                        continue;
                    }
                    string Title = Encoding.Default.GetString(tag.Title);
                    string Artist = Encoding.Default.GetString(tag.Artist);
                    string Album = Encoding.Default.GetString(tag.Album);
                    string Year = Encoding.Default.GetString(tag.Year);
                    string Comment = Encoding.Default.GetString(tag.Comment);
                    string Genre = Encoding.Default.GetString(tag.Genre);
                    //long Length = Encoding.Default.GetString(tag.)

                    Console.WriteLine();
                    Console.WriteLine("Title: " + Title);
                    Console.WriteLine("Artist: " + Artist);
                    Console.WriteLine("Album: " + Album);
                    Console.WriteLine("Year: " + Year);
                    Console.WriteLine("Comment: " + Comment);
                    Console.WriteLine("Genre: " + Genre);
                    Console.WriteLine();

                    // MusicEntry(string genre, string title, long length, string artist, string filePath)
                    MusicEntry m = new MusicEntry(Genre, Title, 0, Artist, filePath);
                    mediaEntries.Add(m);
                }
            }
            return mediaEntries;
        }

        private static ArrayList searchForFiles()
        {
            string[] mp3Files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.mp3");
            string[] aviFiles = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.avi");
            Console.WriteLine("Current Working Directory: " + System.IO.Directory.GetCurrentDirectory());
            ArrayList files = new ArrayList(mp3Files.Length + aviFiles.Length);
            if (mp3Files.Length == 0)
            {
                Console.WriteLine("Error: No_MP3_Doge"); // (╯°□°）╯︵ ┻━┻
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
                Console.WriteLine("Error: No_AVI_Doge"); // (╯°□°）╯︵ ┻━┻
            }
            else
            {
                foreach (string t in aviFiles)
                {
                    Console.WriteLine(t);
                    files.Add(t);
                }
            }

            Console.WriteLine("ArrayList: ");
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
                            Environment.Version.ToString() +
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

        private readonly IToggle _pausePlayToggle;
        private readonly VolumeHandler _volumeHandler;
        private QuickSearchWindow _quickSearchWindow;
        private AdvancedSearchWindow _advancedSearchWindow;
        private readonly DatabaseHandler _databaseHandler;

    }
}
