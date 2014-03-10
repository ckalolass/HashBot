using HashBot.Core.Extensions;
using HashBot.Core.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace HashBot.Core.Services
{
    public class TwitterService : ITwitterService, INotifyPropertyChanged, IBusy
    {
        private readonly String _baseAddress = "https://api.twitter.com/";
        private readonly String _appOnlyAuth = "oauth2/token";
        private readonly String _searchTweets = "1.1/search/tweets.json";
        private readonly String _searchTweetsSeg = "1.1/search/tweets.json{0}";
        private readonly String _consumerKey;
        private readonly String _consumerSecret;
        private String _bearerToken;

        private readonly RestClient _client;

        public TwitterService(string consumerKey, string consumerSecret)
        {
            _bearerToken = IsolatedStorageSettings.ApplicationSettings.LoadSetting<string>("bearerToken");
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _client = new RestClient(_baseAddress);
            _IsBusy = false;
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
        }

        private bool _IsAuthenticated
        {
            get
            {
                return _bearerToken != null;
            }
        }

        private void AddBearerToken(IRestRequest req)
        {
            req.AddHeader("Authorization", String.Format("Bearer {0}", _bearerToken));
        }

        private void AddBasicAuth(IRestRequest req)
        {
            String credentials =
            String.Format("Basic {0}", System.Convert.ToBase64String(
                Encoding.UTF8.GetBytes(
                    String.Format("{0}:{1}",
                        HttpUtility.UrlEncode(_consumerKey),
                        HttpUtility.UrlEncode(_consumerSecret)
                        )
                    )
                  )
               );
            req.AddHeader("Authorization", credentials);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            req.AddParameter("grant_type", "client_credentials");
        }

        private async Task<bool> Authenticate()
        {
            RestRequest req = new RestRequest(_appOnlyAuth, Method.POST);
            AddBasicAuth(req);
            var response = await Execute(req);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject responseData = JObject.Parse(response.Content);
                _bearerToken = responseData["access_token"].Value<string>();
                IsolatedStorageSettings.ApplicationSettings.SaveSetting("bearerToken", _bearerToken);
                return true;
            }
            return false;
        }

        private async Task<IRestResponse> Execute(IRestRequest req)
        {
            req.RequestFormat = DataFormat.Json;
            SetBusy(true);
            var response = await _client.GetResponseContentAsync(req);
            SetBusy(false);
            return response;
        }

        private async Task<IRestResponse<T>> Execute<T>(IRestRequest req) where T : new()
        {
            req.RequestFormat = DataFormat.Json;
            SetBusy(true);
            var response = await _client.GetResponseContentAsync<T>(req);
            SetBusy(false);
            return response;
        }

        public async Task<IEnumerable<TweetModel>> Search(TagModel tag)
        {
            if (_IsAuthenticated || await Authenticate())
            {
                IRestRequest req;
                if (tag.NextResults != null)
                {
                    req = new RestRequest(String.Format(_searchTweetsSeg, tag.NextResults), Method.GET);
                }
                else
                {
                    req = new RestRequest(_searchTweets, Method.GET);
                    req.AddParameter("q", tag.Title);
                }
                AddBearerToken(req);
                var resp = await Execute(req);
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    return ParseTweets(tag, resp.Content);
                }
            }
            return null;
        }

        public IEnumerable<TweetModel> ParseTweets(TagModel tag, string content)
        {
            JObject jo = JObject.Parse(content);
            TweetModel tm = null;
            foreach (var i in jo["statuses"].Children())
            {
                tm = new TweetModel();
                //tm.Id = i["id"].Value<Int64>();
                //tm.Source = i["source"].Value<String>();
                //tm.Text = i["text"].Value<String>();
                //tm.TweetDate = i["created_at"].Value<String>();
                //tm.UserName = i["user"]["name"].Value<String>();
                //tm.ImageUri = i["user"]["profile_image_url"].Value<String>();
                tm.Id = i.GetValueOrDefault<Int64>("id");
                tm.Source = i.GetValueOrDefault<String>("source");
                tm.Text = i.GetValueOrDefault<String>("text");
                tm.TweetDate = i.GetValueOrDefault<String>("created_at");
                tm.UserName = i.GetValueOrDefault<String>("user", "name");
                tm.ImageUri = i.GetValueOrDefault<String>("user", "profile_image_url");
                yield return tm;
            }
            //DateTime date = DateTimeExtensions.ParseTwitterDate(tm.TweetDate);
            tag.NextResults = jo.GetValueOrDefault<String>("search_metadata", "next_results") ??
                String.Format("?max_id={0}&q={1}", (tm.Id - 1).ToString(), HttpUtility.UrlEncode(tag.Title));
        }

        public void SetBusy(bool value)
        {
            _IsBusy = value;
            NotifyPropertyChanged("IsBusy");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
