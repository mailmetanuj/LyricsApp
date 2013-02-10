using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LyricsApp
{
    class TrackInformation
    {
        String _track_id, _track_mbid, _track_name, _track_rating, _track_length, _commontrack_id, _lyrics_id, _subtitles_id, _album_id, _album_name, _artist_id, _artist_mbid, _artist_name;
        String _instrumental, _has_lyrics, _has_subtitles;
        String _album_coverart_100, _album_coverart_350, _album_coverart_500, _album_coverart_800, _updated_time;

        public TrackInformation()
        {
 
        }
        public TrackInformation(JObject o, int i)
        {
            try
            {
                _track_id = o["message"]["body"]["track_list"][i]["track"]["track_id"].ToString();
                _track_mbid = o["message"]["body"]["track_list"][i]["track"]["track_mbid"].ToString();
                _track_name = o["message"]["body"]["track_list"][i]["track"]["track_name"].ToString();
                _track_rating = o["message"]["body"]["track_list"][i]["track"]["track_rating"].ToString();
                _track_length = o["message"]["body"]["track_list"][i]["track"]["track_length"].ToString();
                _commontrack_id = o["message"]["body"]["track_list"][i]["track"]["commontrack_id"].ToString();
                _instrumental = o["message"]["body"]["track_list"][i]["track"]["instrumental"].ToString();
                _has_lyrics = o["message"]["body"]["track_list"][i]["track"]["has_lyrics"].ToString();
                _has_subtitles = o["message"]["body"]["track_list"][i]["track"]["has_subtitles"].ToString();
                _lyrics_id = o["message"]["body"]["track_list"][i]["track"]["lyrics_id"].ToString();
                _subtitles_id = o["message"]["body"]["track_list"][i]["track"]["subtitle_id"].ToString();
                _album_id = o["message"]["body"]["track_list"][i]["track"]["album_id"].ToString();
                _album_name = o["message"]["body"]["track_list"][i]["track"]["album_name"].ToString();
                _artist_id = o["message"]["body"]["track_list"][i]["track"]["artist_id"].ToString();
                _artist_mbid = o["message"]["body"]["track_list"][i]["track"]["artist_mbid"].ToString();
                _artist_name = o["message"]["body"]["track_list"][i]["track"]["artist_name"].ToString();

                _album_coverart_100 = o["message"]["body"]["track_list"][i]["track"]["album_coverart_100x100"].ToString();
                _album_coverart_350 = o["message"]["body"]["track_list"][i]["track"]["album_coverart_350x350"].ToString();
                _album_coverart_500 = o["message"]["body"]["track_list"][i]["track"]["album_coverart_500x500"].ToString();
                _album_coverart_800 = o["message"]["body"]["track_list"][i]["track"]["album_coverart_800x800"].ToString();
                _updated_time = o["message"]["body"]["track_list"][i]["track"]["updated_time"].ToString();
            }
            catch (Exception e)
            {

            }
        }

            public String TrackId()
            {
                return _track_id;
            }

            public String TrackMBID()
            {
                return _track_mbid;
            }

            public String TrackName()
            {
                return _track_name;
            }

            public String TrackRating()
            {
                return _track_rating;
            }

            public String TrackLength ()
            {
                return _track_length;
            }
            
            public String TrackCommonID ()
            {
                return _commontrack_id;
            }

            public String LyricsID ()
            {
                return _lyrics_id ;
            }

            public String SubtitlesID ()
            {
                return _subtitles_id;
            }    

            public String AlbumID ()
            {
                return _album_id;
            }

            public String AlbumName ()
            {
                return _album_name;
            }

            public String ArtistID ()
            {
                return _artist_id;
            }

            public String ArtistMBID ()
            {
                return _artist_mbid;
            }

            public String ArtistName ()
            {
                return _artist_name;
            }

            public bool IsInstrumental()
            {
                if(_instrumental=="1")
                    return true;
                else return false;
            }

            public bool HasLyrics()
            {
                if(_has_lyrics=="1")
                    return true;
                else return false;
            }

            public bool HasSubtitles()
            {
                if(_has_subtitles=="1")
                    return true;
                else return false;
            }
        
            public String AlbumCoverArt_100by100()
            {
                return _album_coverart_100;
            }

            public String AlbumCoverArt_350by350()
            {
                return _album_coverart_350;
            }
            
            public String AlbumCoverArt_500by500()
            {
                return _album_coverart_500;
            }
            
            public String AlbumCoverArt_800by800()
            {
                return _album_coverart_800;
            }
      
            public String UpdatedTime()
            {
                return _updated_time;
            }
        
        }
}

