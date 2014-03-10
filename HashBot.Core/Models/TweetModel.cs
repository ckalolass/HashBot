using HashBot.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashBot.Core.Models
{
    public class TweetModel
    {
        public Int64 Id { get; set; }

        public String UserName { get; set; }

        public String ImageUri { get; set; }

        public String TweetDate { get; set; }

        private DateTime? _TweetDateTime;
        public DateTime? TweetDatetime
        {
            get
            {
                if (!_TweetDateTime.HasValue && !String.IsNullOrEmpty(TweetDate))
                {
                    _TweetDateTime = DateTimeExtensions.ParseTwitterDate(TweetDate);
                }
                return _TweetDateTime;
            }
        }

        public String TimeAgo
        {
            get
            {
                if (TweetDatetime.HasValue)
                {
                    return TweetDatetime.Value.TimeAgo();
                }
                return null;
            }
        }

        public String Text { get; set; }

        public String ShortText
        {
            get
            {
                if (Text.Length > 30)
                {
                    return String.Concat(Text.Substring(0, 30), "...");
                }
                return Text;
            }
        }

        public String Source { get; set; }

        public String Link { get; set; }
    }
}
