using System;
using System.Collections.ObjectModel;

namespace HashBot.Core.Models
{
    public class TagModel : ModelBase
    {
        public TagModel(String aTitle)
        {
            _Title = aTitle;
            _IsLoaded = false;
        }

        private bool _IsLoaded;
        public bool IsLoaded
        {
            get
            {
                return _IsLoaded;
            }
            set
            {
                _IsLoaded = value;
                NotifyPropertyChanged();
            }
        }

        private String _Title;
        public String Title 
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        private ObservableCollection<TweetModel> _RelevantTweets;
        public ObservableCollection<TweetModel> RelevantTweets
        {
            get
            {
                if (_RelevantTweets == null)
                {
                    _RelevantTweets = new ObservableCollection<TweetModel>();
                }
                return _RelevantTweets;
            }
        }

        private TweetModel _SelectedTweet;
        public TweetModel SelectedTweet
        {
            get
            {
                return _SelectedTweet;
            }
            set
            {
                _SelectedTweet = value;
                NotifyPropertyChanged();
            }
        }

        public String NextResults { get; set; }
    }
}
