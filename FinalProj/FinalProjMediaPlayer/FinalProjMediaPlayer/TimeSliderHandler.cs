using System;
using System.Windows.Controls;

namespace FinalProjMediaPlayer
{
    public class TimeSliderHandler
    {
        public TimeSliderHandler(ref MediaElement mediaElement,ref Slider slider)
        {
            _mediaElement = mediaElement;
            _slider = slider;
            
        }

        public void updateMediaPosition()
        {
            if (!_mediaElement.IsLoaded || !_mediaElement.NaturalDuration.HasTimeSpan)
            {
                return;
            }
            if (_slider.Value > Globals.DoubleTolerance)
            {
                double mediaDur = _mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                TimeSpan targetPosition = TimeSpan.FromSeconds(mediaDur/(_slider.Value/Globals.MaxSliderValue));
                _mediaElement.Position = targetPosition;
            }
            else
            {
                _mediaElement.Position = TimeSpan.Zero;
            }
            //MessageBox.Show(targetPosition.ToString());                
        }

        private readonly MediaElement _mediaElement;
        private readonly Slider _slider;
    }
}
