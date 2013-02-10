using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LyricsApp
{
    class Lyrics
    {
        String _lyrics;

        public Lyrics(JObject o)
        {
            _lyrics = o["message"]["body"]["lyrics"]["lyrics_body"].ToString();
        }

        public String GetLyrics()
        {
            return _lyrics;
        }
    }


}
