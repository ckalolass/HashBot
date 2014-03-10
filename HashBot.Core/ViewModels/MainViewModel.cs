using Cirrious.MvvmCross.ViewModels;
using HashBot.Core.Models;
using HashBot.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HashBot.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public MainViewModel()
            : this(new TwitterService("TaZxhiQhd80cHJUURZQ8Q", "ZnoVMMk3fIS6yt4LMV7DLWfPdLiEaMgXMQRbC7C5XPI"))
        {

        }

        MainViewModel(ITwitterService twitterService)
        {
            _twitterService = twitterService;
            _RootTags = LoadRoot();
        }

        ITwitterService _twitterService;

        public IBusy BusyService
        {
            get
            {
                return (IBusy)_twitterService;
            }
        }

        private IEnumerable<TagModel> _RootTags;
        public IEnumerable<TagModel> RootTags
        {
            get
            {
                return _RootTags;
            }
        }

        private IEnumerable<TagModel> LoadRoot()
        {
            yield return new TagModel("#WP8");
            yield return new TagModel("#Dribbble");
            yield return new TagModel("#Twitter");
            yield return new TagModel("#GitHub");
        }

        public ICommand GoToInfoCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<InfoViewModel>();
                });
            }
        }

        public ICommand GoToTweetCommand
        {
            get
            {
                return new MvxCommand<TweetModel>(tm =>
                {
                    ShowViewModel<TweetViewModel>(tm);
                });
            }
        }

        public ICommand LoadTweetsCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    await LoadTweets();
                });
            }
        }

        private TagModel _SelectedTag;
        public TagModel SelectedTag
        {
            get
            {
                return _SelectedTag;
            }
            set
            {
                _SelectedTag = value;
                if (!_SelectedTag.IsLoaded)
                {
                    LoadTweets().ContinueWith(
                        (a) => 
                            {
                                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                {
                                    if (_SelectedTag.RelevantTweets.Count > 0)
                                        _SelectedTag.IsLoaded = true;
                                });
                            });
                }
            }
        }

        private async Task LoadTweets()
        {
            var v = await _twitterService.Search(SelectedTag).ConfigureAwait(false);
            if (v != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        foreach (var i in v)
                        {
                            SelectedTag.RelevantTweets.Add(i);
                        }
                    });
            }
        }
    }

}
