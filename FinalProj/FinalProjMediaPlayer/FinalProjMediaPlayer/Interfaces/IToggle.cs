//     James Felts 2015

namespace FinalProjMediaPlayer.Interfaces
{
    //interface for a toggle
    public interface IToggle<in T>
    {
        bool toggle(T offVar, T onVar);
        //forces the toggle to the off aka initial state
        bool forceOff(params T[] par);
        //forces the toggle to the on state
        bool forceOn(params T[] par);

        bool Toggled { get; }

    }

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