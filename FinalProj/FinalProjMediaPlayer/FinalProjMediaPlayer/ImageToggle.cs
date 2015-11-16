//     James Felts 2015

using System.Windows.Controls;
using System.Windows.Media;
using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    public class ImageToggle: IToggle
    {
        /// <summary>
        /// Defaults to the off state and the image from displayedImage
        /// </summary>
        public ImageToggle(ImageSource otherImage, ref Image displayedImage)
        {
            _img1 = otherImage;
            _img2 = displayedImage.Source;
            _displayedImage = displayedImage;
            Toggled = false;
        }

        public bool toggle()
        {
            return Toggled ? forceOff() : forceOn();
        }

        public virtual bool forceOff()
        {
            Toggled = false;
            _displayedImage.Source = _img2;
            return Toggled;
        }

        public virtual bool forceOn()
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