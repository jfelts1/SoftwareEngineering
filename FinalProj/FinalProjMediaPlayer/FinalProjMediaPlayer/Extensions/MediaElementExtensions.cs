//     Team Ctrl-Alt-Delete

using System;
using System.Windows.Controls;

namespace FinalProjMediaPlayer.Extensions
{
    public static class MediaElementExtensions
    {
        public static void loadMediaEntry(this MediaElement ele,MediaEntry entry)
        {
            ele.Source = new Uri(entry.FilePath);
        }
    }
}