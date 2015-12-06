//     James Felts 2015

using System.Collections;

namespace FinalProjMediaPlayer.Interfaces
{
    public interface IPlayList : IEnumerable
    {
        IList RawList { get; }
        void loadFromDisk(string filePath);
        void saveToDisk(string filePath,string filename);

    }
}