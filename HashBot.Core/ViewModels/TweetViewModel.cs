using Cirrious.MvvmCross.ViewModels;
using HashBot.Core.Models;

namespace HashBot.Core.ViewModels
{
    public class TweetViewModel : MvxViewModel
    {
        protected override void InitFromBundle(IMvxBundle parameters)
        {
            SelectedTweet = parameters.Read<TweetModel>();
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
                RaisePropertyChanged(() => SelectedTweet);
            }
        }
    }
}
