using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Contrib;

namespace SongzaClasses
{
    public static class API
    {
        private const string BaseUrl = "http://songza.com/api/1/";
        private static Track _currentTrack;

        public static async Task<List<Scenario>> ConciergeCategories(int day, int period)
        {
            var request = new RestRequest("situation/targeted");
            string currentDate = DateTime.Now.ToUniversalTime().ToString("o");

            request.AddParameter("current_date", currentDate);
            request.AddParameter("day", day);
            request.AddParameter("period", period);
            request.AddParameter("device", "web");
            request.AddParameter("site", "songza");
            request.AddParameter("optimizer", "default");
            request.AddParameter("max_situations", 5);
            request.AddParameter("max_stations", 3);

            return await Execute<List<Scenario>>(request);
        }

        public static bool IsLoggedIn()
        {
            return Settings.Username != null;
        }

        public static async Task<List<Station>> SimilarStations(Station s)
        {
            var request = new RestRequest(string.Format("station/{0}/similar",s.Id));

            return await Execute<List<Station>>(request);
        }

        public static async Task<List<Favorite>> Favorites()
        {
            var request = new RestRequest(string.Format("collection/user/{0}", Settings.UserId));

            return await Execute<List<Favorite>>(request);
        }

        public static async Task AddToFavorite(string station, string favorite)
        {
            var request = new RestRequest(string.Format("collection/{0}/add-station", favorite));
            request.AddParameter("station", station);
            await Execute<object>(request);
        }

        public static void DayAndPeriod(out int day, out int period)
        {
            day = (int)DateTime.Now.DayOfWeek;

            if (DateTime.Now.Hour < 4)
            {
                period = 5;
                day--;
                if (day < 0)
                    day = 6;
            }
            else if (DateTime.Now.Hour < 10)
                period = 0;
            else if (DateTime.Now.Hour < 12)
                period = 1;
            else if (DateTime.Now.Hour < 18)
                period = 2;
            else if (DateTime.Now.Hour < 21)
                period = 3;
            else if (DateTime.Now.Hour < 23)
                period = 4;
            else
                period = 5;
        }

        private static async void NotifyTrackPlay(string station, string track)
        { 
            var request = new RestRequest(string.Format("station/{0}/song/{1}/notify-play",station,track));
            request.Method = Method.POST;
            await Execute<object>(request);
        }

        public static async void ThumbUpTrack(string station, string track)
        {
            var request = new RestRequest(string.Format("station/{0}/song/{1}/vote/up", station, track));
            request.Method = Method.POST;
            await Execute<object>(request);
        }

        public static async void ThumbDownTrack(string station, string track)
        {
            var request = new RestRequest(string.Format("station/{0}/song/{1}/vote/down", station, track));
            request.Method = Method.POST;
            await Execute<object>(request);
        }

        public static async Task<List<Station>> ListStations(string[] stationids)
        {
            var request = new RestRequest("station/multi");

            foreach (var item in stationids)
            {
                request.AddParameter("id", item);
            }

            return await Execute<List<Station>>(request);
        }

        public static async Task<Station> GetStation(string id)
        {
            var request = new RestRequest(string.Format("station/{0}", id));

            return await Execute<Station>(request);
        }

        public static async Task<List<Station>> Recent()
        {
            var request = new RestRequest(string.Format("user/{0}/stations?limit=40&recent=1", Settings.UserId));
            request.RootElement = "recent";

            var recent = await Execute<Recent>(request);

            return recent.Stations;
        }

        public static async Task<User> Login(string username, string password)
        {
            var request = new RestRequest("login/pw");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.Method = Method.POST;
            request.UserState = "login";

            User resp = await Execute<User>(request);

            if (resp.Id == 0)
                throw new Exception("Login failed");

            SaveUser(username,password,resp.Id.ToString());

            return resp;
        }

        private static void SaveUser(string username, string password, string userid)
        {
            Settings.Username = username;
            Settings.Password = password;
            Settings.UserId = userid;

            Settings.Save();
        }

        public static async Task<Favorite> CreateFavorite(string title)
        {
            var request = new RestRequest(string.Format("collection/user/{0}/create", Settings.UserId));
            request.AddParameter("title", title);
            request.Method = Method.POST;
            return await Execute<Favorite>(request);
        }

