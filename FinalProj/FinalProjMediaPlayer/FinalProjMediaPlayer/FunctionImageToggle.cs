//     James Felts 2015

using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalProjMediaPlayer
{
    /// <summary>
    /// Ties two images and two functions together so that when one image is displayed a specific function is run once
    /// </summary>
    public class FunctionImageToggle : ImageToggle
    {
        /// <summary>
        /// Defaults to the off state and the image from displayedImage does not call funcOff during construction
        /// </summary>
        public FunctionImageToggle(ImageSource otherImage, ref Image displayedImage , Action funcOff, Action funcOn) : 
            base(otherImage,ref displayedImage)
        {
            _funcOff = funcOff;
            _funcOn = funcOn;
        }

        public override bool forceOff()
        {
            bool t = base.forceOff();
            _funcOff();
            return t;
        }

        public override bool forceOn()
        {
            bool t = base.forceOn();
            _funcOn();
            return t;
        }

        private readonly Action _funcOff;
        private readonly Action _funcOn;
    }
}