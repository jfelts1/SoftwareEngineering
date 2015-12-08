using System;
using System.Windows.Controls;

namespace FinalProjMediaPlayer
{
    public class TimeSliderHandler
    {
        public TimeSliderHandler(ref MediaElement mediaElement,ref Slider slider, ref Label timerLabel)
        {
            _mediaElement = mediaElement;
            _slider = slider;
            _timerLabel = timerLabel;
        }

        public void updateSliderPosition(object source, EventArgs e)
        {
            if (!_mediaElement.IsLoaded || !_mediaElement.NaturalDuration.HasTimeSpan)
            {
                return;
            }
            double mediaDur = _mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            string display = $"{_mediaElement.Position.Minutes,2:D2}:{_mediaElement.Position.Seconds,2:D2}";
            double sliderPos = (_mediaElement.Position.TotalSeconds/mediaDur)*Globals.MaxSliderValue;
            _slider.Value = sliderPos;
            
            _timerLabel.Content = display;
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
                TimeSpan targetPosition = TimeSpan.FromSeconds((_slider.Value/Globals.MaxSliderValue)*mediaDur);
                //MessageBox.Show("mediaDur"+_mediaElement.NaturalDuration.TimeSpan.ToString()+"\ntargetPosition"+targetPosition.ToString());
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
        private readonly Label _timerLabel;
    }
}
