//     Team Ctrl-Alt-Delete

using System.Collections;

namespace FinalProjMediaPlayer.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IListExtensions
    {
        public static IList Clone(this IList listToClone)
        {
            IList ret = new ArrayList();
            foreach (var ele in listToClone)
            {
                ret.Add(ele);
            }
            return ret;
        }
    }
}