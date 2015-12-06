//     James Felts 2015

using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    public class PlayList : IPlayList
    {
        public PlayList(IList rawList)
        {
            RawList = rawList;
        }

        public PlayList(string filePath)
        {
            loadFromDisk(filePath);
        }
        
        public IList RawList { get; private set; }

        public void loadFromDisk(string filePath)
        {
            using (FileStream fs = new FileStream(filePath,FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                RawList = bf.Deserialize(fs) as IList;
            }
        }

        public void saveToDisk(string filePath, string filename)
        {
            using (FileStream fs = new FileStream(filePath+"/"+filename,FileMode.Create))
            {
                   BinaryFormatter bf = new BinaryFormatter();
                   bf.Serialize(fs,RawList);
            }
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            foreach (var ele in RawList)
            {
                ret.Append(ele.ToString() + '\n');
            }
            return ret.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return RawList.GetEnumerator();
        }
    }
}