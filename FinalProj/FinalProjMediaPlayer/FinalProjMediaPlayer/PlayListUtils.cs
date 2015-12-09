//     Team Ctrl-Alt-Delete

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalProjMediaPlayer
{
    public partial class Playlist
    {
        public static void save(string filename, Playlist playlist)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                //XmlSerializer xmlSerializer = new XmlSerializer(typeof(IList));
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, playlist);
            }
        }

        public static Playlist load(string filename)
        {
            Playlist playlist;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                //XmlSerializer xmlSerializer = new XmlSerializer(typeof(IList));
                BinaryFormatter bf = new BinaryFormatter();
                playlist = bf.Deserialize(fs) as Playlist;
            }
            return playlist;
        }
    }
}