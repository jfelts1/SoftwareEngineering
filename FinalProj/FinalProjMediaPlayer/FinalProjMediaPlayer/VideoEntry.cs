//     James Felts 2015

using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    public struct VideoEntry:IMediaEntry
    {
        public VideoEntry(string genre, string title, long length, string publisher, string filePath)
        {
            Genre = genre;
            Title = title;
            Length = length;
            Creator = publisher;
            FilePath = filePath;
        }
        public string Genre { get; }
        public string Title { get; }
        public long Length { get; }
        public string Creator { get; }
        public string FilePath { get; }
    }
}