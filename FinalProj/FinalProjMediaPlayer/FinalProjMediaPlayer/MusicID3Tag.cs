namespace FinalProjMediaPlayer
{
    public class MusicID3Tag
    {
        public readonly byte[] tagid = new byte[3];      //  3
        public readonly byte[] title = new byte[30];     //  30
        public readonly byte[] artist = new byte[30];    //  30 
        public readonly byte[] album = new byte[30];     //  30 
        public readonly byte[] year = new byte[4];       //  4 
        public readonly byte[] comment = new byte[30];   //  30 
        public readonly byte[] genre = new byte[1];      //  1
    }
}
