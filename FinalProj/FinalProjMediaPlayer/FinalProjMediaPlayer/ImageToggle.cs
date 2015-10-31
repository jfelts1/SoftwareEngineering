//     James Felts 2015

using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FinalProjMediaPlayer
{
    public class ImageToggle
    {
        public ImageToggle(ImageSource i1, ref Image displayedImage)
        {
            _img1 = i1;
            _img2 = displayedImage.Source;
            _displayedImage = displayedImage;
            toggled = false;
        }

        public bool toggle()
        {
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (!toggled)
            {
                _displayedImage.Source = _img1;
            }
            else
            {
                _displayedImage.Source = _img2;
            }
            toggled = !toggled;
            return toggled;
        }

        public bool forceOn()
        {
            toggled = true;
            _displayedImage.Source = _img2;
            return toggled;
        }

        public bool forceOff()
        {
            toggled = false;
            _displayedImage.Source = _img1;
            return toggled; 
        }

        private readonly Image _displayedImage;
        private readonly ImageSource _img1;
        private readonly ImageSource _img2;
        public bool toggled { get; private set; }
    }
}