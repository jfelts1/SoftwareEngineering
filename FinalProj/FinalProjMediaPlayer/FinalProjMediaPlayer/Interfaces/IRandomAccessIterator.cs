//     Team Ctrl-Alt-Delete
namespace FinalProjMediaPlayer.Interfaces
{
    public interface IRandomAccessIterator
    {
        object Current { get; set; }
        bool MoveNext();
        bool MoveNext(int advanceBy);
        bool MovePrev();
        bool MovePrev(int advanceBy);
        void Reset();
    }
}