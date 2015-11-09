//     James Felts 2015

using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    public struct MusicEntry : IMediaEntry
    {
        public MusicEntry(string genre, string title, long length, string artist, string filePath)
        {
            Genre = genre;
            Title = title;
            Length = length;
            Creator = artist;
            FilePath = filePath;
        }

        public string Genre { get; }
        public string Title { get; }
        public long Length { get; }
        public string Creator { get; }
        public string FilePath { get; }
    }
}