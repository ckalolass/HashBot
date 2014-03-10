using Cirrious.MvvmCross.ViewModels;
using HashBot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
