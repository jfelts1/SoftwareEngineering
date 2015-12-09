//     Team Ctrl-Alt-Delete

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    public class VolumeHandler:IToggle
    {
        public VolumeHandler(ref MediaElement mediaElement, ref Slider volumeSlider, ref Image pic,
                             ImageSource otherImage)
        {
            _mediaElement = mediaElement;
            _volumeImageToggle = new ImageToggle(otherImage,ref pic);
            LastOnVolumeValue = Globals.MaxVolume;
            LastOnVolumeSliderValue = Globals.MaxSliderValue;
            _volumeSlider = volumeSlider;
            forceOn();
        }
        
        public bool toggle()
        {
            return Toggled ? forceOff() : forceOn();
        }

        public bool forceOff()
        {
            Toggled = false;
            _mediaElement.Volume = 0;
            _volumeSlider.Value = 0;
            _volumeImageToggle.forceOn();
            return Toggled;
        }

        public bool forceOn()
        {
            Toggled = true;
            _mediaElement.Volume = LastOnVolumeValue;
            _volumeSlider.Value = LastOnVolumeSliderValue;
            _volumeImageToggle.forceOff();
            return Toggled;
        }

        /// <summary>
        /// converts a slider Position double to a volume level.
        /// throws:
        ///     ArgumentException
        /// </summary>
        public static double convertSliderPosToVolLevel(double val)
        {
            if (!(val >= 0) || !(val <= Globals.MaxSliderValue))
            {
                throw new ArgumentException(
                    $"Invalid Slider Position: {val}. Values must be greater than or equal to 0 and less than or equal to {Globals.MaxSliderValue}.");
            }
            return val / Globals.MaxSliderValue;
        }

        /// <summary>
        /// sets the Volume of the media player.
        /// throws:
        ///     ArgumentException
        /// </summary>
        public void setVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double tmp = convertSliderPosToVolLevel(e.NewValue);

            if (!(tmp >= 0) || !(tmp <= Globals.MaxVolume)) 
            {
                throw new ArgumentException(
                    $"Invalid Volume: {tmp}. Values must be greater than or equal to 0 and less than or equal to {Globals.MaxVolume}.");
            }

            if (Math.Abs(e.NewValue) < Globals.DoubleTolerance)
            {
                forceOff();
            }
            else
            {
                LastOnVolumeValue = tmp;
                LastOnVolumeSliderValue = e.NewValue;
                forceOn();
            }
        }

        public bool Toggled { get; private set; }

        private readonly MediaElement _mediaElement;
        private readonly IToggle _volumeImageToggle;
        private readonly Slider _volumeSlider;
        public double LastOnVolumeValue { get; private set; }
        public double LastOnVolumeSliderValue { get; private set; }
    }
}