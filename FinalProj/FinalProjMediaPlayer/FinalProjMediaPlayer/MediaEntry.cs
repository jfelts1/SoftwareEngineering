//     James Felts 2015

namespace FinalProjMediaPlayer
{
    public abstract class MediaEntry
    {
        protected MediaEntry(string genre, string title, long length, string creator, string filePath)
        {
            Genre = genre;
            Title = title;
            Length = length;
            Creator = creator;
            FilePath = filePath;
            _id++;
            Id = _id;
        }

        public string Genre { get; }
        public string Title { get; }
        public long Length { get; }
        public string Creator { get; }
        public string FilePath { get; }
        public int Id { get; }
        public override string ToString()
        {
            return Title;
        }

        private static int _id;
    }
}