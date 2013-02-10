using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LyricsApp
{
    class Header
    {
        private String _StatusCode, _ExecuteTime, _Available;

        public Header(JObject o)
        {
            _StatusCode = o["message"]["header"]["status_code"].ToString();
            _ExecuteTime = o["message"]["header"]["execute_time"].ToString();
            _Available = o["message"]["header"]["available"].ToString();
        }

        public String StatusCode()
        {
            return _StatusCode;
        }

        

        public String ExecuteTime()
        {
            return _ExecuteTime;
        }

        

        public String Available()
        {
            return _Available;
        }
    }
}
