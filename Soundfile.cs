using System;
using System.Collections.Generic;
using System.Text;
using HundredMilesSoftware.UltraID3Lib;

namespace mp3packer
{

    public class Soundfile
    {
        public long ID;
        public long ArtistID;
        public long ArtistTrackNo;
        public string FileName;
        public string Artist;
        public string Album;
        public string Title;
        public string Recorded;
        public string Comment;
        public string Genre;
        public long Length;

        public Soundfile(string newfile)
        {
            FileName = newfile;
        }


        public void RetrieveTagInfo()
        {
            UltraID3 ultraID3 = new UltraID3();
            ultraID3.Read(FileName);
            Title = ultraID3.Title;
            Artist = ultraID3.Artist;
            Album = ultraID3.Album;
            Recorded = ultraID3.Year;
            Comment = ultraID3.Comments;
            Genre = ultraID3.Genre;
            Length = (long)ultraID3.Duration.TotalSeconds;
        }

    }
}
