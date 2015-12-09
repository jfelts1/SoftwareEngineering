//     Team Ctrl-Alt-Delete

namespace FinalProjMediaPlayer
{
    public abstract class MediaEntry
    {
        protected MediaEntry(string genre, string title, ulong length, string creator, string filePath)
        {
            Genre = genre;
            Title = title;
            Length = length;
            Creator = creator;
            FilePath = filePath;
        }

        public string Genre { get; }
        public string Title { get; }
        public ulong Length { get; }
        public string Creator { get; }
        public string FilePath { get; }

        public override string ToString()
        {
            return Title;
        }
    }
}