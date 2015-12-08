//     James Felts 2015

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    [Serializable]
    public partial class Playlist : IEnumerable, IRandomAccessIterator
    {
        public Playlist(IEnumerable collection)
        {
            RawList = new ArrayList();
            foreach (var ele in collection)
            {
                RawList.Add(ele);
            }
            Current = RawList[_index];
        }

        public Playlist(IEnumerable<string> collection)
        {
            RawList = new ArrayList();
            foreach (var ele in collection)
            {
                RawList.Add(ele);
            }
            Current = RawList[_index];
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

        public bool MoveNext()
        {
            return MoveNext(1);
        }

        public bool MoveNext(int advanceBy)
        {
            _index+=advanceBy;
            if (_index >= RawList.Count)
            {
                return false;
            }
            Current = RawList[_index];
            return true;
        }

        public bool MovePrev()
        {
            return MovePrev(1);
        }

        public bool MovePrev(int advanceBy)
        {
            _index-=advanceBy;
            if (_index <= 0)
            {
                return false;
            }
            Current = RawList[_index];
            return true;
        }

        public void Reset()
        {
            _index = 0;
            Current = RawList[_index];
        }

        private IList RawList
        {
            get
            {
                return _rawList;
            }
            set
            {
                _rawList = value;
            }
        }

        public object Current {
            get
            {
                return _current;
            }
            set
            {
                _index = RawList.IndexOf(value);
                _current = value;
            }
        }

        [NonSerialized]
        private int _index;
        [NonSerialized]
        private object _current;
        private IList _rawList;
    }
}