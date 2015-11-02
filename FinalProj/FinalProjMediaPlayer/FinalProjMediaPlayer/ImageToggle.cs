//     James Felts 2015

using System.Windows.Controls;
using System.Windows.Media;

namespace FinalProjMediaPlayer
{
    public class ImageToggle: IToggle
    {
        //the toggle is initalized in the off state
        public ImageToggle(ImageSource i1, ref Image displayedImage)
        {
            _img1 = i1;
            _img2 = displayedImage.Source;
            _displayedImage = displayedImage;
            Toggled = false;
        }

        public bool toggle()
        {
            return Toggled ? forceOff() : forceOn();
        }

        public bool forceOff()
        {
            Toggled = false;
            _displayedImage.Source = _img2;
            return Toggled;
        }

        public bool forceOn()
        {
            Toggled = true;
            _displayedImage.Source = _img1;
            return Toggled; 
        }

        private readonly Image _displayedImage;
        private readonly ImageSource _img1;
        private readonly ImageSource _img2;
        public bool Toggled { get; private set; }
    }
}