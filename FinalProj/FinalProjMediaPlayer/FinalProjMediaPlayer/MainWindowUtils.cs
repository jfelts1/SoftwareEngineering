//     James Felts 2015
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FinalProjMediaPlayer.Extensions;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell;

namespace FinalProjMediaPlayer
{
    public partial class MainWindow
    {
        private void initListBoxValues(IEnumerable<MediaEntry> mediaEntries, ItemsControl box)
        {
            foreach (var entry in mediaEntries)
            {
                box.Items.Add(entry.ToString());
                _mediaDict.Add(entry.ToString(), entry);
            }
        }

        private void populateListBox(IEnumerable playlist, ItemsControl box)
        {
            box.Items.Clear();
            foreach (var media in playlist)
            {
                if (_mediaDict.ContainsKey(media.ToString()))
                {                    
                    box.Items.Add(media.ToString());
                }
                else
                {
                    MessageBox.Show("Media file not in database. Not adding to playlist."+media);
                }
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
                    ShellFile fin = ShellFile.FromFilePath(filePath);
                    ulong? test = fin.Properties.System.Media.Duration.Value;
                    ulong len = convertNanoSecondsToMiliSeconds(test);

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

                    MusicEntry m = new MusicEntry(genre, title, len, artist, filePath);
                    mediaEntries.Add(m);
                }
            }
            return mediaEntries;
        }

        private static ulong convertNanoSecondsToMiliSeconds(ulong? val)
        {
            if (val != null)
            {
                return (ulong) ((double) val*0.0001);
            }
            throw new FormatException("val does not reprsent a numeric value.");
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

        private void enterPlayList(object sender, RoutedEventArgs e)
        {
            IList selected = ListBoxMainWindowRecentlyPlayed.SelectedItems.Clone();
            
            Playlist playlist = new Playlist(selected);
            
            populateListBox(playlist,ListBoxMainWindowRecentlyPlayed);
            _currentPlaylist = playlist;
        }

        private void savePlayList(object sender, RoutedEventArgs e)
        {
            if (_currentPlaylist == null)
            {
                MessageBox.Show("There is no playlist to save.");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "playlist",
                DefaultExt = ".playlist",
                Filter = "Playlists (.playlist)|*.playlist"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result != true)
            {
                //MessageBox.Show("Unable to save playlist.");
                return;
            }
            string filename = saveFileDialog.FileName;
            Playlist.save(filename,_currentPlaylist);
        }

        private void loadPlayList(object sender, RoutedEventArgs e)
        {         
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileName = "playlist",
                DefaultExt = ".playlist",
                Filter = "Playlists (.playlist)|*.playlist"
            };

            bool? result = openFileDialog.ShowDialog();

            if (result != true)
            {
                //MessageBox.Show("Unable to open playlist.");
                return;
            }
            string filename = openFileDialog.FileName;

            _currentPlaylist = Playlist.load(filename);
            populateListBox(_currentPlaylist,ListBoxMainWindowRecentlyPlayed);
        }

        private void playMedia(string s)
        {
            if (s != null)
            {
                MediaEntry selectedEntry = _mediaDict[s];
                MediaElementMainWindow.loadMediaEntry(selectedEntry);
                MediaElementMainWindow.Play();
                _currentlyPlaying = selectedEntry;
            }
            else
            {
                throw new TypeAccessException("selectedValue is not a string");
            }
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand EnterPlaylist = new RoutedUICommand("EnterPlaylist",
            "enterPlayList",
            typeof (CustomCommands),
            new InputGestureCollection() {new KeyGesture(Key.Enter, ModifierKeys.None)});

        public static readonly RoutedUICommand SavePlaylist = new RoutedUICommand("SavePlaylist",
            "savePlayList",
            typeof (CustomCommands),
            new InputGestureCollection() {new KeyGesture(Key.S, ModifierKeys.Control)});

        public static readonly RoutedUICommand LoadPlaylist = new RoutedUICommand("LoadPlaylist",
            "loadPlayList",
            typeof (CustomCommands),
            new InputGestureCollection() {new KeyGesture(Key.O, ModifierKeys.Control)});
    }
}