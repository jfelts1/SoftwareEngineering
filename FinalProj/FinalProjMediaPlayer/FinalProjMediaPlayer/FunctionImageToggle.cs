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
        /// Defaults to the off state and the image from displayedImage does not call actOff during construction
        /// </summary>
        public FunctionImageToggle(ImageSource otherImage, ref Image displayedImage, Action funcOff, Action funcOn) :
            base(otherImage, ref displayedImage)
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
    /// <summary>
    /// Ties two images and two functions together so that when one image is displayed a specific function is run once
    /// </summary>
    public class FunctionImageToggle<T> : ImageToggle<T>
    {
        /// <summary>
        /// Defaults to the off state and the image from displayedImage does not call actOff during construction
        /// </summary>
        public FunctionImageToggle(ImageSource otherImage, ref Image displayedImage , Action<T> actOff, Action<T> actOn) : 
            base(otherImage,ref displayedImage)
        {
            _actOff = actOff;
            _actOn = actOn;
        }

        public override bool forceOff(params T[] par)
        {
            bool t = base.forceOff();
            _actOff(par[0]);
            return t;
        }

        public override bool forceOn(params T[] par)
        {
            bool t = base.forceOn();
            _actOn(par[0]);
            return t;
        }

        private readonly Action<T> _actOff;
        private readonly Action<T> _actOn;

    }
}