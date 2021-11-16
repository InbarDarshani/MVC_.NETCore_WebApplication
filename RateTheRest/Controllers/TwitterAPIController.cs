using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TweetSharp;

namespace RateTheRest.Controllers
{
    public class TwitterAPIController : Controller
    {
        public static string _ConsumerKey = "qSL9cW9ZMtq4msEGjm7rpcvUa";
        public static string _ConsumerSecret = "DqrYfVvYuPB2tlCaEoHcgANRp12u5VrCjzwaVvTPA70PqgBljh";
        public static string _AccessToken = "921758864139317248-9cD6pJlC4qyY0nWvqlEKiaxvknD7AbO";
        public static string _AccessTokenSecret = "4eGDqUww3v3JyDmOZu1oSwxBPnN4es58IuDcfeHQeRBOn";

        //public static string _ConsumerKey = "Xtjop5XKh3jVDZq6fnQPehWGf";
        //public static string _ConsumerSecret = "wN4veDeE4BTyaxFJqzT5M0cvwvFFbtDruamAoMkumwFYGaSJwV";
        //public static string _AccessToken = "921758864139317248-9K5zoeuhOzDUGBknY6fSplb9wFRkTK2";
        //public static string _AccessTokenSecret = "9gcG30e4HWFB1UgbjnhuXNfp6W8MlcKqC24wkm0gTUPWV";
        
        protected TwitterSearchResult GetTwitterSearchResult()
        {
            var tweets_search = new SearchOptions { Q = "#restaurant", Resulttype = TwitterSearchResultType.Popular };
            TwitterService twService = new TwitterService(_ConsumerKey, _ConsumerSecret, _AccessToken, _AccessTokenSecret);
            TwitterSearchResult searchResult = twService.Search(tweets_search);
            return searchResult;
        }

        public IActionResult Index()
        {
            var statues = GetTwitterSearchResult();
            if (statues != null)
            {
                List<TwitterStatus> tweetList = statues.Statuses.ToList();
                return View(tweetList);
            }
            return View("TwitterError");
        }
    }
}
