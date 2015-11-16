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
            _volumeOnOffToggle = new ImageToggle<double>(new BitmapImage(new Uri("pack://application:,,,/Icons/SoundfileNoSound_461.png")), 
                                                 ref ImageMainWindowVolumePic);
            SliderMainWindowSoundSlider.Value = Globals.MaxSliderValue;
            MediaElementMainWindow.Volume = Globals.MaxVolume;
            
            IList<IMediaEntry> mediaEntries = searchForFilesAndGetInfo();

            _databaseHandler = new DatabaseHandler(mediaEntries);
        }

        private IList<IMediaEntry> searchForFilesAndGetInfo()
        {
            //Jess' code starts here
            string[] mp3Files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.mp3");
            string[] aviFiles = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.avi");
            Console.WriteLine("Current Working Directory: " + System.IO.Directory.GetCurrentDirectory());
            ArrayList files = new ArrayList(mp3Files.Length + aviFiles.Length);
            if (mp3Files.Length == 0) Console.WriteLine("Error: No_MP3_Doge"); // (╯°□°）╯︵ ┻━┻
            else
            {
                foreach (string t in mp3Files)
                {
                    Console.WriteLine(t);
                    files.Add(t);
                }
            }
            if (aviFiles.Length == 0) Console.WriteLine("Error: No_AVI_Doge"); // (╯°□°）╯︵ ┻━┻
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

            //Jess' code ends here

            //Chelsea's code begins here
            string filePath = "";
            IList<IMediaEntry> mediaEntries = new List<IMediaEntry>();

            if(files.Count == 0) Console.WriteLine("Error: No_Files");
            else
            {
                foreach (object filePathObject in files)
                {
                    filePath = filePathObject.ToString();

                    using (FileStream fs = File.OpenRead(filePath))
                    {
                        //Meaning that tags have been added to the file, ie artist, genre, etc
                        if (fs.Length >= 128)
                        {
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

                            if (theTAGID.Equals("TAG"))
                            {
                                string mediaTitle = Encoding.Default.GetString(tag.Title);
                                string mediaArtist = Encoding.Default.GetString(tag.Artist);
                                string mediaAlbum = Encoding.Default.GetString(tag.Album);
                                string mediaYear = Encoding.Default.GetString(tag.Year);
                                string mediaComment = Encoding.Default.GetString(tag.Comment);
                                string mediaGenre = Encoding.Default.GetString(tag.Genre);
                                //long Length = Encoding.Default.GetString(tag.)

                                Console.WriteLine();
                                Console.WriteLine("Title: " + mediaTitle);
                                Console.WriteLine("Artist: " + mediaArtist);
                                Console.WriteLine("Album: " + mediaAlbum);
                                Console.WriteLine("Year: " + mediaYear);
                                Console.WriteLine("Comment: " + mediaComment);
                                Console.WriteLine("Genre: " + mediaGenre);
                                Console.WriteLine();

                                // MusicEntry(string genre, string title, long length, string artist, string filePath)
                                MusicEntry m = new MusicEntry(mediaGenre, mediaTitle, 0, mediaArtist, filePath);
                                mediaEntries.Add(m);
                            }
                        }
                    }
                }

            }

            return mediaEntries;
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
