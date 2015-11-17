//     James Felts 2015

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    public class VolumeHandler:IToggle
    {
        public VolumeHandler(ref MediaElement mediaElement, ref Slider volumeSlider,ref Image pic)
        {
            _mediaElement = mediaElement;
            _volumeSliderToggle = new ImageToggle(
                new BitmapImage(new Uri("pack://application:,,,/Icons/SoundfileNoSound_461.png")),ref pic);
            _lastVolumeValue = Globals.MaxVolume;
            _lastVolumeSliderValue = Globals.MaxSliderValue;
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
            _volumeSliderToggle.forceOn();
            return Toggled;
        }

        public bool forceOn()
        {
            Toggled = true;
            _mediaElement.Volume = _lastVolumeValue;
            _volumeSlider.Value = _lastVolumeSliderValue;
            _volumeSliderToggle.forceOff();
            return Toggled;
        }

        public void setVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double tmp = e.NewValue / Globals.MaxSliderValue;

            if (!(tmp <= 1) || !(tmp >= 0))
            {
                MessageBox.Show("Invalid Volume: " + tmp);
            }

            if (Math.Abs(e.NewValue) < Globals.DoubleTolerance)
            {
                forceOff();
            }
            else
            {
                _lastVolumeValue = tmp;
                _lastVolumeSliderValue = e.NewValue;
                forceOn();
            }
        }

        public bool Toggled { get; private set; }

        private readonly MediaElement _mediaElement;
        private readonly IToggle _volumeSliderToggle;
        private readonly Slider _volumeSlider; 
        private double _lastVolumeValue;
        private double _lastVolumeSliderValue;

    }
}