        public static async Task<Track> NextTrack(Station station)
        {
            var request = new RestRequest(string.Format("station/{0}/next", station.Id));

            var track = await Execute<Track>(request);

            if (track != null)
            {
                track.Station = station;
                NotifyTrackPlay(station.Id.ToString(), track.Song.Id);
                _currentTrack = track;
            }

            return track;
        }

        public static Station GetCurrentStation()
        {
            if (_currentTrack == null)
                return null;

            return _currentTrack.Station;
        }

        public static Track GetCurrentTrack()
        {
            if (_currentTrack == null)
                return null;

            return _currentTrack;
        }

        public static void GetCurrentStationAndTrack(out Station station, out Track track)
        {
            station = null;
            track = null;

            if (_currentTrack == null)
                return;

            station = _currentTrack.Station;
            track = _currentTrack;
        }

        public static string GetThumbUpState()
        {
            if (_currentTrack == null)
                return null;

            return _currentTrack.ThumbUpState;
        }

        public static void AddThumbUpToTrack()
        {
            if (_currentTrack == null)
                return;

            _currentTrack.ThumbUpState = "1";
        }

        public async static Task<List<Station>> QueryStations(string queryString)
        {
            var request = new RestRequest(string.Format("search/station?query={0}", HttpUtility.UrlEncode(queryString)));

            return await Execute<List<Station>>(request);
        }

        public async static Task<List<Track.Artist>> QueryArtists(string queryString)
        {
            var request = new RestRequest(string.Format("search/artist?query={0}", HttpUtility.UrlEncode(queryString)));

            return await Execute<List<Track.Artist>>(request);
        }

        public async static Task<List<Station>> StationsForArtist(string artistId)
        {
            var request = new RestRequest(string.Format("artist/{0}/stations", artistId));

            return await Execute<List<Station>>(request);
        }

        public async static Task<List<Station>> PopularStations(string tag)
        {
            var request = new RestRequest(string.Format("chart/name/songza/{0}", tag));

            return await Execute<List<Station>>(request);
        }

        public async static Task<List<Category>> Categories()
        {
            var request = new RestRequest("tags");

            return await Execute<List<Category>>(request);
        }

        public async static Task<List<SubCategory>> SubCategories(string id)
        {
            var request = new RestRequest(string.Format("gallery/tag/{0}", id));

            return await Execute<List<SubCategory>>(request);
        }

        private static async Task<T> Execute<T>(RestRequest request) where T : new()
        {
            var tcs = new TaskCompletionSource<T>();
            var client = new RestClient(BaseUrl);

            if (Settings.SessionId != null && DateTime.Now.Subtract(Settings.Created).TotalHours < 24)
                request.AddParameter("sessionid", Settings.SessionId, ParameterType.Cookie);

            request.Timeout = 10000;
            request.RequestFormat = DataFormat.Json;

            client.ExecuteAsync<T>(request, (response) =>
                {
                    if (response == null)
                    {
                        tcs.SetResult(default(T));
                        return;
                    }

                    if (response.ErrorException != null)
                    {
                        if (response.Content == "rate limit exceeded")
                        {
                            tcs.SetResult(default(T));
                            return;
                        }
                        tcs.SetException(response.ErrorException);
                        return;
                    }

                    if (response.ResponseStatus == ResponseStatus.TimedOut)
                    {
                        tcs.SetException(new TimeoutException("The server did not respond in time"));
                        return;
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        tcs.SetException(new IOException("The server returned code: " + response.StatusCode));
                        return;
                    }

                    Parameter p = response.Headers.SingleOrDefault(x => x.Name == "Set-Cookie");

                    if(p != null)
                    {
                        string cookies = (string)p.Value;
                        int index = cookies.IndexOf("sessionid=");
                        if (index >= 0)
                        {
                            string temp = cookies.Substring(index + 10);
                            index = temp.IndexOf(';');
                            string sessionid;
                            if (index >= 0)
                                sessionid = temp.Substring(0, index);
                            else
                                sessionid = temp;

                            if ((request.UserState != null && ((string)request.UserState == "login")) || Settings.SessionId == null || DateTime.Now.Subtract(Settings.Created).TotalHours >= 24)
                            {
                                Settings.SessionId = sessionid;
                                Settings.Created = DateTime.Now;

                                Settings.Save();
                            }
                        }
                        
                    }

                    tcs.SetResult(response.Data);
                });

            return await tcs.Task;
        }
    }
}