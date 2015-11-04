//     James Felts 2015

namespace FinalProjMediaPlayer.Interfaces
{
    public interface IMediaEntry
    {
        string Genre { get; }
        string Title { get; }
        long Length { get; }
        string Creator { get; }
        string FilePath { get; }
    }
}