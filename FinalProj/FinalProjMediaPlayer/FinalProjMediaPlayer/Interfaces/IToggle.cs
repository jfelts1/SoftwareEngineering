//     James Felts 2015

namespace FinalProjMediaPlayer.Interfaces
{
    //interface for a toggle
    public interface IToggle
    {
        bool toggle();
        //forces the toggle to the off aka initial state
        bool forceOff();
        //forces the toggle to the on state
        bool forceOn();

        bool Toggled { get; }

    }
}