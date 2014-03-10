using HashBot.Core.Extensions;
using System;

namespace HashBot.Core.Models
{
    public class TweetModel
    {
        public Int64 Id { get; set; }

        public String UserName { get; set; }

        public String ImageUri { get; set; }

        private String _BiggerImageUri;
        public String BiggerImageUri
        {
            get
            {
                if (_BiggerImageUri == null)
                {
                    _BiggerImageUri = ImageUri.Replace("normal", "bigger");
                }
                return _BiggerImageUri;
            }
        }

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
